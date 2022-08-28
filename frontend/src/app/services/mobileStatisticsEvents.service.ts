import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MobileStatisticsEvents } from '../models/mobileStatisticsEvents.model';

const baseUrl = 'http://localhost:63060/mobileStatisticsEvents';

@Injectable({
  providedIn: 'root'
})
export class MobileStatisticsEventsService {

  constructor(private http: HttpClient) { }

  get(id: any): Observable<MobileStatisticsEvents[]> {
    return this.http.get<MobileStatisticsEvents[]>(`${baseUrl}?mobileStatisticsId=${id}`);
  }
}