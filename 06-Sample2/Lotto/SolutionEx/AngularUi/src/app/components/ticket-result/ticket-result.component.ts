import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { LottoService } from 'src/app/services/lotto.service';

@Component({
  selector: 'app-ticket-result',
  templateUrl: './ticket-result.component.html',
  styleUrls: ['./ticket-result.component.css']
})
export class TicketResultComponent {

  ticketNo: string | null = null;
  haveTicketResult = false;

  constructor(private lottoService: LottoService, private router: Router, private activatedRoute: ActivatedRoute) { }  


  getTicketResult() {

    if (this.ticketNo != null) {
      /*
      this.locationService.getLottoResult(this.locationId, this.fromDate, this.toDate).subscribe(
        (data) => {
          this.averageWeatherData = data;
        },
        (error) => {
          console.error('Error loading lotto result:', error);
        }
      );
      */
    }
  }
}
