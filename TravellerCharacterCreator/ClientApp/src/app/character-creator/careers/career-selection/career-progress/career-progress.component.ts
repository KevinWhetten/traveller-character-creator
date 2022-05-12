import {Component, Input, OnInit} from '@angular/core';
import {Career} from "../../../../models/career";

@Component({
  selector: 'app-career-progress',
  templateUrl: './career-progress.component.html',
  styleUrls: ['./career-progress.component.scss']
})
export class CareerProgressComponent implements OnInit {
  @Input() career: Career;

  constructor() {
  }

  ngOnInit(): void {
  }
}
