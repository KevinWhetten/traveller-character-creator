import {Component, OnInit, ViewChild} from '@angular/core';
import {CareerService} from "../../services/data-services/career.service";
import {CharacterService} from "../../services/character.service";
import {RollingService} from "../../services/data-services/rolling.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {CareerBenefitsComponent} from "./career-benefits/career-benefits.component";
import {CareerDescriptionComponent} from "./career-description/career-description.component";
import {CareerEventsComponent} from "./career-events/career-events.component";

export enum CareerStep {
  qualify,
  assignment,
  basicTraining,
  skillGeneration,
  survival,
  event,
  mishap,
  commission,
  advancement,
  leaving,
  benefits
}

@Component({
  selector: 'app-careers',
  templateUrl: './careers.component.html',
  styleUrls: ['./careers.component.scss']
})
export class CareersComponent implements OnInit {
  @ViewChild(CareerBenefitsComponent) benefitsComponent: CareerBenefitsComponent;
  @ViewChild(CareerDescriptionComponent) descriptionComponent: CareerDescriptionComponent;
  @ViewChild(CareerEventsComponent) eventsComponent: CareerEventsComponent;
  careerStep = CareerStep.qualify;
  career = this._careerService.getCareer('Agent');
  careerName: string = "Agent";
  assignment: string;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService) {
  }

  ngOnInit(): void {
  }

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

  isFirstTermInCareer() {
    return !this._metadataService.getCareers().includes(this.career.Name);
  }

  getCareerNames() {
    return this._careerService.careerNames;
  }

  changeCareer() {
    this.career = this._careerService.getCareer(this.careerName);
  }

  isQualifyStep() {
    return this.careerStep == CareerStep.qualify;
  }
}
