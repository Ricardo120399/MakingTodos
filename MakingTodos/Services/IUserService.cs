namespace MakingTodos.Services
{
    public interface IUserService
    {
        public bool IsUser(string email, string password);
    }
}
