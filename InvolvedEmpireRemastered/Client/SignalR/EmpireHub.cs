namespace InvolvedEmpireRemastered.UserInterfaces.SignalR
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

            return base.OnConnectedAsync();
        }

        public async Task<Empire> GetMyEmpire()
        {
            return await _empireService.GetEmpire(UserId, CancellationToken.None);
        }

        public async Task<IEnumerable<Empire>> GetAllEmpires()
        {
            return await _empireService.GetAllEmpires(CancellationToken.None);
        }

        public async Task<PriceList> GetPriceList()
        {
            return await _empireService.GetPriceList(UserId, CancellationToken.None);
        }

        public async Task<Transaction> BuyHouse(int amount)
        {
            return await _empireService.BuyHouse(UserId, amount, CancellationToken.None);
        }

        public async Task<Transaction> EmployMiners(int amount)
        {
            return await _empireService.EmployMiners(UserId, amount, CancellationToken.None);
        }

        public async Task<Transaction> TrainFootSoldiers(int amount)
        {
            return await _empireService.TrainFootSoldiers(UserId, amount, CancellationToken.None);
        }

        public async Task<Transaction> TrainKnights(int amount)
        {
            return await _empireService.TrainKnights(UserId, amount, CancellationToken.None);
        }

        public async Task<BattleReport> Attack(int targetId)
        {
            return await _empireService.Attack(UserId, targetId, CancellationToken.None);
        }
    }
}
