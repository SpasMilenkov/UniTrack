import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { MaterialModule } from './shared/material.module';
import { AuthModule } from './auth/auth.module';
import { ProfileModule } from './profile/profile.module';
import { StatisticsModule } from './statistics/statistics/statistics.module';
import { LeaderboardModule } from './leaderboard/leaderboard.module';
import { SharedModule } from './shared/shared.module';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    AuthModule,
    ProfileModule,
    StatisticsModule,
    LeaderboardModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
