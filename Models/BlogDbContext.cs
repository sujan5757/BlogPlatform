using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Blog>()
                .Property(p => p.Content)
                .IsRequired();

            modelBuilder.Entity<Blog>().HasData(
                new Blog { Id = 1, Title = "Summer season", Content = "This was Good", UserID = "Wilson1" },
                new Blog { Id = 2, Title = "Winter season", Content = "This was Good", UserID = "Wilson1" },
                new Blog { Id = 3, Title = "React", Content = "This was Good", UserID = "Sujan1" },
                new Blog { Id = 4, Title = "Angular", Content = "This was Good", UserID = "Sujan1" },
                new Blog { Id = 5, Title = "C#", Content = "This was Good", UserID = "Daniel1" },
                new Blog { Id = 6, Title = ".NET", Content = "This was Good", UserID = "Daniel1" }
            );
        }
    }
}
