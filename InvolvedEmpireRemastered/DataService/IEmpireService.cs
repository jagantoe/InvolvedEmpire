namespace InvolvedEmpireRemastered.DataService
{
    public interface IEmpireService
    {
        public Task<Empire> GetEmpire(int id, CancellationToken token);
        public Task<IEnumerable<Empire>> GetAllEmpires(CancellationToken token);
        public Task<PriceList> GetPriceList(int id, CancellationToken token);
        public Task<Transaction> BuyHouse(int id, int amount, CancellationToken token);
        public Task<Transaction> EmployMiners(int id, int amount, CancellationToken token);
        public Task<Transaction> TrainFootSoldiers(int id, int amount, CancellationToken token);
        public Task<Transaction> TrainKnights(int id, int amount, CancellationToken token);
        public Task<BattleReport> Attack(int id, int targetId, CancellationToken token);

        public Task LoadEmpires(CancellationToken token);
        public Task<Dictionary<int, Empire>> CalculateNewDay(CancellationToken token);
    }
}
