import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Career} from "../../../models/career";
import {CareerStep} from "../careers.component";

@Component({
  selector: 'app-career-skills',
  templateUrl: './career-skills.component.html',
  styleUrls: ['./career-skills.component.scss']
})
export class CareerSkillsComponent implements OnInit {
  @Input() career: Career;
  @Input() careerStep: CareerStep;
  @Input() isFirstTermInCareer: boolean;
  @Output() nextStep = new EventEmitter<CareerStep>();
  trainingTable: number;
  basicTrainingSkill: string;

  constructor() {
  }

  ngOnInit(): void {
  }

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

  isBasicTrainingStep() {
    return this.careerStep == CareerStep.basicTraining;
  }

  basicTraining() {
    this.nextStep.emit(CareerStep.survival);
  }
}
