import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmbulatoryAttentionRecordEditComponent } from './ambulatory-attention-record-edit.component';

describe('AmbulatoryAttentionRecordEditComponent', () => {
  let component: AmbulatoryAttentionRecordEditComponent;
  let fixture: ComponentFixture<AmbulatoryAttentionRecordEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmbulatoryAttentionRecordEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmbulatoryAttentionRecordEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
