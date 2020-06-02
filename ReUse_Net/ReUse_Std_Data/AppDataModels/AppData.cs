using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReUse_Std.Common;
using ReUse_Std.Base;
//using ReUse_Std.AppData;

namespace ReUse_Std_Data.AppDataModels.Common
{    
    #region Samples
    public class BloggingContext : DbContext
    {
        public DbSet<Blog2> Blogs { get; set; }
        public DbSet<Post2> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=TestStorageUWP;Integrated Security=False;User id=TestUserSQL;password=asdasd123123;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestStorageUWP;Integrated Security=True");
            //optionsBuilder.UseSqlite("Data Source=blogging.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog2>()
                .Property(b => b.Url)
                .IsRequired();
        }
    }

    [Table("blogs")]
    public class Blog2
    {
        public Guid Blog2Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Column("blog_Rating")]
        public Guid Rating { get; set; }
        ///// <summary>
        ///// Value generated on add
        ///// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public DateTime? Inserted { get; set; }

        ///// <summary>
        ///// Value generated on add or update
        ///// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime? LastUpdated { get; set; }
        public List<Post2> Posts { get; set; }
    }

    [Table("blogsPosts")]
    public class Post2
    {
        public Guid Post2Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Content;

        public string Content333;

        public v Method;

        public Guid Blog2Id { get; set; }
        public Blog2 Blog2 { get; set; }
    }



    #endregion
}
