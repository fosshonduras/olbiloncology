import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmbulatoryAttentionRootComponent } from './ambulatory-attention-root.component';

describe('AmbulatoryAttentionRootComponent', () => {
  let component: AmbulatoryAttentionRootComponent;
  let fixture: ComponentFixture<AmbulatoryAttentionRootComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmbulatoryAttentionRootComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmbulatoryAttentionRootComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
