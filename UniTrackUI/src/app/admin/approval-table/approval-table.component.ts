import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Roles } from 'src/app/shared/enums/roles.enum';
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
  roles = Roles;

  dataSource!: MatTableDataSource<UserRequest>;
  @Input() userRequests: UserRequest[] = [];
  @Output() onApproveStudents = new EventEmitter<string[]>();
  @Output() onApproveTeacher = new EventEmitter<string>();
  @Output() onDisapprove = new EventEmitter<string[]>();

  constructor() {}

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<UserRequest>(this.userRequests);
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.filter(({type}) => type === this.roles.STUDENT).length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data.filter(({type}) => type === this.roles.STUDENT));
  }

  checkboxLabel(row?: UserRequest): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${
      row.email + 1
    }`;
  }

  approve(type: string, id?: string): void {
    if(type === this.roles.STUDENT){
      if (id) {
        this.onApproveStudents.emit([id]);
        return;
      }
      const clonedIds = this.selection.selected.map(({ id }) => id);
      this.onApproveStudents.emit(clonedIds);
      return;
    }
    this.onApproveTeacher.emit(id);
  }

  disapprove(id?: string): void {
    if (id) {
      this.onDisapprove.emit([id]);
      return;
    }
    const clonedIds = this.selection.selected.map(({ id }) => id);
    this.onDisapprove.emit(clonedIds);
  }

  selectRow(row: any): void {
    if(row.type === this.roles.STUDENT){
      this.selection.toggle(row);
    }
  }
}
