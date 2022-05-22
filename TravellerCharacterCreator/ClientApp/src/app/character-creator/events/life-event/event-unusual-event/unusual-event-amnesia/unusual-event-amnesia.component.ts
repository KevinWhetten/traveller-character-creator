import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-unusual-event-amnesia',
  templateUrl: './unusual-event-amnesia.component.html',
  styleUrls: ['./unusual-event-amnesia.component.css']
})
export class UnusualEventAmnesiaComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I have amnesia. I don\'t remember what happened this term.');
  }

  submit() {
    this.eventComplete.emit();
  }
}
