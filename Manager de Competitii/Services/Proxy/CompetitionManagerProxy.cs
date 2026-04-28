using Manager_de_Competitii.Models.User;

namespace Manager_de_Competitii.Services.Proxy
{
    public class CompetitionManagerProxy : ICompetitionManager
    {
        private RealCompetitionManager _realManager;
        // In a real application, you'd fetch the user's role from a database or token
        private string _currentUserRole;

        public CompetitionManagerProxy(string currentUserRole)
        {
            _realManager = new RealCompetitionManager();
            _currentUserRole = currentUserRole;
        }

        public void CreateCompetition(string competitionName, int createdByUserId)
        {
            if (HasOrganizerAccess())
            {
                Console.WriteLine("[Proxy] Access granted for CreateCompetition.");
                _realManager.CreateCompetition(competitionName, createdByUserId);
            }
            else
            {
                Console.WriteLine($"[Proxy] Access denied for CreateCompetition. Role required: Organizer. Current Role: {_currentUserRole}");
            }
        }

        public void DeleteCompetition(int competitionId, int deletingUserId)
        {
            if (HasOrganizerAccess())
            {
                Console.WriteLine("[Proxy] Access granted for DeleteCompetition.");
                _realManager.DeleteCompetition(competitionId, deletingUserId);
            }
            else
            {
                Console.WriteLine($"[Proxy] Access denied for DeleteCompetition. Role required: Organizer. Current Role: {_currentUserRole}");
            }
        }

        private bool HasOrganizerAccess()
        {
            return _currentUserRole.Equals("Organizer", StringComparison.OrdinalIgnoreCase);
        }
    }
}
