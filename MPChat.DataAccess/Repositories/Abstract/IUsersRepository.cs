using MPChat.Types.Models;

namespace MPChat.DataAccess.Repositories.Abstract
{
    public interface IUsersRepository : IRepository<User>
    {
        User GetByEmailAddress(string emailAddress);
    }
}
