import { Component, OnInit } from '@angular/core';
import { RecordStorageLocationModel, RecordStorageLocationsClient } from '../../api-clients';
import { Router, ActivatedRoute } from '@angular/router';
import { tap, catchError, distinctUntilChanged, debounceTime, switchMap, map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-record-storage-location-edit',
  templateUrl: './record-storage-location-edit.component.html',
  styleUrls: ['./record-storage-location-edit.component.css']
})
export class RecordStorageLocationEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  recordStorageLocationId: number = -1;
  recordStorageLocation: RecordStorageLocationModel;
  isSaving: boolean = false;

  parentRecordStorageLocationModel: RecordStorageLocationModel;
  isSearchingRecordStorageLocation: boolean = false;
  recordStorageLocationSearchFailed: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: RecordStorageLocationsClient
  ) {
    this.recordStorageLocation = new RecordStorageLocationModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("recordStorageLocationId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.recordStorageLocationId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Ubicación de Expedientes`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getRecordStorageLocation(this.recordStorageLocationId)
        .subscribe(result => {
          this.recordStorageLocation = result;
          this.client.getRecordStorageLocation(this.recordStorageLocation.parentLocationId)
            .subscribe(result => {
              this.parentRecordStorageLocationModel = result;
            }, err => {
              this.toastr.warning(err);
            });
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  recordStorageLocationTAFormatter = (x) => x.name;

  recordStorageLocationSearchResultFormatter = (x: RecordStorageLocationModel) => `${x.name}`

  searchRecordStorageLocation = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingRecordStorageLocation = true),
      switchMap(term =>
        this.client.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
            tap(() => this.recordStorageLocationSearchFailed = false),
            catchError(() => {
              this.recordStorageLocationSearchFailed = true;
              return of([]);
            }))
      ),
      tap(() => this.isSearchingRecordStorageLocation = false)
    )

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.parentRecordStorageLocationModel && this.parentRecordStorageLocationModel.recordStorageLocationId > 0) {
      this.recordStorageLocation.parentLocationId = this.parentRecordStorageLocationModel.recordStorageLocationId;
    }

    if (this.isNewRecord) {
      this.client.createRecordStorageLocation(this.recordStorageLocation)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateRecordStorageLocation(this.recordStorageLocation)
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
      this.router.navigate(['./recordstoragelocations'], { relativeTo: this.route.parent });
    }, 2000);
  }
}

