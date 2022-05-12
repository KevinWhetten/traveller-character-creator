import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-mishaps',
  templateUrl: './career-mishaps.component.html',
  styleUrls: ['./career-mishaps.component.scss']
})
export class CareerMishapsComponent implements OnInit {
  @Input() career: Career;

  constructor() {
  }

  ngOnInit(): void {
  }

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }
}
