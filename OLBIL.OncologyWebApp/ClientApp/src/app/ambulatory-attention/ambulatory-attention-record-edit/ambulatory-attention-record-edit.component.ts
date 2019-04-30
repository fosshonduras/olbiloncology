import { Component, OnInit } from '@angular/core';
import { AmbulatoryAttentionRecordModel, AmbulatoryAttentionRecordsClient, HealthProfessionalModel, HealthProfessionalsClient, OncologyPatientModel, OncologyPatientsClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { distinctUntilChanged, switchMap, map, tap, catchError, debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-ambulatory-attention-record-edit',
  templateUrl: './ambulatory-attention-record-edit.component.html',
  styleUrls: ['./ambulatory-attention-record-edit.component.css']
})
export class AmbulatoryAttentionRecordEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  ambulatoryAttentionRecordId: number = -1;
  ambulatoryAttentionRecord: AmbulatoryAttentionRecordModel;
  isSaving: boolean = false;

  healthProfessionalModel: HealthProfessionalModel;
  isSearchingHealthProfessional: boolean = false;
  healthProfessionalSearchFailed: boolean = false;

  oncologyPatientModel: OncologyPatientModel;
  isSearchingOncologyPatient: boolean = false;
  oncologyPatientSearchFailed: boolean = false;

  //ambulatoryAttentionRecordStatuses: any[] = [
  //  { ambulatoryAttentionRecordStatusId: 1, name: "Libre" },
  //  { ambulatoryAttentionRecordStatusId: 2, name: "Ocupada" },
  //  { ambulatoryAttentionRecordStatusId: 3, name: "Reservada" }
  //];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: AmbulatoryAttentionRecordsClient,
    private healthProfessionalsClient: HealthProfessionalsClient,
    private oncologyPatientsClient: OncologyPatientsClient
  ) {
    this.ambulatoryAttentionRecord = new AmbulatoryAttentionRecordModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("ambulatoryAttentionRecordId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.ambulatoryAttentionRecordId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Atención Ambulatoria`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getAmbulatoryAttentionRecord(this.ambulatoryAttentionRecordId)
        .subscribe(result => {
          this.ambulatoryAttentionRecord = result;
          this.healthProfessionalsClient.getHealthProfessional(this.ambulatoryAttentionRecord.healthProfessionalId)
            .subscribe(result => {
              this.healthProfessionalModel = result;
            }, err => {
              this.toastr.warning(err);
              });
          this.oncologyPatientsClient.getPatient(this.ambulatoryAttentionRecord.oncologyPatientId)
            .subscribe(result => {
              this.oncologyPatientModel = result;
            }, err => {
              this.toastr.warning(err);
            });
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  healthProfessionalTAFormatter = (x) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  healthProfessionalSearchResultFormatter = (x: HealthProfessionalModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  searchHealthProfessional = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingHealthProfessional = true),
      switchMap(term =>
        this.healthProfessionalsClient.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
            tap(() => this.healthProfessionalSearchFailed = false),
            catchError(() => {
              this.healthProfessionalSearchFailed = true;
              return of([]);
            }))
      ),
      tap(() => this.isSearchingHealthProfessional = false)
    )

  oncologyPatientTAFormatter = (x) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  oncologyPatientSearchResultFormatter = (x: OncologyPatientModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  searchOncologyPatient = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingOncologyPatient = true),
      switchMap(term =>
        this.oncologyPatientsClient.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
            tap(() => this.oncologyPatientSearchFailed = false),
            catchError(() => {
              this.oncologyPatientSearchFailed = true;
              return of([]);
            }))
      ),
      tap(() => this.isSearchingOncologyPatient = false)
    )

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.healthProfessionalModel && this.healthProfessionalModel.healthProfessionalId > 0) {
      this.ambulatoryAttentionRecord.healthProfessionalId = this.healthProfessionalModel.healthProfessionalId;
    }

    if (this.oncologyPatientModel && this.oncologyPatientModel.oncologyPatientId > 0) {
      this.ambulatoryAttentionRecord.oncologyPatientId = this.oncologyPatientModel.oncologyPatientId;
    }

    if (this.isNewRecord) {
      this.client.createAmbulatoryAttentionRecord(this.ambulatoryAttentionRecord)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateAmbulatoryAttentionRecord(this.ambulatoryAttentionRecord)
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
      this.router.navigate(['./ambulatoryattentionrecords'], { relativeTo: this.route.parent });
    }, 2000);
  }
}

