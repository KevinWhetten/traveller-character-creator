import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CareerStep} from "../careers.component";
import {Career} from "../../../models/career";

@Component({
  selector: 'app-career-events',
  templateUrl: './career-events.component.html',
  styleUrls: ['./career-events.component.scss']
})
export class CareerEventsComponent implements OnInit {
  @Input() career: Career;
  @Output() nextStep = new EventEmitter<CareerStep>();
  eventRoll: number = 0;

  constructor() {
  }

  ngOnInit(): void {
  }

  defineNumber(number: number) {
    return Array(number).fill(1).map((x, i) => i + 1);
  }

  event() {
    if(this.career.Commission.target > 0){
      this.nextStep.emit(CareerStep.commission);
    }
    else {
      this.nextStep.emit(CareerStep.advancement);
    }
  }

}
