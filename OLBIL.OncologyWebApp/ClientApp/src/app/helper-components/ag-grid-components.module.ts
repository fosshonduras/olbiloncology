import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from "@angular/router";

import { AgGridModule } from 'ag-grid-angular';
import { LinkRendererComponent } from './LinkRendererComponent';

@NgModule({
  declarations: [LinkRendererComponent],
  imports: [
    CommonModule,
    RouterModule,
    AgGridModule.withComponents([]),
  ],
  entryComponents: [LinkRendererComponent],
})
export class AgGridComponentsModule { }
