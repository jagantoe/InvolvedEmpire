import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable, timer } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { DataService } from 'src/app/serices/data.service';
import { Game } from 'src/app/types/game';

@Component({
  selector: 'loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadingComponent {

  game$: Observable<Game>;
  connected$: Observable<boolean>;
  reconnectTimer$: Observable<boolean> = timer(60000).pipe(map(x => true));

  constructor(private dataService: DataService) {
    this.connected$ = dataService.connected$;
    this.game$ = dataService.gameState$;
  }

  reconnect() {
    this.dataService.connect();
  }
}
