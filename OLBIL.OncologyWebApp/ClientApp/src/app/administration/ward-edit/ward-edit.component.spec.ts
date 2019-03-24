import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WardEditComponent } from './ward-edit.component';

describe('WardEditComponent', () => {
  let component: WardEditComponent;
  let fixture: ComponentFixture<WardEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WardEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WardEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
