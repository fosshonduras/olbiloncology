import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientPhysicalRecordEditComponent } from './patient-physical-record-edit.component';

describe('PatientPhysicalRecordEditComponent', () => {
  let component: PatientPhysicalRecordEditComponent;
  let fixture: ComponentFixture<PatientPhysicalRecordEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientPhysicalRecordEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientPhysicalRecordEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
