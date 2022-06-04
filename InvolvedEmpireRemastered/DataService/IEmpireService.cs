namespace InvolvedEmpireRemastered.DataService;

public interface IEmpireService
{
    // Base
    public GameState GetState();

    //Admin
    public Task LoadGame(int gameId, CancellationToken token);
    public void SetStatus(bool status);
    public Task CalculateNewDay(CancellationToken token);

    // Player
    // Gets
    public ValueTask LoadEmpire(int id, CancellationToken token);
    public ValueTask<Empire> GetEmpire(int id, CancellationToken token);
    public ValueTask<IEnumerable<EmpireReport>> GetOtherEmpires(int id, CancellationToken token);
    public ValueTask<PriceList> GetPriceList(int id, CancellationToken token);
    public IEnumerable<Structure> GetStructures();
    public Dragon GetDragon();
    // Actions
    public ValueTask<Transaction> BuyHouses(int id, int amount, CancellationToken token);
    public ValueTask<Transaction> EmployMiners(int id, int amount, CancellationToken token);
    public ValueTask<Transaction> BuyInfantry(int id, int amount, CancellationToken token);
    public ValueTask<Transaction> BuyArchers(int id, int amount, CancellationToken token);
    public ValueTask<Transaction> BuyKnights(int id, int amount, CancellationToken token);
    public ValueTask<Transaction> BuyStructure(int id, string name, CancellationToken token);
    public ValueTask Attack(int id, int targetId, CancellationToken token);
    public ValueTask AttackDragon(int id, CancellationToken token);
}
