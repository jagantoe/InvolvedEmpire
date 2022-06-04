namespace InvolvedEmpireGame;

public class Structure
{
    public static List<Structure> Structures = SetupStructures();

    private static List<Structure> SetupStructures()
    {
        var structures = new List<Structure>();

        structures.Add(new()
        {
            Name = "Smelter",
            Description = "Gold gained from mining is doubled.",
            Cost = 5000
        });
        structures.Add(new()
        {
            Name = "Temple",
            Description = "If you have no combat units your gold income is doubled.",
            Cost = 2500
        });
        structures.Add(new()
        {
            Name = "Monument",
            Description = "Villagers gained each day is doubled.",
            Cost = 15000
        });
        structures.Add(new()
        {
            Name = "Townhall",
            Description = "Gain 5 gold for every house you have.",
            Cost = 2000
        });
        structures.Add(new()
        {
            Name = "Vault",
            Description = "Keep your gold safe from attackers. The amount equals 500 or 20% of your gold, whichever number is higher.",
            Cost = 2000
        });
        structures.Add(new()
        {
            Name = "Dragon Tower",
            Description = "Ward off the dragon.",
            Cost = 3000
        });
        structures.Add(new()
        {
            Name = "Arena",
            Description = "If you attacked someone the previous day. You gain 10 villagers or 200 gold.",
            Cost = 3000
        });
        structures.Add(new()
        {
            Name = "Spy Hideout",
            Description = "View the current state of other empires.",
            Cost = 1000
        });
        structures.Add(new()
        {
            Name = "Infirmary",
            Description = "Combat losses are limited.",
            Cost = 2000
        });
        structures.Add(new()
        {
            Name = "Barracks",
            Description = "Gain 1 infantry at the start of each day.",
            Cost = 1500
        });
        structures.Add(new()
        {
            Name = "Archery Range",
            Description = "Gain 1 archer at the start of each day.",
            Cost = 3000
        });
        structures.Add(new()
        {
            Name = "Stable",
            Description = "Gain 1 knight at the start of each day.",
            Cost = 5000
        });

        return structures;
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
}
