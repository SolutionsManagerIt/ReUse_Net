using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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

        ///// <summary>
        ///// Generates a script to create all tables for the current model in CurrContext
        ///// </summary>
        //public static string Q<T>(this T CurrContext) where T : DbContext
        //{
        //    return CurrContext.Database.();
        //}

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

        //public static async Task<List<D>> GetBlogsAsync<T, D>(this T CurrContext) where T : DbContext
        //{
        //    using (var context = CurrContext)
        //    {
        //        return await context.Blogs.ToListAsync();
        //    }
        //}
        
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

        #region  Insert Or Update single items
        ///// <summary>
        ///// Insert entity to CurrContext. 
        ///// If it is known whether or not an insert or update is needed, then either Add or Update can be used appropriately
        ///// </summary>
        //public static void I<T>(this T CurrContext, object entity) where T : DbContext
        //{
        //    CurrContext.Database.Add(entity);
        //    CurrContext.SaveChanges();
        //}

        ///// <summary>
        ///// Update entity to CurrContext. 
        ///// If it is known whether or not an insert or update is needed, then either Add or Update can be used appropriately
        ///// </summary>
        //public static void U<T>(this T CurrContext, object entity) where T : DbContext
        //{
        //    CurrContext.Update(entity);
        //    CurrContext.SaveChanges();
        //}

        ///// <summary>
        ///// Insert Or Update entity to CurrContext. 
        ///// The Update method normally marks the entity for update, not insert. However, if the entity has a auto-generated key, and no key value has been set, then the entity is instead automatically marked for insert.
        ///// </summary>
        //public static void Iu<T>(this T CurrContext, object entity) where T : DbContext
        //{
        //    CurrContext.Update(entity);
        //    CurrContext.SaveChanges();
        //}
        #endregion

        #endregion

        #region common cases

        /// <summary>
        /// Get common data storage
        /// </summary>
        public static IEnumerable<T> Gd<T>(this string ConnectionName, f<T, bool> MethodToSelect, bool EnsureStructure = true) where T : class
        {
            var cs = new DCx<T>(ConnectionName);
            if (EnsureStructure)
                cs.N();

            IEnumerable<T> res = null;

            cs.R(c =>
            {
                if(MethodToSelect != null)
                    res = c.d1.w(a => MethodToSelect(a));
                else
                    res = c.d1.ToArray();
            });
            return res;
        }

        /// <summary>
        /// AddOrUpdate common data storage
        /// </summary>
        public static bool Ua<T>(this T[] ElementToUpdate, string ConnectionName) where T : class
        {
            var cs = new DCx<T>(ConnectionName);
            var e = ElementToUpdate;

            cs.U(c =>
            {
                c.d1.AddOrUpdate(e);
            });
            return true;
        }

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
