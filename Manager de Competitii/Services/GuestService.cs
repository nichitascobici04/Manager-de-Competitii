using Manager_de_Competitii.Models;


namespace Manager_de_Competitii.Services
{
    public class GuestService
    {
        public Guest CreateGuest()
        {
            // Logic to create a guest
            return new Guest();
        }
        public User Login()
        {
            // Logic to log in as a guest
            int userId = 0; // This would be obtained from the login process
            return new UserCrudService().GetUser(userId);
        }
    }
}
