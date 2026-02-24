namespace ManagerDeCompetitii.Models.Match
{
    public class Match
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public Participant Participant1 { get; set; }
        public Participant Participant2 { get; set; }
        public Participant Winner { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDrawAllowed { get; set; }
        
    }
}
