namespace Manager_de_Competitii.Models
{
    public class MatchSet
    {
        public int Id { get; set; }
        public Participant Participant1 { get; set; }
        public Participant Participant2 { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
    }
}
