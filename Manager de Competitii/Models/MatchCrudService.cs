namespace Manager_de_Competitii.Models
{
    public class MatchCrudService
    {
        public Match CreateMatch()
        {
            // Logic to create a Match
            return new Match();
        }
        public Match UpdateMatch(int MatchId)
        {
            // Logic to update the match
            UpdateMatch(MatchId);
            return new Match();
        }
        public void DeleteCompetition(int MatchId)
        {
            // Logic delete the user
        }
        public Match GetCompetition(int Id)
        {
            // Logic to get the user
            return new Match();
        }
    }
}
