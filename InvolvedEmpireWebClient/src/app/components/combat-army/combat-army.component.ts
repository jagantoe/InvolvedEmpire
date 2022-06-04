import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Archer, Infantry, Knight } from 'src/app/constants/army';
import { BattleArmyReport } from 'src/app/types/battle-army-report';

@Component({
  selector: 'combat-army',
  templateUrl: './combat-army.component.html',
  styleUrls: ['./combat-army.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CombatArmyComponent {

  @Input() armyReport!: BattleArmyReport;

  constructor() { }

  get infantryArmy() {
    return Infantry.repeat(this.armyReport.infantry - this.armyReport.infantryLosses);
  }
  get archerArmy() {
    return Archer.repeat(this.armyReport.archers - this.armyReport.archersLosses);
  }
  get knightArmy() {
    return Knight.repeat(this.armyReport.knights - this.armyReport.knightsLosses);
  }
  get infantryArmyLosses() {
    return Infantry.repeat(this.armyReport.infantryLosses);
  }
  get archerArmyLosses() {
    return Archer.repeat(this.armyReport.archersLosses);
  }
  get knightArmyLosses() {
    return Knight.repeat(this.armyReport.knightsLosses);
  }
  get fontSize() {
    const sum = this.armyReport.infantry + this.armyReport.archers + this.armyReport.knights + this.armyReport.infantryLosses + this.armyReport.archersLosses + this.armyReport.knightsLosses;
    let size;
    if (sum < 300) {
      size = 2;
    }
    else if (sum < 600) {
      size = 1.5;
    }
    else {
      size = 1;
    }
    return `${size}vw`;
  }
}
