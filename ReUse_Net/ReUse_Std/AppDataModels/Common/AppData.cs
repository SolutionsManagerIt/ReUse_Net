using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ReUse_Std.Common;
using ReUse_Std.Base;
//using ReUse_Std.AppData;

namespace ReUse_Std.AppDataModels.Common
{
    /// <summary>
    /// Common App / feature settings storage
    /// </summary>
    [Serializable]
    public class App
    {
        /// <summary>
        /// Current App guid
        /// </summary>
        public Guid AppId { get; set; } = _.g;

        /// <summary>
        /// App encoding schema content storage
        /// </summary>
        public List<En> E { get; set; }
        /// <summary>
        /// App SQL connections storage
        /// </summary>
        public List<Sq> Q { get; set; }
        /// <summary>
        /// App serialized (text/xml/binary) content storage
        /// </summary>
        public List<Bn> B { get; set; }

        /// <summary>
        /// App Logging SQL connections storage
        /// </summary>
        public List<Sq> L { get; set; }
    }

    /// <summary>
    /// Common SQL connection Data Settings storage
    /// </summary>
    [Serializable]
    public class Sq
    {
        /// <summary>
        /// Current connection guid
        /// </summary>
        public Guid SqId { get; set; } = _.g;

        /// <summary>
        /// Current Sql Server Name (or use localhost if empty)
        /// </summary>
        public string S { get; set; }
        /// <summary>
        /// Current Sql DataBase name
        /// </summary>
        public string D { get; set; }

        /// <summary>
        /// Current Sql User Login (or use integrated security if empty)
        /// </summary>
        public string L { get; set; }
        /// <summary>
        /// Current Sql User Password
        /// </summary>
        public string P { get; set; }
        /// <summary>
        /// Current Sql connection Timeout (or default if not positive)
        /// </summary>
        public int? T { get; set; }

        #region Open from File
        /// <summary>
        /// Local File Dir to Open from File
        /// </summary>
        public string F { get; set; }
        /// <summary>
        /// Use App Directory to Open from File
        /// </summary>
        public bool Dr { get; set; } = false;
        /// <summary>
        /// Use Trusted Connection to Open from File
        /// </summary>
        public bool Tr { get; set; } = true;
        /// <summary>
        /// Use User Instance to Open from File
        /// </summary>
        public bool Ui { get; set; } = false;
        #endregion
    }

    /// <summary>
    /// Common App serialized (text/xml/binary) content settings storage
    /// </summary>
    [Serializable]
    public class Bn
    {
        /// <summary>
        /// Current serialized content guid
        /// </summary>
        public Guid BnId { get; set; } = _.g;
        /// <summary>
        /// Serialized Objects data
        /// </summary>
        public byte[] D { get; set; }
        /// <summary>
        /// Serialized Data Guid
        /// </summary>
        public Guid? G { get; set; }
        /// <summary>
        /// Serialized Object Guid
        /// </summary>
        public Guid? O { get; set; }
        /// <summary>
        /// Serialized Data string
        /// </summary>
        public string S { get; set; }
    }

    #region Samples
    
    #region Original
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Blog Blog { get; set; }
    }

    public class AuditEntry
    {
        public int AuditEntryId { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
    } 
    #endregion

    #endregion
}
