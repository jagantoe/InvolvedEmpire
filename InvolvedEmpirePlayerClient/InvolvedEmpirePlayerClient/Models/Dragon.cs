namespace InvolvedEmpirePlayerClient.Models;

public class Dragon
{
    public bool Alive { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int GoldHoard { get; set; }
    public int CurrentTarget { get; set; }
    public string? CurrentTargetName { get; set; }
}
