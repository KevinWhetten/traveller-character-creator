import { Component, OnInit } from '@angular/core';
import {Career} from "../../../../models/career";
import {CareerService} from "../../../../services/data-services/career.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-muster-out',
  templateUrl: './agent-muster-out.component.html',
  styleUrls: ['./agent-muster-out.component.css']
})
export class AgentMusterOutComponent implements OnInit {
  career: Career;
  rollsMade: number = 0;

  constructor(private _careerService: CareerService,
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

  submit() {

  }
}
