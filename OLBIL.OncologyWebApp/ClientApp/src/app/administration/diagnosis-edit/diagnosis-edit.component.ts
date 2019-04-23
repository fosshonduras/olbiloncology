import { Component, OnInit } from '@angular/core';
import { DiagnosisModel, DiagnosesClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-diagnosis-edit',
  templateUrl: './diagnosis-edit.component.html',
  styleUrls: ['./diagnosis-edit.component.css']
})
export class DiagnosisEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  diagnosisId: number = -1;
  diagnosis: DiagnosisModel;
  isSaving: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: DiagnosesClient
  ) {
    this.diagnosis = new DiagnosisModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("diagnosisId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.diagnosisId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nuevo" : "Edición de"} Diagnóstico`;

    this.setupTargetRecord();
  }


  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getDiagnosis(this.diagnosisId)
        .subscribe(result => {
          this.diagnosis = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.isNewRecord) {
      this.client.createDiagnosis(this.diagnosis)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateDiagnosis(this.diagnosis)
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
      this.router.navigate(['./diagnoses'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
