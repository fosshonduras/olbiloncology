import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthProfessionalsListComponent } from './health-professionals-list.component';

describe('HealthProfessionalsListComponent', () => {
  let component: HealthProfessionalsListComponent;
  let fixture: ComponentFixture<HealthProfessionalsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthProfessionalsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthProfessionalsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
