namespace Manager_de_Competitii.Models.Observer
{
    public class LiveMatch
    {
        private List<IMatchObserver> _observers = new List<IMatchObserver>();

        public string TeamA { get; private set; }
        public string TeamB { get; private set; }
        public int ScoreA { get; private set; }
        public int ScoreB { get; private set; }
        public string LastEvent { get; private set; }

        public LiveMatch(string teamA, string teamB)
        {
            TeamA = teamA;
            TeamB = teamB;
            ScoreA = 0;
            ScoreB = 0;
            LastEvent = "Match started.";
        }

        public void Subscribe(IMatchObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IMatchObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void ScoreGoal(string scoringTeam)
        {
            if (scoringTeam == TeamA) ScoreA++;
            else if (scoringTeam == TeamB) ScoreB++;

            LastEvent = $"GOAL! {scoringTeam} scored!";
            NotifyObservers();
        }
    }
}
