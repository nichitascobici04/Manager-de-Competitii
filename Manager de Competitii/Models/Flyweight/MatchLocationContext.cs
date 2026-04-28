namespace Manager_de_Competitii.Models.Flyweight
{
    public class MatchLocationContext
    {
        public string MatchName { get; set; }
        public DateTime MatchDate { get; set; }
        private MatchVenue _venue;

        public MatchLocationContext(string matchName, DateTime matchDate, MatchVenue venue)
        {
            MatchName = matchName;
            MatchDate = matchDate;
            _venue = venue;
        }

        public void Display()
        {
            _venue.DisplayMatchDetails(MatchName, MatchDate);
        }
    }
}
