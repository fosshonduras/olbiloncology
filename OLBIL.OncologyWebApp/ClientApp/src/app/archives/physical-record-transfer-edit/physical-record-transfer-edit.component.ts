import { Component, OnInit } from '@angular/core';
import { RecordStorageLocationModel, PhysicalRecordTransferModel, PhysicalRecordTransfersClient, OncologyPatientsClient, RecordStorageLocationsClient, PatientPhysicalRecordModel, PatientPhysicalRecordsClient } from '../../api-clients';
import { debounceTime, distinctUntilChanged, tap, switchMap, map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';

@Component({
  selector: 'app-physical-record-transfer-edit',
  templateUrl: './physical-record-transfer-edit.component.html',
  styleUrls: ['./physical-record-transfer-edit.component.css']
})
export class PhysicalRecordTransferEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  physicalRecordTransferId: number = -1;
  physicalRecordTransfer: PhysicalRecordTransferModel;
  isSaving: boolean = false;

  patientPhysicalRecordModel: PatientPhysicalRecordModel;
  isSearchingPatientPhysicalRecord: boolean = false;
  patientPhysicalRecordSearchFailed: boolean = false;

  recordStorageLocationModel: RecordStorageLocationModel;
  isSearchingRecordStorageLocation: boolean = false;
  recordStorageLocationSearchFailed: boolean = false;

  physicalRecordTransferDate: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: PhysicalRecordTransfersClient,
    private recordStorageLocationsClient: RecordStorageLocationsClient,
    private patientPhysicalRecordsClient: PatientPhysicalRecordsClient
  ) {
    this.physicalRecordTransfer = new PhysicalRecordTransferModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("physicalRecordTransferId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
      this.updateDates();
    } else {
      this.physicalRecordTransferId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Transferencia`;

    this.setupTargetRecord();
  }

  private updateDates() {
    this.physicalRecordTransferDate = this.physicalRecordTransfer.date && moment(this.physicalRecordTransfer.date).format(moment.HTML5_FMT.DATE);
  }
  
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

  patientPhysicalRecordTAFormatter = (x: PatientPhysicalRecordModel) => `${x.recordNumber}`;

  patientPhysicalRecordSearchResultFormatter = (x: PatientPhysicalRecordModel) => `${x.recordNumber}`;

  searchPatientPhysicalRecord = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingPatientPhysicalRecord = true),
      switchMap(term =>
        term.length < 2 ? of(Array<PatientPhysicalRecordModel>()) :
          this.patientPhysicalRecordsClient.search(term)
            .pipe(
              map((bm, bmi) => bm.items),
              tap(() => this.patientPhysicalRecordSearchFailed = false),
              catchError(() => {
                this.patientPhysicalRecordSearchFailed = true;
                return of(Array<PatientPhysicalRecordModel>());
              }))
      ),
      tap(() => this.isSearchingPatientPhysicalRecord = false)
    )

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getPhysicalRecordTransfer(this.physicalRecordTransferId)
        .subscribe(result => {
          this.physicalRecordTransfer = result;
          this.updateDates();

          this.patientPhysicalRecordsClient.getPatientPhysicalRecord(this.physicalRecordTransfer.patientPhysicalRecordId)
            .subscribe(result => {
              this.patientPhysicalRecordModel = result;
            }, err => {
              this.toastr.warning(err);
              });

          this.recordStorageLocationsClient.getRecordStorageLocation(this.physicalRecordTransfer.targetLocationId)
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
    this.physicalRecordTransfer.date = this.physicalRecordTransferDate && new Date(this.physicalRecordTransferDate)

    if (this.patientPhysicalRecordModel && this.patientPhysicalRecordModel.patientPhysicalRecordId > 0) {
      this.physicalRecordTransfer.patientPhysicalRecordId = this.patientPhysicalRecordModel.patientPhysicalRecordId;
    }

    if (this.recordStorageLocationModel && this.recordStorageLocationModel.recordStorageLocationId > 0) {
      this.physicalRecordTransfer.targetLocationId = this.recordStorageLocationModel.recordStorageLocationId;
    }

    if (this.isNewRecord) {
      this.client.createPhysicalRecordTransfer(this.physicalRecordTransfer)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updatePhysicalRecordTransfer(this.physicalRecordTransfer)
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
      this.router.navigate(['./physicalrecordtransfers'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
