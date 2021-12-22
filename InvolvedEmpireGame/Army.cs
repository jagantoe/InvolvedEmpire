namespace InvolvedEmpireGame
{
    public class Army
    {
        private static readonly Random Random = new Random();

        private static int FootSoldierStrength = 1;
        private static int KnightStrength = 4;

        public int FootSoldiers { get; set; }
        public int Knights { get; set; }

        public int ArmyStrength => (Knights * KnightStrength) + (FootSoldiers * FootSoldierStrength);

        public ArmyReport ApplyLosses(int losses)
        {
            var knightsBefore = Knights;
            var footSoldiersBefore = FootSoldiers;
            if (ArmyStrength == losses)
            {
                WipedOut();
            }
            else
            {
                if (losses < KnightStrength)
                {
                    FootSoldiers -= losses;
                }
                else
                {
                    while (losses > 0)
                    {
                        if (Knights > 0 && losses > KnightStrength)
                        {
                            Knights--;
                            losses -= KnightStrength;
                        }
                        
                        var footSoldiersLost = Random.Next(KnightStrength, KnightStrength * 2);
                        footSoldiersLost = footSoldiersLost > losses ? losses : footSoldiersLost;
                        if (FootSoldiers > 0 && losses > 0)
                        {
                            footSoldiersLost -= losses;
                            losses -= footSoldiersLost;
                        }
                    }
                }
            }
            return new ArmyReport(footSoldiersBefore - FootSoldiers, knightsBefore - Knights);
            
            void WipedOut()
            {
                FootSoldiers = 0;
                Knights = 0;
            }
        }
    }
}
