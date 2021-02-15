using CecSessions.Core.Models.Session;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CecSessions.Core.Entities
{
    public class CecSessionsContext : IdentityDbContext<ApplicationUser>
    {
        public CecSessionsContext(DbContextOptions<CecSessionsContext> options)
            : base(options)
        {
        }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Question> Questions { get; set; }
       
        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<UserDb>().HasData(

        //        new UserDb
        //        {
        //            Id = 1,
        //            Login = "Garegin",
        //            Password = "Sa123456!",
        //            FirstName = "Garegin",
        //            LastName = "Ayvazyan",
        //            Position = "KYH masnaget"
        //        },
        //         new UserDb
        //         {
        //             Id = 2,
        //             Login = "Yelena",
        //             Password = "Sa123456",
        //             FirstName = "Yelena",
        //             LastName = "Ayvazyan",
        //             Position = "KYH dddd"
        //         }

        //        ); 
        //}
    }
}

