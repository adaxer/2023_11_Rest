import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  moviesChanged: Subject<void> = new Subject<void>();
  baseUrl='https://localhost:7267';

  private hubConnection?: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/messages`)
      .withAutomaticReconnect()
      .build();
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.hubConnection?.on('message', s => {
          console.log("SignalR incoming: ", s);
          this.moviesChanged.next();
        });
      })
      .catch(err => console.log('Error while starting connection: ' + err))
  }
}

