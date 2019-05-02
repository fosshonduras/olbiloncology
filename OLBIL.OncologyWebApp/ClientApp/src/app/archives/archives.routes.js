"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var buildings_list_component_1 = require("./buildings-list/buildings-list.component");
var building_edit_component_1 = require("./building-edit/building-edit.component");
var wards_list_component_1 = require("./wards-list/wards-list.component");
var ward_edit_component_1 = require("./ward-edit/ward-edit.component");
var beds_list_component_1 = require("./beds-list/beds-list.component");
var bed_edit_component_1 = require("./bed-edit/bed-edit.component");
var admin_landing_component_1 = require("./admin-landing/admin-landing.component");
var units_list_component_1 = require("./units-list/units-list.component");
var unit_edit_component_1 = require("./unit-edit/unit-edit.component");
var health_professionals_list_component_1 = require("./health-professionals-list/health-professionals-list.component");
var health_professional_edit_component_1 = require("./health-professional-edit/health-professional-edit.component");
exports.ADMINISTRATION_ROUTES = [
    { path: '', pathMatch: 'full', component: admin_landing_component_1.AdminLandingComponent },
    { path: 'beds', component: beds_list_component_1.BedsListComponent },
    { path: 'beds/new', component: bed_edit_component_1.BedEditComponent },
    { path: 'beds/:bedId', component: bed_edit_component_1.BedEditComponent, pathMatch: 'full' },
    { path: 'buildings', component: buildings_list_component_1.BuildingsListComponent },
    { path: 'buildings/new', component: building_edit_component_1.BuildingEditComponent },
    { path: 'buildings/:buildingId', component: building_edit_component_1.BuildingEditComponent, pathMatch: 'full' },
    { path: 'units', component: units_list_component_1.UnitsListComponent },
    { path: 'units/new', component: unit_edit_component_1.UnitEditComponent },
    { path: 'units/:unitId', component: unit_edit_component_1.UnitEditComponent, pathMatch: 'full' },
    { path: 'wards', component: wards_list_component_1.WardsListComponent },
    { path: 'wards/new', component: ward_edit_component_1.WardEditComponent },
    { path: 'wards/:wardId', component: ward_edit_component_1.WardEditComponent, pathMatch: 'full' },
    { path: 'healthprofessionals', component: health_professionals_list_component_1.HealthProfessionalsListComponent },
    { path: 'healthprofessionals/new', component: health_professional_edit_component_1.HealthProfessionalEditComponent },
    { path: 'healthprofessionals/:healthProfessionalId', component: health_professional_edit_component_1.HealthProfessionalEditComponent, pathMatch: 'full' },
];
//# sourceMappingURL=administration.routes.js.map