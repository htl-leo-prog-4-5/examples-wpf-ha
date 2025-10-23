import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OfficeDto } from '../models/office-dto.model';
import { GameDto } from '../models/game-dto.model';
import { CreateTicketDto } from '../models/create-ticket-dto.model';
import { TicketDto } from '../models/ticket-dto.model';
import { tick } from '@angular/core/testing';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
    //,'Authorization': 'my-auth-token'
  })
}
@Injectable({
  providedIn: 'root'
})
export class LottoService {

  private apiUrl = "http://localhost:5000";

  constructor(private http: HttpClient) { }

  getCurrentOpenGames() {
    return this.http.get<GameDto[]>(`${this.apiUrl}/game?state=open`);
  }

  getGame(gameId: number) {
    return this.http.get<GameDto>(`${this.apiUrl}/game/${gameId}`);
  }

  getOffices() {
    return this.http.get<OfficeDto[]>(`${this.apiUrl}/office`);
  }

  addTicket(ticket: CreateTicketDto): Observable<any> {
    const url = `${this.apiUrl}/CreateTicket`;
    return this.http.post<string>(url, ticket, { responseType: 'text' as 'json' });
  }

  getTickets(ticketNo: string) {
    return this.http.get<TicketDto[]>(`${this.apiUrl}/ticket?ticketNo=${ticketNo}`);
  }

}
