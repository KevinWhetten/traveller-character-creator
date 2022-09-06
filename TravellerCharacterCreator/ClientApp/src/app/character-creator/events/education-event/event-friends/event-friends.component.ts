import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {LoggingService} from "../../../../services/metadata-services/logging.service";
import {CharacterService} from "../../../../services/character.service";

@Component({
  selector: 'app-event-friends',
  templateUrl: './event-friends.component.html',
  styleUrls: ['./event-friends.component.css']
})
export class EventFriendsComponent implements OnInit {
  @Output() graduate = new EventEmitter;
  friendRoll: number;
  friendGroup: boolean = false;

  constructor(private _characterService: CharacterService,
              private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
    if (this._loggingService.getLastLog() != 'I became involved in a tightly knit clique or group and made a pact to remain friends forever, wherever in the galaxy we may end up.') {
      this._loggingService.addLog('I became involved in a tightly knit clique or group and made a pact to remain friends forever, wherever in the galaxy we may end up.');
    }
  }

  friends() {
    for (let i = 0; i < this.friendRoll; i++) {
      this._characterService.addAlly('Someone in my clique/group during my education.');
    }
    this.graduate.emit();
  }
}
