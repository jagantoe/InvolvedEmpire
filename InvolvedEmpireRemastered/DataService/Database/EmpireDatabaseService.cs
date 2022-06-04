namespace InvolvedEmpireRemastered.DataService.Database
{
    public class EmpireDatabaseService
    {
        private readonly EmpireContext _context;

        public EmpireDatabaseService(EmpireContext context)
        {
            _context = context;
        }
        public async Task<Game?> GetGame(int id, CancellationToken token)
        {
            return await _context.Game.Include(x => x.UserGames).FirstOrDefaultAsync(u => u.Id == id, token);
        }
        public async Task<User> GetUser(int userId, CancellationToken token)
        {
            return (await _context.User.FirstOrDefaultAsync(u => u.Id == userId, token))!;
        }
        public async Task<UserGame?> GetUserGame(int userId, int gameId, CancellationToken token)
        {
            return await _context.UserGame.FirstOrDefaultAsync(u => u.GameId == gameId && u.UserId == userId, token);
        }

        public async Task<List<UserGame>> GetAllUsers(int id, CancellationToken token)
        {
            return await _context.UserGame.Where(x => x.GameId == id).ToListAsync(token);
        }

        public async Task<User> RegisterUser(string name, string password, CancellationToken token)
        {
            var user = new User(name, password);
            await _context.User.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
            return user;
        }

        public async Task SaveEmpire(UserGame user, CancellationToken token)
        {
            _context.UserGame.UpdateRange(user);
            await _context.SaveChangesAsync(token);
        }
        public async Task SaveEmpires(IEnumerable<UserGame> users, CancellationToken token)
        {
            _context.UserGame.UpdateRange(users);
            await _context.SaveChangesAsync(token);
        }
        public async Task SaveGame(GameState gameState, CancellationToken token)
        {
            var game = await _context.Game.Include(x => x.UserGames).FirstOrDefaultAsync(u => u.Id == gameState.Id, token);
            game.SerializedGame = JsonSerializer.Serialize(gameState);
            await _context.SaveChangesAsync(token);
        }
    }
}
