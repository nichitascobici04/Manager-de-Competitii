namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class RoundRobinMatchType: IMatchType
    {
        public Match GenerateMatch()
        {
            Match RrMatch = new Match
            {
                IsDrawAllowed = true,
                Sets = new List<MatchSet>(),
            };
            return RrMatch;
        }
    }
}
