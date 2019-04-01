import { Component, OnInit, Input } from '@angular/core';
import { HealthProfessionalsClient, HealthProfessionalModel, PersonModel } from '../../api-clients';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import moment = require('moment');

@Component({
  selector: 'app-health-professional-edit',
  templateUrl: './health-professional-edit.component.html',
  styleUrls: ['./health-professional-edit.component.css']
})
export class HealthProfessionalEditComponent implements OnInit {
  medic: any = { person: {} };
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
    pageTitle: string;

  constructor(
    private client: HealthProfessionalsClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("medicId");

    if (id === "new" || id === null) {
      this.isNewRecord = true;
      this.medic = new HealthProfessionalModel({
        person: new PersonModel()
      });
    } else {
      this.client.getHealthProfessional(+id).subscribe(result => {
        this.medic = result;
        this.updateDatesInPatient();
      }, err => {
        this.toastr.warning(this.extractErrorMessage(err));
      });
    }

    this.pageTitle = `${this.isNewRecord ? "Nuevo" : "Edición de"} Médico`;
  }

  private updateDatesInPatient() {
    this.medic.person.birthdate = this.medic.person.birthdate && moment(this.medic.person.birthdate).format(moment.HTML5_FMT.DATE);
  }

  saveRecord(regForm) {
    if (this.isSaving) return;
    this.isSaving = true;
    this.medic.person.birthdate = this.medic.person.birthdate && new Date(this.medic.person.birthdate);

    if (this.isNewRecord) {
      this.client.createHealthProfessional(this.medic)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    } else {
      this.client.updateHealthProfessional(this.medic)
        .subscribe(r => this.handleSuccess(r), e => this.handleFailure(e));
    }
  }

  handleFailure(err) {
    this.isSaving = false;
    this.toastr.warning(err.error ? this.extractErrorMessage(err) : err.message);
  }

  handleSuccess(result) {
    this.toastr.success("Datos guardados.");
    setTimeout(() => {
      this.router.navigate(['./healthprofessionals'], { relativeTo: this.route.parent });
    }, 2000);
  }

  extractErrorMessage(err) {
    let message = JSON.parse(err.response).error;
    return message;
  }
}
