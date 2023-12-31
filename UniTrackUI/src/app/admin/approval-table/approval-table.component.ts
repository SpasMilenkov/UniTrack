import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, tap } from 'rxjs';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { UserRequest } from 'src/app/shared/models/user-request';
import { AdminService } from 'src/app/shared/services/admin.service';

@Component({
  selector: 'app-approval-table',
  templateUrl: './approval-table.component.html',
  styleUrls: ['./approval-table.component.scss'],
})
export class ApprovalTableComponent implements OnInit, OnChanges {
  displayedColumns: string[] = [
    'select',
    'email',
    'firstName',
    'lastName',
    'approved',
    'actions',
  ];
  selection = new SelectionModel<UserRequest>(true, []);
  roles = Roles;

  dataSource!: MatTableDataSource<UserRequest>;
  @Input() userRequests: UserRequest[] = [];
  @Output() onApprove = new EventEmitter<string[] | string>();

  constructor(private adminService: AdminService) {}

  ngOnChanges(changes: SimpleChanges): void {
      console.log(changes)
      if (changes['userRequests'].currentValue) {
        this.dataSource = new MatTableDataSource<UserRequest>(
        changes['userRequests'].currentValue
      )
    }
  }

  ngOnInit(): void {
    console.log(this.userRequests)
    this.dataSource = new MatTableDataSource<UserRequest>(
      this.userRequests
    )
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  checkboxLabel(row?: UserRequest): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${
      row.email + 1
    }`;
  }

  approve(id?: string): void {
    const clonedIds = this.selection.selected.map(({ userId }) => userId);


    const selectedIds = id ? id : clonedIds;
    this.onApprove.emit(selectedIds);
  }

  selectRow(row: any): void {
    this.selection.toggle(row);
  }
}
