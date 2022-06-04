namespace InvolvedEmpireRemastered.DataService;

public partial class EmpireService
{
    public async ValueTask LoadEmpire(int id, CancellationToken token)
    {
        var state = GetState();
        if (state == null) return;
        await GetEmpire(id, token);
    }
    public async ValueTask<Empire> GetEmpire(int id, CancellationToken token)
    {
        var empire = _empireCache[id];
        if (empire == null)
        {
            var state = GetState()!;
            var userGame = await _empireDatabaseService.GetUserGame(id, state.Id, token);
            if (userGame == null)
            {
                var user = await _empireDatabaseService.GetUser(id, token);
                userGame = new UserGame(id, state.Id, new Empire(user.Id, user.Name));
                await _empireDatabaseService.SaveEmpire(userGame, token);
            }
            _empireCache.AddEmpire(userGame);
            empire = _empireCache[id];
        }
        return empire!;
    }
    public async ValueTask<IEnumerable<EmpireReport>> GetOtherEmpires(int id, CancellationToken token)
    {
        if (GetState() == null) return null;
        return GetState().GetOtherEmpires(id);
    }
    public async ValueTask<Transaction> BuyHouses(int id, int amount, CancellationToken token)
    {
        if (GetState() == null) return new() { CurrentState = null, Success = false, Exception = "No game loaded!" };
        if (!GetState().Active) return new() { CurrentState = await GetEmpire(id, token), Success = false, Exception = "Game is pauzed!" };
        var empire = await GetEmpire(id, token);
        var transaction = empire.BuyHouse(amount);
        return transaction;
    }
    public async ValueTask<Transaction> EmployMiners(int id, int amount, CancellationToken token)
    {
        if (GetState() == null) return new() { CurrentState = null, Success = false, Exception = "No game loaded!" };
        if (!GetState().Active) return new() { CurrentState = await GetEmpire(id, token), Success = false, Exception = "Game is pauzed!" };
        var empire = await GetEmpire(id, token);
        var transaction = empire.EmployMiners(amount);
        return transaction;
    }
    public async ValueTask<Transaction> BuyInfantry(int id, int amount, CancellationToken token)
    {
        if (GetState() == null) return new() { CurrentState = null, Success = false, Exception = "No game loaded!" };
        if (!GetState().Active) return new() { CurrentState = await GetEmpire(id, token), Success = false, Exception = "Game is pauzed!" };
        var empire = await GetEmpire(id, token);
        var transaction = empire.BuyInfantry(amount);
        return transaction;
    }
    public async ValueTask<Transaction> BuyArchers(int id, int amount, CancellationToken token)
    {
        if (GetState() == null) return new() { CurrentState = null, Success = false, Exception = "No game loaded!" };
        if (!GetState().Active) return new() { CurrentState = await GetEmpire(id, token), Success = false, Exception = "Game is pauzed!" };
        var empire = await GetEmpire(id, token);
        var transaction = empire.BuyArchers(amount);
        return transaction;
    }
    public async ValueTask<Transaction> BuyKnights(int id, int amount, CancellationToken token)
    {
        if (GetState() == null) return new() { CurrentState = null, Success = false, Exception = "No game loaded!" };
        if (!GetState().Active) return new() { CurrentState = await GetEmpire(id, token), Success = false, Exception = "Game is pauzed!" };
        var empire = await GetEmpire(id, token);
        var transaction = empire.BuyKnights(amount);
        return transaction;
    }
    public async ValueTask<Transaction> BuyStructure(int id, string name, CancellationToken token)
    {
        if (GetState() == null) return new() { CurrentState = null, Success = false, Exception = "No game loaded!" };
        if (!GetState().Active) return new() { CurrentState = await GetEmpire(id, token), Success = false, Exception = "Game is pauzed!" };
        var empire = await GetEmpire(id, token);
        var transaction = empire.BuyStructure(name);
        return transaction;
    }

    public async ValueTask<PriceList> GetPriceList(int id, CancellationToken token)
    {
        if (GetState() == null) return null;
        var empire = await GetEmpire(id, token);
        return empire.GetPriceList();
    }
    public IEnumerable<Structure> GetStructures()
    {
        return Structure.Structures;
    }
    public Dragon GetDragon()
    {
        if (GetState() == null) return null;
        return GetState()!.Dragon;
    }

    public async ValueTask Attack(int id, int targetId, CancellationToken token)
    {
        if (GetState() == null) return;
        if (!GetState().Active) return;
        GetState().Attack(id, targetId);
    }
    public async ValueTask AttackDragon(int id, CancellationToken token)
    {
        if (GetState() == null) return;
        if (!GetState().Active) return;
        GetState().AttackDragon(id);
    }
}

