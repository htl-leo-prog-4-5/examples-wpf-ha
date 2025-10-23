import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { DataService } from '../../services/data.service';
import { Script } from '../model';

@Component({
    selector: 'app-add-competition',
    imports: [FormsModule],
    templateUrl: './add-competition.component.html',
    styleUrl: './add-competition.component.css'
})
export class AddCompentitionComponent implements OnInit {
  constructor(
    private router: Router,
    private dataService: DataService) {
  }

  scripts: Script[] = [];
  selectedScripts: Script[] = [];

  competitionNameVal: string = 'Compentition';
  
  ngOnInit(): void {
    this.dataService.getScripts("name").subscribe({
      next: (data) => {
        this.scripts = data;

      },
      error: (error) => {
        console.log(error);
        alert('Load failed');
      }
    });
  }

  onAddToCompetition(script: Script) {
    this.selectedScripts.push(script);
    this.scripts = this.scripts.filter(item => item != script)
  }

  onRemoveFromCompetition(script: Script) {
    this.scripts.push(script);
    this.selectedScripts = this.selectedScripts.filter(item => item != script)
  }

  addCompetition() {

    this.dataService.addCompetition(this.competitionNameVal, this.selectedScripts).subscribe({
      next: (data) => {
        this.router.navigate(['/competition-overview']);
      },
      error: (error) => {
        //console.log(error.error);
        alert(`Create failed: ${error.error}`);
      }
    });
  }
}
