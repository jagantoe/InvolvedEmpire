namespace InvolvedEmpireRemastered.GameManagement
{
    public class UserGame
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public string SerializedEmpire { get; set; }
        public UserGame()
        {

        }
        public UserGame(int userId, int gameId, Empire empire)
        {
            UserId = userId;
            GameId = gameId;
            SerializedEmpire = JsonSerializer.Serialize(empire);
        }

        public void SetEmpire(Empire empire)
        {
            SerializedEmpire = JsonSerializer.Serialize(empire);
        }
    }
}
