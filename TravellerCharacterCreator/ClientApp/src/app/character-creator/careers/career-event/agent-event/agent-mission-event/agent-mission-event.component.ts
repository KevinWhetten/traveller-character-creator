import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterMetadataService} from "../../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-mission-event',
  templateUrl: './agent-mission-event.component.html',
  styleUrls: ['./agent-mission-event.component.css']
})
export class AgentMissionEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor(private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
  }
}
