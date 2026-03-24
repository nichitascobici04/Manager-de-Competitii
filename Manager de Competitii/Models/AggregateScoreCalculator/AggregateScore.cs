using System;
using System.Collections.Generic;

namespace Manager_de_Competitii.Models.AggregateScoreCalculator
{
    /// Composite - scor calculat dintr-o grupare de meciuri sau etape (agregare)
    public class AggregateScore : ScoreComponent
    {
        private List<ScoreComponent> _components = new List<ScoreComponent>();

        public AggregateScore(string name) : base(name)
        {
        }

        public void Add(ScoreComponent component)
        {
            _components.Add(component);
        }

        public void Remove(ScoreComponent component)
        {
            _components.Remove(component);
        }

        public override int GetScore()
        {
            int total = 0;
            foreach (var component in _components)
            {
                total += component.GetScore();
            }
            return total;
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string(' ', depth) + $"+ {Name} (Total: {GetScore()} puncte)");
            foreach (var component in _components)
            {
                component.Display(depth + 2);
            }
        }
    }
}
