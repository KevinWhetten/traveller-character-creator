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

  scienceSkills = [this._skillService.SkillNames.ScienceArchaeology, this._skillService.SkillNames.ScienceAstronomy,
    this._skillService.SkillNames.ScienceBiology, this._skillService.SkillNames.ScienceChemistry,
    this._skillService.SkillNames.ScienceCosmology, this._skillService.SkillNames.ScienceCybernetics,
    this._skillService.SkillNames.ScienceEconomics, this._skillService.SkillNames.ScienceGenetics,
    this._skillService.SkillNames.ScienceHistory, this._skillService.SkillNames.ScienceLinguistics,
    this._skillService.SkillNames.SciencePhilosophy, this._skillService.SkillNames.SciencePhysics,
    this._skillService.SkillNames.SciencePlanetology, this._skillService.SkillNames.SciencePsionicology,
    this._skillService.SkillNames.SciencePsychology,this._skillService.SkillNames.ScienceRobotics,
    this._skillService.SkillNames.ScienceSophontology, this._skillService.SkillNames.ScienceXenology];

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
