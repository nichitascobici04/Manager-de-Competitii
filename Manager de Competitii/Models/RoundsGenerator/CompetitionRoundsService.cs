namespace Manager_de_Competitii.Models.FactoryMethod
{
    public abstract class CompetitionRoundsService
    {
        public abstract ICompetitionRounds Create();

        public void Competition()
        {
            var competitionRounds = Create();
            competitionRounds.CreateRounds();
        }
    }
}
