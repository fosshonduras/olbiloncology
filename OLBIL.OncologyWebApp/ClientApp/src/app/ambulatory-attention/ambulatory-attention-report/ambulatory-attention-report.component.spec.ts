import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmbulatoryAttentionReportComponent } from './ambulatory-attention-report.component';

describe('AmbulatoryAttentionReportComponent', () => {
  let component: AmbulatoryAttentionReportComponent;
  let fixture: ComponentFixture<AmbulatoryAttentionReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmbulatoryAttentionReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmbulatoryAttentionReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
