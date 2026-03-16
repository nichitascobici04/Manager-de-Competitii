namespace Manager_de_Competitii.Models.CompetitionBuilder
{
    public class CompetitionDirector
    {
        private ICompetitionBuilder _builder;
        public CompetitionDirector(ICompetitionBuilder builder)
        {
            _builder = builder;
        }
        public void NameCompetition(string name)
        {
            _builder.SetName(name);
        }
        public void AddParticipants(List<Participant> participants)
        {
            _builder.SetParticipants(participants);
        }
        public void AddStages(List<Stage> stages)
        {
            _builder.SetStages(stages);
        }
         public Competition GetCompetition()
        {
            return _builder.GetCompetition();
        }
    }
}
