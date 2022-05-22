import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-unusual-event-contact-with-government',
  templateUrl: './unusual-event-contact-with-government.component.html',
  styleUrls: ['./unusual-event-contact-with-government.component.css']
})
export class UnusualEventContactWithGovernmentComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I met with some high government leaders.');
  }

  submit() {
    this.eventComplete.emit();
  }
}
