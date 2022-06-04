import { BattleReport } from "./battle-report";

export interface DayReport {
    day: number;
    battles: BattleReport[];
}