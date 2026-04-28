namespace Manager_de_Competitii.Models.Decorator
{
    public class HomeAdvantageDecorator : ScoreDecorator
    {
        private double _multiplier;

        public HomeAdvantageDecorator(IScoreCalculator wrapped, double multiplier = 1.1) : base(wrapped)
        {
            _multiplier = multiplier;
        }

        public override double CalculateScore(int basePoints)
        {
            double currentScore = base.CalculateScore(basePoints);
            return currentScore * _multiplier;
        }
    }
}
