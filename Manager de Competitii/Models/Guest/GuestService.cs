using ManagerDeCompetitii.Models.User;
using Microsoft.AspNetCore.Identity.Data;

namespace ManagerDeCompetitii.Models.Guest
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
