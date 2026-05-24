namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class RoundRobinFormat : IFormat
    {
        public List<Stage> GenerateStages(List<Participant> participants)
        {
            var stages = new List<Stage>();
            foreach (var participant in participants)
            {
                var stage = new Stage
                {
                    Name = $"Round {participant.Name}",
                    Matches = new List<Match>()
                };
                foreach (var opponent in participants)
                {
                    if (opponent.Id != participant.Id)
                    {
                        stage.Matches.Add(new Match
                        {
                            Participant1Name = participant.Name ?? "",
                            Participant2Name = opponent.Name ?? "",
                            IsDrawAllowed = true,
                            Scores1 = new List<int>(),
                            Scores2 = new List<int>()
                        });
                    }
                }
                stages.Add(stage);
            }
            return stages;
        }
    }
}
