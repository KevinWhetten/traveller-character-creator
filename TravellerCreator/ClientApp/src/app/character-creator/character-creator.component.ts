import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {CharacterMetadataService} from "./services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-character-creator',
  templateUrl: './character-creator.component.html',
  styleUrls: ['./character-creator.component.scss']
})
export class CharacterCreatorComponent implements OnInit {

  constructor(private _router: Router,
              private _characterMetadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this._router.navigate([this._characterMetadataService.getCurrentUrl()]);
  }

}
