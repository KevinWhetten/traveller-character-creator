import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterService} from "../../../../../services/character.service";
import {CharacterMetadataService} from "../../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-conspiracy-event',
  templateUrl: './agent-conspiracy-event.component.html',
  styleUrls: ['./agent-conspiracy-event.component.css']
})
export class AgentConspiracyEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _characterService: CharacterService,
              private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
  }

  submit() {
    this._metadataService.promote();
    this.eventComplete.emit();
  }
}
