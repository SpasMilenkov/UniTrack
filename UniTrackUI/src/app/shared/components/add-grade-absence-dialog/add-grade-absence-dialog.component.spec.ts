import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGradeAbsenceDialogComponent } from './add-grade-absence-dialog.component';

describe('AddGradeAbsenceDialogComponent', () => {
  let component: AddGradeAbsenceDialogComponent;
  let fixture: ComponentFixture<AddGradeAbsenceDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddGradeAbsenceDialogComponent]
    });
    fixture = TestBed.createComponent(AddGradeAbsenceDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
