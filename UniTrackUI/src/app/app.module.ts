import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { MaterialModule } from './shared/material.module';
import { AuthModule } from './auth/auth.module';
import { ProfileModule } from './profile/profile.module';
import { StatisticsModule } from './statistics/statistics.module';
import { LeaderboardModule } from './leaderboard/leaderboard.module';
import { SharedModule } from './shared/shared.module';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { StudentsListModule } from './students-list/students-list.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    MaterialModule,
    AuthModule,
    ProfileModule,
    StatisticsModule,
    LeaderboardModule,
    SharedModule,
    AppRoutingModule,
    StudentsListModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
