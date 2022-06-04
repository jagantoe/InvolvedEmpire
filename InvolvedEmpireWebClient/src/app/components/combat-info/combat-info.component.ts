import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { BattleReport } from 'src/app/types/battle-report';

@Component({
  selector: 'combat-info',
  templateUrl: './combat-info.component.html',
  styleUrls: ['./combat-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CombatInfoComponent {

  @Input() report!: BattleReport;

  constructor() { }
}
