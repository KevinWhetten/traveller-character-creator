import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-agent-home-mishap',
  templateUrl: './agent-home-mishap.component.html',
  styleUrls: ['./agent-home-mishap.component.css']
})
export class AgentHomeMishapComponent {
  @Output() mishapComplete = new EventEmitter;

  submit() {
    this.mishapComplete.emit();
  }
}
