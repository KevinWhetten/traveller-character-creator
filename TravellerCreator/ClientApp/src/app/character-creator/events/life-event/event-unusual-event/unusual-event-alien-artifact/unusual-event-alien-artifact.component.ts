import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";
import {LoggingService} from "../../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-unusual-event-alien-artifact',
  templateUrl: './unusual-event-alien-artifact.component.html',
  styleUrls: ['./unusual-event-alien-artifact.component.css']
})
export class UnusualEventAlienArtifactComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I found an Alien Artifact.');
  }

  submit() {
    this._characterService.addEquipment('Alien Artifact');
    this.eventComplete.emit();
  }
}
