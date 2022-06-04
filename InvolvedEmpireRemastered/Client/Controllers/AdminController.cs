namespace InvolvedEmpireRemastered.UserInterfaces.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly TokenProvider _tokenProvider;
        private readonly EmpireContext _context;
        private readonly IEmpireService _empireService;
        private readonly string AdminPassword = "*** secret admin password***";

        public AdminController(TokenProvider tokenProvider, EmpireContext context, IEmpireService empireService)
        {
            _tokenProvider = tokenProvider;
            _context = context;
            _empireService = empireService;
        }

        [HttpGet]
        [Route("Token")]
        public async Task<IActionResult> GetTokenAsync(string name, string password, CancellationToken token)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Name == name && u.Password == password, token);
            if (user == null) return BadRequest();
            var userToken = _tokenProvider.GenerateToken(user.Id);
            return Ok(userToken);
        }

        public record UserParams(string Name, string Password);
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromQuery] string adminPassword, UserParams userParams, CancellationToken token)
        {
            if (adminPassword == AdminPassword)
            {
                var user = new User(userParams.Name, userParams.Password);
                await _context.User.AddAsync(user, token);
                await _context.SaveChangesAsync(token);
                return Ok();
            }
            return Unauthorized();
        }
        public record GameParams(string Name, int TimeBetweenDays, int DragonHP);
        [HttpPost]
        [Route("AddGame")]
        public async Task<IActionResult> AddGame([FromQuery] string adminPassword, [FromBody] GameParams gameParams, CancellationToken token)
        {
            if (adminPassword == AdminPassword)
            {
                var gamestate = new GameState(gameParams.Name, gameParams.TimeBetweenDays, gameParams.DragonHP);
                var game = new Game(gameParams.Name, gamestate);
                var x = JsonSerializer.Serialize(gamestate);
                _context.Game.Add(game);
                await _context.SaveChangesAsync(token);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("GetGameState")]
        public async Task<IActionResult> GetGameState([FromQuery] string adminPassword, CancellationToken token)
        {
            if (adminPassword == AdminPassword)
            {
                var gamestate = _empireService.GetState();
                return Ok(gamestate);
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("LoadGame")]
        public async Task<IActionResult> LoadGame([FromQuery] string adminPassword, [FromQuery] int id, CancellationToken token)
        {
            if (adminPassword == AdminPassword)
            {
                await _empireService.LoadGame(id, token);
                return Ok();
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("GameStatus")]
        public async Task<IActionResult> GameStatus([FromQuery] string adminPassword, [FromQuery] bool status, CancellationToken token)
        {
            if (adminPassword == AdminPassword)
            {
                _empireService.SetStatus(status);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
