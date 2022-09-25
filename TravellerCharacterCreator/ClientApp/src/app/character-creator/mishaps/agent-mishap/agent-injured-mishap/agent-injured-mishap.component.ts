import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-agent-injured-mishap',
  templateUrl: './agent-injured-mishap.component.html',
  styleUrls: ['./agent-injured-mishap.component.css']
})
export class AgentInjuredMishapComponent {
  @Output() mishapComplete = new EventEmitter;
}
