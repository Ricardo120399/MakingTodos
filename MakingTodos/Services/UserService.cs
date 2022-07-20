namespace MakingTodos.Services
{
    public class UserService : IUserService
    {
        List<User> users = new List<User>()
        {
            new User(){Email = "ricardo123@gmail.com", Password = "123456"},
            new User(){Email = "ricardo456@gmail.com", Password = "654321"},
        };

        public bool IsUser(string email, string password) => 
            users.Where(d => d.Email == email && d.Password == password).Count() > 0;
        
    }
}
