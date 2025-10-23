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

  getCompetition(id: number): Observable<Competition> {
    const url = `${this.baseUrl}/Competitions/${id}`;
    return this.httpClient.get<Competition>(url);
  }

  addCompetition(name: string, selectedscripts: Script[]): Observable<Competition> {

    const url = `${this.baseUrl}/Competitions`;
    const createCompetition: Competition = {
      id : 0,
      description: name,
      active: true,
      scripts: selectedscripts,
    };

    return this.httpClient.post<Competition>(url, createCompetition);
  }

  getCompetitionVoteResult(id: number): Observable<CompetitionVoteResult> {
    const url = `${this.baseUrl}/Competitions/${id}/voteresult`;
    return this.httpClient.get<CompetitionVoteResult>(url);
  }

  voteCompetition(competitionId: number, votes: Vote[]): Observable<Competition> {

    const url = `${this.baseUrl}/Competitions/${competitionId}/vote`;

    return this.httpClient.post<Competition>(url, votes);
  }

  getScripts(orderBy: string): Observable<Script[]> {
    let httpParams = new HttpParams();
    httpParams = httpParams.set('sort', orderBy);
    const url = `${this.baseUrl}/Scripts?${httpParams.toString()}`;
      
    return this.httpClient.get<Script[]>(url);
  }
}
