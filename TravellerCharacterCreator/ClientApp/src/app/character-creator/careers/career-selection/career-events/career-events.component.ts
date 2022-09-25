import {Component, Input} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-events',
  templateUrl: './career-events.component.html',
  styleUrls: ['./career-events.component.scss']
})
export class CareerEventsComponent {
  @Input() career: Career;

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

}
