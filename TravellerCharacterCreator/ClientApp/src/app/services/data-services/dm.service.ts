import { Injectable } from '@angular/core';
import {SkillService} from "./skill.service";

@Injectable({
  providedIn: 'root'
})
export class DmService {

  constructor(private _skillsService: SkillService) { }

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
}
