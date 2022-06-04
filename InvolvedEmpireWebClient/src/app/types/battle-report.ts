import { BattleArmyReport } from "./battle-army-report";

export interface BattleReport {
    dateTime: Date;
    attackSuccessful: boolean;
    winner: string;
    loser: string;
    type: string;

    attacker: BattleArmyReport;
    defender: BattleArmyReport;

    goldStolen: number;
    minersStolen: number;
}