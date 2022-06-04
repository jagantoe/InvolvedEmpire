import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Structures } from 'src/app/types/structures';

@Component({
  selector: 'empire-structures',
  templateUrl: './empire-structures.component.html',
  styleUrls: ['./empire-structures.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmpireStructuresComponent {

  @Input() structures!: Structures;

}
