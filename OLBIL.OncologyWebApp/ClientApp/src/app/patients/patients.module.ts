import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';

import { PatientsListComponent } from './patients-list/patients-list.component';
import { CreatePatientComponent } from './create-patient/create-patient.component';

@NgModule({
  declarations: [PatientsListComponent, CreatePatientComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AgGridModule.withComponents([]),
  ]
})
export class PatientsModule { }
