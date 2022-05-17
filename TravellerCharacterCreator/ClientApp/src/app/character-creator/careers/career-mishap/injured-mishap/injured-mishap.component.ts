import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-injured-mishap',
  templateUrl: './injured-mishap.component.html',
  styleUrls: ['./injured-mishap.component.css']
})
export class InjuredMishapComponent implements OnInit {
  @Output() mishapComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

}
