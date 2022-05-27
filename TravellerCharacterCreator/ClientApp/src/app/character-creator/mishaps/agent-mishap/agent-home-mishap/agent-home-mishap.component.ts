import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-agent-home-mishap',
  templateUrl: './agent-home-mishap.component.html',
  styleUrls: ['./agent-home-mishap.component.css']
})
export class AgentHomeMishapComponent implements OnInit {
  @Output() mishapComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

  submit() {
    this.mishapComplete.emit();
  }
}
