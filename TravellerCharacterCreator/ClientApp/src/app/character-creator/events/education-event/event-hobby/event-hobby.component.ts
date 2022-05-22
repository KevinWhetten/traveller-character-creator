import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {SkillService} from "../../../../services/data-services/skill.service";
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-event-hobby',
  templateUrl: './event-hobby.component.html',
  styleUrls: ['./event-hobby.component.css']
})
export class EventHobbyComponent implements OnInit {
  @Output() graduate = new EventEmitter();
  hobbySkill: string;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  hobby() {
    this._loggingService.addLog('I developed a healthy interest in a hobby or other area of study.');
    this._characterService.addSkills([{Name: this.hobbySkill, Value: 0}]);
    this.graduate.emit();
  }

  getBasicGroups() {
    let list = this._skillService.getBasicGroups(this._skillService.skills.map(x => x.Name));
    this.removeJackOfAllTradesFromBasicList(list);
    return list;
  }

  private removeJackOfAllTradesFromBasicList(list: Record<string, string[]>) {
    if (list['Basic Skills'].indexOf(this._skillService.SkillNames.JackOfAllTrades) >= 0) {
      list['Basic Skills'].splice(list['Basic Skills'].indexOf(this._skillService.SkillNames.JackOfAllTrades), 1)
    }
  }
}
