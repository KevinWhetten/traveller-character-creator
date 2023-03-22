import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {SkillService} from "../../../../services/data-services/skill.service";

@Component({
  selector: 'app-event-tutor',
  templateUrl: './event-tutor.component.html',
  styleUrls: ['./event-tutor.component.scss']
})
export class EventTutorComponent implements OnInit {
  @Output() graduate = new EventEmitter();
  private log: string;
  tutorSkill: string;
  tutorBeaten: boolean;
  tutorWins: boolean;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    if (this._loggingService.getLastLog() != 'A newly arrived tutor rubbed me the wrong way, and I worked hard to overturn their conclusions.') {
      this.log = 'A newly arrived tutor rubbed me the wrong way, and I worked hard to overturn their conclusions.';
    }
  }

  submit(beaten: boolean) {
    if (beaten) {
      this.log += ' I provided a truly elegant proof that soon became accepted as the standard approach!';
      this.tutorBeaten = true;
    } else {
      this.log += ' Nothing really came of it.';
      this.tutorWins = true;
    }
    this._loggingService.addLog(this.log);
  }

  beatTutor() {
    this._characterService.increaseSkills([{Name: this.tutorSkill, Value: 1}]);
    this._characterService.addRival('My tutor who I 1-upped during my education.');
    this.graduate.emit();
  }

  lostToTutor() {
    this.graduate.emit();
  }

  getSkillsLearnedThisTerm() {
    let skillsLearnedThisTerm = this._metadataService.getSkillsLearnedThisTerm();
    return this._skillService.getGroups(skillsLearnedThisTerm);
  }

  getGroupNames() {
    let skillsLearnedThisTerm = this._metadataService.getSkillsLearnedThisTerm();
    return this._skillService.getGroupNames(skillsLearnedThisTerm);
  }

  getModifier() {
    return this._characterService.getSkills()[this.tutorSkill];
  }
}
