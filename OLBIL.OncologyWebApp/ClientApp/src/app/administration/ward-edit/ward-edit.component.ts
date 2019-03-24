import { Component, OnInit } from '@angular/core';
import { WardModel, WardsClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ward-edit',
  templateUrl: './ward-edit.component.html',
  styleUrls: ['./ward-edit.component.css']
})
export class WardEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  wardId: number = -1;
  ward: WardModel;
  isSaving: boolean = false;

  wardStatuses: any[] = [
    { wardStatusId: 1, name: "Habilitada" },
    { wardStatusId: 2, name: "Deshabilitada" }
  ];

  wardGenders: any[] = [
    { wardGenderId: 1, name: "Unisex" },
    { wardGenderId: 2, name: "Femenino" },
    { wardGenderId: 3, name: "Masculino" },
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: WardsClient
  ) {
    this.ward = new WardModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("wardId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.wardId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Sala`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getWard(this.wardId)
        .subscribe(result => {
          this.ward = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;
    if (this.isNewRecord) {
      this.client.createWard(this.ward)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateWard(this.ward)
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
      this.router.navigate(['./wards'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
