namespace Manager_de_Competitii.Models.Strategy
{
    public class MatchResultProcessor
    {
        private IPointCalculationStrategy _strategy;

        public MatchResultProcessor(IPointCalculationStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IPointCalculationStrategy strategy)
        {
            _strategy = strategy;
        }

        public int ProcessMatchResult(int goalsScored, int goalsConceded)
        {
            return _strategy.CalculatePoints(goalsScored, goalsConceded);
        }
    }
}
