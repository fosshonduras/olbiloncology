import { Routes } from "@angular/router";
import { PatientsListComponent } from "./patients-list/patients-list.component";
import { CreatePatientComponent } from "./create-patient/create-patient.component";

export const PATIENTS_ROUTES: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { path: 'list', component: PatientsListComponent },
  { path: 'create', component: CreatePatientComponent }
];
