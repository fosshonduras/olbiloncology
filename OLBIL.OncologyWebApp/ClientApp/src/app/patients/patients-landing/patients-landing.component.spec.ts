import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientsLandingComponent } from './patients-landing.component';

describe('PatientsLandingComponent', () => {
  let component: PatientsLandingComponent;
  let fixture: ComponentFixture<PatientsLandingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientsLandingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientsLandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
