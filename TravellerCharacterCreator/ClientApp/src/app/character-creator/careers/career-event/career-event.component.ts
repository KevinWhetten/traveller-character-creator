import { Component, OnInit } from '@angular/core';
import {Career} from "../../../models/career";
import {CareerService} from "../../../services/data-services/career.service";
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-career-event',
  templateUrl: './career-event.component.html',
  styleUrls: ['./career-event.component.css']
})
export class CareerEventComponent implements OnInit {
  career: Career;
  eventRoll: number;

  constructor(private _careerService: CareerService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    let careerName = this._metadataService.getCurrentCareer();
    this.career = this._careerService.getCareer(careerName);
  }

  submit() {
    this._metadataService.setEventNumber(this.eventRoll);
    this._metadataService.setCurrentUrl(`character-creator/careers/${this.career.Name.toLowerCase()}/event`)
  }
}
