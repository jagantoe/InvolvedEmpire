namespace InvolvedEmpireRemastered.DataService.InMemory
{
    public class EmpireCache
    {
        private GameState GameState;

        public Empire? this[int i] => GameState[i];

        public void LoadGame(Game game)
        {
            var gamestate = JsonSerializer.Deserialize<GameState>(game.SerializedGame)!;
            gamestate.Id = game.Id;
            gamestate.Active = false;
            GameState = gamestate;
            GameState.Reset();
            foreach (var userGame in game.UserGames)
            {
                GameState.AddEmpire(JsonSerializer.Deserialize<Empire>(userGame.SerializedEmpire)!);
            }
        }

        public GameState GetGameState()
        {
            return GameState;
        }

        public void AddEmpire(UserGame userGame)
        {
            GameState.AddEmpire(JsonSerializer.Deserialize<Empire>(userGame.SerializedEmpire)!);
        }
    }
}
