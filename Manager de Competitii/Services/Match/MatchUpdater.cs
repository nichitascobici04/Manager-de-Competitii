namespace Manager_de_Competitii.Services.Match
{
    public class MatchUpdater
    {
        public Match FinishMatch(int MatchId)
        {
            // Logic to finish the match
            MatchFinisher.CompleteMatch(MatchId);
            return new Match();
        }
        public Match ChangeDetails(int MatchId)
        {
            // Logic to change some details of the match
            return new Match();
        }
    }
}
