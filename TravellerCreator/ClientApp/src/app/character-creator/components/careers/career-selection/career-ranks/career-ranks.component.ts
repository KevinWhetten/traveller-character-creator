import {Component, Input} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-ranks',
  templateUrl: './career-ranks.component.html',
  styleUrls: ['./career-ranks.component.scss']
})
export class CareerRanksComponent {
  @Input() career: Career;

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }
}
