import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { ShippingCompanyDto } from 'src/app/models/shipping-company-dto.model';
import { CruiserService } from 'src/app/services/cruiser.service';

@Component({
  selector: 'app-edit-shipping-company',
  templateUrl: './edit-shipping-company.component.html',
  styleUrls: ['./edit-shipping-company.component.css'],
})
export class EditShippingCompanyComponent implements OnInit {

  id: number = 0;
  company: ShippingCompanyDto = {
    id: 0,
    name: ''
  };
  triedToSubmit = false;

  constructor(private route: ActivatedRoute, private cruiserService: CruiserService, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
      this.load();
    });
  }


  load(): void {
    this.cruiserService.getCompany(this.id).subscribe({
      next: data => {
        this.company = data;
      },
      error: error => {
        console.error('Error loading company:', error);
      }
    });
  }

  onSubmit(form: NgForm) {
    this.triedToSubmit = true;

    if (form.valid) {
      this.cruiserService.updateCompany(this.company).subscribe({
        next: () => {
          this.router.navigate(['/shipping-company', this.company.id]);
        },
        error: (error) => {
          alert('Shipping-Company could not be saved ' + error.message);
        }
      });
    }
  }
}
