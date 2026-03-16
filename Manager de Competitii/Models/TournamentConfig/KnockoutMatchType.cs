namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class KnockoutMatchType: IMatchType
    {
        public Match GenerateMatch()
        {
            Match KoMatch = new Match
            {
                IsDrawAllowed = false,
                Sets = new List<MatchSet>(),
            };  
            return KoMatch;
        }
    }
}
