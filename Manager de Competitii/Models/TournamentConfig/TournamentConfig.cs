namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class TournamentConfig
    {
        private readonly IFormat _format;
        private readonly IMatchType _matchType;

        public IFormat Format => _format;

        public TournamentConfig(IFormat format, IMatchType matchType)
        {
            _format = format;
            _matchType = matchType;
        }
        public void Run()
        {
            _format.GenerateStages(new List<Participant>());
            _matchType.GenerateMatch();
        }
    }
}
