namespace InvolvedEmpireGame
{
    internal static class PriceResolver
    {
        internal static PriceList GetPriceList(Empire empire)
        {
            return new PriceList()
            {
                House = CalculateHousePrice(empire.Houses),
                FootSoldier = CalculateFootSoldierPrice(empire.Army.FootSoldiers),
                Knight = CalculateKnightPrice(empire.Army.Knights)
            };
        }

        public static int CalculateTotalHousePrice(int houses, int amount)
        {
            var cost = 0;
            for (int i = 0; i < amount; i++)
            {
                cost += CalculateHousePrice(houses + i);
            }
            return cost;
        }

        public static int CalculateTotalFootSoldierPrice(int footsoldiers, int amount)
        {
            var cost = 0;
            for (int i = 0; i < amount; i++)
            {
                cost += CalculateFootSoldierPrice(footsoldiers + i);
            }
            return cost;
        }

        public static int CalculateTotalKnightPrice(int knights, int amount)
        {
            var cost = 0;
            for (int i = 0; i < amount; i++)
            {
                cost += CalculateKnightPrice(knights + i);
            }
            return cost;
        }

        private static int CalculateHousePrice(int houses)
        {
            return (int)Math.Floor(Math.Ceiling(houses / 2.0) * Math.Pow(houses,2));
        }

        private static int CalculateFootSoldierPrice(int footsoldiers)
        {
            return 1 + (int)Math.Ceiling(footsoldiers / 10.0);
        }

        private static int CalculateKnightPrice(int knights)
        {
            return 5 + (int)(Math.Ceiling(knights / 10.0) * 5);
        }
    }
}
