import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {RollingService} from "../../../../services/data-services/rolling.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterService} from "../../../../services/character.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-event-prank',
  templateUrl: './event-prank.component.html',
  styleUrls: ['./event-prank.component.css']
})
export class EventPrankComponent implements OnInit {
  @Output() graduate = new EventEmitter();

  prankRival: boolean;
  prankEnemy: boolean;
  prankJail: boolean;
  prankRoll: number = 0;
  story: string;
  private log: string;

  constructor(private _characterService: CharacterService,
              private _characterMetadataService: CharacterMetadataService,
              private _dmService: RollingService,
              private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  prank(result: {roll: number, modifier: number, passed: boolean}) {
    this.log = 'A supposedly harmless prank went wrong and someone got hurt...';
    if (result.roll == 2) {
      this.log = ' I must take the Prisoner career next term.';
      this.prankJail = true;
    } else if (!result.passed) {
      this.prankEnemy = true;
    } else {
      this.prankRival = true;
    }
    this._loggingService.addLog(this.log);
  }

  gainPrankRival() {
    this._characterService.addRival('Someone I pranked during my Education.');
    if (this.story) {
      this._loggingService.addLog(this.story);
    }
    this.graduate.emit();
  }

  gainPrankEnemy() {
    this._characterService.addEnemy('Someone I pranked during my Education.');
    if (this.story) {
      this._loggingService.addLog(this.story);
    }
    this.graduate.emit();
  }

  jailed() {
    this._characterService.addEnemy('Someone I pranked during my Education.');
    if (this.story) {
      this._loggingService.addLog(this.story);
    }
    this._characterMetadataService.setJailed(true);
    this._characterMetadataService.setCurrentUrl('character-creator/careers/prison');
    this.graduate.emit();
  }
}
