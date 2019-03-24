import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { OncologyPatientsClient } from '../../api-clients';

@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent implements OnInit {
  isLoading: boolean = true;
  columnDefs = [
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
          this.isLoading = false;
          return { ...r, ...r.person }
        });
      }, error => console.error(error));
  }

  ngOnInit() {
    this.getRegistered();
  }

}
