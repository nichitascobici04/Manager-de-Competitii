using Manager_de_Competitii.Interfaces;
using Manager_de_Competitii.Services;

namespace Manager_de_Competitii.Models
{
    public class Guest : User
    {
        public void Register()
        {
            // Logic to register a guest as a user
            UserCrudService userCrudService = new UserCrudService();
            userCrudService.CreateUser();
        }
        public void Login()
        {
            // Logic to register a guest as a user
            int userId = 0; // This would be obtained from the login process
            UserCrudService userCrudService = new UserCrudService();
            userCrudService.GetUser(userId);
        }
    }
}
