import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-military-academy-event',
  templateUrl: './military-academy-event.component.html',
  styleUrls: ['./military-academy-event.component.css']
})
export class MilitaryAcademyEventComponent implements OnInit {

  constructor(private _router: Router,
              private _characterService: CharacterService) { }

  ngOnInit(): void {
  }

  graduate() {
    this._characterService.updateCurrentUrl('character-creator/education/military-academy/graduate');
    this._router.navigate(['character-creator/education/military-academy/graduate']);
  }

  lifeEvent() {
    this._characterService.updateCurrentUrl('character-creator/education/military-academy/life-event');
    this._router.navigate(['character-creator/education/military-academy/graduate']);
  }

  psionicTest() {
    this._characterService.updateCurrentUrl('character-creator/education/military-academy/psionic-test');
    this._router.navigate(['character-creator/education/military-academy/graduate']);
  }
}
