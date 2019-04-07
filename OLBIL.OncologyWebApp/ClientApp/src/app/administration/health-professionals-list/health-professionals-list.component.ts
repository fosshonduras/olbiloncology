import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { HealthProfessionalsClient } from '../../api-clients';

@Component({
  selector: 'app-health-professionals-list',
  templateUrl: './health-professionals-list.component.html',
  styleUrls: ['./health-professionals-list.component.css']
})
export class HealthProfessionalsListComponent implements OnInit {
  isLoading: boolean = false;

  rowData: any[] = [];

  columnDefs = [
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
    { headerName: 'Fecha de Nacimiento', field: 'birthdate' }
  ];

  constructor(
    private client: HealthProfessionalsClient
  ) { }

  getRegistered() {
    this.isLoading = true;
    this.client.getAll()
      .subscribe(result => {
        this.rowData = result.items.map(r => {
          return {...r, ...r.person }
        });
        this.isLoading = false;
      }, error => console.error(error));
  }

  ngOnInit() {
    this.getRegistered();
  }
}
