using Manager_de_Competitii.Interfaces;
using Manager_de_Competitii.Services;

namespace Manager_de_Competitii.Models;
public class User: IUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Competition> OrganizedCompetitions { get; set; }
    public Competition CreateCompetition(string name, int participantCount) {
        // Logic to create a Competition
        CompetitionCrudService competitionCrudService = new CompetitionCrudService();
        return new Competition();
    }
}
