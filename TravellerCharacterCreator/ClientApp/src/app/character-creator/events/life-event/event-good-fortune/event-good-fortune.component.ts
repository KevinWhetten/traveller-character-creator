import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-good-fortune',
  templateUrl: './event-good-fortune.component.html',
  styleUrls: ['./event-good-fortune.component.css']
})
export class EventGoodFortuneComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('Something good happened to me!');
  }

  submit() {
    this.eventComplete.emit();
  }
}
