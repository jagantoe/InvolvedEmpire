import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from "rxjs/operators";
import { GameService } from 'src/app/serices/game.service';
import { DayReport } from 'src/app/types/day-report';

@Component({
  selector: 'combat-reports',
  templateUrl: './combat-reports.component.html',
  styleUrls: ['./combat-reports.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CombatReportsComponent {

  dayReport$: Observable<DayReport[]>;

  constructor(private gameService: GameService) {
    this.dayReport$ = gameService.combatReports$.pipe(map(x => [...x].reverse()));
  }

  selectReport(report: any) {
    this.gameService.selectReport(report);
  }
}