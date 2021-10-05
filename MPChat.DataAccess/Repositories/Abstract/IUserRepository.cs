using MPChat.Types.Models;

namespace MPChat.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmailAddress(string emailAddress);
    }
}
