namespace Manager_de_Competitii.Models.Season
{
    public class Season
    {
        public Competition CompetitionSeason;
        public string Name { get; set; }
        public Season DeepCopy()
        {
            // Fa asta sa fie deep copy, nu shallow copy
            return (Season) this.MemberwiseClone();
        }
    }
}
