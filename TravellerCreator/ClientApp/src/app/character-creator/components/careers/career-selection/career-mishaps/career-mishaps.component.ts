import {Component, Input} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-mishaps',
  templateUrl: './career-mishaps.component.html',
  styleUrls: ['./career-mishaps.component.scss']
})
export class CareerMishapsComponent {
  @Input() career: Career;

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }
}
