import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ProfileTypes } from 'src/app/shared/enums/profile-types.enum';
import { UserRequest } from 'src/app/shared/models/user-request';

@Component({
  selector: 'app-approval-table',
  templateUrl: './approval-table.component.html',
  styleUrls: ['./approval-table.component.scss'],
})
export class ApprovalTableComponent implements OnInit {
  displayedColumns: string[] = [
    'select',
    'email',
    'type',
    'firstName',
    'lastName',
    'approved',
    'actions',
  ];
  selection = new SelectionModel<UserRequest>(true, []);
  profileTypes = ProfileTypes;

  dataSource!: MatTableDataSource<UserRequest>;
  @Input() userRequests: UserRequest[] = [];
  @Output() onApprove = new EventEmitter<string[]>();
  @Output() onDisapprove = new EventEmitter<string[]>();

  constructor() {}

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<UserRequest>(this.userRequests);
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
    if (id) {
      this.onApprove.emit([id]);
      return;
    }
    const clonedIds = this.selection.selected.map(({ id }) => id);
    this.onApprove.emit(clonedIds);
  }

  disapprove(id?: string): void {
    if (id) {
      this.onDisapprove.emit([id]);
      return;
    }
    const clonedIds = this.selection.selected.map(({ id }) => id);
    this.onDisapprove.emit(clonedIds);
  }
}
