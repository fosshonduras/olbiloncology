import { Component, OnInit } from '@angular/core';
import { AdministrativeDivisionModel, AdministrativeDivisionsClient } from '../../api-clients';
import { Observable, of } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, distinctUntilChanged, catchError, switchMap, tap, map } from 'rxjs/operators';

@Component({
  selector: 'app-administrative-division-edit',
  templateUrl: './administrative-division-edit.component.html',
  styleUrls: ['./administrative-division-edit.component.css']
})
export class AdministrativeDivisionEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  administrativeDivisionId: number = -1;
  administrativeDivision: AdministrativeDivisionModel;
  isSaving: boolean = false;

  parentAdministrativeDivisionModel: AdministrativeDivisionModel;
  isSearchingAdministrativeDivision: boolean = false;
  administrativeDivisionSearchFailed: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: AdministrativeDivisionsClient
  ) {
    this.administrativeDivision = new AdministrativeDivisionModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("administrativeDivisionId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.administrativeDivisionId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} División Administrativa`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getAdministrativeDivision(this.administrativeDivisionId)
        .subscribe(result => {
          this.administrativeDivision = result;
          this.client.getAdministrativeDivision(this.administrativeDivision.parentId)
            .subscribe(result => {
              this.parentAdministrativeDivisionModel = result;
            }, err => {
              this.toastr.warning(err);
            });
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  administrativeDivisionTAFormatter = (x) => x.name;

  administrativeDivisionSearchResultFormatter = (x: AdministrativeDivisionModel) => `${x.code} ${x.name}`

  searchAdministrativeDivision = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingAdministrativeDivision = true),
      switchMap(term =>
        this.client.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
            tap(() => this.administrativeDivisionSearchFailed = false),
            catchError(() => {
              this.administrativeDivisionSearchFailed = true;
              return of([]);
            }))
      ),
      tap(() => this.isSearchingAdministrativeDivision = false)
    )

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.parentAdministrativeDivisionModel && this.parentAdministrativeDivisionModel.administrativeDivisionId > 0) {
      this.administrativeDivision.parentId = this.parentAdministrativeDivisionModel.administrativeDivisionId;
    }

    if (this.isNewRecord) {
      this.client.createAdministrativeDivision(this.administrativeDivision)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateAdministrativeDivision(this.administrativeDivision)
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
      this.router.navigate(['./administrativedivisions'], { relativeTo: this.route.parent });
    }, 2000);
  }
}

