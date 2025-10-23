import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { Trip, Route, Ship, Plane, Hotel } from '../app/model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl = 'https://localhost:5124';

  constructor(private httpClient: HttpClient) {

  }

  /*
  getSomething(orderBy: string): Observable<Someting[]> {
    let httpParams = new HttpParams();
    httpParams = httpParams.set('sort', orderBy);
    const url = `${this.baseUrl}/Someting?${httpParams.toString()}`;
    return this.httpClient.get<Someting[]>(url);
  }
*/
}
