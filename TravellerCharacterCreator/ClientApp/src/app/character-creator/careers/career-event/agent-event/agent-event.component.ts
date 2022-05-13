import { Component, OnInit } from '@angular/core';
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-event',
  templateUrl: './agent-event.component.html',
  styleUrls: ['./agent-event.component.scss']
})
export class AgentEventComponent implements OnInit {
  eventNumber: number;

  constructor(private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this.eventNumber = this._metadataService.getEventNumber();
  }

  proceed() {
    this._metadataService.setCurrentUrl('character-creator/careers/advancement');
  }
}
