import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MobileStatistics } from '../models/mobileStatistics.model';

const baseUrl = 'http://localhost:63060/mobileStatistics';

@Injectable({
  providedIn: 'root'
})



export class MobileStatisticsService {

  constructor(private http: HttpClient) { }
  

  getAll(): Observable<MobileStatistics[]> {
    return this.http.get<MobileStatistics[]>(baseUrl);
  }
  get(id: any): Observable<MobileStatistics> {
    return this.http.get<MobileStatistics>(`${baseUrl}/${id}`);
  }
  
  create(data: any): Observable<any> {
    return this.http.post(baseUrl, data);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put(`${baseUrl}/${id}`, data);
  }

}