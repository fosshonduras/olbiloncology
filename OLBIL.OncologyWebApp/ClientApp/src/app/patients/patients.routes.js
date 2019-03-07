"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var patients_list_component_1 = require("./patients-list/patients-list.component");
var create_patient_component_1 = require("./create-patient/create-patient.component");
var edit_patient_component_1 = require("./edit-patient/edit-patient.component");
exports.PATIENTS_ROUTES = [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', component: patients_list_component_1.PatientsListComponent },
    { path: 'create', component: create_patient_component_1.CreatePatientComponent },
    { path: 'edit/:patientId', component: edit_patient_component_1.EditPatientComponent, pathMatch: 'full' },
];
//# sourceMappingURL=patients.routes.js.map