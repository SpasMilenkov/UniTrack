import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveTeacherDialogComponent } from './approve-teacher-dialog.component';

describe('ApproveTeacherDialogComponent', () => {
  let component: ApproveTeacherDialogComponent;
  let fixture: ComponentFixture<ApproveTeacherDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ApproveTeacherDialogComponent]
    });
    fixture = TestBed.createComponent(ApproveTeacherDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
