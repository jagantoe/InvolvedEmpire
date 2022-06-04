import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { GameService } from './serices/game.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'InvolvedEmpireWebClient';

  currentView$: Observable<number>;

  constructor(private gameService: GameService) {
    this.currentView$ = this.gameService.currentView$.pipe(
      map(x => x == "empire" ? 0 : x == "combat" ? 2 : 1)
    );
  }
}
