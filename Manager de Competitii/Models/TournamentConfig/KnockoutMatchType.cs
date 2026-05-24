namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class KnockoutMatchType : IMatchType
    {
        public Match GenerateMatch()
        {
            return new Match
            {
                IsDrawAllowed = false,
                Scores1 = new List<int>(),
                Scores2 = new List<int>()
            };
        }
    }
}
