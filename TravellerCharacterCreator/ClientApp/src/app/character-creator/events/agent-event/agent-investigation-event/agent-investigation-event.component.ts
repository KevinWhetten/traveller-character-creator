import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {SkillService} from "../../../../services/data-services/skill.service";
import {CharacterService} from "../../../../services/character.service";
import {RollingService} from "../../../../services/data-services/rolling.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-investigation-event',
  templateUrl: './agent-investigation-event.component.html',
  styleUrls: ['./agent-investigation-event.component.scss']
})
export class AgentInvestigationEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  skillName: string;
  rolled: boolean = false;
  success: boolean = false;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _rollingService: RollingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  getSkills() {
    return {
      'Investigation Skills': [this._skillService.SkillNames.Streetwise, this._skillService.SkillNames.Investigate]
    } as Record<string, string[]>
  }

  getSkillGroupNames() {
    return ['Investigation Skills'];
  }

  getIncreaseSkills() {
    return {
      'Increase One:': [this._skillService.SkillNames.Deception, this._skillService.SkillNames.JackOfAllTrades,
        this._skillService.SkillNames.Persuade, this._skillService.SkillNames.Tactics]
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
    this._characterService.increaseSkills([{Name: this.skillName, Value: 1}]);
    this.eventComplete.emit();
  }
}
