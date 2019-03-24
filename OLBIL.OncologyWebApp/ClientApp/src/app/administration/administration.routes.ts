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
];
