import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-agent-severely-injured-mishap',
  templateUrl: './agent-severely-injured-mishap.component.html',
  styleUrls: ['./agent-severely-injured-mishap.component.css']
})
export class AgentSeverelyInjuredMishapComponent {
  @Output() mishapComplete = new EventEmitter;
}
