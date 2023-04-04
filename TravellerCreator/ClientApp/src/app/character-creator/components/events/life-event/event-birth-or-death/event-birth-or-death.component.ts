import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-birth-or-death',
  templateUrl: './event-birth-or-death.component.html',
  styleUrls: ['./event-birth-or-death.component.css']
})
export class EventBirthOrDeathComponent implements OnInit {
  @Output() eventComplete = new EventEmitter();

  constructor(private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('Someone close to you died, was born, or gave birth.');
  }

  submit() {
    this.eventComplete.emit();
  }
}
