﻿namespace InvolvedEmpireRemastered.UserInterfaces
{
    public interface IEmpireClient
    {
        public Task<IEnumerable<Structure>> GetStructures(CancellationToken token);
        public Task<Dragon> GetDragon(CancellationToken token);
        public Task<PriceList> GetPriceList(CancellationToken token);
        public Task<IEnumerable<Empire>> GetOtherEmpires(CancellationToken token);

        public Task<Transaction> BuyHouse(int amount, CancellationToken token);
        public Task<Transaction> EmployMiners(int amount, CancellationToken token);
        public Task<Transaction> TrainInfantry(int amount, CancellationToken token);
        public Task<Transaction> TrainArchers(int amount, CancellationToken token);
        public Task<Transaction> TrainKnights(int amount, CancellationToken token);

        public Task Attack(int targetId, CancellationToken token);
        public Task AttackDragon(CancellationToken token);
    }
}
