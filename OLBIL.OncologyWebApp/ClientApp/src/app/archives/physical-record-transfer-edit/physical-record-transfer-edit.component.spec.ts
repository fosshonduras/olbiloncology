import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalRecordTransferEditComponent } from './physical-record-transfer-edit.component';

describe('PhysicalRecordTransferEditComponent', () => {
  let component: PhysicalRecordTransferEditComponent;
  let fixture: ComponentFixture<PhysicalRecordTransferEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhysicalRecordTransferEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalRecordTransferEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
