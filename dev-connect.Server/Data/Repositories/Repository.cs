using dev_connect.Server.Data.Entities.AuthToken;
using dev_connect.Server.Data.Entities.Users;
using dev_connect.Server.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.MicrosoftExtensions;
using static dev_connect.Server.Utils.UtilsClass;

namespace dev_connect.Server.Data.Repositories
{
    public class Repository : DbContext
    {
        public Repository(DbContextOptions<Repository> options) : base(options)
        {
        }

        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 .Property(u => u.Status)
                 .HasConversion(new EnumDescriptionConverter<VisibleStatus>());


            var passwordHasher = new PasswordHasher<User>();
            string hashedPassword = passwordHasher.HashPassword(null, "C0mplex");


            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Abdullah Mubasir",
                Email = "abduzayn709@gmail.com",
                UserName = "Muba",
                PasswordHash = hashedPassword,
                Status = VisibleStatus.Active,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System",
            }
                );
        }
    }
}
