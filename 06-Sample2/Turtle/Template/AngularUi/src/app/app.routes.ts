import { Routes } from '@angular/router';
import { CompetitionOverviewComponent } from './competition-overview/competition-overview.component';
import { AddCompentitionComponent } from './add-competition/add-competition.component';
import { VoteCompetitionComponent } from './vote-competition/vote-competition.component';
import { CompetitionVoteResultComponent } from './competition-vote-result/competition-vote-result.component';

export const routes: Routes = [
  { path: '', redirectTo: 'competition-overview', pathMatch: 'full' },
  { path: 'competition-overview', component: CompetitionOverviewComponent },
];
