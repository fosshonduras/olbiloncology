import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { environment } from "../../../environments/environment";
import { OncologyPatientModel, PersonModel, OncologyPatientClient } from 'src/app/api-clients';
import * as moment from 'moment';

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.css']
})
export class CreatePatientComponent implements OnInit {
  patient: any;
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

  constructor(
    private client: OncologyPatientClient
    //private http: HttpClient, @Inject('BASE_URL') private baseUrl: string
  ) {
    //if (!environment.production) {
      this.patient = new OncologyPatientModel({
        oncologyPatientId: -1,
        person: new PersonModel( {
          //personId: 'A',
          //firstName: "Karla",
          //lastName: "Tulio",
          //governmentIDNumber: "01012020",
          //nationality: 'Nigeriano',
          //birthdate: new Date('2000-12-12')
        })
      });
    //}
  }

  submitRegistrationAttempt(regForm) {
    this.isSaving = true;
    this.patient.person.birthdate = new Date(this.patient.person.birthdate);
    this.client.attemptCreatePatient(this.patient)
      .subscribe(result => {
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
