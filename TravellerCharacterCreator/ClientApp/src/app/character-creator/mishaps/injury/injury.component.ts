import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-injury',
  templateUrl: './injury.component.html',
  styleUrls: ['./injury.component.css']
})
export class InjuryComponent {
  @Input() rolls: number = 1;
  @Input() injury: number = 0;
  @Output() injure = new EventEmitter;

  constructor() { }

  getRange() {
    let range = [] as number[];
    for(let i = 0; i < this.rolls; i++){
      range.push(i + 1);
    }
    return range;
  }

  proceed() {
    this.injure.emit();
  }
}
