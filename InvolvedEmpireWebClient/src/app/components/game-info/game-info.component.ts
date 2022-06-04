import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { GameService } from 'src/app/serices/game.service';
import { Game } from 'src/app/types/game';

@Component({
  selector: 'game-info',
  templateUrl: './game-info.component.html',
  styleUrls: ['./game-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GameInfoComponent {

  game$: Observable<Game>;

  constructor(private gameService: GameService) {
    this.game$ = this.gameService.game$;
  }
}
