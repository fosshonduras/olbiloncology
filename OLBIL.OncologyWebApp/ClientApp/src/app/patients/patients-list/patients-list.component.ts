import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { OncologyPatientsClient } from '../../api-clients';
import { ColDef, GridOptions } from 'ag-grid-community';

@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent implements OnInit {
  isLoading: boolean = true;
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
    this.isLoading = true;
    this.client.getAll()
      .subscribe(result => {
        this.rowData = result.items.map(r => {
          return { ...r, ...r.person }
        });
        this.isLoading = false;
      }, error => console.error(error));
  }

  ngOnInit() {
    this.getRegistered();
  }

}
