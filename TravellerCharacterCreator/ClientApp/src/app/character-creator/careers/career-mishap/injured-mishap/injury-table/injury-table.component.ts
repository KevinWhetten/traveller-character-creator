import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-injury-table',
  templateUrl: './injury-table.component.html',
  styleUrls: ['./injury-table.component.css']
})
export class InjuryTableComponent implements OnInit {
  @Input() rolls: number;

  constructor() { }

  ngOnInit(): void {
  }

}
