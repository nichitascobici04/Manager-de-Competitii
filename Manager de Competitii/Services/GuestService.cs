using Manager_de_Competitii.Models.Guest;
using Manager_de_Competitii.Models.User;
using Microsoft.AspNetCore.Identity.Data;

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
            return UserCrudService.GetUser();
        }
    }
}
