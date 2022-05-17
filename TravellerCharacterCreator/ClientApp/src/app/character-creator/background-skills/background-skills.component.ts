import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {RollingService} from "../../services/data-services/rolling.service";
import {SkillService} from "../../services/data-services/skill.service";
import {CharacterMetadataService} from "../../services/metadata-services/character-metadata.service";
import {CharacterSkill} from "../../models/character-skill";

@Component({
  selector: 'app-background-skills',
  templateUrl: './background-skills.component.html',
  styleUrls: ['./background-skills.component.scss']
})
export class BackgroundSkillsComponent implements OnInit {
  skillNum: number;
  availableSkills = [this._skillService.SkillNames.Admin, this._skillService.SkillNames.Animals, this._skillService.SkillNames.Art,
    this._skillService.SkillNames.Athletics, this._skillService.SkillNames.Carouse, this._skillService.SkillNames.Drive,
    this._skillService.SkillNames.Electronics, this._skillService.SkillNames.Flyer, this._skillService.SkillNames.Language,
    this._skillService.SkillNames.Mechanic, this._skillService.SkillNames.Medic, this._skillService.SkillNames.Profession,
    this._skillService.SkillNames.Science, this._skillService.SkillNames.Seafarer, this._skillService.SkillNames.Streetwise,
    this._skillService.SkillNames.Survival, this._skillService.SkillNames.VaccSuit];

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmsService: RollingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.skillNum = this._dmsService.getDm(this._characterService.getCharacteristics().Education) + 3;
  }

  submit(skills: string[]) {
    this.updateSkills(skills);
  }

  private updateSkills(skills: string[]) {
    let characterSkills = [] as CharacterSkill[];
    for (let x of skills) {
      characterSkills.push({Name: x, Value: 0} as CharacterSkill);
    }
    this._characterService.addSkills(characterSkills);
    this._characterMetadataService.setCurrentUrl("character-creator/education");
  }
}
