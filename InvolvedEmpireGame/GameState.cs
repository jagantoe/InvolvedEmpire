namespace InvolvedEmpireGame;

public class GameState
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public string Name { get; set; }
    public int Day { get; set; }
    public int TimeBetweenDays { get; set; }
    public Dragon Dragon { get; set; }

    public Dictionary<int, Empire> Empires { get; set; } = new Dictionary<int, Empire>();
    public Dictionary<int, int> AttackQueue { get; set; } = new Dictionary<int, int>();
    public Empire? this[int i] => Empires.GetValueOrDefault(i);

    public GameState()
    {
    }
    public GameState(string name, int timeBetweenDays, int dragonHP)
    {
        Name = name;
        TimeBetweenDays = timeBetweenDays;
        Dragon = new Dragon(dragonHP);
    }

    public void Reset()
    {
        Empires = new Dictionary<int, Empire>();
    }

    public void NewDay()
    {
        Day++;
    }

    public void AddEmpire(Empire empire)
    {
        if (!Empires.ContainsKey(empire.Id))
        {
            Empires.Add(empire.Id, empire);
        }
    }

    public Dictionary<int, Empire> GetEmpires()
    {
        return Empires;
    }
    public IEnumerable<EmpireReport> GetOtherEmpires(int id)
    {
        var spyHideout = this[id].Structures.SpyHideout;

        return Empires.Values.Where(x => x.Id != id).Select(x => new EmpireReport()
        {
            Id = x.Id,
            Name = x.Name,
            Empire = spyHideout ? x : null
        });
    }

    public void Attack(int id, int targetId)
    {
        if (this[id].Army.None || id == targetId) return;
        AttackQueue.Remove(id);
        if (Empires.ContainsKey(targetId))
        {
            AttackQueue.Add(id, targetId);
        }
    }
    public void AttackDragon(int id)
    {
        if (this[id].Army.None) return;
        AttackQueue.Remove(id);
        if (Dragon.Alive)
        {
            AttackQueue.Add(id, Dragon.Id);
        }
    }

    public DayReport HandleBattles()
    {
        var dayReport = new DayReport(Day);

        // Create queue
        var battleQueue = CreateBattleQueue();

        foreach (var battle in battleQueue)
        {
            if (battle.Attacker == Dragon.Id)
            {
                // Dragon siege on city
                if (Dragon.Alive)
                {
                    dayReport.AddBattle(BattleResolver.Battle(Dragon, this[battle.Defender]));
                }
                continue;
            }
            if (battle.Defender == Dragon.Id)
            {
                // Empire siege on dragon
                if (Dragon.Alive)
                {
                    dayReport.AddBattle(BattleResolver.Battle(this[battle.Attacker], Dragon, battle.Field));
                }
                continue;
            }
            var attacker = this[battle.Attacker];
            var defender = this[battle.Defender];
            dayReport.AddBattle(BattleResolver.Battle(attacker, defender, battle.Field));
        }

        // Refill Dragon targets if none left
        if (Dragon.Alive && Dragon.Targets.Count == 0)
        {
            this.PlanDragonTargets();
        }
        // Clear queue
        AttackQueue.Clear();
        return dayReport;
        List<Battle> CreateBattleQueue()
        {
            // Add dragon to queue if alive and player doesn't have dragon tower
            if (Dragon.Alive && Dragon.CurrentTarget != 0)
            {
                if (this[Dragon.CurrentTarget]!.Structures.DragonTower == false)
                {
                    AttackQueue.Add(Dragon.Id, Dragon.CurrentTarget);
                }
                Dragon.NextTarget();
            }
            var battles = new List<Battle>();
            var queue = AttackQueue.Reverse().ToList();
            for (int i = queue.Count - 1; i >= 0; i--)
            {
                var attacker = queue[i].Key;
                var target = queue[i].Value;
                if (target == Dragon.Id)
                {
                    // Dragon Attack
                    if (attacker == Dragon.CurrentTarget)
                    {
                        queue.Remove(queue.First(x => x.Key == target));
                        i--;
                        battles.Add(new Battle(attacker, target, true));
                    }
                    else
                    {
                        battles.Add(new Battle(attacker, target, false));
                    }
                }
                else
                {
                    if (queue.Any(x => x.Key == target && x.Value == attacker))
                    {
                        queue.Remove(queue.First(x => x.Key == target));
                        i--;
                        battles.Add(new Battle(attacker, target, true));
                    }
                    else
                    {
                        battles.Add(new Battle(attacker, target, false));
                    }
                }
            }
            return battles;
        }
    }
    private void PlanDragonTargets()
    {
        if (!Dragon.Alive) return;
        var targets = Empires.Values.Where(x => x.Structures.DragonTower == false).ToList();
        int n = targets.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Shared.Next(n + 1);
            var value = targets[k];
            targets[k] = targets[n];
            targets[n] = value;
        }
        Dragon.SetTargets(targets);
    }
}
