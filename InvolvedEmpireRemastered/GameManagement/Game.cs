namespace InvolvedEmpireRemastered.GameManagement
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerializedGame { get; set; }
        public List<UserGame> UserGames { get; set; }
        public Game()
        {

        }
        public Game(string name, GameState gameState)
        {
            Name = name;
            SerializedGame = JsonSerializer.Serialize(gameState);
        }
    }
}
