import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../user.service';
import { inject } from '@angular/core';
import { Roles } from '../../enums/roles.enum';

export const authGuard: CanActivateFn = (route, state) => {
  const router: Router = inject(Router);
  const userService: UserService = inject(UserService);
  const accessAuthPages = !userService.getRole() || userService.getRole() === Roles.GUEST;

  return accessAuthPages;
};
