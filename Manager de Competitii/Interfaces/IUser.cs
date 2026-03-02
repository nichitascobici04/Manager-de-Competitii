using Manager_de_Competitii.Models;

namespace Manager_de_Competitii.Interfaces
{
    public interface IUser
    {
        public Competition CreateCompetition(string name, int participantCount);
    }
}
