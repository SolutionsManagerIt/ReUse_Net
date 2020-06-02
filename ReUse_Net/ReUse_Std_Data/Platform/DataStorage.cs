using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReUse_Std.Common;
using ReUse_Std.Base;
//using ReUse_Std.AppData;

namespace ReUse_Std_Data.Platform
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
            CurrContext.Database.EnsureCreated();
            return CurrContext;
        }

        /// <summary>
        /// Drop the database for CurrContext if it exists
        /// </summary>
        public static T D<T>(this T CurrContext) where T : DbContext
        {
            CurrContext.Database.EnsureDeleted();
            return CurrContext;
        }

        /// <summary>
        /// Generates a script to create all tables for the current model in CurrContext
        /// </summary>
        public static string Q<T>(this T CurrContext) where T : DbContext
        {
            return CurrContext.Database.GenerateCreateScript();
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
        /// <summary>
        /// Insert entity to CurrContext. 
        /// If it is known whether or not an insert or update is needed, then either Add or Update can be used appropriately
        /// </summary>
        public static void I<T>(this T CurrContext, object entity) where T : DbContext
        {
            CurrContext.Add(entity);
            CurrContext.SaveChanges();
        }

        /// <summary>
        /// Update entity to CurrContext. 
        /// If it is known whether or not an insert or update is needed, then either Add or Update can be used appropriately
        /// </summary>
        public static void U<T>(this T CurrContext, object entity) where T : DbContext
        {
            CurrContext.Update(entity);
            CurrContext.SaveChanges();
        }

        /// <summary>
        /// Insert Or Update entity to CurrContext. 
        /// The Update method normally marks the entity for update, not insert. However, if the entity has a auto-generated key, and no key value has been set, then the entity is instead automatically marked for insert.
        /// </summary>
        public static void Iu<T>(this T CurrContext, object entity) where T : DbContext
        {
            CurrContext.Update(entity);
            CurrContext.SaveChanges();
        }
        #endregion

        #endregion

        #region Context utilities

        /// <summary>
        /// Set the DbContextOptions with ConnectionSettings
        /// </summary>
        public static void C(this DbContextOptionsBuilder optionsBuilder, Dcs ConnectionSettings)
        {
            if (ConnectionSettings.P == Dp.Q)
                optionsBuilder.UseSqlServer(ConnectionSettings.C ?? _m.dq);
            else
                optionsBuilder.UseSqlite(ConnectionSettings.C ?? _m.dl);
        }

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
        public static string dq = @"Server=localhost;Database=TestStorageUWP;Integrated Security=False;User id=TestUserSQL;password=asdasd123123;";

        /// <summary>
        /// Default app connection string for sql Lite connections
        /// </summary>
        public static string dl = @"Data Source=TestStorageUWPLite.db";
        
        /// <summary>
        /// testing connection string
        /// </summary>
        public static string t = @"Server=localhost;Database=TestStorageUWP;Integrated Security=False;User id=TestUserSQL;password=asdasd123123;";
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
        /// Connection settings
        /// </summary>
        private Dcs s = new Dcs();

        /// <summary>
        /// Create new App Data Context for specified ConnectionString and ProviderType
        /// </summary>
        public DCx(string ConnectionString = null, Dp ProviderType = Dp.Q) : base()
        {
            s.C = ConnectionString;
            s.P = ProviderType;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.C(s);
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
        /// Connection settings
        /// </summary>
        private Dcs s = new Dcs();

        /// <summary>
        /// Create new App Data Context for specified ConnectionString and ProviderType
        /// </summary>
        public DCx(string ConnectionString = null, Dp ProviderType = Dp.Q) : base()
        {
            s.C = ConnectionString;
            s.P = ProviderType;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.C(s);
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
        /// Connection settings
        /// </summary>
        private Dcs s = new Dcs();

        /// <summary>
        /// Create new App Data Context for specified ConnectionString and ProviderType
        /// </summary>
        public DCx(string ConnectionString = null, Dp ProviderType = Dp.Q) : base()
        {
            s.C = ConnectionString;
            s.P = ProviderType;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.C(s);
        }
    }
    #endregion

    #region structs enums
    /// <summary>
    /// Common App Data Context Connection settings with Entity First support
    /// </summary>
    public class Dcs
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string C = null;
        /// <summary>
        /// Provider Type
        /// </summary>
        public Dp P = Dp.Q;
    }

    /// <summary>
    /// App data provider types
    /// </summary>
    public enum Dp
    {
        /// <summary>
        /// Use Sql Server provider
        /// </summary>
        Q,
        /// <summary>
        /// Use Sqlite provider
        /// </summary>
        L
    }
    #endregion
}
