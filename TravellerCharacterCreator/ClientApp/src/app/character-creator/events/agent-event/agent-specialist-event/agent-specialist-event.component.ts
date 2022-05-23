import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {SkillService} from "../../../../services/data-services/skill.service";

@Component({
  selector: 'app-agent-specialist-event',
  templateUrl: './agent-specialist-event.component.html',
  styleUrls: ['./agent-specialist-event.component.scss']
})
export class AgentSpecialistEventComponent implements OnInit {
  private chosenSkill: string;
  @Output() eventComplete = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  getGroups() {
    return this._skillService.getGroups(
      [this._skillService.SkillNames.Drive,
        this._skillService.SkillNames.DriveHovercraft,
        this._skillService.SkillNames.DriveMole,
        this._skillService.SkillNames.DriveTrack,
        this._skillService.SkillNames.DriveWalker,
        this._skillService.SkillNames.DriveWheel,
        this._skillService.SkillNames.Flyer,
        this._skillService.SkillNames.FlyerAirship,
        this._skillService.SkillNames.FlyerGrav,
        this._skillService.SkillNames.FlyerOrnithopter,
        this._skillService.SkillNames.FlyerRotor,
        this._skillService.SkillNames.FlyerWing,
        this._skillService.SkillNames.Pilot,
        this._skillService.SkillNames.PilotSmallCraft,
        this._skillService.SkillNames.PilotSpacecraft,
        this._skillService.SkillNames.PilotCapitalShips,
        this._skillService.SkillNames.Gunner,
        this._skillService.SkillNames.GunnerTurret,
        this._skillService.SkillNames.GunnerOrtillery,
        this._skillService.SkillNames.GunnerScreen,
        this._skillService.SkillNames.GunnerCapital
      ]);
  }

  getGroupNames() {
    return this._skillService.getGroupNames([this._skillService.SkillNames.Drive, this._skillService.SkillNames.Flyer,
      this._skillService.SkillNames.Pilot, this._skillService.SkillNames.Gunner]);
  }

  skillChanged(skillName: string) {
    this.chosenSkill = skillName;
  }

  submit() {
    this._characterService.addSkills([{Name: this.chosenSkill, Value: 1}]);
    this.eventComplete.emit();
  }
}
