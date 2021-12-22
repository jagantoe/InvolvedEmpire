namespace InvolvedEmpireGame
{
    public class Transaction
    {
        public bool Success { get; set; }
        public string Exception { get; set; }
        public Empire CurrentState { get; set; }
    }
}
