import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-betrayal',
  templateUrl: './event-betrayal.component.html',
  styleUrls: ['./event-betrayal.component.css']
})
export class EventBetrayalComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  contact: string;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I was betrayed!');
  }

  getAllies() {
    return this._characterService.getAllies();
  }

  getContacts() {
    return this._characterService.getContacts();
  }

  convertAllyToRival() {
    this._characterService.convertAllyToRival(this.contact);
    this.eventComplete.emit();
  }

  convertAllyToEnemy() {
    this._characterService.convertAllyToEnemy(this.contact);
    this.eventComplete.emit();
  }

  convertContactToRival() {
    this._characterService.convertContactToRival(this.contact);
    this.eventComplete.emit();
  }

  convertContactToEnemy() {
    this._characterService.convertContactToEnemy(this.contact);
    this.eventComplete.emit();
  }

  gainRival() {
    this._characterService.addRival('I was betrayed by this person.');
    this.eventComplete.emit();
  }

  gainEnemy() {
    this._characterService.addEnemy('I was betrayed by this person.');
    this.eventComplete.emit();
  }
}
