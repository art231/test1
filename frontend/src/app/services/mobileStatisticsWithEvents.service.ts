import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MobileStatisticsWithEvents } from '../models/mobileStatisticsWithEvents.model';
import { MobileStatisticsEvents } from '../models/mobileStatisticsEvents.model';
import { environment } from 'src/environments/environment';

const baseUrl = environment.apiMobileStatisticsEventUrl;

@Injectable({
  providedIn: 'root'
})
export class MobileStatisticsWithEventsService {

  constructor(private http: HttpClient) { }

  getEventsByMobileStatisticsId(id: any): Observable<MobileStatisticsWithEvents> {
    return this.http.get<MobileStatisticsWithEvents>(`${baseUrl}?mobileStatisticsId=${id}`);
  }
  getEventById(id: any): Observable<MobileStatisticsEvents> {
    return this.http.get<MobileStatisticsEvents>(`${baseUrl}/event?mobileStatisticsEventId=${id}`);
  }
  update(id: any, data: any): Observable<any> {
    return this.http.put(`${baseUrl}`, data);
  }
  deleteEventById(id: any): Observable<MobileStatisticsEvents> {
    return this.http.delete<MobileStatisticsEvents>(`${baseUrl}/event?mobileStatisticsEventId=${id}`);
  }
}