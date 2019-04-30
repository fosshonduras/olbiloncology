import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';
import { NgbTypeaheadModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { PatientsListComponent } from './patients-list/patients-list.component';
import { CreatePatientComponent } from './create-patient/create-patient.component';
import { EditPatientComponent } from './edit-patient/edit-patient.component';
import { AgGridComponentsModule } from '../helper-components/ag-grid-components.module';
import { PatientFormComponent } from './patient-form/patient-form.component';
import { AppointmentEditComponent } from '../appointment-edit/appointment-edit.component';
import { AppointmentsListComponent } from '../appointments-list/appointments-list.component';

@NgModule({
  declarations: [PatientsListComponent, CreatePatientComponent, EditPatientComponent, PatientFormComponent, AppointmentEditComponent, AppointmentsListComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AgGridComponentsModule,
    AgGridModule.withComponents([]),
    NgbTypeaheadModule,
    NgbPaginationModule
  ]
})
export class PatientsModule { }
