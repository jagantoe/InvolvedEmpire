namespace InvolvedEmpireRemastered.Authentication
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string SerializedEmpire { get; set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
            SetEmpire(new Empire(Name));
        }

        public void SetEmpire(Empire empire)
        {
            SerializedEmpire = JsonSerializer.Serialize(empire);
        }
    }

    public static class UserExtensions
    {
        public static int GetTeamId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.First();
            return Convert.ToInt32(claim.Value);
        }
    }
}
