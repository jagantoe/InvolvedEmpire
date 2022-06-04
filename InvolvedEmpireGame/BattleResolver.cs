namespace InvolvedEmpireGame;

public static class BattleResolver
{
    private static int ArcherVsInfantryMultiplier = 2;
    private static int KnightVsInfantryMultiplier = 5;
    private static int KnightVsArcherMultiplier = 2;
    // Siege on city or field
    public static BattleReport Battle(Empire attacker, Empire defender, bool field)
    {
        var (aInfantryDamage, aArcherDamage, aKnightDamage) = ArmyDamage(attacker.Army);
        var (dInfantryDamage, dArcherDamage, dKnightDamage) = ArmyDamage(defender.Army);

        // True means attacker won
        var attackerInfantryLosses = (dInfantryDamage + dArcherDamage * ArcherVsInfantryMultiplier + dKnightDamage * KnightVsInfantryMultiplier) / 5;
        var attackerArcherLosses = (dInfantryDamage / ArcherVsInfantryMultiplier + dArcherDamage + dKnightDamage * KnightVsArcherMultiplier) / 5;
        var attackerKnightLosses = (dInfantryDamage / KnightVsInfantryMultiplier + dArcherDamage / KnightVsArcherMultiplier + dKnightDamage) / 5;

        var defenderInfantryLosses = (aInfantryDamage + aArcherDamage * ArcherVsInfantryMultiplier + aKnightDamage * KnightVsInfantryMultiplier) / 5;
        var defenderArcherLosses = (aInfantryDamage / ArcherVsInfantryMultiplier + aArcherDamage + aKnightDamage * KnightVsArcherMultiplier) / 5;
        var defenderKnightLosses = (aInfantryDamage / KnightVsInfantryMultiplier + aArcherDamage / KnightVsArcherMultiplier + aKnightDamage) / 5;

        var attackerReport = attacker.ArmyLosses(attackerInfantryLosses, attackerArcherLosses, attackerKnightLosses);
        var defenderReport = defender.ArmyLosses(defenderInfantryLosses, defenderArcherLosses, defenderKnightLosses);

        var attackerWins = defenderInfantryLosses + defenderArcherLosses + defenderKnightLosses > attackerInfantryLosses + attackerArcherLosses + attackerKnightLosses;
        if (attackerWins)
        {
            int goldStolen = 0;
            int minersStolen = 0;
            if (!field)
            {
                var goldCarryCapacity = GoldCarryCapacity(attacker.Army);
                goldStolen = defender.Gold >= goldCarryCapacity ? goldCarryCapacity : defender.Gold;
                if (defender.Structures.Vault)
                {
                    var limit = defender.Gold > 500 ? defender.Gold / 5 : 500;
                    var goldLeft = defender.Gold - goldStolen;
                    if (goldLeft < 0) goldLeft = 0;
                    if (goldLeft < limit)
                    {
                        goldStolen -= limit - goldLeft;
                    }
                }
                minersStolen = Random.Shared.Next(0, MinersCarryCapacity(attacker.Army) + 1);
                attacker.AddGold(goldStolen);
                defender.StealGold(goldStolen);
                attacker.AddMiners(minersStolen);
                defender.StealMiners(minersStolen);
            }
            return new BattleReport()
            {
                AttackSuccessful = true,
                Winner = attacker.Name,
                Loser = defender.Name,
                Type = field ? FieldType : SiegeType,
                Attacker = attackerReport,
                Defender = defenderReport,
                GoldStolen = goldStolen,
                MinersStolen = minersStolen
            };
        }
        else
        {
            return new BattleReport()
            {
                AttackSuccessful = false,
                Winner = defender.Name,
                Loser = attacker.Name,
                Type = field ? FieldType : SiegeType,
                Attacker = attackerReport,
                Defender = defenderReport,
            };
        }
    }
    private static (int, int, int) ArmyDamage(Army army)
    {
        return (
            Random.Shared.Next(0, army.Infantry / 2) + army.Infantry,
            Random.Shared.Next(0, army.Archers / 2) + army.Archers,
            Random.Shared.Next(0, army.Knights / 2) + army.Knights
            );
    }
    private static int GoldCarryCapacity(Army army)
    {
        return (army.Infantry * 5 + army.Archers * 2 + army.Knights * 15);
    }
    private static int MinersCarryCapacity(Army army)
    {
        return (army.Infantry + army.Archers + army.Knights) / 20;
    }

    #region Dragon
    private static int ArcherDragonMultiplier = 3;
    private static int KnightDragonMultiplier = 5;
    private static int DragonWeakness = 13;
    private static int DragonMaxGoldSteal = 300;
    private static string FieldType = "Field";
    private static string SiegeType = "Siege";
    // Siege on dragon lair or field
    public static BattleReport Battle(Empire empire, Dragon dragon, bool field)
    {
        var totalDamage = empire.Army.Infantry + empire.Army.Archers * ArcherDragonMultiplier + empire.Army.Knights * KnightDragonMultiplier;
        if (totalDamage % DragonWeakness == 0)
        {
            totalDamage *= 2;
        }
        var (infantryLosses, archerLosses, knightLosses) = DragonDamage();

        var dragonReport = dragon.TakeDamage(totalDamage);
        var armyReport = empire.ArmyLosses(infantryLosses, archerLosses, knightLosses);

        if (dragon.Alive)
        {
            var goldStolen = empire.Gold <= DragonMaxGoldSteal ? empire.Gold : DragonMaxGoldSteal;
            empire.StealGold(goldStolen);
            dragon.AddGold(goldStolen);
            return new BattleReport()
            {
                AttackSuccessful = false,
                Winner = "Dragon",
                Loser = empire.Name,
                Type = field ? FieldType : SiegeType,
                Attacker = armyReport,
                Defender = dragonReport,
            };
        }
        else
        {
            var goldStolen = dragon.GoldHoard;
            empire.AddGold(goldStolen);
            dragon.Dead();

            return new BattleReport()
            {
                AttackSuccessful = true,
                Winner = empire.Name,
                Loser = "Dragon",
                Type = field ? FieldType : SiegeType,
                Attacker = armyReport,
                Defender = dragonReport,
                GoldStolen = goldStolen,
            };
        }
    }
    // Dragon sieges city, never field
    public static BattleReport Battle(Dragon dragon, Empire empire)
    {
        var totalDamage = empire.Army.Infantry + empire.Army.Archers * ArcherDragonMultiplier + empire.Army.Knights * KnightDragonMultiplier;
        if (totalDamage % DragonWeakness == 0)
        {
            totalDamage *= 2;
        }
        var (infantryLosses, archerLosses, knightLosses) = DragonDamage();

        var dragonReport = dragon.TakeDamage(totalDamage);
        var armyReport = empire.ArmyLosses(infantryLosses, archerLosses, knightLosses);

        if (dragon.Alive)
        {
            var goldStolen = empire.Gold <= DragonMaxGoldSteal ? empire.Gold : DragonMaxGoldSteal;
            if (empire.Structures.Vault)
            {
                var limit = empire.Gold > 500 ? empire.Gold / 5 : 500;
                var goldLeft = empire.Gold - goldStolen;
                if (goldLeft < 0) goldLeft = 0;
                if (goldLeft < limit)
                {
                    goldStolen -= limit - goldLeft;
                }
            }
            empire.StealGold(goldStolen);
            dragon.AddGold(goldStolen);
            return new BattleReport()
            {
                AttackSuccessful = true,
                Winner = "Dragon",
                Loser = empire.Name,
                Type = SiegeType,
                Attacker = dragonReport,
                Defender = armyReport,
                GoldStolen = goldStolen,
            };
        }
        else
        {
            var goldStolen = dragon.GoldHoard;
            empire.AddGold(goldStolen);
            dragon.Dead();

            return new BattleReport()
            {
                AttackSuccessful = false,
                Winner = empire.Name,
                Loser = "Dragon",
                Type = SiegeType,
                Attacker = dragonReport,
                Defender = armyReport,
                GoldStolen = goldStolen,
            };
        }
    }
    private static (int, int, int) DragonDamage()
    {
        // Dragon infantry damage 5d10 + 10
        // Dragon archer damage 3d10 + 10
        // Dragon knight damage 1d10 + 10
        return (
            Random.Shared.Next(0, 51) + 10,
            Random.Shared.Next(0, 31) + 10,
            Random.Shared.Next(0, 11) + 10
            );
    }
    #endregion

}
