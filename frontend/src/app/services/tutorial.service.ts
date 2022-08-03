import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tutorial } from '../models/tutorial.model';

const baseUrl = 'http://localhost:63060/Tutorial';

@Injectable({
  providedIn: 'root'
})



export class TutorialService {

  constructor(private http: HttpClient) { }
  

  getAll(): Observable<Tutorial[]> {
    return this.http.get<Tutorial[]>(baseUrl);
  }
}