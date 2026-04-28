namespace Manager_de_Competitii.Models.Flyweight
{
    public class VenueFactory
    {
        private Dictionary<string, MatchVenue> _venues = new Dictionary<string, MatchVenue>();

        public MatchVenue GetVenue(string stadiumName, string location, int capacity)
        {
            string key = stadiumName;
            if (!_venues.ContainsKey(key))
            {
                _venues[key] = new MatchVenue(stadiumName, location, capacity);
                Console.WriteLine($"[VenueFactory] Created new venue: {stadiumName}");
            }
            else
            {
                Console.WriteLine($"[VenueFactory] Reusing existing venue: {stadiumName}");
            }
            return _venues[key];
        }
    }
}
