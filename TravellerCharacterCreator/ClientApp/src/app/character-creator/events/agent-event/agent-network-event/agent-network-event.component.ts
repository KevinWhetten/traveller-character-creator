import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-agent-network-event',
  templateUrl: './agent-network-event.component.html',
  styleUrls: ['./agent-network-event.component.css']
})
export class AgentNetworkEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  contactsMade: number;

  constructor(private _characterService: CharacterService) { }

  ngOnInit(): void {
  }

  submit() {
    for(let i = 0; i < this.contactsMade; i++){
      this._characterService.addContact('Part of my Agent network');
    }
    this.eventComplete.emit();
  }
}
