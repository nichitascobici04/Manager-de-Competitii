using Manager_de_Competitii.Models.AbstractFactory;
using Manager_de_Competitii.Models.FactoryMethod;

namespace Manager_de_Competitii.Models.CompetitionBuilder
{
    public class KnockoutTournamentBuilder : ICompetitionBuilder
    {
        private Competition _tournament;
        public KnockoutTournamentBuilder()
        {
            _tournament = new Competition();
        }
        public void SetName(string name)
        {
            _tournament.Name = name;
        }
        public void SetSport(string sport)
        {
            _tournament.Sport = sport;
        }
        public void SetType(string type)
        {
            _tournament.Type = type;
        }
        public void SetLocation(string location)
        {
            _tournament.Location = location;
        }
        public void SetParticipants(List<Participant> participants)
        {
            _tournament.Participants = participants;
        }
        public void SetStages(List<Stage> stages)
        {
            _tournament.Stages = stages;
        }
        public Competition GetCompetition()
        {
            return _tournament;
        }
    }
}
