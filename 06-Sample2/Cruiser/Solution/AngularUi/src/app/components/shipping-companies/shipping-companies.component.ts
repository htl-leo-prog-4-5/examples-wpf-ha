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

  constructor(private router: Router,private cruiserService: CruiserService) {}

  ngOnInit(): void {
    this.cruiserService.getCompanies().subscribe({
      next: data=>{
        this.companies = data;
      },
      error: error=>{
        console.error('Error loading companies:', error);
      }
    });
  }

  showDetail(company: CompanyOverview): void {
    this.router.navigate(['/shipping-company',company.id]);
  }
}
