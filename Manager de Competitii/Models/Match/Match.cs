namespace Manager_de_Competitii.Models
{
    using Manager_de_Competitii.Repositories;

    public class Match : IEntity
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string Sport { get; set; } = "";
        public bool IsCompleted { get; set; }
        public bool IsDrawAllowed { get; set; }
        public string Participant1Name { get; set; } = "";
        public string Participant2Name { get; set; } = "";
        public string WinnerName { get; set; } = "";
        public List<int> Scores1 { get; set; } = new();
        public List<int> Scores2 { get; set; } = new();
    }
}
