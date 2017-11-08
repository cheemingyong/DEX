// ASP.NET Maker 2017
// Copyright (c) e.World Technology Limited. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static AspNetMaker2017.Models.DEX;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

// Models
namespace AspNetMaker2017.Models {
	public partial class DEX {

		// Debug
		public static bool EW_DEBUG_ENABLED { get; set; } = false; // Set to true for debugging

		// Project
		public const string EW_PROJECT_CLASSNAME = "AspNetMaker2017.Models.DEX"; // DN
		public static string EW_PATH_DELIMITER = Convert.ToString(Path.DirectorySeparatorChar); // Physical path delimiter // DN
		public const short EW_UNFORMAT_YEAR = 50; // Unformat year
		public const string EW_PROJECT_NAME = "DEX"; // Project name
		public static string EW_AREA_NAME { get; set; } = ""; // Area name // DN
		public static string EW_CONTROLLER_NAME { get; set; } = "Home"; // Controller name // DN
		public const string EW_CONFIG_FILE_FOLDER = EW_PROJECT_NAME; // Config file name
		public const string EW_PROJECT_ID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}"; // Project ID (GUID)
		public static string EW_RELATED_PROJECT_ID = "";
		public static string EW_RELATED_LANGUAGE_FOLDER = "";
		public static string EW_RANDOM_KEY = "5w76p7Y9OTngSylI"; // Random key for encryption
		public const string EW_PROJECT_STYLESHEET_FILENAME = "css/DEX.css"; // Project stylesheet file name (relative to wwwroot)
		public const string EW_CHARSET = "utf-8"; // Project charset
		public const string EW_EMAIL_CHARSET = EW_CHARSET; // Email charset
		public const string EW_EMAIL_KEYWORD_SEPARATOR = ""; // Email keyword separator
		public static string EW_COMPOSITE_KEY_SEPARATOR = ","; // Composite key separator
		public static bool EW_HIGHLIGHT_COMPARE { get; set; } = true; // Case-insensitive
		public static bool EW_USE_DOM_XML { get; set; } = false;
		public const int EW_FONT_SIZE = 10;
		public const string EW_TMP_IMAGE_FONT = "Verdana"; // Font for temp files
		public static bool EW_CACHE = false; // Cache // DN

		// Database connection info
		public static Dictionary<string, Dictionary<string, string>> EW_DB = new Dictionary<string, Dictionary<string, string>>() {
			{"DB", new Dictionary<string, string>() {{"id", "DB"}, {"type", "MSSQL"}, {"connectionstring", "Persist Security Info=False;Data Source=WIN-R44025I2O79,1433;Initial Catalog=DEX;User Id=sa;Password=Fir3wall;MultipleActiveResultSets=True"}, {"schema", ""}, {"qs", "["}, {"qe", "]"}}}
		}; // DN
		/**
		 * Password (MD5 and case-sensitivity)
		 * Note: If you enable MD5 password, make sure that the passwords in your
		 * user table are stored as MD5 hash (32-character hexadecimal number) of the
		 * clear text password. If you also use case-insensitive password, convert the
		 * clear text passwords to lower case first before calculating MD5 hash.
		 * Otherwise, existing users will not be able to login. MD5 hash is
		 * irreversible, password will be reset during password recovery.
		 */
		public static bool EW_ENCRYPTED_PASSWORD { get; set; } = false; // Encrypted password
		public static bool EW_CASE_SENSITIVE_PASSWORD { get; set; } = false; // Case Sensitive password
		/**
		 * Remove XSS
		 * Note: If you want to allow these keywords, remove them from the following EW_XSS_ARRAY at your own risks.
		*/
		public static bool EW_REMOVE_XSS { get; set; } = true;
		public static string[] EW_REMOVE_XSS_KEYWORDS = new[] {"javascript", "vbscript", "expression", "<applet", "<meta", "<xml", "<blink", "<link", "<style", "<script", "<embed", "<object", "<iframe", "<frame", "<frameset", "<ilayer", "<layer", "<bgsound", "<title", "<base", "onabort", "onactivate", "onafterprint", "onafterupdate", "onbeforeactivate", "onbeforecopy", "onbeforecut", "onbeforedeactivate", "onbeforeeditfocus", "onbeforepaste", "onbeforeprint", "onbeforeunload", "onbeforeupdate", "onblur", "onbounce", "oncellchange", "onchange", "onclick", "oncontextmenu", "oncontrolselect", "oncopy", "oncut", "ondataavailable", "ondatasetchanged", "ondatasetcomplete", "ondblclick", "ondeactivate", "ondrag", "ondragend", "ondragenter", "ondragleave", "ondragover", "ondragstart", "ondrop", "onerror", "onerrorupdate", "onfilterchange", "onfinish", "onfocus", "onfocusin", "onfocusout", "onhelp", "onkeydown", "onkeypress", "onkeyup", "onlayoutcomplete", "onload", "onlosecapture", "onmousedown", "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover", "onmouseup", "onmousewheel", "onmove", "onmoveend", "onmovestart", "onpaste", "onpropertychange", "onreadystatechange", "onreset", "onresize", "onresizeend", "onresizestart", "onrowenter", "onrowexit", "onrowsdelete", "onrowsinserted", "onscroll", "onselect", "onselectionchange", "onselectstart", "onstart", "onstop", "onsubmit", "onunload"};

		// Check Token
		public static bool EW_CHECK_TOKEN = true; // Check post token // DN

		// Session timeout time
		public static int EW_SESSION_TIMEOUT = 20; // Session timeout time (minutes)

		// Session keep alive interval
		public static int EW_SESSION_KEEP_ALIVE_INTERVAL = 0; // Session keep alive interval (seconds)
		public static int EW_SESSION_TIMEOUT_COUNTDOWN = 60; // Session timeout count down interval (seconds)

		// Session names
		public const string EW_SESSION_STATUS = EW_PROJECT_NAME + "_Status"; // Login status
		public const string EW_SESSION_USER_NAME = EW_SESSION_STATUS + "_UserName";	// User name
		public const string EW_SESSION_USER_LOGIN_TYPE = EW_SESSION_STATUS + "_UserLoginType"; // User login type
		public const string EW_SESSION_USER_ID = EW_SESSION_STATUS + "_UserID";	// User ID
		public const string EW_SESSION_USER_PROFILE = EW_SESSION_STATUS + "_UserProfile"; // User Profile
		public const string EW_SESSION_USER_PROFILE_USER_NAME = EW_SESSION_USER_PROFILE + "_UserName";
		public const string EW_SESSION_USER_PROFILE_PASSWORD = EW_SESSION_USER_PROFILE + "_Password";
		public const string EW_SESSION_USER_PROFILE_LOGIN_TYPE = EW_SESSION_USER_PROFILE + "_LoginType";
		public const string EW_SESSION_USER_LEVEL_ID = EW_SESSION_STATUS + "_UserLevel"; // User level ID
		public const string EW_SESSION_USER_LEVEL_LIST = EW_SESSION_STATUS + "_UserLevelList"; // User Level List
		public const string EW_SESSION_USER_LEVEL_LIST_LOADED = EW_SESSION_STATUS + "_UserLevelListLoaded"; // User Level List Loaded
		public const string EW_SESSION_USER_LEVEL = EW_SESSION_STATUS + "_UserLevelValue"; // User level
		public const string EW_SESSION_PARENT_USER_ID = EW_SESSION_STATUS + "_ParentUserID"; // Parent user ID
		public const string EW_SESSION_SYS_ADMIN = EW_PROJECT_NAME + "_SysAdmin"; // System admin
		public const string EW_SESSION_PROJECT_ID = EW_PROJECT_NAME + "_ProjectID"; // User Level project ID
		public const string EW_SESSION_AR_USER_LEVEL = EW_PROJECT_NAME + "_arUserLevel"; // User level List
		public const string EW_SESSION_AR_USER_LEVEL_PRIV = EW_PROJECT_NAME + "_arUserLevelPriv"; // User level privilege List
		public const string EW_SESSION_USER_LEVEL_MSG = EW_PROJECT_NAME + "_UserLevelMessage"; // User Level messsage
		public const string EW_SESSION_MESSAGE = EW_PROJECT_NAME + "_Message"; // System message
		public const string EW_SESSION_FAILURE_MESSAGE = EW_PROJECT_NAME + "_Failure_Message"; // System error message
		public const string EW_SESSION_SUCCESS_MESSAGE = EW_PROJECT_NAME + "_Success_Message"; // System message
		public const string EW_SESSION_WARNING_MESSAGE = EW_PROJECT_NAME + "_Warning_Message"; // Warning message
		public const string EW_SESSION_INLINE_MODE = EW_PROJECT_NAME + "_InlineMode"; // Inline mode
		public const string EW_SESSION_BREADCRUMB = EW_PROJECT_NAME + "_Breadcrumb"; // Breadcrumb
		public const string EW_SESSION_TEMP_IMAGES = EW_PROJECT_NAME + "_TempImages"; // Temp images

		// Language settings
		public const string EW_LANGUAGE_FOLDER = "lang/";
		public static List<string[]> EW_LANGUAGE_FILE = new List<string[]>() {
			new[] {"en", "", "english.xml"}
		};
		public const string EW_LANGUAGE_DEFAULT_ID = "en";
		public const string EW_SESSION_LANGUAGE_ID = EW_PROJECT_NAME + "_LanguageId"; // Language ID
		public const string EW_LOCALE_FOLDER = "locale/";

		// Page Token
		public const string EW_TOKEN_NAME = "token"; // DO NOT CHANGE!
		public const string EW_SESSION_TOKEN = EW_PROJECT_NAME + "_Token";

		// Data types
		public const short EW_DATATYPE_NUMBER = 1;
		public const short EW_DATATYPE_DATE = 2;
		public const short EW_DATATYPE_STRING = 3;
		public const short EW_DATATYPE_BOOLEAN = 4;
		public const short EW_DATATYPE_MEMO = 5;
		public const short EW_DATATYPE_BLOB = 6;
		public const short EW_DATATYPE_TIME = 7;
		public const short EW_DATATYPE_GUID = 8;
		public const short EW_DATATYPE_XML = 9;
		public const short EW_DATATYPE_OTHER = 10;

		// Row types
		public const short EW_ROWTYPE_HEADER = 0;	// Row type view
		public const short EW_ROWTYPE_VIEW = 1;	// Row type view
		public const short EW_ROWTYPE_ADD = 2; // Row type add
		public const short EW_ROWTYPE_EDIT = 3; // Row type edit
		public const short EW_ROWTYPE_SEARCH = 4; // Row type search
		public const short EW_ROWTYPE_MASTER = 5; // Row type master record
		public const short EW_ROWTYPE_AGGREGATEINIT = 6; // Row type aggregate init
		public const short EW_ROWTYPE_AGGREGATE = 7; // Row type aggregate

		// List actions
		public const string EW_ACTION_POSTBACK = "P"; // Post back
		public const string EW_ACTION_AJAX = "A"; // Ajax
		public const string EW_ACTION_MULTIPLE = "M"; // Multiple records
		public const string EW_ACTION_SINGLE = "S"; // Single record

		// Table parameters
		public const string EW_TABLE_PREFIX = "||ASPNETReportMaker||"; // For backward compatibilty only
		public const string EW_TABLE_REC_PER_PAGE = "recperpage"; // Records per page
		public const string EW_TABLE_START_REC = "start"; // Start record
		public const string EW_TABLE_PAGE_NO = "pageno"; // Page number
		public const string EW_TABLE_BASIC_SEARCH = "psearch"; // Basic search keyword
		public const string EW_TABLE_BASIC_SEARCH_TYPE = "psearchtype"; // Basic search type
		public const string EW_TABLE_ADVANCED_SEARCH = "advsrch"; // Advanced search
		public const string EW_TABLE_SEARCH_WHERE = "searchwhere"; // Search where clause
		public const string EW_TABLE_WHERE = "where"; // Table where
		public const string EW_TABLE_WHERE_LIST = "where_list"; // Table where (list page)
		public const string EW_TABLE_ORDER_BY = "orderby"; // Table order by
		public const string EW_TABLE_ORDER_BY_LIST = "orderby_list"; // Table order by (list page)
		public const string EW_TABLE_SORT = "sort"; // Table sort
		public const string EW_TABLE_KEY = "key"; // Table key
		public const string EW_TABLE_SHOW_MASTER = "showmaster"; // Table show master
		public const string EW_TABLE_SHOW_DETAIL = "showdetail"; // Table show detail
		public const string EW_TABLE_MASTER_TABLE = "mastertable"; // Master table
		public const string EW_TABLE_DETAIL_TABLE = "detailtable"; // Detail table
		public const string EW_TABLE_RETURN_URL = "return"; // Return URL
		public const string EW_TABLE_EXPORT_RETURN_URL = "exportreturn"; // Export return URL
		public const string EW_TABLE_GRID_ADD_ROW_COUNT = "gridaddcnt"; // Grid add row count

		// Audit Trail
		public static bool EW_AUDIT_TRAIL_TO_DATABASE { get; set; } = false; // Write audit trail to DB
		public const string EW_AUDIT_TRAIL_DBID = "DB"; // Audit trail DBID
		public const string EW_AUDIT_TRAIL_TABLE_NAME = ""; // Audit trail table name
		public const string EW_AUDIT_TRAIL_TABLE_VAR = ""; // Audit trail table var
		public const string EW_AUDIT_TRAIL_FIELD_NAME_DATETIME = ""; // Audit trail DateTime field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_SCRIPT = ""; // Audit trail Script field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_USER = ""; // Audit trail User field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_ACTION = ""; // Audit trail Action field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_TABLE = ""; // Audit trail Table field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_FIELD = ""; // Audit trail Field field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_KEYVALUE = ""; // Audit trail Key Value field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_OLDVALUE = ""; // Audit trail Old Value field name
		public const string EW_AUDIT_TRAIL_FIELD_NAME_NEWVALUE = ""; // Audit trail New Value field name

		// Security
		public const string EW_ADMIN_USER_NAME = ""; // Administrator user name
		public const string EW_ADMIN_PASSWORD = ""; // Administrator password
		public static bool EW_USE_CUSTOM_LOGIN { get; set; } = true; // Use custom login
		public static bool EW_ALLOW_LOGIN_BY_URL { get; set; } = false; // Allow login by URL
		public static bool EW_ALLOW_LOGIN_BY_SESSION { get; set; } = false; // Allow login by session variables

		// User level constants
		public static bool EW_USER_LEVEL_COMPAT { get; set; } = false; // Use old user level values
		public const short EW_ALLOW_ADD = 1; // Add
		public const short EW_ALLOW_DELETE = 2; // Delete
		public const short EW_ALLOW_EDIT = 4; // Edit
		public const short EW_ALLOW_LIST = 8; // List
		public const int EW_ALLOW_VIEW = 32; // View (for EW_USER_LEVEL_COMPAT = False)
		public const int EW_ALLOW_SEARCH = 64; // Search (for EW_USER_LEVEL_COMPAT = False)
		public const short EW_ALLOW_REPORT = 8; // Report
		public const short EW_ALLOW_ADMIN = 16; // Admin

		// Hierarchical User ID
		public static bool EW_USER_ID_IS_HIERARCHICAL { get; set; } = true; // True to show all level / False to show 1 level

		// Use subquery for master/detail
		public static bool EW_USE_SUBQUERY_FOR_MASTER_USER_ID { get; set; } = false; // True to use subquery / False to skip
		public const int EW_USER_ID_ALLOW = 104;

		// User table filters
		// User Profile Constants

		public const string EW_USER_PROFILE_SESSION_ID = "SessionID";
		public const string EW_USER_PROFILE_LAST_ACCESSED_DATE_TIME = "LastAccessedDateTime";
		public const int EW_USER_PROFILE_CONCURRENT_SESSION_COUNT = 1; // Maximum sessions allowed
		public const int EW_USER_PROFILE_SESSION_TIMEOUT = 20;
		public const string EW_USER_PROFILE_LOGIN_RETRY_COUNT = "LoginRetryCount";
		public const string EW_USER_PROFILE_LAST_BAD_LOGIN_DATE_TIME = "LastBadLoginDateTime";
		public const int EW_USER_PROFILE_MAX_RETRY = 3;
		public const int EW_USER_PROFILE_RETRY_LOCKOUT = 20;
		public const string EW_USER_PROFILE_LAST_PASSWORD_CHANGED_DATE = "LastPasswordChangedDate";
		public const int EW_USER_PROFILE_PASSWORD_EXPIRE = 90;
		public const string EW_USER_PROFILE_LANGUAGE_ID = "LanguageId";
		public const string EW_USER_PROFILE_SEARCH_FILTERS = "SearchFilters";
		public const string EW_SEARCH_FILTER_OPTION = "Client";

		// Auto hide pager
		public const bool EW_AUTO_HIDE_PAGER = true;
		public const bool EW_AUTO_HIDE_PAGE_SIZE_SELECTOR = false;

		// Email
		public const string EW_SMTP_SERVER = "localhost"; // SMTP server
		public const int EW_SMTP_SERVER_PORT = 25; // SMTP server port
		public const string EW_SMTP_SECURE_OPTION = "";
		public const string EW_SMTP_SERVER_USERNAME = ""; // SMTP server user name
		public const string EW_SMTP_SERVER_PASSWORD = ""; // SMTP server password
		public const string EW_SENDER_EMAIL = ""; // Sender email
		public const string EW_RECIPIENT_EMAIL = ""; // Recipient email
		public const int EW_MAX_EMAIL_RECIPIENT = 3;
		public const int EW_MAX_EMAIL_SENT_COUNT = 3;
		public const string EW_EXPORT_EMAIL_COUNTER = EW_SESSION_STATUS + "_EmailCounter";
		public const string EW_EMAIL_CHANGEPWD_TEMPLATE = "changepwd.html";
		public const string EW_EMAIL_FORGOTPWD_TEMPLATE = "forgotpwd.html";
		public const string EW_EMAIL_NOTIFY_TEMPLATE = "notify.html";
		public const string EW_EMAIL_REGISTER_TEMPLATE = "register.html";
		public const string EW_EMAIL_RESETPWD_TEMPLATE = "resetpwd.html";
		public const string EW_EMAIL_TEMPLATE_PATH = "html"; // Template path // DN

		// File handler // DN
		public const string EW_FILE_URL = "ewfile";

		// File upload
		public const string EW_UPLOAD_TEMP_PATH = ""; // Upload temp path (absolute)
		public const string EW_UPLOAD_DEST_PATH = "uploads/"; // Upload destination path
		public const string EW_UPLOAD_URL = "ewupload"; // Upload URL
		public const string EW_UPLOAD_TEMP_FOLDER_PREFIX = "temp__"; // Upload temp folders prefix
		public const int EW_UPLOAD_TEMP_FOLDER_TIME_LIMIT = 1440; // Upload temp folder time limit (minutes)
		public const string EW_UPLOAD_THUMBNAIL_FOLDER = "thumbnail"; // Temporary thumbnail folder
		public const int EW_UPLOAD_THUMBNAIL_WIDTH = 200; // Temporary thumbnail max width
		public const int EW_UPLOAD_THUMBNAIL_HEIGHT = 0; // Temporary thumbnail max height
		public const int EW_MAX_FILE_COUNT = 0; // Max file count
		public const string EW_UPLOAD_ALLOWED_FILE_EXT = "gif,jpg,jpeg,bmp,png,doc,docx,xls,xlsx,pdf,zip"; // Allowed file extensions
		public static List<string> EW_IMAGE_ALLOWED_FILE_EXT = new List<string>() {"gif","jpg","png","bmp"}; // Allowed file extensions for images
		public static List<string> EW_DOWNLOAD_ALLOWED_FILE_EXT = new List<string>() {"pdf","xls","doc","xlsx","docx"}; // Allowed file extensions for download (non-image)
		public const bool EW_ENCRYPT_FILE_PATH = true; // Encrypt file path
		public const int EW_MAX_FILE_SIZE = 2000000; // Max file size
		public const short EW_THUMBNAIL_DEFAULT_WIDTH = 0; // Thumbnail default width
		public const short EW_THUMBNAIL_DEFAULT_HEIGHT = 0; // Thumbnail default height
		public static bool EW_UPLOAD_CONVERT_ACCENTED_CHARS { get; set; } = false; // Convert accented chars in upload file name
		public static bool EW_USE_COLORBOX { get; set; } = true; // Use Colorbox
		public const char EW_MULTIPLE_UPLOAD_SEPARATOR = ','; // Multiple upload separator

		// Image resize
		public static bool EW_RESIZE_PRESERVE_ASPECT_RATIO { get; set; } = true;
		public static bool EW_RESIZE_PREVENT_ENLARGE { get; set; } = false;
		public static bool EW_RESIZE_CONVERT_GIF_TO_PNG { get; set; } = true;

		// Audit trail
		public const string EW_AUDIT_TRAIL_PATH = ""; // Audit trail path (relative to wwwroot)

		// Export records
		public const bool EW_EXPORT_ALL = true; // Export all records
		public static bool EW_EXPORT_ORIGINAL_VALUE { get; set; } = false; // True to export original value
		public static bool EW_EXPORT_FIELD_CAPTION { get; set; } = false; // True to export field caption
		public static bool EW_EXPORT_CSS_STYLES { get; set; } = true; // True to export css styles
		public static bool EW_EXPORT_MASTER_RECORD { get; set; } = true; // True to export master record
		public static bool EW_EXPORT_MASTER_RECORD_FOR_CSV { get; set; } = false; // True to export master record for CSV
		public static bool EW_EXPORT_DETAIL_RECORDS { get; set; } = true; // True to export detail records
		public static bool EW_EXPORT_DETAIL_RECORDS_FOR_CSV { get; set; } = false; // True to export detail records for CSV

		// Export classes
		public static Dictionary<string, string> EW_EXPORT = new Dictionary<string, string>() {
			{"email", "cExportEmail"},
			{"html", "cExportHtml"},
			{"word", "cExportWord"},
			{"excel", "cExportExcel"},
			{"pdf", "cExportPdf"},
			{"csv", "cExportCsv"},
			{"xml", "cExportXml"}
		};
		public static Dictionary<string, string> EW_EXPORT_REPORT = new Dictionary<string, string>() {
			{"print", "ExportReportHtml"},
			{"html", "ExportReportHtml"},
			{"word", "ExportReportWord"},
			{"excel", "ExportReportExcel"}
		};

		// Boolean html attributes
		public static List<string> EW_BOOLEAN_HTML_ATTRIBUTES = new List<string>() {"checked", "compact", "declare", "defer", "disabled", "ismap", "multiple", "nohref", "noresize", "noshade", "nowrap", "readonly", "selected"};

		// Use ILIKE for PostgreSql
		public static bool EW_USE_ILIKE_FOR_POSTGRESQL { get; set; } = true;

		// Use collation for MySQL
		public const string EW_LIKE_COLLATION_FOR_MYSQL = "";

		// Use collation for MsSQL
		public const string EW_LIKE_COLLATION_FOR_MSSQL = "";

		// Null / Not Null values
		public const string EW_NULL_VALUE = "##null##";
		public const string EW_NOT_NULL_VALUE = "##notnull##";
		/**
		 * Search multi value option
		 * 1 - no multi value
		 * 2 - AND all multi values
		 * 3 - OR all multi values
		*/
		public static short EW_SEARCH_MULTI_VALUE_OPTION { get; set; } = 3;

		// Quick search
		public static string EW_BASIC_SEARCH_IGNORE_PATTERN = @"[\?,\^\*\(\)\[\]\""]"; // Ignore special characters
		public static bool EW_BASIC_SEARCH_ANY_FIELDS { get; set; } = false; // Search "All keywords" in any selected fields

		// Validate option
		public static bool EW_CLIENT_VALIDATE { get; set; } = true;
		public static bool EW_SERVER_VALIDATE { get; set; } = true;

		// Blob field byte count for hash value calculation
		public static int EW_BLOB_FIELD_BYTE_COUNT { get; set; } = 200;

		// Auto suggest max entries
		public const int EW_AUTO_SUGGEST_MAX_ENTRIES = 10;

		// Auto fill original value
		public const bool EW_AUTO_FILL_ORIGINAL_VALUE = false;

		// Lookup filter value separator
		public const char EW_LOOKUP_FILTER_VALUE_SEPARATOR = ',';

		// Checkbox and radio button groups
		public const string EW_ITEM_TEMPLATE_CLASSNAME = "ewTemplate";
		public const string EW_ITEM_TABLE_CLASSNAME = "ewItemTable";

		// Page Title Style
		public const string EW_PAGE_TITLE_STYLE = "Breadcrumb";

		// Use responsive layout
		public static bool EW_USE_RESPONSIVE_LAYOUT { get; set; } = true;

		// Use css-flip
		public static bool EW_CSS_FLIP { get; set; } = false;
		public static List<string> EW_RTL_LANGUAGES = new List<string>() { "ar", "fa", "he", "iw", "ug", "ur" };

		// Cookies
		public static DateTime EW_COOKIE_EXPIRY_TIME = DateTime.Today.AddDays(365);

		// Client variables
		public static Dictionary<string, object> EW_CLIENT_VAR = new Dictionary<string, object>();

		// Menu
		public const string EW_MENUBAR_ID = "RootMenu";
		public static string EW_MENUBAR_BRAND { get; set; } = "";
		public const string EW_MENUBAR_BRAND_HYPERLINK = "";
		public const string EW_MENUBAR_CLASSNAME = "";
		public const string EW_MENU_CLASSNAME = "dropdown-menu";
		public const string EW_SUBMENU_CLASSNAME = "dropdown-menu";
		public const string EW_SUBMENU_DROPDOWN_IMAGE = "";
		public const string EW_SUBMENU_DROPDOWN_ICON_CLASSNAME = "";
		public const string EW_MENU_DIVIDER_CLASSNAME = "divider";
		public const string EW_MENU_ITEM_CLASSNAME = "dropdown-submenu";
		public const string EW_SUBMENU_ITEM_CLASSNAME = "dropdown-submenu";
		public const string EW_MENU_ACTIVE_ITEM_CLASS = "active";
		public const string EW_SUBMENU_ACTIVE_ITEM_CLASS = "active";
		public const bool EW_MENU_ROOT_GROUP_TITLE_AS_SUBMENU = false;
		public const bool EW_SHOW_RIGHT_MENU = false;
	public const string EW_PDF_STYLESHEET_FILENAME = "css/ewpdf.css"; // Export PDF CSS styles
	}
}
