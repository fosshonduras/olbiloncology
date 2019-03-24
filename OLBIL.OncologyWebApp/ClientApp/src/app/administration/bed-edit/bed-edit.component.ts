import { Component, OnInit } from '@angular/core';
import { BedModel, BedsClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bed-edit',
  templateUrl: './bed-edit.component.html',
  styleUrls: ['./bed-edit.component.css']
})
export class BedEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  bedId: number = -1;
  bed: BedModel;
  isSaving: boolean = false;

  bedStatuses: any[] = [
    { bedStatusId: 1, name: "Libre" },
    { bedStatusId: 2, name: "Ocupada" },
    { bedStatusId: 3, name: "Reservada" }
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: BedsClient
  ) {
    this.bed = new BedModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("bedId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.bedId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Cama`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getBed(this.bedId)
        .subscribe(result => {
          this.bed = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;
    if (this.isNewRecord) {
      this.client.createBed(this.bed)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateBed(this.bed)
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
      this.router.navigate(['./beds'], { relativeTo: this.route.parent });
    }, 2000);
  }
}

