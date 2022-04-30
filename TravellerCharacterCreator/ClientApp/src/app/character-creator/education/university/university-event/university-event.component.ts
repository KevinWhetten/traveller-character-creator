import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-university-event',
  templateUrl: './university-event.component.html',
  styleUrls: ['./university-event.component.css']
})
export class UniversityEventComponent implements OnInit {

  constructor(private _router: Router,
              private _characterService: CharacterService) { }

  ngOnInit(): void {
  }

  graduate() {
    this._characterService.updateCurrentUrl('character-creator/education/university/graduate');
    this._router.navigate(['character-creator/education/university/graduate']);
  }

  lifeEvent() {
    this._characterService.updateCurrentUrl('character-creator/education/university/life-event');
    this._router.navigate(['character-creator/education/university/graduate']);
  }

  psionicTest() {
    this._characterService.updateCurrentUrl('character-creator/education/university/psionic-test');
    this._router.navigate(['character-creator/education/university/graduate']);
  }
}
