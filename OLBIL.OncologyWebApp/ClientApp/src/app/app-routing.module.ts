import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PatientsComponent } from './patients/root/patients.component';
import { PATIENTS_ROUTES } from './patients/patients.routes';
import { AdminRootComponent } from './administration/admin-root/admin-root.component';
import { ADMINISTRATION_ROUTES } from './administration/administration.routes';
import { AMBULATORY_ATTENTION_ROUTES } from './ambulatory-attention/ambulatory-attention.routes';
import { AmbulatoryAttentionRootComponent } from './ambulatory-attention/ambulatory-attention-root/ambulatory-attention-root.component';
import { ARCHIVES_ROUTES } from './archives/archives.routes';
import { ArchivesRootComponent } from './archives/archives-root/archives-root.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'patients', component: PatientsComponent, children: PATIENTS_ROUTES },
  { path: 'administration', component: AdminRootComponent, children: ADMINISTRATION_ROUTES },
  { path: 'ambulatoryattention', component: AmbulatoryAttentionRootComponent, children: AMBULATORY_ATTENTION_ROUTES },
  { path: 'archives', component: ArchivesRootComponent, children: ARCHIVES_ROUTES },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
