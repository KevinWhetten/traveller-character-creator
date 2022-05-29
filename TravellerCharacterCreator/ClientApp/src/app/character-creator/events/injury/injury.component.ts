import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-injury',
  templateUrl: './injury.component.html',
  styleUrls: ['./injury.component.css']
})
export class InjuryComponent implements OnInit {
  @Input() rolls: number = 1;
  @Input() injury: number = 0;
  @Output() complete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

  getRange() {
    let range = [] as number[];
    for(let i = 0; i < this.rolls; i++){
      range.push(i + 1);
    }
    return range;
  }

  proceed() {
    this.complete.emit();
  }
}
