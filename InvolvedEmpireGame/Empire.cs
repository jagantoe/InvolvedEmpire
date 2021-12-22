namespace InvolvedEmpireGame
{
    public class Empire
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Houses { get; set; }
        public int Gold { get; set; }
        public int Villagers { get; set; }
        public int Miners { get; set; }

        public Army Army { get; set; }

        public Empire(string name)
        {
            Name = name;
            Houses = 5;
            Gold = 10;
            Villagers = 10;
            Miners = 0;
            Army = new Army();
        }

        public void NewDay()
        {
            Villagers += Houses;
            Gold += Miners;
        }

        public PriceList GetPriceList()
        {
            return PriceResolver.GetPriceList(this);
        }

        public Transaction BuyHouse(int amount)
        {
            var cost = PriceResolver.CalculateTotalHousePrice(Houses, amount);
            if (Gold > cost)
            {
                Houses += amount;
                Gold -= cost;
                return new Transaction()
                {
                    CurrentState = this,
                    Success = true
                };
            }
            else
            {
                return new Transaction()
                {
                    CurrentState = this,
                    Success = false,
                    Exception = "Not enough gold!"
                };
            }
        }

        public Transaction EmployMiners(int amount)
        {
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
            else
            {
                return new Transaction()
                {
                    CurrentState = this,
                    Success = false,
                    Exception = "Not enough villagers!"
                };
            }
        }

        public Transaction TrainFootsoldiers(int amount)
        {
            var cost = PriceResolver.CalculateTotalFootSoldierPrice(Army.FootSoldiers, amount);
            if (Gold > cost && Villagers > amount)
            {
                Army.FootSoldiers += amount;
                Villagers -= amount;
                Gold -= cost;
                return new Transaction()
                {
                    CurrentState = this,
                    Success = true
                };
            }
            else
            {
                return new Transaction()
                {
                    CurrentState = this,
                    Success = false,
                    Exception = $"Not enough {(Gold < cost && Villagers < amount ? "gold and villagers" : Gold < cost ? "gold" : "villagers")}!"
                };
            }
        }

        public Transaction TrainKnights(int amount)
        {
            var cost = PriceResolver.CalculateTotalKnightPrice(Army.Knights, amount);
            if (Gold > cost && Villagers > amount)
            {
                Army.Knights += amount;
                Villagers -= amount;
                Gold -= cost;
                return new Transaction()
                {
                    CurrentState = this,
                    Success = true
                };
            }
            else
            {
                return new Transaction()
                {
                    CurrentState = this,
                    Success = false,
                    Exception = $"Not enough {(Gold < cost && Villagers < amount ? "gold and villagers": Gold < cost ? "gold": "villagers")}!"
                };
            }
        }
    
        public ArmyReport ArmyLosses(int losses)
        {
            if (losses == 0) return new ArmyReport(Name);
            var report = Army.ApplyLosses(losses);
            report.Name = Name;
            return report;
        }

        public BattleReport Attack(Empire? target)
        {
            return BattleResolver.ResolveBattle(this, target);
        }
    }
}