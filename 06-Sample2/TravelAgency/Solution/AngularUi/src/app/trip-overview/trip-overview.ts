import { Component } from '@angular/core';
import { DataService } from '../../services/data.service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { Trip } from '../model';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-trip-overview',
  imports: [FormsModule, DatePipe],
  templateUrl: './trip-overview.html',
  styleUrl: './trip-overview.css'
})
export class TripOverview {

  constructor(
    private router: Router,
    private dataService: DataService) {
  }

  trips: Trip[] = [];
  showAllFilter: boolean = false;


  ngOnInit(): void {
    this.load();
  }

  load() {
    this.dataService.getTrips('Id').subscribe({
      next: (data) => {
        this.trips = data;
        console.log('data', data);

      },
      error: (error) => {
        console.log(error);
        alert('Load trips failed');
      }
    });
  }

  reloadTrips() {
    this.load();
  }

  getSteps(trip: Trip) {
    
      return trip.route.steps.map(s => s.no + ":" + s.description ).join(' => ');
    }
}
