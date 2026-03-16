namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class KnockoutFormat: IFormat
    {
        public List<Stage> GenerateStages(List<Participant> participants)
        {
            List<Stage> stages = new List<Stage>();

            // Stages generation for Knockout format
            for (int i = 0; i < participants.Count; i += 2)
            {
                Stage stage = new Stage
                {
                    Name = $"Round {i / 2 + 1}",
                    Matches = new List<Match>
                    {
                        new Match
                        {
                            Participant1 = participants[i],
                            Participant2 = participants[i + 1],
                            IsDrawAllowed = false
                        }
                    }
                };
                stages.Add(stage);
            }
            return stages;
        }
    }
}
