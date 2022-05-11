import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Career} from "../../../models/career";
import {CareerStep} from "../careers.component";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterService} from "../../../services/character.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";
import {RollingService} from "../../../services/data-services/rolling.service";

@Component({
  selector: 'app-career-description',
  templateUrl: './career-description.component.html',
  styleUrls: ['./career-description.component.scss']
})
export class CareerDescriptionComponent implements OnInit {
  @Input() career: Career;
  @Input() careerStep: CareerStep;
  @Input() isFirstTermInCareer: boolean;
  @Output() nextStep = new EventEmitter<CareerStep>();
  @Output() assignmentChosen = new EventEmitter<string>();
  chosenAssignment: string = '';
  qualificationModifier: number = 0;
  qualificationRoll: number = 0;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
    this.getQualificationModifier();
  }

  isQualifyStep() {
    return this.careerStep == CareerStep.qualify;
  }

  isAssignmentStep() {
    return this.careerStep == CareerStep.assignment;
  }

  qualify() {
    this.qualificationModifier = this.qualificationModifier - this._metadataService.getCareers().length;
    if (this.qualificationRoll + this.qualificationModifier > this.career.Qualification.target) {
      this.nextStep.emit(CareerStep.assignment);
    }
  }

  assignment() {
    if (this.isFirstTermInCareer) {
      this.assignmentChosen.emit(this.chosenAssignment);
      this.nextStep.emit(CareerStep.basicTraining);
    } else {
      this.assignmentChosen.emit(this.chosenAssignment);
      this.nextStep.emit(CareerStep.skillGeneration);
    }
  }

  private getQualificationModifier() {
    if (this.career.Qualification.characteristic.includes('STR')) {
      let strModifier = this._rollingService.getDm(this._characterService.getStrength());
      this.qualificationModifier = strModifier > this.qualificationModifier ? strModifier : this.qualificationModifier;
    }
    if (this.career.Qualification.characteristic.includes('DEX')) {
      let dexModifier = this._rollingService.getDm(this._characterService.getDexterity());
      this.qualificationModifier = dexModifier > this.qualificationModifier ? dexModifier : this.qualificationModifier;
    }
    if (this.career.Qualification.characteristic.includes('END')) {
      let endModifier = this._rollingService.getDm(this._characterService.getEndurance());
      this.qualificationModifier = endModifier > this.qualificationModifier ? endModifier : this.qualificationModifier;
    }
    if (this.career.Qualification.characteristic.includes('INT')) {
      let intModifier = this._rollingService.getDm(this._characterService.getIntellect());
      this.qualificationModifier = intModifier > this.qualificationModifier ? intModifier : this.qualificationModifier;
    }
    if (this.career.Qualification.characteristic.includes('EDU')) {
      let endModifier = this._rollingService.getDm(this._characterService.getEndurance());
      this.qualificationModifier = endModifier > this.qualificationModifier ? endModifier : this.qualificationModifier;
    }
    if (this.career.Qualification.characteristic.includes('SOC')) {
      let socModifier = this._rollingService.getDm(this._characterService.getSocialStatus());
      this.qualificationModifier = socModifier > this.qualificationModifier ? socModifier : this.qualificationModifier;
    }
  }
}
