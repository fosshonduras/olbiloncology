import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchivesLandingComponent } from './archives-landing.component';

describe('ArchivesLandingComponent', () => {
  let component: ArchivesLandingComponent;
  let fixture: ComponentFixture<ArchivesLandingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesLandingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesLandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
