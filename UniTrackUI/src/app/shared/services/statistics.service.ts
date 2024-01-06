import { Injectable } from '@angular/core';
import { Statistic } from '../models/statistic';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {

  constructor(private http: HttpClient, private userService: UserService){}

  getCurrentStudentStatistics(): Observable<Statistic> {
    const {id} = this.userService.getCurrentUserProfile();
    return this.http.get<Statistic>('http://localhost:5036/api/Recommendation/' + id, {withCredentials: true})
  }
}
