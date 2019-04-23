import { Component, OnInit } from '@angular/core';
import { CountryModel, CountriesClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-country-edit',
  templateUrl: './country-edit.component.html',
  styleUrls: ['./country-edit.component.css']
})
export class CountryEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  countryId: number = -1;
  country: CountryModel;
  isSaving: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: CountriesClient
  ) {
    this.country = new CountryModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("countryId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.countryId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nuevo" : "Edición de"} País`;

    this.setupTargetRecord();
  }


  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getCountry(this.countryId)
        .subscribe(result => {
          this.country = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.isNewRecord) {
      this.client.createCountry(this.country)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateCountry(this.country)
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
      this.router.navigate(['./countries'], { relativeTo: this.route.parent });
    }, 2000);
  }
}

