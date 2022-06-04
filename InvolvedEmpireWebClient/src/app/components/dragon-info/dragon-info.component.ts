import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { GameService } from 'src/app/serices/game.service';
import { Dragon } from 'src/app/types/dragon';

@Component({
  selector: 'dragon-info',
  templateUrl: './dragon-info.component.html',
  styleUrls: ['./dragon-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DragonInfoComponent {

  dragon$: Observable<Dragon>;

  constructor(private gameService: GameService) {
    this.dragon$ = gameService.dragon$;
  }
}
