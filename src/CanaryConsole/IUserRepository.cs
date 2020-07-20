using System.Threading.Tasks;

namespace CanaryConsole
{
    public interface IUserRepository
    {
        Task<User> GetUser(string id);
    }
}