import {Component, OnInit} from '@angular/core';
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-career-muster-out',
  templateUrl: './career-muster-out.component.html',
  styleUrls: ['./career-muster-out.component.scss']
})
export class CareerMusterOutComponent implements OnInit {
  careerName: string;

  constructor(private _metadataService: CharacterMetadataService) {
  }

  ngOnInit(): void {
    this.careerName = this._metadataService.getCurrentCareer();

  }

}
