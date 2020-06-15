using System;
using System.Collections.Generic;
using System.Text;

namespace ReUse_Std.AppDataModels.Logging
{
    class Logging
    {
    }


    #region Logs storage structs

    /// <summary>
    /// Simple Logs Storage Data
    /// </summary>
    public struct Lgs
    {

        public List<EntryData> Entries;
        public IDictionary<Guid, List<ErrorLog>> ErrorLogs;
        public IDictionary<Guid, List<InfoLog>> InfoLogs;
        public List<EnvironmentDetails> Environments;
        public IDictionary<Guid, List<PerformanceLog>> PerformanceLogs;
        public List<ProcessLog> ProcessLogs;
        public IDictionary<Guid, ProcessDetails> ProcessDetailsData;
        public List<HttpRequestLog> HttpRequests;
        public List<HttpSessionLog> HttpSessions;
        public List<HttpContextLog> HttpContexts;
        public List<WebPageLog> WebPages;
        public List<WebProfileLog> WebProfiles;
        public List<HttpBrowserCapabilitiesLog> HttpBrowsers;
        public List<WindowsIdentityLog> WindowsIdentities;

    }

    /// <summary>
    /// Simple Session Log Data
    /// </summary>
    public struct Ls
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid LsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SolutionTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CollectLogs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectPerfLogs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectPerfDetails { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectOSData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectErrorDetails { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectHttpRequests { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectHttpSessions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectHttpContexts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectWebPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CollectWebProfiles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? MaxLogsLimit { get; set; }
    }

    /// <summary>
    /// Simple Code Entry Data
    /// </summary>
    public struct EntryData
    {
        public string Class;
        public IDictionary<Guid, string> Methods;

    }

    /// <summary>
    /// Simple Error Log Data
    /// </summary>
    public struct ErrorLog
    {
        public DateTime DateFound;
        public int? ArrayItemSize;
        public int? LogIndex;
        public bool? IsCritical;
        public string ParametersData;
        public string Comments;
        public string ErrorDetails;
        public string ErrorStackTrace;
        public string ErrorToString;
        public string ErrorSource;
        public string ErrorTargetSite;
        public string ErrorMessage;

    }

    /// <summary>
    /// Simple Environment Details Data
    /// </summary>
    public struct EnvironmentDetails
    {
        public DateTime CurrentTime;
        public bool Is64BitOperatingSystem;
        public bool Is64BitProcess;
        public bool UserInteractive;
        public int ProcessorCount;
        public int SystemPageSize;
        public int TickCount;
        public long WorkingSet;
        public string MachineName;
        public string NewLine;
        public string UserDomainName;
        public string UserName;
        public string OSVersion_Platform;
        public string OSVersion_ServicePack;
        public string OSVersion_VersionString;

    }

    /// <summary>
    /// Simple Info Log Data
    /// </summary>
    public struct InfoLog
    {
        public DateTime DateFound;
        public int? ArrayItemSize;
        public int? LogIndex;
        public string ParametersData;
        public string Comments;

    }

    /// <summary>
    /// Simple Performance Log Data
    /// </summary>
    public struct PerformanceLog
    {
        public DateTime Start;
        public DateTime? End;
        public int? ArrayItemSize;
        public int? PerformanceIndex;
        public string ParametersData;
        public string Details;
        public string Comments;
        public Guid? DetailsStart;
        public Guid? DetailsEnd;
        public long? TicksCount;
        public double? TotalMilliseconds;

    }

    /// <summary>
    /// Simple Process Log Data
    /// </summary>
    public struct ProcessLog
    {
        public DateTime DateFound;
        public int? ProcessId;
        public int? SessionId;
        public DateTime? StartTime;

    }

    /// <summary>
    /// Simple Performance Log Data
    /// </summary>
    public struct ProcessDetails
    {
        public DateTime DateFound;
        public int? ThreadsCount;
        public int? HandleCount;
        public long? NonpagedSystemMemorySize64;
        public long? PagedMemorySize64;
        public long? PagedSystemMemorySize64;
        public long? PeakPagedMemorySize64;
        public long? PeakVirtualMemorySize64;
        public long? PeakWorkingSet64;
        public long? PrivateMemorySize64;
        public long? VirtualMemorySize64;
        public long? WorkingSet64;
        public TimeSpan? PrivilegedProcessorTime;

    }

    /// <summary>
    /// Simple Windows Identity Log Data
    /// </summary>
    public struct WindowsIdentityLog
    {
        public DateTime DateFound;
        public string AuthenticationType;
        public string Name;
        public int? ImpersonationLevel;
        public bool? IsAnonymous;
        public bool? IsAuthenticated;
        public bool? IsGuest;
        public bool? IsSystem;

    }

    #endregion

    #region ASP logging structs

    /// <summary>
    /// Simple HttpRequest Log Data
    /// </summary>
    public struct HttpRequestLog
    {
        public DateTime DateFound;
        public string AnonymousID;
        public string ApplicationPath;
        public int? ContentLength;
        public string ContentEncoding;
        public string ContentType;
        public string HttpMethod;
        public bool? IsAuthenticated;
        public bool? IsLocal;
        public bool? IsSecureConnection;
        public string RawUrl;
        public string RequestType;
        public int? TotalBytes;
        public string UrlReferrer;
        public string UserAgent;
        public string UserHostAddress;
        public string UserHostName;

    }

    /// <summary>
    /// Simple HttpSession Log Data
    /// </summary>
    public struct HttpSessionLog
    {
        public DateTime DateFound;

        public int? CodePage;
        public int? Count;
        public bool? IsCookieless;
        public bool? IsNewSession;
        public bool? IsReadOnly;
        public bool? IsSynchronized;
        public int? LCID;
        public int? Mode;
        public int? CookieMode;
        public int? Timeout;

    }

    /// <summary>
    /// Simple HttpContext Log Data
    /// </summary>
    public struct HttpContextLog
    {
        public DateTime DateFound;

        public bool? IsCustomErrorEnabled;
        public bool? IsDebuggingEnabled;
        public bool? IsPostNotification;
        public string Server;
        public string User;

    }

    /// <summary>
    /// Simple Web Page Log Data
    /// </summary>
    public struct WebPageLog
    {
        public DateTime DateFound;

        public double? AsyncTimeout;
        public bool? Buffer;
        public string ClientTarget;
        public int? CodePage;
        public string ContentType;
        public string Culture;
        public string ErrorPage;
        public bool? EnableEventValidation;
        public bool? EnableViewState;
        public bool? EnableViewStateMac;
        public bool? IsAsync;
        public bool? IsCallback;
        public bool? IsCrossPagePostBack;
        public bool? IsPostBack;
        public bool? IsPostBackEventControlRegistered;
        public bool? IsReusable;
        public bool? IsValid;
        public bool? MaintainScrollPositionOnPostBack;
        public string MetaDescription;
        public string MetaKeywords;
        public string ResponseEncoding;
        public int? LCID;
        public int? MaxPageStateFieldLength;
        public bool? SmartNavigation;
        public bool? TraceEnabled;
        public int? TraceModeValue;
        public string UICulture;
        public string Title;
        public string Theme;
        public string StyleSheetTheme;

    }

    /// <summary>
    /// Simple User WebProfile Log Data
    /// </summary>
    public struct WebProfileLog
    {
        public DateTime DateFound;

        public bool? IsAnonymous;
        public bool? IsDirty;
        public DateTime? LastActivityDate;
        public DateTime? LastUpdatedDate;
        public string UserName;

    }

    /// <summary>
    /// Simple Http Browser Capabilities Log Data
    /// </summary>
    public struct HttpBrowserCapabilitiesLog
    {
        public DateTime DateFound;

        public bool? Win32;
        public bool? Win16;
        public string W3CDomVersion;
        public string Version;
        public bool? VBScript;
        public bool? UseOptimizedCacheKey;
        public string Type;
        public string TagWriter;
        public bool? Tables;
        public bool? SupportsXmlHttp;
        public bool? SupportsUncheck;
        public bool? SupportsSelectMultiple;
        public bool? SupportsRedirectWithCookie;
        public bool? SupportsQueryStringInFormAction;
        public bool? SupportsJPhoneSymbols;
        public bool? SupportsJPhoneMultiMediaAttributes;
        public bool? SupportsItalic;
        public bool? SupportsInputMode;
        public bool? SupportsInputIStyle;
        public bool? SupportsIModeSymbols;
        public bool? SupportsImageSubmit;
        public bool? SupportsFontSize;
        public bool? SupportsFontName;
        public bool? SupportsFontColor;
        public bool? SupportsEmptyStringInCookieValue;
        public bool? SupportsDivNoWrap;
        public bool? SupportsDivAlign;
        public bool? SupportsCss;
        public bool? SupportsCallback;
        public bool? SupportsCacheControlMetaTag;
        public bool? SupportsBold;
        public bool? SupportsBodyColor;
        public bool? SupportsAccesskeyAttribute;
        public int? ScreenPixelsWidth;
        public int? ScreenPixelsHeight;
        public int? ScreenCharactersWidth;
        public int? ScreenCharactersHeight;
        public int? ScreenBitDepth;
        public bool? RequiresUrlEncodedPostfieldValues;
        public bool? RequiresUniqueHtmlInputNames;
        public bool? RequiresUniqueHtmlCheckboxNames;
        public bool? RequiresUniqueFilePathSuffix;
        public bool? RequiresSpecialViewStateEncoding;
        public bool? RequiresPhoneNumbersAsPlainText;
        public bool? RequiresOutputOptimization;
        public bool? RequiresNoBreakInFormatting;
        public bool? RequiresLeadingPageBreak;
        public bool? RequiresHtmlAdaptiveErrorReporting;
        public bool? RequiresDBCSCharacter;
        public bool? RequiresControlStateInSession;
        public bool? RequiresContentTypeMetaTag;
        public bool? RequiresAttributeColonSubstitution;
        public string RequiredMetaTagNameValue;
        public bool? RendersWmlSelectsAsMenuCards;
        public bool? RendersWmlDoAcceptsInline;
        public bool? RendersBreaksAfterWmlInput;
        public bool? RendersBreaksAfterWmlAnchor;
        public bool? RendersBreaksAfterHtmlLists;
        public bool? RendersBreakBeforeWmlSelectAndInput;
        public string PreferredResponseEncoding;
        public string PreferredRequestEncoding;
        public string PreferredRenderingType;
        public string PreferredRenderingMime;
        public string PreferredImageMime;
        public string Platform;
        public int? NumberOfSoftkeys;
        public string MSDomVersion;
        public string MobileDeviceModel;
        public string MobileDeviceManufacturer;
        public string MinorVersionString;
        public double? MinorVersion;
        public int? MaximumSoftkeyLabelLength;
        public int? MaximumRenderedPageSize;
        public int? MaximumHrefLength;
        public int? MajorVersion;
        public string JScriptVersion;
        public bool? JavaApplets;
        public bool? IsMobileDevice;
        public bool? IsColor;
        public string InputType;
        public string Id;
        public string HtmlTextWriter;
        public bool? HidesRightAlignedMultiselectScrollbars;
        public bool? HasBackButton;
        public string GatewayVersion;
        public double? GatewayMinorVersion;
        public int? GatewayMajorVersion;
        public bool? Frames;
        public string EcmaScriptVersion;
        public int? DefaultSubmitButtonLimit;
        public bool? Crawler;
        public bool? Cookies;
        public string ClrVersion;
        public bool? CDF;
        public bool? CanSendMail;
        public bool? CanRenderSetvarZeroWithMultiSelectionList;
        public bool? CanRenderPostBackCards;
        public bool? CanRenderOneventAndPrevElementsTogether;
        public bool? CanRenderMixedSelects;
        public bool? CanRenderInputAndSelectElementsTogether;
        public bool? CanRenderEmptySelects;
        public bool? CanRenderAfterInputOrSelectElement;
        public bool? CanInitiateVoiceCall;
        public bool? CanCombineFormsInDeck;
        public string Browser;
        public bool? Beta;
        public bool? BackgroundSounds;
        public bool? AOL;
        public bool? ActiveXControls;

    }

    #endregion



}
