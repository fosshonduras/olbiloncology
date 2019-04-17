import { Component, OnInit } from '@angular/core';
import { WardModel, WardsClient, BuildingsClient, BuildingModel, HospitalUnitsClient, HospitalUnitModel } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, map, tap, switchMap } from 'rxjs/operators';

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

  buildingModel: BuildingModel;
  isSearchingBuilding: boolean = false;
  buildingSearchFailed: boolean = false;

  unitModel: HospitalUnitModel;
  isSearchingUnit: boolean = false;
  unitSearchFailed: boolean = false;

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
    private client: WardsClient,
    private buildingsClient: BuildingsClient,
    private unitsClient: HospitalUnitsClient
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

  buildingTAFormatter = (x) => x.name;

  buildingSearchResultFormatter = (x: BuildingModel) => `${x.code} - ${x.name}`

  searchBuilding = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingBuilding = true),
      switchMap(term =>
        this.buildingsClient.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
          tap(() => this.buildingSearchFailed = false),
          catchError(() => {
            this.buildingSearchFailed = true;
            return of([]);
          }))
      ),
      tap(() => this.isSearchingBuilding = false)
    )

  unitTAFormatter = (x) => x.name;

  unitSearchResultFormatter = (x: HospitalUnitModel) => `${x.code} - ${x.name}`

  searchUnit = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingUnit = true),
      switchMap(term =>
        term.length < 2 ? of(Array<HospitalUnitModel>()) :
        this.unitsClient.search(term)
          .pipe(
            map((bm, bmi) =>  bm.items),
            tap(() => this.unitSearchFailed = false),
            catchError(() => {
              this.unitSearchFailed = true;
              return of(Array<HospitalUnitModel>());
            }))
      ),
      tap(() => this.isSearchingUnit = false)
    )

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getWard(this.wardId)
        .subscribe(result => {
          this.ward = result;
          this.buildingsClient.getBuilding(this.ward.buildingId)
              .subscribe(result => {
                this.buildingModel = result;
              }, err => {
                this.toastr.warning(err);
                });
          this.unitsClient.getHospitalUnit(this.ward.hospitalUnitId)
            .subscribe(result => {
              this.unitModel = result;
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
    if (this.buildingModel && this.buildingModel.buildingId > 0) {
      this.ward.buildingId = this.buildingModel.buildingId;
    }
    if (this.unitModel && this.unitModel.hospitalUnitId > 0) {
      this.ward.hospitalUnitId = this.unitModel.hospitalUnitId;
    }
    
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
