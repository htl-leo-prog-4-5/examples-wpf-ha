import { Routes } from '@angular/router';
import { TripOverview } from './trip-overview/trip-overview';

export const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'app-trip-overview' },
    { path: 'app-trip-overview', component: TripOverview },

];
