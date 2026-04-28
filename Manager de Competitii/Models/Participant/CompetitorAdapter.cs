using Manager_de_Competitii.Interfaces;

namespace Manager_de_Competitii.Models
{
    public class CompetitorAdapter : ICompetitor
    {
        private readonly Participant _participant;

        public CompetitorAdapter(Participant participant)
        {
            _participant = participant;
        }

        public string GetCompetitorName()
        {
            return _participant.Name;
        }

        public string GetCompetitorStatus()
        {
            return _participant.IsBye ? "Inactive (Bye)" : "Active Participant";
        }
    }
}
