import {Component, Input} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-benefits',
  templateUrl: './career-benefits.component.html',
  styleUrls: ['./career-benefits.component.scss']
})
export class CareerBenefitsComponent {
  @Input() career: Career;

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

  benefits() {
  }
}
