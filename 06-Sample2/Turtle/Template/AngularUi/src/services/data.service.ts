import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { Competition, Vote, Script, CompetitionVoteResult } from '../app/model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl = 'http://localhost:5123';

  constructor(private httpClient: HttpClient) {

  }

  getCompetitions(activeOnly: boolean, orderBy: string): Observable<Competition[]> {
    let httpParams = new HttpParams();
    httpParams = httpParams.set('activeOnly', activeOnly);
    httpParams = httpParams.set('sort', orderBy);
    const url = `${this.baseUrl}/Competitions?${httpParams.toString()}`;
    return this.httpClient.get<Competition[]>(url);
  }
}
