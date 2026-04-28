namespace Manager_de_Competitii.Models.Flyweight
{
    public class MatchVenue
    {
        public string StadiumName { get; }
        public string Location { get; }
        public int Capacity { get; }

        public MatchVenue(string stadiumName, string location, int capacity)
        {
            StadiumName = stadiumName;
            Location = location;
            Capacity = capacity;
        }

        public void DisplayMatchDetails(string matchName, DateTime matchDate)
        {
            Console.WriteLine($"Match: {matchName} | Date: {matchDate.ToShortDateString()} | Venue: {StadiumName}, {Location} (Cap: {Capacity})");
        }
    }
}
