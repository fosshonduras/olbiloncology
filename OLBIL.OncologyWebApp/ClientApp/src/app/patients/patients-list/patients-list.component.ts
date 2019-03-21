import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { OncologyPatientClient } from 'src/app/api-clients';

@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent implements OnInit {
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

  //constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  constructor(private client: OncologyPatientClient) {

  }

  getRegistered() {
    this.client.getAll()
      //this.http.get<any>(this.baseUrl + 'api/oncologyPatient/')
      .subscribe(result => {
        this.rowData = result.items.map(r => {
          return { ...r, ...r.person }
        });
      }, error => console.error(error));
  }

  ngOnInit() {
    this.getRegistered();
  }

}
