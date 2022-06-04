namespace InvolvedEmpireRemastered.DataService;

public partial class EmpireService : IEmpireService
{
    private readonly EmpireDatabaseService _empireDatabaseService;
    private readonly EmpireCache _empireCache;

    public EmpireService(EmpireDatabaseService empireDatabaseService, EmpireCache empireCache)
    {
        _empireDatabaseService = empireDatabaseService;
        _empireCache = empireCache;
    }

    public GameState GetState()
    {
        return _empireCache.GetGameState();
    }
}
