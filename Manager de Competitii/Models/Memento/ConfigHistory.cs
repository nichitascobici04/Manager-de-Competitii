namespace Manager_de_Competitii.Models.Memento
{
    public class ConfigHistory
    {
        private Stack<TournamentMemento> _history = new Stack<TournamentMemento>();
        private TournamentConfigurator _originator;

        public ConfigHistory(TournamentConfigurator originator)
        {
            _originator = originator;
        }

        public void Backup()
        {
            _history.Push(_originator.Save());
        }

        public void Undo()
        {
            if (_history.Count == 0) return;

            var memento = _history.Pop();
            Console.WriteLine("[Caretaker] Reverting to previous state...");
            _originator.Restore(memento);
        }
    }
}
