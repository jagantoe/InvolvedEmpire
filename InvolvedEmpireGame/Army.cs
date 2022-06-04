namespace InvolvedEmpireGame;

public class Army
{
    public int Infantry { get; set; }
    public int Archers { get; set; }
    public int Knights { get; set; }
    public bool None => Infantry == 0 && Knights == 0 && Archers == 0;

    public void InfantryRecruited(int amount)
    {
        Infantry += amount;
    }
    public void InfantryLost(int amount)
    {
        Infantry -= amount;
        if (Infantry < 0)
        {
            Infantry = 0;
        }
    }

    public void ArchersRecruited(int amount)
    {
        Archers += amount;
    }
    public void ArchersLost(int amount)
    {
        Archers -= amount;
        if (Archers < 0)
        {
            Archers = 0;
        }
    }

    public void KnightsRecruited(int amount)
    {
        Knights += amount;
    }
    public void KnightsLost(int amount)
    {
        Knights -= amount;
        if (Knights < 0)
        {
            Knights = 0;
        }
    }
}
