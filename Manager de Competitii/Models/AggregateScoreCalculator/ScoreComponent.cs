namespace Manager_de_Competitii.Models.AggregateScoreCalculator
{
    /// Component - interfața comună pentru scorurile individuale și scorurile agregate
    public abstract class ScoreComponent
    {
        public string Name { get; set; }

        protected ScoreComponent(string name)
        {
            Name = name;
        }

        public abstract int GetScore();
        public abstract void Display(int depth);
    }
}
