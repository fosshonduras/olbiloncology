import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PatientsComponent } from './patients/root/patients.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { PATIENTS_ROUTES } from './patients/patients.routes';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'patients', component: PatientsComponent, children: PATIENTS_ROUTES },
  { path: 'appointments', component: AppointmentsComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
