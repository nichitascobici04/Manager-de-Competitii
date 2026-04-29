namespace Manager_de_Competitii.Models.Memento
{
    public class TournamentMemento
    {
        public string Name { get; private set; }
        public int MaxTeams { get; private set; }
        public bool IsPublic { get; private set; }

        public TournamentMemento(string name, int maxTeams, bool isPublic)
        {
            Name = name;
            MaxTeams = maxTeams;
            IsPublic = isPublic;
        }
    }
}
