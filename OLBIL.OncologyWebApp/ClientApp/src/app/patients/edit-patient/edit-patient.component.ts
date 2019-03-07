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
  isSaving: boolean = false;

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
      this.toastr.warning(this.extractErrorMessage(err));
    });
  }

  saveRecord(regForm) {
    if (this.isSaving) return;
    this.isSaving = true;
    if (this.isNewRecord) {
      this.http.post<any>(`${this.baseUrl}api/oncologyPatient/`, this.patient).subscribe(result => {
        this.toastr.success("Paciente registrado.");
        setTimeout(() => {
          this.router.navigate(['./list'], { relativeTo: this.route.parent });
        }, 2000);
      }, err => {
        this.isSaving = false;
        this.toastr.warning(this.extractErrorMessage(err));
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
