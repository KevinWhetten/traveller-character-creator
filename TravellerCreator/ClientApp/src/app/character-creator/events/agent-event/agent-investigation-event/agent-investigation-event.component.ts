import {Component, EventEmitter, Output} from '@angular/core';
import {SkillService} from "../../../../services/data-services/skill.service";
import {CharacterService} from "../../../../services/character.service";
import {RollingService} from "../../../../services/data-services/rolling.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-investigation-event',
  templateUrl: './agent-investigation-event.component.html',
  styleUrls: ['./agent-investigation-event.component.scss']
})
export class AgentInvestigationEventComponent {
  @Output() eventComplete = new EventEmitter;
  skillName: string;
  rolled: boolean = false;
  success: boolean = false;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _loggingService: LoggingService,
              private _rollingService: RollingService,
              private _skillService: SkillService) {
  }

  getSkills() {
    return {
      'Investigation Skills': [this._skillService.SkillName.Streetwise, this._skillService.SkillName.Investigate]
    } as Record<string, string[]>
  }

  getSkillGroupNames() {
    return ['Investigation Skills'];
  }

  getIncreaseSkills() {
    return {
      'Increase One:': [this._skillService.SkillName.Deception, this._skillService.SkillName.JackOfAllTrades,
        this._skillService.SkillName.Persuade, this._skillService.SkillName.Tactics]
    } as Record<string, string[]>
  }

  getIncreaseSkillGroupNames() {
    return ['Increase One:'];
  }

  changeSkill(skillName: string) {
    this.skillName = skillName;
  }

  submit(passed: boolean) {
    this.success = passed;
  }

  submitIncrease() {
    this._loggingService.addLog('An investigation took a dangerous turn. As a result, I increased a skill!');
    this._characterService.increaseSkills([{Name: this.skillName, Value: 1}]);
    this.eventComplete.emit();
  }

  proceed() {
    this._loggingService.addLog('An investigation took a dangerous turn, and I lost the trail.');
    this.eventComplete.emit();
  }
}
