namespace InvolvedEmpireRemastered.BackgroundServices
{
    public class InvolvedEmpireBackgroundService : BackgroundService
    {
        private readonly IHubContext<EmpireHub> _hubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public InvolvedEmpireBackgroundService(IHubContext<EmpireHub> hubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _hubContext = hubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var empireService = scope.ServiceProvider.GetService<IEmpireService>();
                    var users = await empireService.CalculateNewDay(CancellationToken.None);
                    foreach (var (user, empire)in users)
                    {
                        _hubContext.Clients.Group(user.ToString()).SendAsync("NewDay", empire);
                    }
                }

                await Task.Delay(EmpireSettings.TimeBetweenDays);
            }
        }
    }
}
