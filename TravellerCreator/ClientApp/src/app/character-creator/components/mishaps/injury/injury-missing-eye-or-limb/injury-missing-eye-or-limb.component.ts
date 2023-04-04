import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-injury-missing-eye-or-limb',
  templateUrl: './injury-missing-eye-or-limb.component.html',
  styleUrls: ['./injury-missing-eye-or-limb.component.css']
})
export class InjuryMissingEyeOrLimbComponent {
  @Output() injured = new EventEmitter;
  characteristic: string;

  submit() {
    this.injured.emit();
  }
}
