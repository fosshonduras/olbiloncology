import { Routes } from "@angular/router";
import { PatientsListComponent } from "./patients-list/patients-list.component";
import { CreatePatientComponent } from "./create-patient/create-patient.component";
import { EditPatientComponent } from "./edit-patient/edit-patient.component";
import { PatientsLandingComponent } from "./patients-landing/patients-landing.component";
import { AppointmentsListComponent } from "./appointments-list/appointments-list.component";
import { AppointmentEditComponent } from "./appointment-edit/appointment-edit.component";

export const PATIENTS_ROUTES: Routes = [
  { path: '', pathMatch: 'full', component: PatientsLandingComponent },
  { path: 'appointments', component: AppointmentsListComponent },
  { path: 'appointments/new', component: AppointmentEditComponent },
  { path: 'appointments/:appointmentId', component: AppointmentEditComponent },
  { path: 'list', component: PatientsListComponent },
  { path: 'create', component: CreatePatientComponent },
  { path: 'edit/:patientId', component: EditPatientComponent, pathMatch: 'full' },
];
