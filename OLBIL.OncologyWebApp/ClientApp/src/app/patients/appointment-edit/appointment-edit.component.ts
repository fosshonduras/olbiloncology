import { Component, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged, tap, switchMap, map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import {
  OncologyPatientModel, HealthProfessionalModel, AppointmentsClient,
  OncologyPatientsClient, HealthProfessionalsClient, AppointmentModel, AppointmentReasonModel, AppointmentReasonsClient
} from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';

@Component({
  selector: 'app-appointment-edit',
  templateUrl: './appointment-edit.component.html',
  styleUrls: ['./appointment-edit.component.css']
})
export class AppointmentEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  appointmentId: number = -1;
  appointment: AppointmentModel;
  isSaving: boolean = false;

  oncologyPatientModel: OncologyPatientModel;
  isSearchingOncologyPatient: boolean = false;
  oncologyPatientSearchFailed: boolean = false;

  healthProfessionalModel: HealthProfessionalModel;
  isSearchingHealthProfessional: boolean = false;
  healthProfessionalSearchFailed: boolean = false;

  appointmentReasonModel: AppointmentReasonModel;
  isSearchingAppointmentReason: boolean = false;
  appointmentReasonSearchFailed: boolean = false;

  appointmentDate: string;

  appointmentStatuses: any[] = [
    { appointmentStatusId: 1, name: "Programada" },
    { appointmentStatusId: 2, name: "En Proceso" },
    { appointmentStatusId: 2, name: "Reprogramada" },
    { appointmentStatusId: 2, name: "Cancelada" }
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: AppointmentsClient,
    private oncologyPatientsClient: OncologyPatientsClient,
    private healthProfessionalsClient: HealthProfessionalsClient,
    private appointmentReasonsClient: AppointmentReasonsClient
  ) {
    this.appointment = new AppointmentModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("appointmentId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
      this.updateDates();
    } else {
      this.appointmentId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Cita`;

    this.setupTargetRecord();
  }

  private updateDates() {
    this.appointmentDate = this.appointment.date && moment(this.appointment.date).format(moment.HTML5_FMT.DATE);
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

  healthProfessionalTAFormatter = (x: HealthProfessionalModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  healthProfessionalSearchResultFormatter = (x: HealthProfessionalModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  searchHealthProfessional = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingHealthProfessional = true),
      switchMap(term =>
        term.length < 2 ? of(Array<HealthProfessionalModel>()) :
        this.healthProfessionalsClient.search(term)
          .pipe(
            map((bm, bmi) =>  bm.items),
            tap(() => this.healthProfessionalSearchFailed = false),
            catchError(() => {
              this.healthProfessionalSearchFailed = true;
              return of(Array<HealthProfessionalModel>());
            }))
      ),
      tap(() => this.isSearchingHealthProfessional = false)
    )

  appointmentReasonTAFormatter = (x: AppointmentReasonModel) => `${x.description}`;

  appointmentReasonSearchResultFormatter = (x: AppointmentReasonModel) => `${x.description}`;

  searchAppointmentReason = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingAppointmentReason = true),
      switchMap(term =>
        term.length < 2 ? of(Array<AppointmentReasonModel>()) :
          this.appointmentReasonsClient.search(term)
            .pipe(
              map((bm, bmi) => bm.items),
              tap(() => this.appointmentReasonSearchFailed = false),
              catchError(() => {
                this.appointmentReasonSearchFailed = true;
                return of(Array<AppointmentReasonModel>());
              }))
      ),
      tap(() => this.isSearchingAppointmentReason = false)
    )

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getAppointment(this.appointmentId)
        .subscribe(result => {
          this.appointment = result;
          this.updateDates();

          this.oncologyPatientsClient.getPatient(this.appointment.oncologyPatientId)
            .subscribe(result => {
              this.oncologyPatientModel = result;
            }, err => {
              this.toastr.warning(err);
            });
          this.healthProfessionalsClient.getHealthProfessional(this.appointment.healthProfessionalId)
            .subscribe(result => {
              this.healthProfessionalModel = result;
            }, err => {
              this.toastr.warning(err);
            });

          this.appointmentReasonsClient.getAppointmentReason(this.appointment.appointmentReasonId)
            .subscribe(result => {
              this.appointmentReasonModel = result;
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
    this.appointment.date = this.appointmentDate && new Date(this.appointmentDate)

    if (this.oncologyPatientModel && this.oncologyPatientModel.oncologyPatientId > 0) {
      this.appointment.oncologyPatientId = this.oncologyPatientModel.oncologyPatientId;
    }
    if (this.healthProfessionalModel && this.healthProfessionalModel.healthProfessionalId > 0) {
      this.appointment.healthProfessionalId = this.healthProfessionalModel.healthProfessionalId;
    }
    if (this.appointmentReasonModel && this.appointmentReasonModel.appointmentReasonId > 0) {
      this.appointment.appointmentReasonId = this.appointmentReasonModel.appointmentReasonId;
    }
    
    if (this.isNewRecord) {
      this.client.createAppointment(this.appointment)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateAppointment(this.appointment)
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
      this.router.navigate(['./appointments'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
