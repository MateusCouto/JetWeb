import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  isCollapsed = true;
  larguraImagem: number = 150;
  margemImagem: number = 2;
  constructor() { }

  ngOnInit(): void {
  }

}
