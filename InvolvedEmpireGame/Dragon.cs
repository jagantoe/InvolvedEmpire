namespace InvolvedEmpireGame;

public class Dragon
{
    public static int Id = -666;
    public bool Alive => CurrentHealth > 0;
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int GoldHoard { get; set; }
    public int CurrentTarget { get; set; }
    public string? CurrentTargetName { get; set; }
    public List<Empire> Targets = new List<Empire>();

    public Dragon()
    {

    }
    public Dragon(int hp)
    {
        CurrentHealth = hp;
        MaxHealth = hp;
    }

    public void SetTargets(List<Empire> empires)
    {
        Targets = empires;
        NextTarget();
    }
    public void NextTarget()
    {
        if (Targets.Count == 0) return;
        var target = Targets[0];
        CurrentTarget = target.Id;
        CurrentTargetName = target.Name;
        Targets.RemoveAt(0);
    }

    public ArmyReport TakeDamage(int damage)
    {
        var armyReport = new ArmyReport(CurrentHealth);
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        armyReport.SetDragonAfter(CurrentHealth, damage);
        return armyReport;
    }

    public void AddGold(int gold)
    {
        GoldHoard += gold;
    }

    public void ResetHoard()
    {
        GoldHoard = 0;
    }

    internal void Dead()
    {
        GoldHoard = 0;
        CurrentTarget = 0;
        CurrentTargetName = null;
        Targets = null;
    }
}
