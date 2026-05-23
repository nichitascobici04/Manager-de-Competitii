namespace Manager_de_Competitii.Models.CompetitionBuilder
{
    public class CompetitionDirector
    {
        private ICompetitionBuilder _builder;
        public CompetitionDirector(ICompetitionBuilder builder)
        {
            _builder = builder;
        }
        public void BuildBasicInfo(string name, string sport, string type, string location)
        {
            _builder.SetName(name);
            _builder.SetSport(sport);
            _builder.SetType(type);
            _builder.SetLocation(location);
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
