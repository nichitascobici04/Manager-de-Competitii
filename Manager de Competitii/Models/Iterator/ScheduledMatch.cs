namespace Manager_de_Competitii.Models.Iterator
{
    public class ScheduledMatch
    {
        public string Title { get; private set; }
        public string Stadium { get; private set; }

        public ScheduledMatch(string title, string stadium)
        {
            Title = title;
            Stadium = stadium;
        }

        public override string ToString()
        {
            return $"Match: {Title} at {Stadium}";
        }
    }
}
