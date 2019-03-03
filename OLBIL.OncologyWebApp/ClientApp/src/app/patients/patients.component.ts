import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  patient: any = {
    person: {}
  };
  isSaving: boolean = false;

  columnDefs = [
    { headerName: 'Identidad Nacional', field: 'governmentIDNumber' },
    { headerName: 'Nombre', field: 'firstName' },
    { headerName: 'Apellido', field: 'lastName' },
    { headerName: 'Nacionalidad', field: 'nationality' }
  ];

  rowData = [
    {
      governmentId: "0101-1990-00201",
      firstName: "Adan",
      lastName: "Fernandez",
      nationality: "Hondureña"
    },
    {
      governmentId: "0101-1990-00202",
      firstName: "Adan",
      lastName: "Fernandez",
      nationality: "Nicaraguense"
    },
    {
      governmentId: "0101-1990-00203",
      firstName: "Adan",
      lastName: "Fernandez",
      nationality: "Salvadoreña"
    },
    {
      governmentId: "0101-1990-00203",
      firstName: "Adan",
      lastName: "Fernandez",
      nationality: "Salvadoreña"
    }
  ];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getRegistered() {
    this.http.get<any>(this.baseUrl + 'api/oncologyPatient/').subscribe(result => {
      this.rowData = result.items.map(r => {
        return {...r, ...r.person}
      });
    }, error => console.error(error));

  }

  createPatient(regForm) {
    this.isSaving = true;
    this.http.post<any>(this.baseUrl + 'api/oncologyPatient/', this.patient).subscribe(res => {
      this.patient = { person: {} }
      this.isSaving = false;
      this.getRegistered();
    }, er => {
      console.error(er)
      this.isSaving = false;
    });
  }

  ngOnInit() {
    this.getRegistered();
  }
}
