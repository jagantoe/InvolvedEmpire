namespace InvolvedEmpireRemastered.DataService;

public partial class EmpireService
{
    public async Task LoadGame(int gameId, CancellationToken token)
    {
        var game = await _empireDatabaseService.GetGame(gameId, token);
        if (game == null) return;
        _empireCache.LoadGame(game);
    }
    public void SetStatus(bool status)
    {
        GetState().Active = status;
    }
    public async Task CalculateNewDay(CancellationToken token)
    {
        var state = GetState();
        state.NewDay();
        await _empireDatabaseService.SaveGame(state, token);
        var users = await _empireDatabaseService.GetAllUsers(state.Id, token);
        foreach (var user in users)
        {
            var empire = await GetEmpire(user.UserId, token);
            empire.NewDay();
            user.SetEmpire(empire);
        }
        await _empireDatabaseService.SaveEmpires(users, token);
    }
}
