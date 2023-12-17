import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserRequest } from 'src/app/shared/models/user-request';
import { AdminService } from 'src/app/shared/services/admin.service';
import { ApproveTeacherDialogComponent } from '../approve-teacher-dialog/approve-teacher-dialog.component';
import { ApproveTeacherData } from 'src/app/shared/models/approve-teacher-data';
import { ApproveStudentsDialogComponent } from '../approve-students-dialog/approve-students-dialog.component';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss'],
})
export class AdminLayoutComponent implements OnInit {
  userRequests!: UserRequest[];
  approvedUserRequests!: UserRequest[];
  disApprovedUserRequests!: UserRequest[];

  constructor(private adminService: AdminService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.userRequests = this.adminService.getUserApprovalRequests();
  }

  onApproveStudents(ids: string[]): void {
    const dialogRef = this.dialog.open(ApproveStudentsDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      this.adminService.approveStudents({ ids, ...result });
    });
  }

  onApproveTeacher(userId: string): void {
    console.log(userId);
    const dialogRef = this.dialog.open(ApproveTeacherDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      this.adminService.approveTeacher({
        userId,
        ...result,
      } as ApproveTeacherData);
    });
  }

  onDisapprove(event: any): void {
    this.adminService.disApproveUsers(event);
  }
}
