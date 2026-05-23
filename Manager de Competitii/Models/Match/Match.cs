namespace Manager_de_Competitii.Models
{
    using Manager_de_Competitii.Repositories;

    public class Match : IEntity
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        
        public bool IsCompleted { get; set; }
        public bool IsDrawAllowed { get; set; }
        public Participant Participant1 { get; set; }
        public Participant Participant2 { get; set; }
        public Participant Winner { get; set; }
        public List<MatchSet> Sets { get; set; }
        public MatchSet? TieBreakSet { get; set; }
}
}
