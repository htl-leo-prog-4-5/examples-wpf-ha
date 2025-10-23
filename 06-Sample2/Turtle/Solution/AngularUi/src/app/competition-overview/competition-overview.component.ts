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

  competitions: Competition[] = [];
  inactiveFilter: boolean = false;

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.dataService.getCompetitions(!this.inactiveFilter, 'Name').subscribe({
      next: (data) => {
        console.log('data', data);
        this.competitions = data;

      },
      error: (error) => {
        console.log(error);
        alert('Load failed');
      }
    });
  }

  reloadCompetitions() {
    this.load();
  }

  voteCompetition(competition: Competition) {
    this.router.navigate(['/vote-competition', competition.id]);
  }

  voteResult(competition: Competition) {
    this.router.navigate(['/competition-vote-result', competition.id]);
  }

  addCompetition() {
    this.router.navigate(['/add-competition']);
  }
}
