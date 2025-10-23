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

  getTrips(orderBy: string): Observable<Trip[]> {
    let httpParams = new HttpParams();
    httpParams = httpParams.set('sort', orderBy);
    const url = `${this.baseUrl}/Trip?${httpParams.toString()}`;
    return this.httpClient.get<Trip[]>(url);
  }

  getTrip(id: number): Observable<Trip> {
    const url = `${this.baseUrl}/Trip/${id}`;
    return this.httpClient.get<Trip>(url);
  }
}
