using Microsoft.EntityFrameworkCore;
using MPChat.Types.Models;
using MPChat.Types.Options;

namespace MPChat.DataAccess.DbContexts
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
            SetUpUsersTable(modelBuilder);
            SetUpGroupsTable(modelBuilder);
            SetUpGroupMembersTable(modelBuilder);
            SetUpMessagesTable(modelBuilder);
        }

        private void SetUpUsersTable(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            
            // PK
            user.HasKey(u => u.Id);
            
            // FKs
            user.HasMany(u => u.GroupMembers).WithOne(gm => gm.User).HasForeignKey(gm => gm.UserId).IsRequired(false);
            
            // props
            user.Property(u => u.Name).IsRequired();
            user.Property(u => u.EmailAddress).IsRequired();

            // indexes
            user.HasIndex(u => u.EmailAddress).IsUnique();
            
            // ignore
            user.Ignore(u => u.Groups);
        }

        private void SetUpGroupsTable(ModelBuilder modelBuilder)
        {
            var group = modelBuilder.Entity<Group>();
            
            // PK
            group.HasKey(g => g.Id);
            
            // FKs
            group.HasMany(g => g.GroupMembers).WithOne(gm => gm.Group).HasForeignKey(gm => gm.GroupId).IsRequired();
            
            // props
            group.Property(g => g.Name).IsRequired();
            
            // ignore
            group.Ignore(g => g.Memebers);
        }

        private void SetUpGroupMembersTable(ModelBuilder modelBuilder)
        {
            // PK
            modelBuilder.Entity<GroupMembers>().HasKey(gm => new { gm.UserId, gm.GroupId });
        }

        private void SetUpMessagesTable(ModelBuilder modelBuilder)
        {
            // PK
            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            
            // FKs
            modelBuilder.Entity<Message>().HasOne(m => m.User).WithMany(u => u.Messages).HasForeignKey(m => m.UserId).IsRequired(false);
            modelBuilder.Entity<Message>().HasOne(m => m.Group).WithMany(g => g.Messages).HasForeignKey(m => m.GroupId).IsRequired(false);
            
            // CK
            modelBuilder.Entity<Message>()
                .HasCheckConstraint("CK_Recipient", "([UserId] is not null and [GroupId] is null) or ([UserId] is null and [GroupId] is not null)");

            // props
            modelBuilder.Entity<Message>().Property(m => m.Text).IsRequired();
        }
    }
}
