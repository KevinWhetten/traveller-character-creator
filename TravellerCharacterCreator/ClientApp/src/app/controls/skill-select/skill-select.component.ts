import {Component, EventEmitter, Input, Output} from '@angular/core';
import {SkillService} from "../../services/data-services/skill.service";
import {CharacterService} from "../../services/character.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-skill-select',
  templateUrl: './skill-select.component.html',
  styleUrls: ['./skill-select.component.scss']
})
export class SkillSelectComponent {
  @Input() groups: Record<string, string[]> = {};
  @Input() groupNames: string[] = [];
  @Input() label: string = 'Choose one';
  @Input() targetSkillLevel: number = 50;
  @Output() skillChange = new EventEmitter<string>();
  selectedSkill: string;
  characterSkills = this._characterService.getSkills();


  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private readonly _skillService: SkillService) {
  }

  onSkillChange() {
    this.skillChange.emit(this.selectedSkill);
  }

  getSkillDescription(skill: string) {
    return this._skillService.getSkill(skill).Description;
  }

  getAvailableSkills(skills: string[]) {
    return skills.filter(x => this.characterSkills[x] == undefined || this.characterSkills[x] < this.targetSkillLevel)
  }
}
