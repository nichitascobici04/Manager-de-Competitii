namespace Manager_de_Competitii.Models.Strategy
{
    public class FriendlyMatchStrategy : IPointCalculationStrategy
    {
        public int CalculatePoints(int goalsScored, int goalsConceded)
        {
            if (goalsScored > goalsConceded) return 2; // Win
            if (goalsScored == goalsConceded) return 1; // Draw
            return 0; // Loss
        }
    }
}
