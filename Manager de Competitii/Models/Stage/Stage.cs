namespace Manager_de_Competitii.Models.Stage
{
    public class Stage
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public List<Match> Matches { get; set; }
    }
}
