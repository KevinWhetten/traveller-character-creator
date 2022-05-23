import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-agent-disaster-event',
  templateUrl: './agent-disaster-event.component.html',
  styleUrls: ['./agent-disaster-event.component.css']
})
export class AgentDisasterEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor() {
  }

  ngOnInit(): void {
  }

  continue() {
    this.eventComplete.emit();
  }
}
