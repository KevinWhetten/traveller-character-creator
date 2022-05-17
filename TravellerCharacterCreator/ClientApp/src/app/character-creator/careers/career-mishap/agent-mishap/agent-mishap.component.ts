import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-agent-mishap',
  templateUrl: './agent-mishap.component.html',
  styleUrls: ['./agent-mishap.component.css']
})
export class AgentMishapComponent implements OnInit {
  @Input() mishapNumber: number;

  constructor(private _metadataService: CharacterMetadataService) { }

  ngOnInit(): void {
    this.mishapNumber = this._metadataService.getMishapNumber();
  }

  proceed() {
    this._metadataService.setCurrentUrl('character-creator/careers');
  }
}
