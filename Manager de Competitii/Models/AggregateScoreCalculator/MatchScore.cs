using System;

namespace Manager_de_Competitii.Models.AggregateScoreCalculator
{
    /// Leaf - scorul obținut într-un singur meci
    public class MatchScore : ScoreComponent
    {
        private int _points;

        public MatchScore(string name, int points) : base(name)
        {
            _points = points;
        }

        public override int GetScore()
        {
            return _points;
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + $"- {Name} : {_points} puncte");
        }
    }
}
