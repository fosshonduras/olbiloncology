import { Component, OnInit, Input, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import * as  moment from "moment";
import { OncologyPatientClient, IOncologyPatientModel, OncologyPatientModel, PersonModel } from 'src/app/api-clients';

@Component({
  selector: 'app-edit-patient',
  templateUrl: './edit-patient.component.html',
  styleUrls: ['./edit-patient.component.css']
})
export class EditPatientComponent implements OnInit {
  @Input()
  patient: any = { person: {} };

  @Input()
  isNewRecord: boolean = false;
  showEditForm: boolean = true;
  isSaving: boolean = false;

  availableGenders: any[] = [
    { id: 1, name: "Masculino" },
    { id: 2, name: "Femenino" },
    { id: 3, name: "Indeterminado" }
  ];

  schoolLevels: any[] = [
    { id: 1, name: "Ninguno" },
    { id: 2, name: "Pre-Escolar" },
    { id: 3, name: "Primaria" },
    { id: 3, name: "Secundaria" },
    { id: 3, name: "Bachillerato" },
    { id: 3, name: "Universidad" }
  ];

  constructor(
    private client: OncologyPatientClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("patientId");

    if (!this.isNewRecord && id === null) {
      alert("Id not provided");
    } else if (this.isNewRecord) {
      this.updateDatesInPatient();
      return;
    }

    this.client.getPatient(+id).subscribe(result => {
      this.patient = result;
      this.updateDatesInPatient();
    }, err => {
      this.toastr.warning(this.extractErrorMessage(err));
    });
  }

    private updateDatesInPatient() {
        this.patient.person.birthdate = this.patient.person.birthdate && moment(this.patient.person.birthdate).format(moment.HTML5_FMT.DATE);
        this.patient.admissionDate = this.patient.admissionDate && moment(this.patient.admissionDate).format(moment.HTML5_FMT.DATE);
    }

  saveRecord(regForm) {
    if (this.isSaving) return;
    this.isSaving = true;
    this.patient.person.birthdate = this.patient.person.birthdate && new Date(this.patient.person.birthdate);
    this.patient.admissionDate = this.patient.admissionDate && new Date(this.patient.admissionDate);
    if (this.isNewRecord) {
      this.client.createPatient(this.patient).subscribe(result => {
        this.toastr.success("Paciente registrado.");
        setTimeout(() => {
          this.router.navigate(['./list'], { relativeTo: this.route.parent });
        }, 2000);
      }, err => {
        this.isSaving = false;
        this.toastr.warning(err.error ? this.extractErrorMessage(err) : err.message);
      });
    } else {
      this.client.updatePatient(this.patient).subscribe(result => {
        this.toastr.success("Paciente actualizado.");
        setTimeout(() => {
          this.router.navigate(['./list'], { relativeTo: this.route.parent });
        }, 2000);
      }, err => {
        this.isSaving = false;
        this.toastr.warning(err.error ? this.extractErrorMessage(err) : err.message);
      });
    }
  }

  extractErrorMessage(err) {
    let errorObject = err.error;
    let messageArray = errorObject.error;
    let message = messageArray.join("\n");
    return message;
  }
}
