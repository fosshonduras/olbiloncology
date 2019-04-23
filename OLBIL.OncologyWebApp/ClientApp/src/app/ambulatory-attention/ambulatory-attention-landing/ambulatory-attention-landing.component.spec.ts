import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmbulatoryAttentionLandingComponent } from './ambulatory-attention-landing.component';

describe('AmbulatoryAttentionLandingComponent', () => {
  let component: AmbulatoryAttentionLandingComponent;
  let fixture: ComponentFixture<AmbulatoryAttentionLandingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmbulatoryAttentionLandingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmbulatoryAttentionLandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
