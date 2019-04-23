import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalSpecialtyEditComponent } from './medical-specialty-edit.component';

describe('MedicalSpecialtyEditComponent', () => {
  let component: MedicalSpecialtyEditComponent;
  let fixture: ComponentFixture<MedicalSpecialtyEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MedicalSpecialtyEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicalSpecialtyEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
