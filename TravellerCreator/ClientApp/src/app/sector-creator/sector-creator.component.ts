import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {WorldBuilderSector} from "./models/world-builder/world-builder-sector";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-sector-creator',
  templateUrl: './sector-creator.component.html',
  styleUrls: ['./sector-creator.component.scss']
})
export class SectorCreatorComponent {

  constructor(private _router: Router, private _httpClient: HttpClient) {  }

  generateWorldBuilderSector() {
    this._router.navigate(['sector-creator/world-builder']);
  }
}
