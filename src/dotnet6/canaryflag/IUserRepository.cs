internal interface IUserRepository
{
    Task<User> GetUser(string id);
}