import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Empire } from 'src/app/types/empire';

@Component({
  selector: 'empire-info',
  templateUrl: './empire-info.component.html',
  styleUrls: ['./empire-info.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmpireInfoComponent {

  @Input() empire!: Empire;
  constructor() { }

}
