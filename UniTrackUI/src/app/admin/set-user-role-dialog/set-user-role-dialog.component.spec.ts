import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetUserRoleDialogComponent } from './set-user-role-dialog.component';

describe('SetUserRoleDialogComponent', () => {
  let component: SetUserRoleDialogComponent;
  let fixture: ComponentFixture<SetUserRoleDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SetUserRoleDialogComponent]
    });
    fixture = TestBed.createComponent(SetUserRoleDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
