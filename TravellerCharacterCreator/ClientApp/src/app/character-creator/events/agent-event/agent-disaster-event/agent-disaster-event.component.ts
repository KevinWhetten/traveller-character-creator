import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-agent-disaster-event',
  templateUrl: './agent-disaster-event.component.html',
  styleUrls: ['./agent-disaster-event.component.css']
})
export class AgentDisasterEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  continue() {
    this._loggingService.addLog('Disaster struck!');
    this.eventComplete.emit();
  }
}
