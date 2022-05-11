import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-tutor',
  templateUrl: './event-tutor.component.html',
  styleUrls: ['./event-tutor.component.css']
})
export class EventTutorComponent implements OnInit {
  @Output() graduate = new EventEmitter();
  private log: string;
  tutorSkill: string;
  skillRoll: number;
  tutorBeaten: boolean;
  tutorWins: boolean;
  story: string;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  tutor() {
    this.log = 'A newly arrived tutor rubbed me the wrong way, and I worked hard to overturn their conclusions.';
    if (this.tutorSkill != '' && (this.skillRoll >= 2 && this.skillRoll <= 12)) {
      let skillScore = this._characterService.getSkills()[this.tutorSkill];
      if (this.skillRoll + skillScore >= 9) {
        this.log += ' I provided a truly elegant proof that soon became accepted as the standard approach!';
        this.tutorBeaten = true;
      } else {
        this.log += ' Nothing really came of it.';
        this.tutorWins = true;
      }
      this._loggingService.addLog(this.log);
    }
  }

  beatTutor() {
    this._characterService.increaseSkills([{Name: this.tutorSkill, Value: 1}]);
    this._characterService.addRival('My tutor who I 1-upped during my education.');
    if (this.story) {
      this._loggingService.addLog(this.story);
    }
    this.graduate.emit();
  }

  lostToTutor() {
    this.graduate.emit();
  }

}
