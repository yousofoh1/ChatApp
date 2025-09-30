import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRS {
  private hubConnection!: signalR.HubConnection;

  constructor() {}

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.rootUrl+'/chat') // URL of the SignalR hub
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection started'))
      .catch(err => console.log('Error establishing SignalR connection: ' + err));
  }

  public addMessageListener = () => {
    this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
      console.log(`User: ${user}, Message: ${message}`);
    });
  }

  public sendMessage = (user: string, message: string) => {
    this.hubConnection.invoke('SendMessage', user, message)
      .catch(err => console.error(err));
  }
}
