namespace Manager_de_Competitii.Models.FactoryMethod
{
    public class RoundsGenerator
    {
        private readonly Dictionary<string, Func<CompetitionRoundsService>> _creators =
            new Dictionary<string, Func<CompetitionRoundsService>>();
        public RoundsGenerator()
        {
            _creators["tournament"] = () => new TournamentRoundsService();
            _creators["race"] = () => new RaceRoundsService();
        }

        public CompetitionRoundsService GetService(string type)
        {
            if (!_creators.ContainsKey(type))
                throw new ArgumentException("Unsupported type.....!");
            return _creators[type]();
        }
    }
}
