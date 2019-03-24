import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';

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

@NgModule({
  declarations: [BuildingsListComponent, BuildingEditComponent, WardEditComponent, WardsListComponent, BedsListComponent, BedEditComponent, AdminLandingComponent, UnitsListComponent, UnitEditComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AgGridComponentsModule,
    AgGridModule.withComponents([]),
  ]
})
export class AdministrationModule { }
