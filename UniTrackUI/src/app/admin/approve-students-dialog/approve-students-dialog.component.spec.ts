import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveStudentsDialogComponent } from './approve-students-dialog.component';

describe('ApproveStudentsDialogComponent', () => {
  let component: ApproveStudentsDialogComponent;
  let fixture: ComponentFixture<ApproveStudentsDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ApproveStudentsDialogComponent]
    });
    fixture = TestBed.createComponent(ApproveStudentsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
