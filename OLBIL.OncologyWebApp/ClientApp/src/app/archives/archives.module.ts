import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';
import { NgbTypeaheadModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { ArchivesRootComponent } from './archives-root/archives-root.component';
import { ArchivesLandingComponent } from './archives-landing/archives-landing.component';
import { PatientPhysicalRecordsListComponent } from './patient-physical-records-list/patient-physical-records-list.component';
import { PatientPhysicalRecordEditComponent } from './patient-physical-record-edit/patient-physical-record-edit.component';
import { PhysicalRecordTransferEditComponent } from './physical-record-transfer-edit/physical-record-transfer-edit.component';
import { PhysicalRecordTransfersListComponent } from './physical-record-transfers-list/physical-record-transfers-list.component';
import { AgGridComponentsModule } from '../helper-components/ag-grid-components.module';

@NgModule({
  declarations: [ArchivesRootComponent, ArchivesLandingComponent,
    PatientPhysicalRecordsListComponent, PatientPhysicalRecordEditComponent,
    PhysicalRecordTransferEditComponent, PhysicalRecordTransfersListComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AgGridComponentsModule,
    AgGridModule.withComponents([]),
    NgbTypeaheadModule,
    NgbPaginationModule  ]
})
export class ArchivesModule { }
