import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {Hex} from "../../models/sector-creator/hex/hex";

@Component({
  selector: 'app-system-info-dialog',
  templateUrl: './system-info-dialog.html',
  styleUrls: ['./system-info-dialog.scss']
})
export class SystemInfoDialog {
  constructor(@Inject(MAT_DIALOG_DATA) public data: Hex) {}
}
