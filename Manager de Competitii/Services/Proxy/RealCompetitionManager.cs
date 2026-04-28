namespace Manager_de_Competitii.Services.Proxy
{
    public class RealCompetitionManager : ICompetitionManager
    {
        public void CreateCompetition(string competitionName, int createdByUserId)
        {
            Console.WriteLine($"[RealCompetitionManager] Competition '{competitionName}' successfully created by User ID: {createdByUserId}.");
        }

        public void DeleteCompetition(int competitionId, int deletingUserId)
        {
            Console.WriteLine($"[RealCompetitionManager] Competition ID {competitionId} successfully deleted by User ID: {deletingUserId}.");
        }
    }
}
