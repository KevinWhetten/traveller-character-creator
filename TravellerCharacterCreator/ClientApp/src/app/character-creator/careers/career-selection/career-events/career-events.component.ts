import {Component, Input, OnInit} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-events',
  templateUrl: './career-events.component.html',
  styleUrls: ['./career-events.component.scss']
})
export class CareerEventsComponent implements OnInit {
  @Input() career: Career;

  constructor() {
  }

  ngOnInit(): void {
  }

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

}
