namespace InvolvedEmpireGame;

public class ArmyReport
{
    // Empire name or Dragon
    public string Name { get; set; }

    // Army before
    public int Infantry { get; set; }
    public int Archers { get; set; }
    public int Knights { get; set; }
    // Army after
    public int InfantryLosses { get; set; }
    public int ArchersLosses { get; set; }
    public int KnightsLosses { get; set; }

    // Dragon
    public int DragonHPBefore { get; set; }
    public int DragonHPAfter { get; set; }
    public int DragonHPLost { get; set; }

    public ArmyReport(string name, int infantry, int archers, int knights)
    {
        Name = name;
        Infantry = infantry;
        Archers = archers;
        Knights = knights;
    }
    public void SetArmyAfter(int infantryLosses, int archerLosses, int knightLosses)
    {
        InfantryLosses = infantryLosses > Infantry ? Infantry : infantryLosses;
        ArchersLosses = archerLosses > Archers ? Archers : archerLosses;
        KnightsLosses = knightLosses > Knights ? Knights : knightLosses;
    }

    public ArmyReport(int dragonHPBefore)
    {
        Name = "Dragon";
        DragonHPBefore = dragonHPBefore;
    }
    public void SetDragonAfter(int dragonHPAfter, int dragonHPLost)
    {
        DragonHPAfter = dragonHPAfter;
        DragonHPLost = dragonHPLost;
    }
}
