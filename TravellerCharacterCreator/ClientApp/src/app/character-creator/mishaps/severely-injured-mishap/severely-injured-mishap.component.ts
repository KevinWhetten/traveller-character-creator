import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-severely-injured-mishap',
  templateUrl: './severely-injured-mishap.component.html',
  styleUrls: ['./severely-injured-mishap.component.css']
})
export class SeverelyInjuredMishapComponent implements OnInit {
  @Output() mishapComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

  submit() {
    this.mishapComplete.emit();
  }
}
