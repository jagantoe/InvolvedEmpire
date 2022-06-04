namespace InvolvedEmpireGame;

public struct Battle : IEquatable<Battle>
{
    public int Attacker;
    public int Defender;
    public bool Field;

    public Battle(int attacker, int defender, bool field)
    {
        Attacker = attacker;
        Defender = defender;
        Field = field;
    }

    public bool Equals(Battle other)
    {
        return Attacker == other.Attacker && Defender == other.Defender ||
            Attacker == other.Defender && Defender == other.Attacker;
    }
}
