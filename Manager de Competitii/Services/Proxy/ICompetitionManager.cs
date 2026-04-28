namespace Manager_de_Competitii.Services.Proxy
{
    public interface ICompetitionManager
    {
        void CreateCompetition(string competitionName, int createdByUserId);
        void DeleteCompetition(int competitionId, int deletingUserId);
    }
}
