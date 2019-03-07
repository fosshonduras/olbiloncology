import { Component, OnInit, Input, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-patient',
  templateUrl: './edit-patient.component.html',
  styleUrls: ['./edit-patient.component.css']
})
export class EditPatientComponent implements OnInit {
  @Input()
  patient: any = {
    person: {}
  };

  @Input()
  isNewRecord: boolean = false;
  showEditForm: boolean = true;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get("patientId");

    if (!this.isNewRecord && id === null) {
      alert("Id not provided");
    } else if (this.isNewRecord) {
      return;
    }

    this.http.get<any>(`${this.baseUrl}api/oncologyPatient/${id}`).subscribe(result => {
      this.patient = result;
    }, err => {
      console.error(err.error[0]);
    });
  }

  saveRecord(regForm) {
    if (this.isNewRecord) {
      this.http.post<any>(`${this.baseUrl}api/oncologyPatient/`, this.patient).subscribe(result => {

      }, err => {
        this.toastr.error(err.error[0]);
      });
    }
  }
}
