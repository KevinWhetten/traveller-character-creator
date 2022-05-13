import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-agent-mishap',
  templateUrl: './agent-mishap.component.html',
  styleUrls: ['./agent-mishap.component.css']
})
export class AgentMishapComponent implements OnInit {
  @Output() mishapResolved = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

}
