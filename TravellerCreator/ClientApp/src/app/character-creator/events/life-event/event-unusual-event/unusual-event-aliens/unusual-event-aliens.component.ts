import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {SkillService} from "../../../../../services/data-services/skill.service";
import {CharacterService} from "../../../../../services/character.service";
import {CharacterMetadataService} from "../../../../../services/metadata-services/character-metadata.service";
import {LoggingService} from "../../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-unusual-event-aliens',
  templateUrl: './unusual-event-aliens.component.html',
  styleUrls: ['./unusual-event-aliens.component.css']
})
export class UnusualEventAliensComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  selectedScienceSkill: string;

  scienceSkills = [this._skillService.SkillName.ScienceArchaeology, this._skillService.SkillName.ScienceAstronomy,
    this._skillService.SkillName.ScienceBiology, this._skillService.SkillName.ScienceChemistry,
    this._skillService.SkillName.ScienceCosmology, this._skillService.SkillName.ScienceCybernetics,
    this._skillService.SkillName.ScienceEconomics, this._skillService.SkillName.ScienceGenetics,
    this._skillService.SkillName.ScienceHistory, this._skillService.SkillName.ScienceLinguistics,
    this._skillService.SkillName.SciencePhilosophy, this._skillService.SkillName.SciencePhysics,
    this._skillService.SkillName.SciencePlanetology, this._skillService.SkillName.SciencePsionicology,
    this._skillService.SkillName.SciencePsychology,this._skillService.SkillName.ScienceRobotics,
    this._skillService.SkillName.ScienceSophontology, this._skillService.SkillName.ScienceXenology];

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService,
              private _metadataService: CharacterMetadataService,
              private _skillService: SkillService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I met an alien race!');
  }

  getScienceGroup() {
    return this._skillService.getGroups(this.scienceSkills);
  }

  submit() {
    let term = this._metadataService.getTerm();
    this._characterService.addContact(`An alien I met in term ${term}`);
    this._characterService.addSkills([{Name: this.selectedScienceSkill, Value: 1}]);
    this.eventComplete.emit();
  }
}
