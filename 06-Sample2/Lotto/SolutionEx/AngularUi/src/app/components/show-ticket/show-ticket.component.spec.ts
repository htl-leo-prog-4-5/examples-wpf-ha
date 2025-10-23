import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowTicketComponent } from './show-ticket.component';

describe('LocationListComponent', () => {
  let component: ShowTicketComponent;
  let fixture: ComponentFixture<ShowTicketComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShowTicketComponent]
    });
    fixture = TestBed.createComponent(ShowTicketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
