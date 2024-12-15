import { Injectable } from '@angular/core';
import {MatSnackBar} from "@angular/material/snack-bar";
import { Notyf } from 'notyf';
import 'notyf/notyf.min.css';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notyf = new Notyf({
    duration: 2000,
  });

  private notyfInfo = new Notyf({
    types: [
      {
        type: 'info',
        background: '#506980',
        icon:{
          className: 'notyf-icon info',
          tagName: 'i',
          text: 'ℹ️'
        },
      }
    ],
    duration: 15000,
  });

  private notyfWarning = new Notyf({
    types: [
      {
        type: 'warning',
        background: 'orange',
        icon: '<div>🕚</div>',
      },
    ],
    duration: 15000,
  });

  constructor(private snackBar: MatSnackBar
  ) { }

  public sendSuccess(message: string)
  {
    this.notyf.success({
      message: `${message}`
    });
  }

  public sendError(message: string)
  {
    this.notyf.error({
      message: `${message}`,
      dismissible: true
    });
  }

  public sendNotification(message: string) {
    this.notyfInfo.open({
      type: 'info',
      message: `${message}`,
      dismissible: true
    });
  }

  public sendWarning(message: string) {
    this.notyfWarning.open({
      type: 'warning',
      message: `${message}`,
      dismissible: true
    });
  }
}
