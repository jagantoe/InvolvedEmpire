namespace InvolvedEmpireRemastered.DataService.Database
{
    public class EmpireContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public EmpireContext(DbContextOptions options) : base(options)
        {

        }
    }
}
