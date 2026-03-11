namespace Manager_de_Competitii.Models
{
    public class Round
    {
        public int Id { get; set; }
        public List<Participant> Participants { get; set; }
        public List<int> Scores { get; set; }
    }
}
