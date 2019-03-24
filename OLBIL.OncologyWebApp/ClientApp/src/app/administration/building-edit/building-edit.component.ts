import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BuildingModel, BuildingsClient } from '../../api-clients';

@Component({
  selector: 'app-building-edit',
  templateUrl: './building-edit.component.html',
  styleUrls: ['./building-edit.component.css']
})
export class BuildingEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  buildingId: number = -1;
  building: BuildingModel;
  isSaving: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: BuildingsClient
  ) {
    this.building = new BuildingModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("buildingId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.buildingId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nuevo" : "Edición de"} Edificio`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getBuilding(this.buildingId)
        .subscribe(result => {
          this.building = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;
    if (this.isNewRecord) {
      this.client.createBuilding(this.building)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateBuilding(this.building)
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
      this.router.navigate(['./buildings'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
