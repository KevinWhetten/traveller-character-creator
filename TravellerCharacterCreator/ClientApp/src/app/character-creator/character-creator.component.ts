import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {CharacterService} from "../services/character.service";

@Component({
  selector: 'app-character-creator',
  templateUrl: './character-creator.component.html',
  styleUrls: ['./character-creator.component.css']
})
export class CharacterCreatorComponent implements OnInit {

  constructor(private _router: Router,
              private _characterService: CharacterService) { }

  ngOnInit(): void {
    this._router.navigate([this._characterService.getCurrentUrl()]);
  }

}
