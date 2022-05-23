import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-rogue-event',
  templateUrl: './rogue-event.component.html',
  styleUrls: ['./rogue-event.component.css']
})
export class RogueEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

}
