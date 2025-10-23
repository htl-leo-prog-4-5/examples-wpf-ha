import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { Competition } from '../model';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-job-overview',
    imports: [FormsModule],
    templateUrl: './competition-overview.component.html',
    styleUrl: './competition-overview.component.css'
})
export class CompetitionOverviewComponent {
  constructor(
    private router: Router,
    private dataService: DataService) {
  }
}
