namespace InvolvedEmpireRemastered.Client.SignalR
{
    public class DashboardHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "Dashboard");

            return base.OnConnectedAsync();
        }
    }
}
