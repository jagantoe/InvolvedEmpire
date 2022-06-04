namespace InvolvedEmpireRemastered.GameManagement
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<UserGame> UserGames { get; set; }
        public User()
        {

        }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
