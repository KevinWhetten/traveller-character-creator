import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../../services/metadata-services/logging.service";
import {CharacterService} from "../../../../../services/character.service";

@Component({
  selector: 'app-unusual-event-ancient-technology',
  templateUrl: './unusual-event-ancient-technology.component.html',
  styleUrls: ['./unusual-event-ancient-technology.component.css']
})
export class UnusualEventAncientTechnologyComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I gained some piece of ancient technology.');
  }

  submit() {
    this._characterService.addEquipment('Ancient Technology');
    this.eventComplete.emit();
  }
}
