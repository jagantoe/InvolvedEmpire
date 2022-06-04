namespace InvolvedEmpireRemastered.BackgroundServices
{
    public class InvolvedEmpireBackgroundService : BackgroundService
    {
        private readonly IHubContext<EmpireHub> _hubContext;
        private readonly IHubContext<DashboardHub> _dashboardContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public InvolvedEmpireBackgroundService(IHubContext<EmpireHub> hubContext, IHubContext<DashboardHub> dashboardContext, IServiceScopeFactory serviceScopeFactory)
        {
            _hubContext = hubContext;
            _dashboardContext = dashboardContext;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public record GameDashboard
        {
            public bool Active { get; set; }
            public string Name { get; set; }
            public int Day { get; set; }
            public Dragon Dragon { get; set; }
            public IEnumerable<Empire> Empires { get; set; }
            public GameDashboard(GameState gameState)
            {
                Active = gameState.Active;
                Name = gameState.Name;
                Day = gameState.Day;
                Dragon = gameState.Dragon;
                Empires = gameState.GetEmpires().Values.ToList();
            }
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Default delay
                Task delay = Task.Delay(10000);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var empireService = scope.ServiceProvider.GetService<IEmpireService>();
                    var state = empireService.GetState();
                    if (state != null && state.Active)
                    {
                        // Resolve Battles
                        var dayReport = state.HandleBattles();
                        // Notify dashboards of DayReport
                        _ = _dashboardContext.Clients.Group("Dashboard").SendAsync("battlereport", dayReport);
                        // New Day Calculation
                        await empireService.CalculateNewDay(stoppingToken);
                        // Get Empire Dictionary
                        var users = state.GetEmpires();
                        // Notify players of new state
                        foreach (var (user, empire) in users)
                        {
                            _ = _hubContext.Clients.Group(user.ToString()).SendAsync("NewDay", empire);
                        }
                        // Use game delay
                        delay = Task.Delay(state.TimeBetweenDays);
                    }
                    if (state != null) _ = _dashboardContext.Clients.Group("Dashboard").SendAsync("game", new GameDashboard(state));
                }

                await delay;
            }
        }
    }
}
