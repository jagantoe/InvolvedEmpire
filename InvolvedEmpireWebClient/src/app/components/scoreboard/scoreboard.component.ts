import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { GameService } from 'src/app/serices/game.service';
import { Empire } from 'src/app/types/empire';

@Component({
  selector: 'scoreboard',
  templateUrl: './scoreboard.component.html',
  styleUrls: ['./scoreboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ScoreboardComponent {

  displayedColumns: string[] = ['position', 'name', 'gold', 'houses', 'miners', 'army'];
  empires$: Observable<Empire[]>;

  constructor(private gameService: GameService) {
    this.empires$ = gameService.empires$;
  }

  selectEmpire(empire: any) {
    this.gameService.selectEmpire(empire.id);
  }

}
