import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrativeDivisionsListComponent } from './administrative-divisions-list.component';

describe('AdministrativeDivisionsListComponent', () => {
  let component: AdministrativeDivisionsListComponent;
  let fixture: ComponentFixture<AdministrativeDivisionsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministrativeDivisionsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministrativeDivisionsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
