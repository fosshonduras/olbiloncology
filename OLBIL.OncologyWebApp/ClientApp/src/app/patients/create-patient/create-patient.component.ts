import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.css']
})
export class CreatePatientComponent implements OnInit {
  patient: any = {
    person: {
      firstName: "Karla",
      lastName: "Tulio",
      governmentIDNumber: "01012020",
      nationality: 'Nigeriano',
      birthdate: '2000-12-12'
    }
  };
  newPatient: any = {};
  isSaving: boolean = false;
  showForm: boolean = true;
  showMatchesGrid: boolean = false;
  showEditForm: boolean = false;
  matchingRecords: any[] = [];

  columnDefs = [
    {
      headerName: 'Identidad Nacional', field: 'governmentIDNumber', cellRendererFramework: LinkRendererComponent,
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

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  submitRegistrationAttempt(regForm) {
    this.isSaving = true;
    this.http.post<any>(this.baseUrl + 'api/oncologyPatient/attempt', this.patient).subscribe(result => {
      this.matchingRecords = result.items.map(r => {
        return { ...r, ...r.person }
      });
      this.isSaving = false;
      this.showOnlyMatchesGrid()
    }, er => {
      console.error(er)
      this.isSaving = false;
      this.showForm = true;
      this.showMatchesGrid = false;
    });
  }

  showOnlyValidationForm() {
    this.showForm = true;
    this.showMatchesGrid = false;
    this.showEditForm = false;
  }

  showOnlyEditForm() {
    this.showForm = false;
    this.showMatchesGrid = false;
    this.showEditForm = true;
  }

  showOnlyMatchesGrid() {
    this.showForm = false;
    this.showMatchesGrid = true;
    this.showEditForm = false;
  }

  continueRegistration() {
    this.showOnlyEditForm();
    this.newPatient = JSON.parse(JSON.stringify(this.patient));
  }

  ngOnInit() {
    this.showOnlyValidationForm();
  }

}
