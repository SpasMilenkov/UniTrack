import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserRequest } from 'src/app/shared/models/user-request';
import { AdminService } from 'src/app/shared/services/admin.service';
import { ApproveTeacherDialogComponent } from '../approve-teacher-dialog/approve-teacher-dialog.component';
import { ApproveTeacherData } from 'src/app/shared/models/approve-teacher-data';
import { ApproveStudentsDialogComponent } from '../approve-students-dialog/approve-students-dialog.component';
import { LocalStorageKeys } from 'src/app/shared/enums/local-storage-keys.enum';
import { Profile } from 'src/app/shared/models/profile';
import { SetUserRoleDialogComponent } from '../set-user-role-dialog/set-user-role-dialog.component';
import { Roles } from 'src/app/shared/enums/roles.enum';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss'],
})
export class AdminLayoutComponent implements OnInit {
  approvedUserRequests!: UserRequest[];
  schoolId: string = '';

  constructor(private adminService: AdminService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.schoolId = (JSON.parse(localStorage.getItem(LocalStorageKeys.CURRENT_USER) || '2') as Profile).schoolId;
  }

  onApproveStudents(ids: string[]): void {
    const dialogRef = this.dialog.open(ApproveStudentsDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      this.adminService.approveStudents({ schoolId: this.schoolId,  studentIds: ids, ...result }).subscribe();
    });
  }

  onApproveTeacher(userId: string): void {
    console.log(userId);
    const dialogRef = this.dialog.open(ApproveTeacherDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      this.adminService.approveTeacher({
        schoolId: this.schoolId,
        userId,
        ...result,
      } as ApproveTeacherData);
    });
  }

  onApprove(ids: string | string[]): void {
    const dialogRef = this.dialog.open(SetUserRoleDialogComponent, {
      data: {ids}
    });

    dialogRef.afterClosed().subscribe((role) => {
      if(role?.role){
        switch(role.role) {
          case Roles.STUDENT: {
            this.onApproveStudents(Array.isArray(ids) ? ids : [ids]);
            break;
          }
          case Roles.TEACHER: {
            this.onApproveTeacher(Array.isArray(ids) ? ids[0] : ids);
            break;
          }
        }
      }
    });
  }
}
