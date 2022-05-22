import {Component, OnInit} from '@angular/core';
import {LoggingService} from "../../services/metadata-services/logging.service";

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
  styleUrls: ['./story.component.css']
})
export class StoryComponent implements OnInit {
  story: string = '';
  submitted: boolean = false;
  disabled: string = '';

  constructor(private _loggingService: LoggingService) {
  }

  ngOnInit(): void {
  }

  submitStory() {
    if (this.story) {
      this._loggingService.addLog(this.story);
      this.submitted = true;
      this.disabled = 'disabled';
    }
  }
}
