namespace Manager_de_Competitii.Models
{
    public class MatchSet
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public int NumberOfParticipants { get; set; }
        public List<Match> Matches { get; set; }
        public List<Participant> Participants { get; set; }
        public List<Participant> Winners { get; set; }
    }
}
