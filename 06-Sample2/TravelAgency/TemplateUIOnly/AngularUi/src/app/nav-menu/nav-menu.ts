import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  imports: [],
  templateUrl: './nav-menu.html',
  styleUrl: './nav-menu.css'
})
export class NavMenu {

  constructor(private router: Router) { }

}
