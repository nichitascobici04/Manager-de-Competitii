namespace ManagerDeCompetitii.Models.Competiton
{
    public class CompetitionCrudService
    {
        public Competition CreateCompetition()
        {
            // Logic to create a Competition
            return new Competition();
        }
        public void ChangeName(int CompetitionIderId)
        {
            // Logic to change the Competition name
        }
        public void DeleteCompetition(int CompetitionId)
        {
            // Logic delete the user
        }
        public User GetCompetition(int CompetitionId)
        {
            // Logic to get the user
            return new User();
        }
    }
}
