namespace InvolvedEmpireRemastered.DataService
{
    public class EmpireService: IEmpireService
    {
        private readonly EmpireDatabaseService _empireDatabaseService;
        private readonly EmpireCache _empireCache;

        public EmpireService(EmpireDatabaseService empireDatabaseService, EmpireCache empireCache)
        {
            _empireDatabaseService = empireDatabaseService;
            _empireCache = empireCache;
        }

        public async Task<Empire> GetEmpire(int id, CancellationToken token)
        {
            var empire = _empireCache[id];
            if (empire == null)
            {
                var user = await _empireDatabaseService.GetUser(id, token);
                _empireCache.AddEmpire(user);
                empire = _empireCache[id];
            }
            return empire;
        }

        public async Task<IEnumerable<Empire>> GetAllEmpires(CancellationToken token)
        {
            return _empireCache.GetAllEmpires();
        }

        public async Task<PriceList> GetPriceList(int id, CancellationToken token)
        {
            var empire = await GetEmpire(id, token);
            return empire.GetPriceList();
        }

        public async Task<Transaction> BuyHouse(int id, int amount, CancellationToken token)
        {
            var empire = await GetEmpire(id, token);
            var transaction = empire.BuyHouse(amount);
            return transaction;
        }

        public async Task<Transaction> EmployMiners(int id, int amount, CancellationToken token)
        {
            var empire = await GetEmpire(id, token);
            var transaction = empire.EmployMiners(amount);
            return transaction;
        }

        public async Task<Transaction> TrainFootSoldiers(int id, int amount, CancellationToken token)
        {
            var empire = await GetEmpire(id, token);
            var transaction = empire.TrainFootsoldiers(amount);
            return transaction;
        }

        public async Task<Transaction> TrainKnights(int id, int amount, CancellationToken token)
        {
            var empire = await GetEmpire(id, token);
            var transaction = empire.TrainKnights(amount);
            return transaction;
        }

        public async Task<BattleReport> Attack(int id, int targetId, CancellationToken token)
        {
            var empire = await GetEmpire(id, token);
            var target = _empireCache[targetId];
            return empire.Attack(target);
        }

        // Admin
        public async Task LoadEmpires(CancellationToken token)
        {
            var users = await _empireDatabaseService.GetAllUsers(token);
            _empireCache.LoadEmpires(users);
        }

        public async Task<Dictionary<int, Empire>> CalculateNewDay(CancellationToken token)
        {
            var users = await _empireDatabaseService.GetAllUsers(token);
            foreach (var user in users)
            {
                var empire = await GetEmpire(user.Id, token);
                empire.NewDay();
                user.SetEmpire(empire);
            }
            await _empireDatabaseService.SaveEmpires(users, token);
            return _empireCache.GetAllUserEmpires();
        }
    }
}
