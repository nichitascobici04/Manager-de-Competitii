namespace Manager_de_Competitii.Models.Decorator
{
    public abstract class ScoreDecorator : IScoreCalculator
    {
        protected IScoreCalculator _wrapped;

        public ScoreDecorator(IScoreCalculator wrapped)
        {
            _wrapped = wrapped;
        }

        public virtual double CalculateScore(int basePoints)
        {
            return _wrapped.CalculateScore(basePoints);
        }
    }
}
