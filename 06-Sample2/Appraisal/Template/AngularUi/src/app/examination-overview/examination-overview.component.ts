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
}
