import { Routes } from '@angular/router';
import { ExaminationOverviewComponent } from './examination-overview/examination-overview.component';
import { ExaminationFindingsComponent } from './examination-findings/examination-findings.component';

export const routes: Routes = [
  { path: '', redirectTo: 'examination-overview', pathMatch: 'full' },
  { path: 'TODO', component: ExaminationOverviewComponent },

];
