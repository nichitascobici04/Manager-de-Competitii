namespace ManagerDeCompetitii.Models.User
{
    public class UserCrudService
    {
        public User CreateUser()
        {
            // Logic to create a user
            return new User();
        }
        public void ChangeUsername(int UserId)
        {
            // Logic to change the username
        }
        public void ChangePassword(int UserId)
        {
            // Logic to create the password
        }
        public void DeleteUser(int UserId)
        {
            // Logic delete the user
        }
        internal static User GetUser(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
