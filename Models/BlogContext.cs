using Microsoft.EntityFrameworkCore;

namespace BlogAppMy.Models
{
    public class BlogContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }    
    }
}
