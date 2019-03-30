import { Component, OnInit } from '@angular/core';
import { HospitalUnitModel, HospitalUnitsClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-unit-edit',
  templateUrl: './unit-edit.component.html',
  styleUrls: ['./unit-edit.component.css']
})
export class UnitEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  unitId: number = -1;
  unit: HospitalUnitModel;
  isSaving: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: HospitalUnitsClient
  ) {
    this.unit = new HospitalUnitModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("unitId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.unitId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Unidad`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getHospitalUnit(this.unitId)
        .subscribe(result => {
          this.unit = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;
    if (this.isNewRecord) {
      this.client.createHospitalUnit(this.unit)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateHospitalUnit(this.unit)
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
      this.router.navigate(['./units'], { relativeTo: this.route.parent });
    }, 2000);
  }

}
