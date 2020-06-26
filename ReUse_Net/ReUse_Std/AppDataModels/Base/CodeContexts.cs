﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReUse_Std.Base
{
    class CodeContexts
    {
    }

    /// <summary>
    /// common Code Execution Context
    /// </summary>
    [Serializable]
    public class Cx
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid CxId { get; set; } = _.g;

        /// <summary>
        /// Log on/off toggle
        /// </summary>
        public bool L { get; set; }
        /// <summary>
        /// Use Try Catch
        /// </summary>
        public bool T { get; set; }
        /// <summary>
        /// Log Data settings
        /// </summary>
        public Sls S { get; set; }
        /// <summary>
        /// Use elevated privileges (in Sharepoint for example)
        /// </summary>
        public bool? E { get; set; }

        ///// <summary>
        ///// Code Common Performance
        ///// </summary>
        //public Cm Cp;
        ///// <summary>
        ///// Code Common Error Data
        ///// </summary>
        //public Cm Ce;
        ///// <summary>
        ///// Code Common Asp
        ///// </summary>
        //public Cm Ca;
        ///// <summary>
        ///// Code Common Threads
        ///// </summary>
        //public Cm Ctr;
        ///// <summary>
        ///// Code Common Test
        ///// </summary>
        //public Cm Ct;

        ///// <summary>
        ///// Code Entry
        ///// </summary>
        //public Ed C;
    }


    /// <summary>
    /// Simple Log Data settings
    /// </summary>
    [Serializable]
    public class Sls
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid SlsId { get; set; } = _.g;

        /// <summary>
        /// CollectLogs
        /// </summary>
        public bool? Cl { get; set; }
        /// <summary>
        /// CollectPerfLogs
        /// </summary>
        public bool? Cp { get; set; }
        /// <summary>
        /// CollectPerfDetails
        /// </summary>
        public bool? Cpd { get; set; }

        /// <summary>
        /// CollectProcessLogs
        /// </summary>
        public bool? Cpr { get; set; }
        /// <summary>
        /// CollectProcessDetails
        /// </summary>
        public bool? Cprd { get; set; }
        /// <summary>
        /// CollectUsers
        /// </summary>
        public bool? Cu { get; set; }
        /// <summary>
        /// CollectOSData
        /// </summary>
        public bool? Co { get; set; }
        /// <summary>
        /// CollectErrorDetails
        /// </summary>
        public bool? Ced { get; set; }
        /// <summary>
        /// CollectHttpRequests
        /// </summary>
        public bool? Chr { get; set; }
        /// <summary>
        /// CollectHttpSessions
        /// </summary>
        public bool? Chs { get; set; }
        /// <summary>
        /// CollectHttpContexts
        /// </summary>
        public bool? Chc { get; set; }
        /// <summary>
        /// CollectWebPages
        /// </summary>
        public bool? Cwp { get; set; }
        /// <summary>
        /// CollectWebProfiles
        /// </summary>
        public bool? Cwr { get; set; }
        /// <summary>
        /// Collect Browser Details
        /// </summary>
        public bool? Cb { get; set; }
    }

    /// <summary>
    /// Code Common method execution details  - Class Name, Method Name, ParametersData, Details, ArrayItemSize, LogIndex etc
    /// </summary>
    [Serializable]
    public class Cm
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid CmId { get; set; } = _.g;
        /// <summary>
        /// Log on/off toggle
        /// </summary>
        public bool L { get; set; }

        /// <summary>
        /// Common Code Entry Data -  class/method details with comments
        /// </summary>
        public Ed A { get; set; }

        /// <summary>
        /// Class Name
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// Method Name
        /// </summary>
        public string M { get; set; }

        /// <summary>
        /// Error Is Critical
        /// </summary>
        public bool? Cr { get; set; }
        /// <summary>
        /// Re Throw On Error
        /// </summary>
        public bool? R { get; set; }

        /// <summary>
        /// Parameters Data
        /// </summary>
        public string P { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        public string D { get; set; }
        /// <summary>
        /// Array Size
        /// </summary>
        public int? As { get; set; }
        /// <summary>
        /// Log Index
        /// </summary>
        public int? Li { get; set; }
        /// <summary>
        /// Array Block Details 
        /// </summary>
        public Ab B { get; set; }
        /// <summary>
        /// Log SQL Conn String
        /// </summary>
        public string Lq { get; set; }
        /// <summary>
        /// Log XML Dir Path
        /// </summary>
        public string Lx { get; set; }
    }

    /// <summary>
    /// Common Code additional details Data -  class/method details with comments
    /// </summary>
    [Serializable]
    public class Ed
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid EdId { get; set; } = _.g;
        /// <summary>
        /// Custom Error Message
        /// </summary>
        public string Me { get; set; }
        /// <summary>
        /// Custom Info Message
        /// </summary>
        public string Mi { get; set; }
        /// <summary>
        /// Performance Comments
        /// </summary>
        public string Cp { get; set; }
        /// <summary>
        /// Testing Comments
        /// </summary>
        public string Ct { get; set; }
        /// <summary>
        /// Threading Comments
        /// </summary>
        public string Ctr { get; set; }
    }

    /// <summary>
    /// Code Array Block Details for block / buffer / paged methods
    /// </summary>
    [Serializable]
    public class Ab
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid AbId { get; set; } = _.g;
        /// <summary>
        /// Array Block Size
        /// </summary>
        public int? S { get; set; }
        /// <summary>
        /// Custom Array Block Message
        /// </summary>
        public string M { get; set; }
    }
}
