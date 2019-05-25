import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { RecordStorageLocationsClient, RecordStorageLocationModel } from '../../api-clients';
import { GridOptions, ColDef } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-record-storage-locations-list',
  templateUrl: './record-storage-locations-list.component.html',
  styleUrls: ['./record-storage-locations-list.component.css']
})
export class RecordStorageLocationsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: RecordStorageLocationModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'Ubicacion ID', field: 'recordStorageLocationId' },
    {
      headerName: 'Nombre', field: 'name',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.recordStorageLocationId,
          value: data.name
        });
      }
    },
    { headerName: 'Ubicación Padre', field: 'parentLocationName' },
  ];

  constructor(
    private client: RecordStorageLocationsClient
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
