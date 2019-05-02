import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArchivesRootComponent } from '../archives-root/archives-root.component';
import { ArchivesLandingComponent } from '../archives-landing/archives-landing.component';

@NgModule({
  declarations: [ArchivesRootComponent, ArchivesLandingComponent],
  imports: [
    CommonModule
  ]
})
export class ArchivesModule { }
