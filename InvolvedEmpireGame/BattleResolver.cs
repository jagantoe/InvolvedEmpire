namespace InvolvedEmpireGame
{
    internal static class BattleResolver
    {
        private static readonly Random Random = new Random();
        private static double LosesRatio = 0.4;
        private static double StealGoldRatio = 0.8;
        private static double StealMinersRatio = 0.1;
        private static double HouseBurnedRatio = 0.05;
        private static int HouseBurnLimit = 5;

        internal static BattleReport ResolveBattle(Empire attacker, Empire? defender)
        {
            if (defender == null)
            {
                return new BattleReport()
                {
                    Winner = "None",
                    Loser = "None",
                };
            }

            var attackerStrength = attacker.Army.ArmyStrength;
            var defenderStrength = defender.Army.ArmyStrength;

            if (attackerStrength <= 0)
            {
                return new BattleReport()
                {
                    Winner = "None",
                    Loser = "None",
                    Attacker = new ArmyReport(attacker.Name),
                    Defender = new ArmyReport(defender.Name)
                };
            }

            while (attackerStrength > 0 && defenderStrength > 0)
            {
                var attackRoll = Random.Next(attackerStrength) + 1;
                var defenseRoll = Random.Next(defenderStrength) + 1;

                attackerStrength -= defenseRoll;
                defenderStrength -= attackRoll;
            }

            var report = new BattleReport();

            report.Attacker = ApplyLosses(attacker, attackerStrength);
            report.Defender = ApplyLosses(defender, defenderStrength);

            if (attackerStrength > defenderStrength)
            {
                report.AttackSuccessful = true;
                report.Winner = attacker.Name;
                report.GoldStolen = StealGold(attacker, defender);
                report.MinersStolen = StealMiners(attacker, defender);
                report.HousesBurnedDown = BurnHouses(defender);
            }
            else
            {
                report.Winner = defender.Name;
            }
            return report;

            ArmyReport ApplyLosses(Empire empire, int remainingStrength)
            {
                var losses = (int)Math.Ceiling((empire.Army.ArmyStrength - remainingStrength) * LosesRatio);
                
                return empire.ArmyLosses(losses);
            }

            int StealGold(Empire attacker, Empire defender)
            {
                var maxGoldStolen = (int)Math.Ceiling(defender.Gold * StealGoldRatio);
                var goldStolen = Random.Next(maxGoldStolen);
                goldStolen = goldStolen > defender.Gold  ? defender.Gold: goldStolen;
                defender.Gold -= goldStolen;
                attacker.Gold += goldStolen;
                return goldStolen;
            }

            int StealMiners(Empire attacker, Empire defender)
            {
                var maxMinersStolen = (int)Math.Ceiling(defender.Miners * StealMinersRatio);
                var minersStolen = Random.Next(maxMinersStolen);
                minersStolen = minersStolen > defender.Miners ? defender.Miners : minersStolen;
                defender.Miners -= minersStolen;
                attacker.Miners += minersStolen;
                return minersStolen;
            }

            int BurnHouses(Empire defender)
            {
                var maxHousesBurned = (int)Math.Ceiling(defender.Houses * HouseBurnedRatio);
                var housesBurnedDown = Random.Next(maxHousesBurned);
                if (defender.Houses - housesBurnedDown > HouseBurnLimit)
                {
                    defender.Houses -= housesBurnedDown;
                    return housesBurnedDown;
                }
                return 0;
            }
        }
    }
}
