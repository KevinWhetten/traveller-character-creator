import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {RollingService} from "../../../../services/data-services/rolling.service";
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-political',
  templateUrl: './event-political.component.html',
  styleUrls: ['./event-political.component.css']
})
export class EventPoliticalComponent implements OnInit {
  @Output() graduate = new EventEmitter();
  private log: string;
  politicalRoll: number;
  politicalLeader: boolean;
  politicalGrunt: boolean;
  story: string;

  constructor(private _characterService: CharacterService,
              private _dmService: RollingService,
              private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  politicalMovement() {
    this.log = 'I joined a political movement.';
    if (this.politicalRoll + this._dmService.getDm(this._characterService.getCharacteristics().SocialStatus) >= 8) {
      this.log += ' And became a leading figure!';
      this.politicalLeader = true;
    } else {
      this.politicalGrunt = true;
    }
    this._loggingService.addLog(this.log);
  }

  becamePoliticalLeader() {
    this._characterService.addAlly('One person in the political movement I joined during my education.');
    this._characterService.addEnemy('One person outside of the political movement I joined during my education.');
    if (this.story) {
      this._loggingService.addLog(this.story);
    }
    this.graduate.emit();
  }

  notPoliticalLeader() {
    this.graduate.emit();
  }
}
