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

  examination: Examination | null = null;
  examinationId: number = 0;

  medicalFindingsVal: string = '';

  loading = false;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.examinationId = +params['id'];
      this.load(this.examinationId);
    });
  }

  load(id: number): void {
    this.loading = true;
    this.dataService.getExamination(id).subscribe({
      next: (data) => {
        this.examination = data!;
        this.medicalFindingsVal = data!.medicalFindings;
        this.loading = false;
      },
      error: (error) => {
        console.log(error);
        alert('Load examination details failed');
      }
    });
  }
  onUpdateMedicalFindings(): void {

    this.examination!.medicalFindings = this.medicalFindingsVal;
    this.dataService.updateExamination(this.examination!)
      .subscribe({
        next: (data) => {
          this.router.navigate(['/examination-overview']);
        },
        error: (error) => {
          console.log(error);
          alert('Error saving examination');
        }
      });
  }
}

