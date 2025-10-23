import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { TicketDto } from 'src/app/models/ticket-dto.model';
import { LottoService } from 'src/app/services/lotto.service';

@Component({
  selector: 'app-show-ticket',
  templateUrl: './show-ticket.component.html',
  styleUrls: ['./show-ticket.component.css'],
})
export class ShowTicketComponent implements OnInit {

  ticketNo: string = '';
  ticket: TicketDto = {} as TicketDto;

  constructor(private route: ActivatedRoute, private lottoService: LottoService, private router: Router) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.ticketNo = params['ticketNo'];
      this.load();
    });
  }


  load(): void {
    this.lottoService.getTickets(this.ticketNo).subscribe({
      next: data=>{
        this.ticket = data[0];
      },
      error: error=>{
        console.error('Error loading open ticket:', error);
      }
    });
  }
}
