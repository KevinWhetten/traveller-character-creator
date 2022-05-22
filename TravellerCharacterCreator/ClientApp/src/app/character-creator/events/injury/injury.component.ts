import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-injury',
  templateUrl: './injury.component.html',
  styleUrls: ['./injury.component.css']
})
export class InjuryComponent implements OnInit {
  @Input() rolls: number = 1;
  @Output() complete = new EventEmitter;
  injury: number;

  constructor() { }

  ngOnInit(): void {
  }

  proceed() {
    this.complete.emit();
  }
}
