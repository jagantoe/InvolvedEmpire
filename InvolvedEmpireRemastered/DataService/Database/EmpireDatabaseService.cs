namespace InvolvedEmpireRemastered.DataService.Database
{
    public class EmpireDatabaseService
    {
        private readonly EmpireContext _context;

        public EmpireDatabaseService(EmpireContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUser(int id, CancellationToken token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, token);
        }

        public async Task<List<User>> GetAllUsers(CancellationToken token)
        {
            return await _context.Users.ToListAsync(token);
        }

        public async Task<User> RegisterUser(string name, string password, CancellationToken token)
        {
            var user = new User(name, password);
            await _context.Users.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
            return user;
        }

        public async Task SaveEmpires(IEnumerable<User> users, CancellationToken token)
        {
            _context.Users.UpdateRange(users);
            await _context.SaveChangesAsync(token);
        }
    }
}
