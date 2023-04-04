import {Component, EventEmitter, Output} from '@angular/core';
import {SkillService} from "../../../../services/data-services/skill.service";
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-deal-mishap',
  templateUrl: './agent-deal-mishap.component.html',
  styleUrls: ['./agent-deal-mishap.component.css']
})
export class AgentDealMishapComponent {
  @Output() mishapComplete = new EventEmitter;
  accepted: boolean = false;
  rejected: boolean = false;
  injured: boolean = false;
  selectedSkill: string;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService,
              private _skillService: SkillService) {
  }

  accept() {
    this._metadataService.setCurrentUrl('character-creator/careers/benefits');
  }

  reject() {
    this._characterService.addEnemy('A criminal or other figure under investigation who offered me a deal. I refused.');
    this._characterService.increaseSkills([{Name: this.selectedSkill, Value: 1}]);
    this.mishapComplete.emit();
  }

  getGroups() {
    return this._skillService.getGroups(this._skillService.SkillNames);
  }

  getGroupNames() {
    return this._skillService.getGroupNames(this._skillService.SkillNames);
  }

  changeSkill(skillName: string) {
    this.selectedSkill = skillName;
  }
}
