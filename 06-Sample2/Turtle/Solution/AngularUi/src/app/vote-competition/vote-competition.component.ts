import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { Competition, Script, Vote } from '../model';

@Component({
    selector: 'app-vote-competition',
    imports: [],
    templateUrl: './vote-competition.component.html',
    styleUrl: './vote-competition.component.css'
})
export class VoteCompetitionComponent {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService) {
  }

  competition: Competition | undefined;
  competitionId: number = 0;
  votes: Vote[] = [];

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.competitionId = +params['id'];
      this.load();
    });
  }

  load(): void {

    this.dataService.getCompetition(this.competitionId).subscribe({
      next: (data) => {
        this.competition = data;
        this.votes = new Array<Vote>(this.competition!.scripts.length);
        for (let i = 0; i < this.votes.length; i++) {
          this.votes[i] = { scriptId: this.competition!.scripts[i].id, rate: 0 };
        }
        console.log('data', data);
      },
      error: (error) => {
        console.log(error);
        alert('Load failed');
      }
    });
  }

  onVote(script: Script, idx: number, rate: number) {

    this.votes[idx].rate = rate;
  }

  allVote(): boolean {
    return this.votes.every(v => v.rate > 0);
  }

  submitVotes() {
    this.dataService.voteCompetition(this.competitionId, this.votes ).subscribe({
      next: (data) => {
        this.router.navigate(['/competition-overview']);
        console.log('data', data);
      },
      error: (error) => {
        console.log(error);
        alert('Error saving votes');
      }
    });
  }

  back() {
    this.router.navigate(['/competition-overview']);
  }
}
