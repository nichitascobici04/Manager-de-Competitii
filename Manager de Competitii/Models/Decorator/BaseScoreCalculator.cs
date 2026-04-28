namespace Manager_de_Competitii.Models.Decorator
{
    public class BaseScoreCalculator : IScoreCalculator
    {
        public double CalculateScore(int basePoints)
        {
            return basePoints;
        }
    }
}
