import {Component, Input, OnInit} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-description',
  templateUrl: './career-description.component.html',
  styleUrls: ['./career-description.component.scss']
})
export class CareerDescriptionComponent implements OnInit {
  @Input() career: Career;

  constructor() {
  }

  ngOnInit(): void {
  }
}
