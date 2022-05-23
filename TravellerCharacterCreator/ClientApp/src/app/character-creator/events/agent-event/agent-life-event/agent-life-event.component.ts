import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-agent-life-event',
  templateUrl: './agent-life-event.component.html',
  styleUrls: ['./agent-life-event.component.scss']
})
export class AgentLifeEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

  proceed() {
    this.eventComplete.emit();
  }
}
