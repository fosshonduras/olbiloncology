import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentReasonsListComponent } from './appointment-reasons-list.component';

describe('AppointmentReasonsListComponent', () => {
  let component: AppointmentReasonsListComponent;
  let fixture: ComponentFixture<AppointmentReasonsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppointmentReasonsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppointmentReasonsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
