import {Component, Input, OnInit} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-benefits',
  templateUrl: './career-benefits.component.html',
  styleUrls: ['./career-benefits.component.scss']
})
export class CareerBenefitsComponent implements OnInit {
  @Input() career: Career;

  constructor() {
  }

  ngOnInit(): void {
  }

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

  benefits() {
  }
}
