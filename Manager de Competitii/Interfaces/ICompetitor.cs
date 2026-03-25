namespace Manager_de_Competitii.Interfaces
{
    /// <summary>
    /// Target - interfața așteptată de sistemul extern de raportare
    /// </summary>
    public interface ICompetitor
    {
        string GetCompetitorName();
        string GetCompetitorStatus();
    }
}
