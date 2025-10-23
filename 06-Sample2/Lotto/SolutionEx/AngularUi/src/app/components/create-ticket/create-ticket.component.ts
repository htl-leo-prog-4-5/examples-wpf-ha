import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { GameDto } from 'src/app/models/game-dto.model';
import { LottoService } from 'src/app/services/lotto.service';

@Component({
  selector: 'app-create-ticket',
  templateUrl: './create-ticket.component.html',
  styleUrls: ['./create-ticket.component.css'],
})
export class CreateTicketComponent implements OnInit {
  games: GameDto[] = [];

  constructor(private lottoService: LottoService, private router: Router) {}

  ngOnInit(): void {
    this.lottoService.getCurrentOpenGames().subscribe({
      next: data=>{
        this.games = data;
      },
      error: error=>{
        console.error('Error loading open games:', error);
      }
    });
  }

  newTicket(game: GameDto): void {
    this.router.navigate(['/new-ticket',game.id]);
  }
}
