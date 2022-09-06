import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-injured-mishap',
  templateUrl: './injured-mishap.component.html',
  styleUrls: ['./injured-mishap.component.css']
})
export class InjuredMishapComponent {
  @Output() mishapComplete = new EventEmitter;
}
