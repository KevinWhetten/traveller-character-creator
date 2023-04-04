import { Component, OnInit } from '@angular/core';
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-career-leaving',
  templateUrl: './career-leaving.component.html',
  styleUrls: ['./career-leaving.component.css']
})
export class CareerLeavingComponent implements OnInit {
  career: string;

  constructor(private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this.career = this._metadataService.getCurrentCareer();
  }

  yes() {
    this._metadataService.setCurrentUrl('character-creator/careers/skill-generation');
  }

  no() {
    this._metadataService.setCurrentUrl('character-creator/careers/benefits')
  }
}
