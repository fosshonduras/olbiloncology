import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-create-patient',
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.css']
})
export class CreatePatientComponent implements OnInit {
  patient: any = {
    person: {}
  };
  isSaving: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  createPatient(regForm) {
    this.isSaving = true;
    this.http.post<any>(this.baseUrl + 'api/oncologyPatient/attempt', this.patient).subscribe(res => {
      this.patient = { person: {} }
      this.isSaving = false;
    }, er => {
      console.error(er)
      this.isSaving = false;
    });
  }
  ngOnInit() {
  }

}
