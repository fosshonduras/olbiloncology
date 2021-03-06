import { Component, OnInit, Inject } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { OncologyPatientModel, PersonModel, OncologyPatientsClient } from '../../api-clients';
import * as moment from 'moment';
import { ColDef, GridOptions } from 'ag-grid-community';

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

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
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
    private client: OncologyPatientsClient
  ) {
      this.patient = new OncologyPatientModel({
        oncologyPatientId: -1,
        person: new PersonModel( {
        })
      });
  }

  submitRegistrationAttempt(regForm) {
    this.isSaving = true;
    this.patient.person.birthdate = this.patient.person.birthdate && new Date(this.patient.person.birthdate);

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

  volverAPreliminar() {
    this.showOnlyValidationForm();
  }

  continueRegistration() {
    this.showOnlyEditForm();
    this.newPatient = JSON.parse(JSON.stringify(this.patient));
  }

  ngOnInit() {
    this.showOnlyValidationForm();
  }

}
