import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { Examination } from '../model';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-examination-overview',
  imports: [FormsModule, DatePipe],
  templateUrl: './examination-overview.component.html',
  styleUrl: './examination-overview.component.css'
})
export class ExaminationOverviewComponent {
  constructor(
    private router: Router,
    private dataService: DataService) {
  }

  examinations: Examination[] = [];
  showAllFilter: boolean = false;

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.dataService.getExaminations(!this.showAllFilter, 'ExaminationDate').subscribe({
      next: (data) => {
        console.log('data', data);
        this.examinations = data;

      },
      error: (error) => {
        console.log(error);
        alert('Load failed');
      }
    });
  }

  reloadExaminations() {
    this.load();
  }

  finding(examination: Examination) {
    this.router.navigate(['examination-findings', examination.id]);

  }
}
