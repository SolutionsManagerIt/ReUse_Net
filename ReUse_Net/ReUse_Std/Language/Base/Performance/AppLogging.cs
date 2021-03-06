﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// Curr Session
        /// </summary>
        private Sld S;
        /// <summary>
        /// Method To Save Logs Sync
        /// </summary>
        private f<Lst, bool> Sm;
        /// <summary>
        /// Method To Save Logs Async
        /// </summary>
        private f<Lst, Task<bool>> Sa;

        /// <summary>
        /// Check settings
        /// </summary>
        private Sls C;

        /// <summary>
        /// Max records per block limit
        /// </summary>
        private int Rl;

        #endregion

        /// <summary>
        /// Start Sync logger with SyncSaveLogsMethod and parameters from CurrLogSession
        /// </summary>
        public Lg(f<Lst, bool> SyncSaveLogsMethod, Sld CurrSession = null)
        {
            Sm = SyncSaveLogsMethod;
            I(CurrSession);
        }

        /// <summary>
        /// Start Async logger with AsyncSaveLogsMethod and parameters from CurrLogSession
        /// </summary>
        public Lg(f<Lst, Task<bool>> AsyncSaveLogsMethod, Sld CurrSession = null)
        {
            Sa = AsyncSaveLogsMethod;
            I(CurrSession);
        }

        #region public methods
        #region Error and Info Log Entry

        #region Errors

        /// <summary>
        /// Create New Error Log Entry
        /// </summary>
        public void E(string CustomComments = null, Exception CurrExc = null, Cx CurrCodeType = null, Mx CustomMethodContext = null)
        {
            Ae(CurrCodeType, CustomComments, CurrExc, CustomMethodContext);
        }

        #endregion

        #region Info

        /// <summary>
        /// Create New Info Log Entry
        /// </summary>
        public void I(string Comments = null, Cx CurrCodeType = null, Mx CustomMethodContext = null)
        {
            Ai(CurrCodeType, Comments, CustomMethodContext);
        }

        #endregion

        #endregion

        #region Performance Log Entry

        #region Create New

        /// <summary>
        /// Create New Performance Log Entry
        /// </summary>
        /// <returns>Performance Log Entry Record</returns>
        public Prf P(Cx CurrCodeType = null, Mx CustomMethodContext = null)
        {

            return Pn(CurrCodeType, CustomMethodContext);
        }

        #endregion

        #region Add Created Log To Storage

        /// <summary>
        /// Add Created Performance Log Record To Storage
        /// </summary>
        public void Pa(Prf Log, string CustomComments = null, Cx CurrCodeType = null)
        {
            Pa(Log, CurrCodeType, CustomComments);
        }

        #endregion

        #endregion 

        #region Details Logging

        /// <summary>
        /// Get Current Process Common Details
        /// </summary>
        public Guid? Pc(Cx CurrCodeType)
        {
            if (C == null || C.Pr != true)
                return null;

            Prc r = new Prc();

            if (Pr == null)
                Pr = Process.GetCurrentProcess();

            if (Pr != null)
            {
                Pr.Refresh();
                r.Ip = Pr.Id;
                r.Is = Pr.SessionId;
                r.S = Pr.StartTime;
            }
            L.Pr.Add(r);
            return r.PrcId;
        }

        /// <summary>
        /// Get Current Process Performance Details
        /// </summary>
        public Guid? Pp(Cx CurrCodeType)
        {
            if (C == null || C.Pp != true)
                return null;

            Prd r = new Prd();

            if (Pr == null)
                Pr = Process.GetCurrentProcess();

            if (Pr != null)
            {
                try
                {
                    Pr.Refresh();
                    r.Ch = Pr.HandleCount;
                    r.Sx = Pr.NonpagedSystemMemorySize64;
                    r.Mx = Pr.PagedMemorySize64;
                    r.Sp = Pr.PagedSystemMemorySize64;
                    r.Px = Pr.PeakPagedMemorySize64;
                    r.Vp = Pr.PeakVirtualMemorySize64;
                    r.Wp = Pr.PeakWorkingSet64;
                    r.Mp = Pr.PrivateMemorySize64;
                    r.Tp = Pr.PrivilegedProcessorTime;
                    r.Ct = Pr.Threads.Count;
                    r.V = Pr.VirtualMemorySize64;
                    r.W = Pr.WorkingSet64;
                }
                catch (Exception exc)
                {
                    int ttt = 555;
                }
            }

            L.Pd.Add(r);
            return r.PrdId;
        }

        /// <summary>
        /// Get Environment Details
        /// </summary>
        public Guid? En(Cx CurrCodeType)
        {
            if (C == null || C.En != true)
                return null;

            Env r = new Env();

            r.Xo = Environment.Is64BitOperatingSystem;
            r.Xp = Environment.Is64BitProcess;
            r.M = Environment.MachineName;
            r.N = Environment.NewLine;
            r.Pc = Environment.ProcessorCount;
            r.Sp = Environment.SystemPageSize;
            r.Ct = Environment.TickCount;

            if (C.U == true)
            {
                r.Ud = Environment.UserDomainName;
                r.Ui = Environment.UserInteractive;
                r.Un = Environment.UserName;
            }

            r.W = Environment.WorkingSet;

            if (C.O == true && Environment.OSVersion != null)
            {
                r.Op = Environment.OSVersion.Platform.ToString();
                r.Os = Environment.OSVersion.ServicePack;
                r.Ov = Environment.OSVersion.VersionString;
            }

            L.En.Add(r);
            return r.EnvId;
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Start Sync logger with parameters from CurrLogSession
        /// </summary>
        public void I(Sld CurrSession = null)
        {
            S = CurrSession;
            L = new Lst().I(S);
            Ls = new List<Lst>();
            Rn = 0;
            Rl = -1;
            if (S.M != null && S.M.Value > 1)
                Rl = S.M.Value;
            if (S != null && S.S != null && S.S.L == true)
                C = S.S;
        }

        #region Error and Info Log Entry

        /// <summary>
        /// Add Log Error
        /// </summary>
        private void Ae(Cx CurrCodeType, string Comments = null, Exception CurrExc = null, Mx CustomMethodContext = null)
        {
            if (C == null || C.L != true)
                return;

            Ce(CurrCodeType, CustomMethodContext);
            var r = new Err() { C = Comments };

            if (CurrCodeType != null)
                r.X = CurrCodeType.CxId;
            if (CustomMethodContext != null)
                r.M = CustomMethodContext.CmId;

            if (CurrExc != null)
            {
                r.Esr = CurrExc.Source;
                r.Em = CurrExc.Message;
                if (S.S.Ea != true)
                {
                    r.Et = CurrExc.StackTrace;
                    r.Es = CurrExc.ToString();
                    r.Ets = CurrExc.TargetSite.ToString();
                }
            }
            L.E.Add(r);
            Rn++;

            if (Rl > 1 && Rn > Rl)
                Sv();
        }

        /// <summary>
        /// Add Log Information
        /// </summary>
        private void Ai(Cx CurrCodeType, string Comments = null, Mx CustomMethodContext = null)
        {
            if (C == null || C.L != true)
                return;

            Ce(CurrCodeType, CustomMethodContext);
            var r = new Inf() { C = Comments };
            if (CurrCodeType != null)
                r.X = CurrCodeType.CxId;
            if (CustomMethodContext != null)
                r.M = CustomMethodContext.CmId;

            L.I.Add(r);
            Rn++;

            if (Rl > 1 && Rn > Rl)
                Sv();
        }

        #endregion

        #region Perf Log Entry

        /// <summary>
        /// Create New Log Entry
        /// </summary>
        private Prf Pn(Cx CurrCodeType, Mx CustomMethodContext = null)
        {
            if (C == null || C.P != true)
                return null;

            Ce(CurrCodeType, CustomMethodContext);
            Prf r = new Prf();

            if (C.Pr == true && CurrCodeType != null && CurrCodeType.S != null && CurrCodeType.S.Pr == true)
                r.Is = Pp(CurrCodeType);
            if (CurrCodeType != null)
                r.X = CurrCodeType.CxId;
            if (CustomMethodContext != null)
                r.M = CustomMethodContext.CmId;
            return r;
        }

        /// <summary>
        /// Add Created Perf Record
        /// </summary>
        private void Pa(Prf CurrLog, Cx CurrCodeType, string Comments = null)
        {
            if (C == null || C.P != true || CurrLog == null)
                return;

            var Log = CurrLog;

            if (C.Pr == true && CurrCodeType != null && CurrCodeType.S != null && CurrCodeType.S.Pr == true)
                Log.Ie = Pp(CurrCodeType);

            Log.C = Comments;
            var End = _.d;

            Log.De = End;
            Log.T = (End - Log.Ds).Ticks;
            Log.Ms = (End - Log.Ds).TotalMilliseconds;

            L.P.Add(Log);
            Rn++;

            if (Rl > 1 && Rn > Rl)
                Sv();
        }

        #endregion

        #region Details Logging

        /// <summary>
        /// Add details on contexts with check
        /// </summary>
        private void Ce(Cx CurrCodeType = null, Mx CustomMethodContext = null)
        {
            if (CurrCodeType != null && !L.X.Contains(CurrCodeType))
                L.X.Add(CurrCodeType);
            if (CustomMethodContext != null && !L.C.Contains(CustomMethodContext))
                L.C.Add(CustomMethodContext);
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
        /// Save All Logs Sync to SQL storage
        /// </summary>
        public Guid[] Sv(bool StartNewLogsAfterSave = true)
        {
            if (Sm == null)
                return null;

            if(L.En.Count < 1 && L.C.Count < 1 && L.Pd.Count < 1 && L.Pr.Count < 1)
                return null;

            var Copy = L;
            Ls.Add(Copy);

            var q = Ls.s(e =>
            {
                var r = Sm(e);
                if (r)
                    e.I(S);
                return r._c(e.LstId);
            });

            if (StartNewLogsAfterSave)
                L = new Lst().I(S);

            return q.s(e => e._2, e => !e._1);
        }

        /// <summary>
        /// Save All Logs Async to SQL storage in separate thread
        /// </summary>
        public async Task<Guid[]> Sva(bool StartNewLogsAfterSave = true)
        {
            if (Sa == null)
                return null;

            if (L.En.Count < 1 && L.C.Count < 1 && L.Pd.Count < 1 && L.Pr.Count < 1)
                return null;

            var Copy = L;
            Ls.Add(Copy);
            
            var q = Ls.s(async e =>
            {
                var r = await Sa(e);
                if (r)
                    e.I(S);
                return r._c(e.LstId);
            });

            var t = await Task.WhenAll(q);

            if (StartNewLogsAfterSave)
                L = new Lst().I(S);

            return t.s(e => e._2, e => !e._1);
        }
    }

    /// <summary>
    /// Common Logs Utilities
    /// </summary>
    public static class Log_Utilities
    {

        ///// <summary>
        ///// Set Common ASP additional settings for  current CurrentSessionLog with error logs enabled
        ///// </summary>
        //public static Sld S(this Sld CurrentSessionLog, bool CollectErrorDetails = true, bool CollectPerfDetails = true, bool CollectUsers = true, bool CollectOSData = true, int? MaxLogsLimit = 10000)
        //{
        //    var r = CurrentSessionLog;

        //    r.I = _.g;
        //    r.A = Assembly.GetExecutingAssembly().ToString();

        //    r.Ced = CollectErrorDetails;
        //    r.Cpd = CollectPerfDetails;
        //    r.Cu = CollectUsers;
        //    r.Co = CollectOSData;
        //    r.M = MaxLogsLimit;

        //    return CurrentSessionLog;
        //}

        /// <summary>
        /// Get new Sync logger with parameters from current CurrLogSession
        /// </summary>
        public static Lg N(this Sld CurrSession, f<Lst, bool> SyncSaveLogsMethod)
        {
            return new Lg(SyncSaveLogsMethod, CurrSession);
        }

        /// <summary>
        /// Get new Async logger with parameters from current CurrLogSession
        /// </summary>
        public static Lg N(this Sld CurrSession, f<Lst, Task<bool>> AsyncSaveLogsMethod)
        {
            return new Lg(AsyncSaveLogsMethod, CurrSession);
        }

        #region Logs storage

        /// <summary>
        /// Init new Logs Storage for logger using CollectDetailsSettings
        /// </summary>
        public static Lst I(this Lst CurrLogsStorage, Sld CollectDetailsSettings)
        {
            var r = CurrLogsStorage;

            r.S = CollectDetailsSettings;
            
            if (r.S != null && r.S.S != null)
            {
                r.C = new List<Mx>();
                r.X = new List<Cx>();
                r.E = new List<Err>();
                r.I = new List<Inf>();

                var s = r.S.S;
                if (s.En == true)
                    r.En = new List<Env>();
                if (s.P == true)
                    r.P = new List<Prf>();
                if (s.Pr == true)
                    r.Pr = new List<Prc>();
                if (s.Pp == true)
                    r.Pd = new List<Prd>();
                if (s.Hr == true)
                    r.Hr = new List<Hrq>();
                if (s.Hs == true)
                    r.Hs = new List<Hsl>();
                if (s.Hc == true)
                    r.Hc = new List<Hcx>();
                if (s.Wp == true)
                    r.Wp = new List<Wpl>();
                if (s.Wr == true)
                    r.Wpr = new List<Wpr>();
                if (s.B == true)
                    r.Hb = new List<Hbc>();
                if (s.U == true)
                    r.Wi = new List<Wil>();
            }

            return CurrLogsStorage;
        }

        #endregion
    }
}
