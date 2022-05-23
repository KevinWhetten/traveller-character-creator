import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CharacterMetadataService} from "../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-event',
  templateUrl: './agent-event.component.html',
  styleUrls: ['./agent-event.component.scss']
})
export class AgentEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;
  eventNumber: number;

  constructor(private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this.eventNumber = this._metadataService.getEventNumber();
  }

  proceed() {
    this.eventComplete.emit();
  }
}
