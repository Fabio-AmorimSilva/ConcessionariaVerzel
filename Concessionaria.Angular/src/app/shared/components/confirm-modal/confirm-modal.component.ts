import { ConfirmModalConfig } from './classes/confirm-modal-config';
/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.scss']
})
export class ConfirmModalComponent implements OnInit {

  constructor(
    private matDialogRef: MatDialogRef<ConfirmModalComponent>,
    @Inject(MAT_DIALOG_DATA) public config: ConfirmModalConfig
  ) { }

  confirm(confirmed: boolean): void {
    this.matDialogRef.close(confirmed);
  }

  ngOnInit(): void {

  }
}




