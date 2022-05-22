import { Component } from '@angular/core';
import {PageService} from "../services/page.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private _pageService: PageService) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  characterSheetEnabled() {
    return !this._pageService.isCharacterSheetDisabled();
  }
}
