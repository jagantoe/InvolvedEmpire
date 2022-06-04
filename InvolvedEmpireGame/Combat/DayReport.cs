namespace InvolvedEmpireGame;

public class DayReport
{
    public int Day { get; set; }
    public List<BattleReport> Battles { get; set; }
    public DayReport(int day)
    {
        Day = day;
        Battles = new List<BattleReport>();
    }
    public void AddBattle(BattleReport battleReport)
    {
        Battles.Add(battleReport);
    }
}
