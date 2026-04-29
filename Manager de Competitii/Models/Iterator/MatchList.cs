namespace Manager_de_Competitii.Models.Iterator
{
    public class MatchList : IMatchCollection
    {
        private List<ScheduledMatch> _matches = new List<ScheduledMatch>();

        public void AddMatch(ScheduledMatch match)
        {
            _matches.Add(match);
        }

        public List<ScheduledMatch> GetItems()
        {
            return _matches;
        }

        public IMatchIterator CreateIterator()
        {
            return new StandardMatchIterator(this);
        }

        public IMatchIterator CreateStadiumIterator(string stadiumName)
        {
            return new StadiumMatchIterator(this, stadiumName);
        }
    }
}
