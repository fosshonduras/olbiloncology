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
      //this.patient = new OncologyPatientModel({
      //  oncologyPatientId: 314,
      //  person: new PersonModel({
      //    personId: 'A',
      //    firstName: "Karla",
      //    lastName: "Tulio",
      //    governmentIDNumber: "01012020",
      //    nationality: 'Nigeriano',
      //    birthdate: moment('2000-12-12')
      //  })
      //});
      return;
    }

    this.client.getPatient(+id).subscribe(result => {
      this.patient = result;
      this.patient.person.birthdate = moment(this.patient.person.birthdate).format(moment.HTML5_FMT.DATE);
      this.patient.admissionDate = moment(this.patient.admissionDate).format(moment.HTML5_FMT.DATE);
    }, err => {
      this.toastr.warning(this.extractErrorMessage(err));
    });
  }

  saveRecord(regForm) {
    if (this.isSaving) return;
    this.isSaving = true;
    this.patient.person.birthdate = new Date(this.patient.person.birthdate);
    this.patient.admissionDate = new Date(this.patient.admissionDate);
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
