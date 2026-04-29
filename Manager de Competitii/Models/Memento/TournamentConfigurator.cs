namespace Manager_de_Competitii.Models.Memento
{
    public class TournamentConfigurator
    {
        public string Name { get; set; }
        public int MaxTeams { get; set; }
        public bool IsPublic { get; set; }

        public TournamentConfigurator(string name, int maxTeams, bool isPublic)
        {
            Name = name;
            MaxTeams = maxTeams;
            IsPublic = isPublic;
        }

        public TournamentMemento Save()
        {
            Console.WriteLine($"[Configurator] Saving state: {Name}, {MaxTeams} teams, Public: {IsPublic}");
            return new TournamentMemento(Name, MaxTeams, IsPublic);
        }

        public void Restore(TournamentMemento memento)
        {
            Name = memento.Name;
            MaxTeams = memento.MaxTeams;
            IsPublic = memento.IsPublic;
            Console.WriteLine($"[Configurator] Restored state: {Name}, {MaxTeams} teams, Public: {IsPublic}");
        }
    }
}
