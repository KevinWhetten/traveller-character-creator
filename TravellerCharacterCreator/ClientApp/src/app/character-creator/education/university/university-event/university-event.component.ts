import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-university-event',
  templateUrl: './university-event.component.html',
  styleUrls: ['./university-event.component.css']
})
export class UniversityEventComponent implements OnInit {

  constructor(private _characterMetadataService: CharacterMetadataService) { }

  ngOnInit(): void {
  }

  graduate() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/graduate');
  }

  lifeEvent() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/life-event');
  }

  psionicTest() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/psionic-test');
  }
}
