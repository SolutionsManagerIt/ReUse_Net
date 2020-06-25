using System;
using System.Collections.Generic;
using System.Text;
using ReUse_Std.Base;

namespace ReUse_Std.AppDataModels.Logging
{
    class Logging
    {
    }


    #region Logs storage structs

    /// <summary>
    /// Simple Logs Storage Data
    /// </summary>
    public class Lst
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid LstId { get; set; } = _.g;

        /// <summary>
        /// Entries
        /// </summary>
        public List<Cde> C { get; set; }
        /// <summary>
        /// Error Logs
        /// </summary>
        public List<Err> E { get; set; }
        /// <summary>
        /// Info Logs
        /// </summary>
        public List<Inf> I { get; set; }
        /// <summary>
        /// Environment Details
        /// </summary>
        public List<Env> En { get; set; }
        /// <summary>
        /// Performance Logs
        /// </summary>
        public List<Prf> P { get; set; }
        /// <summary>
        /// Process Logs
        /// </summary>
        public List<Prc> Pr { get; set; }
        /// <summary>
        /// Process Details Data
        /// </summary>
        public List<Prd> Pd { get; set; }
        /// <summary>
        /// Http Requests
        /// </summary>
        public List<Hrq> Hr { get; set; }
        /// <summary>
        /// Http Sessions
        /// </summary>
        public List<Hsl> Hs { get; set; }
        /// <summary>
        /// Http Contexts
        /// </summary>
        public List<Hcx> Hc { get; set; }
        /// <summary>
        /// Web Pages
        /// </summary>
        public List<Wpl> Wp { get; set; }
        /// <summary>
        /// Web Profiles
        /// </summary>
        public List<Wpr> Wpr { get; set; }
        /// <summary>
        /// Http Browsers
        /// </summary>
        public List<Hbc> Hb { get; set; }
        /// <summary>
        /// Windows Identities
        /// </summary>
        public List<Wil> Wi { get; set; }

    }

    /// <summary>
    /// Simple Session Log Data
    /// </summary>
    public class Sld
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid SldId { get; set; } = _.g;

        /// <summary>
        /// Session_UID
        /// </summary>
        public Guid I { get; set; }
        /// <summary>
        /// SolutionTitle
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// AssemblyName
        /// </summary>
        public string A { get; set; }

        /// <summary>
        /// CollectLogs
        /// </summary>
        public bool Cl { get; set; }
        /// <summary>
        /// CollectPerfLogs
        /// </summary>
        public bool Cp { get; set; }
        /// <summary>
        /// CollectPerfDetails
        /// </summary>
        public bool Cpd { get; set; }

        /// <summary>
        /// CollectProcessLogs
        /// </summary>
        public bool Cpr { get; set; }
        /// <summary>
        /// CollectProcessDetails
        /// </summary>
        public bool Cprd { get; set; }
        /// <summary>
        /// CollectUsers
        /// </summary>
        public bool Cu { get; set; }
        /// <summary>
        /// CollectOSData
        /// </summary>
        public bool Co { get; set; }
        /// <summary>
        /// CollectErrorDetails
        /// </summary>
        public bool Ced { get; set; }
        /// <summary>
        /// CollectHttpRequests
        /// </summary>
        public bool Chr { get; set; }
        /// <summary>
        /// CollectHttpSessions
        /// </summary>
        public bool Chs { get; set; }
        /// <summary>
        /// CollectHttpContexts
        /// </summary>
        public bool Chc { get; set; }
        /// <summary>
        /// CollectWebPages
        /// </summary>
        public bool Cwp { get; set; }
        /// <summary>
        /// CollectWebProfiles
        /// </summary>
        public bool Cwr { get; set; }
        /// <summary>
        /// Collect Browser Details
        /// </summary>
        public bool Cb { get; set; }
        /// <summary>
        /// MaxLogsLimit
        /// </summary>
        public int? M { get; set; }
        /// <summary>
        /// DateTime Now
        /// </summary>
        public DateTime D { get; set; } = _.D;
        /// <summary>
        /// DateTime UtcNow
        /// </summary>
        public DateTime Du { get; set; } = _.d;
    }

    /// <summary>
    /// Simple Code Entry Data
    /// </summary>
    public class Cde
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid CdeId { get; set; } = _.g;

        /// <summary>
        /// Class name
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// Class Methods list
        /// </summary>
        public List<Cme> M { get; set; }

    }

    /// <summary>
    /// Simple Methods Entry Data
    /// </summary>
    public class Cme
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid CmeId { get; set; } = _.g;
        /// <summary>
        /// MethodGuid
        /// </summary>
        public Guid G { get; set; }
        /// <summary>
        /// Method
        /// </summary>
        public string M { get; set; }

    }

    /// <summary>
    /// Simple Error Log Data
    /// </summary>
    public class Err
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid ErrId { get; set; } = _.g;

        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime D { get; set; } = _.d;
        /// <summary>
        /// ArrayItemSize
        /// </summary>
        public int? As { get; set; }
        /// <summary>
        /// LogIndex
        /// </summary>
        public int? I { get; set; }
        /// <summary>
        /// IsCritical
        /// </summary>
        public bool? Cr { get; set; }
        /// <summary>
        /// ParametersData
        /// </summary>
        public string P { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// ErrorDetails
        /// </summary>
        public string Ed { get; set; }
        /// <summary>
        /// ErrorStackTrace
        /// </summary>
        public string Et { get; set; }
        /// <summary>
        /// ErrorToString
        /// </summary>
        public string Es { get; set; }
        /// <summary>
        /// ErrorSource
        /// </summary>
        public string Esr { get; set; }
        /// <summary>
        /// ErrorTargetSite
        /// </summary>
        public string Ets { get; set; }
        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string Em { get; set; }

    }

    /// <summary>
    /// Simple Environment Details Data
    /// </summary>
    public class Env
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid EnvId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// CurrentTime
        /// </summary>
        public DateTime D { get; set; } = _.d;
        /// <summary>
        /// Is64BitOperatingSystem
        /// </summary>
        public bool? Xo { get; set; }
        /// <summary>
        /// Is64BitProcess
        /// </summary>
        public bool? Xp { get; set; }
        /// <summary>
        /// UserInteractive
        /// </summary>
        public bool? Ui { get; set; }
        /// <summary>
        /// ProcessorCount
        /// </summary>
        public int? Pc { get; set; }
        /// <summary>
        /// SystemPageSize
        /// </summary>
        public int? Sp { get; set; }
        /// <summary>
        /// TickCount
        /// </summary>
        public int? Ct { get; set; }
        /// <summary>
        /// WorkingSet
        /// </summary>
        public long? W { get; set; }
        /// <summary>
        /// MachineName
        /// </summary>
        public string M { get; set; }
        /// <summary>
        /// NewLine
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// UserDomainName
        /// </summary>
        public string Ud { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string Un { get; set; }
        /// <summary>
        /// OSVersion_Platform
        /// </summary>
        public string Op { get; set; }
        /// <summary>
        /// OSVersion_ServicePack
        /// </summary>
        public string Os { get; set; }
        /// <summary>
        /// OSVersion_VersionString
        /// </summary>
        public string Ov { get; set; }

    }

    /// <summary>
    /// Simple Info Log Data
    /// </summary>
    public class Inf
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid InfId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime Df { get; set; } = _.d;
        /// <summary>
        /// ArrayItemSize
        /// </summary>
        public int? As { get; set; }
        /// <summary>
        /// LogIndex
        /// </summary>
        public int? I { get; set; }
        /// <summary>
        /// ParametersData
        /// </summary>
        public string P { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string C { get; set; }

    }

    /// <summary>
    /// Simple Performance Log Data
    /// </summary>
    public class Prf
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid PrfId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// Start
        /// </summary>
        public DateTime Ds { get; set; } = _.d;
        /// <summary>
        /// End
        /// </summary>
        public DateTime? De { get; set; }
        /// <summary>
        /// ArrayItemSize
        /// </summary>
        public int? As { get; set; }
        /// <summary>
        /// PerformanceIndex
        /// </summary>
        public int? I { get; set; }
        /// <summary>
        /// ParametersData
        /// </summary>
        public string P { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        public string D { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// DetailsStart
        /// </summary>
        public Guid? Is { get; set; }
        /// <summary>
        /// DetailsEnd
        /// </summary>
        public Guid? Ie { get; set; }
        /// <summary>
        /// TicksCount
        /// </summary>
        public long? T { get; set; }
        /// <summary>
        /// TotalMilliseconds
        /// </summary>
        public double? M { get; set; }

    }

    /// <summary>
    /// Simple Process Log Data
    /// </summary>
    public class Prc
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid PrcId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime D { get; set; } = _.d;
        /// <summary>
        /// ProcessId
        /// </summary>
        public int? Ip { get; set; }
        /// <summary>
        /// SessionId
        /// </summary>
        public int? Is { get; set; }
        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime? S { get; set; }

    }

    /// <summary>
    /// Simple Process Details Log Data
    /// </summary>
    public class Prd
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid PrdId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime D { get; set; } = _.d;
        /// <summary>
        /// ThreadsCount
        /// </summary>
        public int? Ct { get; set; }
        /// <summary>
        /// HandleCount
        /// </summary>
        public int? Ch { get; set; }
        /// <summary>
        /// NonpagedSystemMemorySize64
        /// </summary>
        public long? Sx { get; set; }
        /// <summary>
        /// PagedMemorySize64
        /// </summary>
        public long? Mx { get; set; }
        /// <summary>
        /// PagedSystemMemorySize64
        /// </summary>
        public long? Sp { get; set; }
        /// <summary>
        /// PeakPagedMemorySize64
        /// </summary>
        public long? Px { get; set; }
        /// <summary>
        /// PeakVirtualMemorySize64
        /// </summary>
        public long? Vp { get; set; }
        /// <summary>
        /// PeakWorkingSet64
        /// </summary>
        public long? Wp { get; set; }
        /// <summary>
        /// PrivateMemorySize64
        /// </summary>
        public long? Mp { get; set; }
        /// <summary>
        /// VirtualMemorySize64
        /// </summary>
        public long? V { get; set; }
        /// <summary>
        /// WorkingSet64
        /// </summary>
        public long? W { get; set; }
        /// <summary>
        /// PrivilegedProcessorTime
        /// </summary>
        public TimeSpan? Tp { get; set; }

    }

    /// <summary>
    /// Simple Windows Identity Log Data
    /// </summary>
    public class Wil
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid WilId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime D { get; set; } = _.d;
        /// <summary>
        /// AuthenticationType
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// ImpersonationLevel
        /// </summary>
        public int? L { get; set; }
        /// <summary>
        /// IsAnonymous
        /// </summary>
        public bool? A { get; set; }
        /// <summary>
        /// IsAuthenticated
        /// </summary>
        public bool? U { get; set; }
        /// <summary>
        /// IsGuest
        /// </summary>
        public bool? G { get; set; }
        /// <summary>
        /// IsSystem
        /// </summary>
        public bool? S { get; set; }

    }

    #endregion

    #region ASP logging structs

    /// <summary>
    /// Simple HttpRequest Log Data
    /// </summary>
    public class Hrq
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid HrqId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime DateFound { get; set; } = _.d;
        /// <summary>
        /// AnonymousID
        /// </summary>
        public string AnonymousID { get; set; }
        /// <summary>
        /// ApplicationPath
        /// </summary>
        public string ApplicationPath { get; set; }
        /// <summary>
        /// ContentLength
        /// </summary>
        public int? ContentLength { get; set; }
        /// <summary>
        /// ContentEncoding
        /// </summary>
        public string ContentEncoding { get; set; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// HttpMethod
        /// </summary>
        public string HttpMethod { get; set; }
        /// <summary>
        /// IsAuthenticated
        /// </summary>
        public bool? IsAuthenticated { get; set; }
        /// <summary>
        /// IsLocal
        /// </summary>
        public bool? IsLocal { get; set; }
        /// <summary>
        /// IsSecureConnection
        /// </summary>
        public bool? IsSecureConnection { get; set; }
        /// <summary>
        /// RawUrl
        /// </summary>
        public string RawUrl { get; set; }
        /// <summary>
        /// RequestType
        /// </summary>
        public string RequestType { get; set; }
        /// <summary>
        /// TotalBytes
        /// </summary>
        public int? TotalBytes { get; set; }
        /// <summary>
        /// UrlReferrer
        /// </summary>
        public string UrlReferrer { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// UserHostAddress
        /// </summary>
        public string UserHostAddress { get; set; }
        /// <summary>
        /// UserHostName
        /// </summary>
        public string UserHostName { get; set; }

    }

    /// <summary>
    /// Simple HttpSession Log Data
    /// </summary>
    public class Hsl
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid HslId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime DateFound { get; set; } = _.d;

        /// <summary>
        /// CodePage
        /// </summary>
        public int? CodePage { get; set; }
        /// <summary>
        /// Count
        /// </summary>
        public int? Count { get; set; }
        /// <summary>
        /// IsCookieless
        /// </summary>
        public bool? IsCookieless { get; set; }
        /// <summary>
        /// IsNewSession
        /// </summary>
        public bool? IsNewSession { get; set; }
        /// <summary>
        /// IsReadOnly
        /// </summary>
        public bool? IsReadOnly { get; set; }
        /// <summary>
        /// IsSynchronized
        /// </summary>
        public bool? IsSynchronized { get; set; }
        /// <summary>
        /// LCID
        /// </summary>
        public int? LCID { get; set; }
        /// <summary>
        /// Mode
        /// </summary>
        public int? Mode { get; set; }
        /// <summary>
        /// CookieMode
        /// </summary>
        public int? CookieMode { get; set; }
        /// <summary>
        /// Timeout
        /// </summary>
        public int? Timeout { get; set; }

    }

    /// <summary>
    /// Simple HttpContext Log Data
    /// </summary>
    public class Hcx
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid HcxId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime DateFound { get; set; } = _.d;

        /// <summary>
        /// IsCustomErrorEnabled
        /// </summary>
        public bool? IsCustomErrorEnabled { get; set; }
        /// <summary>
        /// IsDebuggingEnabled
        /// </summary>
        public bool? IsDebuggingEnabled { get; set; }
        /// <summary>
        /// IsPostNotification
        /// </summary>
        public bool? IsPostNotification { get; set; }
        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public string User { get; set; }

    }

    /// <summary>
    /// Simple Web Page Log Data
    /// </summary>
    public class Wpl
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid WplId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime DateFound { get; set; } = _.d;

        /// <summary>
        /// AsyncTimeout
        /// </summary>
        public double? AsyncTimeout { get; set; }
        /// <summary>
        /// Buffer
        /// </summary>
        public bool? Buffer { get; set; }
        /// <summary>
        /// ClientTarget
        /// </summary>
        public string ClientTarget { get; set; }
        /// <summary>
        /// CodePage
        /// </summary>
        public int? CodePage { get; set; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Culture
        /// </summary>
        public string Culture { get; set; }
        /// <summary>
        /// ErrorPage
        /// </summary>
        public string ErrorPage { get; set; }
        /// <summary>
        /// EnableEventValidation
        /// </summary>
        public bool? EnableEventValidation { get; set; }
        /// <summary>
        /// EnableViewState
        /// </summary>
        public bool? EnableViewState { get; set; }
        /// <summary>
        /// EnableViewStateMac
        /// </summary>
        public bool? EnableViewStateMac { get; set; }
        /// <summary>
        /// IsAsync
        /// </summary>
        public bool? IsAsync { get; set; }
        /// <summary>
        /// IsCallback
        /// </summary>
        public bool? IsCallback { get; set; }
        /// <summary>
        /// IsCrossPagePostBack
        /// </summary>
        public bool? IsCrossPagePostBack { get; set; }
        /// <summary>
        /// IsPostBack
        /// </summary>
        public bool? IsPostBack { get; set; }
        /// <summary>
        /// IsPostBackEventControlRegistered
        /// </summary>
        public bool? IsPostBackEventControlRegistered { get; set; }
        /// <summary>
        /// IsReusable
        /// </summary>
        public bool? IsReusable { get; set; }
        /// <summary>
        /// IsValid
        /// </summary>
        public bool? IsValid { get; set; }
        /// <summary>
        /// MaintainScrollPositionOnPostBack
        /// </summary>
        public bool? MaintainScrollPositionOnPostBack { get; set; }
        /// <summary>
        /// MetaDescription
        /// </summary>
        public string MetaDescription { get; set; }
        /// <summary>
        /// MetaKeywords
        /// </summary>
        public string MetaKeywords { get; set; }
        /// <summary>
        /// ResponseEncoding
        /// </summary>
        public string ResponseEncoding { get; set; }
        /// <summary>
        /// LCID
        /// </summary>
        public int? LCID { get; set; }
        /// <summary>
        /// MaxPageStateFieldLength
        /// </summary>
        public int? MaxPageStateFieldLength { get; set; }
        /// <summary>
        /// SmartNavigation
        /// </summary>
        public bool? SmartNavigation { get; set; }
        /// <summary>
        /// TraceEnabled
        /// </summary>
        public bool? TraceEnabled { get; set; }
        /// <summary>
        /// TraceModeValue
        /// </summary>
        public int? TraceModeValue { get; set; }
        /// <summary>
        /// UICulture
        /// </summary>
        public string UICulture { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Theme
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// StyleSheetTheme
        /// </summary>
        public string StyleSheetTheme { get; set; }

    }

    /// <summary>
    /// Simple User WebProfile Log Data
    /// </summary>
    public class Wpr
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid WprId { get; set; } = _.g;
        /// <summary>
        /// Code Entry guid
        /// </summary>
        public Guid? E { get; set; }
        /// <summary>
        /// DateFound
        /// </summary>
        public DateTime D { get; set; } = _.d;

        /// <summary>
        /// IsAnonymous
        /// </summary>
        public bool? A { get; set; }
        /// <summary>
        /// IsDirty
        /// </summary>
        public bool? Dr { get; set; }
        /// <summary>
        /// LastActivityDate
        /// </summary>
        public DateTime? Da { get; set; }
        /// <summary>
        /// LastUpdatedDate
        /// </summary>
        public DateTime? Du { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string U { get; set; }

    }

    /// <summary>
    /// Simple Http Browser Capabilities Log Data
    /// </summary>
    public class Hbc
    {
        /// <summary>
        /// Current item key guid
        /// </summary>
        public Guid HbcId { get; set; } = _.g;
        public DateTime DateFound { get; set; } = _.d;

        public bool? Win32 { get; set; }
        public bool? Win16 { get; set; }
        public string W3CDomVersion { get; set; }
        public string Version { get; set; }
        public bool? VBScript { get; set; }
        public bool? UseOptimizedCacheKey { get; set; }
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
        /// <summary>
        /// Id
        /// </summary>
        public string IdVal { get; set; }
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
        public bool? CanRenderEmptySelects { get; set; }
        public bool? CanRenderAfterInputOrSelectElement { get; set; }
        public bool? CanInitiateVoiceCall { get; set; }
        public bool? CanCombineFormsInDeck { get; set; }
        public string Browser { get; set; }
        public bool? Beta { get; set; }
        public bool? BackgroundSounds { get; set; }
        public bool? AOL { get; set; }
        public bool? ActiveXControls { get; set; }

    }

    #endregion



}
