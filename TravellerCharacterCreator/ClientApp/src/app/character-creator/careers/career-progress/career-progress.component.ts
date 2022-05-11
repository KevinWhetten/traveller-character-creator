import {Component, Input, OnInit} from '@angular/core';
import {Career} from "../../../models/career";
import {CareerStep} from "../careers.component";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterService} from "../../../services/character.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../../services/data-services/rolling.service";

@Component({
  selector: 'app-career-progress',
  templateUrl: './career-progress.component.html',
  styleUrls: ['./career-progress.component.scss']
})
export class CareerProgressComponent implements OnInit {
  @Input() career: Career;
  @Input() careerStep: CareerStep;
  @Input() assignment: string;
  survivalModifier: number = -3;
  advancementModifier: number = -3;
  survivalRoll: number = 0;
  advancementRoll: number;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
  }

  survive() {
    let assignment = this.career.Assignments.find(x => x.Name == this.assignment);
    if (assignment != undefined) {
      if (this.survivalRoll + this.survivalModifier > assignment.Survival.target) {
        this.careerStep = CareerStep.event;
      } else {
        this.careerStep = CareerStep.mishap;
      }
    }
  }

  isSurvivalStep() {
    return this.careerStep == CareerStep.survival;
  }

  isAdvancementStep() {
    return false;
  }

  advance() {

  }
}
