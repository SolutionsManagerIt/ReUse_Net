using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text;
//using CommonStructsDataUtilities.Structs.System;
using System.Data;
using System.Diagnostics;
//using CommonStructsDataUtilities.Logs;
//using CommonStructsDataUtilities.Logs_old;
using ReUse_Std.Common;
using ReUse_Std.Base.Performance;
using ReUse_Std.AppDataModels.Logging;
using System.Reflection;
using System.Threading.Tasks;

namespace ReUse_Std.Base
{
    /// <summary>
    /// Common Code Schema Utilities  -  Run, Try, Loop, If, Process, For, Each, etc
    /// </summary>
    public static class CommonCodeUtilities
    {
        #region private

        private static int Mbv = 1024 * 1024;
        private static int Kbv = 1024;
        private static int Gbv = 1024 * 1024 * 1024;

        #endregion

        #region Run Code

        /// <summary>
        /// Run Common Try FuncToRun With Errors And Performance Logging based on CurrContext and optional CustomCodeType.
        /// </summary>        
        public static T r<T, Tx>(this Ax<Tx> CurrContext, f<Cx, T> FuncToRun, Tx CustomCodeType = default, Mx CustomMethodContext = null, T ReturnOnError = default)
        {
            if (CurrContext == null || FuncToRun == null)
                return ReturnOnError;
            Cx x = null;
            if (CustomCodeType != null && CurrContext.T != null)
                x = CurrContext.T.v(CustomCodeType);
            return CurrContext.L.r(() => FuncToRun(x), x, CustomMethodContext, ReturnOnError);
        }

        /// <summary>
        /// Run Common Sync Try FuncToRunSync With Errors And Performance Logging based on CurrCodeType.
        /// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        /// </summary>        
        public static T r<T>(this Lg CurrLog, f<T> FuncToRunSync, Cx CustomCodeType = null, Mx CustomMethodContext = null, T ReturnOnError = default)
        {
            if (FuncToRunSync == null)
                return ReturnOnError;

            var t = CustomCodeType;
            Prf PerfLog = null;
            var ef = false;
            if (t.S != null && t.S.P == true)
                PerfLog = CurrLog.P(t, CustomMethodContext);

            T Result = ReturnOnError;

            if (CustomCodeType.T)
            {
                try
                {
                    Result = FuncToRunSync();
                }
                catch (Exception exc)
                {
                    if (t.S != null && t.S.L == true)
                        CurrLog.E(null, exc, t, CustomMethodContext);
                    ef = true;                    
                }
            }
            else
                Result = FuncToRunSync();

            if (t.S != null && t.S.P == true)
                CurrLog.Pa(PerfLog, null, t);

            if(ef)
                return ReturnOnError;

            return Result;
        }

        /// <summary>
        /// Run Common Async Try FuncToRunAsync With Errors And Performance Logging based on CurrContext and optional CustomCodeType.
        /// </summary>        
        public static async Task<T> R<T, Tx>(this Ax<Tx> CurrContext, f<Cx, Task<T>> FuncToRunAsync, Tx CustomCodeType = default, Mx CustomMethodContext = null, T ReturnOnError = default)
        {
            if (CurrContext == null || FuncToRunAsync == null)
                return ReturnOnError;
            Cx x = null;
            if (CustomCodeType != null && CurrContext.T != null)
                x = CurrContext.T.v(CustomCodeType);
            return await CurrContext.L.R(() => FuncToRunAsync(x), x, CustomMethodContext, ReturnOnError);
        }

        /// <summary>
        /// Run Common Async Try FuncToRunAsync With Errors And Performance Logging based on CurrCodeType.
        /// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        /// </summary>        
        public static async Task<T> R<T>(this Lg CurrLog, f<Task<T>> FuncToRunAsync, Cx CustomCodeType = null, Mx CustomMethodContext = null, T ReturnOnError = default)
        {
            if (FuncToRunAsync == null)
                return ReturnOnError;

            var t = CustomCodeType;
            Prf PerfLog = null;
            var ef = false;
            if (t.S != null && t.S.P == true)
                PerfLog = CurrLog.P(t, CustomMethodContext);

            T Result = ReturnOnError;

            if (CustomCodeType.T)
            {
                try
                {
                    Result = await FuncToRunAsync();
                }
                catch (Exception exc)
                {
                    if (t.S != null && t.S.L == true)
                        CurrLog.E(null, exc, t, CustomMethodContext);
                    ef = true;
                }
            }
            else
                Result = await FuncToRunAsync();

            if (t.S != null && t.S.P == true)
                CurrLog.Pa(PerfLog, null, t);

            if (ef)
                return ReturnOnError;

            return Result;
        }

        ///// <summary>
        ///// Run Common Try Code Schema Block for ProcessToRun With Errors And Performance Logging based on CurrCodeType.
        ///// Schema : Init --> Pre --> Validate --> 
        ///// try[CustomUseTryCatch or CurrCodeType] 
        ///// ((Process or ValidFalse or ValidNull) or Fail) with Error OnErrorCatch
        ///// --> Post --> End.
        ///// Check meyhods Init and End methods return bool? results and run CheckTrue/CheckFalse/CheckNull methods if available
        ///// or return default on not true. 
        ///// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        ///// </summary>        
        //public static ResT R<ParamT, ResT>(this Lg CurrLog, Pr<ParamT, ResT> ProcessToRun, Cx CustomCodeType = null)
        //{
        //    var t = CustomCodeType;
        //    bool UseTryCatch = t != null && t.T;
        //    Prf PerfLog = null;

        //    bool UsePerformanceLog = t != null && t.S != null && t.S.Cp == true,
        //        UseErrorLog = t != null && t.S != null && t.S.Cl == true;

        //    if (UsePerformanceLog)
        //        PerfLog = CurrLog.P(t);

        //    Pr<ParamT, ResT> Res = ProcessToRun;

        //    Res.Er = (param, result, exc, code) =>
        //    {
        //        if (UseErrorLog)
        //            CurrLog.E(null, exc, t);
        //        return (ProcessToRun.Er == null) ? result : ProcessToRun.Er(param, result, exc, t);
        //    };

        //    var ResultData = Ti(Res, t);

        //    if (UsePerformanceLog)
        //        CurrLog.P(PerfLog, null, t);
        //    return ResultData;
        //}

        #endregion

        #region Start Apps Logs Utilities

        /// <summary>
        /// Get new Sync Common App Context from current CurrSession with SaveLogsMethod and BaseContexts
        /// </summary>
        public static Ax<Tx> N<Tx>(this Sld CurrSession, f<Lst, bool> SyncSaveLogsMethod, IDictionary<Tx, Cx> BaseContexts = null)
        {
            return new Ax<Tx>(CurrSession, SyncSaveLogsMethod, BaseContexts);
        }

        /// <summary>
        /// Get new Async Common App Context from current CurrSession with SaveLogsMethod and BaseContexts
        /// </summary>
        public static Ax<Tx> N<Tx>(this Sld CurrSession, f<Lst, Task<bool>> AsyncSaveLogsMethod, IDictionary<Tx, Cx> BaseContexts = null)
        {
            return new Ax<Tx>(CurrSession, AsyncSaveLogsMethod, BaseContexts);
        }

        /// <summary>
        /// Get new Common SessionLog from current CurrentLogSettings to Log Init with MaxLogsLimit
        /// </summary>
        public static Sld N(this Sls CurrentLogSettings, string SolutionTitle = null, int? MaxLogsLimit = 10000)
        {
            var r = new Sld();

            r.I = _.g;
            r.A = Assembly.GetExecutingAssembly().ToString();

            r.S = CurrentLogSettings;
            r.M = MaxLogsLimit;
            r.D = _.D;
            r.Du = _.d;
            r.T = SolutionTitle;

            return r;
        }

        /// <summary>
        /// Get new Common Session Log Settings for current CollectPerfLogs to Log Init with error logs enabled
        /// </summary>
        public static Sls Ns(this bool CollectPerfLogs, bool CollectErrorDetails = true, bool CollectPerfDetails = true, bool CollectUsers = true, bool CollectOSData = true)
        {
            var r = new Sls();

            r.L = true;
            r.P = CollectPerfLogs;
            r.Ea = CollectErrorDetails;
            r.Pa = CollectPerfDetails;
            r.U = CollectUsers;
            r.O = CollectOSData;

            return r;
        }

        /// <summary>
        /// Set current Session Log Settings with additional details
        /// </summary>
        public static Sls sA(this Sls CurrSetts, bool CollectEnvironment = true, bool CollectProcess = true, bool CollectProcessDetails = true, bool CollectWindowsIdentity = true)
        {
            CurrSetts.En = CollectEnvironment;
            CurrSetts.Pr = CollectProcess;
            CurrSetts.Pp = CollectProcessDetails;
            CurrSetts.I = CollectWindowsIdentity;

            return CurrSetts;
        }

        /// <summary>
        /// Set current Session Log Settings with http logging details
        /// </summary>
        public static Sls sH(this Sls CurrSetts, bool CollectHttpRequests = true, bool CollectHttpSessions = true, bool CollectHttpContexts = true)
        {
            CurrSetts.Hr = CollectHttpRequests;
            CurrSetts.Hs = CollectHttpSessions;
            CurrSetts.Hc = CollectHttpContexts;

            return CurrSetts;
        }

        /// <summary>
        /// Set current Session Log Settings with web pages details
        /// </summary>
        public static Sls sW(this Sls CurrSetts, bool CollectBrowserDetails = true, bool CollectWebPages = true, bool CollectWebProfiles = true)
        {
            CurrSetts.B = CollectBrowserDetails;
            CurrSetts.Wp = CollectWebPages;
            CurrSetts.Wr = CollectWebProfiles;

            return CurrSetts;
        }

        #endregion

        #region Processes

        #region Code Common Patterns

        /// <summary>
        /// Run Common Code Schema without additional  code  
        /// Schema : Init --> Pre --> (Process  or Fail) --> Post --> End.
        /// Check methods Init and End methods return bool? results and run CheckTrue/CheckFalse/CheckNull methods if available
        /// or return default on not true
        /// </summary>        
        public static ResT F<ParamT, ResT>(Pr<ParamT, ResT> ProcessToRun, Cx CurrCodeType = null)
        {
            if ((ProcessToRun.Vt == null && ProcessToRun.F == null) || ProcessToRun.P == null)
                return ProcessToRun.D;

            if (!Code_FuncUtils.E(ProcessToRun, ProcessToRun.I, CurrCodeType))
                return ProcessToRun.D;

            ParamT CurrParam = ProcessToRun.P;
            if (ProcessToRun.Pe != null)
                CurrParam = ProcessToRun.Pe(ProcessToRun.P, CurrCodeType);

            ResT Result = ProcessToRun.D;
            if (ProcessToRun.Vt != null)
                Result = ProcessToRun.Vt(ProcessToRun.P, CurrCodeType);
            else
                Result = ProcessToRun.F(ProcessToRun.P, CurrCodeType);
            if (ProcessToRun.Ps != null)
                Result = ProcessToRun.Ps(ProcessToRun.P, Result, CurrCodeType);

            if (!Code_FuncUtils.E(ProcessToRun, ProcessToRun.E, Result, CurrCodeType))
                return ProcessToRun.D;

            return Result;
        }

        /// <summary>
        /// Run Common Try Code Schema  with Error Handling
        /// Schema : Init --> Pre --> Validate --> 
        /// try[CustomUseTryCatch or CurrCodeType] (Process or Fail) with Error OnErrorCatch
        /// --> Post --> End.
        /// Check methods Init and End methods return bool? results and run CheckTrue/CheckFalse/CheckNull methods if available
        /// or return default on not true
        /// </summary> 
        public static ResT T<ParamT, ResT>(Pr<ParamT, ResT> ProcessToRun, Cx CurrCodeType = null)
        {
            bool UseTryCatch = CurrCodeType != null && CurrCodeType.T;

            Pr<ParamT, ResT> Res = ProcessToRun;

            Res.Vt = (param, code) =>
            {
                ResT Result = ProcessToRun.D;
                if (UseTryCatch)
                {
                    try
                    {
                        if (ProcessToRun.Vt != null)
                            Result = ProcessToRun.Vt(param, CurrCodeType);
                        else
                            Result = ProcessToRun.F(param, CurrCodeType);
                    }
                    catch (Exception exc)
                    {
                        Result = ProcessToRun.D;
                        if (ProcessToRun.Er != null)
                            Result = ProcessToRun.Er(param, Result, exc, CurrCodeType);
                    }
                }
                else
                    if (ProcessToRun.Vt != null)
                    Result = ProcessToRun.Vt(param, CurrCodeType);
                else
                    Result = ProcessToRun.F(param, CurrCodeType);
                return Result;
            };

            return F(Res, CurrCodeType);
        }

        /// <summary>
        /// Run Common If Code Schema with Validation  
        /// Schema : Init --> Pre --> Validate --> 
        /// ((Process or ValidFalse or ValidNull) or Fail)
        /// --> Post --> End.
        /// Check methods Init and End methods return bool? results and run CheckTrue/CheckFalse/CheckNull methods if available
        /// or return default on not true
        /// </summary>        
        public static ResT I<ParamT, ResT>(Pr<ParamT, ResT> ProcessToRun, Cx CurrCodeType = null)
        {
            if (ProcessToRun.V == null && ProcessToRun.Vn == null)
                return ProcessToRun.D;

            Pr<ParamT, ResT> Res = ProcessToRun;

            Res.Vt = (param, code) =>
            {
                bool? Validated = true;
                ResT Result = ProcessToRun.D;
                if (ProcessToRun.V != null)
                    Validated = ProcessToRun.V(param, CurrCodeType);

                if (Validated == null)
                {
                    if (ProcessToRun.Vn != null)
                        Result = ProcessToRun.Vn(param, CurrCodeType);
                    else
                        if (ProcessToRun.F != null)
                        Result = ProcessToRun.F(param, CurrCodeType);
                }
                else
                    if (Validated == false)
                {
                    if (ProcessToRun.Vf != null)
                        Result = ProcessToRun.Vf(param, CurrCodeType);
                    else
                        if (ProcessToRun.F != null)
                        Result = ProcessToRun.F(param, CurrCodeType);
                }
                else
                        if (ProcessToRun.Vt != null)
                    Result = ProcessToRun.Vt(param, CurrCodeType);
                else
                            if (ProcessToRun.F != null)
                    Result = ProcessToRun.F(param, CurrCodeType);
                return Result;
            };

            return F(Res, CurrCodeType);
        }

        /// <summary>
        /// Run Common Try Code Schema Block With Errors And Performance Logging based on CurrCodeType.
        /// Schema : Init --> Pre --> Validate --> 
        /// try[CustomUseTryCatch or CurrCodeType] 
        /// ((Process or ValidFalse or ValidNull) or Fail) with Error OnErrorCatch
        /// --> Post --> End.
        /// Check methods Init and End methods return bool? results and run CheckTrue/CheckFalse/CheckNull methods if available
        /// or return default on not true
        /// </summary> 
        public static ResT Ti<ParamT, ResT>(Pr<ParamT, ResT> ProcessToRun, Cx CurrCodeType = null)
        {
            bool UseTryCatch = CurrCodeType != null && CurrCodeType.T;

            Pr<ParamT, ResT> Res = ProcessToRun;

            Res.Vt = (param, code) =>
            {
                bool? Validated = true;
                ResT Result = ProcessToRun.D;
                if (ProcessToRun.V != null)
                    Validated = ProcessToRun.V(param, CurrCodeType);

                if (Validated == null)
                {
                    if (ProcessToRun.Vn != null)
                        Result = ProcessToRun.Vn(param, CurrCodeType);
                    else
                        if (ProcessToRun.F != null)
                        Result = ProcessToRun.F(param, CurrCodeType);
                }
                else
                    if (Validated == false)
                {
                    if (ProcessToRun.Vf != null)
                        Result = ProcessToRun.Vf(param, CurrCodeType);
                    else
                        if (ProcessToRun.F != null)
                        Result = ProcessToRun.F(param, CurrCodeType);
                }
                else
                        if (ProcessToRun.Vt != null)
                    Result = ProcessToRun.Vt(param, CurrCodeType);
                else
                            if (ProcessToRun.F != null)
                    Result = ProcessToRun.F(param, CurrCodeType);
                return Result;
            };

            return T(Res, CurrCodeType);
        }

        #endregion

        #region For Each
                
        #endregion

        #region Switches

        //public static IDictionary<KeyT, ResT> Case<KeyT, ValueT, ResT>(IDictionary<KeyT, ValueT> CasesToSelect, IDictionary<KeyT, _f<KeyT, ValueT, ResT>> CasesToProcess)
        //{

        //}

        #endregion

        #endregion

        #region Additional utilities

        /// <summary>
        /// Get Memory Usage Text
        /// </summary>
        public static string T(this Mvt ValueType)
        {
            var v = Process.GetCurrentProcess().WorkingSet64;
            if (ValueType == Mvt.Mb)
                return v / Mbv + " Mb";
            if (ValueType == Mvt.Gb)
                return v / Gbv + " Gb";
            return v / Kbv + " Kb";
        }

        /// <summary>
        /// Get Memory Usage value
        /// </summary>
        public static double V(this Mvt ValueType)
        {
            var v = Process.GetCurrentProcess().WorkingSet64;
            if (ValueType == Mvt.Mb)
                return v / Mbv;
            if (ValueType == Mvt.Gb)
                return v / Gbv;
            return v / Kbv;
        }

        #endregion
    }

    /// <summary>
    /// Default Common Applications Props and values. Methods to generate new items, common data, types and environment variables 
    /// </summary>
    public static class _
    {
        #region Properties
        /// <summary>
        /// Return current DateTime Now
        /// </summary>
        public static DateTime D { get { return DateTime.Now; } }
        /// <summary>
        /// Return current DateTime Utc Now
        /// </summary>
        public static DateTime d { get { return DateTime.UtcNow; } }
        /// <summary>
        /// Return New Guid
        /// </summary>
        public static Guid g { get { return Guid.NewGuid(); } }

        #region Environment
        /// <summary>
        /// Return Environment UserName
        /// </summary>
        public static string u { get { return Environment.UserName; } }
        /// <summary>
        /// Return Environment UserDomainName
        /// </summary>
        public static string ud { get { return Environment.UserDomainName; } }

        /// <summary>
        /// Return Environment MachineName
        /// </summary>
        public static string m { get { return Environment.MachineName; } }

        /// <summary>
        /// Return Environment CurrentDirectory
        /// </summary>
        public static string cd { get { return Environment.CurrentDirectory; } }
        #endregion

        /// <summary>
        /// Return new Empty List of strings
        /// </summary>
        public static List<string> s { get { return new List<string>(); } }

        /// <summary>
        /// Return new Empty DataTable
        /// </summary>
        public static DataTable t { get { return new DataTable(); } }

        #endregion

        #region Return new methods
        /// <summary>
        /// Return new Empty List of current Type
        /// </summary>
        public static List<T> l<T>(IEnumerable<T> DataToAdd = null) 
        { 
            if (DataToAdd == null)
                return new List<T>();
            return DataToAdd.l(); 
        }

        /// <summary>
        /// Return new Empty IEnumerable of current Type
        /// </summary>
        public static IEnumerable<T> i<T>() { return new List<T>(); }

        /// <summary>
        /// Return new Empty Dictionary with current Key and Value Types
        /// </summary>
        public static IDictionary<KeyT, ValueT> dc<KeyT, ValueT>() { return new Dictionary<KeyT, ValueT>(); }

        /// <summary>
        /// Create Empty KeyValuePair with current Types
        /// </summary>
        public static KeyValuePair<KeyT, ValueT> k<KeyT, ValueT>(KeyT Key, ValueT Value = default(ValueT))
        {
            return new KeyValuePair<KeyT, ValueT>(Key, Value);
        }

        /// <summary>
        /// Create Empty Nullable KeyValuePair? with current Types
        /// </summary>
        public static KeyValuePair<KeyT, ValueT>? kn<KeyT, ValueT>()
        {
            return new KeyValuePair<KeyT, ValueT>?();
        }
        #endregion

        #region SQL types

        public const SqlDbType qn = SqlDbType.NVarChar;
        public const SqlDbType qib = SqlDbType.BigInt;
        public const SqlDbType qbn = SqlDbType.Binary;
        public const SqlDbType qb = SqlDbType.Bit;
        public const SqlDbType qc = SqlDbType.Char;
        public const SqlDbType qd = SqlDbType.DateTime2;
        public const SqlDbType qdc = SqlDbType.Decimal;
        public const SqlDbType qf = SqlDbType.Float;
        public const SqlDbType qi = SqlDbType.Int;
        public const SqlDbType qg = SqlDbType.UniqueIdentifier;
        public const SqlDbType qt = SqlDbType.VarBinary;
        //public const SqlDbType qn = SqlDbType.NVarChar;
        //public const SqlDbType qn = SqlDbType.NVarChar;
        //public const SqlDbType qn = SqlDbType.NVarChar;

        #endregion 

        #region App Exec Context utils

        
                
        #endregion
    }

    #region Code Data Structs Utils

    /// <summary>
    /// Code Type Utils
    /// </summary>
    public static class Code_Type_Utils
    {
        /// <summary>
        /// Create new Code Context for current Settings with UseTryCatch, LogError and Elevated setting
        /// </summary>
        public static Cx X(this Sls Settings, bool UseTryCatch = true, bool LogError = true, bool Elevated = false)
        {
            var Res = new Cx() { S = new Sls() };

            Res.T = UseTryCatch;
            Res.S = Settings;
            Res.L = LogError;
            Res.E = Elevated;

            return Res;
        }
    }

    /// <summary>
    /// CodeCommon Utils
    /// </summary>
    public static class Code_CommonUtils
    {
        /// <summary>
        /// Create new Common method execution details for common code with current LogIndex setting
        /// </summary>
        public static Mx M(this int LogIndex, string ClassName = null, string MethodName = null, string CustomDetails = null, string ParametersData = null, int? ArraySize = null, Ab Block = null, bool LogError = true)
        {
            Mx Res = new Mx();

            Res.C = ClassName;
            Res.M = MethodName;
            Res.L = LogError;
            Res.P = ParametersData;
            Res.D = CustomDetails;
            Res.As = ArraySize;
            Res.Li = LogIndex;
            Res.B = Block;

            return Res;
        }

        /// <summary>
        /// Create new Common method execution details for common code with current LogError setting
        /// </summary>
        public static Mx M(this bool LogError, string ClassName = null, string MethodName = null, string CustomDetails = null, string ParametersData = null, int? ArraySize = null, Ab Block = null, int? LogIndex = null)
        {
            Mx Res = new Mx();

            Res.C = ClassName;
            Res.M = MethodName;
            Res.L = LogError;
            Res.P = ParametersData;
            Res.D = CustomDetails;
            Res.As = ArraySize;
            Res.Li = LogIndex;
            Res.B = Block;

            return Res;
        }
       
        /// <summary>
        /// Sets method context Arrays data for current CurrCode - ArraySize, ArrayBlockSize, CustomArrayBlockMessage
        /// </summary>
        public static Mx S(this Mx CurrCode, int? ArraySize = null, int? ArrayBlockSize = null, string CustomArrayBlockMessage = null)
        {
            Mx Res = new Mx();

            if (ArrayBlockSize != null || CustomArrayBlockMessage != null)
                CurrCode.B = new Ab() { S = ArrayBlockSize, M = CustomArrayBlockMessage };
            CurrCode.As = ArraySize;

            return Res;
        }

        /// <summary>
        /// Sets logs destinations for current CurrCode - LogSQL_ConnString, LogXML_DirPath
        /// </summary>
        public static Mx S(this Mx CurrCode, string LogSQL_ConnString = null, string LogXML_DirPath = null)
        {
            Mx Res = new Mx();

            CurrCode.Lq = LogSQL_ConnString;
            CurrCode.Lx = LogXML_DirPath;

            return Res;
        }
    }

    /// <summary>
    /// Code Func Lambda expressions Utils
    /// </summary>
    public static class Code_FuncUtils
    {
        /// <summary>
        /// Evaluate common bool? return from CheckToEvaluate with context CheckFalse/CheckNull/CheckTrue methods details
        /// Returns true if any method return true, else false
        /// </summary>        
        /// <returns>Returns true if any method return true, else false</returns>
        public static bool E<ParamT, ResT>(this Pr<ParamT, ResT> CurrProcess, f<ParamT, Cx, bool?> CheckToEvaluate, Cx CurrCodeType = null, ResT ResToEvaluate = default(ResT), bool EvaluateDefault = true)
        {
            if (CheckToEvaluate == null)
                return EvaluateDefault;
            bool? Res = CheckToEvaluate(CurrProcess.P, CurrCodeType);

            if (Res == null && CurrProcess.Cn != null)
                return CurrProcess.Cn(CurrProcess.P, ResToEvaluate, CurrCodeType);
            if (Res == false && CurrProcess.Cf != null)
                return !CurrProcess.Cf(CurrProcess.P, ResToEvaluate, CurrCodeType);
            if (Res == true && CurrProcess.Ct != null)
                return !CurrProcess.Ct(CurrProcess.P, ResToEvaluate, CurrCodeType);

            if (Res == true)
                return true;
            return false;
        }

        /// <summary>
        /// Evaluate common bool? return from CheckToEvaluate with context CheckFalse/CheckNull/CheckTrue methods details
        /// Returns true if any method return true, else false
        /// </summary>        
        /// <returns>Returns true if any method return true, else false</returns>
        public static bool E<ParamT, ResT>(this Pr<ParamT, ResT> CurrProcess, f<ParamT, ResT, Cx, bool?> CheckToEvaluate, ResT ResToEvaluate = default(ResT), Cx CurrCodeType = null, bool EvaluateDefault = true)
        {
            if (CheckToEvaluate == null)
                return EvaluateDefault;
            bool? Res = CheckToEvaluate(CurrProcess.P, ResToEvaluate, CurrCodeType);

            if (Res == null && CurrProcess.Cn != null)
                return CurrProcess.Cn(CurrProcess.P, ResToEvaluate, CurrCodeType);
            if (Res == false && CurrProcess.Cf != null)
                return !CurrProcess.Cf(CurrProcess.P, ResToEvaluate, CurrCodeType);
            if (Res == true && CurrProcess.Ct != null)
                return !CurrProcess.Ct(CurrProcess.P, ResToEvaluate, CurrCodeType);

            if (Res == true)
                return true;
            return false;
        }

        /// <summary>
        /// Create new Func and add AddOn method with Original with AddAfterOriginal order
        /// </summary>        
        public static f<ParamT, Cx, ResT> A<ParamT, ResT>(this f<ParamT, Cx, ResT> Original, f<ParamT, Cx, ResT> AddOn, Cx CurrCodeType = null, bool AddAfterOriginal = false)
        {
            if (Original == null && AddOn == null)
                return null;

            return (arr, code) =>
            {
                var Res = default(ResT);

                if (!AddAfterOriginal && AddOn != null)
                    Res = AddOn(arr, code);

                if (Original != null)
                    Res = Original(arr, code);

                if (AddAfterOriginal && AddOn != null)
                    Res = AddOn(arr, code);
                return Res;
            };
        }

        /// <summary>
        /// Create new Func and add AddOn method with Original with AddAfterOriginal order
        /// </summary>        
        public static f<ParamT, ResT, Cx, ResT> A<ParamT, ResT>(this f<ParamT, ResT, Cx, ResT> Original, f<ParamT, ResT, Cx, ResT> AddOn, Cx CurrCodeType = null, bool AddAfterOriginal = false)
        {
            if (Original == null && AddOn == null)
                return null;

            return (key, arr, code) =>
            {
                var Res = default(ResT);

                if (!AddAfterOriginal && AddOn != null)
                    Res = AddOn(key, arr, code);

                if (Original != null)
                    Res = Original(key, arr, code);

                if (AddAfterOriginal && AddOn != null)
                    Res = AddOn(key, arr, code);
                return Res;
            };
        }

        /// <summary>
        /// Create new custom process with specified methods
        /// </summary>        
        public static Pr<ParamT, ResT> E<ParamT, ResT>(this ParamT StartParam, f<ParamT, Cx, ResT> ProcessFunc, Cx CurrCodeType = null, f<ParamT, Cx, ResT> ProcessFail = null, f<ParamT, ResT, Exception, Cx, ResT> ProcessError = null)
        {
            Pr<ParamT, ResT> Result = new Pr<ParamT, ResT>();

            Result.P = StartParam;
            if (ProcessFunc == null)
                Result.Vt = ProcessFunc;
            if (ProcessFail == null)
                Result.F = ProcessFail;
            if (ProcessError == null)
                Result.Er = ProcessError;

            return Result;
        }

    }

    #endregion

    #region Code Data Structs

    /// <summary>
    /// Global Applications Common Execution Context with Typed Contexts collection
    /// </summary>
    public class Ax<Tx> : D<bool>
    {
        /// <summary>
        /// Base default app Code Contexts Typed collection
        /// </summary>
        public IDictionary<Tx,Cx> T;
        /// <summary>
        /// Application logging
        /// </summary>
        public Lg L;

        /// <summary>
        /// Get Common Sync Context with errors and performance with process details
        /// </summary>
        public Ax(Sld CurrSession, f<Lst, bool> SyncSaveLogsMethod, IDictionary<Tx, Cx> BaseContexts = null)
            : base(null, true)
        {
            T = BaseContexts;
            L = CurrSession.N(SyncSaveLogsMethod);
            this.M = () =>
            {
                if (L != null)
                    L.Sv();
                return true;
            };
        }


        /// <summary>
        /// Get Common Async Context with errors and performance with process details
        /// </summary>
        public Ax(Sld CurrSession, f<Lst, Task<bool>> AsyncSaveLogsMethod, IDictionary<Tx, Cx> BaseContexts = null)
            : base(null, true)
        {
            T = BaseContexts;
            L = CurrSession.N(AsyncSaveLogsMethod);
            this.M = () =>
            {
                if (L != null)
                {
                    var r = L.Sva();
                    if (r != null)
                    {
                        var rs = r.Result;
                    }                        
                }
                    
                return true;
            };
        }
    }
        
    /// <summary>
    /// Common Process Data Functions
    /// </summary>    
    public class Pr<ParamT, ResT>
    {
        /// <summary>
        /// Params To Start Process
        /// </summary>
        public ParamT P;
        /// <summary>
        /// Check Params on Start
        /// </summary>
        public f<ParamT, Cx, bool?> I;
        /// <summary>
        /// Pre Process
        /// </summary>
        public f<ParamT, Cx, ParamT> Pe;
        /// <summary>
        /// Validate Process Type For Params
        /// </summary>
        public f<ParamT, Cx, bool?> V;
        /// <summary>
        /// Process Params on Validate True (or Validate Null)
        /// </summary>
        public f<ParamT, Cx, ResT> Vt;
        /// <summary>
        /// Process Params on Validate False 
        /// </summary>
        public f<ParamT, Cx, ResT> Vf;
        /// <summary>
        /// Process Params on Validate Null
        /// </summary>
        public f<ParamT, Cx, ResT> Vn;
        /// <summary>
        /// Run On Main Process Is Null
        /// </summary>
        public f<ParamT, Cx, ResT> F;
        /// <summary>
        /// Post Process
        /// </summary>
        public f<ParamT, ResT, Cx, ResT> Ps;
        /// <summary>
        /// Check Result
        /// </summary>
        public f<ParamT, ResT, Cx, bool?> E;
        /// <summary>
        /// Custom Use Try Catch Error Handler
        /// </summary>
        public f<ParamT, ResT, Exception, Cx, ResT> Er;
        /// <summary>
        /// Run On Check Is Null
        /// </summary>
        public f<ParamT, ResT, Cx, bool> Cn;
        /// <summary>
        /// Run On Check Is True
        /// </summary>
        public f<ParamT, ResT, Cx, bool> Ct;
        /// <summary>
        /// Run On Check Is False
        /// </summary>
        public f<ParamT, ResT, Cx, bool> Cf;
        /// <summary>
        /// Return Default On Error
        /// </summary>
        public ResT D;
        /// <summary>
        /// Custom Use Try Catch 
        /// </summary>
        public f<ParamT, Cx, bool?> Tc;
        /// <summary>
        /// Re Throw Error On Error Catch
        /// </summary>
        public bool? Re;
    }

    /// <summary>
    /// Memory value Type - Kb, Mb, Gb
    /// </summary>
    public enum Mvt { Kb, Mb, Gb };

    #endregion
}
