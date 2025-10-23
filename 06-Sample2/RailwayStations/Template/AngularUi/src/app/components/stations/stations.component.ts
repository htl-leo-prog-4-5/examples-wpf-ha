import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { StationOverview } from 'src/app/models/station-overview.model';
import { RailwayService } from 'src/app/services/railway.service';

@Component({
  selector: 'app-stations',
  templateUrl: './stations.component.html',
  styleUrls: ['./stations.component.css'],
})
export class StationsComponent implements OnInit {
  stations: StationOverview[] = [];

  constructor(private railwayService: RailwayService) {}

  ngOnInit(): void {
    //TODO: load data from railwayService and store it in stations: StationOverview[] = [];
  }
}
