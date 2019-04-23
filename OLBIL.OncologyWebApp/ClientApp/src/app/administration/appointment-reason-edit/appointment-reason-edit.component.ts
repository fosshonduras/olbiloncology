import { Component, OnInit } from '@angular/core';
import { AppointmentReasonModel, AppointmentReasonsClient } from '../../api-clients';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-appointment-reason-edit',
  templateUrl: './appointment-reason-edit.component.html',
  styleUrls: ['./appointment-reason-edit.component.css']
})
export class AppointmentReasonEditComponent implements OnInit {
  pageTitle: string;
  isNewRecord: boolean = false;
  appointmentReasonId: number = -1;
  appointmentReason: AppointmentReasonModel;
  isSaving: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: AppointmentReasonsClient
  ) {
    this.appointmentReason = new AppointmentReasonModel();
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("appointmentReasonId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
    } else {
      this.appointmentReasonId = +id;
    }

    this.pageTitle = `${this.isNewRecord ? "Nueva" : "Edición de"} Razón para cita`;

    this.setupTargetRecord();
  }

  setupTargetRecord(): any {
    if (!this.isNewRecord) {
      this.client.getAppointmentReason(this.appointmentReasonId)
        .subscribe(result => {
          this.appointmentReason = result;
        }, err => {
          this.toastr.warning(err);
        });
    }
  }

  submitRegistration(regForm) {
    this.isSaving = true;

    if (this.isNewRecord) {
      this.client.createAppointmentReason(this.appointmentReason)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateAppointmentReason(this.appointmentReason)
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
      this.router.navigate(['./appointmentreasons'], { relativeTo: this.route.parent });
    }, 2000);
  }
}
