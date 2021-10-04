using Microsoft.EntityFrameworkCore;
using MPChat.Types.Models;
using MPChat.Types.Options;

namespace MPChat.DataAccess
{
    public class SqlServerDbContext : DbContext
    {
        private readonly ConnectionStringsOptions _connectionStrings;

        public SqlServerDbContext(ConnectionStringsOptions connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.Database);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
