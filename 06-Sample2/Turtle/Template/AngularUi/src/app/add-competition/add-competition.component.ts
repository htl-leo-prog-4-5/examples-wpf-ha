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

  ngOnInit(): void {
  }
}
