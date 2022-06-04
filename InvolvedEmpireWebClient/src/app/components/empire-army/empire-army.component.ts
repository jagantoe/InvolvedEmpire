import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Archer, Infantry, Knight } from 'src/app/constants/army';
import { Army } from 'src/app/types/army';

@Component({
  selector: 'empire-army',
  templateUrl: './empire-army.component.html',
  styleUrls: ['./empire-army.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmpireArmyComponent {

  @Input() army!: Army;

  constructor() { }

  get infantry() {
    return this.army.infantry;
  }
  get archers() {
    return this.army.archers;
  }
  get knights() {
    return this.army.knights;
  }
  get infantryArmy() {
    return Infantry.repeat(this.army.infantry);
  }
  get archerArmy() {
    return Archer.repeat(this.army.archers);
  }
  get knightArmy() {
    return Knight.repeat(this.army.knights);
  }
  get fontSize() {
    const sum = this.army.infantry + this.army.archers + this.army.knights;
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
