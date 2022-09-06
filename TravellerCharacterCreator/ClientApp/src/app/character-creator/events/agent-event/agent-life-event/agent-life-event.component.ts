import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-agent-life-event',
  templateUrl: './agent-life-event.component.html',
  styleUrls: ['./agent-life-event.component.scss']
})
export class AgentLifeEventComponent {
  @Output() eventComplete = new EventEmitter;

  proceed() {
    this.eventComplete.emit();
  }
}
