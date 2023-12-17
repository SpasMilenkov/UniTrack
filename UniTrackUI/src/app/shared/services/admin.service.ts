import { Injectable } from '@angular/core';
import { UserRequest } from '../models/user-request';
import { Roles } from '../enums/roles.enum';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  approveUsers(userIds: string) {
    console.log(userIds);
  }

  disApproveUsers(userIds: string) {
    console.log(userIds);
  }

  getUserApprovalRequests(): UserRequest[] {
    return [
      {
        id: '1',
        email: 'test1@test.com',
        firstName: 'John',
        lastName: 'Doe',
        type: Roles.STUDENT,
        approved: false
      },
      {
        id: '2',
        email: 'test2@test.com',
        firstName: 'Jane',
        lastName: 'Smith',
        type: Roles.STUDENT,
        approved: false
      },
      {
        id: '3',
        email: 'test3@test.com',
        firstName: 'Alice',
        lastName: 'Johnson',
        type: Roles.TEACHER,
        approved: true
      },
      {
        id: '4',
        email: 'test4@test.com',
        firstName: 'Bob',
        lastName: 'Anderson',
        type: Roles.STUDENT,
        approved: false
      },
      {
        id: '5',
        email: 'test5@test.com',
        firstName: 'Eva',
        lastName: 'Brown',
        type: Roles.TEACHER,
        approved: false
      },
      {
        id: '6',
        email: 'test6@test.com',
        firstName: 'Mike',
        lastName: 'Wilson',
        type: Roles.STUDENT,
        approved: false
      },
      {
        id: '7',
        email: 'test7@test.com',
        firstName: 'Sara',
        lastName: 'Miller',
        type: Roles.STUDENT,
        approved: false
      },
      {
        id: '8',
        email: 'test8@test.com',
        firstName: 'David',
        lastName: 'Clark',
        type: Roles.TEACHER,
        approved: false
      },
      {
        id: '9',
        email: 'test9@test.com',
        firstName: 'Grace',
        lastName: 'White',
        type: Roles.TEACHER,
        approved: false
      },
      {
        id: '10',
        email: 'test10@test.com',
        firstName: 'Tom',
        lastName: 'Taylor',
        type: Roles.STUDENT,
        approved: false
      },
    ];
  }
}
