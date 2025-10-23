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

}
