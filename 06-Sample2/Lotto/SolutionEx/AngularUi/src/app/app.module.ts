import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateTicketComponent } from './components/create-ticket/create-ticket.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NewTicketComponent } from './components/new-ticket/new-ticket.component';
import { ShowTicketComponent } from './components/show-ticket/show-ticket.component';
import { TicketResultComponent } from './components/ticket-result/ticket-result.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateTicketComponent,
    NewTicketComponent,
    ShowTicketComponent,
    TicketResultComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
