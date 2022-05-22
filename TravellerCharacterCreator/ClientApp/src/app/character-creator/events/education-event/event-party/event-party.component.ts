import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterSkill} from "../../../../models/character-skill";
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterService} from "../../../../services/character.service";
import {SkillService} from "../../../../services/data-services/skill.service";

@Component({
  selector: 'app-event-party',
  templateUrl: './event-party.component.html',
  styleUrls: ['./event-party.component.css']
})
export class EventPartyComponent implements OnInit {
  @Output() graduate = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  party() {
    this._loggingService.addLog('I took advantage of youth, and partied as much as I studied.');
    this._characterService.addSkills([{Name: this._skillService.SkillNames.Carouse, Value: 1} as CharacterSkill]);
    this.graduate.emit();
  }

}
