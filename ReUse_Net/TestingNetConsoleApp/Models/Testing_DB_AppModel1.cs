namespace TestingNetConsoleApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Testing_DB_AppModel1 : DbContext
    {
        public Testing_DB_AppModel1()
            : base("name=Testing_DB_AppModel1")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
