import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordStorageLocationEditComponent } from './record-storage-location-edit.component';

describe('RecordStorageLocationEditComponent', () => {
  let component: RecordStorageLocationEditComponent;
  let fixture: ComponentFixture<RecordStorageLocationEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecordStorageLocationEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecordStorageLocationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
