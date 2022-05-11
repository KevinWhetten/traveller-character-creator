import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterMetadataService} from "../../../../services/metadata-services/character-metadata.service";

@Component({
  selector: 'app-event-psionics',
  templateUrl: './event-psionics.component.html',
  styleUrls: ['./event-psionics.component.css']
})
export class EventPsionicsComponent implements OnInit {
  @Output() graduate = new EventEmitter();
  constructor(private _characterMetadataService: CharacterMetadataService,
              private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  testPsi() {
    this._loggingService.addLog('I was approached by an underground (and highly illegal) psionic group who sensed potential in me.  I got tested for Psi, and I can enter the \'Psion\' career in any subsequent term.');
    this._characterMetadataService.setCurrentUrl('character-creator/education/university/psionic-test');
  }

  moveOn() {
    this._loggingService.addLog('I was approached by an underground (and highly illegal) psionic group who sensed potential in me, but decided against getting tested.');
    this.graduate.emit();
  }
}
