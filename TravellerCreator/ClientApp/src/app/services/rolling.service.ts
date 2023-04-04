import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RollingService {

  constructor() { }

  getDm(score: number) {
    switch (score) {
      case 0:
        return -3;
      case 1:
      case 2:
        return -2;
      case 3:
      case 4:
      case 5:
        return -1;
      case 6:
      case 7:
      case 8:
        return 0;
      case 9:
      case 10:
      case 11:
        return 1;
      case 12:
      case 13:
      case 14:
        return 2;
      case 15:
        return 3;
      default:
        return 0;
    }
  }

  roll(numberOfDice: number) {
    let sum = 0;
    for (let i = 0; i < numberOfDice; i++){
      sum += Math.ceil(Math.random() * 6);
    }
    return sum;
  }

  rollBenford() {
    let roll = this.roll(2);

    switch (roll) {
      case 2:
        return 1;
      case 3:
        return 7;
      case 4:
        return 5;
      case 5:
        return 3;
      case 6:
        return 1;
      case 7:
        return 2;
      case 8:
        return 1;
      case 9:
        return 4;
      case 10:
        return 6;
      case 11:
        return 8;
      case 12:
        return 9;
      default:
        return 0;
    }
  }
}
