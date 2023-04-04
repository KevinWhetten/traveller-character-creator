import {Component} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {AlertType} from "../../../controls/alert/alert.component";

@Component({
  selector: 'app-basic-info',
  templateUrl: './basic-info.component.html',
  styleUrls: ['./basic-info.component.scss']
})
export class BasicInfoComponent {
  name: string;
  species: string;
  homeworld: string;
  hasError: boolean = false;
  error = AlertType.Error;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService) {
  }

  submit() {
    if (this.name && this.species && this.homeworld) {
      this._characterService.setName(this.name);
      this._characterService.setSpecies(this.species);
      this._characterService.setHomeworld(this.homeworld);
      this._metadataService.setCurrentUrl('character-creator/characteristics');
    } else {
      this.hasError = true;
    }
  }
}
