namespace Manager_de_Competitii.Models.Strategy
{
    public interface IPointCalculationStrategy
    {
        int CalculatePoints(int goalsScored, int goalsConceded);
    }
}
