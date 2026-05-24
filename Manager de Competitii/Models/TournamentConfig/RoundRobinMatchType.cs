namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class RoundRobinMatchType : IMatchType
    {
        public Match GenerateMatch()
        {
            return new Match
            {
                IsDrawAllowed = true,
                Scores1 = new List<int>(),
                Scores2 = new List<int>()
            };
        }
    }
}
