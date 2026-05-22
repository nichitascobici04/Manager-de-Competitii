namespace Client.Models
{
    public class MatchDto 
    { 
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; } = string.Empty;
    }
}
