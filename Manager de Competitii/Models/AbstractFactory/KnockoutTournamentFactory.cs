namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class KnockoutTournamentFactor : ITournamentFactory
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
