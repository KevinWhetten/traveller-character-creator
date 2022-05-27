import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-agent-injured-mishap',
  templateUrl: './agent-injured-mishap.component.html',
  styleUrls: ['./agent-injured-mishap.component.css']
})
export class AgentInjuredMishapComponent implements OnInit {
  @Output() mishapComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

}
