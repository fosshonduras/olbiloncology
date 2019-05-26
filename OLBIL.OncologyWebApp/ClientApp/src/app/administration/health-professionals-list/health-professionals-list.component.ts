import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { HealthProfessionalsClient } from '../../api-clients';
import { ColDef, GridOptions, ColumnApi, GridApi } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';
import { renderDate } from '../../common/AgGridRenderers';

@Component({
  selector: 'app-health-professionals-list',
  templateUrl: './health-professionals-list.component.html',
  styleUrls: ['./health-professionals-list.component.css']
})
export class HealthProfessionalsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: any[] = [];
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
          inRouterLink: `./`,
          routeParam: data.healthProfessionalId,
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

  constructor(
    private client: HealthProfessionalsClient
  ) { }

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
          return {...r, ...r.person }
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
