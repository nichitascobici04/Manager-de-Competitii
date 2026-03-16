namespace Manager_de_Competitii.Models.FactoryMethod
{
    public class TournamentRounds: ICompetitionRounds
    {  
        public List<Round> CreateRounds() {
            List<Round> rounds = new List<Round>();
            for (int i = 1; i <= 3; i++)
            {
                Round round = new Round
                {
                    Id = i,
                    Participants = new List<Participant>
                {
                    new Participant { Id = 1, Name = "Team A" },
                    new Participant { Id = 2, Name = "Team B" }
                },
                    Scores = new List<int> { 0, 0 }
                };
                rounds.Add(round);
            }        
            return rounds;
        }
    }
}
