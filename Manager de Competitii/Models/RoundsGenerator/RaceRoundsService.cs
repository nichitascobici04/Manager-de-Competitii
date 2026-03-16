namespace Manager_de_Competitii.Models.FactoryMethod
{
    public class RaceRoundsService : CompetitionRoundsService
    {
        public override ICompetitionRounds Create()
        {
            return new RaceRounds();
        }
    }
}
