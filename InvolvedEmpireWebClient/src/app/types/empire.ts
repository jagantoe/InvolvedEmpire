import { Army } from "./army";
import { Structures } from "./structures";

export interface Empire {
    id: number,
    name: string;
    gold: number;
    houses: number;
    villagers: number;
    miners: number;
    army: Army;
    structures: Structures;
}