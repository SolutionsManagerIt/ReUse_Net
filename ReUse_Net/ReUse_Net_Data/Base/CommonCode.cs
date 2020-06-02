﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text;
//using CommonStructsDataUtilities.Structs.System;
using System.Data;
using System.Diagnostics;
//using CommonStructsDataUtilities.Logs;
//using CommonStructsDataUtilities.Logs_old;
using System.Data.SqlClient;
using ReUse.Common;
using ReUse.Base.Performance;

namespace ReUse.Base
{
    /// <summary>
    /// Common Code Schema Utilities  -  Run, Try, Loop, If, Process, For, Each, etc
    /// </summary>
    public static class C
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
        public static T R<T>(this Ax CurrContext, f<T> FuncToRun, Cx? CustomCodeType = null, T ReturnOnError = default(T))
        {
            if (FuncToRun == null)
                return ReturnOnError;
            var c = CurrContext ?? _.C;
            var t = c.T;
            if (CustomCodeType != null)
                t = CustomCodeType.Value;

            return t.R(FuncToRun, c.L, ReturnOnError);
        }

        /// <summary>
        /// Run Common Try FuncToRun With Errors And Performance Logging based on CurrCodeType.
        /// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        /// </summary>        
        public static T R<T>(this Cx CurrCodeType, f<T> FuncToRun, _Log? CustomLogToAddRecords = null, T ReturnOnError = default(T))
        {
            if (FuncToRun == null)
                return ReturnOnError;

            var l = _.C.L;
            if (CustomLogToAddRecords != null)
                l = CustomLogToAddRecords.Value;

            PerformanceLog? PerfLog = null;

            if (CurrCodeType.Cp.L)
                PerfLog = l._P(CurrCodeType);

            T Result = ReturnOnError;

            if (CurrCodeType.T)
            {
                try
                {
                    Result = FuncToRun();
                }
                catch (Exception exc)
                {
                    if (CurrCodeType.Ce.L)
                        l._E(null, exc, CurrCodeType);
                    return ReturnOnError;
                }
            }
            else
                Result = FuncToRun();

            if (CurrCodeType.Cp.L)
                l._P(PerfLog, null, CurrCodeType);

            return Result;
        }

        /// <summary>
        /// Run Common Try FuncToRun With Performance only Logging based on CurrCodeType for current MethodName.
        /// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        /// </summary>        
        public static T _Rp<T>(this string MethodName, f<T> FuncToRun, T ReturnOnError = default(T), string ClassName = null, Cx? CurrCodeType = null, string PerformanceComments = null, _Log? CustomLogToAddRecords = null, bool GetPerformanceStats = true)
        {
            if (FuncToRun == null)
                return ReturnOnError;
            return MethodName._R(FuncToRun, ReturnOnError, ClassName, CurrCodeType, null, PerformanceComments, false, GetPerformanceStats, CustomLogToAddRecords);
        }

        /// <summary>
        /// Run Common Try FuncToRun With Errors only Logging based on CurrCodeType.
        /// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        /// </summary>        
        public static T _Re<T>(this string MethodName, f<T> FuncToRun, T ReturnOnError = default(T), string ClassName = null, Cx? CurrCodeType = null, string CustomErrorMessage = null, _Log? CustomLogToAddRecords = null)
        {
            if (FuncToRun == null)
                return ReturnOnError;
            return MethodName._R(FuncToRun, ReturnOnError, ClassName, CurrCodeType, CustomErrorMessage, null, true, false, CustomLogToAddRecords);
        }

        /// <summary>
        /// Run Common Try FuncToRun With Errors And Performance Logging based on CurrCodeType.
        /// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        /// </summary>        
        public static T _R<T>(this string MethodName, f<T> FuncToRun, T ReturnOnError = default(T), string ClassName = null, Cx? CurrCodeType = null, string CustomErrorMessage = null, string PerformanceComments = null, bool SendLogOnError = true, bool GetPerformanceStats = true, _Log? CustomLogToAddRecords = null)
        {
            if (FuncToRun == null)
                return ReturnOnError;

            PerformanceLog? PerfLog = null;
            var l = _.C.L;
            if (CustomLogToAddRecords != null)
                l = CustomLogToAddRecords.Value;
            bool UsePerformanceLog = GetPerformanceStats && CurrCodeType != null && CurrCodeType.Value.Cp.L;

            if (UsePerformanceLog)
                PerfLog = l._P(CurrCodeType);

            T Result = ReturnOnError;

            if (CurrCodeType != null && CurrCodeType.Value.T)
            {
                try
                {
                    Result = FuncToRun();
                }
                catch (Exception exc)
                {
                    if (SendLogOnError)
                        l._E(ClassName, MethodName, CustomErrorMessage, exc, CurrCodeType);
                    return ReturnOnError;
                }
            }
            else
                Result = FuncToRun();

            if (UsePerformanceLog)
                l._P(PerfLog, ClassName, MethodName, PerformanceComments, CurrCodeType);

            return Result;
        }

        /// <summary>
        /// Run Common Try Code Schema Block for ProcessToRun With Errors And Performance Logging based on CurrCodeType.
        /// Schema : Init --> Pre --> Validate --> 
        /// try[CustomUseTryCatch or CurrCodeType] 
        /// ((Process or ValidFalse or ValidNull) or Fail) with Error OnErrorCatch
        /// --> Post --> End.
        /// Check meyhods Init and End methods return bool? results and run CheckTrue/CheckFalse/CheckNull methods if available
        /// or return default on not true. 
        /// Logging is saved to CustomLogToAddRecords or default static logs (_.D.L)
        /// </summary>        
        public static ResT _R<ParamT, ResT>(this Proc<ParamT, ResT> ProcessToRun, Cx? CurrCodeType = null, _Log? CustomLogToAddRecords = null)
        {
            bool UseTryCatch = CurrCodeType != null && CurrCodeType.Value.T;
            PerformanceLog? PerfLog = null;
            var l = _.C.L;
            if (CustomLogToAddRecords != null)
                l = CustomLogToAddRecords.Value;
            bool UsePerformanceLog = CurrCodeType != null && CurrCodeType.Value.Cp.L,
                UseErrorLog = CurrCodeType != null && CurrCodeType.Value.Ce.L;

            if (UsePerformanceLog)
                PerfLog = l._P(CurrCodeType);

            Proc<ParamT, ResT> Res = ProcessToRun;

            Res.Error = (param, result, exc, code) =>
            {
                if (UseErrorLog)
                    l._E(null, exc, CurrCodeType);
                return (ProcessToRun.Error == null) ? result : ProcessToRun.Error(param, result, exc, CurrCodeType);
            };

            var ResultData = TryIf(Res, CurrCodeType);

            if (UsePerformanceLog)
                l._P(PerfLog, null, CurrCodeType);
            return ResultData;
        }


        #endregion

        #region Start App

        /// <summary>
        /// Start App method with logs and performance (old format)
        /// </summary>        
        public static T AppS<T>(string SolutionName, f<T> FuncToRun, T ReturnOnError = default(T), Cx? FuncRunContext = null, Cx? CustomLogs = null, Cx? CustomPerf = null)
        {
            //Log.CurrLogsCode = FuncRunContext ?? CustomLogs;
            //Log.Start();
            //Prf.CurrLogsCode = FuncRunContext ?? CustomPerf;
            //Prf.Start();

            T Result = (FuncRunContext == null ? _.C.T : FuncRunContext.Value).R(() => { return FuncToRun(); }); //, ReturnOnError);

            //Prf.Save();
            //Prf.End();
            //Log.Save();
            //Log.End();

            return Result;
        }

        ///// <summary>
        ///// Start App method with parameters
        ///// </summary>        
        //public static ResT AppS<KeyT, ValueT, ResT>(string SolutionName, KeyT KeyParam, ValueT Value, _f<KeyT, ValueT, ResT> FuncToRun, ResT ReturnOnError = default(ResT), CodeType? CurrCodeType = null, string ClassName = null, string MethodName = null, string CustomErrorMessage = null, string PerformanceComments = null)
        //{
        //    T Result = ReturnOnError;

        //    Log.CurrLogsCode = CurrCodeType;
        //    Log.Start();

        //    Result = FuncToRun();

        //    return Result;
        //}

        #endregion

        #region Processes

        #region Code Common Patterns

        /// <summary>
        /// Run Common Code Schema without additional  code  
        /// Schema : Init --> Pre --> (Process  or Fail) --> Post --> End.
        /// Check methods Init and End methods return bool? results and run CheckTrue/CheckFalse/CheckNull methods if available
        /// or return default on not true
        /// </summary>        
        public static ResT F<ParamT, ResT>(Proc<ParamT, ResT> ProcessToRun, Cx? CurrCodeType = null)
        {
            if ((ProcessToRun.Process == null && ProcessToRun.Fail == null) || ProcessToRun.Params == null)
                return ProcessToRun.Default;

            if (!C_Func.Eval(ProcessToRun, ProcessToRun.Init, CurrCodeType))
                return ProcessToRun.Default;

            ParamT CurrParam = ProcessToRun.Params;
            if (ProcessToRun.Pre != null)
                CurrParam = ProcessToRun.Pre(ProcessToRun.Params, CurrCodeType);

            ResT Result = ProcessToRun.Default;
            if (ProcessToRun.Process != null)
                Result = ProcessToRun.Process(ProcessToRun.Params, CurrCodeType);
            else
                Result = ProcessToRun.Fail(ProcessToRun.Params, CurrCodeType);
            if (ProcessToRun.Post != null)
                Result = ProcessToRun.Post(ProcessToRun.Params, Result, CurrCodeType);

            if (!C_Func.Eval(ProcessToRun, ProcessToRun.End, Result, CurrCodeType))
                return ProcessToRun.Default;

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
        public static ResT Try<ParamT, ResT>(Proc<ParamT, ResT> ProcessToRun, Cx? CurrCodeType = null)
        {
            bool UseTryCatch = CurrCodeType != null && CurrCodeType.Value.T;

            Proc<ParamT, ResT> Res = ProcessToRun;

            Res.Process = (param, code) =>
            {
                ResT Result = ProcessToRun.Default;
                if (UseTryCatch)
                {
                    try
                    {
                        if (ProcessToRun.Process != null)
                            Result = ProcessToRun.Process(param, CurrCodeType);
                        else
                            Result = ProcessToRun.Fail(param, CurrCodeType);
                    }
                    catch (Exception exc)
                    {
                        Result = ProcessToRun.Default;
                        if (ProcessToRun.Error != null)
                            Result = ProcessToRun.Error(param, Result, exc, CurrCodeType);
                    }
                }
                else
                    if (ProcessToRun.Process != null)
                    Result = ProcessToRun.Process(param, CurrCodeType);
                else
                    Result = ProcessToRun.Fail(param, CurrCodeType);
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
        public static ResT If<ParamT, ResT>(Proc<ParamT, ResT> ProcessToRun, Cx? CurrCodeType = null)
        {
            if (ProcessToRun.Validate == null && ProcessToRun.ValidNull == null)
                return ProcessToRun.Default;

            Proc<ParamT, ResT> Res = ProcessToRun;

            Res.Process = (param, code) =>
            {
                bool? Validated = true;
                ResT Result = ProcessToRun.Default;
                if (ProcessToRun.Validate != null)
                    Validated = ProcessToRun.Validate(param, CurrCodeType);

                if (Validated == null)
                {
                    if (ProcessToRun.ValidNull != null)
                        Result = ProcessToRun.ValidNull(param, CurrCodeType);
                    else
                        if (ProcessToRun.Fail != null)
                        Result = ProcessToRun.Fail(param, CurrCodeType);
                }
                else
                    if (Validated == false)
                {
                    if (ProcessToRun.ValidFalse != null)
                        Result = ProcessToRun.ValidFalse(param, CurrCodeType);
                    else
                        if (ProcessToRun.Fail != null)
                        Result = ProcessToRun.Fail(param, CurrCodeType);
                }
                else
                        if (ProcessToRun.Process != null)
                    Result = ProcessToRun.Process(param, CurrCodeType);
                else
                            if (ProcessToRun.Fail != null)
                    Result = ProcessToRun.Fail(param, CurrCodeType);
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
        public static ResT TryIf<ParamT, ResT>(Proc<ParamT, ResT> ProcessToRun, Cx? CurrCodeType = null)
        {
            bool UseTryCatch = CurrCodeType != null && CurrCodeType.Value.T;

            Proc<ParamT, ResT> Res = ProcessToRun;

            Res.Process = (param, code) =>
            {
                bool? Validated = true;
                ResT Result = ProcessToRun.Default;
                if (ProcessToRun.Validate != null)
                    Validated = ProcessToRun.Validate(param, CurrCodeType);

                if (Validated == null)
                {
                    if (ProcessToRun.ValidNull != null)
                        Result = ProcessToRun.ValidNull(param, CurrCodeType);
                    else
                        if (ProcessToRun.Fail != null)
                        Result = ProcessToRun.Fail(param, CurrCodeType);
                }
                else
                    if (Validated == false)
                {
                    if (ProcessToRun.ValidFalse != null)
                        Result = ProcessToRun.ValidFalse(param, CurrCodeType);
                    else
                        if (ProcessToRun.Fail != null)
                        Result = ProcessToRun.Fail(param, CurrCodeType);
                }
                else
                        if (ProcessToRun.Process != null)
                    Result = ProcessToRun.Process(param, CurrCodeType);
                else
                            if (ProcessToRun.Fail != null)
                    Result = ProcessToRun.Fail(param, CurrCodeType);
                return Result;
            };

            return Try(Res, CurrCodeType);
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
        public static string _Mu(this Mvt ValueType)
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
        public static double _Muv(this Mvt ValueType)
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
    /// Default Common Applications Props and
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

        /// <summary>
        /// Return new List of SqlParameters
        /// </summary>
        public static List<SqlParameter> p { get { return new List<SqlParameter>(); } }

        /// <summary>
        /// Return new List of strings
        /// </summary>
        public static List<string> s { get { return new List<string>(); } }


        #endregion

        #region Fields
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
        #endregion



        #region App Exec Context utils

        /// <summary>
        /// Base default Application Execution Context (with default logs from static old format code)
        /// </summary>
        public static Ax C = _Ge(true);
        /// <summary>
        /// Application logging
        /// </summary>
        //public _Log L;

        /// <summary>
        /// Get Common Context with errors and performance with CollectPerfProcessDetails
        /// </summary>
        public static Ax _Ge(this bool LogPerformance, bool CollectPerfProcessDetails = true)
        {
            return new Ax() { L = new _Log(), T = true._Te(LogPerformance, CollectPerfProcessDetails) };
        }

        /// <summary>
        /// Get Common Context with CommonCodeType
        /// </summary>
        public static Ax _G(this Cx CommonCodeType)
        {
            return new Ax() { L = new _Log(), T = CommonCodeType };
        }

        #endregion
    }

    #region Code Data Structs Utils

    /// <summary>
    /// Code Type Utils
    /// </summary>
    public static class C_Type
    {
        /// <summary>
        /// Create new CodeType for App with current code UseTryCatch setting
        /// </summary>
        public static Cx _Tn(this bool UseTryCatch, bool LogError = true, bool LogPerformance = true, bool? ErrorIsCritical = null, bool? GetPerformanceProcessData = null, int? ArrayItemSize = null, int? CodeIndex = null, string ParametersData = null, string CustomDetails = null)
        {
            var Res = new Cx();

            Res.T = UseTryCatch;
            Res.P = GetPerformanceProcessData;
            Res.Cr = ErrorIsCritical;

            Res.Cp = LogError._Cc(ParametersData, CustomDetails, ArrayItemSize, CodeIndex);
            Res.Ce = LogError._Cc(ParametersData, CustomDetails, ArrayItemSize, CodeIndex);

            return Res;
        }

        /// <summary>
        /// Create new CodeType for Error with current code UseTryCatch setting
        /// </summary>
        public static Cx _Te(this bool UseTryCatch, bool LogPerformance = true, bool? GetPerformanceProcessData = null, bool? ErrorIsCritical = null, int? ArrayItemSize = null, int? CodeIndex = null, string ParametersData = null, string CustomDetails = null)
        {
            var Res = new Cx();

            Res.T = UseTryCatch;
            Res.P = GetPerformanceProcessData;
            Res.Cr = ErrorIsCritical;

            Res.Cp = ParametersData._Ccl(CustomDetails, ArrayItemSize, CodeIndex);
            Res.Ce = ParametersData._Ccl(CustomDetails, ArrayItemSize, CodeIndex);

            return Res;
        }

        /// <summary>
        /// Create new CodeType for Error Only with current code UseTryCatch setting
        /// </summary>
        public static Cx _Teo(this bool UseTryCatch, bool? ErrorIsCritical = null, int? ArrayItemSize = null, int? CodeIndex = null, string ParametersData = null, string CustomDetails = null)
        {
            var Res = new Cx();

            Res.T = UseTryCatch;
            Res.P = null;
            Res.Cr = ErrorIsCritical;

            Res.Cp = false._Cc(ParametersData, CustomDetails, ArrayItemSize, CodeIndex);
            Res.Ce = ParametersData._Ccl(CustomDetails, ArrayItemSize, CodeIndex);

            return Res;
        }

        /// <summary>
        /// Create new CodeType for Perf with current code UseTryCatch setting
        /// </summary>
        public static Cx _Tp(this bool UseTryCatch, bool LogError = true, bool? ErrorIsCritical = null, bool? GetPerformanceProcessData = true, int? ArrayItemSize = null, int? CodeIndex = null, string ParametersData = null, string CustomDetails = null)
        {
            var Res = new Cx();

            Res.T = UseTryCatch;
            Res.P = GetPerformanceProcessData;
            Res.Cr = ErrorIsCritical;

            Res.Cp = ParametersData._Ccl(CustomDetails, ArrayItemSize, CodeIndex);
            Res.Ce = LogError._Cc(ParametersData, CustomDetails, ArrayItemSize, CodeIndex);

            return Res;
        }

        /// <summary>
        /// Create new CodeType for Common Perf with current code UseTryCatch setting
        /// </summary>
        public static Cx _Tpc(this bool UseTryCatch, bool LogError = true, bool? ErrorIsCritical = null, int? ArrayItemSize = null, int? CodeIndex = null, string ParametersData = null, string CustomDetails = null)
        {
            var Res = new Cx();

            Res.T = UseTryCatch;
            Res.P = null;
            Res.Cr = ErrorIsCritical;

            Res.Cp = ParametersData._Ccl(CustomDetails, ArrayItemSize, CodeIndex);
            Res.Ce = LogError._Cc(ParametersData, CustomDetails, ArrayItemSize, CodeIndex);

            return Res;
        }

        /// <summary>
        /// Create new CodeType for Perf with Details with current code UseTryCatch setting
        /// </summary>
        public static Cx _Tpd(this bool UseTryCatch, bool LogError = true, bool? ErrorIsCritical = null, int? ArrayItemSize = null, int? CodeIndex = null, string ParametersData = null, string CustomDetails = null)
        {
            var Res = new Cx();

            Res.T = UseTryCatch;
            Res.P = true;
            Res.Cr = ErrorIsCritical;

            Res.Cp = ParametersData._Ccl(CustomDetails, ArrayItemSize, CodeIndex);
            Res.Ce = LogError._Cc(ParametersData, CustomDetails, ArrayItemSize, CodeIndex);

            return Res;
        }

        /// <summary>
        /// Create new CodeType for Tests with current code UseTryCatch setting
        /// </summary>
        public static Cx _Tt(this bool UseTryCatch, int? ArrayItemSize = null, bool? GetPerformanceProcessData = true, bool LogError = true, string ParametersData = null, string CustomDetails = null, int? CodeIndex = null)
        {
            var Res = new Cx();

            Res.T = UseTryCatch;
            Res.P = GetPerformanceProcessData;
            Res.Cr = null;

            Res.Cp = ParametersData._Ccl(CustomDetails, ArrayItemSize, CodeIndex);
            Res.Ce = LogError._Cc(ParametersData, CustomDetails, ArrayItemSize, CodeIndex);

            return Res;
        }

    }

    /// <summary>
    /// CodeCommon Utils
    /// </summary>
    public static class C_Common
    {
        /// <summary>
        /// Create new CodeCommon details with Logs for current ParametersData setting
        /// </summary>
        public static Cm _Ccl(this string ParametersData, string CustomDetails = null, int? ArraySize = null, int? CodeIndex = null, Ab? Block = null)
        {
            Cm Res = new Cm();

            Res.L = true;
            Res.P = ParametersData;
            Res.D = CustomDetails;
            Res.As = ArraySize;
            Res.Li = CodeIndex;
            Res.B = Block;

            return Res;
        }

        /// <summary>
        /// Create new CodeCommon details for common code for current LogError setting
        /// </summary>
        public static Cm _Cc(this bool LogError, string ParametersData = null, string CustomDetails = null, int? ArraySize = null, int? CodeIndex = null, Ab? Block = null)
        {
            Cm Res = new Cm();

            Res.L = LogError;
            Res.P = ParametersData;
            Res.D = CustomDetails;
            Res.As = ArraySize;
            Res.Li = CodeIndex;
            Res.B = Block;

            return Res;
        }

        /// <summary>
        /// Create new CodeArrayBlock details for Logs for current ArrayBlockSize setting
        /// </summary>
        public static Ab _Cc(this int? ArrayBlockSize, string CustomArrayBlockMessage = null)
        {
            Ab Res = new Ab();

            Res.S = ArrayBlockSize;
            Res.M = CustomArrayBlockMessage;

            return Res;
        }

        /// <summary>
        /// Create new CodeCommon details for Tests for current LogError setting
        /// </summary>
        public static Cm _Cct(this bool LogError, string LogSQL_ConnString = null, string LogXML_DirPath = null, Ab? Block = null, int? ArraySize = null, string ParametersData = null, string CustomDetails = null, int? CodeIndex = null)
        {
            Cm Res = new Cm();

            Res.L = LogError;
            Res.P = ParametersData;
            Res.D = CustomDetails;
            Res.As = ArraySize;
            Res.Li = CodeIndex;
            Res.B = Block;
            Res.Lq = LogSQL_ConnString;
            Res.Lx = LogXML_DirPath;

            return Res;
        }

        /// <summary>
        /// Sets logs destinations for current CurrCode - LogSQL_ConnString, LogXML_DirPath
        /// </summary>
        public static Cm _S(this Cm CurrCode, string LogSQL_ConnString = null, string LogXML_DirPath = null)
        {
            Cm Res = new Cm();

            CurrCode.Lq = LogSQL_ConnString;
            CurrCode.Lx = LogXML_DirPath;

            return Res;
        }
    }

    /// <summary>
    /// CodeEntry Utils
    /// </summary>
    public static class C_Entry
    {
        /// <summary>
        /// Create new CodeEntryData for current ClassName
        /// </summary>
        public static Ed _Ce(this string ClassName, string MethodName = null, string CustomErrorMessage = null, string PerformanceComments = null)
        {
            Ed Res = new Ed();

            Res.C = ClassName;
            Res.M = MethodName;
            Res.Me = CustomErrorMessage;
            Res.Cp = PerformanceComments;

            return Res;
        }

    }

    /// <summary>
    /// Code Func Lambda expressions Utils
    /// </summary>
    public static class C_Func
    {
        /// <summary>
        /// Evaluate common bool? return from CheckToEvaluate with context CheckFalse/CheckNull/CheckTrue methods details
        /// Returns true if any method return true, else false
        /// </summary>        
        /// <returns>Returns true if any method return true, else false</returns>
        public static bool Eval<ParamT, ResT>(Proc<ParamT, ResT> CurrProcess, f<ParamT, Cx?, bool?> CheckToEvaluate, Cx? CurrCodeType = null, ResT ResToEvaluate = default(ResT), bool EvaluateDefault = true)
        {
            if (CheckToEvaluate == null)
                return EvaluateDefault;
            bool? Res = CheckToEvaluate(CurrProcess.Params, CurrCodeType);

            if (Res == null && CurrProcess.CheckNull != null)
                return CurrProcess.CheckNull(CurrProcess.Params, ResToEvaluate, CurrCodeType);
            if (Res == false && CurrProcess.CheckFalse != null)
                return !CurrProcess.CheckFalse(CurrProcess.Params, ResToEvaluate, CurrCodeType);
            if (Res == true && CurrProcess.CheckTrue != null)
                return !CurrProcess.CheckTrue(CurrProcess.Params, ResToEvaluate, CurrCodeType);

            if (Res == true)
                return true;
            return false;
        }

        /// <summary>
        /// Evaluate common bool? return from CheckToEvaluate with context CheckFalse/CheckNull/CheckTrue methods details
        /// Returns true if any method return true, else false
        /// </summary>        
        /// <returns>Returns true if any method return true, else false</returns>
        public static bool Eval<ParamT, ResT>(Proc<ParamT, ResT> CurrProcess, f<ParamT, ResT, Cx?, bool?> CheckToEvaluate, ResT ResToEvaluate = default(ResT), Cx? CurrCodeType = null, bool EvaluateDefault = true)
        {
            if (CheckToEvaluate == null)
                return EvaluateDefault;
            bool? Res = CheckToEvaluate(CurrProcess.Params, ResToEvaluate, CurrCodeType);

            if (Res == null && CurrProcess.CheckNull != null)
                return CurrProcess.CheckNull(CurrProcess.Params, ResToEvaluate, CurrCodeType);
            if (Res == false && CurrProcess.CheckFalse != null)
                return !CurrProcess.CheckFalse(CurrProcess.Params, ResToEvaluate, CurrCodeType);
            if (Res == true && CurrProcess.CheckTrue != null)
                return !CurrProcess.CheckTrue(CurrProcess.Params, ResToEvaluate, CurrCodeType);

            if (Res == true)
                return true;
            return false;
        }

        /// <summary>
        /// Create new Func and add AddOn method with Original with AddAfterOriginal order
        /// </summary>        
        public static f<ParamT, Cx?, ResT> Add<ParamT, ResT>(f<ParamT, Cx?, ResT> Original, f<ParamT, Cx?, ResT> AddOn, Cx? CurrCodeType = null, bool AddAfterOriginal = false)
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
        public static f<ParamT, ResT, Cx?, ResT> Add<ParamT, ResT>(f<ParamT, ResT, Cx?, ResT> Original, f<ParamT, ResT, Cx?, ResT> AddOn, Cx? CurrCodeType = null, bool AddAfterOriginal = false)
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
        public static Proc<ParamT, ResT> Eval<ParamT, ResT>(ParamT StartParam, f<ParamT, Cx?, ResT> ProcessFunc, Cx? CurrCodeType = null, f<ParamT, Cx?, ResT> ProcessFail = null, f<ParamT, ResT, Exception, Cx?, ResT> ProcessError = null)
        {
            Proc<ParamT, ResT> Result = new Proc<ParamT, ResT>();

            Result.Params = StartParam;
            if (ProcessFunc == null)
                Result.Process = ProcessFunc;
            if (ProcessFail == null)
                Result.Fail = ProcessFail;
            if (ProcessError == null)
                Result.Error = ProcessError;

            return Result;
        }

    }

    #endregion

    #region Code Data Structs

    /// <summary>
    /// Global Applications Common Execution Context
    /// </summary>
    public class Ax : _D<bool>
    {
        /// <summary>
        /// Base default app Code Type
        /// </summary>
        public Cx T;
        /// <summary>
        /// Application logging
        /// </summary>
        public _Log L;

        /// <summary>
        /// Get Common Context with errors and performance with process details
        /// </summary>
        public Ax(bool LogPerformance = true, bool CollectPerfProcessDetails = true)
            : base(null, true)
        {
            this._Dm = () =>
            {
                return true;// L.Save();
            };
            T = true._Te(LogPerformance, CollectPerfProcessDetails);
            //L = new _Log();
        }
    }

    /// <summary>
    /// Code Type Execution Context
    /// </summary>
    public struct Cx
    {
        /// <summary>
        /// Use Try Catch
        /// </summary>
        public bool T;
        /// <summary>
        /// Get Performance Process Data
        /// </summary>
        public bool? P;
        /// <summary>
        /// Error Is Critical
        /// </summary>
        public bool? Cr;
        /// <summary>
        /// Re Throw On Error
        /// </summary>
        public bool? R;

        /// <summary>
        /// Code Common Performance
        /// </summary>
        public Cm Cp;
        /// <summary>
        /// Code Common Error Data
        /// </summary>
        public Cm Ce;
        /// <summary>
        /// Code Common Asp
        /// </summary>
        public Cm? Ca;
        /// <summary>
        /// Code Common Threads
        /// </summary>
        public Cm? Ctr;
        /// <summary>
        /// Code Common Test
        /// </summary>
        public Cm? Ct;

        /// <summary>
        /// Code Entry
        /// </summary>
        public Ed C;
    }

    /// <summary>
    /// Code Common details  - ParametersData, Details, ArrayItemSize, LogIndex etc
    /// </summary>
    public struct Cm
    {
        /// <summary>
        /// Log 
        /// </summary>
        public bool L;
        /// <summary>
        /// Parameters Data
        /// </summary>
        public string P;
        /// <summary>
        /// Details
        /// </summary>
        public string D;
        /// <summary>
        /// Array Size
        /// </summary>
        public int? As;
        /// <summary>
        /// Log Index
        /// </summary>
        public int? Li;
        /// <summary>
        /// Array Block Details 
        /// </summary>
        public Ab? B;
        /// <summary>
        /// Log SQL Conn String
        /// </summary>
        public string Lq;
        /// <summary>
        /// Log XML Dir Path
        /// </summary>
        public string Lx;
    }

    /// <summary>
    /// Common Code Entry Data -  class/method details with comments
    /// </summary>
    public struct Ed
    {
        /// <summary>
        /// Class Name
        /// </summary>
        public string C;
        /// <summary>
        /// Method Name
        /// </summary>
        public string M;
        /// <summary>
        /// Custom Error Message
        /// </summary>
        public string Me;
        /// <summary>
        /// Custom Info Message
        /// </summary>
        public string Mi;
        /// <summary>
        /// Performance Comments
        /// </summary>
        public string Cp;
        /// <summary>
        /// Testing Comments
        /// </summary>
        public string Ct;
        /// <summary>
        /// Threading Comments
        /// </summary>
        public string Ctr;
    }

    /// <summary>
    /// Code Array Block Details for block / buffer / paged methods
    /// </summary>
    public struct Ab
    {
        /// <summary>
        /// Array Block Size
        /// </summary>
        public int? S;
        /// <summary>
        /// Custom Array Block Message
        /// </summary>
        public string M;
    }

    /// <summary>
    /// Common Process Data Functions
    /// </summary>    
    public struct Proc<ParamT, ResT>
    {
        /// <summary>
        /// Params To Start Process
        /// </summary>
        public ParamT Params;
        /// <summary>
        /// Check Params on Start
        /// </summary>
        public f<ParamT, Cx?, bool?> Init;
        /// <summary>
        /// Pre Process
        /// </summary>
        public f<ParamT, Cx?, ParamT> Pre;
        /// <summary>
        /// Validate Process Type For Params
        /// </summary>
        public f<ParamT, Cx?, bool?> Validate;
        /// <summary>
        /// Process Params on Validate True (or Validate Null)
        /// </summary>
        public f<ParamT, Cx?, ResT> Process;
        /// <summary>
        /// Process Params on Validate False 
        /// </summary>
        public f<ParamT, Cx?, ResT> ValidFalse;
        /// <summary>
        /// Process Params on Validate Null
        /// </summary>
        public f<ParamT, Cx?, ResT> ValidNull;
        /// <summary>
        /// Run On Main Process Is Null
        /// </summary>
        public f<ParamT, Cx?, ResT> Fail;
        /// <summary>
        /// Post Process
        /// </summary>
        public f<ParamT, ResT, Cx?, ResT> Post;
        /// <summary>
        /// Check Result
        /// </summary>
        public f<ParamT, ResT, Cx?, bool?> End;
        /// <summary>
        /// Custom Use Try Catch Error Handler
        /// </summary>
        public f<ParamT, ResT, Exception, Cx?, ResT> Error;
        /// <summary>
        /// Run On Check Is Null
        /// </summary>
        public f<ParamT, ResT, Cx?, bool> CheckNull;
        /// <summary>
        /// Run On Check Is True
        /// </summary>
        public f<ParamT, ResT, Cx?, bool> CheckTrue;
        /// <summary>
        /// Run On Check Is False
        /// </summary>
        public f<ParamT, ResT, Cx?, bool> CheckFalse;
        /// <summary>
        /// Return Default On Error
        /// </summary>
        public ResT Default;
        /// <summary>
        /// Custom Use Try Catch 
        /// </summary>
        public f<ParamT, Cx?, bool?> CustomUseTryCatch;
        /// <summary>
        /// Re Throw Error On Error Catch
        /// </summary>
        public bool? ReThrowOnError;
    }

    //public struct CodeError
    //{

    //} 

    /// <summary>
    /// Memory value Type - Kb, Mb, Gb
    /// </summary>
    public enum Mvt { Kb, Mb, Gb };

    #endregion
}