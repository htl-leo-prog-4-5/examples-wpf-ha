import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { Examination, ExaminationDataStream } from '../model';
import { ExaminationDataStreamComponent } from '../examination-data-stream/examination-data-stream.component';

@Component({
  selector: 'app-examination-overview',
  standalone: true,
  imports: [ExaminationDataStreamComponent, DatePipe, FormsModule],
  templateUrl: './examination-findings.component.html',
  styleUrl: './examination-findings.component.css'
})
export class ExaminationFindingsComponent {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService) {
  }
}

