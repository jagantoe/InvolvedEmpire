using System.Threading;

string hubUri = "https://involvedempire.azurewebsites.net/hub/InvolvedEmpire";
var logging = true;

// Use your username + password to retreive a token at https://involvedempire.azurewebsites.net/swagger/index.html#/Admin/Admin_GetToken
// The tokens are valid for 24h
var token = "";

// Dashboard: https://kind-hill-0fa661703.1.azurestaticapps.net/
Empire empire;
#region Config
var webSocket = new HubConnectionBuilder()
    .WithUrl(hubUri, options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(token);
    })
    .WithAutomaticReconnect()
    .Build();

webSocket.On<Empire>("NewDay", async response =>
{
    empire = response;
    await DayLogic();
});

await webSocket.StartAsync();

while (true)
{
    Thread.Sleep(100);
}
#endregion

// Write all code here
// Empire variable is auto updated on new day and with each state changing call
// Make sure to await each call to guarantee you have the latest state
// When passing an amount use -1 for max buy
async Task DayLogic()
{

}


#region Websocket calls
// Warranty void if code is changed

async Task<List<Structure>> GetStructures()
{
    return await webSocket.InvokeAsync<List<Structure>>("GetStructures");
}

async Task<Dragon> GetDragon()
{
    return await webSocket.InvokeAsync<Dragon>("GetDragon");
}

async Task<PriceList> GetPriceList()
{
    return await webSocket.InvokeAsync<PriceList>("GetPriceList");
}

async Task<List<EmpireReport>> GetOtherEmpires()
{
    return await webSocket.InvokeAsync<List<EmpireReport>>("GetOtherEmpires");
}

async Task BuyHouses(int amount)
{
    await BasicTransaction("BuyHouses", amount);
}

async Task EmployMiners(int amount)
{
    await BasicTransaction("EmployMiners", amount);
}

async Task TrainInfantry(int amount)
{
    await BasicTransaction("TrainInfantry", amount);
}

async Task TrainArchers(int amount)
{
    await BasicTransaction("TrainArchers", amount);
}

async Task TrainKnights(int amount)
{
    await BasicTransaction("TrainKnights", amount);
}

async Task BuyStructure(string name)
{
    var transaction = await webSocket.InvokeAsync<Transaction>("BuyStructure", name);
    empire = transaction.CurrentState;
    if (logging && !transaction.Success)
    {
        Console.WriteLine(transaction.Exception);
    }
}

async Task AttackEmpire(int id)
{
    await webSocket.InvokeAsync("Attack", id);
}

async Task AttackDragon()
{
    await webSocket.InvokeAsync("AttackDragon");
}

async Task BasicTransaction(string endpoint, int amount)
{
    if (amount == 0) return;
    if (amount < -1) throw new Exception($"Invalid amount: {amount}");
    var transaction = await webSocket.InvokeAsync<Transaction>(endpoint, amount);
    empire = transaction.CurrentState;
    if (logging && !transaction.Success)
    {
        Console.WriteLine(transaction.Exception);
    }
}
#endregion