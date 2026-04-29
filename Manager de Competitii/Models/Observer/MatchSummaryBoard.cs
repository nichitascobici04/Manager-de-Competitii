namespace Manager_de_Competitii.Models.Observer
{
    public class MatchSummaryBoard : IMatchObserver
    {
        public void Update(LiveMatch matchContext)
        {
            Console.WriteLine($"[MatchSummaryBoard] SCORE UPDATE: {matchContext.TeamA} {matchContext.ScoreA} - {matchContext.ScoreB} {matchContext.TeamB}");
        }
    }
}
