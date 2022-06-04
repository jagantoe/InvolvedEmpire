import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { Observable, Subject, timer } from 'rxjs';
import { map, scan } from 'rxjs/operators';
import { DayReport } from '../types/day-report';
import { Game } from '../types/game';

@Injectable({
  providedIn: 'root'
})
export class DataService {


  private hubConnection: HubConnection = new HubConnectionBuilder()
    .withUrl("***place app url here***" + '/hub/Dashboard')
    .withAutomaticReconnect()
    .build();
  connected$: Observable<boolean> = timer(1000, 10000).pipe(
    map(x => this.hubConnection.state == HubConnectionState.Connected)
  );

  private gameStateSubject = new Subject<Game>();
  gameState$: Observable<Game> = this.gameStateSubject.asObservable();

  private battleReportsSubject = new Subject<DayReport>();
  battleReports$: Observable<DayReport[]> = this.battleReportsSubject.asObservable().pipe(
    scan((acc, cur) => {
      acc.push(cur);
      return acc;
    }, [] as DayReport[]),
  );

  constructor() {
    this.connect();
    this.addListeners();
  }

  public connect() {
    try {
      this.hubConnection.start().catch();
    } catch (error) {
      console.log('error while establishing signalr connection: ' + error);
    }
  }

  private addListeners() {
    this.hubConnection.on("game", (gameState) => {
      this.gameStateSubject.next(gameState);
    });
    this.hubConnection.on("battlereport", (battleReport) => {
      this.battleReportsSubject.next(battleReport);
    });
  }
}
