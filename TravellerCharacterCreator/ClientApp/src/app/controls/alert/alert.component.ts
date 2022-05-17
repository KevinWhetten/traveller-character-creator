import {Component, Input, OnInit} from '@angular/core';

export enum AlertType {
  Error = "error",
  Warning = "warning",
  Success = "success"
}

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['alert.component.scss']
})
export class AlertComponent implements OnInit {
  @Input() type: AlertType;
  @Input() message: string;

  constructor() { }

  ngOnInit(): void {
  }

}
