import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-injury-lightly-injured',
  templateUrl: './injury-lightly-injured.component.html',
  styleUrls: ['./injury-lightly-injured.component.css']
})
export class InjuryLightlyInjuredComponent {
  @Output() injured = new EventEmitter;

  submit() {
    this.injured.emit();
  }
}
