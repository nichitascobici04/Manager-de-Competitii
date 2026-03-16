namespace Manager_de_Competitii.Models.AbstractFactory
{
    public interface ITournamentFactory
    {
        IFormat CreateFormat();
        IMatchType CreateMatchType();
    }
}
