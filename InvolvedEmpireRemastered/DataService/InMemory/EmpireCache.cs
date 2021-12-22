namespace InvolvedEmpireRemastered.DataService.InMemory
{
    public class EmpireCache
    {
        private Dictionary<int, Empire> Empires { get; set; } = new Dictionary<int,Empire>();

        public Empire? this[int i] => Empires.GetValueOrDefault(i);

        private void Reset()
        {
            Empires = new Dictionary<int,Empire>();
        }

        public IEnumerable<Empire> GetAllEmpires()
        {
            return Empires.Values;
        }
        public Dictionary<int, Empire> GetAllUserEmpires()
        {
            return Empires;
        }

        public void AddEmpire(User user)
        {
            if (user != null && !Empires.ContainsKey(user.Id))
            {
                var empire = JsonSerializer.Deserialize<Empire>(user.SerializedEmpire);
                empire.Id = user.Id;
                Empires.Add(user.Id, empire);
            }
        }

        public void LoadEmpires(IEnumerable<User> users)
        {
            Reset();
            foreach (var user in users)
            {
                Empires.Add(user.Id, JsonSerializer.Deserialize<Empire>(user.SerializedEmpire));
            }
        }
    }
}
