import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {SkillService} from "../../../../services/data-services/skill.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-specialist-event',
  templateUrl: './agent-specialist-event.component.html',
  styleUrls: ['./agent-specialist-event.component.scss']
})
export class AgentSpecialistEventComponent implements OnInit {
  private chosenSkill: string;
  @Output() eventComplete = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _skillService: SkillService) {
  }

  ngOnInit(): void {
  }

  getGroups() {
    return this._skillService.getGroups(
      [this._skillService.SkillName.Drive,
        this._skillService.SkillName.DriveHovercraft,
        this._skillService.SkillName.DriveMole,
        this._skillService.SkillName.DriveTrack,
        this._skillService.SkillName.DriveWalker,
        this._skillService.SkillName.DriveWheel,
        this._skillService.SkillName.Flyer,
        this._skillService.SkillName.FlyerAirship,
        this._skillService.SkillName.FlyerGrav,
        this._skillService.SkillName.FlyerOrnithopter,
        this._skillService.SkillName.FlyerRotor,
        this._skillService.SkillName.FlyerWing,
        this._skillService.SkillName.Pilot,
        this._skillService.SkillName.PilotSmallCraft,
        this._skillService.SkillName.PilotSpacecraft,
        this._skillService.SkillName.PilotCapitalShips,
        this._skillService.SkillName.Gunner,
        this._skillService.SkillName.GunnerTurret,
        this._skillService.SkillName.GunnerOrtillery,
        this._skillService.SkillName.GunnerScreen,
        this._skillService.SkillName.GunnerCapital
      ]);
  }

  getGroupNames() {
    return this._skillService.getGroupNames([this._skillService.SkillName.Drive, this._skillService.SkillName.Flyer,
      this._skillService.SkillName.Pilot, this._skillService.SkillName.Gunner]);
  }

  skillChanged(skillName: string) {
    this.chosenSkill = skillName;
  }

  submit() {
    this._loggingService.addLog('I received specialist training in vehicles.');
    this._characterService.addSkills([{Name: this.chosenSkill, Value: 1}]);
    this.eventComplete.emit();
  }
}
