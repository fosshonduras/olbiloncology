import { Component, OnInit } from '@angular/core';
import { RecordStorageLocationModel, OncologyPatientModel, PatientPhysicalRecordModel, PatientPhysicalRecordsClient, RecordStorageLocationsClient, OncologyPatientsClient } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, tap, switchMap, map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-patient-physical-record-edit',
  templateUrl: './patient-physical-record-edit.component.html',
  styleUrls: ['./patient-physical-record-edit.component.css']
})
export class PatientPhysicalRecordEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  patientPhysicalRecordId: number = -1;
  patientPhysicalRecord: PatientPhysicalRecordModel;
  isSaving: boolean = false;

  oncologyPatientModel: OncologyPatientModel;
  isSearchingOncologyPatient: boolean = false;
  oncologyPatientSearchFailed: boolean = false;

  recordStorageLocationModel: RecordStorageLocationModel;
  isSearchingRecordStorageLocation: boolean = false;
  recordStorageLocationSearchFailed: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: PatientPhysicalRecordsClient,
    private oncologyPatientsClient: OncologyPatientsClient,
    private recordStorageLocationsClient: RecordStorageLocationsClient
  ) {
    this.patientPhysicalRecord = new PatientPhysicalRecordModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("patientPhysicalRecordId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.patientPhysicalRecordId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nuevo" : "Edición de"} Expediente Físico`;

    this.setupTargetRecord();
  }

  oncologyPatientTAFormatter = (x: OncologyPatientModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

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

  recordStorageLocationTAFormatter = (x: RecordStorageLocationModel) => `${x.name}`;

  recordStorageLocationSearchResultFormatter = (x: RecordStorageLocationModel) => `${x.name}`;

  searchRecordStorageLocation = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingRecordStorageLocation = true),
      switchMap(term =>
        term.length < 2 ? of(Array<RecordStorageLocationModel>()) :
          this.recordStorageLocationsClient.search(term)
            .pipe(
              map((bm, bmi) => bm.items),
              tap(() => this.recordStorageLocationSearchFailed = false),
              catchError(() => {
                this.recordStorageLocationSearchFailed = true;
                return of(Array<RecordStorageLocationModel>());
              }))
      ),
      tap(() => this.isSearchingRecordStorageLocation = false)
    )

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getPatientPhysicalRecord(this.patientPhysicalRecordId)
        .subscribe(result => {
          this.patientPhysicalRecord = result;

          this.oncologyPatientsClient.getPatient(this.patientPhysicalRecord.oncologyPatientId)
            .subscribe(result => {
              this.oncologyPatientModel = result;
            }, err => {
              this.toastr.warning(err);
            });

          this.recordStorageLocationsClient.getRecordStorageLocation(this.patientPhysicalRecord.recordStorageLocationId)
            .subscribe(result => {
              this.recordStorageLocationModel = result;
            }, err => {
              this.toastr.warning(err);
            });
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.oncologyPatientModel && this.oncologyPatientModel.oncologyPatientId > 0) {
      this.patientPhysicalRecord.oncologyPatientId = this.oncologyPatientModel.oncologyPatientId;
    }
    if (this.recordStorageLocationModel && this.recordStorageLocationModel.recordStorageLocationId > 0) {
      this.patientPhysicalRecord.recordStorageLocationId = this.recordStorageLocationModel.recordStorageLocationId;
    }

    if (this.isNewRecord) {
      this.client.createPatientPhysicalRecord(this.patientPhysicalRecord)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updatePatientPhysicalRecord(this.patientPhysicalRecord)
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
      this.router.navigate(['./patientphysicalrecords'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
