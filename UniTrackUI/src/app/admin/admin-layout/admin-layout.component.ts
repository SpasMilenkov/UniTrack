import { Component, OnInit } from '@angular/core';
import { UserRequest } from 'src/app/shared/models/user-request';
import { AdminService } from 'src/app/shared/services/admin.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {
  userRequests!: UserRequest[];
  approvedUserRequests!: UserRequest[];
  disApprovedUserRequests!: UserRequest[];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.userRequests = this.adminService.getUserApprovalRequests();
  }

  onApprove(event: any): void{
    console.log(event);
  }

  onDisapprove(event: any): void{
    console.log(event);
  }
}
