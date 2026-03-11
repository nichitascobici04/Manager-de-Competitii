namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class KnockoutTournamentFactory : ITournamentFactory
    {
        public IFormat CreateFormat()
        {
            return new KnockoutFormat();
        }
        public IMatchType CreateMatchType()
        {
            return new KnockoutMatchType();
        }
    }
}
