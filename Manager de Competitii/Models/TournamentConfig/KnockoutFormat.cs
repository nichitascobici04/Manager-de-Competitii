namespace Manager_de_Competitii.Models.AbstractFactory
{
    public class KnockoutFormat : IFormat
    {
        public List<Stage> GenerateStages(List<Participant> participants)
        {
            var stages = new List<Stage>();
            for (int i = 0; i + 1 < participants.Count; i += 2)
            {
                stages.Add(new Stage
                {
                    Name = $"Round {i / 2 + 1}",
                    Matches = new List<Match>
                    {
                        new Match
                        {
                            Participant1Name = participants[i].Name ?? "",
                            Participant2Name = participants[i + 1].Name ?? "",
                            IsDrawAllowed = false,
                            Scores1 = new List<int>(),
                            Scores2 = new List<int>()
                        }
                    }
                });
            }
            return stages;
        }
    }
}
