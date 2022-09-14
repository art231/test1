import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MobileStatistics } from '../models/mobileStatistics.model';
import { environment } from 'src/environments/environment';

const baseUrl = environment.apiMobileStatisticsUrl;

@Injectable({
  providedIn: 'root'
})



export class MobileStatisticsService {

  constructor(private http: HttpClient) { }
  
  handleError:any;
  getAll(): Observable<MobileStatistics[]> {
    return this.http
      .get<MobileStatistics[]>(baseUrl)
      .pipe(catchError(this.handleError));
  }
  get(id: any): Observable<MobileStatistics> {
    return this.http.get<MobileStatistics>(`${baseUrl}/${id}`);
  }
  
  create(data: any): Observable<any> {
    return this.http.post(baseUrl, data);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put(`${baseUrl}`, data);
  }

}