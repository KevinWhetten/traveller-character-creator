import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-rogue-event',
  templateUrl: './rogue-event.component.html',
  styleUrls: ['./rogue-event.component.css']
})
export class RogueEventComponent {
  @Output() eventComplete = new EventEmitter;
}
