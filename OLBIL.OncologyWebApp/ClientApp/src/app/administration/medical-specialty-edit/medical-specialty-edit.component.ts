import { Component, OnInit } from '@angular/core';
import { MedicalSpecialtyModel, MedicalSpecialtiesClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-medical-specialty-edit',
  templateUrl: './medical-specialty-edit.component.html',
  styleUrls: ['./medical-specialty-edit.component.css']
})
export class MedicalSpecialtyEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  medicalSpecialtyId: number = -1;
  medicalSpecialty: MedicalSpecialtyModel;
  isSaving: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: MedicalSpecialtiesClient
  ) {
    this.medicalSpecialty = new MedicalSpecialtyModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("medicalSpecialtyId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.medicalSpecialtyId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Especialidad Médica`;

    this.setupTargetRecord();
  }


  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getMedicalSpecialty(this.medicalSpecialtyId)
        .subscribe(result => {
          this.medicalSpecialty = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.isNewRecord) {
      this.client.createMedicalSpecialty(this.medicalSpecialty)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateMedicalSpecialty(this.medicalSpecialty)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    }
  }

  private handleFailure(err: any) {
    this.isSaving = false;
    this.toastr.warning(JSON.parse(err.response).error);
  }

  private handleSuccess(result) {
    this.toastr.success("Datos guardados.");
    setTimeout(() => {
      this.router.navigate(['./medicalspecialties'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
