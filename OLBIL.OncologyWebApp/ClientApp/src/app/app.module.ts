import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PatientsComponent } from './patients/root/patients.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { PatientsModule } from './patients/patients.module';
import { AppRoutingModule } from "./app-routing.module";
import { OncologyPatientsClient } from './api-clients';
import { AdministrationComponent } from './administration/root/administration.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PatientsComponent,
    AdministrationComponent,
    AppointmentsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AppRoutingModule,
    PatientsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot()
  ],
  providers: [OncologyPatientsClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
