import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentReasonEditComponent } from './appointment-reason-edit.component';

describe('AppointmentReasonEditComponent', () => {
  let component: AppointmentReasonEditComponent;
  let fixture: ComponentFixture<AppointmentReasonEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppointmentReasonEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppointmentReasonEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
