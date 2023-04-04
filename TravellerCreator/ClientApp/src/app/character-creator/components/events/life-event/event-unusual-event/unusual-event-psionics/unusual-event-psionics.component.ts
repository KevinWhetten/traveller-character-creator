import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-unusual-event-psionics',
  templateUrl: './unusual-event-psionics.component.html',
  styleUrls: ['./unusual-event-psionics.component.css']
})
export class UnusualEventPsionicsComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I encountered a Psionic Institute!');
  }

  refuse() {
    this._loggingService.addLog('I decided against getting tested!');
    this.eventComplete.emit();
  }

  accept() {
    this._loggingService.addLog('I decided to get tested!');
    this.eventComplete.emit();
  }
}
