import { Component, OnInit } from '@angular/core';
import { AdministrativeDivisionModel, AdministrativeDivisionsClient } from '../../api-clients';
import { GridOptions, ColDef } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-administrative-divisions-list',
  templateUrl: './administrative-divisions-list.component.html',
  styleUrls: ['./administrative-divisions-list.component.css']
})
export class AdministrativeDivisionsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: AdministrativeDivisionModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'División ID', field: 'administrativeDivisionId' },
    {
      headerName: 'Código', field: 'code',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.administrativeDivisionId,
          value: data.code
        });
      }
    },
    { headerName: 'Nombre (Es)', field: 'name' },
    { headerName: 'Nivel', field: 'level' },
    { headerName: 'División Padre', field: 'parentName' },
  ];

  constructor(
    private client: AdministrativeDivisionsClient
  ) { }

  ngOnInit() {
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true;
    this.getParams.sortInfo.push({ "shortDescriptor": true });
    this.client.getAll(this.getParams.sortInfo, this.getParams.pageIndex, this.getParams.pageSize)
      .subscribe(result => {
        this.rowData = result.items;
        this.isLoading = false;
        this.getParams.totalCount = result.totalCount;
      }, err => {
        console.log(err);
      })
  }

  onPageChanged(newPage: number) {
    this.getParams.pageIndex = newPage;
    this.retrieveData();
  }
}
