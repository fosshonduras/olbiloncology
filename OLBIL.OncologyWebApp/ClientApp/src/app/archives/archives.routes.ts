import { Routes } from "@angular/router";

import { ArchivesLandingComponent } from './archives-landing/archives-landing.component';
import { PatientPhysicalRecordsListComponent } from './patient-physical-records-list/patient-physical-records-list.component';
import { PatientPhysicalRecordEditComponent } from './patient-physical-record-edit/patient-physical-record-edit.component';
import { PhysicalRecordTransferEditComponent } from './physical-record-transfer-edit/physical-record-transfer-edit.component';
import { PhysicalRecordTransfersListComponent } from './physical-record-transfers-list/physical-record-transfers-list.component';

export const ARCHIVES_ROUTES: Routes = [
  { path: '', pathMatch: 'full', component: ArchivesLandingComponent },

  { path: 'patientphysicalrecords', component: PatientPhysicalRecordsListComponent },
  { path: 'patientphysicalrecords/new', component: PatientPhysicalRecordEditComponent },
  { path: 'patientphysicalrecords/:patientPhysicalRecordId', component: PatientPhysicalRecordEditComponent, pathMatch: 'full' },

  { path: 'physicalrecordtransfers', component: PhysicalRecordTransfersListComponent },
  { path: 'physicalrecordtransfers/new', component: PhysicalRecordTransferEditComponent },
  { path: 'physicalrecordtransfers/:physicalRecordTransferId', component: PhysicalRecordTransferEditComponent, pathMatch: 'full' },
];
