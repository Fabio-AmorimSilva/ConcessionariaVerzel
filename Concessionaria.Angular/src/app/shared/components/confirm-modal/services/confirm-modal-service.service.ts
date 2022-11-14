import { ConfirmModalConfig } from './../classes/confirm-modal-config';
import { take } from 'rxjs';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmModalComponent } from './../confirm-modal.component';
import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfirmModalService {

  closed = new EventEmitter<boolean>();

  constructor(private matDialog: MatDialog) {}

  open(data: ConfirmModalConfig): void {
    const dialog = this.matDialog.open(ConfirmModalComponent, {data});

    dialog.afterClosed()
      .pipe(take(1))
      .subscribe((result: boolean) => {
        this.closed.emit(result);
      });
  }
}
