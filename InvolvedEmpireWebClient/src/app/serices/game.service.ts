import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BattleReport } from '../types/battle-report';
import { DayReport } from '../types/day-report';
import { Dragon } from '../types/dragon';
import { Empire } from '../types/empire';
import { Game } from '../types/game';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  game$: Observable<Game>;
  dragon$: Observable<Dragon>;
  empires$: Observable<Empire[]>;
  combatReports$: Observable<DayReport[]>;

  private currentView: BehaviorSubject<string> = new BehaviorSubject<string>("");
  currentView$: Observable<string> = this.currentView.asObservable();

  private currentEmpire: BehaviorSubject<number> = new BehaviorSubject<number>(null as any);
  currentEmpire$: Observable<Empire>;

  private currentReport: BehaviorSubject<BattleReport> = new BehaviorSubject<BattleReport>(null as any);
  currentReport$: Observable<BattleReport> = this.currentReport.asObservable();


  constructor(private dataService: DataService) {
    this.game$ = dataService.gameState$;
    this.dragon$ = this.game$.pipe(map(game => game.dragon));
    this.empires$ = this.game$.pipe(map(game => game.empires));
    this.combatReports$ = dataService.battleReports$;
    this.currentEmpire$ = combineLatest([this.currentEmpire.asObservable(), this.game$]).pipe(
      map(([id, game]) => game.empires.find(x => x.id == id)!)
    )
  }


  public selectEmpire(empire: number) {
    this.currentView.next("empire");
    this.currentEmpire.next(empire);
  }

  public selectReport(report: BattleReport) {
    this.currentView.next("combat");
    this.currentReport.next(report);
  }
}
