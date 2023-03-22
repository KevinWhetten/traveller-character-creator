import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PageService {
  private navDisabled = false;

  constructor() { }

  enableNav() {
    this.navDisabled = false;
  }

  disableNav(){
    this.navDisabled = true;
  }

  isCharacterSheetDisabled() {
    return this.navDisabled;
  }
}
