import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PercentageService {

  constructor() { }

  get2d6Percentage(target: number, dm: number) {
      switch(target - dm){
        case 2:
          return 100;
        case 3:
          return 97;
        case 4:
          return 92;
        case 5:
          return 83;
        case 6:
          return 72;
        case 7:
          return 58;
        case 8:
          return 42;
        case 9:
          return 28;
        case 10:
          return 17;
        case 11:
          return 8;
        case 12:
          return 3;
        default:
          return target < 2 ? 100 : 2;
      }
  }
}
