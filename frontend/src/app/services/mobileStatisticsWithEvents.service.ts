import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MobileStatisticsWithEvents } from '../models/mobileStatisticsWithEvents.model';
import { environment } from 'src/environments/environment';

const baseUrl = environment.apiMobileStatisticsEventUrl;

@Injectable({
  providedIn: 'root'
})
export class MobileStatisticsWithEventsService {

  constructor(private http: HttpClient) { }

  get(id: any): Observable<MobileStatisticsWithEvents> {
    return this.http.get<MobileStatisticsWithEvents>(`${baseUrl}?mobileStatisticsId=${id}`);
  }
}