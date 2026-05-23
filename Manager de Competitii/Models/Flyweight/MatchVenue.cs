namespace Manager_de_Competitii.Models.Flyweight
{
    using Manager_de_Competitii.Repositories;

    public class MatchVenue : IEntity
    {
        public int Id { get; set; }
        public string StadiumName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public MatchVenue() { }

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
