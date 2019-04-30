import { Component, OnInit } from '@angular/core';
import { HospitalUnitModel, HospitalUnitsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { ColDef, GridOptions } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-units-list',
  templateUrl: './units-list.component.html',
  styleUrls: ['./units-list.component.css']
})
export class UnitsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: HospitalUnitModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'Unit ID', field: 'hospitalUnitId' },
    {
      headerName: 'Código', field: 'code',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.hospitalUnitId,
          value: data.code
        });
      }
    },
    { headerName: 'Nombre', field: 'name' },
  ];

  constructor(
    private client: HospitalUnitsClient
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
