import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-citizen-event',
  templateUrl: './citizen-event.component.html',
  styleUrls: ['./citizen-event.component.css']
})
export class CitizenEventComponent implements OnInit {
  @Output() eventComplete = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

}
