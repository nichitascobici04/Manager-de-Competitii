namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class RoundRobinFormat: IFormat
    {
        public List<Stage> GenerateStages(List<Participant> participants)
        {
            List<Stage> stages = new List<Stage>();

            // Stages generation for Round Robin format
            foreach (var participant in participants) {
                Stage stage = new Stage
                {
                    Name = $"Round {participant.Name}",
                    Matches = new List<Match>()
                };
                foreach (var opponent in participants) {
                    if (opponent.Id != participant.Id) {
                        stage.Matches.Add(new Match
                        {
                            Participant1 = participant,
                            Participant2 = opponent,
                            IsDrawAllowed = true
                        });
                    }
                }
                stages.Add(stage);
            }
            return stages;
        }
    }
}
