import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalSpecialtiesListComponent } from './medical-specialties-list.component';

describe('MedicalSpecialtiesListComponent', () => {
  let component: MedicalSpecialtiesListComponent;
  let fixture: ComponentFixture<MedicalSpecialtiesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MedicalSpecialtiesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicalSpecialtiesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
