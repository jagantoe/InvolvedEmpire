namespace InvolvedEmpireGame;

public static class PriceResolver
{
    public static PriceList GetPriceList(Empire empire)
    {
        return new()
        {
            House = HousePrice(empire.Houses, 1),
            Infantry = InfantryPrice(empire.Army.Infantry, 1),
            Archer = ArcherPrice(empire.Army.Archers, 1),
            Knight = KnightPrice(empire.Army.Knights, 1)
        };
    }

    public static int MaxBuy(int gold, int current, Func<int, int, double> func)
    {
        var amount = 0;
        var totalCost = 0.0;
        while (gold >= totalCost)
        {
            totalCost += func(current + amount, 1);
            amount++;
        }
        return amount - 1;
    }
    public static int MaxBuy(int gold, int villagers, int current, Func<int, int, double> func)
    {
        var amount = 0;
        var totalCost = 0.0;
        while (gold >= totalCost && villagers > amount)
        {
            totalCost += func(current + amount, 1);
            amount++;
        }
        return amount - 1;
    }

    public static int HousePrice(int current, int amount) => (int)CalculateHousePrice(current, amount);
    public static double CalculateHousePrice(int current, int amount)
    {
        return CalculateTotalHousePrice(current + amount) - CalculateTotalHousePrice(current);
    }
    public static int InfantryPrice(int current, int amount) => (int)CalculateInfantryPrice(current, amount);
    public static double CalculateInfantryPrice(int current, int amount)
    {
        return CalculateTotalInfantryPrice(current + amount) - CalculateTotalInfantryPrice(current);
    }
    public static int ArcherPrice(int current, int amount) => (int)CalculateArcherPrice(current, amount);
    public static double CalculateArcherPrice(int current, int amount)
    {
        return CalculateTotalArcherPrice(current + amount) - CalculateTotalArcherPrice(current);
    }
    public static int KnightPrice(int current, int amount) => (int)CalculateKnightPrice(current, amount);
    public static double CalculateKnightPrice(int current, int amount)
    {
        return CalculateTotalKnightPrice(current + amount) - CalculateTotalKnightPrice(current);
    }

    #region house
    private static int houseBasePrice = 30;
    private static int houseIncl = 20;
    private static double CalculateTotalHousePrice(int amount)
    {
        return (houseIncl / 2.0) * amount * amount + (houseBasePrice * amount) + amount * (houseIncl / 2.0);
    }
    #endregion

    #region infantry
    private static int infantryBasePrice = 5;
    private static double infantryIncl = 0.2; // Non natural number causes problems with bulk buy
    private static double CalculateTotalInfantryPrice(double amount)
    {
        return (infantryIncl / 2.0) * amount * amount + (infantryBasePrice * amount) + amount * (infantryIncl / 2.0);
    }
    #endregion

    #region archer
    private static int archerBasePrice = 15;
    private static double archerIncl = 0.5; // Non natural number causes problems with bulk buy
    private static double CalculateTotalArcherPrice(int amount)
    {
        return (archerIncl / 2.0) * amount * amount + (archerBasePrice * amount) + amount * (archerIncl / 2.0);
    }
    #endregion

    #region knight
    private static int knightBasePrice = 30;
    private static int knightIncl = 2;
    private static double CalculateTotalKnightPrice(int amount)
    {
        return (knightIncl / 2.0) * amount * amount + (knightBasePrice * amount) + amount * (knightIncl / 2.0);
    }
    #endregion
}
