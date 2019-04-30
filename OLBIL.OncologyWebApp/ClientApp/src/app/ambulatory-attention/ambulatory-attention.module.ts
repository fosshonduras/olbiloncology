import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';
import { NgbTypeaheadModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { AgGridComponentsModule } from '../helper-components/ag-grid-components.module';

import { AmbulatoryAttentionRecordEditComponent } from './ambulatory-attention-record-edit/ambulatory-attention-record-edit.component';
import { AmbulatoryAttentionRecordsListComponent } from './ambulatory-attention-records-list/ambulatory-attention-records-list.component';
import { AmbulatoryAttentionLandingComponent } from './ambulatory-attention-landing/ambulatory-attention-landing.component';
import { AmbulatoryAttentionRootComponent } from './ambulatory-attention-root/ambulatory-attention-root.component';
import { AmbulatoryAttentionReportComponent } from './ambulatory-attention-report/ambulatory-attention-report.component';

@NgModule({
  declarations: [AmbulatoryAttentionRecordEditComponent,
    AmbulatoryAttentionRecordsListComponent,
    AmbulatoryAttentionLandingComponent,
    AmbulatoryAttentionRootComponent,
    AmbulatoryAttentionReportComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AgGridComponentsModule,
    AgGridModule.withComponents([]),
    NgbTypeaheadModule,
    NgbPaginationModule
  ]
})
export class AmbulatoryAttentionModule { }
