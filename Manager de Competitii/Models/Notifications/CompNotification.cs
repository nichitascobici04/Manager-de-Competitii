namespace Manager_de_Competitii.Models.Notifications
{
    using Manager_de_Competitii.Repositories;

    public class CompNotification : IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string Channel { get; set; } = "";
        public string Message { get; set; } = "";
        public string Target { get; set; } = "";
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public int? CompetitionId { get; set; }
    }
}
