import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordStorageLocationsListComponent } from './record-storage-locations-list.component';

describe('RecordStorageLocationsListComponent', () => {
  let component: RecordStorageLocationsListComponent;
  let fixture: ComponentFixture<RecordStorageLocationsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecordStorageLocationsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordStorageLocationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
