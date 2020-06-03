using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
//using Microsoft.EntityFrameworkCore;
using ReUse_Std.AppDataModels.Common;
using ReUse_Std.Common;
using System.Data.Entity;
using ReUse_Net_Data.Platform;
using ReUse_Std.Base;
using ReUse_Std.AppDataModels.Feats;

namespace TestingNetConsoleApp.Tests
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
            var qs = "name=Model1"; // Model1   Testing_DB_Common

            var cs = new DCx<El, Ni, Ts>(qs);
            if (Ensure)
                cs.N();

            cs.U(c =>
            {
                var e = new El() { Et = "fghfgh ", ElId = _.g };
                e.C = new Ci() { T = "gdfgdfgdfg", CiId = _.g };
                var n = new Ni() { A = _.g, NiId = _.g };
                e.N = n;
                var t = new Ts() { U = true, TsId = _.g };
                t.W = new Tr() { D = "gdfgdfgdfg", TrId = _.g };
                t.S = new Tr() { D = "gdfgdfgdfg dfgg df hgfhd fg", TrId = _.g };
                //b. = new List<Ts>();
                //b.Posts.Add(new Ts() { Content = "gdf dgdg ", Title = "gfhjfghg hhfh" });
                //b.Posts.Add(new Ts() { Content = "gdf dgdg  3232423", Title = "gfhjfghg hhfh 2422342423" });
                c.d1.Add(e);
                c.d2.Add(n);
                c.d3.Add(t);
            });

            var cs2 = new DCx<El, Ni, Ts>(qs);

            cs2.R(c =>
            {
                var tt = c.d1
                .Include(a => a.N).Include(a => a.D).Include(a => a.C)
                //.Include(blog => blog.Posts)
                .L();
                var tt2 = c.d2
                //.Include(a => a)
                .L();
                var tt3 = c.d3
                .Include(a => a.W)
                .Include(a => a.S)
                .L();
            });

            //AppDataModels.sv();
        }
    }
}
