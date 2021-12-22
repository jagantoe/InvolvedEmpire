namespace InvolvedEmpireGame
{
    public class ArmyReport
    {
        public string Name { get; set; }
        public int FootSoldiersLost { get; set; }
        public int KnightsLost { get; set; }

        public ArmyReport(string name)
        {
            Name = name;
        }

        public ArmyReport(int footSoldiersLost, int knightsLost)
        {
            FootSoldiersLost = footSoldiersLost;
            KnightsLost = knightsLost;
        }
    }
}