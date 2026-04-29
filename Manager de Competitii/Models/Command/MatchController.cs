namespace Manager_de_Competitii.Models.Command
{
    public class MatchController
    {
        public string MatchId { get; private set; }
        public string Status { get; private set; }

        public MatchController(string matchId)
        {
            MatchId = matchId;
            Status = "Pending";
        }

        public void Start()
        {
            Status = "In Progress";
            Console.WriteLine($"[MatchController] Match '{MatchId}' has started.");
        }

        public void Cancel()
        {
            Status = "Cancelled";
            Console.WriteLine($"[MatchController] Match '{MatchId}' has been cancelled.");
        }
    }
}
