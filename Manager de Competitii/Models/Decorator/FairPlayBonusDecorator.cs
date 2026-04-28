namespace Manager_de_Competitii.Models.Decorator
{
    public class FairPlayBonusDecorator : ScoreDecorator
    {
        private double _bonusAmount;

        public FairPlayBonusDecorator(IScoreCalculator wrapped, double bonusAmount) : base(wrapped)
        {
            _bonusAmount = bonusAmount;
        }

        public override double CalculateScore(int basePoints)
        {
            double currentScore = base.CalculateScore(basePoints);
            return currentScore + _bonusAmount;
        }
    }
}
