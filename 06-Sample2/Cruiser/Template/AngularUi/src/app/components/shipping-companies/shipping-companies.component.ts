import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CompanyOverview } from 'src/app/models/company-overview.model';
import { CruiserService } from 'src/app/services/cruiser.service';

@Component({
  selector: 'app-shipping-companies',
  templateUrl: './shipping-companies.component.html',
  styleUrls: ['./shipping-companies.component.css'],
})
export class ShippingCompaniesComponent implements OnInit {
  companies: CompanyOverview[] = [];

  constructor(private cruiserService: CruiserService, private router: Router) {}

  ngOnInit(): void {
    //TODO: load data from cruiserService and store it in companies: CompanyOverview[] = [];
  }
}
