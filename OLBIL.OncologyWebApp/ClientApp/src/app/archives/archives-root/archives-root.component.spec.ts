import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchivesRootComponent } from './archives-root.component';

describe('ArchivesRootComponent', () => {
  let component: ArchivesRootComponent;
  let fixture: ComponentFixture<ArchivesRootComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArchivesRootComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArchivesRootComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
