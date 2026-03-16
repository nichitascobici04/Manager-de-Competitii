namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class RoundRobinTournamentFactory : ITournamentFactory
    {
        public IFormat CreateFormat()
        {
            return new RoundRobinFormat();
        }
        public IMatchType CreateMatchType()
        {
            return new RoundRobinMatchType();
        }
    }
}
