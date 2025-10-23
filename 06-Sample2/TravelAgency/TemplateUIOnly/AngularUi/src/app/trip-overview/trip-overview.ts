import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-trip-overview',
  imports: [FormsModule, DatePipe],
  templateUrl: './trip-overview.html',
  styleUrl: './trip-overview.css'
})
export class TripOverview {

  constructor() {
  }
}
