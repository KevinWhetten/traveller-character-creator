import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-military-academy-event',
  templateUrl: './military-academy-event.component.html',
  styleUrls: ['./military-academy-event.component.css']
})
export class MilitaryAcademyEventComponent implements OnInit {

  constructor(private _characterMetadataService: CharacterMetadataService) { }

  ngOnInit(): void {
  }

  graduate() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/military-academy/graduate');
  }

  lifeEvent() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/military-academy/life-event');
  }

  psionicTest() {
    this._characterMetadataService.setCurrentUrl('character-creator/education/military-academy/psionic-test');
  }
}
