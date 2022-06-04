namespace InvolvedEmpireGame;

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

    public Empire(int id, string name)
    {
        Id = id;
        Name = name;
        Houses = 5;
        Gold = 150;
        Villagers = 10;
        Miners = 0;
        Army = new Army();
        Structures = new EmpireStructures();
    }

    public void NewDay()
    {
        GoldGain();
        VillagersGain();
        ArmyGain();
        ArenaGain();

        void GoldGain()
        {
            var gold = Miners;
            if (Structures.Smelter)
            {
                gold *= 2;
            }
            if (Structures.Townhall)
            {
                gold += Houses * 5;
            }
            if (Structures.Temple && Army.None)
            {
                gold *= 2;
            }
            Gold += gold;
        }
        void VillagersGain()
        {
            var villagers = Houses;
            if (Structures.Monument)
            {
                villagers += Houses;
            }
            Villagers += villagers;
        }
        void ArmyGain()
        {
            if (Structures.Barracks)
            {
                Army.InfantryRecruited(1);
            }
            if (Structures.ArcheryRange)
            {
                Army.ArchersRecruited(1);
            }
            if (Structures.Stable)
            {
                Army.KnightsRecruited(1);
            }
        }
        void ArenaGain()
        {
            if (Structures.Arena)
            {
                var rnd = Random.Shared.Next(0, 2) == 0;
                if (rnd)
                {
                    Gold += 200;
                }
                else
                {
                    Villagers += 10;
                }
            }
        }
    }

    public void StealGold(int gold)
    {
        Gold -= gold;
        if (Gold < 0) Gold = 0;
    }
    public void AddGold(int gold)
    {
        Gold += gold;
    }

    public void StealMiners(int miners)
    {
        Miners -= miners;
        if (Miners < 0) Miners = 0;
    }
    public void AddMiners(int miners)
    {
        Miners += miners;
    }

    public ArmyReport ArmyLosses(int infantryLosses, int archerLosses, int knightLosses)
    {
        var armyReport = new ArmyReport(Name, Army.Infantry, Army.Archers, Army.Knights);
        if (Structures.Infirmary)
        {
            infantryLosses /= 2;
            archerLosses /= 2;
            knightLosses /= 2;
        }
        Army.InfantryLost(infantryLosses);
        Army.ArchersLost(archerLosses);
        Army.KnightsLost(knightLosses);
        armyReport.SetArmyAfter(infantryLosses, archerLosses, knightLosses);
        return armyReport;
    }

    public PriceList GetPriceList()
    {
        return PriceResolver.GetPriceList(this);
    }

    public Transaction BuyHouse(int amount)
    {
        if (amount == -1)
        {
            amount = PriceResolver.MaxBuy(Gold, Houses, PriceResolver.CalculateHousePrice);
        }
        if (amount == 0)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        if (amount < -1)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = "Invalid amount value!"
            };
        }

        var cost = PriceResolver.HousePrice(Houses, amount);
        if (Gold >= cost)
        {
            Houses += amount;
            Gold -= cost;
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        return new Transaction()
        {
            CurrentState = this,
            Success = false,
            Exception = $"Not enough gold! Available: {Gold} - Required: {cost} - Requested Houses: {amount}"
        };
    }
    public Transaction EmployMiners(int amount)
    {
        if (amount == 0)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        if (amount < -1)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = "Invalid amount value!"
            };
        }
        if (amount == -1)
        {
            amount = Villagers;
        }

        if (Villagers >= amount)
        {
            Miners += amount;
            Villagers -= amount;
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        return new Transaction()
        {
            CurrentState = this,
            Success = false,
            Exception = $"Not enough villagers! Available: {Villagers} - Requested: {amount}"
        };
    }
    public Transaction BuyInfantry(int amount)
    {
        if (amount == -1)
        {
            amount = PriceResolver.MaxBuy(Gold, Villagers, Army.Infantry, PriceResolver.CalculateInfantryPrice);
        }
        if (amount == 0)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        if (amount < -1)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = "Invalid amount value!"
            };
        }
        if (Villagers < amount)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = $"Not enough villagers! Available: {Villagers} - Requested: {amount}"
            };
        }

        var cost = PriceResolver.InfantryPrice(Army.Infantry, amount);
        if (Gold >= cost)
        {
            Army.InfantryRecruited(amount);
            Gold -= cost;
            Villagers -= amount;
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        return new Transaction()
        {
            CurrentState = this,
            Success = false,
            Exception = $"Not enough gold! Available: {Gold} - Required: {cost} - Requested Infantry: {amount}"
        };
    }
    public Transaction BuyArchers(int amount)
    {
        if (amount == -1)
        {
            amount = PriceResolver.MaxBuy(Gold, Villagers, Army.Archers, PriceResolver.CalculateArcherPrice);
        }
        if (amount == 0)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        if (amount < -1)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = "Invalid amount value!"
            };
        }
        if (Villagers < amount)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = $"Not enough villagers! Available: {Villagers} - Requested: {amount}"
            };
        }

        var cost = PriceResolver.ArcherPrice(Army.Archers, amount);
        if (Gold >= cost)
        {
            Army.ArchersRecruited(amount);
            Gold -= cost;
            Villagers -= amount;
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        return new Transaction()
        {
            CurrentState = this,
            Success = false,
            Exception = $"Not enough gold! Available: {Gold} - Required: {cost} - Requested Archers: {amount}"
        };
    }
    public Transaction BuyKnights(int amount)
    {
        if (amount == -1)
        {
            amount = PriceResolver.MaxBuy(Gold, Villagers, Army.Knights, PriceResolver.CalculateKnightPrice);
        }
        if (amount == 0)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        if (amount < -1)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = "Invalid amount value!"
            };
        }
        if (Villagers < amount)
        {
            return new Transaction()
            {
                CurrentState = this,
                Success = false,
                Exception = $"Not enough villagers! Available: {Villagers} - Requested: {amount}"
            };
        }

        var cost = PriceResolver.KnightPrice(Army.Knights, amount);
        if (Gold >= cost)
        {
            Army.KnightsRecruited(amount);
            Gold -= cost;
            Villagers -= amount;
            return new Transaction()
            {
                CurrentState = this,
                Success = true
            };
        }
        return new Transaction()
        {
            CurrentState = this,
            Success = false,
            Exception = $"Not enough gold! Available: {Gold} - Required: {cost} - Requested Knights: {amount}"
        };
    }
    public Transaction BuyStructure(string name)
    {
        var structure = Structure.Structures.FirstOrDefault(x => x.Name == name);
        if (structure == null)
        {
            return new Transaction()
            {
                Success = false,
                CurrentState = this,
                Exception = "Invalid structure name!"
            };
        }
        if (structure.Cost > Gold)
        {
            return new Transaction()
            {
                Success = false,
                CurrentState = this,
                Exception = $"Not enough gold for {structure.Name}! Available: {Gold} - Required: {structure.Cost}"
            };
        }
        var owned = Structures.AlreadyBought(structure.Name);
        if (!owned)
        {
            Structures.BuildStructure(structure.Name);
            Gold -= structure.Cost;
        }
        return new Transaction()
        {
            Success = true,
            CurrentState = this,
        };
    }
}
