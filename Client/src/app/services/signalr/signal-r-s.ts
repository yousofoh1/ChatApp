import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { BehaviorSubject } from 'rxjs';

export interface IChatMessage {
  senderUserName: string;
  senderImageUrl: string;
  senderFullName: string;
  receiverUserName: string;
  text: string;
}

@Injectable({
  providedIn: 'root',
})
export class SignalRS {
  private hubConnection!: signalR.HubConnection;
  messages$ = new BehaviorSubject<IChatMessage | null>(null);

  constructor() {}

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.rootUrl + '/chat', {
        accessTokenFactory: () => localStorage.getItem('token') || '',
      }) // URL of the SignalR hub
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR Connection started');
        this.addMessageListener();
      })
      .catch((err) =>
        console.log('Error establishing SignalR connection: ' + err)
      );
  };

  private addMessageListener = () => {
    this.hubConnection.on('ReceiveMessage', (message: IChatMessage) => {
      console.log(message);
      this.messages$.next(message);
    });
  };

  public sendMessage = (message: IChatMessage) => {
    if (this.hubConnection.state === signalR.HubConnectionState.Connected) {
      this.hubConnection
        .invoke('SendMessage', message)
        .catch((err) => console.error(err));
    }
  };
}
