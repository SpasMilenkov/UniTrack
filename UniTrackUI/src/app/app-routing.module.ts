import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthLayoutComponent } from './auth/auth-layout/auth-layout.component';
import { LoginComponent } from './auth/login/login.component';
import { SignupComponent } from './auth/signup/signup.component';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';
import { ProfileComponent } from './profile/profile/profile.component';
import { StudentsListComponent } from './students-list/students-list.component';
import { StatisticsLayoutComponent } from './statistics/statistics-layout/statistics-layout.component';
import { ConfirmEmailComponent } from './auth/confirm-email/confirm-email.component';
import { ApprovalTableComponent } from './admin/approval-table/approval-table.component';
import { AdminLayoutComponent } from './admin/admin-layout/admin-layout.component';
import { teacherGuard } from './shared/services/guards/teacher.guard';
import { studentGuard } from './shared/services/guards/student.guard';
import { adminGuard } from './shared/services/guards/admin.guard';
import { profileGuard } from './shared/services/guards/profile.guard';
import { authGuard } from './shared/services/guards/auth.guard';

const routes: Routes = [
  {
    path: 'auth',
    component: AuthLayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: SignupComponent },
      { path: 'reset-password', component: ResetPasswordComponent },
      { path: 'confirm-email', component: ConfirmEmailComponent },
    ],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [profileGuard],
  },
  {
    path: 'students-list',
    component: StudentsListComponent,
    canActivate: [teacherGuard],
  },
  {
    path: 'statistics',
    component: StatisticsLayoutComponent,
    canActivate: [studentGuard],
  },
  {
    path: 'approval-table',
    component: AdminLayoutComponent,
    canActivate: [adminGuard],
  },
  { path: '**', redirectTo: '/profile', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
