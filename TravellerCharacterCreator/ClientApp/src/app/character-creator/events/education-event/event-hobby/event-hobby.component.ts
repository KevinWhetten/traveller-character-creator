import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";

@Component({
  selector: 'app-event-hobby',
  templateUrl: './event-hobby.component.html',
  styleUrls: ['./event-hobby.component.css']
})
export class EventHobbyComponent implements OnInit {
  @Output() graduate = new EventEmitter();
  hobbySkill: string;

  constructor(private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  hobby() {
    this._loggingService.addLog('I developed a healthy interest in a hobby or other area of study.');
    this.graduate.emit();
  }

}
