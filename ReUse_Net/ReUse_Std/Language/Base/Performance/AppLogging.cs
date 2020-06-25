using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
//using System.Web.SessionState;
//using System.Web.Profile;
using System.Security.Principal;
//using System.Web.UI;
using System.Diagnostics;
using System.Reflection;
//using CommonStructsDataUtilities.Structs.System;
//using CommonStructsDataUtilities.Threads;
using ReUse_Std.AppDataModels.Logging;
using ReUse_Std.Common;
using ReUse_Std.Base;

namespace ReUse_Std.Base.Performance
{
    /// <summary>
    /// Simple non static logger with messages limit and threaded save
    /// </summary>
    public class Lg
    {
        #region private Settings
        /// <summary>
        /// Current Process
        /// </summary>
        private Process Pr;
        /// <summary>
        /// Current Records No
        /// </summary>
        private int Rn;
        /// <summary>
        /// Current Logs
        /// </summary>
        private Lst L;
        /// <summary>
        /// Logs To Save
        /// </summary>
        private List<Lst> Ls;

        /// <summary>
        /// Server Name
        /// </summary>
        private string Ns;
        /// <summary>
        /// DataBase Name
        /// </summary>
        private string Nd;
        /// <summary>
        /// Curr Session
        /// </summary>
        private Sld S;
        /// <summary>
        /// Method To Save Logs
        /// </summary>
        private f<Lst, bool> Sm;
        #endregion

        /// <summary>
        /// Start logger with parameters from CurrLogSession
        /// </summary>
        public Lg(f<Lst, bool> SaveLogsMethod, Sld CurrSession = null, string ServerName = null, string DataBaseName = null)
        {
            S = CurrSession;
            Ns = ServerName;
            Nd = DataBaseName;
            Pr = null;
            Sm = SaveLogsMethod;
            L = new Lst().I(S);
            Ls = new List<Lst>();
            Rn = 0;
        }

        #region Error and Info Log Entry

        #region Errors

        /// <summary>
        /// Create New Error Log Entry
        /// </summary>
        public void E(string ClassName = null, string MethodName = null, string Comments = null, Exception CurrExc = null, bool? IsCritical = null, string ParametersData = null, string ErrorDetails = null, int? ArrayItemSize = null, int? LogIndex = null)
        {
            Ae(ClassName, MethodName, Comments, CurrExc, IsCritical, ParametersData, ErrorDetails, ArrayItemSize, LogIndex);
        }

        /// <summary>
        /// Create New Error Log Entry
        /// </summary>
        public void E(string ClassName = null, string MethodName = null, string Comments = null, Exception CurrExc = null, Cx CurrCodeType = null)
        {
            if (CurrCodeType == null)
                Ae(ClassName, MethodName, Comments, CurrExc);
            else
                Ae(ClassName, MethodName, Comments, CurrExc, CurrCodeType.Cr, CurrCodeType.Ce.P, CurrCodeType.Ce.D, CurrCodeType.Ce.As, CurrCodeType.Ce.Li);
        }

        /// <summary>
        /// Create New Error Log Entry
        /// </summary>
        public void E(string CustomComments = null, Exception CurrExc = null, Cx CurrCodeType = null)
        {
            string ClassName = null, MethodName = null, ErrComments = CustomComments;

            if (CurrCodeType != null)
            {
                ClassName = CurrCodeType.C.C;
                MethodName = CurrCodeType.C.M;
                ErrComments = CustomComments ?? CurrCodeType.C.Me;
            }

            if (CurrCodeType == null)
                Ae(ClassName, MethodName, ErrComments, CurrExc);
            else
                Ae(ClassName, MethodName, ErrComments, CurrExc, CurrCodeType.Cr, CurrCodeType.Ce.P, CurrCodeType.Ce.D, CurrCodeType.Ce.As, CurrCodeType.Ce.Li);
        }

        #endregion

        #region Info

        /// <summary>
        /// Create New Info Log Entry
        /// </summary>
        public void I(Ed CurrCodeEntry = null, string CustomComments = null, string ParametersData = null, int? ArrayItemSize = null, int? LogIndex = null)
        {
            string ClassName = null, MethodName = null;

            if (CurrCodeEntry != null)
            {
                ClassName = CurrCodeEntry.C;
                MethodName = CurrCodeEntry.M;
            }

            Ai(ClassName, MethodName, CustomComments, ParametersData, ArrayItemSize, LogIndex);
        }

        /// <summary>
        /// Create New Info Log Entry
        /// </summary>
        public void I(Cx CurrCodeType = null, string CustomComments = null, string ParametersData = null, int? ArrayItemSize = null, int? LogIndex = null)
        {
            string ClassName = null, MethodName = null;

            if (CurrCodeType != null)
            {
                ClassName = CurrCodeType.C.C;
                MethodName = CurrCodeType.C.M;
            }

            Ai(ClassName, MethodName, CustomComments, ParametersData, ArrayItemSize, LogIndex);
        }

        /// <summary>
        /// Create New Info Log Entry
        /// </summary>
        public void I(string ClassName = null, string MethodName = null, string Comments = null, string ParametersData = null, int? ArrayItemSize = null, int? LogIndex = null)
        {
            Ai(ClassName, MethodName, Comments, ParametersData, ArrayItemSize, LogIndex);
        }

        #endregion

        #endregion

        #region Performance Log Entry

        #region Create New

        /// <summary>
        /// Create New Performance Log Entry
        /// </summary>
        /// <param name="ParametersData">Method Parameters Data</param>
        /// <param name="Details">Details</param>
        /// <param name="GetProcessData">Get Process Data (memory, processor usage)</param>
        /// <param name="ArrayItemSize">Array Items No (for method parameters)</param>
        /// <returns>Performance Log Entry Record</returns>
        public Prf P(string ParametersData = null, string Details = null, bool? GetProcessData = null, int? ArrayItemSize = null, int? PerformanceIndex = null)
        {
            return Pn(ParametersData, Details, GetProcessData ?? S.Cpd, ArrayItemSize, PerformanceIndex);
        }

        /// <summary>
        /// Create New Performance Log Entry
        /// </summary>
        public Prf P(Cx CurrCodeType = null)
        {
            if (CurrCodeType != null && CurrCodeType.Cp.L)
                return Pn(CurrCodeType.Cp.P, CurrCodeType.Cp.D, CurrCodeType.P ?? S.Cpd, CurrCodeType.Cp.As, CurrCodeType.Cp.Li);
            return null;
        }

        #endregion

        #region Add Created Log To Storage

        /// <summary>
        /// Add Created Performance Log Record To Storage
        /// </summary>
        /// <param name="Log">Performance Record</param>
        /// <param name="ClassName">Class Name</param>
        /// <param name="MethodName">Method Name</param>
        /// <param name="Comments">Comments (optional)</param>
        /// <param name="GetProcessData">Get Process Data (memory, processor usage)</param>
        public void P(Prf Log, string ClassName = null, string MethodName = null, string Comments = null, bool? GetProcessData = null)
        {
            Pa(Log, ClassName, MethodName, Comments, GetProcessData ?? false);
        }

        /// <summary>
        /// Add Created Performance Log Record To Storage
        /// </summary>
        public void P(Prf Log, string ClassName = null, string MethodName = null, string Comments = null, Cx CurrCodeType = null)
        {
            if (CurrCodeType == null)
                Pa(Log, ClassName, MethodName, Comments, false);
            else
                Pa(Log, ClassName, MethodName, Comments, CurrCodeType.P ?? false);
        }

        /// <summary>
        /// Add Created Performance Log Record To Storage
        /// </summary>
        public void P(Prf Log, string CustomComments = null, Cx CurrCodeType = null)
        {
            string ClassName = null, MethodName = null, PerfComments = CustomComments;

            if (CurrCodeType != null)
            {
                if (CurrCodeType.C != null)
                {
                    ClassName = CurrCodeType.C.C;
                    MethodName = CurrCodeType.C.M;
                }                    
                
                PerfComments = CustomComments ?? CurrCodeType?.C?.Cp;
            }

            if (CurrCodeType == null)
                Pa(Log, ClassName, MethodName, PerfComments, false);
            else
                Pa(Log, ClassName, MethodName, PerfComments, CurrCodeType.P ?? false);
        }

        #endregion

        #endregion

        #region Private methods

        #region Error and Info Log Entry

        /// <summary>
        /// Add Log Error
        /// </summary>
        private void Ae(string ClassName = null, string MethodName = null, string Comments = null, Exception CurrExc = null, bool? IsCritical = null, string ParametersData = null, string ErrorDetails = null, int? ArrayItemSize = null, int? LogIndex = null)
        {
            if (!S.Cl)
                return;

            var EntryGuid = Ce(ClassName, MethodName);

            var r = new Err();

            r.As = ArrayItemSize;
            r.I = LogIndex;

            r.P = ParametersData;
            r.C = Comments;
            r.Ed = ErrorDetails;
            r.Cr = IsCritical;
            r.E = EntryGuid;

            if (CurrExc != null)
            {
                r.Esr = CurrExc.Source;
                r.Em = CurrExc.Message;
                if (S.Ced)
                {
                    r.Et = CurrExc.StackTrace;
                    r.Es = CurrExc.ToString();
                    r.Ets = CurrExc.TargetSite.ToString();
                }
            }
            L.E.Add(r);
            Rn++;

            if (S.M != null && S.M.Value > 1 && Rn > S.M.Value)
                Save();
        }

        /// <summary>
        /// Add Log Information
        /// </summary>
        private void Ai(string ClassName = null, string MethodName = null, string Comments = null, string ParametersData = null, int? ArrayItemSize = null, int? LogIndex = null)
        {
            if (!S.Cl)
                return;

            var EntryGuid = Ce(ClassName, MethodName);

            var r = new Inf();

            r.E = EntryGuid;
            r.As = ArrayItemSize;
            r.I = LogIndex;
            r.P = ParametersData;
            r.C = Comments;

            L.I.Add(r);
            Rn++;

            if (S.M != null && S.M.Value > 1 && Rn > S.M.Value)
                Save();
        }

        #endregion

        #region Perf Log Entry

        /// <summary>
        /// Create New Log Entry
        /// </summary>
        private Prf Pn(string ParametersData = null, string Details = null, bool GetProcessData = true, int? ArrayItemSize = null, int? PerformanceIndex = null)
        {
            if (!S.Cp)
                return null;

            Prf Result = new Prf();

            Result.As = ArrayItemSize;
            Result.I = PerformanceIndex;

            if (S.Cpr && GetProcessData)
                Result.Is = Prd();

            Result.P = ParametersData;
            Result.D = Details;

            return Result;
        }

        /// <summary>
        /// Add Created Perf Record
        /// </summary>
        private void Pa(Prf CurrLog, string ClassName = null, string MethodName = null, string Comments = null, bool GetProcessData = true)
        {
            if (!S.Cp || CurrLog == null)
                return;

            var Log = CurrLog;

            if (S.Cpr && GetProcessData)
                Log.Ie = Prd();

            Log.C = Comments;
            var End = _.d;

            Log.De = End;
            Log.T = (End - Log.Ds).Ticks;
            Log.M = (End - Log.Ds).TotalMilliseconds;

            var EntryGuid = Ce(ClassName, MethodName);

            Log.E = EntryGuid;
            L.P.Add(Log);
            Rn++;

            if (S.M != null && S.M.Value > 1 && Rn > S.M.Value)
                Save();
        }

        #endregion

        #region Details Logging

        /// <summary>
        /// Get Current Process Common Details
        /// </summary>
        private void Prc()
        {
            if (!S.Cpr)
                return;

            Prc Result = new Prc();

            if (Pr != null)
            {
                Pr.Refresh();
                Result.Ip = Pr.Id;
                Result.Is = Pr.SessionId;
                Result.S = Pr.StartTime;
            }
            L.Pr.Add(Result);
        }

        /// <summary>
        /// Get Current Process Performance Details
        /// </summary>
        private Guid? Prd()
        {
            if (!S.Cprd)
                return null;

            Prd Result = new Prd();

            if (Pr != null)
            {
                try
                {
                    Pr.Refresh();
                    Result.Ch = Pr.HandleCount;
                    Result.Sx = Pr.NonpagedSystemMemorySize64;
                    Result.Mx = Pr.PagedMemorySize64;
                    Result.Sp = Pr.PagedSystemMemorySize64;
                    Result.Px = Pr.PeakPagedMemorySize64;
                    Result.Vp = Pr.PeakVirtualMemorySize64;
                    Result.Wp = Pr.PeakWorkingSet64;
                    Result.Mp = Pr.PrivateMemorySize64;
                    Result.Tp = Pr.PrivilegedProcessorTime;
                    Result.Ct = Pr.Threads.Count;
                    Result.V = Pr.VirtualMemorySize64;
                    Result.W = Pr.WorkingSet64;
                }
                catch (Exception exc)
                {
                    int ttt = 555;
                }
            }

            L.Pd.Add(Result);
            return Result.PrdId;
        }

        /// <summary>
        /// Create New Code Entry
        /// </summary>
        private Guid Ce(string ClassTitle = null, string MethodTitle = null)
        {
            var NewUID = _.g;
            var ClassData = L.C.w(e => e.C == ClassTitle);
            if (ClassData.C())
            {
                var MethodsData = ClassData.f().M.w(e => e.M == MethodTitle);
                if (MethodsData.C())
                    return MethodsData.f().G;
                else
                    ClassData.f().M.Add(new Cme() { G = NewUID, M = MethodTitle });
            }
            else
                L.C.Add(new Cde() { C = ClassTitle, M = new Cme() { G = NewUID, M = MethodTitle }.L() });

            return NewUID;
        }

        /// <summary>
        /// Get Environment Details
        /// </summary>
        private void En()
        {
            Env Result = new Env();

            Result.Xo = Environment.Is64BitOperatingSystem;
            Result.Xp = Environment.Is64BitProcess;
            Result.M = Environment.MachineName;
            Result.N = Environment.NewLine;
            Result.Pc = Environment.ProcessorCount;
            Result.Sp = Environment.SystemPageSize;
            Result.Ct = Environment.TickCount;

            if (S.Cu)
            {
                Result.Ud = Environment.UserDomainName;
                Result.Ui = Environment.UserInteractive;
                Result.Un = Environment.UserName;
            }

            Result.W = Environment.WorkingSet;

            if (S.Co && Environment.OSVersion != null)
            {
                Result.Op = Environment.OSVersion.Platform.ToString();
                Result.Os = Environment.OSVersion.ServicePack;
                Result.Ov = Environment.OSVersion.VersionString;
            }

            L.En.Add(Result);
        }

        #endregion

        //private void GetWindowsIdentity(WindowsIdentity CurrentIdentity)
        //{
        //    WindowsIdentityLog Result = new WindowsIdentityLog();
        //    Result.DateFound = DateTime.Now;

        //    Result.AuthenticationType = CurrentIdentity.AuthenticationType;
        //    Result.ImpersonationLevel = (int)CurrentIdentity.ImpersonationLevel;
        //    Result.IsAnonymous = CurrentIdentity.IsAnonymous;
        //    Result.IsAuthenticated = CurrentIdentity.IsAuthenticated;
        //    Result.IsGuest = CurrentIdentity.IsGuest;
        //    Result.IsSystem = CurrentIdentity.IsSystem;
        //    Result.Name = CurrentIdentity.Name;

        //    CurrentLogs.WindowsIdentities.Add(Result);
        //}

        #region ASP Logging

        //private void GetCurrentHttpRequestDetails(HttpRequest CurrentRequest)
        //{
        //    if (!CurrSession.CollectPerfLogs)
        //        return;

        //    HttpRequestLog Result = new HttpRequestLog();
        //    Result.DateFound = DateTime.Now;

        //    if (CurrentRequest != null)
        //    {
        //        Result.AnonymousID = CurrentRequest.AnonymousID;
        //        Result.ApplicationPath = CurrentRequest.ApplicationPath;
        //        if (CurrentRequest.ContentEncoding != null)
        //            Result.ContentEncoding = CurrentRequest.ContentEncoding.EncodingName + " (" + CurrentRequest.ContentEncoding.WebName + ")";
        //        Result.ContentLength = CurrentRequest.ContentLength;
        //        Result.ContentType = CurrentRequest.ContentType;
        //        Result.HttpMethod = CurrentRequest.HttpMethod;
        //        Result.IsAuthenticated = CurrentRequest.IsAuthenticated;
        //        Result.IsLocal = CurrentRequest.IsLocal;
        //        Result.IsSecureConnection = CurrentRequest.IsSecureConnection;
        //        Result.RawUrl = CurrentRequest.RawUrl;
        //        Result.RequestType = CurrentRequest.RequestType;
        //        Result.TotalBytes = CurrentRequest.TotalBytes;
        //        if (CurrentRequest.UrlReferrer != null)
        //            Result.UrlReferrer = CurrentRequest.UrlReferrer.ToString();
        //        Result.UserAgent = CurrentRequest.UserAgent;
        //        Result.UserHostAddress = CurrentRequest.UserHostAddress;
        //        Result.UserHostName = CurrentRequest.UserHostName;
        //    }
        //    CurrentLogs.HttpRequests.Add(Result);
        //}

        //private void GetCurrentHttpBrowser(HttpBrowserCapabilitiesBase CurrentRequestBrowser)
        //{
        //    if (!CurrSession.CollectPerfLogs)
        //        return;
        //    // CurrentRequestBrowser = CurrentHttpRequest.Browser
        //    HttpBrowserCapabilitiesLog Result = new HttpBrowserCapabilitiesLog();
        //    Result.DateFound = DateTime.Now;

        //    if (CurrentRequestBrowser != null)
        //    {
        //        if (CurrentRequestBrowser.Win32 != null)
        //            Result.Win32 = CurrentRequestBrowser.Win32;
        //        if (CurrentRequestBrowser.Win16 != null)
        //            Result.Win16 = CurrentRequestBrowser.Win16;
        //        if (CurrentRequestBrowser.W3CDomVersion != null)
        //            Result.W3CDomVersion = CurrentRequestBrowser.W3CDomVersion.ToString();
        //        if (CurrentRequestBrowser.Version != null)
        //            Result.Version = CurrentRequestBrowser.Version;
        //        if (CurrentRequestBrowser.VBScript != null)
        //            Result.VBScript = CurrentRequestBrowser.VBScript;
        //        if (CurrentRequestBrowser.UseOptimizedCacheKey != null)
        //            Result.UseOptimizedCacheKey = CurrentRequestBrowser.UseOptimizedCacheKey;
        //        if (CurrentRequestBrowser.Type != null)
        //            Result.Type = CurrentRequestBrowser.Type;
        //        if (CurrentRequestBrowser.TagWriter != null)
        //            Result.TagWriter = CurrentRequestBrowser.TagWriter.ToString();
        //        if (CurrentRequestBrowser.Tables != null)
        //            Result.Tables = CurrentRequestBrowser.Tables;
        //        if (CurrentRequestBrowser.SupportsXmlHttp != null)
        //            Result.SupportsXmlHttp = CurrentRequestBrowser.SupportsXmlHttp;
        //        if (CurrentRequestBrowser.SupportsUncheck != null)
        //            Result.SupportsUncheck = CurrentRequestBrowser.SupportsUncheck;
        //        if (CurrentRequestBrowser.SupportsSelectMultiple != null)
        //            Result.SupportsSelectMultiple = CurrentRequestBrowser.SupportsSelectMultiple;
        //        if (CurrentRequestBrowser.SupportsRedirectWithCookie != null)
        //            Result.SupportsRedirectWithCookie = CurrentRequestBrowser.SupportsRedirectWithCookie;
        //        if (CurrentRequestBrowser.SupportsQueryStringInFormAction != null)
        //            Result.SupportsQueryStringInFormAction = CurrentRequestBrowser.SupportsQueryStringInFormAction;
        //        if (CurrentRequestBrowser.SupportsJPhoneSymbols != null)
        //            Result.SupportsJPhoneSymbols = CurrentRequestBrowser.SupportsJPhoneSymbols;
        //        if (CurrentRequestBrowser.SupportsJPhoneMultiMediaAttributes != null)
        //            Result.SupportsJPhoneMultiMediaAttributes = CurrentRequestBrowser.SupportsJPhoneMultiMediaAttributes;
        //        if (CurrentRequestBrowser.SupportsItalic != null)
        //            Result.SupportsItalic = CurrentRequestBrowser.SupportsItalic;
        //        if (CurrentRequestBrowser.SupportsInputMode != null)
        //            Result.SupportsInputMode = CurrentRequestBrowser.SupportsInputMode;
        //        if (CurrentRequestBrowser.SupportsInputIStyle != null)
        //            Result.SupportsInputIStyle = CurrentRequestBrowser.SupportsInputIStyle;
        //        if (CurrentRequestBrowser.SupportsIModeSymbols != null)
        //            Result.SupportsIModeSymbols = CurrentRequestBrowser.SupportsIModeSymbols;
        //        if (CurrentRequestBrowser.SupportsImageSubmit != null)
        //            Result.SupportsImageSubmit = CurrentRequestBrowser.SupportsImageSubmit;
        //        if (CurrentRequestBrowser.SupportsFontSize != null)
        //            Result.SupportsFontSize = CurrentRequestBrowser.SupportsFontSize;
        //        if (CurrentRequestBrowser.SupportsFontName != null)
        //            Result.SupportsFontName = CurrentRequestBrowser.SupportsFontName;
        //        if (CurrentRequestBrowser.SupportsFontColor != null)
        //            Result.SupportsFontColor = CurrentRequestBrowser.SupportsFontColor;
        //        if (CurrentRequestBrowser.SupportsEmptyStringInCookieValue != null)
        //            Result.SupportsEmptyStringInCookieValue = CurrentRequestBrowser.SupportsEmptyStringInCookieValue;
        //        if (CurrentRequestBrowser.SupportsDivNoWrap != null)
        //            Result.SupportsDivNoWrap = CurrentRequestBrowser.SupportsDivNoWrap;
        //        if (CurrentRequestBrowser.SupportsDivAlign != null)
        //            Result.SupportsDivAlign = CurrentRequestBrowser.SupportsDivAlign;
        //        if (CurrentRequestBrowser.SupportsCss != null)
        //            Result.SupportsCss = CurrentRequestBrowser.SupportsCss;
        //        if (CurrentRequestBrowser.SupportsCallback != null)
        //            Result.SupportsCallback = CurrentRequestBrowser.SupportsCallback;
        //        if (CurrentRequestBrowser.SupportsCacheControlMetaTag != null)
        //            Result.SupportsCacheControlMetaTag = CurrentRequestBrowser.SupportsCacheControlMetaTag;
        //        if (CurrentRequestBrowser.SupportsBold != null)
        //            Result.SupportsBold = CurrentRequestBrowser.SupportsBold;
        //        if (CurrentRequestBrowser.SupportsBodyColor != null)
        //            Result.SupportsBodyColor = CurrentRequestBrowser.SupportsBodyColor;
        //        if (CurrentRequestBrowser.SupportsAccesskeyAttribute != null)
        //            Result.SupportsAccesskeyAttribute = CurrentRequestBrowser.SupportsAccesskeyAttribute;
        //        if (CurrentRequestBrowser.ScreenPixelsWidth != null)
        //            Result.ScreenPixelsWidth = CurrentRequestBrowser.ScreenPixelsWidth;
        //        if (CurrentRequestBrowser.ScreenPixelsHeight != null)
        //            Result.ScreenPixelsHeight = CurrentRequestBrowser.ScreenPixelsHeight;
        //        if (CurrentRequestBrowser.ScreenCharactersWidth != null)
        //            Result.ScreenCharactersWidth = CurrentRequestBrowser.ScreenCharactersWidth;
        //        if (CurrentRequestBrowser.ScreenCharactersHeight != null)
        //            Result.ScreenCharactersHeight = CurrentRequestBrowser.ScreenCharactersHeight;
        //        if (CurrentRequestBrowser.ScreenBitDepth != null)
        //            Result.ScreenBitDepth = CurrentRequestBrowser.ScreenBitDepth;
        //        if (CurrentRequestBrowser.RequiresUrlEncodedPostfieldValues != null)
        //            Result.RequiresUrlEncodedPostfieldValues = CurrentRequestBrowser.RequiresUrlEncodedPostfieldValues;
        //        if (CurrentRequestBrowser.RequiresUniqueHtmlInputNames != null)
        //            Result.RequiresUniqueHtmlInputNames = CurrentRequestBrowser.RequiresUniqueHtmlInputNames;
        //        if (CurrentRequestBrowser.RequiresUniqueHtmlCheckboxNames != null)
        //            Result.RequiresUniqueHtmlCheckboxNames = CurrentRequestBrowser.RequiresUniqueHtmlCheckboxNames;
        //        if (CurrentRequestBrowser.RequiresUniqueFilePathSuffix != null)
        //            Result.RequiresUniqueFilePathSuffix = CurrentRequestBrowser.RequiresUniqueFilePathSuffix;
        //        if (CurrentRequestBrowser.RequiresSpecialViewStateEncoding != null)
        //            Result.RequiresSpecialViewStateEncoding = CurrentRequestBrowser.RequiresSpecialViewStateEncoding;
        //        if (CurrentRequestBrowser.RequiresPhoneNumbersAsPlainText != null)
        //            Result.RequiresPhoneNumbersAsPlainText = CurrentRequestBrowser.RequiresPhoneNumbersAsPlainText;
        //        if (CurrentRequestBrowser.RequiresOutputOptimization != null)
        //            Result.RequiresOutputOptimization = CurrentRequestBrowser.RequiresOutputOptimization;
        //        if (CurrentRequestBrowser.RequiresNoBreakInFormatting != null)
        //            Result.RequiresNoBreakInFormatting = CurrentRequestBrowser.RequiresNoBreakInFormatting;
        //        if (CurrentRequestBrowser.RequiresLeadingPageBreak != null)
        //            Result.RequiresLeadingPageBreak = CurrentRequestBrowser.RequiresLeadingPageBreak;
        //        if (CurrentRequestBrowser.RequiresHtmlAdaptiveErrorReporting != null)
        //            Result.RequiresHtmlAdaptiveErrorReporting = CurrentRequestBrowser.RequiresHtmlAdaptiveErrorReporting;
        //        if (CurrentRequestBrowser.RequiresDBCSCharacter != null)
        //            Result.RequiresDBCSCharacter = CurrentRequestBrowser.RequiresDBCSCharacter;
        //        if (CurrentRequestBrowser.RequiresControlStateInSession != null)
        //            Result.RequiresControlStateInSession = CurrentRequestBrowser.RequiresControlStateInSession;
        //        if (CurrentRequestBrowser.RequiresContentTypeMetaTag != null)
        //            Result.RequiresContentTypeMetaTag = CurrentRequestBrowser.RequiresContentTypeMetaTag;
        //        if (CurrentRequestBrowser.RequiresAttributeColonSubstitution != null)
        //            Result.RequiresAttributeColonSubstitution = CurrentRequestBrowser.RequiresAttributeColonSubstitution;
        //        if (CurrentRequestBrowser.RequiredMetaTagNameValue != null)
        //            Result.RequiredMetaTagNameValue = CurrentRequestBrowser.RequiredMetaTagNameValue;
        //        if (CurrentRequestBrowser.RendersWmlSelectsAsMenuCards != null)
        //            Result.RendersWmlSelectsAsMenuCards = CurrentRequestBrowser.RendersWmlSelectsAsMenuCards;
        //        if (CurrentRequestBrowser.RendersWmlDoAcceptsInline != null)
        //            Result.RendersWmlDoAcceptsInline = CurrentRequestBrowser.RendersWmlDoAcceptsInline;
        //        if (CurrentRequestBrowser.RendersBreaksAfterWmlInput != null)
        //            Result.RendersBreaksAfterWmlInput = CurrentRequestBrowser.RendersBreaksAfterWmlInput;
        //        if (CurrentRequestBrowser.RendersBreaksAfterWmlAnchor != null)
        //            Result.RendersBreaksAfterWmlAnchor = CurrentRequestBrowser.RendersBreaksAfterWmlAnchor;
        //        if (CurrentRequestBrowser.RendersBreaksAfterHtmlLists != null)
        //            Result.RendersBreaksAfterHtmlLists = CurrentRequestBrowser.RendersBreaksAfterHtmlLists;
        //        if (CurrentRequestBrowser.RendersBreakBeforeWmlSelectAndInput != null)
        //            Result.RendersBreakBeforeWmlSelectAndInput = CurrentRequestBrowser.RendersBreakBeforeWmlSelectAndInput;
        //        if (CurrentRequestBrowser.PreferredResponseEncoding != null)
        //            Result.PreferredResponseEncoding = CurrentRequestBrowser.PreferredResponseEncoding;
        //        if (CurrentRequestBrowser.PreferredRequestEncoding != null)
        //            Result.PreferredRequestEncoding = CurrentRequestBrowser.PreferredRequestEncoding;
        //        if (CurrentRequestBrowser.PreferredRenderingType != null)
        //            Result.PreferredRenderingType = CurrentRequestBrowser.PreferredRenderingType;
        //        if (CurrentRequestBrowser.PreferredRenderingMime != null)
        //            Result.PreferredRenderingMime = CurrentRequestBrowser.PreferredRenderingMime;
        //        if (CurrentRequestBrowser.PreferredImageMime != null)
        //            Result.PreferredImageMime = CurrentRequestBrowser.PreferredImageMime;
        //        if (CurrentRequestBrowser.Platform != null)
        //            Result.Platform = CurrentRequestBrowser.Platform;
        //        if (CurrentRequestBrowser.NumberOfSoftkeys != null)
        //            Result.NumberOfSoftkeys = CurrentRequestBrowser.NumberOfSoftkeys;
        //        if (CurrentRequestBrowser.MSDomVersion != null)
        //            Result.MSDomVersion = CurrentRequestBrowser.MSDomVersion.ToString();
        //        if (CurrentRequestBrowser.MobileDeviceModel != null)
        //            Result.MobileDeviceModel = CurrentRequestBrowser.MobileDeviceModel;
        //        if (CurrentRequestBrowser.MobileDeviceManufacturer != null)
        //            Result.MobileDeviceManufacturer = CurrentRequestBrowser.MobileDeviceManufacturer;
        //        if (CurrentRequestBrowser.MinorVersionString != null)
        //            Result.MinorVersionString = CurrentRequestBrowser.MinorVersionString;
        //        if (CurrentRequestBrowser.MinorVersion != null)
        //            Result.MinorVersion = CurrentRequestBrowser.MinorVersion;
        //        if (CurrentRequestBrowser.MaximumSoftkeyLabelLength != null)
        //            Result.MaximumSoftkeyLabelLength = CurrentRequestBrowser.MaximumSoftkeyLabelLength;
        //        if (CurrentRequestBrowser.MaximumRenderedPageSize != null)
        //            Result.MaximumRenderedPageSize = CurrentRequestBrowser.MaximumRenderedPageSize;
        //        if (CurrentRequestBrowser.MaximumHrefLength != null)
        //            Result.MaximumHrefLength = CurrentRequestBrowser.MaximumHrefLength;
        //        if (CurrentRequestBrowser.MajorVersion != null)
        //            Result.MajorVersion = CurrentRequestBrowser.MajorVersion;
        //        if (CurrentRequestBrowser.JScriptVersion != null)
        //            Result.JScriptVersion = CurrentRequestBrowser.JScriptVersion.ToString();
        //        if (CurrentRequestBrowser.JavaApplets != null)
        //            Result.JavaApplets = CurrentRequestBrowser.JavaApplets;
        //        if (CurrentRequestBrowser.IsMobileDevice != null)
        //            Result.IsMobileDevice = CurrentRequestBrowser.IsMobileDevice;
        //        if (CurrentRequestBrowser.IsColor != null)
        //            Result.IsColor = CurrentRequestBrowser.IsColor;
        //        if (CurrentRequestBrowser.InputType != null)
        //            Result.InputType = CurrentRequestBrowser.InputType;
        //        if (CurrentRequestBrowser.Id != null)
        //            Result.Id = CurrentRequestBrowser.Id;
        //        if (CurrentRequestBrowser.HtmlTextWriter != null)
        //            Result.HtmlTextWriter = CurrentRequestBrowser.HtmlTextWriter;
        //        if (CurrentRequestBrowser.HidesRightAlignedMultiselectScrollbars != null)
        //            Result.HidesRightAlignedMultiselectScrollbars = CurrentRequestBrowser.HidesRightAlignedMultiselectScrollbars;
        //        if (CurrentRequestBrowser.HasBackButton != null)
        //            Result.HasBackButton = CurrentRequestBrowser.HasBackButton;
        //        if (CurrentRequestBrowser.GatewayVersion != null)
        //            Result.GatewayVersion = CurrentRequestBrowser.GatewayVersion;
        //        if (CurrentRequestBrowser.GatewayMinorVersion != null)
        //            Result.GatewayMinorVersion = CurrentRequestBrowser.GatewayMinorVersion;
        //        if (CurrentRequestBrowser.GatewayMajorVersion != null)
        //            Result.GatewayMajorVersion = CurrentRequestBrowser.GatewayMajorVersion;
        //        if (CurrentRequestBrowser.Frames != null)
        //            Result.Frames = CurrentRequestBrowser.Frames;
        //        if (CurrentRequestBrowser.EcmaScriptVersion != null)
        //            Result.EcmaScriptVersion = CurrentRequestBrowser.EcmaScriptVersion.ToString();
        //        if (CurrentRequestBrowser.DefaultSubmitButtonLimit != null)
        //            Result.DefaultSubmitButtonLimit = CurrentRequestBrowser.DefaultSubmitButtonLimit;
        //        if (CurrentRequestBrowser.Crawler != null)
        //            Result.Crawler = CurrentRequestBrowser.Crawler;
        //        if (CurrentRequestBrowser.Cookies != null)
        //            Result.Cookies = CurrentRequestBrowser.Cookies;
        //        if (CurrentRequestBrowser.ClrVersion != null)
        //            Result.ClrVersion = CurrentRequestBrowser.ClrVersion.ToString();
        //        if (CurrentRequestBrowser.CDF != null)
        //            Result.CDF = CurrentRequestBrowser.CDF;
        //        if (CurrentRequestBrowser.CanSendMail != null)
        //            Result.CanSendMail = CurrentRequestBrowser.CanSendMail;
        //        if (CurrentRequestBrowser.CanRenderSetvarZeroWithMultiSelectionList != null)
        //            Result.CanRenderSetvarZeroWithMultiSelectionList = CurrentRequestBrowser.CanRenderSetvarZeroWithMultiSelectionList;
        //        if (CurrentRequestBrowser.CanRenderPostBackCards != null)
        //            Result.CanRenderPostBackCards = CurrentRequestBrowser.CanRenderPostBackCards;
        //        if (CurrentRequestBrowser.CanRenderOneventAndPrevElementsTogether != null)
        //            Result.CanRenderOneventAndPrevElementsTogether = CurrentRequestBrowser.CanRenderOneventAndPrevElementsTogether;
        //        if (CurrentRequestBrowser.CanRenderMixedSelects != null)
        //            Result.CanRenderMixedSelects = CurrentRequestBrowser.CanRenderMixedSelects;
        //        if (CurrentRequestBrowser.CanRenderInputAndSelectElementsTogether != null)
        //            Result.CanRenderInputAndSelectElementsTogether = CurrentRequestBrowser.CanRenderInputAndSelectElementsTogether;
        //        if (CurrentRequestBrowser.CanRenderEmptySelects != null)
        //            Result.CanRenderEmptySelects = CurrentRequestBrowser.CanRenderEmptySelects;
        //        if (CurrentRequestBrowser.CanRenderAfterInputOrSelectElement != null)
        //            Result.CanRenderAfterInputOrSelectElement = CurrentRequestBrowser.CanRenderAfterInputOrSelectElement;
        //        if (CurrentRequestBrowser.CanInitiateVoiceCall != null)
        //            Result.CanInitiateVoiceCall = CurrentRequestBrowser.CanInitiateVoiceCall;
        //        if (CurrentRequestBrowser.CanCombineFormsInDeck != null)
        //            Result.CanCombineFormsInDeck = CurrentRequestBrowser.CanCombineFormsInDeck;
        //        if (CurrentRequestBrowser.Browser != null)
        //            Result.Browser = CurrentRequestBrowser.Browser;
        //        if (CurrentRequestBrowser.Beta != null)
        //            Result.Beta = CurrentRequestBrowser.Beta;
        //        if (CurrentRequestBrowser.BackgroundSounds != null)
        //            Result.BackgroundSounds = CurrentRequestBrowser.BackgroundSounds;
        //        if (CurrentRequestBrowser.AOL != null)
        //            Result.AOL = CurrentRequestBrowser.AOL;
        //        if (CurrentRequestBrowser.ActiveXControls != null)
        //            Result.ActiveXControls = CurrentRequestBrowser.ActiveXControls;

        //    }
        //    CurrentLogs.HttpBrowsers.Add(Result);
        //}

        //private void GetCurrentHttpSessionDetails(HttpSessionState CurrentSession)
        //{
        //    if (!CurrSession.CollectPerfLogs)
        //        return;

        //    HttpSessionLog Result = new HttpSessionLog();
        //    Result.DateFound = DateTime.Now;

        //    if (CurrentSession != null)
        //    {
        //        Result.CodePage = CurrentSession.CodePage;
        //        Result.CookieMode = (int)CurrentSession.CookieMode;
        //        Result.Count = CurrentSession.Count;
        //        Result.IsCookieless = CurrentSession.IsCookieless;
        //        Result.IsNewSession = CurrentSession.IsNewSession;
        //        Result.IsReadOnly = CurrentSession.IsReadOnly;
        //        Result.IsSynchronized = CurrentSession.IsSynchronized;
        //        Result.LCID = CurrentSession.LCID;
        //        Result.Mode = (int)CurrentSession.Mode;
        //        Result.Timeout = CurrentSession.Timeout;
        //    }
        //    CurrentLogs.HttpSessions.Add(Result);
        //}

        //private void GetCurrentHttpContextDetails(HttpContext CurrentContext)
        //{
        //    if (!CurrSession.CollectPerfLogs)
        //        return;

        //    HttpContextLog Result = new HttpContextLog();
        //    Result.DateFound = DateTime.Now;

        //    if (CurrentContext != null)
        //    {
        //        Result.IsCustomErrorEnabled = CurrentContext.IsCustomErrorEnabled;
        //        Result.IsDebuggingEnabled = CurrentContext.IsDebuggingEnabled;
        //        Result.IsPostNotification = CurrentContext.IsPostNotification;
        //        if (CurrentContext.Server != null)
        //            Result.Server = CurrentContext.Server.MachineName;
        //        if (CurrentContext.User != null)
        //            Result.User = CurrentContext.User.ToString();
        //    }
        //    CurrentLogs.HttpContexts.Add(Result);
        //}

        //private void GetCurrentWebProfileDetails(ProfileBase CurrentProfile)
        //{
        //    if (!CurrSession.CollectPerfLogs)
        //        return;

        //    WebProfileLog Result = new WebProfileLog();
        //    Result.DateFound = DateTime.Now;

        //    if (CurrentProfile != null)
        //    {
        //        Result.IsAnonymous = CurrentProfile.IsAnonymous;
        //        Result.IsDirty = CurrentProfile.IsDirty;
        //        Result.LastActivityDate = CurrentProfile.LastActivityDate;
        //        Result.LastUpdatedDate = CurrentProfile.LastUpdatedDate;
        //        Result.UserName = CurrentProfile.UserName;
        //    }
        //    CurrentLogs.WebProfiles.Add(Result);
        //}

        //private void GetCurrentWebPageDetails(Page CurrentWebPage)
        //{
        //    if (!CurrSession.CollectPerfLogs)
        //        return;

        //    WebPageLog Result = new WebPageLog();
        //    Result.DateFound = DateTime.Now;

        //    if (CurrentWebPage != null)
        //    {
        //        Result.AsyncTimeout = CurrentWebPage.AsyncTimeout.TotalSeconds;
        //        Result.Buffer = CurrentWebPage.Buffer;
        //        Result.ClientTarget = CurrentWebPage.ClientTarget;
        //        Result.CodePage = CurrentWebPage.CodePage;
        //        Result.ContentType = CurrentWebPage.ContentType;
        //        Result.Culture = CurrentWebPage.Culture;
        //        Result.EnableEventValidation = CurrentWebPage.EnableEventValidation;
        //        Result.EnableViewState = CurrentWebPage.EnableViewState;
        //        Result.EnableViewStateMac = CurrentWebPage.EnableViewStateMac;
        //        Result.ErrorPage = CurrentWebPage.ErrorPage;
        //        Result.IsAsync = CurrentWebPage.IsAsync;
        //        Result.IsCallback = CurrentWebPage.IsCallback;
        //        Result.IsCrossPagePostBack = CurrentWebPage.IsCrossPagePostBack;
        //        Result.IsPostBack = CurrentWebPage.IsPostBack;
        //        Result.IsPostBackEventControlRegistered = CurrentWebPage.IsPostBackEventControlRegistered;
        //        Result.IsReusable = CurrentWebPage.IsReusable;
        //        Result.IsValid = CurrentWebPage.IsValid;
        //        Result.LCID = CurrentWebPage.LCID;
        //        Result.MaintainScrollPositionOnPostBack = CurrentWebPage.MaintainScrollPositionOnPostBack;
        //        Result.MaxPageStateFieldLength = CurrentWebPage.MaxPageStateFieldLength;
        //        Result.MetaDescription = CurrentWebPage.MetaDescription;
        //        Result.MetaKeywords = CurrentWebPage.MetaKeywords;
        //        Result.ResponseEncoding = CurrentWebPage.ResponseEncoding;
        //        Result.SmartNavigation = CurrentWebPage.SmartNavigation;
        //        Result.StyleSheetTheme = CurrentWebPage.StyleSheetTheme;
        //        Result.Theme = CurrentWebPage.Theme;
        //        Result.Title = CurrentWebPage.Title;
        //        Result.TraceEnabled = CurrentWebPage.TraceEnabled;
        //        Result.TraceModeValue = (int)CurrentWebPage.TraceModeValue;
        //        Result.UICulture = CurrentWebPage.UICulture;
        //    }
        //    CurrentLogs.WebPages.Add(Result);
        //}


        #endregion

        #endregion

        /// <summary>
        /// Save All Logs to SQL storage in separate thread
        /// </summary>
        public Guid[] Save(bool StartNewLogsAfterSave = true)
        {
            var Copy = L;
            Ls.Add(Copy);

            var q = Ls.s(e =>
            {
                bool r = false;
                if (Sm != null)
                    r = Sm(e);
                if (r)
                    e.I();
                return r._c(e.LstId);
            });

            if (StartNewLogsAfterSave)
                L = new Lst().I();
            
            return q.s(e => e._2, e => !e._1);
        }
    }

    /// <summary>
    /// Common Logs Utilities
    /// </summary>
    public static class Log_Utilities
    {
        /// <summary>
        /// Get new Common SessionLog for current CollectPerfLogs to Log Init with error logs enabled
        /// </summary>
        public static Sld _Ls(this bool CollectPerfLogs, bool CollectErrorDetails = true, bool CollectPerfDetails = true, bool CollectUsers = true, bool CollectOSData = true, int? MaxLogsLimit = 10000)
        {
            var r = new Sld();

            r.I = _.g;
            r.A = Assembly.GetExecutingAssembly().ToString();

            r.Cp = CollectPerfLogs;
            r.Ced = CollectErrorDetails;
            r.Cpd = CollectPerfDetails;
            r.Cu = CollectUsers;
            r.Co = CollectOSData;
            r.M = MaxLogsLimit;

            return r;
        }

        /// <summary>
        /// Set Common ASP additional settings for  current CurrentSessionLog with error logs enabled
        /// </summary>
        public static Sld S(this Sld CurrentSessionLog, bool CollectErrorDetails = true, bool CollectPerfDetails = true, bool CollectUsers = true, bool CollectOSData = true, int? MaxLogsLimit = 10000)
        {
            var r = CurrentSessionLog;

            r.I = _.g;
            r.A = Assembly.GetExecutingAssembly().ToString();

            r.Ced = CollectErrorDetails;
            r.Cpd = CollectPerfDetails;
            r.Cu = CollectUsers;
            r.Co = CollectOSData;
            r.M = MaxLogsLimit;

            return CurrentSessionLog;
        }

        /// <summary>
        /// Get new logger with parameters from current CurrLogSession
        /// </summary>
        public static Lg N(this Sld CurrSession, f<Lst, bool> SaveLogsMethod, string ServerName = null, string DataBaseName = null)
        {
            return new Lg(SaveLogsMethod, CurrSession, ServerName, DataBaseName);
        }
    }

    /// <summary>
    /// Common Logs Storage Utilities
    /// </summary>
    public static class Log_Storage_Utilities
    {
        #region Logs storage

        //Sld S

        /// <summary>
        /// Init new Logs Storage for logger using CollectDetailsSettings
        /// </summary>
        public static Lst I(this Lst CurrLogsStorage, Sld CollectDetailsSettings)
        {
            var s = CollectDetailsSettings;
            //return CurrLogsStorage.I(s.Cp, s.Cpd, s.Cu, s.Chr, s.Chs);

            var r = CurrLogsStorage;

            r.C = new List<Cde>();
            r.E = new List<Err>();
            r.I = new List<Inf>();
            r.En = new List<Env>();
            if (s.Cp)
                r.P = new List<Prf>();
            if (s.Cpd)
            {
                r.Pr = new List<Prc>();
                r.Pd = new List<Prd>();

            }

            if (s.Chr)
                r.Hr = new List<Hrq>();
            if (s.Chs)
                r.Hs = new List<Hsl>();
            if (s.Chc)
                r.Hc = new List<Hcx>();
            if (s.Cwp)
                r.Wp = new List<Wpl>();
            if (s.Cwr)
                r.Wpr = new List<Wpr>();
            if (s.Chc)
                r.Hb = new List<Hbc>();

            if (s.Cu)
                r.Wi = new List<Wil>();

            return CurrLogsStorage;
        }


        /// <summary>
        /// Init new Logs Storage for logger
        /// </summary>
        public static Lst I(this Lst CurrLogsStorage, bool CollectPerfLogs = true, bool CollectProcessDetails = true, bool CollectUsers = true, bool CollectHttpWeb = true, bool CollectBrowsers = true)
        {
            var r = CurrLogsStorage;

            r.C = new List<Cde>();
            r.E = new List<Err>();
            r.I = new List<Inf>();
            r.En = new List<Env>();
            if (CollectPerfLogs)
                r.P = new List<Prf>();
            if (CollectProcessDetails)
            {
                r.Pr = new List<Prc>();
                r.Pd = new List<Prd>();

            }
            if (CollectHttpWeb)
            {
                r.Hr = new List<Hrq>();
                r.Hs = new List<Hsl>();
                r.Hc = new List<Hcx>();
                r.Wp = new List<Wpl>();
                if (CollectUsers)
                    r.Wpr = new List<Wpr>();
                if (CollectBrowsers)
                    r.Hb = new List<Hbc>();
            }
            if (CollectUsers)
                r.Wi = new List<Wil>();

            return CurrLogsStorage;
        }

        ///// <summary>
        ///// Save Current Logs Storage Data to SQL
        ///// </summary>
        //public static Lst S(this Lst CurrLogsStorage, string ServerName, string DataBaseName, Sld CurrSession, Cx CurrCode = null)
        //{
        //    var TablesToGet = "SessionLog".I("EntryData", "ErrorLog", "EnvironmentDetails"
        //        , "InfoLog", "PerformanceLog", "ProcessLog", "ProcessDetails", "HttpRequestLog",
        //        "HttpSessionLog", "HttpContextLog", "WebPageLog", "WebProfileLog", "HttpBrowsersLog", "WindowsIdentityLog");

        //    //var Curr = this;

        //    "Save".R(() =>
        //    {
        //        //var Conn = (ServerName ?? "localhost")._Qc(DataBaseName ?? "LogsNewFormat");
        //        //var CurrLogs = Conn._Gb(TablesToGet);

        //        //if (CurrLogs == null || CurrLogs.Count() != TablesToGet.Count())
        //        //    return false;

        //        //CurrSession.A(CurrLogs["SessionLog"]);

        //        //foreach (var i in CurrLogsStorage.Entries)
        //        //    i.A(CurrLogs["EntryData"], CurrSession.Session_UID);
        //        //foreach (var d in CurrLogsStorage.ErrorLogs)
        //        //    foreach (var i in d.Value)
        //        //        i.A(CurrLogs["ErrorLog"], CurrSession.Session_UID, d.Key);
        //        //foreach (var d in CurrLogsStorage.InfoLogs)
        //        //    foreach (var i in d.Value)
        //        //        i.A(CurrLogs["InfoLog"], CurrSession.Session_UID, d.Key);
        //        //foreach (var d in CurrLogsStorage.PerformanceLogs)
        //        //    foreach (var i in d.Value)
        //        //        i.A(CurrLogs["PerformanceLog"], CurrSession.Session_UID, d.Key);
        //        //foreach (var i in CurrLogsStorage.ProcessDetailsData)
        //        //    i.Value.A(CurrLogs["ProcessDetails"], CurrSession.Session_UID, i.Key);
        //        //foreach (var i in CurrLogsStorage.Environments)
        //        //    i.A(CurrLogs["EnvironmentDetails"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.ProcessLogs)
        //        //    i.A(CurrLogs["ProcessLog"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.HttpRequests)
        //        //    i.A(CurrLogs["HttpRequestLog"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.HttpSessions)
        //        //    i.A(CurrLogs["HttpSessionLog"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.HttpContexts)
        //        //    i.A(CurrLogs["HttpContextLog"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.WebPages)
        //        //    i.A(CurrLogs["WebPageLog"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.WebProfiles)
        //        //    i.A(CurrLogs["WebProfileLog"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.HttpBrowsers)
        //        //    i.A(CurrLogs["HttpBrowsersLog"], CurrSession.Session_UID);
        //        //foreach (var i in CurrLogsStorage.WindowsIdentities)
        //        //    i.A(CurrLogs["WindowsIdentityLog"], CurrSession.Session_UID);

        //        //Conn._Cp(CurrLogs);

        //        return true;
        //    }, false, "LogsStorage", CurrCode);
        //    return CurrLogsStorage;
        //}



        #endregion

        //#region Common storage structs

        ///// <summary>
        ///// Add current SessionLog to TableToAdd
        ///// </summary>
        //public static void A(this SessionLog CurrSessionLog, DataTable TableToAdd)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = CurrSessionLog.Session_UID;
        //    if (CurrSessionLog.SolutionTitle != null)
        //        New["SolutionTitle"] = CurrSessionLog.SolutionTitle;
        //    if (CurrSessionLog.AssemblyName != null)
        //        New["AssemblyName"] = CurrSessionLog.AssemblyName;
        //    New["CollectLogs"] = CurrSessionLog.CollectLogs;
        //    New["CollectPerfLogs"] = CurrSessionLog.CollectPerfLogs;
        //    New["CollectPerfDetails"] = CurrSessionLog.CollectPerfDetails;
        //    New["CollectUsers"] = CurrSessionLog.CollectUsers;
        //    New["CollectOSData"] = CurrSessionLog.CollectOSData;
        //    New["CollectErrorDetails"] = CurrSessionLog.CollectErrorDetails;
        //    New["CollectHttpRequests"] = CurrSessionLog.CollectHttpRequests;
        //    New["CollectHttpSessions"] = CurrSessionLog.CollectHttpSessions;
        //    New["CollectHttpContexts"] = CurrSessionLog.CollectHttpContexts;
        //    New["CollectWebPages"] = CurrSessionLog.CollectWebPages;
        //    New["CollectWebProfiles"] = CurrSessionLog.CollectWebProfiles;
        //    if (CurrSessionLog.MaxLogsLimit != null)
        //        New["MaxLogsLimit"] = CurrSessionLog.MaxLogsLimit.Value;

        //    TableToAdd.Rows.Add(New);

        //    //return CurrSessionLog;
        //}

        ///// <summary>
        ///// Add current EntryData to TableToAdd
        ///// </summary>
        //public static void A(this EntryData CurrEntryData, DataTable TableToAdd, Guid SessionID)
        //{
        //    foreach (var m in CurrEntryData.Methods)
        //    {
        //        var New = TableToAdd.NewRow();

        //        New["Session_UID"] = SessionID;
        //        if (CurrEntryData.Class != null)
        //            New["Class"] = CurrEntryData.Class;
        //        New["Entry_UID"] = m.Key;
        //        if (m.Value != null)
        //            New["Method"] = m.Value;
        //        TableToAdd.Rows.Add(New);
        //    }
        //    //return CurrEntryData;
        //}

        ///// <summary>
        ///// Add current ErrorLog to TableToAdd
        ///// </summary>
        //public static void A(this ErrorLog CurrErrorLog, DataTable TableToAdd, Guid SessionID, Guid EntryID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["Entry_UID"] = EntryID;
        //    New["DateFound"] = CurrErrorLog.DateFound;
        //    if (CurrErrorLog.ArrayItemSize != null)
        //        New["ArrayItemSize"] = CurrErrorLog.ArrayItemSize.Value;
        //    if (CurrErrorLog.LogIndex != null)
        //        New["LogIndex"] = CurrErrorLog.LogIndex.Value;
        //    if (CurrErrorLog.IsCritical != null)
        //        New["IsCritical"] = CurrErrorLog.IsCritical.Value;
        //    if (CurrErrorLog.ParametersData != null)
        //        New["ParametersData"] = CurrErrorLog.ParametersData;
        //    if (CurrErrorLog.Comments != null)
        //        New["Comments"] = CurrErrorLog.Comments;
        //    if (CurrErrorLog.ErrorDetails != null)
        //        New["ErrorDetails"] = CurrErrorLog.ErrorDetails;
        //    if (CurrErrorLog.ErrorStackTrace != null)
        //        New["ErrorStackTrace"] = CurrErrorLog.ErrorStackTrace;
        //    if (CurrErrorLog.ErrorToString != null)
        //        New["ErrorToString"] = CurrErrorLog.ErrorToString;
        //    if (CurrErrorLog.ErrorSource != null)
        //        New["ErrorSource"] = CurrErrorLog.ErrorSource;
        //    if (CurrErrorLog.ErrorTargetSite != null)
        //        New["ErrorTargetSite"] = CurrErrorLog.ErrorTargetSite;
        //    if (CurrErrorLog.ErrorMessage != null)
        //        New["ErrorMessage"] = CurrErrorLog.ErrorMessage;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrErrorLog;
        //}

        ///// <summary>
        ///// Add current EnvironmentDetails to TableToAdd
        ///// </summary>
        //public static void A(this EnvironmentDetails CurrEnvironmentDetails, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["Is64BitOperatingSystem"] = CurrEnvironmentDetails.Is64BitOperatingSystem;
        //    New["Is64BitProcess"] = CurrEnvironmentDetails.Is64BitProcess;
        //    New["UserInteractive"] = CurrEnvironmentDetails.UserInteractive;
        //    New["ProcessorCount"] = CurrEnvironmentDetails.ProcessorCount;
        //    New["SystemPageSize"] = CurrEnvironmentDetails.SystemPageSize;
        //    New["TickCount"] = CurrEnvironmentDetails.TickCount;
        //    New["WorkingSet"] = CurrEnvironmentDetails.WorkingSet;
        //    if (CurrEnvironmentDetails.MachineName != null)
        //        New["MachineName"] = CurrEnvironmentDetails.MachineName;
        //    if (CurrEnvironmentDetails.NewLine != null)
        //        New["NewLine"] = CurrEnvironmentDetails.NewLine;
        //    if (CurrEnvironmentDetails.UserDomainName != null)
        //        New["UserDomainName"] = CurrEnvironmentDetails.UserDomainName;
        //    if (CurrEnvironmentDetails.UserName != null)
        //        New["UserName"] = CurrEnvironmentDetails.UserName;
        //    if (CurrEnvironmentDetails.OSVersion_Platform != null)
        //        New["OSVersion_Platform"] = CurrEnvironmentDetails.OSVersion_Platform;
        //    if (CurrEnvironmentDetails.OSVersion_ServicePack != null)
        //        New["OSVersion_ServicePack"] = CurrEnvironmentDetails.OSVersion_ServicePack;
        //    if (CurrEnvironmentDetails.OSVersion_VersionString != null)
        //        New["OSVersion_VersionString"] = CurrEnvironmentDetails.OSVersion_VersionString;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrEnvironmentDetails;
        //}

        ///// <summary>
        ///// Add current InfoLog to TableToAdd
        ///// </summary>
        //public static void A(this InfoLog CurrInfoLog, DataTable TableToAdd, Guid SessionID, Guid EntryID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["Entry_UID"] = EntryID;
        //    New["DateFound"] = CurrInfoLog.DateFound;
        //    if (CurrInfoLog.ArrayItemSize != null)
        //        New["ArrayItemSize"] = CurrInfoLog.ArrayItemSize.Value;
        //    if (CurrInfoLog.LogIndex != null)
        //        New["LogIndex"] = CurrInfoLog.LogIndex.Value;
        //    if (CurrInfoLog.ParametersData != null)
        //        New["ParametersData"] = CurrInfoLog.ParametersData;
        //    if (CurrInfoLog.Comments != null)
        //        New["Comments"] = CurrInfoLog.Comments;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrInfoLog;
        //}

        ///// <summary>
        ///// Add current PerformanceLog to TableToAdd
        ///// </summary>
        //public static void A(this PerformanceLog CurrPerformanceLog, DataTable TableToAdd, Guid SessionID, Guid EntryID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["Entry_UID"] = EntryID;
        //    New["Start"] = CurrPerformanceLog.Start;
        //    if (CurrPerformanceLog.ArrayItemSize != null)
        //        New["ArrayItemSize"] = CurrPerformanceLog.ArrayItemSize.Value;
        //    if (CurrPerformanceLog.PerformanceIndex != null)
        //        New["PerformanceIndex"] = CurrPerformanceLog.PerformanceIndex.Value;
        //    if (CurrPerformanceLog.End != null)
        //        New["End"] = CurrPerformanceLog.End.Value;
        //    if (CurrPerformanceLog.ParametersData != null)
        //        New["ParametersData"] = CurrPerformanceLog.ParametersData;
        //    if (CurrPerformanceLog.Comments != null)
        //        New["Comments"] = CurrPerformanceLog.Comments;
        //    if (CurrPerformanceLog.Details != null)
        //        New["Details"] = CurrPerformanceLog.Details;
        //    if (CurrPerformanceLog.DetailsStart != null)
        //        New["DetailsStart"] = CurrPerformanceLog.DetailsStart.Value;
        //    if (CurrPerformanceLog.DetailsEnd != null)
        //        New["DetailsEnd"] = CurrPerformanceLog.DetailsEnd.Value;
        //    if (CurrPerformanceLog.TicksCount != null)
        //        New["TicksCount"] = CurrPerformanceLog.TicksCount.Value;
        //    if (CurrPerformanceLog.TotalMilliseconds != null)
        //        New["TotalMilliseconds"] = CurrPerformanceLog.TotalMilliseconds.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrPerformanceLog;
        //}

        ///// <summary>
        ///// Add current ProcessLog to TableToAdd
        ///// </summary>
        //public static void A(this ProcessLog CurrProcessLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrProcessLog.DateFound;
        //    if (CurrProcessLog.ProcessId != null)
        //        New["ProcessId"] = CurrProcessLog.ProcessId.Value;
        //    if (CurrProcessLog.SessionId != null)
        //        New["SessionId"] = CurrProcessLog.SessionId.Value;
        //    if (CurrProcessLog.StartTime != null)
        //        New["StartTime"] = CurrProcessLog.StartTime.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrProcessLog;
        //}

        ///// <summary>
        ///// Add current ProcessDetails to TableToAdd
        ///// </summary>
        //public static void A(this ProcessDetails CurrProcessDetails, DataTable TableToAdd, Guid SessionID, Guid DetailsID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["Details_UID"] = DetailsID;
        //    New["DateFound"] = CurrProcessDetails.DateFound;
        //    if (CurrProcessDetails.ThreadsCount != null)
        //        New["ThreadsCount"] = CurrProcessDetails.ThreadsCount.Value;
        //    if (CurrProcessDetails.HandleCount != null)
        //        New["HandleCount"] = CurrProcessDetails.HandleCount.Value;
        //    if (CurrProcessDetails.NonpagedSystemMemorySize64 != null)
        //        New["NonpagedSystemMemorySize64"] = CurrProcessDetails.NonpagedSystemMemorySize64.Value;
        //    if (CurrProcessDetails.PagedMemorySize64 != null)
        //        New["PagedMemorySize64"] = CurrProcessDetails.PagedMemorySize64;
        //    if (CurrProcessDetails.PagedSystemMemorySize64 != null)
        //        New["PagedSystemMemorySize64"] = CurrProcessDetails.PagedSystemMemorySize64;
        //    if (CurrProcessDetails.PeakPagedMemorySize64 != null)
        //        New["PeakPagedMemorySize64"] = CurrProcessDetails.PeakPagedMemorySize64;
        //    if (CurrProcessDetails.PeakVirtualMemorySize64 != null)
        //        New["PeakVirtualMemorySize64"] = CurrProcessDetails.PeakVirtualMemorySize64;
        //    if (CurrProcessDetails.PeakWorkingSet64 != null)
        //        New["PeakWorkingSet64"] = CurrProcessDetails.PeakWorkingSet64;
        //    if (CurrProcessDetails.PrivateMemorySize64 != null)
        //        New["PrivateMemorySize64"] = CurrProcessDetails.PrivateMemorySize64;
        //    if (CurrProcessDetails.VirtualMemorySize64 != null)
        //        New["VirtualMemorySize64"] = CurrProcessDetails.VirtualMemorySize64;
        //    if (CurrProcessDetails.WorkingSet64 != null)
        //        New["WorkingSet64"] = CurrProcessDetails.WorkingSet64;
        //    if (CurrProcessDetails.PrivilegedProcessorTime != null)
        //        New["PrivilegedProcessorTime"] = CurrProcessDetails.PrivilegedProcessorTime;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrProcessDetails;
        //}

        ///// <summary>
        ///// Add current WindowsIdentityLog to TableToAdd
        ///// </summary>
        //public static void A(this WindowsIdentityLog CurrWindowsIdentityLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrWindowsIdentityLog.DateFound;
        //    if (CurrWindowsIdentityLog.AuthenticationType != null)
        //        New["AuthenticationType"] = CurrWindowsIdentityLog.AuthenticationType;
        //    if (CurrWindowsIdentityLog.Name != null)
        //        New["Name"] = CurrWindowsIdentityLog.Name;
        //    if (CurrWindowsIdentityLog.ImpersonationLevel != null)
        //        New["ImpersonationLevel"] = CurrWindowsIdentityLog.ImpersonationLevel.Value;
        //    if (CurrWindowsIdentityLog.IsAnonymous != null)
        //        New["IsAnonymous"] = CurrWindowsIdentityLog.IsAnonymous.Value;
        //    if (CurrWindowsIdentityLog.IsAuthenticated != null)
        //        New["IsAuthenticated"] = CurrWindowsIdentityLog.IsAuthenticated.Value;
        //    if (CurrWindowsIdentityLog.IsAuthenticated != null)
        //        New["IsAuthenticated"] = CurrWindowsIdentityLog.IsAuthenticated.Value;
        //    if (CurrWindowsIdentityLog.IsGuest != null)
        //        New["IsGuest"] = CurrWindowsIdentityLog.IsGuest.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrWindowsIdentityLog;
        //}

        //#endregion

        //#region ASP storage structs

        ///// <summary>
        ///// Add current HttpRequestLog to TableToAdd
        ///// </summary>
        //public static void A(this HttpRequestLog CurrHttpRequestLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrHttpRequestLog.DateFound;

        //    if (CurrHttpRequestLog.UserHostName != null)
        //        New["UserHostName"] = CurrHttpRequestLog.UserHostName;
        //    if (CurrHttpRequestLog.UserHostAddress != null)
        //        New["UserHostAddress"] = CurrHttpRequestLog.UserHostAddress;
        //    if (CurrHttpRequestLog.UserAgent != null)
        //        New["UserAgent"] = CurrHttpRequestLog.UserAgent;
        //    if (CurrHttpRequestLog.UrlReferrer != null)
        //        New["UrlReferrer"] = CurrHttpRequestLog.UrlReferrer;
        //    if (CurrHttpRequestLog.TotalBytes != null)
        //        New["TotalBytes"] = CurrHttpRequestLog.TotalBytes.Value;
        //    if (CurrHttpRequestLog.RequestType != null)
        //        New["RequestType"] = CurrHttpRequestLog.RequestType;
        //    if (CurrHttpRequestLog.RawUrl != null)
        //        New["RawUrl"] = CurrHttpRequestLog.RawUrl;
        //    if (CurrHttpRequestLog.IsSecureConnection != null)
        //        New["IsSecureConnection"] = CurrHttpRequestLog.IsSecureConnection.Value;
        //    if (CurrHttpRequestLog.IsLocal != null)
        //        New["IsLocal"] = CurrHttpRequestLog.IsLocal.Value;
        //    if (CurrHttpRequestLog.IsAuthenticated != null)
        //        New["IsAuthenticated"] = CurrHttpRequestLog.IsAuthenticated.Value;
        //    if (CurrHttpRequestLog.HttpMethod != null)
        //        New["HttpMethod"] = CurrHttpRequestLog.HttpMethod;
        //    if (CurrHttpRequestLog.ContentType != null)
        //        New["ContentType"] = CurrHttpRequestLog.ContentType;
        //    if (CurrHttpRequestLog.ContentEncoding != null)
        //        New["ContentEncoding"] = CurrHttpRequestLog.ContentEncoding;
        //    if (CurrHttpRequestLog.ContentLength != null)
        //        New["ContentLength"] = CurrHttpRequestLog.ContentLength.Value;
        //    if (CurrHttpRequestLog.ApplicationPath != null)
        //        New["ApplicationPath"] = CurrHttpRequestLog.ApplicationPath;
        //    if (CurrHttpRequestLog.AnonymousID != null)
        //        New["AnonymousID"] = CurrHttpRequestLog.AnonymousID;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrHttpRequestLog;
        //}

        ///// <summary>
        ///// Add current HttpSessionLog to TableToAdd
        ///// </summary>
        //public static void A(this HttpSessionLog CurrHttpSessionLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrHttpSessionLog.DateFound;

        //    if (CurrHttpSessionLog.Timeout != null)
        //        New["Timeout"] = CurrHttpSessionLog.Timeout.Value;
        //    if (CurrHttpSessionLog.CookieMode != null)
        //        New["CookieMode"] = CurrHttpSessionLog.CookieMode.Value;
        //    if (CurrHttpSessionLog.Mode != null)
        //        New["Mode"] = CurrHttpSessionLog.Mode.Value;
        //    if (CurrHttpSessionLog.LCID != null)
        //        New["LCID"] = CurrHttpSessionLog.LCID.Value;
        //    if (CurrHttpSessionLog.IsSynchronized != null)
        //        New["IsSynchronized"] = CurrHttpSessionLog.IsSynchronized.Value;
        //    if (CurrHttpSessionLog.IsReadOnly != null)
        //        New["IsReadOnly"] = CurrHttpSessionLog.IsReadOnly.Value;
        //    if (CurrHttpSessionLog.IsNewSession != null)
        //        New["IsNewSession"] = CurrHttpSessionLog.IsNewSession.Value;
        //    if (CurrHttpSessionLog.IsCookieless != null)
        //        New["IsCookieless"] = CurrHttpSessionLog.IsCookieless.Value;
        //    if (CurrHttpSessionLog.Count != null)
        //        New["Count"] = CurrHttpSessionLog.Count.Value;
        //    if (CurrHttpSessionLog.CodePage != null)
        //        New["CodePage"] = CurrHttpSessionLog.CodePage.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrHttpSessionLog;
        //}

        ///// <summary>
        ///// Add current HttpContextLog to TableToAdd
        ///// </summary>
        //public static void A(this HttpContextLog CurrHttpContextLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrHttpContextLog.DateFound;

        //    if (CurrHttpContextLog.User != null)
        //        New["User"] = CurrHttpContextLog.User;
        //    if (CurrHttpContextLog.Server != null)
        //        New["Server"] = CurrHttpContextLog.Server;
        //    if (CurrHttpContextLog.IsPostNotification != null)
        //        New["IsPostNotification"] = CurrHttpContextLog.IsPostNotification.Value;
        //    if (CurrHttpContextLog.IsDebuggingEnabled != null)
        //        New["IsDebuggingEnabled"] = CurrHttpContextLog.IsDebuggingEnabled.Value;
        //    if (CurrHttpContextLog.IsCustomErrorEnabled != null)
        //        New["IsCustomErrorEnabled"] = CurrHttpContextLog.IsCustomErrorEnabled.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrHttpContextLog;
        //}

        ///// <summary>
        ///// Add current WebPageLog to TableToAdd
        ///// </summary>
        //public static void A(this WebPageLog CurrWebPageLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrWebPageLog.DateFound;

        //    if (CurrWebPageLog.StyleSheetTheme != null)
        //        New["StyleSheetTheme"] = CurrWebPageLog.StyleSheetTheme;
        //    if (CurrWebPageLog.Theme != null)
        //        New["Theme"] = CurrWebPageLog.Theme;
        //    if (CurrWebPageLog.Title != null)
        //        New["Title"] = CurrWebPageLog.Title;
        //    if (CurrWebPageLog.UICulture != null)
        //        New["UICulture"] = CurrWebPageLog.UICulture;
        //    if (CurrWebPageLog.TraceModeValue != null)
        //        New["TraceModeValue"] = CurrWebPageLog.TraceModeValue.Value;
        //    if (CurrWebPageLog.TraceEnabled != null)
        //        New["TraceEnabled"] = CurrWebPageLog.TraceEnabled.Value;
        //    if (CurrWebPageLog.SmartNavigation != null)
        //        New["SmartNavigation"] = CurrWebPageLog.SmartNavigation.Value;
        //    if (CurrWebPageLog.MaxPageStateFieldLength != null)
        //        New["MaxPageStateFieldLength"] = CurrWebPageLog.MaxPageStateFieldLength.Value;
        //    if (CurrWebPageLog.LCID != null)
        //        New["LCID"] = CurrWebPageLog.LCID.Value;
        //    if (CurrWebPageLog.ResponseEncoding != null)
        //        New["ResponseEncoding"] = CurrWebPageLog.ResponseEncoding;
        //    if (CurrWebPageLog.MetaKeywords != null)
        //        New["MetaKeywords"] = CurrWebPageLog.MetaKeywords;
        //    if (CurrWebPageLog.MetaDescription != null)
        //        New["MetaDescription"] = CurrWebPageLog.MetaDescription;
        //    if (CurrWebPageLog.MaintainScrollPositionOnPostBack != null)
        //        New["MaintainScrollPositionOnPostBack"] = CurrWebPageLog.MaintainScrollPositionOnPostBack.Value;
        //    if (CurrWebPageLog.IsValid != null)
        //        New["IsValid"] = CurrWebPageLog.IsValid.Value;
        //    if (CurrWebPageLog.IsReusable != null)
        //        New["IsReusable"] = CurrWebPageLog.IsReusable.Value;
        //    if (CurrWebPageLog.IsPostBackEventControlRegistered != null)
        //        New["IsPostBackEventControlRegistered"] = CurrWebPageLog.IsPostBackEventControlRegistered.Value;
        //    if (CurrWebPageLog.IsPostBack != null)
        //        New["IsPostBack"] = CurrWebPageLog.IsPostBack.Value;
        //    if (CurrWebPageLog.IsCrossPagePostBack != null)
        //        New["IsCrossPagePostBack"] = CurrWebPageLog.IsCrossPagePostBack.Value;
        //    if (CurrWebPageLog.IsCallback != null)
        //        New["IsCallback"] = CurrWebPageLog.IsCallback.Value;
        //    if (CurrWebPageLog.IsAsync != null)
        //        New["IsAsync"] = CurrWebPageLog.IsAsync.Value;
        //    if (CurrWebPageLog.EnableViewStateMac != null)
        //        New["EnableViewStateMac"] = CurrWebPageLog.EnableViewStateMac.Value;
        //    if (CurrWebPageLog.EnableViewState != null)
        //        New["EnableViewState"] = CurrWebPageLog.EnableViewState.Value;
        //    if (CurrWebPageLog.EnableEventValidation != null)
        //        New["EnableEventValidation"] = CurrWebPageLog.EnableEventValidation.Value;
        //    if (CurrWebPageLog.ErrorPage != null)
        //        New["ErrorPage"] = CurrWebPageLog.ErrorPage;
        //    if (CurrWebPageLog.Culture != null)
        //        New["Culture"] = CurrWebPageLog.Culture;
        //    if (CurrWebPageLog.ContentType != null)
        //        New["ContentType"] = CurrWebPageLog.ContentType;
        //    if (CurrWebPageLog.CodePage != null)
        //        New["CodePage"] = CurrWebPageLog.CodePage.Value;
        //    if (CurrWebPageLog.ClientTarget != null)
        //        New["ClientTarget"] = CurrWebPageLog.ClientTarget;
        //    if (CurrWebPageLog.Buffer != null)
        //        New["Buffer"] = CurrWebPageLog.Buffer.Value;
        //    if (CurrWebPageLog.AsyncTimeout != null)
        //        New["AsyncTimeout"] = CurrWebPageLog.AsyncTimeout.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrWebPageLog;
        //}

        ///// <summary>
        ///// Add current WebProfileLog to TableToAdd
        ///// </summary>
        //public static void A(this WebProfileLog CurrWebProfileLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrWebProfileLog.DateFound;

        //    if (CurrWebProfileLog.UserName != null)
        //        New["UserName"] = CurrWebProfileLog.UserName;
        //    if (CurrWebProfileLog.LastUpdatedDate != null)
        //        New["LastUpdatedDate"] = CurrWebProfileLog.LastUpdatedDate.Value;
        //    if (CurrWebProfileLog.LastActivityDate != null)
        //        New["LastActivityDate"] = CurrWebProfileLog.LastActivityDate.Value;
        //    if (CurrWebProfileLog.IsDirty != null)
        //        New["IsDirty"] = CurrWebProfileLog.IsDirty.Value;
        //    if (CurrWebProfileLog.IsAnonymous != null)
        //        New["IsAnonymous"] = CurrWebProfileLog.IsAnonymous.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrWebProfileLog;
        //}

        ///// <summary>
        ///// Add current HttpBrowserCapabilitiesLog to TableToAdd
        ///// </summary>
        //public static void A(this HttpBrowserCapabilitiesLog CurrHttpBrowserCapabilitiesLog, DataTable TableToAdd, Guid SessionID)
        //{
        //    var New = TableToAdd.NewRow();

        //    New["Session_UID"] = SessionID;
        //    New["DateFound"] = CurrHttpBrowserCapabilitiesLog.DateFound;

        //    if (CurrHttpBrowserCapabilitiesLog.Win32 != null)
        //        New["Win32"] = CurrHttpBrowserCapabilitiesLog.Win32.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.Win16 != null)
        //        New["Win16"] = CurrHttpBrowserCapabilitiesLog.Win16.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.W3CDomVersion != null)
        //        New["W3CDomVersion"] = CurrHttpBrowserCapabilitiesLog.W3CDomVersion;
        //    if (CurrHttpBrowserCapabilitiesLog.Version != null)
        //        New["Version"] = CurrHttpBrowserCapabilitiesLog.Version;
        //    if (CurrHttpBrowserCapabilitiesLog.VBScript != null)
        //        New["VBScript"] = CurrHttpBrowserCapabilitiesLog.VBScript.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.UseOptimizedCacheKey != null)
        //        New["UseOptimizedCacheKey"] = CurrHttpBrowserCapabilitiesLog.UseOptimizedCacheKey.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.Type != null)
        //        New["Type"] = CurrHttpBrowserCapabilitiesLog.Type;
        //    if (CurrHttpBrowserCapabilitiesLog.TagWriter != null)
        //        New["TagWriter"] = CurrHttpBrowserCapabilitiesLog.TagWriter;
        //    if (CurrHttpBrowserCapabilitiesLog.Tables != null)
        //        New["Tables"] = CurrHttpBrowserCapabilitiesLog.Tables.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsXmlHttp != null)
        //        New["SupportsXmlHttp"] = CurrHttpBrowserCapabilitiesLog.SupportsXmlHttp.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsUncheck != null)
        //        New["SupportsUncheck"] = CurrHttpBrowserCapabilitiesLog.SupportsUncheck.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsSelectMultiple != null)
        //        New["SupportsSelectMultiple"] = CurrHttpBrowserCapabilitiesLog.SupportsSelectMultiple.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsRedirectWithCookie != null)
        //        New["SupportsRedirectWithCookie"] = CurrHttpBrowserCapabilitiesLog.SupportsRedirectWithCookie.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsQueryStringInFormAction != null)
        //        New["SupportsQueryStringInFormAction"] = CurrHttpBrowserCapabilitiesLog.SupportsQueryStringInFormAction.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsJPhoneSymbols != null)
        //        New["SupportsJPhoneSymbols"] = CurrHttpBrowserCapabilitiesLog.SupportsJPhoneSymbols.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsJPhoneMultiMediaAttributes != null)
        //        New["SupportsJPhoneMultiMediaAttributes"] = CurrHttpBrowserCapabilitiesLog.SupportsJPhoneMultiMediaAttributes.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsItalic != null)
        //        New["SupportsItalic"] = CurrHttpBrowserCapabilitiesLog.SupportsItalic.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsInputMode != null)
        //        New["SupportsInputMode"] = CurrHttpBrowserCapabilitiesLog.SupportsInputMode.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsInputIStyle != null)
        //        New["SupportsInputIStyle"] = CurrHttpBrowserCapabilitiesLog.SupportsInputIStyle.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsIModeSymbols != null)
        //        New["SupportsIModeSymbols"] = CurrHttpBrowserCapabilitiesLog.SupportsIModeSymbols.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsImageSubmit != null)
        //        New["SupportsImageSubmit"] = CurrHttpBrowserCapabilitiesLog.SupportsImageSubmit.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsFontSize != null)
        //        New["SupportsFontSize"] = CurrHttpBrowserCapabilitiesLog.SupportsFontSize.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsFontName != null)
        //        New["SupportsFontName"] = CurrHttpBrowserCapabilitiesLog.SupportsFontName.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsFontColor != null)
        //        New["SupportsFontColor"] = CurrHttpBrowserCapabilitiesLog.SupportsFontColor.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsEmptyStringInCookieValue != null)
        //        New["SupportsEmptyStringInCookieValue"] = CurrHttpBrowserCapabilitiesLog.SupportsEmptyStringInCookieValue.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsDivNoWrap != null)
        //        New["SupportsDivNoWrap"] = CurrHttpBrowserCapabilitiesLog.SupportsDivNoWrap.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsDivAlign != null)
        //        New["SupportsDivAlign"] = CurrHttpBrowserCapabilitiesLog.SupportsDivAlign.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsCss != null)
        //        New["SupportsCss"] = CurrHttpBrowserCapabilitiesLog.SupportsCss.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsCallback != null)
        //        New["SupportsCallback"] = CurrHttpBrowserCapabilitiesLog.SupportsCallback.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsCacheControlMetaTag != null)
        //        New["SupportsCacheControlMetaTag"] = CurrHttpBrowserCapabilitiesLog.SupportsCacheControlMetaTag.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsBold != null)
        //        New["SupportsBold"] = CurrHttpBrowserCapabilitiesLog.SupportsBold.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsBodyColor != null)
        //        New["SupportsBodyColor"] = CurrHttpBrowserCapabilitiesLog.SupportsBodyColor.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.SupportsAccesskeyAttribute != null)
        //        New["SupportsAccesskeyAttribute"] = CurrHttpBrowserCapabilitiesLog.SupportsAccesskeyAttribute.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.ScreenPixelsWidth != null)
        //        New["ScreenPixelsWidth"] = CurrHttpBrowserCapabilitiesLog.ScreenPixelsWidth.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.ScreenPixelsHeight != null)
        //        New["ScreenPixelsHeight"] = CurrHttpBrowserCapabilitiesLog.ScreenPixelsHeight.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.ScreenCharactersWidth != null)
        //        New["ScreenCharactersWidth"] = CurrHttpBrowserCapabilitiesLog.ScreenCharactersWidth.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.ScreenCharactersHeight != null)
        //        New["ScreenCharactersHeight"] = CurrHttpBrowserCapabilitiesLog.ScreenCharactersHeight.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.ScreenBitDepth != null)
        //        New["ScreenBitDepth"] = CurrHttpBrowserCapabilitiesLog.ScreenBitDepth.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresUrlEncodedPostfieldValues != null)
        //        New["RequiresUrlEncodedPostfieldValues"] = CurrHttpBrowserCapabilitiesLog.RequiresUrlEncodedPostfieldValues.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresUniqueHtmlInputNames != null)
        //        New["RequiresUniqueHtmlInputNames"] = CurrHttpBrowserCapabilitiesLog.RequiresUniqueHtmlInputNames.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresUniqueHtmlCheckboxNames != null)
        //        New["RequiresUniqueHtmlCheckboxNames"] = CurrHttpBrowserCapabilitiesLog.RequiresUniqueHtmlCheckboxNames.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresUniqueFilePathSuffix != null)
        //        New["RequiresUniqueFilePathSuffix"] = CurrHttpBrowserCapabilitiesLog.RequiresUniqueFilePathSuffix.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresSpecialViewStateEncoding != null)
        //        New["RequiresSpecialViewStateEncoding"] = CurrHttpBrowserCapabilitiesLog.RequiresSpecialViewStateEncoding.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresPhoneNumbersAsPlainText != null)
        //        New["RequiresPhoneNumbersAsPlainText"] = CurrHttpBrowserCapabilitiesLog.RequiresPhoneNumbersAsPlainText.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresOutputOptimization != null)
        //        New["RequiresOutputOptimization"] = CurrHttpBrowserCapabilitiesLog.RequiresOutputOptimization.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresNoBreakInFormatting != null)
        //        New["RequiresNoBreakInFormatting"] = CurrHttpBrowserCapabilitiesLog.RequiresNoBreakInFormatting.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresLeadingPageBreak != null)
        //        New["RequiresLeadingPageBreak"] = CurrHttpBrowserCapabilitiesLog.RequiresLeadingPageBreak.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresHtmlAdaptiveErrorReporting != null)
        //        New["RequiresHtmlAdaptiveErrorReporting"] = CurrHttpBrowserCapabilitiesLog.RequiresHtmlAdaptiveErrorReporting.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresDBCSCharacter != null)
        //        New["RequiresDBCSCharacter"] = CurrHttpBrowserCapabilitiesLog.RequiresDBCSCharacter.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresControlStateInSession != null)
        //        New["RequiresControlStateInSession"] = CurrHttpBrowserCapabilitiesLog.RequiresControlStateInSession.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresContentTypeMetaTag != null)
        //        New["RequiresContentTypeMetaTag"] = CurrHttpBrowserCapabilitiesLog.RequiresContentTypeMetaTag.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiresAttributeColonSubstitution != null)
        //        New["RequiresAttributeColonSubstitution"] = CurrHttpBrowserCapabilitiesLog.RequiresAttributeColonSubstitution.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RequiredMetaTagNameValue != null)
        //        New["RequiredMetaTagNameValue"] = CurrHttpBrowserCapabilitiesLog.RequiredMetaTagNameValue;
        //    if (CurrHttpBrowserCapabilitiesLog.RendersWmlSelectsAsMenuCards != null)
        //        New["RendersWmlSelectsAsMenuCards"] = CurrHttpBrowserCapabilitiesLog.RendersWmlSelectsAsMenuCards.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RendersWmlDoAcceptsInline != null)
        //        New["RendersWmlDoAcceptsInline"] = CurrHttpBrowserCapabilitiesLog.RendersWmlDoAcceptsInline.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RendersBreaksAfterWmlInput != null)
        //        New["RendersBreaksAfterWmlInput"] = CurrHttpBrowserCapabilitiesLog.RendersBreaksAfterWmlInput.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RendersBreaksAfterWmlAnchor != null)
        //        New["RendersBreaksAfterWmlAnchor"] = CurrHttpBrowserCapabilitiesLog.RendersBreaksAfterWmlAnchor.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RendersBreaksAfterHtmlLists != null)
        //        New["RendersBreaksAfterHtmlLists"] = CurrHttpBrowserCapabilitiesLog.RendersBreaksAfterHtmlLists.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.RendersBreakBeforeWmlSelectAndInput != null)
        //        New["RendersBreakBeforeWmlSelectAndInput"] = CurrHttpBrowserCapabilitiesLog.RendersBreakBeforeWmlSelectAndInput.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.PreferredResponseEncoding != null)
        //        New["PreferredResponseEncoding"] = CurrHttpBrowserCapabilitiesLog.PreferredResponseEncoding;
        //    if (CurrHttpBrowserCapabilitiesLog.PreferredRequestEncoding != null)
        //        New["PreferredRequestEncoding"] = CurrHttpBrowserCapabilitiesLog.PreferredRequestEncoding;
        //    if (CurrHttpBrowserCapabilitiesLog.PreferredRenderingType != null)
        //        New["PreferredRenderingType"] = CurrHttpBrowserCapabilitiesLog.PreferredRenderingType;
        //    if (CurrHttpBrowserCapabilitiesLog.PreferredRenderingMime != null)
        //        New["PreferredRenderingMime"] = CurrHttpBrowserCapabilitiesLog.PreferredRenderingMime;
        //    if (CurrHttpBrowserCapabilitiesLog.PreferredImageMime != null)
        //        New["PreferredImageMime"] = CurrHttpBrowserCapabilitiesLog.PreferredImageMime;
        //    if (CurrHttpBrowserCapabilitiesLog.Platform != null)
        //        New["Platform"] = CurrHttpBrowserCapabilitiesLog.Platform;
        //    if (CurrHttpBrowserCapabilitiesLog.NumberOfSoftkeys != null)
        //        New["NumberOfSoftkeys"] = CurrHttpBrowserCapabilitiesLog.NumberOfSoftkeys.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.MSDomVersion != null)
        //        New["MSDomVersion"] = CurrHttpBrowserCapabilitiesLog.MSDomVersion;
        //    if (CurrHttpBrowserCapabilitiesLog.MobileDeviceModel != null)
        //        New["MobileDeviceModel"] = CurrHttpBrowserCapabilitiesLog.MobileDeviceModel;
        //    if (CurrHttpBrowserCapabilitiesLog.MobileDeviceManufacturer != null)
        //        New["MobileDeviceManufacturer"] = CurrHttpBrowserCapabilitiesLog.MobileDeviceManufacturer;
        //    if (CurrHttpBrowserCapabilitiesLog.MinorVersionString != null)
        //        New["MinorVersionString"] = CurrHttpBrowserCapabilitiesLog.MinorVersionString;
        //    if (CurrHttpBrowserCapabilitiesLog.MinorVersion != null)
        //        New["MinorVersion"] = CurrHttpBrowserCapabilitiesLog.MinorVersion.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.MaximumSoftkeyLabelLength != null)
        //        New["MaximumSoftkeyLabelLength"] = CurrHttpBrowserCapabilitiesLog.MaximumSoftkeyLabelLength.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.MaximumRenderedPageSize != null)
        //        New["MaximumRenderedPageSize"] = CurrHttpBrowserCapabilitiesLog.MaximumRenderedPageSize.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.MaximumHrefLength != null)
        //        New["MaximumHrefLength"] = CurrHttpBrowserCapabilitiesLog.MaximumHrefLength.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.MajorVersion != null)
        //        New["MajorVersion"] = CurrHttpBrowserCapabilitiesLog.MajorVersion.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.JScriptVersion != null)
        //        New["JScriptVersion"] = CurrHttpBrowserCapabilitiesLog.JScriptVersion;
        //    if (CurrHttpBrowserCapabilitiesLog.JavaApplets != null)
        //        New["JavaApplets"] = CurrHttpBrowserCapabilitiesLog.JavaApplets.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.IsMobileDevice != null)
        //        New["IsMobileDevice"] = CurrHttpBrowserCapabilitiesLog.IsMobileDevice.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.IsColor != null)
        //        New["IsColor"] = CurrHttpBrowserCapabilitiesLog.IsColor.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.InputType != null)
        //        New["InputType"] = CurrHttpBrowserCapabilitiesLog.InputType;
        //    if (CurrHttpBrowserCapabilitiesLog.Id != null)
        //        New["Id"] = CurrHttpBrowserCapabilitiesLog.Id;
        //    if (CurrHttpBrowserCapabilitiesLog.HtmlTextWriter != null)
        //        New["HtmlTextWriter"] = CurrHttpBrowserCapabilitiesLog.HtmlTextWriter;
        //    if (CurrHttpBrowserCapabilitiesLog.HidesRightAlignedMultiselectScrollbars != null)
        //        New["HidesRightAlignedMultiselectScrollbars"] = CurrHttpBrowserCapabilitiesLog.HidesRightAlignedMultiselectScrollbars.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.HasBackButton != null)
        //        New["HasBackButton"] = CurrHttpBrowserCapabilitiesLog.HasBackButton.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.GatewayVersion != null)
        //        New["GatewayVersion"] = CurrHttpBrowserCapabilitiesLog.GatewayVersion;
        //    if (CurrHttpBrowserCapabilitiesLog.GatewayMinorVersion != null)
        //        New["GatewayMinorVersion"] = CurrHttpBrowserCapabilitiesLog.GatewayMinorVersion.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.GatewayMajorVersion != null)
        //        New["GatewayMajorVersion"] = CurrHttpBrowserCapabilitiesLog.GatewayMajorVersion.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.Frames != null)
        //        New["Frames"] = CurrHttpBrowserCapabilitiesLog.Frames.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.EcmaScriptVersion != null)
        //        New["EcmaScriptVersion"] = CurrHttpBrowserCapabilitiesLog.EcmaScriptVersion;
        //    if (CurrHttpBrowserCapabilitiesLog.DefaultSubmitButtonLimit != null)
        //        New["DefaultSubmitButtonLimit"] = CurrHttpBrowserCapabilitiesLog.DefaultSubmitButtonLimit.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.Crawler != null)
        //        New["Crawler"] = CurrHttpBrowserCapabilitiesLog.Crawler.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.Cookies != null)
        //        New["Cookies"] = CurrHttpBrowserCapabilitiesLog.Cookies.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.ClrVersion != null)
        //        New["ClrVersion"] = CurrHttpBrowserCapabilitiesLog.ClrVersion;
        //    if (CurrHttpBrowserCapabilitiesLog.CDF != null)
        //        New["CDF"] = CurrHttpBrowserCapabilitiesLog.CDF.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanSendMail != null)
        //        New["CanSendMail"] = CurrHttpBrowserCapabilitiesLog.CanSendMail.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanRenderSetvarZeroWithMultiSelectionList != null)
        //        New["CanRenderSetvarZeroWithMultiSelectionList"] = CurrHttpBrowserCapabilitiesLog.CanRenderSetvarZeroWithMultiSelectionList.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanRenderPostBackCards != null)
        //        New["CanRenderPostBackCards"] = CurrHttpBrowserCapabilitiesLog.CanRenderPostBackCards.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanRenderOneventAndPrevElementsTogether != null)
        //        New["CanRenderOneventAndPrevElementsTogether"] = CurrHttpBrowserCapabilitiesLog.CanRenderOneventAndPrevElementsTogether.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanRenderMixedSelects != null)
        //        New["CanRenderMixedSelects"] = CurrHttpBrowserCapabilitiesLog.CanRenderMixedSelects.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanRenderInputAndSelectElementsTogether != null)
        //        New["CanRenderInputAndSelectElementsTogether"] = CurrHttpBrowserCapabilitiesLog.CanRenderInputAndSelectElementsTogether.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanRenderEmptySelects != null)
        //        New["CanRenderEmptySelects"] = CurrHttpBrowserCapabilitiesLog.CanRenderEmptySelects.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanRenderAfterInputOrSelectElement != null)
        //        New["CanRenderAfterInputOrSelectElement"] = CurrHttpBrowserCapabilitiesLog.CanRenderAfterInputOrSelectElement.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanInitiateVoiceCall != null)
        //        New["CanInitiateVoiceCall"] = CurrHttpBrowserCapabilitiesLog.CanInitiateVoiceCall.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.CanCombineFormsInDeck != null)
        //        New["CanCombineFormsInDeck"] = CurrHttpBrowserCapabilitiesLog.CanCombineFormsInDeck.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.Browser != null)
        //        New["Browser"] = CurrHttpBrowserCapabilitiesLog.Browser;
        //    if (CurrHttpBrowserCapabilitiesLog.Beta != null)
        //        New["Beta"] = CurrHttpBrowserCapabilitiesLog.Beta.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.BackgroundSounds != null)
        //        New["BackgroundSounds"] = CurrHttpBrowserCapabilitiesLog.BackgroundSounds.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.AOL != null)
        //        New["AOL"] = CurrHttpBrowserCapabilitiesLog.AOL.Value;
        //    if (CurrHttpBrowserCapabilitiesLog.ActiveXControls != null)
        //        New["ActiveXControls"] = CurrHttpBrowserCapabilitiesLog.ActiveXControls.Value;

        //    TableToAdd.Rows.Add(New);
        //    //return CurrHttpBrowserCapabilitiesLog;
        //}

        //#endregion

    }

    //#region Logs storage structs

    ///// <summary>
    ///// Simple Logs Storage Data
    ///// </summary>
    //public class LogsStorage
    //{
    //    public List<EntryData> Entries;
    //    public IDictionary<Guid, List<ErrorLog>> ErrorLogs;
    //    public IDictionary<Guid, List<InfoLog>> InfoLogs;
    //    public List<EnvironmentDetails> Environments;
    //    public IDictionary<Guid, List<PerformanceLog>> PerformanceLogs;
    //    public List<ProcessLog> ProcessLogs;
    //    public IDictionary<Guid, ProcessDetails> ProcessDetailsData;
    //    public List<HttpRequestLog> HttpRequests;
    //    public List<HttpSessionLog> HttpSessions;
    //    public List<HttpContextLog> HttpContexts;
    //    public List<WebPageLog> WebPages;
    //    public List<WebProfileLog> WebProfiles;
    //    public List<HttpBrowserCapabilitiesLog> HttpBrowsers;
    //    public List<WindowsIdentityLog> WindowsIdentities;

    //}

    ///// <summary>
    ///// Simple Session Log Data
    ///// </summary>
    //public struct SessionLog
    //{
    //    public Guid Session_UID;
    //    public string SolutionTitle;
    //    public string AssemblyName;

    //    public bool CollectLogs;
    //    public bool CollectPerfLogs;
    //    public bool CollectPerfDetails;
    //    public bool CollectUsers;
    //    public bool CollectOSData;
    //    public bool CollectErrorDetails;
    //    public bool CollectHttpRequests;
    //    public bool CollectHttpSessions;
    //    public bool CollectHttpContexts;
    //    public bool CollectWebPages;
    //    public bool CollectWebProfiles;
    //    public int? MaxLogsLimit;
    //}

    ///// <summary>
    ///// Simple Code Entry Data
    ///// </summary>
    //public struct EntryData
    //{
    //    public string Class;
    //    public IDictionary<Guid, string> Methods;

    //}

    ///// <summary>
    ///// Simple Error Log Data
    ///// </summary>
    //public struct ErrorLog
    //{
    //    public DateTime DateFound;
    //    public int? ArrayItemSize;
    //    public int? LogIndex;
    //    public bool? IsCritical;
    //    public string ParametersData;
    //    public string Comments;
    //    public string ErrorDetails;
    //    public string ErrorStackTrace;
    //    public string ErrorToString;
    //    public string ErrorSource;
    //    public string ErrorTargetSite;
    //    public string ErrorMessage;

    //}

    ///// <summary>
    ///// Simple Environment Details Data
    ///// </summary>
    //public struct EnvironmentDetails
    //{
    //    public DateTime CurrentTime;
    //    public bool Is64BitOperatingSystem;
    //    public bool Is64BitProcess;
    //    public bool UserInteractive;
    //    public int ProcessorCount;
    //    public int SystemPageSize;
    //    public int TickCount;
    //    public long WorkingSet;
    //    public string MachineName;
    //    public string NewLine;
    //    public string UserDomainName;
    //    public string UserName;
    //    public string OSVersion_Platform;
    //    public string OSVersion_ServicePack;
    //    public string OSVersion_VersionString;

    //}

    ///// <summary>
    ///// Simple Info Log Data
    ///// </summary>
    //public struct InfoLog
    //{
    //    public DateTime DateFound;
    //    public int? ArrayItemSize;
    //    public int? LogIndex;
    //    public string ParametersData;
    //    public string Comments;

    //}

    ///// <summary>
    ///// Simple Performance Log Data
    ///// </summary>
    //public struct PerformanceLog
    //{
    //    public DateTime Start;
    //    public DateTime? End;
    //    public int? ArrayItemSize;
    //    public int? PerformanceIndex;
    //    public string ParametersData;
    //    public string Details;
    //    public string Comments;
    //    public Guid? DetailsStart;
    //    public Guid? DetailsEnd;
    //    public long? TicksCount;
    //    public double? TotalMilliseconds;

    //}

    ///// <summary>
    ///// Simple Process Log Data
    ///// </summary>
    //public struct ProcessLog
    //{
    //    public DateTime DateFound;
    //    public int? ProcessId;
    //    public int? SessionId;
    //    public DateTime? StartTime;

    //}

    ///// <summary>
    ///// Simple Performance Log Data
    ///// </summary>
    //public struct ProcessDetails
    //{
    //    public DateTime DateFound;
    //    public int? ThreadsCount;
    //    public int? HandleCount;
    //    public long? NonpagedSystemMemorySize64;
    //    public long? PagedMemorySize64;
    //    public long? PagedSystemMemorySize64;
    //    public long? PeakPagedMemorySize64;
    //    public long? PeakVirtualMemorySize64;
    //    public long? PeakWorkingSet64;
    //    public long? PrivateMemorySize64;
    //    public long? VirtualMemorySize64;
    //    public long? WorkingSet64;
    //    public TimeSpan? PrivilegedProcessorTime;

    //}

    ///// <summary>
    ///// Simple Windows Identity Log Data
    ///// </summary>
    //public struct WindowsIdentityLog
    //{
    //    public DateTime DateFound;
    //    public string AuthenticationType;
    //    public string Name;
    //    public int? ImpersonationLevel;
    //    public bool? IsAnonymous;
    //    public bool? IsAuthenticated;
    //    public bool? IsGuest;
    //    public bool? IsSystem;

    //}

    //#endregion

    //#region ASP logging structs

    ///// <summary>
    ///// Simple HttpRequest Log Data
    ///// </summary>
    //public struct HttpRequestLog
    //{
    //    public DateTime DateFound;
    //    public string AnonymousID;
    //    public string ApplicationPath;
    //    public int? ContentLength;
    //    public string ContentEncoding;
    //    public string ContentType;
    //    public string HttpMethod;
    //    public bool? IsAuthenticated;
    //    public bool? IsLocal;
    //    public bool? IsSecureConnection;
    //    public string RawUrl;
    //    public string RequestType;
    //    public int? TotalBytes;
    //    public string UrlReferrer;
    //    public string UserAgent;
    //    public string UserHostAddress;
    //    public string UserHostName;

    //}

    ///// <summary>
    ///// Simple HttpSession Log Data
    ///// </summary>
    //public struct HttpSessionLog
    //{
    //    public DateTime DateFound;

    //    public int? CodePage;
    //    public int? Count;
    //    public bool? IsCookieless;
    //    public bool? IsNewSession;
    //    public bool? IsReadOnly;
    //    public bool? IsSynchronized;
    //    public int? LCID;
    //    public int? Mode;
    //    public int? CookieMode;
    //    public int? Timeout;

    //}

    ///// <summary>
    ///// Simple HttpContext Log Data
    ///// </summary>
    //public struct HttpContextLog
    //{
    //    public DateTime DateFound;

    //    public bool? IsCustomErrorEnabled;
    //    public bool? IsDebuggingEnabled;
    //    public bool? IsPostNotification;
    //    public string Server;
    //    public string User;

    //}

    ///// <summary>
    ///// Simple Web Page Log Data
    ///// </summary>
    //public struct WebPageLog
    //{
    //    public DateTime DateFound;

    //    public double? AsyncTimeout;
    //    public bool? Buffer;
    //    public string ClientTarget;
    //    public int? CodePage;
    //    public string ContentType;
    //    public string Culture;
    //    public string ErrorPage;
    //    public bool? EnableEventValidation;
    //    public bool? EnableViewState;
    //    public bool? EnableViewStateMac;
    //    public bool? IsAsync;
    //    public bool? IsCallback;
    //    public bool? IsCrossPagePostBack;
    //    public bool? IsPostBack;
    //    public bool? IsPostBackEventControlRegistered;
    //    public bool? IsReusable;
    //    public bool? IsValid;
    //    public bool? MaintainScrollPositionOnPostBack;
    //    public string MetaDescription;
    //    public string MetaKeywords;
    //    public string ResponseEncoding;
    //    public int? LCID;
    //    public int? MaxPageStateFieldLength;
    //    public bool? SmartNavigation;
    //    public bool? TraceEnabled;
    //    public int? TraceModeValue;
    //    public string UICulture;
    //    public string Title;
    //    public string Theme;
    //    public string StyleSheetTheme;

    //}

    ///// <summary>
    ///// Simple User WebProfile Log Data
    ///// </summary>
    //public struct WebProfileLog
    //{
    //    public DateTime DateFound;

    //    public bool? IsAnonymous;
    //    public bool? IsDirty;
    //    public DateTime? LastActivityDate;
    //    public DateTime? LastUpdatedDate;
    //    public string UserName;

    //}

    ///// <summary>
    ///// Simple Http Browser Capabilities Log Data
    ///// </summary>
    //public struct HttpBrowserCapabilitiesLog
    //{
    //    public DateTime DateFound;

    //    public bool? Win32;
    //    public bool? Win16;
    //    public string W3CDomVersion;
    //    public string Version;
    //    public bool? VBScript;
    //    public bool? UseOptimizedCacheKey;
    //    public string Type;
    //    public string TagWriter;
    //    public bool? Tables;
    //    public bool? SupportsXmlHttp;
    //    public bool? SupportsUncheck;
    //    public bool? SupportsSelectMultiple;
    //    public bool? SupportsRedirectWithCookie;
    //    public bool? SupportsQueryStringInFormAction;
    //    public bool? SupportsJPhoneSymbols;
    //    public bool? SupportsJPhoneMultiMediaAttributes;
    //    public bool? SupportsItalic;
    //    public bool? SupportsInputMode;
    //    public bool? SupportsInputIStyle;
    //    public bool? SupportsIModeSymbols;
    //    public bool? SupportsImageSubmit;
    //    public bool? SupportsFontSize;
    //    public bool? SupportsFontName;
    //    public bool? SupportsFontColor;
    //    public bool? SupportsEmptyStringInCookieValue;
    //    public bool? SupportsDivNoWrap;
    //    public bool? SupportsDivAlign;
    //    public bool? SupportsCss;
    //    public bool? SupportsCallback;
    //    public bool? SupportsCacheControlMetaTag;
    //    public bool? SupportsBold;
    //    public bool? SupportsBodyColor;
    //    public bool? SupportsAccesskeyAttribute;
    //    public int? ScreenPixelsWidth;
    //    public int? ScreenPixelsHeight;
    //    public int? ScreenCharactersWidth;
    //    public int? ScreenCharactersHeight;
    //    public int? ScreenBitDepth;
    //    public bool? RequiresUrlEncodedPostfieldValues;
    //    public bool? RequiresUniqueHtmlInputNames;
    //    public bool? RequiresUniqueHtmlCheckboxNames;
    //    public bool? RequiresUniqueFilePathSuffix;
    //    public bool? RequiresSpecialViewStateEncoding;
    //    public bool? RequiresPhoneNumbersAsPlainText;
    //    public bool? RequiresOutputOptimization;
    //    public bool? RequiresNoBreakInFormatting;
    //    public bool? RequiresLeadingPageBreak;
    //    public bool? RequiresHtmlAdaptiveErrorReporting;
    //    public bool? RequiresDBCSCharacter;
    //    public bool? RequiresControlStateInSession;
    //    public bool? RequiresContentTypeMetaTag;
    //    public bool? RequiresAttributeColonSubstitution;
    //    public string RequiredMetaTagNameValue;
    //    public bool? RendersWmlSelectsAsMenuCards;
    //    public bool? RendersWmlDoAcceptsInline;
    //    public bool? RendersBreaksAfterWmlInput;
    //    public bool? RendersBreaksAfterWmlAnchor;
    //    public bool? RendersBreaksAfterHtmlLists;
    //    public bool? RendersBreakBeforeWmlSelectAndInput;
    //    public string PreferredResponseEncoding;
    //    public string PreferredRequestEncoding;
    //    public string PreferredRenderingType;
    //    public string PreferredRenderingMime;
    //    public string PreferredImageMime;
    //    public string Platform;
    //    public int? NumberOfSoftkeys;
    //    public string MSDomVersion;
    //    public string MobileDeviceModel;
    //    public string MobileDeviceManufacturer;
    //    public string MinorVersionString;
    //    public double? MinorVersion;
    //    public int? MaximumSoftkeyLabelLength;
    //    public int? MaximumRenderedPageSize;
    //    public int? MaximumHrefLength;
    //    public int? MajorVersion;
    //    public string JScriptVersion;
    //    public bool? JavaApplets;
    //    public bool? IsMobileDevice;
    //    public bool? IsColor;
    //    public string InputType;
    //    public string Id;
    //    public string HtmlTextWriter;
    //    public bool? HidesRightAlignedMultiselectScrollbars;
    //    public bool? HasBackButton;
    //    public string GatewayVersion;
    //    public double? GatewayMinorVersion;
    //    public int? GatewayMajorVersion;
    //    public bool? Frames;
    //    public string EcmaScriptVersion;
    //    public int? DefaultSubmitButtonLimit;
    //    public bool? Crawler;
    //    public bool? Cookies;
    //    public string ClrVersion;
    //    public bool? CDF;
    //    public bool? CanSendMail;
    //    public bool? CanRenderSetvarZeroWithMultiSelectionList;
    //    public bool? CanRenderPostBackCards;
    //    public bool? CanRenderOneventAndPrevElementsTogether;
    //    public bool? CanRenderMixedSelects;
    //    public bool? CanRenderInputAndSelectElementsTogether;
    //    public bool? CanRenderEmptySelects;
    //    public bool? CanRenderAfterInputOrSelectElement;
    //    public bool? CanInitiateVoiceCall;
    //    public bool? CanCombineFormsInDeck;
    //    public string Browser;
    //    public bool? Beta;
    //    public bool? BackgroundSounds;
    //    public bool? AOL;
    //    public bool? ActiveXControls;

    //}

    //#endregion
}
