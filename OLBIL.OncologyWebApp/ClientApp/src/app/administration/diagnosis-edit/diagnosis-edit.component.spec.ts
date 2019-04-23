import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnosisEditComponent } from './diagnosis-edit.component';

describe('DiagnosisEditComponent', () => {
  let component: DiagnosisEditComponent;
  let fixture: ComponentFixture<DiagnosisEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiagnosisEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagnosisEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
