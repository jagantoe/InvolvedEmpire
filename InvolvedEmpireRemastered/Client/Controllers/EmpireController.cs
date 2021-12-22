namespace InvolvedEmpireRemastered.Client.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EmpireController : Controller, IEmpireClient
    {
        private readonly IEmpireService _empireService;
        private int UserId => User.GetTeamId();

        public EmpireController(IEmpireService empireService)
        {
            _empireService = empireService;
        }

        [HttpGet]
        [Route("Empire")]
        public async Task<Empire> GetMyEmpire(CancellationToken token)
        {
            return await _empireService.GetEmpire(UserId, token);
        }

        [HttpGet]
        [Route("AllEmpires")]
        public async Task<IEnumerable<Empire>> GetAllEmpires(CancellationToken token)
        {
            return await _empireService.GetAllEmpires(token);
        }

        [HttpGet]
        [Route("PriceList")]
        public async Task<PriceList> GetPriceList(CancellationToken token)
        {
            return await _empireService.GetPriceList(UserId, token);
        }

        [HttpGet]
        [Route("BuyHouse/{amount}")]
        public async Task<Transaction> BuyHouse([FromRoute] int amount, CancellationToken token)
        {
            return await _empireService.BuyHouse(UserId, amount, token);
        }

        [HttpGet]
        [Route("EmployMiners/{amount}")]
        public async Task<Transaction> EmployMiners([FromRoute] int amount, CancellationToken token)
        {
            return await _empireService.EmployMiners(UserId, amount, token);
        }

        [HttpGet]
        [Route("TrainFootSoldier/{amount}")]
        public async Task<Transaction> TrainFootSoldiers([FromRoute] int amount, CancellationToken token)
        {
            return await _empireService.TrainFootSoldiers(UserId, amount, token);
        }

        [HttpGet]
        [Route("TrainKnight/{amount}")]
        public async Task<Transaction> TrainKnights([FromRoute] int amount, CancellationToken token)
        {
            return await _empireService.TrainKnights(UserId, amount, token);
        }

        [HttpGet]
        [Route("Attack/{targetId}")]
        public async Task<BattleReport> Attack([FromRoute] int targetId, CancellationToken token)
        {
            return await _empireService.Attack(UserId, targetId, token);
        }
    }
}
