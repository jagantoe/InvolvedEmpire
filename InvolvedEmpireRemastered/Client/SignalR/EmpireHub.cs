namespace InvolvedEmpireRemastered.Client.SignalR
{
    [Authorize]
    public class EmpireHub : Hub<IEmpireClient>
    {
        private readonly IEmpireService _empireService;
        private int UserId => Context.User.GetTeamId();

        public EmpireHub(IEmpireService empireService)
        {
            _empireService = empireService;
        }
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, UserId.ToString());
            return LoadEmpireOnConnect();
        }

        private async Task<Task> LoadEmpireOnConnect()
        {
            await _empireService.LoadEmpire(UserId, CancellationToken.None);
            return base.OnConnectedAsync();
        }

        public IEnumerable<Structure> GetStructures()
        {
            return _empireService.GetStructures();
        }
        public Dragon GetDragon()
        {
            return _empireService.GetDragon();
        }
        public async ValueTask<PriceList> GetPriceList()
        {
            return await _empireService.GetPriceList(UserId, CancellationToken.None);
        }
        public async ValueTask<IEnumerable<EmpireReport>> GetOtherEmpires()
        {
            return await _empireService.GetOtherEmpires(UserId, CancellationToken.None);
        }

        public async ValueTask<Transaction> BuyHouses(int amount)
        {
            return await _empireService.BuyHouses(UserId, amount, CancellationToken.None);
        }
        public async ValueTask<Transaction> EmployMiners(int amount)
        {
            return await _empireService.EmployMiners(UserId, amount, CancellationToken.None);
        }
        public async ValueTask<Transaction> TrainInfantry(int amount)
        {
            return await _empireService.BuyInfantry(UserId, amount, CancellationToken.None);
        }
        public async ValueTask<Transaction> TrainArchers(int amount)
        {
            return await _empireService.BuyArchers(UserId, amount, CancellationToken.None);
        }
        public async ValueTask<Transaction> TrainKnights(int amount)
        {
            return await _empireService.BuyKnights(UserId, amount, CancellationToken.None);
        }
        public async ValueTask<Transaction> BuyStructure(string name)
        {
            return await _empireService.BuyStructure(UserId, name, CancellationToken.None);
        }

        public async Task Attack(int targetId)
        {
            await _empireService.Attack(UserId, targetId, CancellationToken.None);
        }
        public async Task AttackDragon()
        {
            await _empireService.AttackDragon(UserId, CancellationToken.None);
        }
    }
}
