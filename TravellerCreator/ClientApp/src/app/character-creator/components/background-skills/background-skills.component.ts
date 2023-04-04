import {Component, OnInit} from '@angular/core';
import {CharacterService} from "../../services/character.service";
import {RollingService} from "../../../services/rolling.service";
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
  availableSkills = [this._skillService.SkillName.Admin, this._skillService.SkillName.Animals, this._skillService.SkillName.Art,
    this._skillService.SkillName.Athletics, this._skillService.SkillName.Carouse, this._skillService.SkillName.Drive,
    this._skillService.SkillName.Electronics, this._skillService.SkillName.Flyer, this._skillService.SkillName.Language,
    this._skillService.SkillName.Mechanic, this._skillService.SkillName.Medic, this._skillService.SkillName.Profession,
    this._skillService.SkillName.Science, this._skillService.SkillName.Seafarer, this._skillService.SkillName.Streetwise,
    this._skillService.SkillName.Survival, this._skillService.SkillName.VaccSuit];

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmsService: RollingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
    this.skillNum = this._dmsService.getDm(this._characterService.getCharacteristics().Education.current) + 3;
  }

  submit(skills: string[]) {
    this.updateSkills(skills);
    this._characterMetadataService.setCurrentUrl("character-creator/education");
  }

  private updateSkills(skills: string[]) {
    let characterSkills = [] as CharacterSkill[];
    for (let x of skills) {
      characterSkills.push({Name: x, Value: 0} as CharacterSkill);
    }
    this._characterService.addSkills(characterSkills);
  }
}
