namespace Manager_de_Competitii.Models.Strategy
{
    public class CustomEsportsStrategy : IPointCalculationStrategy
    {
        public int CalculatePoints(int goalsScored, int goalsConceded)
        {
            if (goalsScored > goalsConceded) return 5; // Win
            if (goalsScored == goalsConceded) return 0; // Draw
            return -1; // Loss penalty
        }
    }
}
