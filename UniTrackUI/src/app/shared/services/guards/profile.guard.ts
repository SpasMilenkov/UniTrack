import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../user.service';
import { inject } from '@angular/core';
import { Roles } from '../../enums/roles.enum';

export const profileGuard: CanActivateFn = (route, state) => {
  const userService: UserService = inject(UserService);
  const router: Router = inject(Router);

  const role = userService.getRole();
  if(role === Roles.ADMIN){
    router.navigateByUrl('approval-table');
  }

  return (role === Roles.STUDENT || userService.getRole() === Roles.TEACHER);
};
