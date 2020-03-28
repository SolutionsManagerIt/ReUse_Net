using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReUse_Std.AppData.Common
{
    public class AppDataModels
    {
        public static async Task AddBlogAsync(string url)
        {
            using (var context = new BloggingContext())
            {
                var blog = new Blog { Url = url };
                context.Blogs.Add(blog);
                await context.SaveChangesAsync();
            }
        }

        public static void sv()
        {                       
            using (var context = new BloggingContext())
            {
                // seeding database
                context.Blogs.Add(new Blog { Url = "http://example.com/blog" });
                context.Blogs.Add(new Blog { Url = "http://example.com/another_blog" });
                context.SaveChanges();
            }

            using (var context = new BloggingContext())
            {
                // add
                context.Blogs.Add(new Blog { Url = "http://example.com/blog_one" });
                context.Blogs.Add(new Blog { Url = "http://example.com/blog_two" });

                // update
                var firstBlog = context.Blogs.First();
                firstBlog.Url = "";

                // remove
                var lastBlog = context.Blogs.Last();
                context.Blogs.Remove(lastBlog);

                context.SaveChanges();
            }
        }
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=TestStorageUWP;Integrated Security=False;User id=TestUserSQL;password=asdasd123123;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestStorageUWP;Integrated Security=True");
            //optionsBuilder.UseSqlite("Data Source=blogging.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(b => b.Url)
                .IsRequired();
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        [MaxLength(500)]
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
