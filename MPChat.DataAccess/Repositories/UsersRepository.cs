using Microsoft.EntityFrameworkCore;
using MPChat.DataAccess.DbContexts;
using MPChat.Types.Models;
using System.Linq;

namespace MPChat.DataAccess.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly SqlServerDbContext _dbContext;

        public UsersRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public User Get(int id)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == id)
                .Include(u => u.GroupMembers)
                .ThenInclude(gm => gm.Group)
                .SingleOrDefault();
            if (user is null)
                return null;

            user.Groups = user.GroupMembers.Select(gm => gm.Group);

            return user;
        }

        public User Add(User entity)
        {
            var result = _dbContext.Users.Add(entity).Entity;
            _dbContext.SaveChanges();

            return result;
        }

        public User Update(User entity)
        {
            var result = _dbContext.Users.Update(entity).Entity;
            _dbContext.SaveChanges();
            
            return result;
        }

        public User Delete(User entity)
        {
            var result = _dbContext.Users.Remove(entity).Entity;
            _dbContext.SaveChanges();
            
            return result;
        }
    }
}
