import {Component} from '@angular/core';
import {WorldBuilderSector} from "../models/world-builder/world-builder-sector";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-world-builder',
  templateUrl: './world-builder.component.html',
  styleUrls: ['./world-builder.component.css']
})
export class WorldBuilderComponent {
  worldBuilderSector: WorldBuilderSector;

  constructor(private _httpClient: HttpClient, private _router: Router) {
  }

  ngOnInit(): void {
    this._httpClient.get<WorldBuilderSector>('http://localhost:5000/CreateSector/WorldBuilderSector').subscribe((x) => {
      this._router.navigate(['sector-creator/world-builder-sector', x]);
    });
  }

}
