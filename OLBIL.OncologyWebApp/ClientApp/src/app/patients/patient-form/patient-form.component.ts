import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.css']
})
export class PatientFormComponent implements OnInit {
  @Input()
  patient: any = {
    person: {}
  };
  
  @Input()
  isNewRecord: boolean = false;
isSaving: boolean = false;
  showEditForm: boolean = true;

  constructor() { }

  ngOnInit() {
  }

}
