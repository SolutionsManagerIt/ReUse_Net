using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReUse_Std.AppDataModels.Common;
using ReUse_Std.Common;
using ReUse_Std.Platform;

namespace TestCoreConsole.Tests
{
    /// <summary>
    /// Test SQL Storage
    /// </summary>
    public static class SQL_Storage
    {
        ///// <summary>
        ///// Test Sample Storage 
        ///// </summary>
        //public static void Ts(bool Ensure = true)
        //{
        //    if (Ensure)
        //        new BloggingContext().N();
        //    AppDataModels.sv();
        //}

        /// <summary>
        /// Test Sample Storage with custom context 
        /// </summary>
        public static void Tcs(bool Ensure = true)
        {
            var cs = new DCx<Blog2, Post2>();
            if (Ensure)
                cs.N();

            cs.U(c => 
            {
                var b = new Blog2() { Url = "fghfgh " };
                b.Posts = new List<Post2>();
                b.Posts.Add(new Post2() { Content = "gdf dgdg ", Title= "gfhjfghg hhfh"});
                b.Posts.Add(new Post2() { Content = "gdf dgdg  3232423", Title = "gfhjfghg hhfh 2422342423" });
                c.d1.Add(b);
            });

            var cs2 = new DCx<Blog2, Post2>();

            cs2.R(c =>
            {
                var tt = c.d1.Include(blog => blog.Posts).L();

            });

            //AppDataModels.sv();
        }
    }
}
