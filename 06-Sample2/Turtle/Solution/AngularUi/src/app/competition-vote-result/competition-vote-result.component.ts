import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { CompetitionVoteResult, Vote } from '../model';

@Component({
  selector: 'app-competition-vote-result',
  standalone: true,
  templateUrl: './competition-vote-result.component.html',
  styleUrl: './competition-vote-result.component.css'
})
export class CompetitionVoteResultComponent {

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService) {
  }

  competitionVoteResult: CompetitionVoteResult | undefined;
  competitionId: number = 0;
  votes: Vote[] = [];

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.competitionId = +params['id'];
      this.load();
    });
  }

  load(): void {

    this.dataService.getCompetitionVoteResult(this.competitionId).subscribe({
      next: (data) => {
        this.competitionVoteResult = data;
        console.log('data', data);
      },
      error: (error) => {
        console.log(error);
        alert('Load failed');
      }
    });
  }
}
