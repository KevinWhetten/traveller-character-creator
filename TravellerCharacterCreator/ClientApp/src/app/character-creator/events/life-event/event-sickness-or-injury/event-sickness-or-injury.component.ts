import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-sickness-or-injury',
  templateUrl: './event-sickness-or-injury.component.html',
  styleUrls: ['./event-sickness-or-injury.component.css']
})
export class EventSicknessOrInjuryComponent implements OnInit {
  @Output() eventComplete = new EventEmitter();
  isInjured: boolean = false;


  constructor(private _loggingService: LoggingService) { }

  ngOnInit(): void {
    this._loggingService.addLog('I was injured or sick.');
  }

  submit() {
    this.eventComplete.emit();
  }

  injured() {
    this.isInjured = true;
  }
}
