import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-agent-beyond-event',
  templateUrl: './agent-beyond-event.component.html',
  styleUrls: ['./agent-beyond-event.component.css']
})
export class AgentBeyondEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

  proceed() {
    this.eventComplete.emit();
  }
}
