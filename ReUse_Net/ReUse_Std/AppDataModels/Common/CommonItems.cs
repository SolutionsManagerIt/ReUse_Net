using System;
using System.Collections.Generic;
using System.Text;

namespace ReUse_Std.AppDataModels.Common
{
    /// <summary>
    /// Common App user item with title and description
    /// </summary>
    [Serializable]
    public class Ci
    {
        /// <summary>
        /// Current item with title and description guid
        /// </summary>
        public Guid CiId { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// Tool Tip Text
        /// </summary>
        public string Tt { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string D { get; set; }
    }

    /// <summary>
    /// Common App date item with details on added updated
    /// </summary>
    [Serializable]
    public class Di
    {
        /// <summary>
        /// Current date item with details on added updated guid
        /// </summary>
        public Guid DiId { get; set; }
        /// <summary>
        /// Updated At
        /// </summary>
        public DateTime? Ut { get; set; }
        /// <summary>
        /// Added At
        /// </summary>
        public DateTime? At { get; set; }
        /// <summary>
        /// Updated By
        /// </summary>
        public string Ub { get; set; }
        /// <summary>
        /// Added By
        /// </summary>
        public string Ab { get; set; }
    }

    /// <summary>
    /// Common App visibility item with details
    /// </summary>
    [Serializable]
    public class Vi
    {
        /// <summary>
        /// Current visibility item with details on added updated guid
        /// </summary>
        public Guid ViId { get; set; }
        /// <summary>
        /// Use Global
        /// </summary>
        public bool? G { get; set; }
        /// <summary>
        /// Visible User
        /// </summary>
        public bool? U { get; set; }
        /// <summary>
        /// Visible PC 
        /// </summary>
        public bool? P { get; set; }
        /// <summary>
        /// Visible App
        /// </summary>
        public bool? A { get; set; }
    }

    /// <summary>
    /// Common App navigation item with details
    /// </summary>
    [Serializable]
    public class Ni
    {
        /// <summary>
        /// Current navigation item with details on added updated guid
        /// </summary>
        public Guid NiId { get; set; }
        /// <summary>
        /// Parent item Guid 
        /// </summary>
        public Guid? P { get; set; }
        /// <summary>
        /// Array Guid
        /// </summary>
        public Guid? A { get; set; }
        /// <summary>
        /// Session Guid 
        /// </summary>
        public Guid? S { get; set; }
        /// <summary>
        /// App Guid
        /// </summary>
        public Guid? G { get; set; }
    }
}
