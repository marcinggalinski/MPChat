﻿using Microsoft.EntityFrameworkCore;
using MPChat.DataAccess.DbContexts;
using MPChat.DataAccess.Repositories.Abstract;
using MPChat.Types.Models;
using System.Linq;

namespace MPChat.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerDbContext _dbContext;

        public UserRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetByEmailAddress(string emailAddress)
        {
            var user = _dbContext.Users
                .Where(u => u.EmailAddress == emailAddress)
                .Include(u => u.GroupMembers)
                .ThenInclude(gm => gm.Group)
                .SingleOrDefault();
            if (user is null)
                return null;

            user.Groups = user.GroupMembers.Select(gm => gm.Group);

            return user;
        }

        public User GetById(int id)
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
            var user = _dbContext.Users.Add(entity).Entity;
            _dbContext.SaveChanges();

            return user;
        }

        public User Update(User entity)
        {
            var user = _dbContext.Users.Update(entity).Entity;
            _dbContext.SaveChanges();
            
            return user;
        }

        public User Delete(User entity)
        {
            var user = _dbContext.Users.Remove(entity).Entity;
            _dbContext.SaveChanges();
            
            return user;
        }
    }
}
