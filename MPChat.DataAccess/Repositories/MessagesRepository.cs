using MPChat.DataAccess.DbContexts;
using MPChat.Types.Models;
using System.Linq;

namespace MPChat.DataAccess.Repositories
{
    public class MessagesRepository : IRepository<Message>
    {
        private readonly SqlServerDbContext _dbContext;

        public MessagesRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Message Get(int id)
        {
            var message = _dbContext.Messages.SingleOrDefault(m => m.Id == id);
            if (message is null)
                return null;
            
            if (message.UserId is not null)
                _dbContext.Entry(message).Reference(m => m.User).Load();
            else
                _dbContext.Entry(message).Reference(m => m.Group).Load();


            return message;
        }

        public Message Add(Message entity) => _dbContext.Messages.Add(entity).Entity;

        public Message Update(Message entity) => _dbContext.Messages.Update(entity).Entity;

        public Message Delete(Message entity) => _dbContext.Messages.Remove(entity).Entity;
    }
}
