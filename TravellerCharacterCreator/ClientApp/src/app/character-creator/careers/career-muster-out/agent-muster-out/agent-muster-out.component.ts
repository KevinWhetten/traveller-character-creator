import { Component, OnInit } from '@angular/core';
import {Career} from "../../../../models/career";
import {CareerService} from "../../../../services/data-services/career.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-agent-muster-out',
  templateUrl: './agent-muster-out.component.html',
  styleUrls: ['./agent-muster-out.component.css']
})
export class AgentMusterOutComponent implements OnInit {
  career: Career;
  rollsMade: number = 0;
  benefitsObtained: string[] = [];
  benefitRoll: number;
  benefitType: string;
  option6: boolean = false;
  option6Choice: string;

  constructor(private _careerService: CareerService,
              private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService) {
  }

  ngOnInit(): void {
    let careerName = this._metadataService.getCurrentCareer();
    this.career = this._careerService.getCareer(careerName);
  }

  getBenefitNum() {
    let terms = this._metadataService.getCurrentCareerTerms();
    let rank = this._metadataService.getRank();
    if (rank >= 5) {
      return terms + 3 - this.rollsMade;
    }
    if (rank >= 3) {
      return terms + 2 - this.rollsMade;
    }
    if (rank >= 1) {
      return terms + 1 - this.rollsMade;
    }
    return terms;
  }

  getCashBenefitNum() {
    return this._metadataService.getCashRolls();
  }

  submitRoll() {
    if(this.benefitType == 'cash'){
      this.rollsMade++;
      this.makeCashBenefit();
    } else if (this.benefitType == 'benefit'){
      this.rollsMade++;
      this.makeBenefitRoll();
    }
  }

  private makeCashBenefit() {
    this._characterService.addCash(this.career.BenefitTable[this.benefitRoll].Cash);
  }

  private makeBenefitRoll() {
    switch(this.benefitRoll){
      case 1:
        this.benefitsObtained.push('Armour');
        this._characterService.addArmour();
        break;
      case 2:
        this.benefitsObtained.push('INT +1');
        this._characterService.increaseIntellect(1);
        break;
      case 3:
        this.benefitsObtained.push('EDU +1');
        this._characterService.increaseEducation(1);
        break;
      case 4:
        this.benefitsObtained.push('Weapon');
        this._characterService.addWeapon();
        break;
      case 5:
        this.benefitsObtained.push('TAS Membership');
        this._characterService.addTASMembership();
        break;
      case 6:
        this.option6 = true;
        break;
      case 7:
        this.benefitsObtained.push('SOC +2');
        this._characterService.increaseSocialStatus(2);
        break;
    }
  }
  resolveOption6() {
    if(this.option6Choice == 'armour'){
      this.benefitsObtained.push('Armour');
      this._characterService.addArmour();
    } else {
      this.benefitsObtained.push('END +1');
      this._characterService.increaseEndurance(1);
    }
    this.option6 = false;
  }

  proceed() {
    this._metadataService.startNewTerm();
    this._metadataService.setCurrentUrl('character-creator/careers')
  }
}
