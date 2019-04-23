import { Routes } from "@angular/router";
import { AmbulatoryAttentionRecordEditComponent } from './ambulatory-attention-record-edit/ambulatory-attention-record-edit.component';
import { AmbulatoryAttentionRecordsListComponent } from './ambulatory-attention-records-list/ambulatory-attention-records-list.component';
import { AmbulatoryAttentionLandingComponent } from './ambulatory-attention-landing/ambulatory-attention-landing.component';


export const AMBULATORY_ATTENTION_ROUTES: Routes = [
  { path: '', pathMatch: 'full', component: AmbulatoryAttentionLandingComponent },
  { path: 'ambulatoryattentionrecords', component: AmbulatoryAttentionRecordsListComponent },
  { path: 'ambulatoryattentionrecords/new', component: AmbulatoryAttentionRecordEditComponent },
  { path: 'ambulatoryattentionrecords/:ambulatoryAttentionRecordId', component: AmbulatoryAttentionRecordEditComponent, pathMatch: 'full' },
];
