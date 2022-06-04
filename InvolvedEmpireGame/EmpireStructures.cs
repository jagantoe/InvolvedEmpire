namespace InvolvedEmpireGame;

public class EmpireStructures
{
    public bool Smelter { get; set; }
    public bool Temple { get; set; }
    public bool Monument { get; set; }
    public bool Townhall { get; set; }
    public bool Vault { get; set; }
    public bool DragonTower { get; set; }
    public bool Arena { get; set; }
    public bool SpyHideout { get; set; }
    public bool Infirmary { get; set; }
    public bool Barracks { get; set; }
    public bool ArcheryRange { get; set; }
    public bool Stable { get; set; }

    public bool AlreadyBought(string name)
    {
        switch (name)
        {
            case "Smelter":
                return Smelter;
            case "Temple":
                return Temple;
            case "Monument":
                return Monument;
            case "Townhall":
                return Townhall;
            case "Vault":
                return Vault;
            case "Dragon Tower":
                return DragonTower;
            case "Arena":
                return Arena;
            case "Spy Hideout":
                return SpyHideout;
            case "Infirmary":
                return Infirmary;
            case "Barracks":
                return Barracks;
            case "Archery Range":
                return ArcheryRange;
            case "Stable":
                return Stable;
        }
        return true;
    }
    public void BuildStructure(string name)
    {
        switch (name)
        {
            case "Smelter":
                Smelter = true;
                break;
            case "Temple":
                Temple = true;
                break;
            case "Monument":
                Monument = true;
                break;
            case "Townhall":
                Townhall = true;
                break;
            case "Vault":
                Vault = true;
                break;
            case "Dragon Tower":
                DragonTower = true;
                break;
            case "Arena":
                Arena = true;
                break;
            case "Spy Hideout":
                SpyHideout = true;
                break;
            case "Infirmary":
                Infirmary = true;
                break;
            case "Barracks":
                Barracks = true;
                break;
            case "Archery Range":
                ArcheryRange = true;
                break;
            case "Stable":
                Stable = true;
                break;
        }
    }
}
