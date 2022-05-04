import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {Router} from "@angular/router";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-basic-info',
  templateUrl: './basic-info.component.html',
  styleUrls: ['./basic-info.component.css']
})
export class BasicInfoComponent implements OnInit {
  name: string;
  species: string;
  homeworld: string;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService) {
  }

  ngOnInit(): void {
  }

  submit() {
    if (this.name && this.species && this.homeworld) {
      this._characterService.setName(this.name);
      this._characterService.setSpecies(this.species);
      this._characterService.setHomeworld(this.homeworld);
      this._metadataService.setCurrentUrl('character-creator/characteristics');
    }
  }
}
