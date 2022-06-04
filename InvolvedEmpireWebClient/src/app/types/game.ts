import { Dragon } from "./dragon";
import { Empire } from "./empire";

export interface Game {
    active: boolean;
    name: string;
    day: number;
    dragon: Dragon;
    empires: Empire[];
}