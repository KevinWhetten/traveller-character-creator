import { Component} from '@angular/core';
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-university-event',
  templateUrl: './university-event.component.html',
  styleUrls: ['./university-event.component.css']
})
export class UniversityEventComponent {

  constructor(private _characterMetadataService: CharacterMetadataService) { }

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
