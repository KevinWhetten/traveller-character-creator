import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-injury-missing-eye-or-limb',
  templateUrl: './injury-missing-eye-or-limb.component.html',
  styleUrls: ['./injury-missing-eye-or-limb.component.css']
})
export class InjuryMissingEyeOrLimbComponent implements OnInit {
  @Output() injured = new EventEmitter;
  characteristic: string;

  constructor() { }

  ngOnInit(): void {
  }

  submit() {
    this.injured.emit();
  }
}
