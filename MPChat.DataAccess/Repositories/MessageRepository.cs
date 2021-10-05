﻿using MPChat.DataAccess.DbContexts;
using MPChat.DataAccess.Repositories.Abstract;
using MPChat.Types.Models;
using System.Linq;

namespace MPChat.DataAccess.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly SqlServerDbContext _dbContext;

        public MessageRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Message GetById(int id)
        {
            var message = _dbContext.Messages.SingleOrDefault(m => m.Id == id);
            if (message is null)
                return null;

            if (message.UserId is not null)
                _dbContext.Entry(message)
                    .Reference(m => m.User)
                    .Load();
            else
                _dbContext.Entry(message)
                    .Reference(m => m.Group)
                    .Load();

            return message;
        }

        public Message Add(Message entity)
        {
            var message = _dbContext.Messages.Add(entity)
                .Entity;
            _dbContext.SaveChanges();

            return message;
        }

        public Message Update(Message entity)
        {
            var message = _dbContext.Messages.Update(entity)
                .Entity;
            _dbContext.SaveChanges();

            return message;
        }

        public Message Delete(Message entity)
        {
            var message = _dbContext.Messages.Remove(entity)
                .Entity;
            _dbContext.SaveChanges();

            return message;
        }
    }
}
