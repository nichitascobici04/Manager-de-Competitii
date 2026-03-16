namespace Manager_de_Competitii.Models.Season
{
    public class Season
    {
        public Competition CompetitionSeason;
        public string Name { get; set; }
        public Season ShallowCopy()
        {
            return (Season) this.MemberwiseClone();
        }
    }
}
