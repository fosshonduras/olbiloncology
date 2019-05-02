import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { WardsClient, WardModel } from '../../api-clients';
import { ColDef, GridOptions } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-wards-list',
  templateUrl: './wards-list.component.html',
  styleUrls: ['./wards-list.component.css']
})
export class WardsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: WardModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'Sala ID', field: 'wardId' },
    {
      headerName: 'Nombre', field: 'name',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.wardId,
          value: data.name
        });
      }
    },
    { headerName: 'Unidad', field: 'hospitalUnitId' },
    { headerName: 'Edificio', field: 'buildingId' },
    { headerName: 'Nivel', field: 'floorNumber' },
    { headerName: 'Género', field: 'wardGenderId' }
  ];

  constructor(
    private client: WardsClient
  ) { }

  ngOnInit() {
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true;    this.getParams.sortInfo.push({ "shortDescriptor": true });
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
