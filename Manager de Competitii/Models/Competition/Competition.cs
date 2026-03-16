namespace Manager_de_Competitii.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ParticipantCount { get; set; }
        public User Organizer { get; set; }
        public List<Participant>? Participants { get; set; }
        public Participant Winner { get; set; }
        public List<Stage>? Stages { get; set; }
        public bool IsCompleted { get; set; }
    }
}