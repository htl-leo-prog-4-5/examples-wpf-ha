import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StationsComponent } from './components/stations/stations.component';

const routes: Routes = [
  {path: 'stations', component: StationsComponent},
  {path: '', redirectTo: '/stations', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
