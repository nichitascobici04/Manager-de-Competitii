using Manager_de_Competitii.Models.AbstractFactory;
using Manager_de_Competitii.Models.FactoryMethod;

namespace Manager_de_Competitii.Models.CompetitionBuilder
{
    public interface ICompetitionBuilder
    {
        void SetName(string name);
        void SetParticipants(List<Participant> participants);
        void SetStages(List<Stage> stages);
        Competition GetCompetition();
    }
}
