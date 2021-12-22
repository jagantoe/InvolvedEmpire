namespace InvolvedEmpireRemastered.UserInterfaces
{
    public interface IEmpireClient
    {
        public Task<Empire> GetMyEmpire(CancellationToken token);
        public Task<IEnumerable<Empire>> GetAllEmpires(CancellationToken token);
        public Task<PriceList> GetPriceList(CancellationToken token);
        public Task<Transaction> BuyHouse(int amount, CancellationToken token);
        public Task<Transaction> EmployMiners(int amount, CancellationToken token);
        public Task<Transaction> TrainFootSoldiers(int amount, CancellationToken token);
        public Task<Transaction> TrainKnights(int amount, CancellationToken token);
        public Task<BattleReport> Attack(int targetId, CancellationToken token);
    }
}
