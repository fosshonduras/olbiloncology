import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthProfessionalEditComponent } from './health-professional-edit.component';

describe('HealthProfessionalEditComponent', () => {
  let component: HealthProfessionalEditComponent;
  let fixture: ComponentFixture<HealthProfessionalEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthProfessionalEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthProfessionalEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
