namespace Manager_de_Competitii.Models.FactoryMethod
{
    public class TournamentRoundsService : CompetitionRoundsService
    {
        public override ICompetitionRounds Create()
        {
            return new TournamentRounds();
        }
    }
}
