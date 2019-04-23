import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrativeDivisionEditComponent } from './administrative-division-edit.component';

describe('AdministrativeDivisionEditComponent', () => {
  let component: AdministrativeDivisionEditComponent;
  let fixture: ComponentFixture<AdministrativeDivisionEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministrativeDivisionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministrativeDivisionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
