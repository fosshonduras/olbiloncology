import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmbulatoryAttentionRecordsListComponent } from './ambulatory-attention-records-list.component';

describe('AmbulatoryAttentionRecordsListComponent', () => {
  let component: AmbulatoryAttentionRecordsListComponent;
  let fixture: ComponentFixture<AmbulatoryAttentionRecordsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmbulatoryAttentionRecordsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmbulatoryAttentionRecordsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
