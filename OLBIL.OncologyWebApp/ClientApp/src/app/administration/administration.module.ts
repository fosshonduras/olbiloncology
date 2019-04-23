import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';
import { NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';

import { AgGridComponentsModule } from '../helper-components/ag-grid-components.module';
import { BuildingsListComponent } from './buildings-list/buildings-list.component';
import { BuildingEditComponent } from './building-edit/building-edit.component';
import { WardEditComponent } from './ward-edit/ward-edit.component';
import { WardsListComponent } from './wards-list/wards-list.component';
import { BedsListComponent } from './beds-list/beds-list.component';
import { BedEditComponent } from './bed-edit/bed-edit.component';
import { AdminLandingComponent } from './admin-landing/admin-landing.component';
import { UnitsListComponent } from './units-list/units-list.component';
import { UnitEditComponent } from './unit-edit/unit-edit.component';
import { HealthProfessionalsListComponent } from './health-professionals-list/health-professionals-list.component';
import { HealthProfessionalEditComponent } from './health-professional-edit/health-professional-edit.component';
import { AdministrativeDivisionEditComponent } from './administrative-division-edit/administrative-division-edit.component';
import { AdministrativeDivisionsListComponent } from './administrative-divisions-list/administrative-divisions-list.component';
import { AppointmentReasonEditComponent } from './appointment-reason-edit/appointment-reason-edit.component';
import { AppointmentReasonsListComponent } from './appointment-reasons-list/appointment-reasons-list.component';
import { CountryEditComponent } from './country-edit/country-edit.component';
import { CountriesListComponent } from './countries-list/countries-list.component';
import { MedicalSpecialtiesListComponent } from './medical-specialties-list/medical-specialties-list.component';
import { MedicalSpecialtyEditComponent } from './medical-specialty-edit/medical-specialty-edit.component';
import { DiagnosisEditComponent } from './diagnosis-edit/diagnosis-edit.component';
import { DiagnosesListComponent } from './diagnoses-list/diagnoses-list.component';

@NgModule({
  declarations: [BuildingsListComponent, BuildingEditComponent, WardEditComponent,
    WardsListComponent, BedsListComponent, BedEditComponent, AdminLandingComponent,
    UnitsListComponent, UnitEditComponent, HealthProfessionalsListComponent,
    HealthProfessionalEditComponent, AdministrativeDivisionEditComponent,
    AdministrativeDivisionsListComponent, AppointmentReasonEditComponent,
    AppointmentReasonsListComponent, CountryEditComponent, CountriesListComponent,
    MedicalSpecialtiesListComponent, MedicalSpecialtyEditComponent,
    DiagnosisEditComponent, DiagnosesListComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AgGridComponentsModule,
    AgGridModule.withComponents([]),
    NgbTypeaheadModule
  ]
})
export class AdministrationModule { }
