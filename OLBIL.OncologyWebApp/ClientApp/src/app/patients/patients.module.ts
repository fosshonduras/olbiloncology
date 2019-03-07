import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';

import { PatientsListComponent } from './patients-list/patients-list.component';
import { CreatePatientComponent } from './create-patient/create-patient.component';
import { EditPatientComponent } from './edit-patient/edit-patient.component';
import { AgGridComponentsModule } from '../helper-components/ag-grid-components.module';

@NgModule({
  declarations: [PatientsListComponent, CreatePatientComponent, EditPatientComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AgGridComponentsModule,
    AgGridModule.withComponents([]),
  ]
})
export class PatientsModule { }
