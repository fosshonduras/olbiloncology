import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientPhysicalRecordsListComponent } from './patient-physical-records-list.component';

describe('PatientPhysicalRecordsListComponent', () => {
  let component: PatientPhysicalRecordsListComponent;
  let fixture: ComponentFixture<PatientPhysicalRecordsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientPhysicalRecordsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientPhysicalRecordsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
