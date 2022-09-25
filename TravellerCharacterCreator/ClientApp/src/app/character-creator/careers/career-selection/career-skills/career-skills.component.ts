import {Component, Input} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-skills',
  templateUrl: './career-skills.component.html',
  styleUrls: ['./career-skills.component.scss']
})
export class CareerSkillsComponent {
  @Input() career: Career;

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }
}
