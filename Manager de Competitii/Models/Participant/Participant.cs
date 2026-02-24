namespace ManagerDeCompetitii.Models.Participant
{
    public class Participant
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public bool IsBye { get; set; }
        public bool IsUser { get; set; }
        public int UserId { get; set; }
    }
}