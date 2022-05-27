import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-agent-deal-mishap',
  templateUrl: './agent-deal-mishap.component.html',
  styleUrls: ['./agent-deal-mishap.component.css']
})
export class AgentDealMishapComponent implements OnInit {
  @Output() mishapComplete = new EventEmitter;
  accepted: boolean = false;
  rejected: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  accept() {
    this.accepted = true;
  }

  reject() {
    this.rejected = true;
  }

  proceed() {
    this.mishapComplete.emit();
  }
}
