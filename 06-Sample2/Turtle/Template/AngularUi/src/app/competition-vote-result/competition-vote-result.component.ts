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
}
