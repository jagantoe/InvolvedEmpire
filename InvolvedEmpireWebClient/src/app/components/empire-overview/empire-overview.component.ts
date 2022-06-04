import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GameService } from 'src/app/serices/game.service';

@Component({
  selector: 'empire-overview',
  templateUrl: './empire-overview.component.html',
  styleUrls: ['./empire-overview.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmpireOverviewComponent {
  empire$;

  constructor(private gameService: GameService) {
    this.empire$ = gameService.currentEmpire$;
  }
}
