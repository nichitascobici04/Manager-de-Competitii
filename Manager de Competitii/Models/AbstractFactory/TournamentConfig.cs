namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class TournamentConfig
    {
        private readonly IFormat _format;
        private readonly IMatchType _matchType;

        public TournamentConfig(ITournamentFactory factory)
        {
            _format = factory.CreateFormat();
            _matchType = factory.CreateMatchType();
        }
        public void Run()
        {
            _format.GenerateStages(new List<Participant>());
            _matchType.GenerateMatch();
        }
    }
}
