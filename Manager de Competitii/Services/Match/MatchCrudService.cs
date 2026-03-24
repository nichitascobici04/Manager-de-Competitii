namespace Manager_de_Competitii.Services.MatchServices
{
    public class MatchCrudService
    {
        static internal Match CreateMatch()
        {
            // Logic to create a Match
            return new Match();
        }
        static internal Match UpdateMatch(int MatchId)
        {
            // Logic to update the match
            UpdateMatch(MatchId);
            return new Match();
        }
        static internal void DeleteMatch(int MatchId)
        {
            // Logic delete the match
        }
        static internal Match GetMatch(int MatchId)
        {
            // Logic to get the match
            return new Match();
        }
    }
}
