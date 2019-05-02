import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalRecordTransfersListComponent } from './physical-record-transfers-list.component';

describe('PhysicalRecordTransfersListComponent', () => {
  let component: PhysicalRecordTransfersListComponent;
  let fixture: ComponentFixture<PhysicalRecordTransfersListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhysicalRecordTransfersListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalRecordTransfersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
