import { Component, OnInit } from '@angular/core';
import { BedModel, BedsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { AgGridNg2 } from 'ag-grid-angular';
import { GridOptions, ColDef, ColumnApi, GridApi } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-beds-list',
  templateUrl: './beds-list.component.html',
  styleUrls: ['./beds-list.component.css']
})
export class BedsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: BedModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {
    defaultColDef: {
      resizable: true
    },
    suppressColumnVirtualisation: true
  };
  gridApi: GridApi;
  gridColumnApi: ColumnApi;

  columnDefs: ColDef[] = [
    { headerName: 'Cama ID', field: 'bedId' },
    {
      headerName: 'Nombre', field: 'name',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.bedId,
          value: data.name
        });
      }
    },
    { headerName: 'Sala', field: 'wardName' },
    { headerName: 'Status', field: 'bedStatusName' },
  ];

  constructor(
    private client: BedsClient
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
        this.autoSizeAll();
      }, err => {
        console.log(err);
      })
  }

  autoSizeAll() {
    if (this.gridColumnApi) {
      this.gridColumnApi.autoSizeAllColumns();
    }
  }

  onPageChanged(newPage: number) {
    this.getParams.pageIndex = newPage;
    this.retrieveData();
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
  }

  firstDataRendered(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridColumnApi.autoSizeAllColumns();
  }
}
