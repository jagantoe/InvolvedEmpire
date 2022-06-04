namespace InvolvedEmpirePlayerClient.Models;

public class Empire
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Houses { get; set; }
    public int Gold { get; set; }
    public int Villagers { get; set; }
    public int Miners { get; set; }

    public Army Army { get; set; }
    public EmpireStructures Structures { get; set; }
}
