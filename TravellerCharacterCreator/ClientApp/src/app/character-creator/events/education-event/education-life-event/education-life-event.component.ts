import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-education-life-event',
  templateUrl: './education-life-event.component.html',
  styleUrls: ['./education-life-event.component.css']
})
export class EducationLifeEventComponent implements OnInit {
  @Output() finished = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

}
