namespace Manager_de_Competitii.Models
{
    using Manager_de_Competitii.Repositories;

    public class Competition : IEntity, System.ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sport { get; set; }
        public string Location { get; set; }
        public int ParticipantCount { get; set; }
        public User Organizer { get; set; }
        public List<Participant>? Participants { get; set; }
        public Participant Winner { get; set; }
        public List<Stage>? Stages { get; set; }
        public bool IsCompleted { get; set; }

        public object Clone()
        {
            var clone = (Competition)this.MemberwiseClone();
            clone.Id = 0; // Reset ID for new entity
            clone.Name = this.Name + " (Copy)";
            clone.IsCompleted = false;
            clone.Winner = null;
            clone.Stages = null; // Clear stages/matches
            
            // Deep copy participants list
            if (this.Participants != null)
            {
                clone.Participants = this.Participants.Select(p => (Participant)p.Clone()).ToList();
            }
            return clone;
        }
    }
}