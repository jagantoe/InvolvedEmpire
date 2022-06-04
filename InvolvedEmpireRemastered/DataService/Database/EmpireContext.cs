namespace InvolvedEmpireRemastered.DataService.Database
{
    public class EmpireContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<UserGame> UserGame { get; set; }

        public EmpireContext(DbContextOptions options) : base(options)
        {

        }
    }
}
