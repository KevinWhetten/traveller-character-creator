import {Component, Input, OnInit} from '@angular/core';
import {Career} from "../../../models/career";

@Component({
  selector: 'app-career-ranks',
  templateUrl: './career-ranks.component.html',
  styleUrls: ['./career-ranks.component.scss']
})
export class CareerRanksComponent implements OnInit {
  @Input() career: Career;

  constructor() {
  }

  ngOnInit(): void {
  }

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

}
