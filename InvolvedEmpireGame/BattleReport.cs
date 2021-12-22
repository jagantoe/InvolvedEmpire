namespace InvolvedEmpireGame
{
    public class BattleReport
    {
        public DateTime DateTime { get; set; }
        public bool AttackSuccessful { get; set; }
        public string Winner { get; set; }
        public string Loser { get; set; }
        public ArmyReport? Attacker { get; set; }
        public ArmyReport? Defender { get; set; }
        public int GoldStolen { get; set; }
        public int MinersStolen { get; set; }
        public int HousesBurnedDown { get; set; }
        
        public BattleReport()
        {
            DateTime = DateTime.Now;
        }
    }
}
