import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-injury-lightly-injured',
  templateUrl: './injury-lightly-injured.component.html',
  styleUrls: ['./injury-lightly-injured.component.css']
})
export class InjuryLightlyInjuredComponent implements OnInit {
  @Output() injured = new EventEmitter;

  constructor() {
  }

  ngOnInit(): void {
  }

  submit() {
    this.injured.emit();
  }
}
