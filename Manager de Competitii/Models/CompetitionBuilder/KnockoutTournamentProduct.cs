namespace Manager_de_Competitii.Models.CompetitionBuilder
{
    public class KnockoutTournamentProduct
    {
        private List<object> _components = new List<object>();
        public void Add(string component)
        {
            this._components.Add(component);
        }
        public string ListComponents()
        {
            string result = "Tournament components: ";
            foreach (var component in _components)
                result += component + ", ";
            return result;
        }
    }
}
