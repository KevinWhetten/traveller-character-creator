import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MilitaryAcademyService {
  private academy: string = '';

  constructor() { }

  getAcademy() {
    return this.academy;
  }
}
