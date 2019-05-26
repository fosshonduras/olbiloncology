import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { OncologyPatientsClient } from '../../api-clients';
import { ColDef, GridOptions, ColumnApi, GridApi } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';
import { renderDate } from '../../common/AgGridRenderers';

@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent implements OnInit {
  isLoading: boolean = true;
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
    {
      headerName: 'Identidad Nacional', field: 'governmentIDNumber',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `../edit`,
          routeParam: data.oncologyPatientId,
          value: data.person.governmentIDNumber
        });
      }
    },
    { headerName: 'Nombre', field: 'firstName' },
    { headerName: 'Apellido', field: 'lastName' },
    { headerName: 'Nacionalidad', field: 'nationality' },
    {
      headerName: 'Fecha de Nacimiento', field: 'birthdate',
      cellRenderer: ({ data }) => renderDate(data.birthdate)
    }
  ];

  rowData: any[] = [ ];

  constructor(private client: OncologyPatientsClient) {

  }

  ngOnInit() {
    this.getRegistered();
  }

  getRegistered() {
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true;    this.getParams.sortInfo.push({ "shortDescriptor": true });

    this.client.getAll(this.getParams.sortInfo, this.getParams.pageIndex, this.getParams.pageSize)
      .subscribe(result => {
        this.rowData = result.items.map(r => {
          return { ...r, ...r.person }
        });
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
