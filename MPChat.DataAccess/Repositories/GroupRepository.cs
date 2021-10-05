using Microsoft.EntityFrameworkCore;
using MPChat.DataAccess.DbContexts;
using MPChat.DataAccess.Repositories.Abstract;
using MPChat.Types.Models;
using System.Linq;

namespace MPChat.DataAccess.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        private readonly SqlServerDbContext _dbContext;

        public GroupRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Group GetById(int id)
        {
            var group = _dbContext.Groups
                .Where(g => g.Id == id)
                .Include(g => g.GroupMembers)
                .ThenInclude(gm => gm.User)
                .SingleOrDefault();
            if (group is null)
                return null;

            group.Members = group.GroupMembers.Select(gm => gm.User);
            
            return group;
        }

        public Group Add(Group entity)
        {
            var group = _dbContext.Groups.Add(entity).Entity;
            _dbContext.SaveChanges();

            return group;
        }

        public Group Update(Group entity)
        {
            var group = _dbContext.Groups.Update(entity).Entity;
            _dbContext.SaveChanges();

            return group;
        }

        public Group Delete(Group entity)
        {
            var group = _dbContext.Groups.Remove(entity).Entity;
            _dbContext.SaveChanges();

            return group;
        }
    }
}
