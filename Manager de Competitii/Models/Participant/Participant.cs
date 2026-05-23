namespace Manager_de_Competitii.Models
{
    using Manager_de_Competitii.Repositories;

    public class Participant : System.ICloneable, IEntity
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } = "";
        public bool IsBye { get; set; }
        public bool IsUser { get; set; }
        public User? AssociatedUser { get; set; }

        public object Clone()
        {
            return new Participant
            {
                TournamentId = this.TournamentId,
                Name = this.Name + " (Copy)",
                IsBye = this.IsBye,
                IsUser = this.IsUser,
                AssociatedUser = null // We don't deep copy users for this demo
            };
        }
    }
}