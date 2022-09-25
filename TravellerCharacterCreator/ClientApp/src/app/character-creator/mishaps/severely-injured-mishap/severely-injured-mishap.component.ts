import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-severely-injured-mishap',
  templateUrl: './severely-injured-mishap.component.html',
  styleUrls: ['./severely-injured-mishap.component.css']
})
export class SeverelyInjuredMishapComponent {
  @Output() mishapComplete = new EventEmitter;

  constructor() { }

  submit() {
    this.mishapComplete.emit();
  }
}
