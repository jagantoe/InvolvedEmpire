import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GameService } from 'src/app/serices/game.service';

@Component({
  selector: 'combat-overview',
  templateUrl: './combat-overview.component.html',
  styleUrls: ['./combat-overview.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CombatOverviewComponent {
  report$;

  constructor(private gameService: GameService) {
    this.report$ = gameService.currentReport$;
  }
}
