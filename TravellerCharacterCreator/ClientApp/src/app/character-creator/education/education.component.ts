import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../../services/character.service";

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent implements OnInit {

  constructor(private _router: Router,
              private _characterService: CharacterService) {
  }

  ngOnInit(): void {
  }

  toUniversity() {
    this._characterService.updateCurrentUrl('character-creator/education/university');
    this._characterService.addLog('Chose to apply for University...');
    this._characterService.startNewTerm();
    this._router.navigate(['character-creator/education/university']);
  }

  toMilitaryAcademy() {
    this._characterService.updateCurrentUrl('character-creator/education/military-academy');
    this._characterService.addLog('Chose to apply for Military Academy...');
    this._characterService.startNewTerm();
    this._router.navigate(['character-creator/education/military-academy']);
  }

  submit() {
    this._characterService.startNewTerm();
  }
}
