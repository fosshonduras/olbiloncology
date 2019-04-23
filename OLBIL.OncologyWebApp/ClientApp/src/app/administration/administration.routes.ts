import { Routes } from "@angular/router";
import { BuildingsListComponent } from "./buildings-list/buildings-list.component";
import { BuildingEditComponent } from "./building-edit/building-edit.component";
import { WardsListComponent } from "./wards-list/wards-list.component";
import { WardEditComponent } from "./ward-edit/ward-edit.component";
import { BedsListComponent } from "./beds-list/beds-list.component";
import { BedEditComponent } from "./bed-edit/bed-edit.component";
import { AdminLandingComponent } from "./admin-landing/admin-landing.component";
import { UnitsListComponent } from "./units-list/units-list.component";
import { UnitEditComponent } from "./unit-edit/unit-edit.component";
import { HealthProfessionalsListComponent } from "./health-professionals-list/health-professionals-list.component";
import { HealthProfessionalEditComponent } from "./health-professional-edit/health-professional-edit.component";
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

export const ADMINISTRATION_ROUTES: Routes = [
  { path: '', pathMatch: 'full', component: AdminLandingComponent },
  { path: 'beds', component: BedsListComponent },
  { path: 'beds/new', component: BedEditComponent },
  { path: 'beds/:bedId', component: BedEditComponent, pathMatch: 'full' },
  { path: 'buildings', component: BuildingsListComponent },
  { path: 'buildings/new', component: BuildingEditComponent },
  { path: 'buildings/:buildingId', component: BuildingEditComponent, pathMatch: 'full' },
  { path: 'units', component: UnitsListComponent },
  { path: 'units/new', component: UnitEditComponent },
  { path: 'units/:unitId', component: UnitEditComponent, pathMatch: 'full' },
  { path: 'wards', component: WardsListComponent },
  { path: 'wards/new', component: WardEditComponent },
  { path: 'wards/:wardId', component: WardEditComponent, pathMatch: 'full' },
  { path: 'healthprofessionals', component: HealthProfessionalsListComponent },
  { path: 'healthprofessionals/new', component: HealthProfessionalEditComponent },
  { path: 'healthprofessionals/:healthProfessionalId', component: HealthProfessionalEditComponent, pathMatch: 'full' },

  { path: 'appointmentreasons', component: AppointmentReasonsListComponent },
  { path: 'appointmentreasons/new', component: AppointmentReasonEditComponent },
  { path: 'appointmentreasons/:appointmentReasonId', component: AppointmentReasonEditComponent, pathMatch: 'full' },
  { path: 'medicalspecialties', component: MedicalSpecialtiesListComponent },
  { path: 'medicalspecialties/new', component: MedicalSpecialtyEditComponent },
  { path: 'medicalspecialties/:medicalSpecialtyId', component: MedicalSpecialtyEditComponent, pathMatch: 'full' },
  { path: 'diagnoses', component: DiagnosesListComponent },
  { path: 'diagnoses/new', component: DiagnosisEditComponent },
  { path: 'diagnoses/:diagnosisId', component: DiagnosisEditComponent, pathMatch: 'full' },

  { path: 'administrativedivisions', component: AdministrativeDivisionsListComponent },
  { path: 'administrativedivisions/new', component: AdministrativeDivisionEditComponent },
  { path: 'administrativedivisions/:administrativeDivisionId', component: AdministrativeDivisionEditComponent, pathMatch: 'full' },
  { path: 'countries', component: CountriesListComponent },
  { path: 'countries/new', component: CountryEditComponent },
  { path: 'countries/:countryId', component: CountryEditComponent, pathMatch: 'full' },
];
