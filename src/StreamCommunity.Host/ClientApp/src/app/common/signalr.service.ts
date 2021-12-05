import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  connection: signalR.HubConnection;

  constructor() {}

  public initiateSignalrConnection(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl('notify')
        .build();

      this.connection
        .start()
        .then(() => {
          console.log(
            `SignalR connection success! connectionId: ${this.connection.connectionId} `
          );
          resolve();
        })
        .catch((error) => {
          console.log(`SignalR connection error: ${error}`);
          reject();
        });
    });
  }
}
