import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { OncologyPatientsClient } from '../../api-clients';
import { ColDef, GridOptions } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';

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

  gridOptions: GridOptions = {};
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
    { headerName: 'Fecha de Nacimiento', field: 'birthdate' }
  ];

  rowData: any[] = [ ];

  constructor(private client: OncologyPatientsClient) {

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
      }, error => console.error(error));
  }

  ngOnInit() {
    this.getRegistered();
  }

  onPageChanged(newPage: number) {
    this.getParams.pageIndex = newPage;
    this.retrieveData();
  }

}
