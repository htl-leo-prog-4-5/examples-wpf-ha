import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTicketComponent } from './components/create-ticket/create-ticket.component';
import { NewTicketComponent } from './components/new-ticket/new-ticket.component';
import { ShowTicketComponent } from './components/show-ticket/show-ticket.component';
import { TicketResultComponent } from './components/ticket-result/ticket-result.component';

const routes: Routes = [
  {path: 'create-ticket', component: CreateTicketComponent},
  {path: 'new-ticket/:id', component: NewTicketComponent},
  {path: 'ticket-result', component: TicketResultComponent},
  {path: 'show-ticket/:ticketNo', component: ShowTicketComponent},
  {path: '', redirectTo: '/create-ticket', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
