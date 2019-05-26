import { Component, OnInit } from '@angular/core';
import { DiagnosisModel, DiagnosesClient } from '../../api-clients';
import { ColDef, GridOptions, GridApi, ColumnApi } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-diagnoses-list',
  templateUrl: './diagnoses-list.component.html',
  styleUrls: ['./diagnoses-list.component.css']
})
export class DiagnosesListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: DiagnosisModel[] = [];
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
    { headerName: 'Diagnóstico ID', field: 'diagnosisId' },
    {
      headerName: 'Código CIE', field: 'icdCode',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.diagnosisId,
          value: data.icdCode
        });
      }
    },
    { headerName: 'Descripción corta', field: 'shortDescriptor' },
    { headerName: 'Descripción larga', field: 'completeDescriptor' },
  ];

  constructor(
    private client: DiagnosesClient
  ) { }

  ngOnInit() {
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true;
    this.client.getAll(this.getParams.sortInfo, this.getParams.pageIndex, this.getParams.pageSize)
        .subscribe(result => {
            this.rowData = result.items;
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
