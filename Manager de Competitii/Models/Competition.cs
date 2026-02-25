namespace Manager_de_Competitii.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParticipantCount { get; set; }
        public List<Participant> Participants { get; set; }
        public Participant Winner { get; set; }
        public bool IsCompleted { get; set; }
        public string type { get; set; }
    }
}