import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { OfficeDto } from 'src/app/models/office-dto.model';

import { CreateTicketDto } from 'src/app/models/create-ticket-dto.model';
import { LottoService } from 'src/app/services/lotto.service';

@Component({
  selector: 'app-new-ticket',
  templateUrl: './new-ticket.component.html',
  styleUrls: ['./new-ticket.component.css']
})
export class NewTicketComponent {
  triedToSubmit = false;
  newLocation: CreateTicketDto = {
    officeNo: '',
    gameFromDate: new Date(),
    gameToDate: new Date(),
    tips: [[0, 0, 0, 0, 0, 0]]
  };

  offices: OfficeDto[] = [];

  gameId: number = 0;

  constructor(private route: ActivatedRoute, private lottoService: LottoService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.gameId = +params['id'];
      this.load();
    });
  }

  load() {

    this.lottoService.getOffices().subscribe({
      next: data => {
        this.offices = data;
      },
      error: error => {
        console.error('Error loading offices:', error.message);
      }
    });

    this.lottoService.getGame(this.gameId).subscribe({
      next: data => {
        this.newLocation.gameFromDate = data.dateFrom;
        this.newLocation.gameToDate = data.dateTo;
      },
      error: error => {
        console.error('Error loading offices:', error.message);
      }
    });

  }

  onSubmit(form: NgForm) {
    this.triedToSubmit = true;

    if (form.valid) {
            this.lottoService.addTicket(this.newLocation).subscribe({
              next: (data) => {
                this.router.navigate(['/show-tickte',data]);
              },
              error: (error) => {
                alert('New ticket could not be saved ' + error.message);
              }
            });
    }
  }

  btnBackClicked() {
    // Adjust the navigation logic based on your application's requirements
    this.router.navigate(['/locations']);
  }

}
