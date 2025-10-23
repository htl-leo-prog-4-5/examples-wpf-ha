import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { Examination } from '../app/model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl = 'http://localhost:5123';

  constructor(private httpClient: HttpClient) {
  }

  getExaminations(withFindingsOnly: boolean, orderBy: string): Observable<Examination[]> {
    let httpParams = new HttpParams();
    httpParams = httpParams.set('withFindingsOnly', withFindingsOnly);
    httpParams = httpParams.set('sort', orderBy);
    const url = `${this.baseUrl}/Examination?${httpParams.toString()}`;
    return this.httpClient.get<Examination[]>(url);
  }

  getExamination(id: number): Observable<Examination> {
    const url = `${this.baseUrl}/Examination/${id}`;
    return this.httpClient.get<Examination>(url);
  }

  updateExamination(examination: Examination): Observable<void> {
    const url = `${this.baseUrl}/examination/${examination.id}`;
    return this.httpClient.put<void>(url, examination);
  }
}
