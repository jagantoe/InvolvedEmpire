namespace InvolvedEmpireRemastered.UserInterfaces.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class UserController: Controller
    {
        private readonly TokenProvider _tokenProvider;
        private readonly EmpireContext _context;

        public UserController(TokenProvider tokenProvider, EmpireContext context)
        {
            _tokenProvider = tokenProvider;
            _context = context;
        }

        [HttpGet]
        [Route("Token")]
        public async Task<IActionResult> GetTokenAsync(string name, string password, CancellationToken token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name && u.Password == password, token);
            if (user != null)
            {
                var userToken = _tokenProvider.GenerateToken(user.Id);
                return Ok(userToken);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(string name, string password, string adminPassword, CancellationToken token)
        {
            if (adminPassword == "crocodile")
            {
                var user = new User(name, password);
                await _context.Users.AddAsync(user, token);
                await _context.SaveChangesAsync(token);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
