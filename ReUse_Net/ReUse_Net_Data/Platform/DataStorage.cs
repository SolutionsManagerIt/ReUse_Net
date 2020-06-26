using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
//using System.Data.Entity.Utilities;
//using System.Data.Entity;
using ReUse_Std.Common;
using ReUse_Std.Base;
//using ReUse_Std.AppData;
using System.Data.Entity.Migrations;

namespace ReUse_Net_Data.Platform
{
    /// <summary>
    /// Common App Data Models Utilities
    /// </summary>
    public static class DataStorageUtilities
    {
        #region database
        /// <summary>
        /// Create the database for CurrContext if it doesn't exist
        /// </summary>
        public static T N<T>(this T CurrContext) where T : DbContext
        {
            CurrContext.Database.CreateIfNotExists();
            return CurrContext;
        }

        /// <summary>
        /// Drop the database for CurrContext if it exists
        /// </summary>
        public static T D<T>(this T CurrContext) where T : DbContext
        {
            CurrContext.Database.Delete();
            return CurrContext;
        }

        #endregion

        #region items

        #region Items sets
        /// <summary>
        /// Update data in CurrContext using custom MethodToUpdateData. 
        /// </summary>
        public static T U<T>(this T CurrContext, v<T> MethodToUpdateData) where T : DbContext
        {
            using (var c = CurrContext)
            {
                MethodToUpdateData(c);
                c.SaveChanges();
            }
            return CurrContext;
        }

        /// <summary>
        /// Read data in CurrContext using custom MethodToReadData. 
        /// </summary>
        public static T R<T>(this T CurrContext, v<T> MethodToReadData) where T : DbContext
        {
            using (var c = CurrContext)
            {
                MethodToReadData(c);
            }
            return CurrContext;
        }

        /// <summary>
        /// Update data Async in CurrContext using custom MethodToUpdateData. 
        /// </summary>
        public static async Task Ua<T>(this T CurrContext, v<T> MethodToUpdateData) where T : DbContext
        {
            using (var c = CurrContext)
            {
                MethodToUpdateData(c);
                await c.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Read data Async in CurrContext using custom MethodToReadData. 
        /// </summary>
        public static async Task Ra<T, F>(this T CurrContext, f<T, Task<F>> MethodToReadData) where T : DbContext
        {
            using (var c = CurrContext)
            {
                await MethodToReadData(c);
            }
        }

        #endregion

        #region  Insert Or Update items

        /// <summary>
        /// AddOrUpdate common data storage
        /// </summary>
        public static async Task<bool> Ua<T>(this T[] ElementsToUpdate, string ConnectionName) where T : class
        {
            var cs = new DCx<T>(ConnectionName);
            await cs.Ua(c =>
            {
                c.d1.AddOrUpdate(ElementsToUpdate);
            });
            return true;
        }

        /// <summary>
        /// AddOrUpdate common data storage
        /// </summary>
        public static bool U<T>(this T[] ElementsToUpdate, string ConnectionName) where T : class
        {
            var cs = new DCx<T>(ConnectionName);
            cs.U(c =>
            {
                c.d1.AddOrUpdate(ElementsToUpdate);
            });
            return true;
        }


        /// <summary>
        /// AddOrUpdate common data storage
        /// </summary>
        public static async Task<bool> ua<T>(this T ElementToUpdate, string ConnectionName) where T : class
        {
            var cs = new DCx<T>(ConnectionName);
            await cs.Ua(c =>
            {
                c.d1.AddOrUpdate(ElementToUpdate);
            });
            return true;
        }

        /// <summary>
        /// AddOrUpdate common data storage
        /// </summary>
        public static bool u<T>(this T ElementToUpdate, string ConnectionName) where T : class
        {
            var cs = new DCx<T>(ConnectionName);
            cs.U(c =>
            {
                c.d1.AddOrUpdate(ElementToUpdate);
            });
            return true;
        }

        #endregion

        #endregion

        #region common cases

        #region Include data 
        /// <summary>
        /// Include common data storage with 1 property type
        /// </summary>
        public static IQueryable<T> I<T, TProperty>(this IQueryable<T> Storage, Expression<Func<T, TProperty>> IncludePath, params Expression<Func<T, TProperty>>[] MorePathes)
        {
            var r = Storage.Include(IncludePath);
            foreach (Expression<Func<T, TProperty>> cn in MorePathes)
                r = r.Include(cn);
            return r;
        }

        /// <summary>
        /// Include common data storage with 2 property types
        /// </summary>
        public static IQueryable<T> I<T, TPr, TPr1>(this IQueryable<T> Storage, Expression<Func<T, TPr>> IncludePath, Expression<Func<T, TPr1>> IncludePath1)
        {
            var r = Storage.Include(IncludePath);
            if (IncludePath1 != null)
                r = r.Include(IncludePath1);
            return r;
        }

        /// <summary>
        /// Include common data storage with 3 property types
        /// </summary>
        public static IQueryable<T> I<T, TPr, TPr1, TPr2>(this IQueryable<T> Storage, Expression<Func<T, TPr>> IncludePath, Expression<Func<T, TPr1>> IncludePath1, Expression<Func<T, TPr2>> IncludePath2)
        {
            var r = Storage.Include(IncludePath);
            if(IncludePath1 != null)
                r = r.Include(IncludePath1);
            if (IncludePath2 != null)
                r = r.Include(IncludePath2);
            return r;
        }

        /// <summary>
        /// Include common data storage with 4 property types
        /// </summary>
        public static IQueryable<T> I<T, TPr, TPr1, TPr2, TPr3>(this IQueryable<T> Storage, Expression<Func<T, TPr>> IncludePath, Expression<Func<T, TPr1>> IncludePath1, Expression<Func<T, TPr2>> IncludePath2, Expression<Func<T, TPr3>> IncludePath3)
        {
            var r = Storage.Include(IncludePath);
            if (IncludePath1 != null)
                r = r.Include(IncludePath1);
            if (IncludePath2 != null)
                r = r.Include(IncludePath2);
            if (IncludePath3 != null)
                r = r.Include(IncludePath3);
            return r;
        }

        /// <summary>
        /// Include common data storage with 5 property types
        /// </summary>
        public static IQueryable<T> I<T, TPr, TPr1, TPr2, TPr3, TPr4>(this IQueryable<T> Storage, Expression<Func<T, TPr>> IncludePath, Expression<Func<T, TPr1>> IncludePath1, Expression<Func<T, TPr2>> IncludePath2, Expression<Func<T, TPr3>> IncludePath3, Expression<Func<T, TPr4>> IncludePath4)
        {
            var r = Storage.Include(IncludePath);
            if (IncludePath1 != null)
                r = r.Include(IncludePath1);
            if (IncludePath2 != null)
                r = r.Include(IncludePath2);
            if (IncludePath3 != null)
                r = r.Include(IncludePath3);
            if (IncludePath4 != null)
                r = r.Include(IncludePath4);
            return r;
        }

        /// <summary>
        /// Include common data storage with 6 property types
        /// </summary>
        public static IQueryable<T> I<T, TPr, TPr1, TPr2, TPr3, TPr4, TPr5>(this IQueryable<T> Storage, Expression<Func<T, TPr>> IncludePath, Expression<Func<T, TPr1>> IncludePath1, Expression<Func<T, TPr2>> IncludePath2, Expression<Func<T, TPr3>> IncludePath3, Expression<Func<T, TPr4>> IncludePath4, Expression<Func<T, TPr5>> IncludePath5)
        {
            var r = Storage.Include(IncludePath);
            if (IncludePath1 != null)
                r = r.Include(IncludePath1);
            if (IncludePath2 != null)
                r = r.Include(IncludePath2);
            if (IncludePath3 != null)
                r = r.Include(IncludePath3);
            if (IncludePath4 != null)
                r = r.Include(IncludePath4);
            if (IncludePath5 != null)
                r = r.Include(IncludePath5);
            return r;
        }

        #endregion

        #region Get data
        /// <summary>
        /// Get common data storage
        /// </summary>
        public static IEnumerable<T1> Gd<T1>(this string ConnectionName, f<IQueryable<T1>, IQueryable<T1>> MethodToInclude = null, f<T1, bool> MethodToSelect = null, bool EnsureStructure = true) where T1 : class
        {
            var cs = new DCx<T1>(ConnectionName);
            if (EnsureStructure)
                cs.N();

            IQueryable<T1> resq = null;
            IEnumerable<T1> res = null;

            cs.R(c =>
            {
                if (MethodToInclude != null)
                    resq = MethodToInclude(c.d1);
                else
                    resq = c.d1;
                if (MethodToSelect != null)
                    res = resq.w(e => MethodToSelect(e));
                else
                    res = resq.ToArray();
            });
            return res;
        }

        /// <summary>
        /// Get common data storage
        /// </summary>
        public static IEnumerable<T1> Gd<T1, T2>(this string ConnectionName, f<IQueryable<T1>, IQueryable<T1>> MethodToInclude, f<IQueryable<T2>, IQueryable<T2>> MethodToInclude2 = null, f<T1, bool> MethodToSelect = null, bool EnsureStructure = true) where T1 : class where T2 : class
        {
            var cs = new DCx<T1, T2>(ConnectionName);
            if (EnsureStructure)
                cs.N();

            IQueryable<T1> resq = null;
            IEnumerable<T1> res = null;
            IEnumerable<T2> res2 = null;

            cs.R(c =>
            {
                if (MethodToInclude != null)
                    resq = MethodToInclude(c.d1);
                else
                    resq = c.d1;
                if (MethodToSelect != null)
                    res = resq.w(e => MethodToSelect(e));
                else
                    res = resq.ToArray();
                if (MethodToInclude2 != null)
                    res2 = MethodToInclude2(c.d2).w(e => e != null);
            });
            return res;
        }

        /// <summary>
        /// Get common data storage
        /// </summary>
        public static IEnumerable<T1> Gd<T1, T2, T3>(this string ConnectionName, f<IQueryable<T1>, IQueryable<T1>> MethodToInclude, f<IQueryable<T2>, IQueryable<T2>> MethodToInclude2, f<IQueryable<T3>, IQueryable<T3>> MethodToInclude3 = null, f<T1, bool> MethodToSelect = null, bool EnsureStructure = true) where T1 : class where T2 : class where T3 : class
        {
            var cs = new DCx<T1, T2, T3>(ConnectionName);
            if (EnsureStructure)
                cs.N();

            IQueryable<T1> resq = null;
            IEnumerable<T1> res = null;
            IEnumerable<T2> res2 = null;
            IEnumerable<T3> res3 = null;

            cs.R(c =>
            {
                if (MethodToInclude != null)
                    resq = MethodToInclude(c.d1);
                else
                    resq = c.d1;
                if (MethodToSelect != null)
                    res = resq.w(e => MethodToSelect(e));
                else
                    res = resq.ToArray();
                if (MethodToInclude2 != null)
                    res2 = MethodToInclude2(c.d2).w(e => e != null);
                if (MethodToInclude3 != null)
                    res3 = MethodToInclude3(c.d3).w(e => e != null);
            });
            return res;
        }
        #endregion

        #endregion

        #region Context utilities

        ///// <summary>
        ///// Set the DbContextOptions with ConnectionSettings
        ///// </summary>
        //public static void C(this DbContextOptionsBuilder optionsBuilder, Dcs ConnectionSettings)
        //{
        //    if (ConnectionSettings.P == Dp.Q)
        //        optionsBuilder.UseSqlServer(ConnectionSettings.C ?? _m.dq);
        //    else
        //        optionsBuilder.UseSqlite(ConnectionSettings.C ?? _m.dl);
        //}

        #endregion
    }

    /// <summary>
    /// Common app data models storage settings
    /// </summary>
    public static class _m
    {
        public static string s = "";

        /// <summary>
        /// Default app connection string for sql server connections
        /// </summary>
        public static string dq = @"Server=localhost;Database=Testing_DB_Common;Integrated Security=True;";

        /// <summary>
        /// Default app connection string for file sql connections
        /// </summary>
        public static string dl = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\AppStorage\AppStorageTesting.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework";
        
        /// <summary>
        /// testing connection string
        /// </summary>
        public static string t = @"Server=localhost;Database=Testing_DB_Common;Integrated Security=True;";
    }

    #region App Data Contexts
    /// <summary>
    /// Common App Data Context with Entity First support
    /// </summary>
    public class DCx<T> : DbContext where T : class
    {
        /// <summary>
        /// Data storage container for type # 1
        /// </summary>
        public DbSet<T> d1 { get; set; }

        /// <summary>
        /// Create new App Data Context for specified ConnectionTitleString
        /// </summary>
        public DCx(string ConnectionTitleString = null) : base(ConnectionTitleString)
        {            
        }

        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }
    }

    /// <summary>
    /// Common App Data Context for 2 data types with Entity First support
    /// </summary>
    public class DCx<T1, T2> : DbContext where T1 : class where T2 : class
    {
        /// <summary>
        /// Data storage container for type # 1
        /// </summary>
        public DbSet<T1> d1 { get; set; }
        /// <summary>
        /// Data storage container for type # 2
        /// </summary>
        public DbSet<T2> d2 { get; set; }

        /// <summary>
        /// Create new App Data Context for specified ConnectionTitleString
        /// </summary>
        public DCx(string ConnectionTitleString = null) : base(ConnectionTitleString)
        {
        }

        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }
    }

    /// <summary>
    /// Common App Data Context for 3 data types with Entity First support
    /// </summary>
    public class DCx<T1, T2, T3> : DbContext where T1 : class where T2 : class where T3 : class
    {
        /// <summary>
        /// Data storage container for type # 1
        /// </summary>
        public DbSet<T1> d1 { get; set; }
        /// <summary>
        /// Data storage container for type # 2
        /// </summary>
        public DbSet<T2> d2 { get; set; }
        /// <summary>
        /// Data storage container for type # 3
        /// </summary>
        public DbSet<T3> d3 { get; set; }

        /// <summary>
        /// Create new App Data Context for specified ConnectionTitleString
        /// </summary>
        public DCx(string ConnectionTitleString = null) : base(ConnectionTitleString)
        {

        }
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }
    }
    #endregion

    #region structs enums
    
    #endregion
}
