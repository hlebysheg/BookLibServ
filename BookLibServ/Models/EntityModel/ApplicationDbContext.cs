using Microsoft.EntityFrameworkCore;

namespace BookLibServ.Models.EntityModel
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<TagModel> Tag { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<User> User { get; set; }
    }
}

