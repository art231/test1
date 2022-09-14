import { MobileStatisticsEvents } from "../models/mobileStatisticsEvents.model";
import * as signalR from "@microsoft/signalr"
import { Injectable } from '@angular/core';
//import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class MobileStatisticsSignalRService {
    public data?: MobileStatisticsEvents[];
    private hubConnection?: signalR.HubConnection
    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:63060/mobileStatisticsHub')
            .withAutomaticReconnect([0, 1000, 10000])
            .build();
        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addTransferChartDataListener = () => {
        this.hubConnection?.on('transferData', (data) => {
            this.data = data.events;
            console.log("listener", data);
        });
    }
}
