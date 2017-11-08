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

		// Current HttpContext
		public static HttpContext ew_HttpContext {
			get { return ew_HttpContextAccessor.HttpContext; }
		}

		// Hosting environment
		public static IHostingEnvironment ew_Env;

		// HttpContext accessor
		private static IHttpContextAccessor ew_HttpContextAccessor;

		// Configure HttpContext accessor and Hosting environment
		public static void Configure(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env) {
			ew_HttpContextAccessor = httpContextAccessor;
			ew_Env = env;
			ew_Session = new cSession();
			ew_Cookie = new cCookie();
		}

		// Web root path
		public static string ew_WebRootPath {
			get {
				return ew_Env?.WebRootPath;
			}
		}

		// Is development
		public static bool IsDevelopment() {
			return ew_Env.IsDevelopment();
		}

		// Current view (RazorPage)
		public static RazorPage ew_View {
			get {
				return (RazorPage)ew_HttpContext.Items["__View"];
			}
			set {
				ew_HttpContext.Items["__View"] = value;
			}
		}

		// Current controller
		public static Controller ew_Controller {
			get {
				return (Controller)ew_HttpContext.Items["__Controller"];
			}
			set {
				ew_HttpContext.Items["__Controller"] = value;
			}
		}

		// IAspNetMakerPage interface // DN
		public interface IAspNetMakerPage
		{
			IActionResult Page_Init();
			IActionResult Page_Main();
			IActionResult Page_Terminate(string url = "");
		}

		// Page data class
		public class cViewData
		{
			private Dictionary<string, object> _data = new Dictionary<string, object>();

			// Indexers
			public object this[string key] {
				get {
					return (_data.ContainsKey(key)) ? _data[key] : null;
				}
				set {
					_data[key] = value;
				}
			}
		}

		// Page data
		public static cViewData ew_ViewData
		{
			get {
				if (!ew_HttpContext.Items.ContainsKey("__ViewData"))
					ew_HttpContext.Items.Add("__ViewData", new cViewData());
				return (cViewData)ew_HttpContext.Items["__ViewData"];
			}
		}

		// Attribute dictionary // DN
		public class cAttributes : Dictionary<string, string>
		{

			// Indexer
			public new string this[string key]
			{
				get {
					return ContainsKey(key) ? base[key] : "";
				}
				set {
					base[key] = value;
				}
			}

			// Append
			public void Append(string key, string value)
			{
				this[key] += value;
			}

			// Prepend
			public void Prepend(string key, string value)
			{
				this[key] = value + this[key];
			}

			// Concat
			public void Concat(string key, string value, string sep)
			{
				this[key] = ew_Concat(this[key], value, sep);
			}

			// Append class
			public void AppendClass(string value)
			{
				this["class"] = ew_AppendClass(this["class"], value);
			}

			// Prepend class
			public void PrependClass(string value)
			{
				this["class"] = ew_PrependClass(this["class"], value);
			}

			// Get an attribute value and remove it from dictionary
			public string Extract(string key)
			{
				if (ContainsKey(key)) {
					var val = this[key];
					base.Remove(key);
					return ew_NotEmpty(val) ? val : null; // Returns null if empty
				}
				return null; // Returns null if key does not exist
			}
		}

		// HttpContext.User
		public static ClaimsPrincipal ew_User
		{
			get
			{
				return ew_HttpContext.User;
			}
		}

		// Is authenticated
		public static bool IsAuthenticated()
		{
			return ew_User.Identity.IsAuthenticated;
		}

		// Request
		public static HttpRequest ew_Request
		{
			get {
				return ew_HttpContext.Request;
			}
		}

		// Response
		public static HttpResponse ew_Response
		{
			get {
				return ew_HttpContext.Response;
			}
		}

		// Form
		public static IFormCollection ew_Form
		{
			get {
				return ew_Request.HasFormContentType ? ew_Request.Form : FormCollection.Empty;
			}
		}

		// QueryString
		public static IQueryCollection ew_QueryString
		{
			get {
				return ew_Request.Query;
			}
		}

		// Files
		public static IFormFileCollection ew_Files
		{
			get {
				return ew_Form.Files;
			}
		}

		// IsPost
		public static bool IsPost {
			get {
				return ew_SameText(ew_Request.Method, "POST");
			}
		}

		// Compare object as string
		public static bool ew_SameStr(object v1, object v2)
		{
			if (v1 is DateTime || v2 is DateTime) {
				return ew_SameDate(v1, v2);
			} else {
				return string.Equals(Convert.ToString(v1).Trim(), Convert.ToString(v2).Trim());
			}
		}

		// Compare object as string (case insensitive)
		public static bool ew_SameText(object v1, object v2)
		{
			if (v1 is DateTime || v2 is DateTime) {
				return ew_SameDate(v1, v2);
			} else {
				return string.Equals(Convert.ToString(v1).Trim().ToUpperInvariant(), Convert.ToString(v2).Trim().ToUpperInvariant());
			}
		}

		// Compare object as DateTime
		public static bool ew_SameDate(object v1, object v2)
		{
			try {
				return (Convert.ToDateTime(v1) == Convert.ToDateTime(v2));
			} catch	{
				return false;
			}
		}

		// Compare object as integer
		public static bool ew_SameInt(object v1, object v2)
		{
			try {
				return (Convert.ToInt32(v1) == Convert.ToInt32(v2));
			} catch {
				return false;
			}
		}

		// Compare file name without extension // DN
		public static bool ew_SameFileName(string v1, string v2)
		{
			return string.Equals(Path.GetFileNameWithoutExtension(v1), Path.GetFileNameWithoutExtension(v2));
		}

		// Check if empty string/collection // DN
		public static bool ew_Empty(object value)
		{
			return string.Equals(Convert.ToString(value).Trim(), string.Empty);
		}

		// Check if not empty string/collection // DN
		public static bool ew_NotEmpty(object value)
		{
			return !ew_Empty(value);
		}

		// Convert object to integer
		public static int ew_ConvertToInt(object value)
		{
			try {
				return Convert.ToInt32(value);
			} catch {
				return 0;
			}
		}

		// Convert object to integer
		public static long ew_ConvertToInt64(object value)
		{
			try {
				return Convert.ToInt64(value);
			} catch {
				return 0;
			}
		}

		// Convert object to double
		public static double ew_ConvertToDouble(object value)
		{
			try {
				return Convert.ToDouble(value);
			} catch {
				return 0;
			}
		}
		public static DateTime ew_ConvertToDateTime(object value)
 		{
 			try {
 				if (value is TimeSpan)
 					return DateTime.Parse(Convert.ToString(value));
 				else
 					return Convert.ToDateTime(value);
 			} catch {
				return DateTime.MinValue;
			}
 		}

		// Convert object to bool
		public static bool ew_ConvertToBool(object value)
		{
			try {
				if (ew_IsNumeric(value)) {
					return ew_ConvertToInt(value) != 0;
				} else if (value is string) { // ***
					return ew_SameText(value, "y") || ew_SameText(value, "t") || ew_SameText(value, "true");
				} else {
					return Convert.ToBoolean(value);
				}
			} catch {
				return false;
			}
		}

		// Convert input value to boolean value for SQL paramater // DN
		public static object ew_ConvertToBool(object Value, string TrueValue, string FalseValue)
		{
			var res = Value;
			if (!ew_SameStr(Value, TrueValue) && !ew_SameStr(Value, FalseValue))
				res = ew_NotEmpty(Value) ? TrueValue : FalseValue;
			if (ew_IsNumeric(res)) // Convert to int so it can be converted to bool if necessary
				res = ew_ConvertToInt(res);
			return res;
		}

		// Prepend CSS class name // DN
		public static string ew_PrependClass(string attr, string classname) {
			classname = classname.Trim();
			if (ew_NotEmpty(classname)) {
				attr = attr.Trim();
				if (ew_NotEmpty(attr))
					attr = " " + attr;
				attr = classname + attr;
			}
			return attr;
		}

		// Append CSS class name // DN
		public static string ew_AppendClass(string attr, string classname) {
			classname = classname.Trim();
			if (ew_NotEmpty(classname)) {
				attr = attr.Trim();
				if (ew_NotEmpty(attr))
					attr += " ";
				attr += classname;
			}
			return attr;
		}

		// Get numeric formatting information
		public static JObject ew_LocaleConv() {
			string langid = gsLanguage;
			string localefile = langid.ToLower() + ".json";
			if (!ew_FileExists(localefile, ew_ServerMapPath(EW_LOCALE_FOLDER))) // Locale file not found, fall back to English ("en") locale
				localefile = "en.json";
			return (JObject)JsonConvert.DeserializeObject(File.ReadAllText(ew_ServerMapPath(EW_LOCALE_FOLDER + localefile), Encoding.UTF8));
		}

		// Get internal default date format (e.g. "yyyy/mm/dd"") from date format (int)
		// 5 - Ymd (default)
		// 6 - mdY
		// 7 - dmY
		// 9 - YmdHis
		// 10 - mdYHis
		// 11 - dmYHis
		// 12 - ymd
		// 13 - mdy
		// 14 - dmy
		// 15 - ymdHis
		// 16 - mdyHis
		// 17 - dmyHis

		public static string ew_DateFormat(object dateFormat) {
			if (ew_IsNumeric(dateFormat)) {
				int iDateFormat = ew_ConvertToInt(dateFormat);
				switch (iDateFormat) {
					case 5:
					case 9:
						return "yyyy" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "dd";
					case 6:
					case 10:
						return "mm" + EW_DATE_SEPARATOR + "dd" + EW_DATE_SEPARATOR + "yyyy";
					case 7:
					case 11:
						return "dd" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "yyyy";
					case 12:
					case 15:
						return "yy" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "dd";
					case 13:
					case 16:
						return "mm" + EW_DATE_SEPARATOR + "dd" + EW_DATE_SEPARATOR + "yy";
					case 14:
					case 17:
						return "dd" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "yy";
				}
			} else if (ew_NotEmpty(dateFormat.ToString())) {
				string sDateFormat = dateFormat.ToString();
				switch (sDateFormat.Substring(0, 3)) {
					case "Ymd":
						return "yyyy" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "dd";
					case "mdY":
						return "mm" + EW_DATE_SEPARATOR + "dd" + EW_DATE_SEPARATOR + "yyyy";
					case "dmY":
						return "dd" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "yyyy";
					case "ymd":
						return "yy" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "dd";
					case "mdy":
						return "mm" + EW_DATE_SEPARATOR + "dd" + EW_DATE_SEPARATOR + "yy";
					case "dmy":
						return "dd" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "yy";
				}
			}
			return "yyyy" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "dd";
		}

		// Validate locale file date format
		public static int ew_DateFormatId(object dateFormat) {
			if (ew_IsNumeric(dateFormat)) {
				List<int> dateFormatId = new List<int>(new int[] { 5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17 });
				int iDateFormat = ew_ConvertToInt(dateFormat);
				if (dateFormatId.Contains(iDateFormat))
					return iDateFormat;
				else
					return 5;
			} else if (ew_NotEmpty(dateFormat.ToString())) {
				switch (dateFormat.ToString()) {
					case "Ymd":
						return 5;
					case "mdY":
						return 6;
					case "dmY":
						return 7;
					case "YmdHis":
						return 9;
					case "mdYHis":
						return 10;
					case "dmYHis":
						return 11;
					case "ymd":
						return 12;
					case "mdy":
						return 13;
					case "dmy":
						return 14;
					case "ymdHis":
						return 15;
					case "mdyHis":
						return 16;
					case "dmyHis":
						return 17;
				}
			}
			return 5;
		}

		// Add message
		public static void ew_AddMessage(ref string msg, string newmsg) {
			if (ew_NotEmpty(newmsg)) {
				if (ew_NotEmpty(msg))
					msg += "<br><br>";
				msg += newmsg;
			}
		}

		// Add messages by "<br>" and return the combined message // DN
		public static string ew_AddMessage(string msg, string newmsg) {
			if (ew_NotEmpty(newmsg)) {
				if (ew_NotEmpty(msg))
					msg += "<br><br>";
				msg += newmsg;
			}
			return msg;
		}

		// Add filter
		public static void ew_AddFilter(ref string filter, string newfilter) {
			if (ew_Empty(newfilter))
				return;
			if (ew_NotEmpty(filter)) {
				filter = "(" + filter + ") AND (" + newfilter + ")";
			} else {
				filter = newfilter;
			}
		}

		// Add filters by "AND" and return the combined filter
		public static string ew_AddFilter(string filter, string newfilter) {
			if (ew_Empty(newfilter))
				return filter;
			if (ew_NotEmpty(filter)) {
				filter = "(" + filter + ") AND (" + newfilter + ")";
			} else {
				filter = newfilter;
			}
			return filter;
		}

		// Get user IP
		public static string ew_CurrentUserIP()
		{
			var ipaddr = ew_HttpContext.Connection.RemoteIpAddress?.ToString() ?? ew_HttpContext.Connection.LocalIpAddress?.ToString();
			if (ew_Empty(ipaddr) || ipaddr == "::1") { // No remote or local IP address or IPv6 enabled machine, check if localhost
				ipaddr = ew_GetIP4Address(ew_Request.Host.ToString().Split(':')[0]);
				if (ipaddr == "127.0.0.1")
					return ipaddr;
			}
			return ipaddr; // Unknown
		}

		// Is local // DN
		public static bool IsLocal()
		{
			return ew_HttpContext.Connection.LocalIpAddress == ew_HttpContext.Connection.RemoteIpAddress || ew_CurrentUserIP() == "127.0.0.1";
		}

			// Get IPv4 Address
		public static string ew_GetIP4Address(string host)
		{
			string ipaddr = String.Empty;
			try {
				foreach (IPAddress IPA in Dns.GetHostAddresses(host)) {
					if (IPA.AddressFamily.ToString() == "InterNetwork") {
						ipaddr = IPA.ToString();
						break;
					}
				}
			} catch {}
			return ipaddr;
		}

		// Get current date in default date format
		public static string ew_CurrentDate(int namedformat)
		{
			string DT;
			if (ew_Contains(namedformat, new int[] {5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17})) {
				if (ew_Contains(namedformat, new int[] {5, 9, 12, 15})) {
					DT = ew_FormatDateTime(DateTime.Today, 5);
				} else if (ew_Contains(namedformat, new int[] {6, 10, 13, 16})) {
					DT = ew_FormatDateTime(DateTime.Today, 6);
				} else {
					DT = ew_FormatDateTime(DateTime.Today, 7);
				}
				return DT;
			} else {
				return ew_FormatDateTime(DateTime.Today, 5);
			}
		}
		public static string ew_CurrentDate()
		{
			return ew_CurrentDate(-1);
		}

		// Get current time in hh:mm:ss format
		public static string ew_CurrentTime() {
			DateTime DT;
			DT = DateTime.Now;
			return DT.ToString("HH':'mm':'ss");
		}

		// Get current date in default date format with
		// Current time in hh:mm:ss format

		public static string ew_CurrentDateTime(int namedformat) {
			string DT;
			if (ew_Contains(namedformat, new int[] {5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17})) {
				if (ew_Contains(namedformat, new int[] {5, 9, 12, 15})) {
					DT = ew_FormatDateTime(DateTime.Now, 9);
				} else if (ew_Contains(namedformat, new int[] {6, 10, 13, 16})) {
					DT = ew_FormatDateTime(DateTime.Now, 10);
				} else {
					DT = ew_FormatDateTime(DateTime.Now, 11);
				}
				return DT;
			} else {
				return ew_FormatDateTime(DateTime.Now, 9);
			}
		}

		// Get current date in default date format with
		// Current time in hh:mm:ss format

		public static string ew_CurrentDateTime() {
			return ew_CurrentDateTime(-1);
		}

		// Remove XSS
		public static string ew_RemoveXSS(object val)
		{

			// Handle null value
			if (val == null)
				return null;

			// Remove all non-printable characters. CR(0a) and LF(0b) and TAB(9) are allowed
			// This prevents some character re-spacing such as <java\0script>
			// Note that you have to handle splits with \n, \r, and \t later since they *are* allowed in some inputs

			Regex regEx = new Regex("([\\x00-\\x08][\\x0b-\\x0c][\\x0e-\\x20])", RegexOptions.IgnoreCase);

			// Create regular expression.
			val = regEx.Replace(Convert.ToString(val), "");

			// Straight replacements, the user should never need these since they're normal characters
			// This prevents like <IMG SRC=&#X40&#X61&#X76&#X61&#X73&#X63&#X72&#X69&#X70&#X74&#X3A&#X61&#X6C&#X65&#X72&#X74&#X28&#X27&#X58&#X53&#X53&#X27&#X29>

			var search = "abcdefghijklmnopqrstuvwxyz";
			search += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			search += "1234567890!@#$%^&*()";
			search += "~`\";:?+/={}[]-_|'\\";
			for (int i = 0; i <= search.Length - 1; i++) {

				// ;? matches the ;, which is optional
				// 0{0,7} matches any padded zeros, which are optional and go up to 8 chars
				// &#x0040 @ search for the hex values

				regEx = new Regex("(&#[x|X]0{0,8}" + ((int)search[i]).ToString("X") + ";?)"); // Conversion.Hex((int)search[i])

				// With a ;
				val = regEx.Replace(Convert.ToString(val), Convert.ToString(search[i]));

				// &#00064 @ 0{0,7} matches '0' zero to seven times
				regEx = new Regex("(&#0{0,8}" + (int)search[i] + ";?)");

				// With a ;
				val = regEx.Replace(Convert.ToString(val), Convert.ToString(search[i]));
			}

			// Now the only remaining whitespace attacks are \t, \n, and \r
			bool Found = true;
			string val_before, pattern, replacement;

			// Keep replacing as long as the previous round replaced something
			while (Found) {
				val_before = Convert.ToString(val);
				for (int i = 0; i <= EW_REMOVE_XSS_KEYWORDS.GetUpperBound(0); i++) {
					pattern = "";
					for (int j = 0; j <= EW_REMOVE_XSS_KEYWORDS[i].Length - 1; j++) {
						if (j > 0) {
							pattern = pattern + "(";
							pattern = pattern + "(&#[x|X]0{0,8}([9][a][b]);?)?";
							pattern = pattern + "|(&#0{0,8}([9][10][13]);?)?";
							pattern = pattern + ")?";
						}
						pattern = pattern + EW_REMOVE_XSS_KEYWORDS[i][j];
					}
					replacement = EW_REMOVE_XSS_KEYWORDS[i].Substring(0, 2) + "<x>" + EW_REMOVE_XSS_KEYWORDS[i].Substring(2);

					// Add in <> to nerf the tag
					regEx = new Regex(pattern);
					val = regEx.Replace(Convert.ToString(val), replacement);

					// Filter out the hex tags
					if (ew_SameStr(val_before, val)) {

						// No replacements were made, so exit the loop
						Found = false;
					}
				}
			}
			return Convert.ToString(val);
		}

		// Get session timeout time (seconds)
		public static int ew_SessionTimeoutTime() {
			int mlt = 0;
			if (EW_SESSION_TIMEOUT > 0) // User specified timeout time
				mlt = EW_SESSION_TIMEOUT * 60;
			if (mlt <= 0)
				mlt = 1200; // Default (1200s = 20min)
			return mlt - 30; // Add some safety margin
		}

		// Check token
		public static bool ew_CheckToken(string t, int timeout = 0) {
			if (timeout <= 0)
				timeout = ew_SessionTimeoutTime();
			return (DateTime.Now.Ticks - ew_ConvertToInt64(ew_Decrypt(t))) < ew_ConvertToInt64(timeout * Math.Pow(10, 7));
		}

		// Create token
		public static string ew_CreateToken() {
			return ew_Encrypt(DateTime.Now.Ticks);
		}

		// Set client variable
		public void ew_SetClientVar(string name, object value) {
			if (name != "")
				EW_CLIENT_VAR[name] = value;
		}

		// Highlight keywords
		public static string ew_Highlight(string name, object src, string bkw, string bkwtype, string akw = "", string akw2 = "")
		{
			string outstr = "";
			if (ew_NotEmpty(src) && (ew_NotEmpty(bkw) || ew_NotEmpty(akw) || ew_NotEmpty(akw2))) {
				string kw = "", kwstr = "";
				int x = 0, y = 0, xx = 0;
				var srcstr = Convert.ToString(src);
				var yy = srcstr.IndexOf("<", xx);
				if (yy < 0) yy = srcstr.Length;
				while (yy >= 0) {
					if (yy > xx) {
						var wrksrc = srcstr.Substring(xx, yy - xx);
						kwstr = bkw.Trim();
						List<string> kwlist;
						if (kwstr.Length > 0 && bkwtype == "=") { // Check for exact phase
							kwlist = new List<string>() {kwstr}; // Use single array element
						} else {
							kwlist = ew_GetKeywords(kwstr);
						}
						if (ew_NotEmpty(akw))
							kwlist.Add(akw.Trim());
						if (ew_NotEmpty(akw2))
							kwlist.Add(akw2.Trim());
						x = 0;
						ew_GetKeyword(wrksrc, kwlist, x, ref y, ref kw);
						while (y >= 0) {
							outstr += wrksrc.Substring(x, y - x) + "<span class=\"" + name + " ewHighlightSearch\">" + wrksrc.Substring(y, kw.Length) + "</span>";
							x = y + kw.Length;
							ew_GetKeyword(wrksrc, kwlist, x, ref y, ref kw);
						}
						outstr += wrksrc.Substring(x);
						xx += wrksrc.Length;
					}
					if (xx < srcstr.Length) {
						yy = srcstr.IndexOf(">", xx);
						if (yy >= 0) {
							outstr += srcstr.Substring(xx, yy - xx + 1);
							xx = yy + 1;
							yy = srcstr.IndexOf("<", xx);
							if (yy < 0) yy = srcstr.Length;
						} else {
							outstr += srcstr.Substring(xx);
							yy = -1;
						}
					} else {
						yy = -1;
					}
				}
			} else {
				outstr = Convert.ToString(src);
			}
			return outstr;
		}

		// Get keywords from search string as string list
		public static List<string> ew_GetKeywords(string search) {
			List<string> ar = new List<string>();

			// Match quoted keywords (i.e.: "...")
			foreach (Match match in Regex.Matches(search, @"""([^""]*)""", RegexOptions.IgnoreCase)) {
				int p = search.IndexOf(match.Value);
				string str = search.Substring(0, p);
				search = search.Substring(p + match.Value.Length);
				if (ew_NotEmpty(str))
					ar.AddRange(str.Trim().Split(' '));
				ar.Add(match.Groups[1].Value); // Save quoted keyword
			}

			// Match individual keywords
			if (ew_NotEmpty(search))
				ar.AddRange(search.Trim().Split(' '));
			return ar;
		}

		// Get keyword
		public static void ew_GetKeyword(string src, List<string> kwlist, int x, ref int y, ref string kw)
		{
			int wrky, thisy = -1;
			string thiskw = "";
			foreach (var wrkkw in kwlist) {
				if (ew_NotEmpty(wrkkw)) {
					if (EW_HIGHLIGHT_COMPARE) { // Case-insensitive
						wrky = src.IndexOf(wrkkw, x, StringComparison.InvariantCultureIgnoreCase);
					} else {
						wrky = src.IndexOf(wrkkw, x);
					}
					if (wrky > -1) {
						if (thisy == -1) {
							thisy = wrky;
							thiskw = wrkkw;
						} else if (wrky < thisy) {
							thisy = wrky;
							thiskw = wrkkw;
						}
					}
				}
			}
			y = thisy;
			kw = thiskw;
		}

		//
		// Security shortcut functions
		//
		// Get current user name

		public static string CurrentUserName()
		{
			return Security?.CurrentUserName ?? Convert.ToString(ew_Session[EW_SESSION_USER_NAME]);
		}

		// Get current user ID
		public static string CurrentUserID()
		{
			return Security?.CurrentUserID ?? Convert.ToString(ew_Session[EW_SESSION_USER_ID]);
		}

		// Get current parent user ID
		public static string CurrentParentUserID()
		{
			return Security?.CurrentParentUserID ?? Convert.ToString(ew_Session[EW_SESSION_PARENT_USER_ID]);
		}

		// Get current user level
		public static int CurrentUserLevel()
		{
			return Security?.CurrentUserLevelID ?? Convert.ToInt32(ew_Session[EW_SESSION_USER_LEVEL_ID]);
		}

		// Get current user level list
		public static string CurrentUserLevelList()
		{
			return Security?.UserLevelList() ?? Convert.ToString(ew_Session[EW_SESSION_USER_LEVEL_LIST]);
		}

		// Get Current user info
		public static object CurrentUserInfo(string fldname)
		{
			object info = null;
			if (info == null && IsAuthenticated())
				info = ew_User.FindFirst(fldname)?.Value;
			return info;
		}

		// Get current page ID
		public static string CurrentPageID()
		{
			return CurrentPage?.PageID ?? "";
		}

		// Check if user password expired
		public static bool IsPasswordExpired()
		{
			return ew_SameStr(ew_Session[EW_SESSION_STATUS], "passwordexpired");
		}

		// Check if user password reset
		public static bool IsPasswordReset()
		{
			return ew_SameStr(ew_Session[EW_SESSION_STATUS], "passwordreset");
		}

		// Set session password expired
		public void SetSessionPasswordExpired() {
			ew_Session[EW_SESSION_STATUS] = "passwordexpired";
		}

		// Check if user is logging in (after changing password)
		public static bool IsLoggingIn()
		{
			return ew_SameStr(ew_Session[EW_SESSION_STATUS], "loggingin");
		}

		// Is Logged In
		public static bool IsLoggedIn()
		{
			return ew_SameStr(ew_Session[EW_SESSION_STATUS], "login");
		}

		// Is auto login (login with option "Auto login until I logout explicitly")
		public static bool IsAutoLogin()
		{
			return ew_SameStr(ew_Session[EW_SESSION_USER_LOGIN_TYPE], "a");
		}

		// Is System Admin
		public static bool IsSysAdmin()
		{
			return ew_ConvertToInt(ew_Session[EW_SESSION_SYS_ADMIN]) == 1;
		}

		// Is Admin
		public static bool IsAdmin()
		{
			return Security?.IsAdmin ?? IsSysAdmin();
		}

		// Current master table object
		public static dynamic CurrentMasterTable {
			get {
				string MasterTableName = CurrentPage?.CurrentMasterTable;
				if (ew_NotEmpty(MasterTableName) && ew_ViewData[MasterTableName] != null)
					return (dynamic)ew_ViewData[MasterTableName];
				return null;
			}
		}

		// Get current language ID
		public static string CurrentLanguageID {
			get {
				return gsLanguage;
			}
		}

		// Get current project ID
		public static string CurrentProjectID {
			get {
				return CurrentPage?.ProjectID ?? EW_PROJECT_ID;
			}
		}

		// Encrypt
		public static string ew_Encrypt(object Data, string Key)
		{
			if (ew_Empty(Data))
				return "";
			byte[] Results = {};
			try {
				System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
				MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
				byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Key));
				TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
				TDESAlgorithm.Key = TDESKey;
				TDESAlgorithm.Mode = CipherMode.ECB;
				TDESAlgorithm.Padding = PaddingMode.PKCS7;
				try {
					byte[] DataToEncrypt = UTF8.GetBytes(Convert.ToString(Data));
					ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
					Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
				} finally {
					TDESAlgorithm.Clear();
					HashProvider.Clear();
				}
			} catch {}
			return Convert.ToBase64String(Results).Replace("+", "_2B").Replace("/", "_2F").Replace("=", "_2E");
		}

		// Encrypt
		public static string ew_Encrypt(object Data)
		{
			return ew_Encrypt(Data, EW_RANDOM_KEY);
		}

		// Decrypt
		public static string ew_Decrypt(object Data, string Key)
		{
			if (ew_Empty(Data))
				return "";
			byte[] Results = {};
			System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
			try {
				MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
				byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Key));
				TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
				TDESAlgorithm.Key = TDESKey;
				TDESAlgorithm.Mode = CipherMode.ECB;
				TDESAlgorithm.Padding = PaddingMode.PKCS7;
				string sData = Convert.ToString(Data).Replace("_2B", "+").Replace("_2F", "/").Replace("_2E", "=");
				try {
					byte[] DataToDecrypt = Convert.FromBase64String(sData);
					ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
					Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
				} finally {
					TDESAlgorithm.Clear();
					HashProvider.Clear();
				}
			} catch {}
			return UTF8.GetString(Results);
		}

		// Decrypt
		public static string ew_Decrypt(object Data)
		{
			return ew_Decrypt(Data, EW_RANDOM_KEY);
		}

		// Save binary to file
		public static bool ew_SaveFile(string folder, string fn, ref byte[] filedata)
		{
			if (ew_CreateFolder(folder)) {
				try {
					File.WriteAllBytes(ew_IncludeTrailingDelimiter(folder, true) + fn, filedata);
					return true;
				} catch {
					if (EW_DEBUG_ENABLED) throw;
					return false;
				}
			}
			return false;
		}

		// Read global debug message
		public static string ew_DebugMsg() {
			var msg = Regex.Replace(gsDebugMsg, @"^(<br>)*", "").Trim();
			gsDebugMsg = "";
			return (ew_NotEmpty(msg)) ? "<div class=\"alert alert-info ewAlert\">" + msg + "</div>" : "";
		}

		// Show global debug message // DN
		public static IHtmlContent DebugMsg() {
			var str = (EW_DEBUG_ENABLED) ? ew_DebugMsg() : "";
			return new HtmlString(str);
		}

		// Write global debug message // DN
		public static void ew_SetDebugMsg(string v) {
			if (!gsDebugMsg.EndsWith(v + "<br><br>")) // Avoid duplicate message
				gsDebugMsg = ew_AddMessage(gsDebugMsg, v);
		}

		// Permission denied message
		public static string ew_DeniedMsg() {
			return Language.Phrase("NoPermission").Replace("%s", ew_CurrentUrl());
		}

		//
		// Language class
		//

		public class cLanguage : IDisposable
		{
			public string LanguageId;
			public XmlDocument objDOM;
			public Dictionary<string, string> Col;
			public string LanguageFolder = EW_LANGUAGE_FOLDER; // DN

			// Constructor
			public cLanguage(string langfolder = "", string langid = "")
			{
				if (ew_NotEmpty(langfolder))
					LanguageFolder = langfolder;

				// Set up file list
				LoadFileList();

				// Set up language id
				if (ew_NotEmpty(langid)) { // Set up language id
					LanguageId = langid;
					ew_Session[EW_SESSION_LANGUAGE_ID] = LanguageId;
				} else if (ew_NotEmpty(ew_Get("language"))) {
					LanguageId = ew_Get("language");
					ew_Session[EW_SESSION_LANGUAGE_ID] = LanguageId;
				} else if (ew_NotEmpty(ew_Session[EW_SESSION_LANGUAGE_ID])) {
					LanguageId = Convert.ToString(ew_Session[EW_SESSION_LANGUAGE_ID]);
				} else {
					LanguageId = EW_LANGUAGE_DEFAULT_ID;
				}
				gsLanguage = LanguageId;
				EW_CSS_FLIP = EW_RTL_LANGUAGES.Contains(gsLanguage, StringComparer.InvariantCultureIgnoreCase);
				Load(LanguageId);

				// Call Language Load event
				Language_Load();
			}

			// Terminate
			public void Dispose()
			{
				objDOM = null;
			}

			// Load language file list
			private void LoadFileList()
			{
				if (ew_IsList(EW_LANGUAGE_FILE)) {
					for (int i = 0; i < EW_LANGUAGE_FILE.Count; i++)
						EW_LANGUAGE_FILE[i][1] = LoadFileDesc(ew_MapPath(LanguageFolder + EW_LANGUAGE_FILE[i][2])); // DN
				}
			}

			// Load language file description
			private string LoadFileDesc(string File)
			{
				var xmlr = new XmlTextReader(File);
				xmlr.WhitespaceHandling = WhitespaceHandling.None;
				try {
					while (!xmlr.EOF) {
						xmlr.Read();
						if (xmlr.IsStartElement() && xmlr.Name == "ew-language")
							return xmlr.GetAttribute("desc");
					}
				} finally {
					xmlr.Close();
				}
				return "";
			}

			// Load language file
			private void Load(string id)
			{
				string sFileName = GetFileName(id);
				if (ew_Empty(sFileName))
					sFileName = GetFileName(EW_LANGUAGE_DEFAULT_ID);
				if (ew_Empty(sFileName))
					return;
				if (EW_USE_DOM_XML) {
					objDOM = new XmlDocument();
					objDOM.Load(sFileName);
				} else { // DN
					var key = EW_PROJECT_NAME + "_" + sFileName.Replace(ew_WebRootPath, "").Replace(".xml", "").Replace(EW_PATH_DELIMITER, "_");
					if (ew_Session[key] != null) {
						Col = JsonConvert.DeserializeObject<Dictionary<string, string>>(Convert.ToString(ew_Session[key]));
					} else {
						Col = new Dictionary<string, string>();
						XmlToCollection(sFileName);
						ew_Session[key] = JsonConvert.SerializeObject(Col);
					}
				}

				// Set up locale / currency format for language
				JObject locale = ew_LocaleConv();
				EW_DECIMAL_POINT = Convert.ToString(locale["decimal_point"]);
				EW_THOUSANDS_SEP = Convert.ToString(locale["thousands_sep"]);
				EW_MON_DECIMAL_POINT = Convert.ToString(locale["mon_decimal_point"]);
				EW_MON_THOUSANDS_SEP = Convert.ToString(locale["mon_thousands_sep"]);
				EW_CURRENCY_SYMBOL = Convert.ToString(locale["currency_symbol"]);
				EW_POSITIVE_SIGN = Convert.ToString(locale["positive_sign"]); // Note: positive_sign can be empty.
				EW_NEGATIVE_SIGN = Convert.ToString(locale["negative_sign"]);
				EW_FRAC_DIGITS = Convert.ToInt32(locale["frac_digits"]);
				EW_P_CS_PRECEDES = Convert.ToInt32(locale["p_cs_precedes"]);
				EW_P_SEP_BY_SPACE = Convert.ToInt32(locale["p_sep_by_space"]);
				EW_N_CS_PRECEDES = Convert.ToInt32(locale["n_cs_precedes"]);
				EW_N_SEP_BY_SPACE = Convert.ToInt32(locale["n_sep_by_space"]);
				EW_P_SIGN_POSN = Convert.ToInt32(locale["p_sign_posn"]);
				EW_N_SIGN_POSN = Convert.ToInt32(locale["n_sign_posn"]);
				EW_DATE_SEPARATOR = Convert.ToString(locale["date_sep"]);
				EW_TIME_SEPARATOR = Convert.ToString(locale["time_sep"]);
				EW_DATE_FORMAT = ew_DateFormat(locale["date_format"]);
				EW_DATE_FORMAT_ID = ew_DateFormatId(locale["date_format"]);
				ew_SetupNumberFormatInfo();
			}

			// Convert XML to Collection
			private void XmlToCollection(string File)
			{
				string Key = "/";
				var OldKey = new List<string>();
				var xmlr = new XmlTextReader(File);
				xmlr.WhitespaceHandling = WhitespaceHandling.None;
				try {
					while (!xmlr.EOF) {
						xmlr.Read();
						string Name = xmlr.Name;
						string Id = xmlr.GetAttribute("id");
						if (Name == "ew-language")
							continue;
						switch (xmlr.NodeType) {
							case XmlNodeType.Element:
								if (xmlr.IsStartElement() && !xmlr.IsEmptyElement) {
									OldKey.Add(Key);
									Key += Name + "/";
									if (Id != null)
										Key += Id + "/";
								}
								if (Id != null && xmlr.IsEmptyElement) { // phrase
									Id = Name + "/" + Id;
									if (xmlr.GetAttribute("imageurl") != null)
										Col[Key + Id + "/imageurl"] = xmlr.GetAttribute("imageurl");
									if (xmlr.GetAttribute("imagewidth") != null)
										Col[Key + Id + "/imagewidth"] = xmlr.GetAttribute("imagewidth");
									if (xmlr.GetAttribute("imageheight") != null)
										Col[Key + Id + "/imageheight"] = xmlr.GetAttribute("imageheight");
									if (xmlr.GetAttribute("class") != null)
											Col[Key + Id + "/class"] = xmlr.GetAttribute("class");
									if (xmlr.GetAttribute("client") == "1")
										Id += "/1";
									Col[Key + Id] = xmlr.GetAttribute("value");
								}
								break;
							case XmlNodeType.EndElement:
								Key = OldKey.Last();
								OldKey.RemoveAt(OldKey.Count - 1);
								break;
						}
					}
				} finally {
					xmlr.Close();
				}
			}

			// Get language file name
			private string GetFileName(string Id)
			{
				if (ew_IsList(EW_LANGUAGE_FILE)) {
					foreach (string[] langfile in EW_LANGUAGE_FILE) {
						if (langfile[0] == Id)
							return ew_MapPath(LanguageFolder + langfile[2]);
					}
				}
				return "";
			}

			// Get node attribute
			private string GetNodeAtt(XmlNode Node, string Att)
			{
				if (Node != null) {
					return ((XmlElement)Node).GetAttribute(Att);
				} else {
					return "";
				}
			}

			// Set node attribute
			private void SetNodeAtt(XmlNode Node, string Att, string Value)
			{
				if (Node != null)
					((XmlElement)Node).SetAttribute(Att, Value);
			}

			// Get phrase
			public string Phrase(string Id, bool UseText = false)
			{
				var ImageUrl = PhraseAttr(Id, "imageurl");
				var ImageWidth = PhraseAttr(Id, "imagewidth");
				var ImageHeight = PhraseAttr(Id, "imageheight");
				var ImageClass = PhraseAttr(Id, "class");
				var Text = PhraseAttr(Id, "value");
				if (!UseText && ImageClass != "") {
					return "<span data-phrase=\"" + Id + "\" class=\"" + ImageClass + "\" data-caption=\"" + ew_HtmlEncode(Text) + "\"></span>";
				} else if (!UseText && ImageUrl != "") {
					var style = (ImageWidth != "") ? " width: " + ImageWidth + "px;" : "";
					style += (ImageHeight != "") ? " height: " + ImageHeight + "px;" : "";
					return "<img data-phrase=\"" + Id + "\" src=\"" + ew_HtmlEncode(ew_AppPath(ImageUrl)) + "\" style=\"" + style.Trim() + "\" alt=\"" + ew_HtmlEncode(Text) + "\" title=\"" + ew_HtmlEncode(Text) + "\">";
				} else {
					return Text;
				}
			}

			// Set phrase
			public void SetPhrase(string Id, string Value)
			{
				SetPhraseAttr(Id, "value", Value);
			}

			// Get project phrase
			public string ProjectPhrase(string Id)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					return GetNodeAtt(objDOM.SelectSingleNode("//project/phrase[@id='" + Id + "']"), "value");
				} else {
					Id = "/project/phrase/" + Id;
					return Col.ContainsKey(Id) ? Col[Id] : "";
				}
			}

			// Set project phrase
			public void SetProjectPhrase(string Id, string Value)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					SetNodeAtt(objDOM.SelectSingleNode("//project/phrase[@id='" + Id + "']"), "value", Value);
				} else {
					Col["/project/phrase/" + Id] = Value;
				}
			}

			// Get menu phrase
			public string MenuPhrase(string MenuId, string Id)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					return GetNodeAtt(objDOM.SelectSingleNode("//project/menu[@id='" + MenuId.ToLower() + "']/phrase[@id='" + Id + "']"), "value");
				} else {
					Id = "/project/menu/" + MenuId.ToLower() + "/phrase/" + Id;
					return Col.ContainsKey(Id) ? Col[Id] : "";
				}
			}

			// Set menu phrase
			public void SetMenuPhrase(string MenuId, string Id, string Value)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					SetNodeAtt(objDOM.SelectSingleNode("//project/menu[@id='" + MenuId.ToLower() + "']/phrase[@id='" + Id + "']"), "value", Value);
				} else {
					Col["/project/menu/" + MenuId.ToLower() + "/phrase/" + Id] = Value;
				}
			}

			// Get table phrase
			public string TablePhrase(string TblVar, string Id)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					return GetNodeAtt(objDOM.SelectSingleNode("//project/table[@id='" + TblVar.ToLower() + "']/phrase[@id='" + Id + "']"), "value");
				} else {
					return Col["/project/table/" + TblVar.ToLower() + "/phrase/" + Id];
				}
			}

			// Set table phrase
			public void SetTablePhrase(string TblVar, string Id, string Value)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					SetNodeAtt(objDOM.SelectSingleNode("//project/table[@id='" + TblVar.ToLower() + "']/phrase[@id='" + Id + "']"), "value", Value);
				} else {
					Col["/project/table/" + TblVar.ToLower() + "/phrase/" + Id] = Value;
				}
			}

			// Get field phrase
			public string FieldPhrase(string TblVar, string FldVar, string Id)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					return GetNodeAtt(objDOM.SelectSingleNode("//project/table[@id='" + TblVar.ToLower() + "']/field[@id='" + FldVar.ToLower() + "']/phrase[@id='" + Id + "']"), "value");
				} else {
					Id = "/project/table/" + TblVar.ToLower() + "/field/" + FldVar.ToLower() + "/phrase/" + Id;
					return Col.ContainsKey(Id) ? Col[Id] : "";
				}
			}

			// Set field phrase
			public void SetFieldPhrase(string TblVar, string FldVar, string Id, string Value)
			{
				Id = Id.ToLower();
				if (EW_USE_DOM_XML) {
					SetNodeAtt(objDOM.SelectSingleNode("//project/table[@id='" + TblVar.ToLower() + "']/field[@id='" + FldVar.ToLower() + "']/phrase[@id='" + Id + "']"), "value", Value);
				} else {
					Col["/project/table/" + TblVar.ToLower() + "/field/" + FldVar.ToLower() + "/phrase/" + Id] = Value;
				}
			}

			// Get phrase attribute
			public string PhraseAttr(string Id, string Name)
			{
				Id = Id.ToLower();
				Name = Name.ToLower();
				if (EW_USE_DOM_XML) {
					return GetNodeAtt(objDOM.SelectSingleNode("//global/phrase[@id='" + Id + "']"), Name);
				} else {
					Id += (Name != "value") ? "/" + Name : "";
					if (Col.ContainsKey("/global/phrase/" + Id)) {
						return Col["/global/phrase/" + Id];
					} else if (Col.ContainsKey("/global/phrase/" + Id + "/1")) {
						return Col["/global/phrase/" + Id + "/1"];
					}
				}
				return "";
			}

			// Set phrase attribute
			public void SetPhraseAttr(string Id, string Name, string Value)
			{
				Id = Id.ToLower();
				Name = Name.ToLower();
				if (EW_USE_DOM_XML) {
					XmlNode Node = objDOM.SelectSingleNode("//global/phrase[@id='" + Id + "']");
					if (Node == null) { // Create new phrase
						Node = (XmlNode)objDOM.CreateElement(ew_XmlTagName("phrase"));
						SetNodeAtt(Node, "id", Id);
						objDOM.SelectSingleNode("//global").AppendChild(Node);
					}
					SetNodeAtt(Node, Name, Value);
				} else {
					Id += (Name != "value") ? "/" + Name : "";
					if (Col.ContainsKey("/global/phrase/" + Id)) {
						Col["/global/phrase/" + Id] = Value;
					} else if (Col.ContainsKey("/global/phrase/" + Id + "/1")) {
						Col["/global/phrase/" + Id + "/1"] = Value;
					}
				}
			}

			// Get phrase class
			public string PhraseClass(string Id) {
				return PhraseAttr(Id, "class");
			}

			// Set phrase attribute
			public void SetPhraseClass(string Id, string Value) {
				SetPhraseAttr(Id, "class", Value);
			}

			// Output XML as JSON
			public string XmlToJSON(string XPath)
			{
				XmlNodeList NodeList = objDOM.SelectNodes(XPath);
				string Str = "{";
				foreach (XmlNode Node in NodeList) {
					string Id = GetNodeAtt(Node, "id");
					string Value = GetNodeAtt(Node, "value");
					Str += "\"" + ew_JsEncode2(Id) + "\":\"" + ew_JsEncode2(Value) + "\",";
				}
				if (Str.EndsWith(","))
					Str = Str.Substring(0, Str.Length - 1);
				Str += "}\r\n";
				return Str;
			}

			// Output collection as JSON
			public string CollectionToJSON(string KeyPattern)
			{
				var d = new Dictionary<string, string>();
				foreach (string Key in Col.Keys) {
					var m = Regex.Match(Key, KeyPattern);
					if (m.Success) {
						var Name = m.Groups[1].Value;
						d.Add(Name, Col[Key]);
					}
				}
				return ew_ArrayToJson(d);
			}

			// Output all phrases as JSON
			public string AllToJSON()
			{
				if (EW_USE_DOM_XML) {
					return "var ewLanguage = new ew_Language(" + XmlToJSON("//global/phrase") + ");";
				} else {
					return "var ewLanguage = new ew_Language(" + CollectionToJSON(@"^/global/phrase/(\w+)") + ");";
				}
			}

			// Output client phrases as JSON
			public string ToJSON()
			{
				if (EW_USE_DOM_XML) {
					return "var ewLanguage = new ew_Language(" + XmlToJSON("//global/phrase[@client='1']") + ");";
				} else {
					return "var ewLanguage = new ew_Language(" + CollectionToJSON(@"^/global/phrase/(\w+)/1$") + ");";
				}
			}

			// SELECT template
			//public string SelectTemplate = "<select class=\"form-control\" id=\"ewLanguage\" name=\"ewLanguage\" onchange=\"ew_SetLanguage(this);\">{options}</select>"; // SELECT tag template
			//public string OptionTemplate = "<option value=\"{langid}\">{langdesc}</option>"; // OPTION tag template
			//public string SelectedOptionTemplate = "<option value=\"{langid}\" selected>{langdesc}</option>"; // Selected OPTION tag template
			// RADIO template

			public string SelectTemplate = "<div class=\"btn-group\" data-toggle=\"buttons\">{options}</div>"; // DIV tag template
			public string OptionTemplate = "<label class=\"btn btn-default ewTooltip\" data-container=\"body\" data-placement=\"bottom\" title=\"{langdesc}\"><input type=\"radio\" name=\"ewLanguage\" autocomplete=\"off\" onchange=\"ew_SetLanguage(this);\" value=\"{langid}\">{langid}</label>"; // INPUT tag template
			public string SelectedOptionTemplate = "<label class=\"btn btn-default ewTooltip active\" data-container=\"body\" data-placement=\"bottom\" title=\"{langdesc}\"><input type=\"radio\" name=\"ewLanguage\" autocomplete=\"off\" onchange=\"ew_SetLanguage(this);\" value=\"{langid}\" checked>{langid}</label>"; // Selected INPUT tag template

			// Output language selection form
			public IHtmlContent SelectionForm() {
				string form = "";
				int cnt = EW_LANGUAGE_FILE.Count;
				if (cnt > 1) {
					for (int i = 0; i < cnt; i++) {
						string langid = EW_LANGUAGE_FILE[i][0];
						string phrase = ew_NotEmpty(Phrase(langid)) ? Phrase(langid) : EW_LANGUAGE_FILE[i][1];
						string option = (langid == gsLanguage) ? SelectedOptionTemplate : OptionTemplate;
						option = option.Replace("{langid}", langid).Replace("{langdesc}", phrase);
						form += option;
					}
				}
				if (form != "")
					form = "<div class=\"ewLanguageOption\">" + SelectTemplate.Replace("{options}", form) + "</div>";
				return new HtmlString(form);
			}

			// Language Load event
			public void Language_Load() {

				// Example:
				//SetPhrase("SaveBtn", "Save Me"); // Refer to language XML file for phrase IDs

			}
		}

		//
		// XML document class
		//

		public class cXMLDocument : IDisposable
		{
			public string Encoding = "";
			string RootTagName = "table";
			string SubTblName = "";
			string RowTagName = "row";
			XmlDocument XmlDoc;
			XmlElement XmlTbl;
			XmlElement XmlSubTbl;
			XmlElement XmlRow;
			XmlElement XmlFld;

			// Constructor
			public cXMLDocument()
			{
				XmlDoc = new XmlDocument();
			}

			// OuterXml
			public string OuterXml {
				get {
					return XML();
				}
			}

			// Load
			public XmlDocument Load(string phyfile) {
				if (File.Exists(phyfile)) {
					XmlDoc.Load(phyfile);
					return XmlDoc;
				}
				return null;
			}

			// Get document element
			public XmlElement DocumentElement {
				get {
					return XmlDoc?.DocumentElement;
				}
			}

			// Get attribute
			public string GetAttribute(ref XmlElement element, string name) {
				return element?.GetAttribute(name) ?? "";
			}

			// Set attribute
			public void SetAttribute(ref XmlElement element, string name, string value) {
				element?.SetAttribute(name, value);
				}

			// Select single node
			public XmlElement SelectSingleNode(string query) {
				var node = XmlDoc.SelectSingleNode(query);
				return (node != null) ? (XmlElement)node : null;
			}

			// Select nodes
			public XmlNodeList SelectNodes(string query) {
				return XmlDoc.SelectNodes(query);
			}

			// Add root
			public void AddRoot(string rootname)
			{
				RootTagName = ew_XmlTagName(rootname);
				XmlTbl = XmlDoc.CreateElement(RootTagName);
				XmlDoc.AppendChild(XmlTbl);
			}

			// Add row
			public void AddRow(string tablename = "", string rowname = "")
			{
				if (ew_NotEmpty(rowname))
					RowTagName = ew_XmlTagName(rowname);
				XmlRow = XmlDoc.CreateElement(RowTagName);
				if (ew_Empty(tablename)) {
					XmlTbl?.AppendChild(XmlRow);
				} else {
					if (ew_Empty(SubTblName) || !ew_SameStr(SubTblName, tablename)) {
						SubTblName = ew_XmlTagName(tablename);
						XmlSubTbl = XmlDoc.CreateElement(SubTblName);
						XmlTbl.AppendChild(XmlSubTbl);
					}
					XmlSubTbl?.AppendChild(XmlRow);
				}
			}

			// Add row by name
			public void AddRowEx(string Name)
			{
				XmlRow = XmlDoc.CreateElement(Name);
				XmlTbl.AppendChild(XmlRow);
			}

			// Add field
			public void AddField(string name, object value)
			{
				XmlFld = XmlDoc.CreateElement(ew_XmlTagName(name));
				XmlRow.AppendChild(XmlFld);
				XmlFld.AppendChild(XmlDoc.CreateTextNode(Convert.ToString(value)));
			}

			// XML
			public string XML()
			{
				return XmlDoc.OuterXml;
			}

			// Output
			public void Output()
			{
				ew_Response.Clear();
				ew_Response.ContentType = "text/xml";
				string PI = "<?xml version=\"1.0\"";
				if (ew_NotEmpty(Encoding))
					PI += " encoding=\"" + Encoding + "\"";
				PI += " ?>";
				ew_Write(PI + XmlDoc.OuterXml);
			}

			// Output XML for debug
			public void Print()
			{
				ew_Response.Clear();
				ew_Response.ContentType = "text/plain";
				ew_Write(ew_HtmlEncode(XmlDoc.OuterXml));
			}

			// Terminate
			public void Dispose()
			{
				XmlFld = null;
				XmlRow = null;
				XmlTbl = null;
				XmlDoc = null;
			}
		}

		//
		// HTML to Text class
		//

		public class HtmlToText
		{

			// Static data tables
			protected static Dictionary<string, string> _tags;
			protected static HashSet<string> _ignoreTags;

			// Instance variables
			protected TextBuilder _text;
			protected string _html;
			protected int _pos;

			// Static constructor (one time only)
			static HtmlToText()
			{
				_tags = new Dictionary<string, string>();
				_tags.Add("address", "\n");
				_tags.Add("blockquote", "\n");
				_tags.Add("div", "\n");
				_tags.Add("dl", "\n");
				_tags.Add("fieldset", "\n");
				_tags.Add("form", "\n");
				_tags.Add("h1", "\n");
				_tags.Add("/h1", "\n");
				_tags.Add("h2", "\n");
				_tags.Add("/h2", "\n");
				_tags.Add("h3", "\n");
				_tags.Add("/h3", "\n");
				_tags.Add("h4", "\n");
				_tags.Add("/h4", "\n");
				_tags.Add("h5", "\n");
				_tags.Add("/h5", "\n");
				_tags.Add("h6", "\n");
				_tags.Add("/h6", "\n");
				_tags.Add("p", "\n");
				_tags.Add("/p", "\n");
				_tags.Add("table", "\n");
				_tags.Add("/table", "\n");
				_tags.Add("ul", "\n");
				_tags.Add("/ul", "\n");
				_tags.Add("ol", "\n");
				_tags.Add("/ol", "\n");
				_tags.Add("/li", "\n");
				_tags.Add("br", "\n");
				_tags.Add("/td", "\t");
				_tags.Add("/tr", "\n");
				_tags.Add("/pre", "\n");
				_ignoreTags = new HashSet<string>();
				_ignoreTags.Add("script");
				_ignoreTags.Add("noscript");
				_ignoreTags.Add("style");
				_ignoreTags.Add("object");
			}

			// <summary>
			// Converts the given HTML to plain text and returns the result.
			// </summary>
			// <param name="html">HTML to be converted</param>
			// <returns>Resulting plain text</returns>

			public string Convert(string html)
			{

				// Initialize state variables
				_text = new TextBuilder();
				_html = html;
				_pos = 0;

				// Process input
				while (!EndOfText)
				{
					if (Peek() == '<')
					{

						// HTML tag
						bool selfClosing;
						string tag = ParseTag(out selfClosing);

						// Handle special tag cases
						if (tag == "body")
						{

							// Discard content before <body>
							_text.Clear();
						}
						else if (tag == "/body")
						{

							// Discard content after </body>
							_pos = _html.Length;
						}
						else if (tag == "pre")
						{

							// Enter preformatted mode
							_text.Preformatted = true;
							EatWhitespaceToNextLine();
						}
						else if (tag == "/pre")
						{

							// Exit preformatted mode
							_text.Preformatted = false;
						}
						string value;
						if (_tags.TryGetValue(tag, out value))
							_text.Write(value);
						if (_ignoreTags.Contains(tag))
							EatInnerContent(tag);
					}
					else if (Char.IsWhiteSpace(Peek()))
					{

						// Whitespace (treat all as space)
						_text.Write(_text.Preformatted ? Peek() : ' ');
						MoveAhead();
					}
					else
					{

						// Other text
						_text.Write(Peek());
						MoveAhead();
					}
				}

				// Return result
				return ew_HtmlDecode(_text.ToString());
			}

			// Eats all characters that are part of the current tag
			// and returns information about that tag

			protected string ParseTag(out bool selfClosing)
			{
				string tag = String.Empty;
				selfClosing = false;
				if (Peek() == '<')
				{
					MoveAhead();

					// Parse tag name
					EatWhitespace();
					int start = _pos;
					if (Peek() == '/')
						MoveAhead();
					while (!EndOfText && !Char.IsWhiteSpace(Peek()) &&
						Peek() != '/' && Peek() != '>')
						MoveAhead();
					tag = _html.Substring(start, _pos - start).ToLower();

					// Parse rest of tag
					while (!EndOfText && Peek() != '>')
					{
						if (Peek() == '"' || Peek() == '\'')
							EatQuotedValue();
						else
						{
							if (Peek() == '/')
								selfClosing = true;
							MoveAhead();
						}
					}
					MoveAhead();
				}
				return tag;
			}

			// Consumes inner content from the current tag
			protected void EatInnerContent(string tag)
			{
				string endTag = "/" + tag;
				while (!EndOfText)
				{
					if (Peek() == '<')
					{

						// Consume a tag
						bool selfClosing;
						if (ParseTag(out selfClosing) == endTag)
							return;

						// Use recursion to consume nested tags
						if (!selfClosing && !tag.StartsWith("/"))
							EatInnerContent(tag);
					}
					else MoveAhead();
				}
			}

			// Returns true if the current position is at the end of
			// the string

			protected bool EndOfText
			{
				get { return (_pos >= _html.Length); }
			}

			// Safely returns the character at the current position
			protected char Peek()
			{
				return (_pos < _html.Length) ? _html[_pos] : (char)0;
			}

			// Safely advances to current position to the next character
			protected void MoveAhead()
			{
				_pos = Math.Min(_pos + 1, _html.Length);
			}

			// Moves the current position to the next non-whitespace
			// character.

			protected void EatWhitespace()
			{
				while (Char.IsWhiteSpace(Peek()))
					MoveAhead();
			}

			// Moves the current position to the next non-whitespace
			// character or the start of the next line, whichever
			// comes first

			protected void EatWhitespaceToNextLine()
			{
				while (Char.IsWhiteSpace(Peek()))
				{
					char c = Peek();
					MoveAhead();
					if (c == '\n')
						break;
				}
			}

			// Moves the current position past a quoted value
			protected void EatQuotedValue()
			{
				char c = Peek();
				if (c == '"' || c == '\'')
				{

					// Opening quote
					MoveAhead();

					// Find end of value
					int start = _pos;
					_pos = _html.IndexOfAny(new char[] { c, '\r', '\n' }, _pos);
					if (_pos < 0)
						_pos = _html.Length;
					else
						MoveAhead();	// Closing quote
				}
			}

			// <summary>
			// A StringBuilder class that helps eliminate excess whitespace.
			// </summary>

			protected class TextBuilder
			{
				private StringBuilder _text;
				private StringBuilder _currLine;
				private int _emptyLines;
				private bool _preformatted;

				// Construction
				public TextBuilder()
				{
					_text = new StringBuilder();
					_currLine = new StringBuilder();
					_emptyLines = 0;
					_preformatted = false;
				}

				// <summary>
				// Normally, extra whitespace characters are discarded.
				// If this property is set to true, they are passed
				// through unchanged.
				// </summary>

				public bool Preformatted
				{
					get
					{
						return _preformatted;
					}
					set
					{
						if (value)
						{

							// Clear line buffer if changing to
							// preformatted mode

							if (_currLine.Length > 0)
								FlushCurrLine();
							_emptyLines = 0;
						}
						_preformatted = value;
					}
				}

				// <summary>
				// Clears all current text.
				// </summary>

				public void Clear()
				{
					_text.Length = 0;
					_currLine.Length = 0;
					_emptyLines = 0;
				}

				// <summary>
				// Writes the given string to the output buffer.
				// </summary>
				// <param name="s"></param>

				public void Write(string s)
				{
					foreach (char c in s)
						Write(c);
				}

				// <summary>
				// Writes the given character to the output buffer.
				// </summary>
				// <param name="c">Character to write</param>

				public void Write(char c)
				{
					if (_preformatted)
					{

						// Write preformatted character
						_text.Append(c);
					}
					else
					{
						if (c == '\r')
						{

							// Ignore carriage returns. We'll process
							// '\n' if it comes next

						}
						else if (c == '\n')
						{

							// Flush current line
							FlushCurrLine();
						}
						else if (Char.IsWhiteSpace(c))
						{

							// Write single space character
							int len = _currLine.Length;
							if (len == 0 || !Char.IsWhiteSpace(_currLine[len - 1]))
								_currLine.Append(' ');
						}
						else
						{

							// Add character to current line
							_currLine.Append(c);
						}
					}
				}

				// Appends the current line to output buffer
				protected void FlushCurrLine()
				{

					// Get current line
					string line = _currLine.ToString().Trim();

					// Determine if line contains non-space characters
					string tmp = line.Replace("&nbsp;", String.Empty);
					if (tmp.Length == 0)
					{

						// An empty line
						_emptyLines++;
						if (_emptyLines < 2 && _text.Length > 0)
							_text.AppendLine(line);
					}
					else
					{

						// A non-empty line
						_emptyLines = 0;
						_text.AppendLine(line);
					}

					// Reset current line
					_currLine.Length = 0;
				}

				// <summary>
				// Returns the current output as a string.
				// </summary>

				public override string ToString()
				{
					if (_currLine.Length > 0)
						FlushCurrLine();
					return _text.ToString();
				}
			}
		}

		//
		// Email class
		//

		public class cEmail
		{
			public string Sender = ""; // Sender
			public string Recipient = ""; // Recipient
			public string Cc = ""; // Cc
			public string Bcc = ""; // Bcc
			public string Subject = ""; // Subject
			public string Format = "HTML"; // Format
			public string Content = ""; // Content

			//public string AttachmentContent = ""; // Attachement content
			//public string AttachmentFileName = ""; // Attachment file name

			public List<string> Attachments = new List<string>(); // Attachments
			public List<string> EmbeddedImages = new List<string>(); // Embedded image
			public string Charset = EW_EMAIL_CHARSET; // Charset
			public string SendErrNumber = ""; // Send error number
			public string SendErrDescription = ""; // Send error description
			public bool EnableSsl = ew_SameText(EW_SMTP_SECURE_OPTION, "SSL"); // Send secure option
			public SmtpClient Mailer = null;

			// Load email from template
			public void Load(string fn, string langid = "")
			{
				string wrkfile = "";
				string wrkpath = "";
				string sWrk = "";
				string wrkid, sContent, sHeader;
				langid = (ew_Empty(langid)) ? gsLanguage : langid;
				int pos = fn.LastIndexOf('.');
				if (pos > -1) {
					string wrkname = fn.Substring(0, pos); // Get file name
					string wrkext = fn.Substring(pos+1); // Get file extension
					wrkpath = EW_EMAIL_TEMPLATE_PATH + EW_PATH_DELIMITER; // Get file path
					var ar = (ew_NotEmpty(langid)) ? new List<string>{"_" + langid, "-" + langid, ""} : new List<string>();
					bool exist = false;
					foreach (var suffix in ar) {
						wrkfile = wrkpath + wrkname + suffix + "." + wrkext;
						exist = File.Exists(ew_MapPath(wrkfile));
						if (exist) break;
					}
					if (!exist) return;
					sWrk = ew_LoadTxt(wrkfile); // Load template file content
					if (sWrk.StartsWith("\xEF\xBB\xBF")) // UTF-8 BOM
						sWrk = sWrk.Substring(3);
					wrkid = wrkname + "_content";
					if (sWrk.Contains(wrkid)) { // Replace content
						wrkfile = wrkpath + wrkid + "." + wrkext;
						if (File.Exists(ew_MapPath(wrkfile))) {
							sContent = ew_LoadTxt(wrkfile);
							if (sContent.StartsWith("\xEF\xBB\xBF")) // UTF-8 BOM
								sContent = sContent.Substring(3);
							sWrk = sWrk.Replace("<!--" + wrkid + "-->", sContent);
						}
					}
				}
				sWrk = sWrk.Replace("\r\n", "\n");

				// Convert to Lf
				sWrk = sWrk.Replace("\r", "\n");

				// Convert to Lf
				if (ew_NotEmpty(sWrk)) {
					int i = sWrk.IndexOf("\n" + "\n");

					// Locate header and mail content
					if (i > 0) {
						sHeader = sWrk.Substring(0, i + 1);
						Content = sWrk.Substring(i + 2);
						string[] arrHeader = sHeader.Split('\n');
						for (int j = 0; j <= arrHeader.GetUpperBound(0); j++) {
							i = arrHeader[j].IndexOf(":");
													if (i > 0)
													{
															string sName = arrHeader[j].Substring(0, i).Trim();
															string sValue = arrHeader[j].Substring(i + 1).Trim();
															switch (sName.ToLower())
															{
																	case "subject":
																			Subject = sValue;
																			break;
																	case "from":
																			Sender = sValue;
																			break;
																	case "to":
																			Recipient = sValue;
																			break;
																	case "cc":
																			Cc = sValue;
																			break;
																	case "bcc":
																			Bcc = sValue;
																			break;
																	case "format":
																			Format = sValue;
																			break;
								}
							}
						}
					}
				}
			}

			// Replace sender
			public void ReplaceSender(string ASender)
			{
				if (Sender.Contains("<!--$From-->"))
					Sender = Sender.Replace("<!--$From-->", ASender);
				else
					Sender = ASender;
			}

			// Replace recipient
			public void ReplaceRecipient(string ARecipient)
			{
				if (Recipient.Contains("<!--$To-->"))
					Recipient = Recipient.Replace("<!--$To-->", ARecipient);
				else
					AddRecipient(ARecipient);
			}

			// Method to add recipient
			public void AddRecipient(string ARecipient) {
				Recipient = ew_Concat(Recipient, ARecipient, ";");
			}

			// Add cc email
			public void AddCc(string ACc)
			{
				if (ew_NotEmpty(ACc)) {
					Cc = ew_Concat(Cc, ACc, ";");
				}
			}

			// Add bcc email
			public void AddBcc(string ABcc)
			{
				if (ew_NotEmpty(ABcc)) {
					Bcc = ew_Concat(Bcc, ABcc, ";");
				}
			}

			// Replace subject
			public void ReplaceSubject(string ASubject)
			{
				if (Subject.Contains("<!--$Subject-->"))
					Subject = Subject.Replace("<!--$Subject-->", ASubject);
				else
					Subject = ASubject;
			}

			// Replace content
			public void ReplaceContent(string Find, string ReplaceWith)
			{
				Content = Content.Replace(Find, ReplaceWith);
			}

			// Method to add embedded image
			public void AddEmbeddedImage(string image) {
				if (ew_NotEmpty(image))
					EmbeddedImages.Add(image);
			}

			// Method to add attachment
			public void AddAttachment(string filename) {
				if (ew_NotEmpty(filename))
					Attachments.Add(filename);
			}

			// Send email
			public bool Send()
			{
				bool bSend = ew_SendEmail(Sender, Recipient, Cc, Bcc, Subject, Content, Format, Charset, EnableSsl, Attachments, EmbeddedImages, Mailer);
				if (!bSend)
					SendErrDescription = gsEmailErrDesc; // Send error description
				return bSend;
			}
		}

		//
		// Class for Pager item
		//

		public class cPagerItem
		{
			public string Text;
			public int Start;
			public bool Enabled;

			// Constructor
			public cPagerItem(int AStart, string AText, bool AEnabled)
			{
				Text = AText;
				Start = AStart;
				Enabled = AEnabled;
			}

			// Constructor
			public cPagerItem()
			{

				// Do nothing
			}
		}

		//
		// Class for PrevNext pager
		//

		public class cPager
		{
			public cPagerItem NextButton;
			public cPagerItem FirstButton;
			public cPagerItem PrevButton;
			public cPagerItem LastButton;
			public int PageSize;
			public int FromIndex;
			public int ToIndex;
			public int RecordCount;
			public bool Visible = true;
		}

		//
		// Class for Numeric pager
		//

		public class cNumericPager : cPager
		{
			public List<cPagerItem> Items = new List<cPagerItem>();
			public int Range;
			public int ButtonCount = 0;
			public bool AutoHidePager = true;

			// Constructor
			public cNumericPager(int AFromIndex, int APageSize, int ARecordCount, int ARange, bool AAutoHidePager = EW_AUTO_HIDE_PAGER)
			{
				AutoHidePager = AAutoHidePager;
				if (AAutoHidePager && AFromIndex == 1 && ARecordCount <= APageSize)
					Visible = false;
				FromIndex = AFromIndex;
				PageSize = APageSize;
				RecordCount = ARecordCount;
				Range = ARange;
				FirstButton = new cPagerItem();
				PrevButton = new cPagerItem();
				NextButton = new cPagerItem();
				LastButton = new cPagerItem();
				Init();
			}

			// Init pager
			public void Init()
			{
				if (FromIndex > RecordCount) FromIndex = RecordCount;
				ToIndex = FromIndex + PageSize - 1;
				if (ToIndex > RecordCount) ToIndex = RecordCount;
				SetupNumericPager();

				// Update button count
				if (FirstButton.Enabled) ButtonCount++;
							if (PrevButton.Enabled) ButtonCount++;
							if (NextButton.Enabled) ButtonCount++;
							if (LastButton.Enabled) ButtonCount++;
			}

			// Add pager item
			private void AddPagerItem(int StartIndex, string Text, bool Enabled)
			{
				Items.Add(new cPagerItem(StartIndex, Text, Enabled));
			}

			// Setup pager items
			private void SetupNumericPager()
			{
				bool HasPrev;
				bool NoNext;
				int dy2;
				int dx2;
				int y;
				int x;
				int dx1;
				int dy1;
				int ny;
				int TempIndex;
				if (RecordCount > PageSize) {
					NoNext = (RecordCount < (FromIndex + PageSize));
					HasPrev = (FromIndex > 1);

					// First Button
					TempIndex = 1;
					FirstButton.Start = TempIndex;
					FirstButton.Enabled = (FromIndex > TempIndex);

					// Prev Button
					TempIndex = FromIndex - PageSize;
					if (TempIndex < 1) TempIndex = 1;
					PrevButton.Start = TempIndex;
					PrevButton.Enabled = HasPrev;

					// Page links
					if (HasPrev | !NoNext) {
						x = 1;
						y = 1;
						dx1 = ((FromIndex - 1) / (PageSize * Range)) * PageSize * Range + 1;
						dy1 = ((FromIndex - 1) / (PageSize * Range)) * Range + 1;
						if ((dx1 + PageSize * Range - 1) > RecordCount) {
							dx2 = (RecordCount / PageSize) * PageSize + 1;
							dy2 = (RecordCount / PageSize) + 1;
						} else {
							dx2 = dx1 + PageSize * Range - 1;
							dy2 = dy1 + Range - 1;
						}
						while (x <= RecordCount) {
							if (x >= dx1 & x <= dx2) {
								AddPagerItem(x, Convert.ToString(y), FromIndex != x);
								x = x + PageSize;
								y = y + 1;
							}
							else if (x >= (dx1 - PageSize * Range) & x <= (dx2 + PageSize * Range)) {
								if (x + Range * PageSize < RecordCount) {
									AddPagerItem(x, y + "-" + (y + Range - 1), true);
								} else {
									ny = (RecordCount - 1) / PageSize + 1;
									if (ny == y) {
										AddPagerItem(x, Convert.ToString(y), true);
									} else {
										AddPagerItem(x, y + "-" + ny, true);
									}
								}
								x = x + Range * PageSize;
								y = y + Range;
							} else {
								x = x + Range * PageSize;
								y = y + Range;
							}
						}
					}

					// Next Button
					NextButton.Start = FromIndex + PageSize;
					TempIndex = FromIndex + PageSize;
					NextButton.Start = TempIndex;
					NextButton.Enabled = !NoNext;

					// Last Button
					TempIndex = ((RecordCount - 1) / PageSize) * PageSize + 1;
					LastButton.Start = TempIndex;
					LastButton.Enabled = (FromIndex < TempIndex);
				}
			}
		}

		//
		// Class for PrevNext pager
		//

		public class cPrevNextPager : cPager
		{
			public int PageCount;
			public int CurrentPage;
			public bool AutoHidePager = true;

			// Constructor
			public cPrevNextPager(int AFromIndex, int APageSize, int ARecordCount, bool AAutoHidePager = EW_AUTO_HIDE_PAGER)
			{
				AutoHidePager = AAutoHidePager;
				if (AAutoHidePager && AFromIndex == 1 && ARecordCount <= APageSize)
					Visible = false;
				FromIndex = AFromIndex;
				PageSize = APageSize;
				RecordCount = ARecordCount;
				FirstButton = new cPagerItem();
				PrevButton = new cPagerItem();
				NextButton = new cPagerItem();
				LastButton = new cPagerItem();
				Init();
			}

			// Method to init pager
			public void Init()
			{
				int TempIndex;
				if (PageSize > 0) {
					CurrentPage = (FromIndex - 1) / PageSize + 1;
					PageCount = (RecordCount - 1) / PageSize + 1;
					if (FromIndex > RecordCount) FromIndex = RecordCount;
					ToIndex = FromIndex + PageSize - 1;
					if (ToIndex > RecordCount) ToIndex = RecordCount;

					// First Button
					TempIndex = 1;
					FirstButton.Start = TempIndex;
					FirstButton.Enabled = (TempIndex != FromIndex);

					// Prev Button
					TempIndex = FromIndex - PageSize;
					if (TempIndex < 1) TempIndex = 1;
					PrevButton.Start = TempIndex;
					PrevButton.Enabled = (TempIndex != FromIndex);

					// Next Button
					TempIndex = FromIndex + PageSize;
					if (TempIndex > RecordCount) TempIndex = FromIndex;
					NextButton.Start = TempIndex;
					NextButton.Enabled = (TempIndex != FromIndex);

					// Last Button
					TempIndex = ((RecordCount - 1) / PageSize) * PageSize + 1;
					LastButton.Start = TempIndex;
					LastButton.Enabled = (TempIndex != FromIndex);
				}
			}
		}

		//
		// Breadcrumb class
		//

		public class cBreadcrumbLink { // DN
			public string Id;
			public string Title;
			public string Url;
			public string Cls;
			public string TableVar;
			public bool IsCurrent;

			// Constructor
			public cBreadcrumbLink(string aid, string atitle, string aurl, string acls, string atablevar = "", bool acurrent = false) {
				Set(aid, atitle, aurl, acls, atablevar, acurrent);
			}

			// Set properties
			public void Set(string aid, string atitle, string aurl, string acls, string atablevar = "", bool acurrent = false) {
				Id = aid;
				Title = atitle;
				Url = aurl;
				Cls = acls;
				TableVar = atablevar;
				IsCurrent = acurrent;
			}
		}
		public class cBreadcrumb {
			public List<cBreadcrumbLink> Links = new List<cBreadcrumbLink>();
			public List<cBreadcrumbLink> SessionLinks = new List<cBreadcrumbLink>();
			public bool Visible = true;

			// Constructor
			public cBreadcrumb() {
				Links.Add(new cBreadcrumbLink("home", "HomePage", "Index", "ewHome")); // Home
			}

			// Check if an item exists
			public bool Exists(string pageid, string table, string pageurl) {
				foreach (var Link in Links) {
					if (pageid == Link.Id && table == Link.TableVar && pageurl == Link.Url)
						return true;
				}
				return false;
			}

			// Add breadcrumb
			public void Add(string pageid, string pagetitle, string pageurl, string pageurlclass = "", string table = "", bool current = false) {

				// Load session links
				LoadSession();

				// Get list of master tables
				List<string> mastertable = new List<string>();
				if (ew_NotEmpty(table)) {
					var tablevar = table;
					while (ew_NotEmpty(ew_Session[EW_PROJECT_NAME + "_" + tablevar + "_" + EW_TABLE_MASTER_TABLE])) {
						tablevar = Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + tablevar + "_" + EW_TABLE_MASTER_TABLE]);
						if (mastertable.Contains(tablevar))
							break;
						mastertable.Add(tablevar);
					}
				}

				// Add master links first
				foreach (var Link in SessionLinks) {
					if (mastertable.Contains(Link.TableVar) && Link.Id == "list") {
						if (Link.Url == pageurl)
							break;
						if (!Exists(Link.Id, Link.TableVar, Link.Url)) // DN
							Links.Add(new cBreadcrumbLink(Link.Id, Link.Title, Link.Url, Link.Cls, Link.TableVar, false));
					}
				}

				// Add this link
				if (!Exists(pageid, table, pageurl))
					Links.Add(new cBreadcrumbLink(pageid, pagetitle, pageurl, pageurlclass, table, current));

				// Save session links
				SaveSession();
			}

			// Save links to Session
			public void SaveSession() {
				ew_Session[EW_SESSION_BREADCRUMB] = JsonConvert.SerializeObject(Links); // DN
			}

			// Load links from Session
			public void LoadSession() { // DN
				if (ew_Session[EW_SESSION_BREADCRUMB] != null)
					SessionLinks = JsonConvert.DeserializeObject<List<cBreadcrumbLink>>(Convert.ToString(ew_Session[EW_SESSION_BREADCRUMB]));
			}

			// Load language phrase
			public string LanguagePhrase(string title, string table, bool current) {
				var wrktitle = (title == table) ? Language.TablePhrase(title, "TblCaption") : Language.Phrase(title);
				if (current)
					wrktitle = "<span id=\"ewPageCaption\">" + wrktitle + "</span>";
				return wrktitle;
			}
			#pragma warning disable 162

			// Render
			public void Render() {
				if (!Visible || EW_PAGE_TITLE_STYLE == "" || EW_PAGE_TITLE_STYLE == "None")
					return;
				var nav = "<ul class=\"breadcrumb ewBreadcrumbs\">";
				if (ew_IsList(Links)) {
					var cnt = Links.Count;
					if (EW_PAGE_TITLE_STYLE == "Caption") {
						ew_Write("<div class=\"ewPageTitle\">" + LanguagePhrase(Links[cnt-1].Title, Links[cnt-1].TableVar, Links[cnt-1].IsCurrent) + "</div>");
						return;
					} else {
						for (var i = 0; i < cnt; i++) {
							var Link = Links[i];
							var url = Link.Url;
							if (i < cnt - 1) {
								nav += "<li id=\"ewBreadcrumb" + (i + 1) + "\">";
							} else {
								nav += "<li id=\"ewBreadcrumb" + (i + 1) + "\" class=\"active\">";
								url = ""; // No need to show URL for current page
							}
							var text = LanguagePhrase(Link.Title, Link.TableVar, Link.IsCurrent);
							var title = ew_HtmlTitle(text);
							if (ew_NotEmpty(url)) {
								nav += "<a href=\"" + ew_GetUrl(url) + "\""; // Output the URL as is // DN
								if (ew_NotEmpty(title) && title != text)
									nav += " title=\"" + ew_HtmlEncode(title) + "\"";
								if (Link.Cls != "")
									nav += " class=\"" + Link.Cls + "\"";
								nav += ">" + text + "</a>";
							} else {
								nav += text;
							}
							nav += "</li>";
						}
					}
				}
				nav += "</ul>";
				ew_Write(nav);
			}
		}
		#pragma warning restore 162

		//
		// Table classes
		//
		// Common class for table and report

		public class cTableBase {
			public string TableVar = "";
			public string TableName = "";
			public string TableType = "";
			private string _TableCaption = "";
			public string DBID = "DB"; // Table database ID
			public bool Visible = true;
			public Dictionary<string, cField> Fields = new Dictionary<string, cField>();
			public string Export = ""; // Export
			public string CustomExport = ""; // Custom export
			public bool ExportAll;
			public int ExportPageBreakCount; // Page break per every n record (PDF only)
			public string ExportPageOrientation; // Page orientation (PDF only)
			public string ExportPageSize; // Page size (PDF only)
			public float[] ExportColumnWidths; // Column widths (PDF only) // DN
			public bool SendEmail; // Send email
			public string TableCustomInnerHtml = ""; // Custom inner HTML
			public cBasicSearch BasicSearch; // Basic search
			public string CurrentFilter = ""; // Current filter
			public string CurrentOrder = ""; // Current order
			public string CurrentOrderType = ""; // Current order type
			public int RowType; // Row type
			public string CssClass = ""; // CSS class
			public string CssStyle = ""; // CSS style
			public cAttributes RowAttrs = new cAttributes(); // DN
			public Dictionary<int, string> PgCaption = new Dictionary<int, string>();
			public string CurrentAction = ""; // Current action
			public string LastAction = ""; // Last action
			public int UserIDAllowSecurity = 0; // User ID Allow
			public string Command = "";

			// Update Table
			public string UpdateTable = "";

			// Protected fields (Temp data) // DN
			protected bool emptywrk;
			protected string wrkonchange;
			protected List<OrderedDictionary> rswrk;
			protected List<OrderedDictionary> alwrk;
			protected bool selwrk;
			protected string[] arwrk;
			protected OrderedDictionary odwrk;
			protected string sSqlWrk;
			protected string sFilterWrk;
			protected string sWhereWrk;

			// Build filter from array
			public string ArrayToFilter(OrderedDictionary rs) {
				string filter = "";
				foreach (DictionaryEntry f in rs) {
					var fld = FieldByName((string)f.Key);
					ew_AddFilter(ref filter, ew_QuotedName(fld.FldName, DBID) + '=' + ew_QuotedValue(f.Value, fld.FldDataType, DBID));
				}
				return filter;
			}

			// Build UPDATE statement with WHERE clause
			// rs (array) array of field to be updated
			// where (string|array) WHERE clause as string or array of field
			//public string UpdateSQL(ref OrderedDictionary rs, string where) {
				//string sql, filter;
				//if (ew_Empty(UpdateTable) || ew_Empty(where))
					//return ""; // Does not allow updating all records
				//sql = "UPDATE " + UpdateTable + " SET ";
				//foreach (KeyValuePair<string, string> kvp in rs) {
					//if ((Fields[kvp.Key] == null) || Fields[kvp.Key].FldIsCustom)
						//continue;
					//sql += Fields[kvp.Key].FldExpression + "=";
					//sql += ew_QuotedValue(kvp.Value, Fields[kvp.Key].FldDataType, Convert.ToInt32(DBID)) + ",";
				//}
				//while (sql.Substring(-1) == ",")
					//sql = sql.Substring(0, -1);
				//filter = ew_IsList(where) ? ArrayToFilter(where) : where;
				//return sql + " WHERE " + filter;
			//}
			// Build DELETE statement
			// where (string|array) WHERE clause as string or array of field
			//public string DeleteSQL(ref object where) {
				//string sql, filter;
				//if (ew_Empty(UpdateTable) || ew_Empty(where))
					//return ""; // Does not allow deleting all records
				//sql = "DELETE FROM " + UpdateTable;
				//filter = ew_IsList(where) ? ArrayToFilter((dynamic)where) : (string)where;
				//return sql + " WHERE " + filter;
			//}
			// Reset attributes for table object

			public void ResetAttrs() {
				CssClass = "";
				CssStyle = "";
					RowAttrs.Clear();
				foreach (var kvp in Fields)
					kvp.Value.ResetAttrs();
			}

			// Setup field titles
			public void SetupFieldTitles() {
				foreach (KeyValuePair<string, cField> kvp in Fields) {
					cField fld = kvp.Value;
					if (ew_NotEmpty(fld.FldTitle)) {
						fld.EditAttrs["data-toggle"] = "tooltip";
						fld.EditAttrs["title"] = ew_HtmlEncode(fld.FldTitle);
					}
				}
			}

			// Get form values (for validation)
			public Dictionary<string, string> GetFormValues() {
				var values = new Dictionary<string, string>();
				foreach (KeyValuePair<string, cField> kvp in Fields) {
					cField fld = kvp.Value;
					values.Add(fld.FldName, fld.FormValue);
				}
				return values;
			}

			// Get field values
			public OrderedDictionary GetFieldValues(string name) {
				var values = new OrderedDictionary();
				foreach (KeyValuePair<string, cField> kvp in Fields) {
					cField fld = kvp.Value;
					PropertyInfo pi = fld.GetType().GetProperty(name); // Property
					if (pi != null) {
						values.Add(fld.FldName, pi.GetValue(fld, null));
						continue;
					}
					FieldInfo fi = fld.GetType().GetField(name); // Field
					if (fi != null) {
						values.Add(fld.FldName, fi.GetValue(fld));
						continue;
					}
					values.Add(fld.FldName, null);
				}
				return values;
			}

			// Table caption
			public string TableCaption {
				get {
					if (ew_NotEmpty(_TableCaption))
						return _TableCaption;
					else
						return Language.TablePhrase(TableVar, "TblCaption");
				}
				set { _TableCaption = value; }
			}

			// Set page caption
			public void SetPageCaption(int Page, string v) {
				PgCaption[Page] = v;
			}

			// Page caption
			public string PageCaption(int Page) {
				string Caption = "";
				if (PgCaption.ContainsKey(Page))
					Caption = PgCaption[Page];
				if (ew_NotEmpty(Caption)) {
					return Caption;
				} else {
					Caption = Language.TablePhrase(TableVar, "TblPageCaption" + Convert.ToString(Page));
					if (ew_Empty(Caption))
						Caption = "Page " + Convert.ToString(Page);
					return Caption;
				}
			}

			// Add URL parameter
			public string UrlParm(string parm = "") {
				var result = "";
				if (ew_NotEmpty(parm)) {
					if (ew_NotEmpty(result))
						result += "&";
					result += parm;
				}
				return result;
			}

			// Row Styles
			public string RowStyles {
				get {
					string sAtt = "";
					string sStyle = CssStyle;
					if (RowAttrs.ContainsKey("style") && ew_NotEmpty(RowAttrs["style"]))
						sStyle += " " + RowAttrs["style"];
					string sClass = CssClass;
					if (RowAttrs.ContainsKey("class") && ew_NotEmpty(RowAttrs["class"]))
						sClass += " " + RowAttrs["class"];
					if (ew_NotEmpty(sStyle))
						sAtt += " style=\"" + sStyle.Trim() + "\"";
					if (ew_NotEmpty(sClass))
						sAtt += " class=\"" + sClass.Trim() + "\"";
					return sAtt;
				}
			}

			// Row Attribute
			public string RowAttributes {
				get {
					string sAtt = RowStyles;
					if (ew_Empty(Export)) {
						foreach (KeyValuePair<string, string> Attr in RowAttrs) {
							if (!ew_SameText(Attr.Key, "style") && !ew_SameText(Attr.Key, "class") && ew_NotEmpty(Attr.Value))
								sAtt += " " + Attr.Key + "=\"" + Attr.Value.Trim() + "\"";
						}
					}
					return sAtt;
				}
			}

			// Get field object by name
			public cField FieldByName(string Name) {
				if (Fields.ContainsKey(Name))
					return Fields[Name];
				return null;
			}

			// Set field visibility
 			public virtual bool SetFieldVisibility(string fldname) {
				var fld = FieldByName(fldname);
				return fld.Visible; // Returns original value
			}
		}

		// class for table
		public class cTable : cTableBase {
			public string CurrentMode = ""; // Current mode
			public string UpdateConflict; // Update conflict
			public string EventName = ""; // Event name
			public bool EventCancelled; // Event cancelled
			public string CancelMessage = ""; // Cancel message
			public bool AllowAddDeleteRow = true; // Allow add/delete row
			public bool ValidateKey = true; // Validate key
			public bool DetailAdd; // Allow detail add
			public bool DetailEdit; // Allow detail edit
			public bool DetailView; // Allow detail view
			public bool ShowMultipleDetails; // Show multiple details
			public int GridAddRowCount;
			public Dictionary<string, string> CustomActions = new Dictionary<string, string>(); // Custom actions

			// Check current action
			// Add

			public bool IsAdd {
				get { return CurrentAction == "add"; }
			}

			// Copy
			public bool IsCopy {
				get { return CurrentAction == "copy" || CurrentAction == "C"; }
			}

			// Edit
			public bool IsEdit {
				get { return CurrentAction == "edit"; }
			}

			// Delete
			public bool IsDelete {
				get { return CurrentAction == "D"; }
			}

			// Confirm
			public bool IsConfirm {
				get { return CurrentAction == "F"; }
			}

			// Overwrite
			public bool IsOverwrite {
				get { return CurrentAction == "overwrite"; }
			}

			// Cancel
			public bool IsCancel {
				get { return CurrentAction == "cancel"; }
			}

			// Grid add
			public bool IsGridAdd {
				get { return CurrentAction == "gridadd"; }
			}

			// Grid edit
			public bool IsGridEdit {
				get { return CurrentAction == "gridedit"; }
			}

			// Add/Copy/Edit/GridAdd/GridEdit
			public bool IsAddOrEdit {
				get { return IsAdd || IsCopy || IsEdit || IsGridAdd || IsGridEdit; }
			}

			// Insert
			public bool IsInsert {
				get { return CurrentAction == "insert" || CurrentAction == "A"; }
			}

			// Update
			public bool IsUpdate {
				get { return CurrentAction == "update" || CurrentAction == "U"; }
			}

			// Grid update
			public bool IsGridUpdate {
				get { return CurrentAction == "gridupdate"; }
			}

			// Grid insert
			public bool IsGridInsert {
				get { return CurrentAction == "gridinsert"; }
			}

			// Grid overwrite
			public bool IsGridOverwrite {
				get { return CurrentAction == "gridoverwrite"; }
			}

			// Check last action
			// Cancelled

			public bool IsCanceled {
				get { return LastAction == "cancel" && CurrentAction == ""; }
			}

			// Inline inserted
			public bool IsInlineInserted {
				get { return LastAction == "insert" && CurrentAction == ""; }
			}

			// Inline updated
			public bool IsInlineUpdated {
				get { return LastAction == "update" && CurrentAction == ""; }
			}

			// Grid updated
			public bool IsGridUpdated {
				get { return LastAction == "gridupdate" && CurrentAction == ""; }
			}

			// Grid inserted
			public bool IsGridInserted {
				get { return LastAction == "gridinsert" && CurrentAction == ""; }
			}

			// Inserted or Updated
				public bool IsInsertedOrUpdated {
						get { return IsInlineInserted || IsInlineUpdated || IsGridUpdated || IsGridInserted; }
				}

			// Export Return Page
			public string ExportReturnUrl {
				get {
					if (ew_NotEmpty(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_EXPORT_RETURN_URL])) {
						return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_EXPORT_RETURN_URL]);
					} else {
						return ew_CurrentPage();
					}
				}
				set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_EXPORT_RETURN_URL] = value; }
			}

			// Records per page
			public int RecordsPerPage {
				get { return Convert.ToInt32(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_REC_PER_PAGE]); }
				set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_REC_PER_PAGE] = value; }
			}

			// Start record number
			public int StartRecordNumber {
				get { return Convert.ToInt32(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_START_REC]); }
				set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_START_REC] = value; }
			}

			// Search Highlight Name
			public string HighlightName {
				get { return TableVar + "_Highlight"; }
			}

			// Search WHERE clause
			public string SessionSearchWhere {
				get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_SEARCH_WHERE]); }
				set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_SEARCH_WHERE] = value; }
			}

			// Session WHERE Clause
			public string SessionWhere {
				get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_WHERE]); }
				set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_WHERE] = value; }
			}

			// Session ORDER BY
			public string SessionOrderBy {
				get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_ORDER_BY]); }
				set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_ORDER_BY] = value; }
			}

			// Session key
			public object GetKey(string fld) {
				return ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_KEY + "_" + fld];
			}
			public void SetKey(string fld, object v) {
				ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_KEY + "_" + fld] = v;
			}
		}

		//
		// Field class of T
		//

		public class cField<T> : cField	{ // DN

			// Get data type
			public T Type {
				get {
					return (T)FldDbType;
				}
			}
		}

		//
		// Field class
		//

		public class cField {
			public string TblName;
			public string TblVar; // Table var
			public string FldName; // Field name
			public string FldVar; // Field variable name
			public string FldExpression; // Field expression (used in SQL)
			public string FldBasicSearchExpression; // Field expression (used in basic search SQL)
			public bool FldIsCustom = false; // Custom field
			public bool FldIsVirtual; // Virtual field
			public string FldVirtualExpression; // Virtual field expression (used in ListSQL)
			public bool FldForceSelection; // Autosuggest force selection
			public bool FldSelectMultiple; // Select multiple
			public bool FldVirtualSearch; // Search as virtual field
			public object VirtualValue; // Virtual field value
			public object TooltipValue; // Field tooltip value
			public int TooltipWidth = 0; // Field tooltip width
			public int FldType; // Field type (ADO data type)
			public object FldDbType; // Field type (.NET data type)
			public int FldDataType; // Field type (ASP.NET Maker data type)
			public string FldBlobType; // For Oracle only
			public bool Visible = true; // Visible
			public string FldViewTag = ""; // View Tag
			public string FldHtmlTag; // Html Tag
			public bool FldIsDetailKey = false; // Field is detail key
			public int FldDateTimeFormat; // Date time format
			public string CssStyle = ""; // CSS style
			public string CssClass = ""; // CSS class
			public string ImageAlt = ""; // Image alt
			public int ImageWidth = 0; // Image width
			public int ImageHeight = 0; // Image height
			public bool ImageResize = false; // Image resize

			// remove public int ResizeQuality = 100; // Resize quality
			public bool IsBlobImage = false; // Is blob image
			public object ViewCustomAttributes;
			public object EditCustomAttributes;
			public object CellCustomAttributes;
			public object LinkCustomAttributes; // Link custom attributes
			public string CustomMsg = ""; // Custom message
			public string CellCssClass = ""; // Cell CSS class
			public string CellCssStyle = ""; // Cell CSS style
			public string MultiUpdate = ""; // Multi update
			public object OldValue; // Old Value
			public object ConfirmValue; // Confirm Value
			public object CurrentValue; // Current value
			public object ViewValue; // View value
			public object EditValue; // Edit value
			public object EditValue2; // Edit value 2 (search)
			public object HrefValue; // Href value
			public object HrefValue2; // Href value 2 (confirm page upload control)
			public cAttributes CellAttrs = new cAttributes(); // Cell attributes
			public cAttributes EditAttrs = new cAttributes(); // Edit Attributes
			public cAttributes ViewAttrs = new cAttributes(); // View Attributes
			public cAttributes LinkAttrs = new cAttributes(); // Link custom attributes
			public Dictionary<string, string> LookupFilters = new Dictionary<string, string>();
			public int OptionCount = 0;
			private string _FormValue; // Form value
			private string _QueryStringValue; // QueryString value
			private object _DbValue; // Database value
			public bool Disabled; // Disabled
			public bool ReadOnly; // ReadOnly
			public bool UsePleaseSelect;
			public string PleaseSelectText;
			public bool TruncateMemoRemoveHtml; // Remove Html from Memo field
			public string FldDefaultErrMsg;
			public bool Sortable = true;
			public int Count = 0; // Count
			public double Total = 0; // Total
			public string TrueValue = "1";
			public string FalseValue = "0";
			public cAdvancedSearch AdvancedSearch; // Advanced Search Object
			public cUpload Upload; // Upload Object
			public bool IsUpload; // DN
			public string UploadPath = EW_UPLOAD_DEST_PATH; // Upload path
			public string OldUploadPath = EW_UPLOAD_DEST_PATH; // Old upload path (for deleting old image)
			public string UploadAllowedFileExt = EW_UPLOAD_ALLOWED_FILE_EXT; // Allowed file extensions
			public int UploadMaxFileSize = EW_MAX_FILE_SIZE; // Upload max file size
			public int UploadMaxFileCount = EW_MAX_FILE_COUNT; // Upload max file count
			public bool UploadMultiple = false; // Multiple Upload
			public bool UseColorbox = EW_USE_COLORBOX; // Use Colorbox
			public object DisplayValueSeparator = ", "; // String or String[]
			public bool Exportable = true;
			public bool AutoFillOriginalValue = EW_AUTO_FILL_ORIGINAL_VALUE;
			public string ReqErrMsg = Language.Phrase("EnterRequiredField");
			public Func<string> FldSelectFilter;
			public object FldSearchDefault { get; set; }
			public object FldSearchDefault2 { get; set; }
			public object FldEditCustomAttributes { get; set; }
			public object FldViewCustomAttributes { get; set; }
			public object FldTagACustomAttributes { get; set; }
			public object FldAutoUpdateValue { get; set; }
			public object FldDefault { get; set; }
			public object FldTagHiddenValue { get; set; }
			public object FldUploadPath { get; set; }
			public object FldServerValidateArgs { get; set; }
			public object FldTagAPrefix { get; set; }
			public object FldTagASuffix { get; set; }
			public Action<cField, string> SetupLookupFilters { get; set; } // DN
			public Action<cField, string> SetupAutoSuggestFilters { get; set; } // DN

			// Init
			public void Init() {
				FldDataType = ew_FieldDataType(FldType);
				AdvancedSearch = new cAdvancedSearch(TblVar, FldVar);
				if (ew_QueryString.ContainsKey(FldVar))
					SetQueryStringValue(ew_Get(FldVar), false);
				if (ew_Form.ContainsKey(FldVar))
					SetFormValue(ew_Post(FldVar), false);
				if (IsUpload) {
					Upload = new cUpload(TblVar, FldVar);
					Upload.UploadMultiple = UploadMultiple;
				}
			}

			// Place holder
			private string _PlaceHolder = "";
			public string PlaceHolder {
				get {
					return (ReadOnly || (EditAttrs.ContainsKey("readonly"))) ? "" : _PlaceHolder;
				}
				set {
					_PlaceHolder = value;
				}
			}

			// Field caption
			private string Caption = "";
			public string FldCaption {
				get {
					if (ew_NotEmpty(Caption))
						return Caption;
					else
						return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldCaption");
				}
				set {
					Caption = value;
				}
			}

			// Field title
			public string FldTitle {
				get {
					return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldTitle");
				}
			}

			// Field alt
			public string FldAlt {
				get {
					return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldAlt");
				}
			}

			// Field error msg
			public string FldErrMsg {
				get {
					string sFldErrMsg = Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldErrMsg");
					if (ew_Empty(sFldErrMsg))
						sFldErrMsg = FldDefaultErrMsg + " - " + FldCaption;
					return sFldErrMsg;
				}
			}

			// Field option value
			public string FldTagValue(int i) {
				return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldTagValue" + Convert.ToString(i));
			}

			// Field option caption
			public string FldTagCaption(int i) {
				return Language.FieldPhrase(TblVar, FldVar.Substring(2), "FldTagCaption" + Convert.ToString(i));
			}

			// Set field visibility
			public void SetVisibility() {
				Visible = ((cTable)ew_ViewData[TblVar]).SetFieldVisibility(FldName);
			}

			// Field option caption by option value
			public string OptionCaption(string val) {
				for (var i = 0; i < OptionCount; i++) {
					if (val == FldTagValue(i + 1)) {
						string caption = FldTagCaption(i + 1);
					return (caption != "") ? caption : val;
					}
				}
				return val;
			}

			// Get field user options as array
			public List<OrderedDictionary> Options(bool pleaseSelect = false) {
				var arwrk = new List<OrderedDictionary>();
				if (pleaseSelect) // Add "Please Select"
					arwrk.Add(new OrderedDictionary() {{0, ""}, {1, Language.Phrase("PleaseSelect")}});
				for (var i = 0; i < OptionCount; i++) {
					var value = FldTagValue(i + 1);
					var caption = FldTagCaption(i + 1);
					caption = (caption != "") ? caption : value;
					arwrk.Add(new OrderedDictionary() {{0, value}, {1, caption}});
				}
				return arwrk;
			}

			// Get select options HTML // DN
			public IHtmlContent SelectOptionListHtml(string name = "") {
				var str = "";
				var emptywrk = true;
				var curValue = CurrentPage.RowType == EW_ROWTYPE_SEARCH ? AdvancedSearch.SearchValue : CurrentValue;
				string[] armultiwrk = null;
				if (ew_IsList(EditValue)) {
					var alwrk = (List<OrderedDictionary>)EditValue;
					if (FldSelectMultiple) {
						armultiwrk = ew_NotEmpty(curValue) ? Convert.ToString(curValue).Split(',') : new string[] {};
						foreach (var odwrk in alwrk) {
							var selwrk = false;
							for (int ari = 0; ari < armultiwrk.Length; ari++) {
								if (ew_SameStr(odwrk[0], armultiwrk[ari])) {
									armultiwrk[ari] = null; // Marked for removal
									selwrk = true;
									emptywrk = false;
									break;
								}
							}
							if (!selwrk)
								continue;
							for (var i = 1; i < odwrk.Count; i++)
								odwrk[i] = ew_RemoveHtml(Convert.ToString(odwrk[i]));
							str += "<option value=\"" + ew_HtmlEncode(odwrk[0]) + "\" selected>" + DisplayValue(odwrk) + "</option>";
						}
					} else {
						if (UsePleaseSelect)
							str += "<option value=\"\">" + Language.Phrase("PleaseSelect") + "</option>";
						foreach (var odwrk in alwrk) {
							if (ew_SameStr(curValue, odwrk[0]))
								emptywrk = false;
							else
								continue;
							for (var i = 1; i < odwrk.Count; i++)
								odwrk[i] = ew_RemoveHtml(Convert.ToString(odwrk[i]));
							str += "<option value=\"" + ew_HtmlEncode(odwrk[0]) + "\" selected>" + DisplayValue(odwrk) + "</option>";
						}
					}
					if (FldSelectMultiple) {
						for (var ari = 0; ari < armultiwrk.Length; ari++) {
							if (armultiwrk[ari] != null)
								str += "<option value=\"" + ew_HtmlEncode(armultiwrk[ari]) + "\" selected>" + armultiwrk[ari] + "</option>";
						}
					} else {
						if (emptywrk && ew_NotEmpty(curValue))
							str += "<option value=\"" + ew_HtmlEncode(curValue) + "\" selected>" + curValue + "</option>";
					}
				}
				if (emptywrk)
					OldValue = "";
				return new HtmlString(str);
			}

			// Get radio buttons HTML // DN
			public IHtmlContent RadioButtonListHtml(bool isDropdown, string name, int page = -1) {
				var emptywrk = true;
				var curValue = CurrentPage.RowType == EW_ROWTYPE_SEARCH ? AdvancedSearch.SearchValue : CurrentValue;
				var str = "";
				if (ew_IsList(EditValue)) {
					var alwrk = (List<OrderedDictionary>)EditValue;
					for (var rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
						var odwrk = alwrk[rowcntwrk];
						if (ew_SameStr(curValue, odwrk[0]))
							emptywrk = false;
						else
							continue;
						var html = "<input type=\"radio\" data-table=\"" + TblVar + "\" data-field=\"" + FldVar + "\"" +
							((page > -1) ? " data-page=\"" + Convert.ToString(page) + "\"" : "") +
							" name=\"" + name + "\" id=\"" + name + "_" + Convert.ToString(rowcntwrk) + "\"" +
							" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
							" value=\"" + ew_HtmlEncode(odwrk[0]) + "\" checked" + EditAttributes + ">" + DisplayValue(odwrk);
						if (!isDropdown)
							html = "<label class=\"radio-inline\">" + html + "</label>";
						str += html;
					}
					if (emptywrk && ew_NotEmpty(curValue)) {
						var html = "<input type=\"radio\" data-table=\"" + TblVar + "\" data-field=\"" + FldVar + "\"" +
							((page > -1) ? " data-page=\"" + Convert.ToString(page) + "\"" : "") +
							" name=\"" + name + "\" id=\"" + name + "_" + Convert.ToString(alwrk.Count) + "\"" +
							" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
							" value=\"" + ew_HtmlEncode(curValue) + "\" checked" + EditAttributes + ">" + Convert.ToString(curValue);
						if (!isDropdown)
							html = "<label class=\"radio-inline\">" + html + "</label>";
						str += html;
					}
				}
				if (emptywrk)
					OldValue = "";
				return new HtmlString(str);
			}

			// Get checkboxes HTML // DN
			public IHtmlContent CheckBoxListHtml(bool isDropdown, string name, int page = -1) {
				var emptywrk = true;
				var curValue = CurrentPage.RowType == EW_ROWTYPE_SEARCH ? AdvancedSearch.SearchValue : CurrentValue;
				var str = "";
				string[] armultiwrk = null;
				if (ew_IsList(EditValue)) {
					var alwrk = (List<OrderedDictionary>)EditValue;
					armultiwrk = ew_NotEmpty(curValue) ? Convert.ToString(curValue).Split(',') : new string[] {};
					for (var rowcntwrk = 0; rowcntwrk < alwrk.Count; rowcntwrk++) {
						var odwrk = alwrk[rowcntwrk];
						var selwrk = false;
						for (var ari = 0; ari < armultiwrk.Length; ari++) {
							if (ew_SameStr(odwrk[0], armultiwrk[ari])) {
								armultiwrk[ari] = null; // Marked for removal
								selwrk = true;
								emptywrk = false;
								break;
							}
						}
						if (!selwrk)
							continue;
						var html = "<input type=\"checkbox\" data-table=\"" + TblVar + "\" data-field=\"" + FldVar + "\"" +
							((page > -1) ? " data-page=\"" + Convert.ToString(page) + "\"" : "") +
							" name=\"" + name + "\" id=\"" + name + "_" + Convert.ToString(rowcntwrk) + "\"" +
							" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
							" value=\"" + ew_HtmlEncode(alwrk[rowcntwrk][0]) + "\" checked" + EditAttributes + ">" + DisplayValue(odwrk);
						if (!isDropdown)
							html = "<label class=\"checkbox-inline\">" + html + "</label>"; // Note: No spacing within the LABEL tag
						str += html;
					}
					for (var ari = 0; ari < armultiwrk.Length; ari++) {
						if (armultiwrk[ari] != null) {
							var html = "<input type=\"checkbox\" data-table=\"" + TblVar + "\" data-field=\"" + FldVar + "\"" +
								((page > -1) ? " data-page=\"" + Convert.ToString(page) + "\"" : "") +
								" name=\"" + name + "\" value=\"" + ew_HtmlEncode(armultiwrk[ari]) + "\" checked" +
								" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
								EditAttributes + ">" + armultiwrk[ari];
							if (!isDropdown)
								html = "<label class=\"checkbox-inline\">" + html + "</label>";
							str += html;
						}
					}
				}
				if (emptywrk)
					OldValue = "";
				return new HtmlString(str);
			}

			// Get display field value separator
			// idx (int) display field index (1|2|3)

			public string GetDisplayValueSeparator(int idx) {
				if (ew_IsList(DisplayValueSeparator)) {
					List<string> sep = (List<string>)DisplayValueSeparator;
					return (idx < sep.Count) ? sep[idx - 1] : null;
				} else { // string
					string sep = Convert.ToString(DisplayValueSeparator);
					return (sep != "") ? sep : ", ";
				}
			}

			// Get display field value separator as attribute value
			public string DisplayValueSeparatorAttribute {
				get {
					if (ew_IsList(DisplayValueSeparator))
						return ew_ArrayToJson(DisplayValueSeparator);
					else
						return Convert.ToString(DisplayValueSeparator);
				}
			}

			// Get display value (for lookup field)
			public string DisplayValue(OrderedDictionary od) {
				var val = Convert.ToString(od[1]); // Display field 1
				for (var i = 2; i <= 4; i++) { // Display field 2 to 4
					var sep = GetDisplayValueSeparator(i - 1);
					if (sep == null || i >= od.Count) // No separator, break
						break;
					if (ew_NotEmpty(od[i]))
						val += sep + Convert.ToString(od[i]);
				}
				return val;
			}

			// Reset attributes for field object
			public void ResetAttrs() {
				CssStyle = "";
				CssClass = "";
				CellCssStyle = "";
				CellCssClass = "";
				CellAttrs.Clear();
				EditAttrs.Clear();
				ViewAttrs.Clear();
				LinkAttrs.Clear();
			}

			// View Attributes
			public string ViewAttributes {
				get {
					var viewattrs = ViewAttrs;
					if (FldViewTag == "IMAGE")
						viewattrs["alt"] = (ImageAlt.Trim() != "") ? ImageAlt.Trim() : ""; // IMG tag requires alt attribute
					string attrs = ""; // Custom attributes
					if (ew_IsDictionary(ViewCustomAttributes)) { // Custom attributes as array
						var ar = (Dictionary<string, string>)ViewCustomAttributes;
						foreach (KeyValuePair<string, string> kvp in ar) { // Duplicate attributes
							if (viewattrs.ContainsKey(kvp.Key)) { // Duplicate attributes
								if (kvp.Key == "style" || kvp.Key.StartsWith("on")) // "style" and events
									viewattrs.Concat(kvp.Key, kvp.Value, ";");
								else // "class" and others
									viewattrs.Concat(kvp.Key, kvp.Value, " ");
							} else {
								viewattrs[kvp.Key] = kvp.Value;
							}
						}
					} else {
						attrs = Convert.ToString(ViewCustomAttributes);
					}
					string sAtt = "", sStyle = "", sClass = CssClass;
					if (FldViewTag == "IMAGE" && ImageWidth > 0 && (!ImageResize || ImageHeight <= 0))
						sStyle += "width: " + ImageWidth + "px; ";
					if (FldViewTag == "IMAGE" && ImageHeight > 0 && (!ImageResize || ImageWidth <= 0))
						sStyle += "height: " + ImageHeight + "px; ";
					if (viewattrs.ContainsKey("style"))
						viewattrs.Concat("style", sStyle + CssStyle.Trim(), ";");
					else
						if (ew_NotEmpty(sStyle + CssStyle)) viewattrs.Add("style", sStyle + CssStyle.Trim());
					if (viewattrs.ContainsKey("class"))
						viewattrs.Concat("class", CssClass, " ");
					else
						if (ew_NotEmpty(CssClass)) viewattrs.Add("class", CssClass);
					foreach (KeyValuePair<string, string> kvp in viewattrs) {
						if (ew_NotEmpty(kvp.Key) && (ew_NotEmpty(kvp.Value) || ew_IsBooleanAttr(kvp.Key))) { // Allow boolean attributes, e.g. "disabled"
							sAtt += " " + kvp.Key;
							if (ew_NotEmpty(kvp.Value))
								sAtt += "=\"" + kvp.Value.Trim() + "\"";
						} else if (kvp.Key == "alt" && ew_Empty(kvp.Value)) { // Allow alt="" since it is a required attribute
							sAtt += " alt=\"\"";
						}
					}
					if (ew_NotEmpty(attrs)) // Custom attributes as string
						sAtt += " " + attrs;
					return sAtt;
				}
			}

			// Edit Attributes
			public string EditAttributes {
				get {
					string sAtt = "";
					string sStyle = CssStyle;
					string sClass = CssClass;
					var editattrs = EditAttrs;
					string attrs = ""; // Custom attributes
					if (ew_IsDictionary(EditCustomAttributes)) { // Custom attributes as array
						var ar = (Dictionary<string, string>)EditCustomAttributes;
						foreach (KeyValuePair<string, string> kvp in ar) { // Duplicate attributes
							if (editattrs.ContainsKey(kvp.Key)) { // Duplicate attributes
								if (kvp.Key == "style" || kvp.Key.StartsWith("on")) // "style" and events
									editattrs.Concat(kvp.Key, kvp.Value, ";");
								else // "class" and others
									editattrs.Concat(kvp.Key, kvp.Value, " ");
							} else {
								editattrs[kvp.Key] = kvp.Value;
							}
						}
					} else {
						attrs = Convert.ToString(EditCustomAttributes);
					}
					if (editattrs.ContainsKey("style"))
						editattrs.Concat("style", CssStyle, ";");
					else
						if (ew_NotEmpty(CssStyle)) editattrs.Add("style", CssStyle);
					if (editattrs.ContainsKey("class"))
						editattrs.Concat("class", CssClass, " ");
					else
						if (ew_NotEmpty(CssClass)) editattrs.Add("style", CssClass);
					if (Disabled)
						editattrs["disabled"] = "disabled";
					if (ReadOnly) // For TEXT/PASSWORD/TEXTAREA only
						editattrs["readonly"] = "readonly";
					foreach (KeyValuePair<string, string> kvp in editattrs) {
						if (ew_NotEmpty(kvp.Key) && ew_NotEmpty(kvp.Value) || ew_IsBooleanAttr(kvp.Key)) { // Allow boolean attributes, e.g. "disabled"
							sAtt += " " + kvp.Key;
							if 	(ew_NotEmpty(kvp.Value))
								sAtt += "=\"" + kvp.Value.Trim() + "\"";
						}
					}
					if (ew_NotEmpty(attrs)) // Custom attributes as string
						sAtt += " " + attrs;
					return sAtt;
				}
			}

			// Link attributes
			public string LinkAttributes {
				get {
					string sAtt = "";
					var linkattrs = LinkAttrs;
					string attrs = ""; // Custom attributes
					if (ew_IsDictionary(LinkCustomAttributes)) { // Custom attributes as array
						var ar = (Dictionary<string, string>)LinkCustomAttributes;
						foreach (KeyValuePair<string, string> kvp in ar) { // Duplicate attributes
							if (linkattrs.ContainsKey(kvp.Key)) { // Duplicate attributes
								if (kvp.Key == "style" || kvp.Key.StartsWith("on")) // "style" and events
									linkattrs.Concat(kvp.Key, kvp.Value, ";");
								else // "class" and others
									linkattrs.Concat(kvp.Key, kvp.Value, " ");
							} else {
								linkattrs[kvp.Key] = kvp.Value;
							}
						}
					} else {
						attrs = Convert.ToString(LinkCustomAttributes);
					}
					string sHref = Convert.ToString(HrefValue).Trim();
					if (ew_NotEmpty(sHref))
						linkattrs["href"] = sHref;
					foreach (KeyValuePair<string, string> kvp in linkattrs) {
						if (ew_NotEmpty(kvp.Key) && (ew_NotEmpty(kvp.Value) || ew_IsBooleanAttr(kvp.Key))) { // Allow boolean attributes, e.g. "disabled"
							sAtt += " " + kvp.Key;
							if (ew_NotEmpty(kvp.Value))
								sAtt += "=\"" + kvp.Value.Trim() + "\"";
						}
					}
					if (ew_NotEmpty(attrs)) // Custom attributes as string
						sAtt += " " + attrs;
					return sAtt;
				}
			}

			// Cell Styles (Used in export)
			public string CellStyles {
				get {
					string sAtt = "";
					string sStyle = CellCssStyle;
					string sClass = CellCssClass;
					if (CellAttrs.ContainsKey("style") && ew_NotEmpty(CellAttrs["style"]))
						sStyle += " " + CellAttrs["style"];
					if (CellAttrs.ContainsKey("class") && ew_NotEmpty(CellAttrs["class"]))
						sClass += " " + CellAttrs["class"];
					if (ew_NotEmpty(sStyle))
						sAtt += " style=\"" + sStyle.Trim() + "\"";
					if (ew_NotEmpty(sClass))
						sAtt += " class=\"" + sClass.Trim() + "\"";
					return sAtt;
				}
			}

			// Cell Attributes
			public string CellAttributes {
				get {
					string sAtt = "";
					var cellattrs = CellAttrs;
					string attrs = ""; // Custom attributes
					if (ew_IsDictionary(CellCustomAttributes)) { // Custom attributes as array
						var ar = (Dictionary<string, string>)CellCustomAttributes;
						foreach (KeyValuePair<string, string> kvp in ar) { // Duplicate attributes
							if (cellattrs.ContainsKey(kvp.Key)) { // Duplicate attributes
								if (kvp.Key == "style" || kvp.Key.StartsWith("on")) // "style" and events
									cellattrs.Concat(kvp.Key, kvp.Value, ";");
								else // "class" and others
									cellattrs.Concat(kvp.Key, kvp.Value, " ");
							} else {
								cellattrs[kvp.Key] = kvp.Value;
							}
						}
					} else {
						attrs = Convert.ToString(CellCustomAttributes);
					}
					if (cellattrs.ContainsKey("style"))
						cellattrs.Concat("style", CellCssStyle, ";");
					else
						if (ew_NotEmpty(CellCssStyle)) cellattrs.Add("style", CellCssStyle);
					if (cellattrs.ContainsKey("class"))
						cellattrs.Concat("class", CellCssClass, " ");
					else
						if (ew_NotEmpty(CellCssClass)) cellattrs.Add("class", CellCssClass);
					foreach (KeyValuePair<string, string> kvp in cellattrs) {
						if (ew_NotEmpty(kvp.Key)&& (ew_NotEmpty(kvp.Value)) || ew_IsBooleanAttr(kvp.Key)) { // Allow boolean attributes, e.g. "disabled"
							sAtt += " " + kvp.Key;
							if (ew_NotEmpty(kvp.Value))
								sAtt += "=\"" + kvp.Value.Trim() + "\"";
						}
					}
					if (ew_NotEmpty(attrs)) // Custom attributes as string
						sAtt += " " + attrs;
					return sAtt;
				}
			}

			// Sort Attributes
			public string Sort {
				get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_SORT + "_" + FldVar]); }
				set {
					if (!ew_SameText(ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_SORT + "_" + FldVar], value))
						ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + EW_TABLE_SORT + "_" + FldVar] = value;
				}
			}

			// List View value
			public string ListViewValue {
				get {
					if (ew_Empty(ViewValue)) {
						return "&nbsp;";
					} else {
						var Result = Convert.ToString(ViewValue);
						var Result2 = Regex.Replace(Result, "<[^img][^>]*>" , String.Empty); // Remove all except non-empty image tag
						return (Result2.Trim().Equals(String.Empty)) ? "&nbsp;" : Result;
					}
				}
			}
			public bool ExportOriginalValue = EW_EXPORT_ORIGINAL_VALUE; // Export original value

			// Export Caption
			public string ExportCaption {
				get {
					return (EW_EXPORT_FIELD_CAPTION) ? FldCaption : FldName;
				}
			}

			// Export Value
			public string ExportValue {
				get {
					return (ExportOriginalValue) ? Convert.ToString(CurrentValue) : Convert.ToString(ViewValue);
				}
			}

			// Get temp image
			public string GetTempImage() {
				if (FldDataType == EW_DATATYPE_BLOB) {
					if (!Convert.IsDBNull(Upload.DbValue)) { // DN
						byte[] wrkdata = (byte[])Upload.DbValue;
						if (ImageResize) {
							int wrkwidth = ImageWidth;
							int wrkheight = ImageHeight;
							ew_ResizeBinary(ref wrkdata, ref wrkwidth, ref wrkheight);
						}
						return ew_TmpImage(wrkdata);
					}
				} else {
					string wrkfile = Convert.ToString(Upload.DbValue);
					if (ew_Empty(wrkfile))
						wrkfile = Convert.ToString(CurrentValue);
					if (ew_NotEmpty(wrkfile)) {
						if (!UploadMultiple) {
							string imagefn = ew_UploadPathEx(true, UploadPath) + wrkfile;
							if (ImageResize) {
								int wrkwidth = ImageWidth;
								int wrkheight = ImageHeight;
								byte[] wrkdata = ew_ResizeFileToBinary(imagefn, ref wrkwidth, ref wrkheight);
								return ew_TmpImage(wrkdata);
							} else {
								return ew_TmpFile(imagefn);
							}
						 } else {
							var tmpfiles = wrkfile.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
							var tmpimage = "";
							foreach (var tmpfile in tmpfiles) {
								if (ew_NotEmpty(tmpfile)) {
									string imagefn = ew_UploadPathEx(true, UploadPath) + wrkfile;
									if (ImageResize) {
										int wrkwidth = ImageWidth;
										int wrkheight = ImageHeight;
										byte[] wrkdata = ew_ResizeFileToBinary(imagefn, ref wrkwidth, ref wrkheight);
										if (ew_NotEmpty(tmpimage))
											tmpimage += ",";
										tmpimage += ew_TmpImage(wrkdata);
									} else {
										if (ew_NotEmpty(tmpimage))
											tmpimage += ",";
										tmpimage += ew_ConvertFullUrl(UploadPath + tmpfile);
									}
								}
							}
							return tmpimage;
						}
					}
				}
				return "";
			}

			// Form value
			public void SetFormValue(object value, bool current = true) {
				if (ew_NotEmpty(value) && FldDataType == EW_DATATYPE_NUMBER && !ew_IsNumeric(value)) // Check data type
					_FormValue = null;
				else
					_FormValue = Convert.ToString(value);
				if (current)
					CurrentValue = _FormValue;
			}
			public string FormValue {
				get { return _FormValue; }
				set {
					if (ew_NotEmpty(value) && FldDataType == EW_DATATYPE_NUMBER && !ew_IsNumeric(value)) { // Check data type
						_FormValue = null;
						CurrentValue = _FormValue;
					} else {
						_FormValue = value;
						CurrentValue = _FormValue;
					}	
				}
			}

			// QueryString value
			public void SetQueryStringValue(object value, bool current = true) {
				if (ew_NotEmpty(value) && FldDataType == EW_DATATYPE_NUMBER && !ew_IsNumeric(value)) // Check data type
					_QueryStringValue = null;
				else
					_QueryStringValue = Convert.ToString(value);
				if (current)
					CurrentValue = _QueryStringValue;
			}
			public string QueryStringValue {
				get { return _QueryStringValue; }
				set {
					if (ew_NotEmpty(value) && FldDataType == EW_DATATYPE_NUMBER && !ew_IsNumeric(value)) { // Check data type
						_QueryStringValue = null;
						CurrentValue = _QueryStringValue;
					} else {
						_QueryStringValue = value;
						CurrentValue = _QueryStringValue;
					}
				}
			}

			// DbValue
			public void SetDbValue(object value, bool current = false) {
				_DbValue = value;
				if (current)
					CurrentValue = _DbValue;
			}
			public object DbValue {
				get {
					return _DbValue;
				}
				set {
					_DbValue = value;
					CurrentValue = _DbValue;
				}
			}

			// Session Value
			public object SessionValue {
				get {
					return JsonConvert.DeserializeObject(Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_SessionValue"])); // DN
				}
				set {
					ew_Session[EW_PROJECT_NAME + "_" + TblVar + "_" + FldVar + "_SessionValue"] = JsonConvert.SerializeObject(value); // DN
				}
			}
			public string ReverseSort()
			{
				return (Sort == "ASC") ? "DESC" : "ASC";
			}

			// Advanced search
			public string UrlParameterName(string name)
			{
				string fldparm = FldVar.Substring(2);
				if (ew_SameText(name, "SearchValue")) {
					fldparm = "x_" + fldparm;
				} else if (ew_SameText(name, "SearchOperator")) {
					fldparm = "z_" + fldparm;
				} else if (ew_SameText(name, "SearchCondition")) {
					fldparm = "v_" + fldparm;
				} else if (ew_SameText(name, "SearchValue2")) {
					fldparm = "y_" + fldparm;
				} else if (ew_SameText(name, "SearchOperator2")) {
					fldparm = "w_" + fldparm;
				}
				return fldparm;
			}

			// Lookup filter query
			public string LookupFilterQuery(bool isAutoSuggest = false, string pageId = null) {
				string str = "";
				if (isAutoSuggest) {
					if (SetupAutoSuggestFilters != null) // DN
						SetupAutoSuggestFilters(this, pageId);
				} else {
					if (SetupLookupFilters != null) // DN
						SetupLookupFilters(this, pageId);
				}
				foreach (KeyValuePair<string, string> kvp in LookupFilters) {
					if (Regex.IsMatch(kvp.Key, @"^f\d+$|^s$|^dx\d+$")) // "f<n>" or "s" or "dx<n>"
						str += kvp.Key + "=" + ew_Encrypt(kvp.Value) + "&"; // Encrypt SQL and filter
					else
						str += kvp.Key + "=" + kvp.Value + "&";
				}
				return Regex.Replace(str, "&$", ""); // Remove trailing "&"
			}

			// Set up database value
			public void SetDbValue(ref OrderedDictionary rs, object value, object def, bool skip)
			{
				bool bSkipUpdate = (skip || !Visible || Disabled);
				if (bSkipUpdate)
					return;
				switch (FldType) {
					case 2:
					case 3:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:

						// Int
						if (ew_IsNumeric(Convert.ToString(value))) {
							_DbValue = ew_Conv(value, FldType);
						} else {
							_DbValue = def;
						}
						break;
					case 5:
					case 6:
					case 14:
					case 131:
					case 139:

						// Double
						value = ew_StrToFloat(value);
						if (ew_IsNumeric(Convert.ToString(value))) {
							_DbValue = ew_Conv(value, FldType);
						} else {
							_DbValue = def;
						}
						break;
					case 4:

						// Single
						value = ew_StrToFloat(value);
						if (ew_IsNumeric(Convert.ToString(value))) {
							_DbValue = ew_Conv(value, FldType);
						} else {
							_DbValue = def;
						}
						break;
					case 7:
					case 133:
					case 135:

						// Date
						if (ew_IsDate(value)) {
							_DbValue = Convert.ToDateTime(value);
						} else {
							_DbValue = def;
						}
						break;
					case 134:
					case 145:

						// Time
						TimeSpan ts;
						if (TimeSpan.TryParse(Convert.ToString(value), out ts)) {
							_DbValue = ts;
						} else {
							_DbValue = def;
						}
						break;
					case 146:

						// DateTimeOffset
						DateTimeOffset dt;
						if (DateTimeOffset.TryParse(Convert.ToString(value), out dt)) {
							_DbValue = dt;
						} else {
							_DbValue = def;
						}
						break;
					case 201:
					case 203:
					case 129:
					case 130:
					case 200:
					case 202:

						// String
						if (EW_REMOVE_XSS) {
							_DbValue = ew_RemoveXSS(Convert.ToString(value));
						} else {
							_DbValue = Convert.ToString(value);
						}
						if (Convert.ToString(_DbValue) == "") _DbValue = def;
						break;
					case 141:

						// XML
						if (ew_NotEmpty(value)) {
							_DbValue = value;
						} else {
							_DbValue = def;
						}
						break;
					case 128:
					case 204:
					case 205:

						// Binary
						if (Convert.IsDBNull(value)) {
							_DbValue = def;
						} else {
							_DbValue = value;
						}
						break;
					case 72:

						// GUID
						if (ew_NotEmpty(value) && ew_CheckGUID(Convert.ToString(value).Trim())) {
							_DbValue = value;
						} else {
							_DbValue = def;
						}
						break;
					default:
						_DbValue = value;
						break;
				}
				rs[FldName] = _DbValue;
			}
		}

		//
		// List option collection class
		//

		public class cListOptions
		{
			public List<cListOption> Items = new List<cListOption>();
			public string CustomItem = "";
			public string Tag = "td";
			public string TagClassName = "";
			public string TableVar = "";
			public string RowCnt = "";
			public string ScriptType = "block";
			public string ScriptId = "";
			public string ScriptClassName = "";
			public string JavaScript = "";
			public int RowSpan = 1;
			public bool UseDropDownButton = false;
			public bool UseButtonGroup = false;
			public string ButtonClass = "";
			public string GroupOptionName = "button";
			public string DropDownButtonPhrase = "";
			public bool UseImageAndText = false;

			// Check visible
			public bool Visible
			{
				get {
					foreach (var item in Items) {
						if (item.Visible)
							return true;
					}
					return false;
				}
			}

			// Check group option visible
			public bool GroupOptionVisible
			{
				get {
					var cnt = 0;
					foreach (var item in Items) {
						if (item.Name != GroupOptionName &&
							((item.Visible && item.ShowInDropDown && UseDropDownButton) ||
							(item.Visible && item.ShowInButtonGroup && UseButtonGroup))) {
								cnt++;
							if (UseDropDownButton && cnt > 1)
								return true;
							else if (UseButtonGroup)
								return true;
						}
					}
					return false;
				}
			}

			// Add and return a new option
			public cListOption Add(string Name)
			{
				cListOption item = new cListOption(Name);
				item.Parent = this;
				Items.Add(item);
				return item;
			}

			// Load default settings
			public void LoadDefault()
			{
				CustomItem = "";
				foreach (var item in Items)
					item.Body = "";
			}

			// Hide all options
			public void HideAllOptions(List<string> List = null)
			{
				foreach (var item in Items)
					if (List == null || !List.Contains(item.Name))
						item.Visible = false;
			}

			// Show all options
			public void ShowAllOptions()
			{
				foreach (var item in Items)
					item.Visible = true;
			}

			// Get item by name (predefined names: view/edit/copy/delete/detail_<DetailTable>/userpermission/checkbox)
			public cListOption GetItem(string name)
			{
				return Items.Find(item => item.Name == name);
			}

			// Get item by name
			public cListOption this[string name] {
				get {
					return GetItem(name);
				}
			}

			// Get item by index
			public cListOption this[int index] {
				get {
					return Items[index];
				}
			}

			// Get item index by name (predefined names: view/edit/copy/delete/detail_<DetailTable>/userpermission/checkbox)
			public int GetItemIndex(string name)
			{
				return Items.FindIndex(item => item.Name == name);
			}

			// Move item to position
			public void MoveItem(string Name, int Pos)
			{
				int newpos = Pos;
				if (newpos < 0) // If negative, count from the end
					newpos = Items.Count + newpos;
				if (newpos < 0) {
					newpos = 0;
				} else if (newpos >= Items.Count) {
					newpos = Items.Count - 1;
				}
				var CurItem = GetItem(Name);
				int oldpos = GetItemIndex(Name);
				if (oldpos > -1 && newpos != oldpos) {
					Items.RemoveAt(oldpos); // Remove old item
					if (oldpos < newpos)
						newpos--; // Adjust new position
					Items.Insert(newpos, CurItem); // Insert new item
				}
			}

			// Render list options
			public void Render(string aPart, string aPos = "", object aRowCnt = null, string aScriptType = "block", string aScriptId = "", string aScriptClassName = "") {
				var groupitem = GetItem(GroupOptionName);
				if (ew_Empty(CustomItem) && groupitem != null && ShowPos(groupitem.OnLeft, aPos)) {
					if (UseDropDownButton) { // Render dropdown
						var buttonvalue = "";
						int cnt = 0;
						foreach (var item in Items) {
							if (item.Name != GroupOptionName && item.Visible) {
								if (item.ShowInDropDown) {
									buttonvalue += item.Body;
									cnt++;
								} else if (item.Name == "listactions") { // Show listactions as button group
									item.Body = RenderButtonGroup(item.Body);
								}
							}
						}
						if (cnt <= 1) {
							UseDropDownButton = false; // No need to use drop down button
						} else {
							groupitem.Body = RenderDropDownButton(buttonvalue, aPos);
							groupitem.Visible = true;
						}
					}
					if (!UseDropDownButton && UseButtonGroup) { // Render button group
						var visible = false;
						var buttongroups = new Dictionary<string, string>();
						foreach (var item in Items) {
							if (item.Name != GroupOptionName && item.Visible && ew_NotEmpty(item.Body)) {
								if (item.ShowInButtonGroup) {
									visible = true;
									var buttonvalue = (UseImageAndText) ? item.GetImageAndText(item.Body) : item.Body;
									if (!buttongroups.ContainsKey(item.ButtonGroupName))
										buttongroups[item.ButtonGroupName] = "";
									buttongroups[item.ButtonGroupName] += buttonvalue;
								} else if (item.Name == "listactions") { // Show listactions as button group
									item.Body = RenderButtonGroup(item.Body);
								}
							}
						}
						groupitem.Body = "";
						foreach (KeyValuePair<string, string> kvp in buttongroups)
							groupitem.Body += RenderButtonGroup(kvp.Value);
						if (visible)
							groupitem.Visible = true;
					}
				}
				if (ew_NotEmpty(aScriptId)) {
					RenderEx(aPart, aPos, aRowCnt, "block", aScriptId, aScriptClassName); // Original block for ew_ShowTemplates
					RenderEx(aPart, aPos, aRowCnt, "blocknotd", aScriptId);
					RenderEx(aPart, aPos, aRowCnt, "single", aScriptId);
				} else {
					RenderEx(aPart, aPos, aRowCnt, aScriptType, aScriptId, aScriptClassName);
				}
			}

			// Render list options
			public void RenderEx(string aPart, string aPos = "", object aRowCnt = null, string aScriptType = "block", string aScriptId = "", string aScriptClassName = "") {
				RowCnt = Convert.ToString(aRowCnt);
				ScriptType = aScriptType;
				ScriptId = aScriptId;
				ScriptClassName = aScriptClassName;
				JavaScript = "";
				if (ew_NotEmpty(ScriptId)) {
					Tag = (ScriptType == "block") ? "td" : "span";
					if (ScriptType == "block") {
						if (aPart == "header")
							ew_Write("<script id=\"tpoh_" + ScriptId + "\" class=\"" + ScriptClassName + "\" type=\"text/html\">");
						else if (aPart == "body")
							ew_Write("<script id=\"tpob" + RowCnt + "_" + ScriptId + "\" class=\"" + ScriptClassName + "\" type=\"text/html\">");
						else if (aPart == "footer")
							ew_Write("<script id=\"tpof_" + ScriptId + "\" class=\"" + ScriptClassName + "\" type=\"text/html\">");
					} else if (aScriptType == "blocknotd") {
						if (aPart == "header")
							ew_Write("<script id=\"tpo2h_" + ScriptId + "\" class=\"" + ScriptClassName + "\" type=\"text/html\">");
						else if (aPart == "body")
							ew_Write("<script id=\"tpo2b" + RowCnt + "_" + ScriptId + "\" class=\"" + ScriptClassName + "\" type=\"text/html\">");
						else if (aPart == "footer")
							ew_Write("<script id=\"tpo2f_" + ScriptId + "\" class=\"" + ScriptClassName + "\" type=\"text/html\">");
						ew_Write("<span>");
					}
				} else {
					Tag = (ew_NotEmpty(aPos) && aPos != "bottom") ? "td" : "div";
				}
				if (ew_NotEmpty(CustomItem)) {
					cListOption opt = null;
					int cnt = 0;
					foreach (var item in Items) {
						if (ShowItem(item, ScriptId, aPos))
							cnt++;
						if (item.Name == CustomItem)
							opt = item;
					}
					var bUseButtonGroup = UseButtonGroup; // Backup options
					var bUseImageAndText = UseImageAndText;
					UseButtonGroup = true; // Show button group for custom item
					UseImageAndText = true; // Use image and text for custom item
					if (opt != null && cnt > 0) {
						if (ew_NotEmpty(ScriptId) || ShowPos(opt.OnLeft, aPos)) {
							ew_Write(opt.Render(aPart, cnt));
						} else {
							ew_Write(opt.Render("", cnt));
						}
					}
					UseButtonGroup = bUseButtonGroup; // Restore options
					UseImageAndText = bUseImageAndText;
				} else {
					foreach (var item in Items) {
						if (ShowItem(item, ScriptId, aPos))
							ew_Write(item.Render(aPart, 1));
					}
				}
				if ((aScriptType == "block" || aScriptType == "blocknotd") && ew_NotEmpty(aScriptId)) {
					if (aScriptType == "blocknotd")
						ew_Write("</span>");
					ew_Write("</script>");
					if (ew_NotEmpty(JavaScript))
						ew_Write(JavaScript);
				}
			}

			// Show item
			private bool ShowItem(cListOption item, string ScriptId, string Pos)
			{
				var show = item.Visible && (ew_NotEmpty(ScriptId) || ShowPos(item.OnLeft, Pos));
				if (show)
					if (UseDropDownButton)
						show = (item.Name == GroupOptionName || !item.ShowInDropDown);
					else if (UseButtonGroup)
						show = (item.Name == GroupOptionName || !item.ShowInButtonGroup);
				return show;
			}

			// Show position
			private bool ShowPos(bool OnLeft, string Pos)
			{
				return (OnLeft && Pos == "left") || (!OnLeft && Pos == "right") || Pos == "" || Pos == "bottom";
			}

			// Concat options and return concatenated HTML
			// pattern - regular expression pattern for matching the option names, e.g. "^detail_"

			public string Concat(string pattern, string separator = "") {
				var ar = new List<string>();
				foreach (var item in Items) {
					if (Regex.IsMatch(item.Name, pattern) && ew_NotEmpty(item.Body))
						ar.Add(item.Body);
				}
				return String.Join(separator, ar);
			}

			// Merge options to the first option and return it
			// pattern - regular expression pattern for matching the option names, e.g. "^detail_"

			public cListOption Merge(string pattern, string separator = "") {
				cListOption first = null;
				foreach (var item in Items) {
					if (Regex.IsMatch(item.Name, pattern)) {
						if (first == null) {
							first = item;
							first.Body = Concat(pattern, separator);
						} else {
							item.Visible = false;
						}
					}
				}
				return first;
			}

			// Get button group link
			public string RenderButtonGroup(string body) {

				// Get all inputs
				// format: <input type="hidden" ...>

				var inputs = new List<string>();
				foreach (Match match in Regex.Matches(body, @"<input\s+type=['""]hidden['""]\s+([^>]*)>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					inputs.Add(match.Value);
				}

				// Get all buttons
				// format: <div class="btn-group">...</div>

				var btns = new List<string>();
				foreach (Match match in Regex.Matches(body, @"<div\s+class\s*=\s*['""]btn-group['""]([^>]*)>([\s\S]*?)</div\s*>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					btns.Add(match.Value);
				}
				var links = "";

				// Get all links/buttons
				// format: <a ...>...</a> / <button ...>...</button>

				foreach (Match match in Regex.Matches(body, @"<(a|button)([^>]*)>([\s\S]*?)</(a|button)\s*>", RegexOptions.IgnoreCase)) {
					string tag = match.Groups[1].Value, cls = "", attrs = "";
					var m = Regex.Match(match.Groups[2].Value, @"\s+class\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (m.Success) { // Match class="class"
						cls = m.Groups[1].Value;
						attrs = match.Groups[2].Value.Replace(m.Value, "");
					} else {
						attrs = match.Groups[2].Value;
					}
					var caption = match.Groups[3].Value;
					if (!cls.Contains("btn btn-default"))
						cls = ew_PrependClass(cls, "btn btn-default"); // Prepend button classes
					if (ew_NotEmpty(ButtonClass))
						cls = ew_AppendClass(cls, ButtonClass); // Append button classes
					attrs = " class=\"" + cls + "\" " + attrs;
					var link = "<" + tag + attrs + ">" + caption + "</" + tag + ">";
					links += link;
				}
				var btngroup = "";
				if (ew_NotEmpty(links))
					btngroup = "<div class=\"btn-group ewButtonGroup\">" + links + "</div>";
				foreach (var btn in btns)
					btngroup += btn;
				foreach (var input in inputs)
					btngroup += input;
				return btngroup;
			}

			// Render drop down button
			public string RenderDropDownButton(string body, string pos) {

				// Get all inputs
				// format: <input type="hidden" ...>

				var inputs = new List<string>();
				foreach (Match match in Regex.Matches(body, @"<input\s+type=['""]hidden['""]\s+([^>]*)>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					inputs.Add(match.Value);
				}

				// Remove <div class="hide ewPreview">...</div>
				var previewlinks = "";
				foreach (Match match in Regex.Matches(body, @"<div\s+class\s*=\s*['""]hide\s+ewPreview['""]>([\s\S]*?)(<div([^>]*)>([\s\S]*?)</div\s*>)+([\s\S]*?)</div\s*>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					previewlinks += match.Value;
				}

				// Remove toggle button first <button ... data-toggle="dropdown">...</button>
				foreach (Match match in Regex.Matches(body, @"<button\s+([\s\S]*?)data-toggle\s*=\s*['""]dropdown['""]\s*>([\s\S]*?)<\/button\s*>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
				}

				// Get all links/buttons <a ...>...</a> / <button ...>...</button>
				var matches = Regex.Matches(body, @"<(a|button)([^>]*)>([\s\S]*?)</(a|button)\s*>", RegexOptions.IgnoreCase);
				if (matches.Count == 0)
					return "";
				string links = "", submenulink = "", submenulinks = "";
				var submenu = false;
				foreach (Match match in matches) {
					string tag = match.Groups[1].Value, action = "", caption = "", cls = "", attrs = "";
					var actionmatches = Regex.Match(match.Value, @"\s+data-action\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (actionmatches.Success) // Match data-action='action'
						action = actionmatches.Groups[1].Value;
					var submatches = Regex.Match(match.Groups[2].Value, @"\s+class\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (submatches.Success) { // Match class='class'
						cls = Regex.Replace(submatches.Groups[1].Value, @"btn[\S]*\s+", "", RegexOptions.IgnoreCase);
						attrs = match.Groups[2].Value.Replace(submatches.Value, "");
					} else {
						attrs = match.Groups[2].Value;
					}
					attrs = Regex.Replace(attrs, @"\s+title\s*=\s*['""]([\s\S]*?)['""]", "", RegexOptions.IgnoreCase); // Remove title='title'
					submatches = Regex.Match(attrs, @"\s+data-caption\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (submatches.Success) // Match data-caption='caption'
						caption = submatches.Groups[1].Value;
					attrs = " class=\"" + cls + "\" " + attrs;
					if (ew_SameText(tag, "button")) // Add href for button
						attrs += " href=\"javascript:void(0);\"";
					if (UseImageAndText) { // Image and text
						var submatch = Regex.Match(match.Groups[3].Value, @"<img([^>]*)>", RegexOptions.IgnoreCase); // <img> tag
						if (submatch.Success)
							caption = submatch.Value + "&nbsp;&nbsp;" + caption;
						submatch = Regex.Match(match.Groups[3].Value, @"<span([^>]*)>([\s\S]*?)<\/span\s*>", RegexOptions.IgnoreCase); // <span class='class'></span> tag
						if (submatch.Success)
							if (Regex.IsMatch(submatch.Groups[1].Value, @"\s+class\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase)) // Match class='class'
								caption = submatch.Value + "&nbsp;&nbsp;" + caption;
					}
					if (ew_Empty(caption))
						caption = match.Groups[3].Value;
					string link = "<a" + attrs + ">" + caption + "</a>";
					if (action == "list") { // Start new submenu
						if (submenu) { // End previous submenu
							if (ew_NotEmpty(submenulinks)) { // Set up submenu
								links += "<li class=\"dropdown-submenu\">" + submenulink + "<ul class=\"dropdown-menu\">" + submenulinks + "</ul></li>";
							} else {
								links += "<li>" + submenulink + "</li>";
							}
						}
						submenu = true;
						submenulink = link;
						submenulinks = "";
					} else {
						if (ew_Empty(action) && submenu) { // End previous submenu
							if (ew_NotEmpty(submenulinks)) { // Set up submenu
								links += "<li class=\"dropdown-submenu\">" + submenulink + "<ul class=\"dropdown-menu\">" + submenulinks + "</ul></li>";
							} else {
								links += "<li>" + submenulink + "</li>";
							}
							submenu = false;
						}
						if (submenu)
							submenulinks += "<li>" + link + "</li>";
						else
							links += "<li>" + link + "</li>";
					}
				}
				var btndropdown = "";
				if (ew_NotEmpty(links)) {
					if (submenu) { // End previous submenu
						if (ew_NotEmpty(submenulinks)) { // Set up submenu
							links += "<li class=\"dropdown-submenu\">" + submenulink + "<ul class=\"dropdown-menu\">" + submenulinks + "</ul></li>";
						} else {
							links += "<li>" + submenulink + "</li>";
						}
					}
					var btnclass = "dropdown-toggle btn btn-default";
					if (ew_NotEmpty(ButtonClass))
						btnclass = ew_AppendClass(btnclass, ButtonClass);
					var buttontitle = ew_HtmlTitle(DropDownButtonPhrase);
					buttontitle = (DropDownButtonPhrase != buttontitle) ? " title=\"" + buttontitle + "\"" : "";
					var button = "<button class=\"" + btnclass + "\"" + buttontitle + " data-toggle=\"dropdown\">" + DropDownButtonPhrase + " <b class=\"caret\"></b></button><ul class=\"dropdown-menu " + ((pos == "right") ? "dropdown-menu-right " : "") + "ewMenu\">" + links + "</ul>";
					if (pos == "bottom") // Use dropup
						btndropdown = "<div class=\"btn-group dropup ewButtonDropdown\">" + button + "</div>";
					else
						btndropdown = "<div class=\"btn-group ewButtonDropdown\">" + button + "</div>";
				}
				foreach (var input in inputs)
					btndropdown += input;
				btndropdown += previewlinks;
				return btndropdown;
			}

			// Hide detail items for dropdown
			public void HideDetailItemsForDropDown() {
				var showdtl = false;
				if (UseDropDownButton) {
					foreach (var item in Items) {
						if (item.Name != GroupOptionName && item.Visible && item.ShowInDropDown && !item.Name.StartsWith("detail_")) {
							showdtl = true;
							break;
						}
					}
				}
				if (!showdtl) {
					foreach (var item in Items) {
						if (item.Name.StartsWith("detail_"))
							item.Visible = false;
					}
				}
			}
		}

		//
		// List option class
		//

		public class cListOption
		{
			public string Name = "";
			public bool OnLeft = false;
			public string CssStyle = "";
			public string CssClass = "";
			public bool Visible = true;
			public string Header = "";
			public string Body = "";
			public string Footer = "";
			public string Tag = "td";
			public string Separator = "";
			public cListOptions Parent;
			public bool ShowInButtonGroup = true;
			public bool ShowInDropDown = true;
			public string ButtonGroupName = "_default";

			// Constructor
			public cListOption(string aName)
			{
				Name = aName;
			}

			// Clear
			public void Clear() {
				Body = "";
			}

			// Move
			public void MoveTo(int Pos) {
				Parent.MoveItem(Name, Pos);
			}

			// Render
			public string Render(string Part, int ColSpan = 1) {
				var tagclass = Parent.TagClassName;
				var value = "";
				if (Part == "header") {
					if (tagclass == "") tagclass = "ewListOptionHeader";
					value = Header;
				} else if (Part == "body") {
					if (tagclass == "") tagclass = "ewListOptionBody";
					if (Parent.Tag != "td")
						tagclass = ew_AppendClass(tagclass, "ewListOptionSeparator");
					value = Body;
				} else if (Part == "footer") {
					if (tagclass == "") tagclass = "ewListOptionFooter";
					value = Footer;
				} else {
					value = Part;
				}
				if (ew_Empty(value) && Parent.Tag == "span" && ew_Empty(Parent.ScriptId))
					return "";
				var res = (ew_NotEmpty(value)) ? value : "&nbsp;";
				tagclass = ew_AppendClass(tagclass, CssClass);
				var attrs = new cAttributes() {{"class", tagclass}, {"style", CssStyle}, {"data-name", Name}}; // DN
				if (ew_SameText(Parent.Tag, "td") && Parent.RowSpan > 1)
					attrs["rowspan"] = Convert.ToString(Parent.RowSpan);
				if (ew_SameText(Parent.Tag, "td") && ColSpan > 1)
					attrs["colspan"] = Convert.ToString(ColSpan);
				var name = Parent.TableVar + "_" + Name;
				if (Name != Parent.GroupOptionName) {
					if (!(new List<string>() {"checkbox", "rowcnt"}).Contains(Name)) {
						if (Parent.UseImageAndText)
							res = GetImageAndText(res);
						if (Parent.UseButtonGroup && ShowInButtonGroup) {
							res = Parent.RenderButtonGroup(res);
							if (OnLeft && ew_SameText(Parent.Tag, "td") && ColSpan > 1)
								res = "<div style=\"text-align: right\">" + res + "</div>";
						}
					}
					if (Part == "header")
						res = "<span id=\"elh_" + name + "\" class=\"" + name + "\">" + res + "</span>";
					else if (Part == "body")
						res = "<span id=\"el" + Parent.RowCnt + "_" + name + "\" class=\"" + name + "\">" + res + "</span>";
					else if (Part == "footer")
						res = "<span id=\"elf_" + name + "\" class=\"" + name + "\">" + res + "</span>";
				}

				// remove string tag = (Parent.Tag == "td" && Part == "header") ? "th" : Parent.Tag;
				var tag = (Parent.Tag == "td" && Part == "header") ? "th" : Parent.Tag;
				if (Parent.UseButtonGroup && ShowInButtonGroup)
					attrs["style"] += "white-space: nowrap;";
				res = ew_HtmlElement(tag, attrs, res);
				if (ew_NotEmpty(Parent.ScriptId)) {
					var js = ew_ExtractScript(ref res, Parent.ScriptClassName + "_js");
					if (Parent.ScriptType == "single") {
						if (Part == "header")
							res = "<script id=\"tpoh_" + Parent.ScriptId + "_" + Name + "\" type=\"text/html\">" + res + "</script>";
						else if (Part == "body")
							res = "<script id=\"tpob" + Parent.RowCnt + "_" + Parent.ScriptId + "_" + Name + "\" type=\"text/html\">" + res + "</script>";
						else if (Part == "footer")
							res = "<script id=\"tpof_" + Parent.ScriptId + "_" + Name + "\" type=\"text/html\">" + res + "</script>";
					}
					if (ew_NotEmpty(js))
						if (Parent.ScriptType == "single")
							res += js;
						else
							Parent.JavaScript += js;
				}
				return res;
			}

			// Get image and text link
			public string GetImageAndText(string body) {
				foreach (Match match in Regex.Matches(body, @"<a([^>]*)>([\s\S]*?)</a\s*>", RegexOptions.IgnoreCase)) {
					var submatch = Regex.Match(match.Groups[1].Value, @"\s+data-caption\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (submatch.Success) { // Match data-caption='caption'
						var caption = submatch.Groups[1].Value;
						if (Regex.Match(match.Groups[2].Value, @"<img([^>]*)>", RegexOptions.IgnoreCase).Success) // Image and text
							body = body.Replace(match.Groups[2].Value, match.Groups[2].Value + "&nbsp;&nbsp;" + caption);
					}
				}
				return body;
			}
		}

		// List actions
		public class cListActions {
			public Dictionary<string, cListAction> Items = new Dictionary<string, cListAction>();

			// Add and return a new option
			public cListAction Add(string Name, object Action, bool Allow = true, string Method = EW_ACTION_POSTBACK, string Select = EW_ACTION_MULTIPLE, string ConfirmMsg = "", string Icon = "glyphicon glyphicon-star ewIcon", string Success = "") {
				cListAction item;
				if (Action is string) {
					item = new cListAction(Name, Convert.ToString(Action), Allow, Method, Select, ConfirmMsg, Icon, Success);
					Items[Name] = item;
				} else {
					item = (cListAction)Action;
					Items[Name] = item;
				}
				return item;
			}

			// Get item by name
			public cListAction GetItem(string Name) {
				return Items.ContainsKey(Name) ? Items[Name] : null;
			}

			// Indexer
			public cListAction this[string index] {
				get {
					return GetItem(index);
				}
			}
		}

		// List action
		public class cListAction {
			public string Action = "";
			public string Caption = "";
			public bool Allow = true;
			public string Method = EW_ACTION_POSTBACK; // Post back (p) / Ajax (a)
			public string Select = EW_ACTION_MULTIPLE; // Multiple (m) / Single (s)
			public string ConfirmMsg = "";
			public string Icon = "glyphicon glyphicon-star ewIcon"; // Icon
			public string Success = ""; // JavaScript callback function name

			// Constructor
			public cListAction(string action, string caption, bool allow = true, string method = EW_ACTION_POSTBACK, string select = EW_ACTION_MULTIPLE, string confirmMsg = "", string icon = "glyphicon glyphicon-star ewIcon", string success = "") {
				Action = action;
				Caption = caption;
				Allow = allow;
				Method = method;
				Select = select;
				ConfirmMsg = confirmMsg;
				Icon = icon;
				Success = success;
			}

			// To JSON
			public string ToJson(bool htmlencode = false) {
				var ar = new Dictionary<string, string>() {{"msg", ConfirmMsg}, {"action", Action}, {"method", Method}, {"select", Select}, {"success", Success}};
				var json = ew_ArrayToJson(ar);
				if (htmlencode)
					json = ew_HtmlEncode(json);
				return json;
			}
		}

		// Sub pages
		public class cSubPages {
			public bool Justified = false;
			public string Style = ""; // "tabs" or "pills" or "" (panels)
			public Dictionary<object, cSubPage> Items = new Dictionary<object, cSubPage>();
			public List<string> ValidKeys = new List<string>();

			// Get nav style
			public string NavStyle {
				get {
					string style = " nav-" + Style;
					if (Justified)
						style += " nav-justified";
					return style;
				}
			}

			// Get tab style
			public string TabStyle(object k) {
				var item = GetItem(k);
				string style = "";
				if (ew_SameStr(ActivePageIndex, k)) {
					style = "active";
				} else if (item != null) {
					if (!item.Visible)
						style = "hidden ewHidden";
					else if (item.Disabled && Style != "")
						style = "disabled ewDisabled";
				}
				return (style != "") ? " class=\"" + style + "\"" : "";
			}

			// Get page style
			public string PageStyle(object k) {
				if (ew_SameStr(ActivePageIndex, k))
					if (Style == "")
						return " in";
					else
						return " active";
				var item = GetItem(k);
				if (item != null) {
					if (!item.Visible)
						return " hidden ewHidden";
					else if (item.Disabled && Style != "")
						return " disabled ewDisabled";
				}
				return "";
			}

			// Get count
			public int Count {
				get {
					return Items.Count;
				}
			}

			// Add item by name
			public cSubPage Add(object k) {
				var item = new cSubPage();
				if (ew_NotEmpty(k))
					Items.Add(k, item);
				return item;
			}

			// Get item by key
			public cSubPage GetItem(object k) {
				return Items.ContainsKey(k) ? Items[k] : null;
			}

			// Active page index
			public object ActivePageIndex {
				get {

					// Return first active page
					foreach (KeyValuePair<object, cSubPage> kvp in Items)
						if ((ValidKeys.Count == 0 || ValidKeys.Contains(kvp.Key)) && kvp.Value.Visible && !kvp.Value.Disabled && kvp.Value.Active && (!ew_IsNumeric(kvp.Key) || ew_ConvertToInt(kvp.Key) != 0))
							return kvp.Key;

					// If not found, return first visible page
					foreach (KeyValuePair<object, cSubPage> kvp in Items)
						if ((ValidKeys.Count == 0 || ValidKeys.Contains(kvp.Key)) && kvp.Value.Visible && !kvp.Value.Disabled && (!ew_IsNumeric(kvp.Key) || ew_ConvertToInt(kvp.Key) != 0))
							return kvp.Key;

					// Not found
					return null;
				}
			}

			// Indexer
			public cSubPage this[object index] {
				get {
					return GetItem(index);
				}
			}
		}

		// Sub page
		public class cSubPage {
			public bool Active = false;
			public bool Visible = true; // If false, add class "hidden ewHidden" to the li or div.panel
			public bool Disabled = false; // If true, add class "disabled ewDisabled" to the li (for tabs only, panels cannot be disabled)
		}

		//
		// CSS parser
		//

		public class cCssParser
		{
			public Dictionary<string, Dictionary<string, string>> css;

			// Constructor
			public cCssParser()
			{
				css = new Dictionary<string, Dictionary<string, string>>();
			}

			// Clear all styles
			public void Clear()
			{
				foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
					kvp.Value.Clear();
				css.Clear();
			}

			// add a section
			public void Add(string key, string codestr)
			{
				key = key.ToLower().Trim();
				if (key == "")
					return;
				codestr = codestr.ToLower();
				if (!css.ContainsKey(key))
					css[key] = new Dictionary<string, string>();
				string[] codes = codestr.Split(';');
				if (codes.Length > 0)
				{
					foreach (string code in codes)
					{
						string sCode = code.ToLower();
						string[] arCode = code.Split(':');
						string codekey = arCode[0];
						string codevalue = "";
						if (arCode.Length > 1)
							codevalue = arCode[1];
						if (codekey.Length > 0)
						{
							css[key][codekey.Trim()] = codevalue.Trim();
						}
					}
				}
			}

			// explode a string into two
			private void Explode(string str, char sep, ref string str1, ref string str2)
			{
				string[] ar = str.Split(sep);
				str1 = ar[0];
				if (ar.Length > 1)
					str2 = ar[1];
			}

			// Get a style
			public string Get(string key, string property)
			{
				key = key.ToLower();
				property = property.ToLower();
				string tag = "", subtag = "", cls = "", id = "";
				Explode(key, ':', ref tag, ref subtag);
				Explode(tag, '.', ref tag, ref cls);
				Explode(tag, '#', ref tag, ref id);
				string result = "";
				foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
				{
					string _tag = kvp.Key;
					Dictionary<string, string> value = kvp.Value;
					string _subtag = "", _cls = "", _id = "";
					Explode(_tag, ':', ref _tag, ref _subtag);
					Explode(_tag, '.', ref _tag, ref _cls);
					Explode(_tag, '#', ref _tag, ref _id);
					bool tagmatch = (tag == _tag || _tag.Length == 0);
					bool subtagmatch = (subtag == _subtag || _subtag.Length == 0);
					bool classmatch = (cls == _cls || _cls.Length == 0);
					bool idmatch = (id == _id);
					if (tagmatch && subtagmatch && classmatch && idmatch)
					{
						string temp = _tag;
						if (temp.Length > 0 && _cls.Length > 0)
						{
							temp += "." + _cls;
						}
						else if (temp.Length == 0)
						{
							temp = "." + _cls;
						}
						if (temp.Length > 0 && _subtag.Length > 0)
						{
							temp += ":" + _subtag;
						}
						else if (temp.Length == 0)
						{
							temp = ":" + _subtag;
						}
						if (css[temp].ContainsKey(property))
							result = css[temp][property];
					}
				}
				return result;
			}

			// Get section as dictionary
			public Dictionary<string, string> GetSection(string key)
			{
				key = key.ToLower();
				string tag = "", subtag = "", cls = "", id = "";
				Explode(key, ':', ref tag, ref subtag);
				Explode(tag, '.', ref tag, ref cls);
				Explode(tag, '#', ref tag, ref id);
				Dictionary<string, string> result = new Dictionary<string, string>();
				foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
				{
					string _tag = kvp.Key;
					Dictionary<string, string> value = kvp.Value;
					string _subtag = "", _cls = "", _id = "";
					Explode(_tag, ':', ref _tag, ref _subtag);
					Explode(_tag, '.', ref _tag, ref _cls);
					Explode(_tag, '#', ref _tag, ref _id);
					bool tagmatch = (tag == _tag || _tag.Length == 0);
					bool subtagmatch = (subtag == _subtag || _subtag.Length == 0);
					bool classmatch = (cls == _cls || _cls.Length == 0);
					bool idmatch = (id == _id);
					if (tagmatch && subtagmatch && classmatch && idmatch)
					{
						string temp = _tag;
						if (temp.Length > 0 && _cls.Length > 0)
						{
							temp += "." + _cls;
						}
						else if (temp.Length == 0)
						{
							temp = "." + _cls;
						}
						if (temp.Length > 0 && _subtag.Length > 0)
						{
							temp += ":" + _subtag;
						}
						else if (temp.Length == 0)
						{
							temp = ":" + _subtag;
						}
						foreach (KeyValuePair<string, string> kv in css[temp])
						{
							result[kv.Key] = kv.Value;
						}
					}
				}
				return result;
			}

			// Get section as string
			public string GetSectionString(string key)
			{
				Dictionary<string, string> dict = GetSection(key);
				string result = "";
				foreach (KeyValuePair<string, string> kv in dict)
					result += kv.Key + ":" + kv.Value + ";"; // no spaces
				return result;
			}

			// Parse string
			public bool ParseStr(string str)
			{
				Clear();

				// Remove comments
				str = Regex.Replace(str, @"\/\*(.*)?\*\/", "");

				// Parse the csscode
				string[] parts = str.Split('}');
				if (parts.Length > 0)
				{
					foreach (string part in parts)
					{
						string keystr = "", codestr = "";
						Explode(part, '{', ref keystr, ref codestr);
						string[] keys = keystr.Split(',');
						if (keys.Length > 0)
						{
							foreach (string akey in keys)
							{
								string key = akey;
								if (key.Length > 0)
								{
									key = key.Replace("\n", "");
									key = key.Replace("\\", "");
									Add(key, codestr.Trim());
								}
							}
						}
					}
				}
				return (css.Count > 0);
			}

			// Parse a stylesheet
			public bool ParseFile(string filename)
			{
				Clear();
				if (File.Exists(filename))
				{
					return ParseStr(File.ReadAllText(filename));
				}
				else
				{
					return false;
				}
			}

			// Get CSS string
			public string GetCSS()
			{
				string result = "";
				foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css)
				{
					result += kvp.Key + " {\n";
					foreach (KeyValuePair<string, string> kv in kvp.Value)
					{
						result += "\t" + kv.Key + ": " + kv.Value + ";\n";
					}
					result += "}\n\n";
				}
				return result;
			}
		}

		// Get connection object
		public static dynamic ew_GetConn(string dbid = "DB") { // DN
			var db = Db(dbid);
			if (db != null && !Connections.ContainsKey(dbid))
				Connections[dbid] = ew_CreateConn(dbid);
			if (Connections.ContainsKey(dbid))
				return Connections[dbid];
			return null;
		}

		// DbHelper (alias)
		public static dynamic DbHelper(string dbid = "DB") { // DN
			return ew_GetConn(dbid);
		}

		// Get database object
		public static Dictionary<string, string> Db(string dbid = "DB") {
			if (ew_Empty(dbid))
				dbid = "DB";
			if (EW_DB.ContainsKey(dbid))
				return EW_DB[dbid];
			return null;
		}

		// Get connection type
		public static string ew_GetConnectionType(string dbid = "DB") {
			var db = Db(dbid);
			if (db != null)
				return db["type"];
			return "";
		}

		// Create a new connection
		public static dynamic ew_CreateConn(string dbid = "DB") {
			var db = Db(dbid);
			var dbtype = db["type"];
			dynamic c = null;
			if (ew_SameText(dbtype, "MSSQL")) {
				c = ew_CreateInstance("cConnection`4", new object[] {dbid}, new Type[] {typeof(SqlConnection), typeof(SqlCommand), typeof(SqlDataReader), typeof(SqlDbType)}) ??
					new cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>();
			}
			return c;
		}

		// Create Advanced Security
		public static dynamic ew_CreateSecurity() {
			dynamic s = ew_CreateInstance("cAdvancedSecurity") ?? new cAdvancedSecurityBase();
			return s;
		}

		// Get last insert ID SQL // DN
		public static string ew_GetLastInsertIdSql(string dbid = "DB") {
			var db = Db(dbid);
			var dbtype = db["type"];
			if (ew_SameText(dbtype, "MYSQL")) {
				return "SELECT LAST_INSERT_ID()";
			} else if (ew_SameText(dbtype, "ACCESS") || ew_SameText(dbtype, "MSSQL")) {
				return "SELECT @@Identity";
			} else {
				return "";
			}
		}

		// Get SQL parameter symbol // DN
		public static string ew_GetSqlParamSymbol(string dbid = "DB") {
			var db = Db(dbid);
			var dbtype = db["type"];
			if (ew_SameText(dbtype, "MYSQL") || ew_SameText(dbtype, "MSSQL")) {
				return "@";
			} else if (ew_SameText(dbtype, "POSTGRESQL") || ew_SameText(dbtype, "ORACLE")) {
				return ":";
			} else {
				return "?";
			}
		}

		// Close database connections
		public static void ew_CloseConn() {
			foreach (KeyValuePair<string, dynamic> kvp in Connections)
				kvp.Value?.Close();
			Connections.Clear();
			Conn?.Close();
			Conn = null;
		}

		// Get a row as OrderedDictionary from data reader // DN
		public static OrderedDictionary ew_GetRow(DbDataReader dr) {
			if (dr != null) {
				var od = new OrderedDictionary();
				for (int i = 0; i < dr.FieldCount; i++) {
					try {
						if (ew_NotEmpty(dr.GetName(i))) {
							od[dr.GetName(i)] = dr[i];
						} else {
							od[Convert.ToString(i)] = dr[i]; // Convert index to string as key
						}
					} catch {}
				}
				return od;
			}
			return null;
		}

		//
		// Connection object
		//

		public class cConnectionBase<N, M, R, T> : IDisposable
			where N : DbConnection
			where M : DbCommand
			where R : DbDataReader
		{
			public string ConnectionString;
			public N Conn;
			private N _Conn;
			private DbTransaction Trans;
			private List<M> _Command = new List<M>();
			private List<R> _DataReader = new List<R>();
			public Dictionary<string, string> Info;
			public string DBID;

			// Constructor
			public cConnectionBase(string dbid)
			{
				Init(dbid);
			}

			// Constructor
			public cConnectionBase(): this("DB")
			{
			}

			// Init
			public virtual void Init(string dbid) {
				Info = Db(dbid);
				if (Info != null) {
					DBID = Info["id"];
					ConnectionString = Info["connectionstring"];
					Conn = OpenConnection();
				}
			}

			// Get data type
			public T GetDataType(object dt) {
				return (T)dt;
			}

			// Access
			public bool IsAccess {
				get {
					return typeof(N).ToString().Contains(".OleDbConnection");
				}
			}

			// Microsoft SQL Server
			public bool IsMsSQL {
				get {
					return typeof(N).ToString().Contains(".SqlConnection");
				}
			}

			// Microsoft SQL Server >= 2012
			private bool? _sql2012 = null;
			public bool IsMsSQL2012 {
				get {
					if (!IsMsSQL)
						return false;
					if (!_sql2012.HasValue) {
						var m = Regex.Match(Convert.ToString(ExecuteScalar("SELECT @@version")), @"Microsoft SQL Server (\d+)");
						_sql2012 = m.Success && Convert.ToInt32(m.Groups[1].Value) >= 2012;
					}
					return _sql2012 == true;
				}
			}

			// MySQL
			public bool IsMySQL {
				get {
					return typeof(N).ToString().Contains(".MySqlConnection");
				}
				}

			// PostgreSQL
			public bool IsPostgreSQL {
				get {
					return typeof(N).ToString().Contains(".NpgsqlConnection");
				}
			}

			// Oracle
			public bool IsOracle {
				get {
					return typeof(N).ToString().Contains(".OracleConnection");
				}
			}

			// Select limit
			public R SelectLimit(string sql, int nrows = -1, int offset = -1, bool hasOrderBy = false) {
				string sOffset, sLimit, sSql = sql;
				if (IsMsSQL) {
					if (IsMsSQL2012) { // Microsoft SQL Server >= 2012
						if (!hasOrderBy)
							sql += " ORDER BY @@version"; // Dummy ORDER BY clause
						if (offset > -1)
							sql += " OFFSET " + Convert.ToString(offset) + " ROWS";
						if (nrows > 0) {
							if (offset < 0)
								sql += " OFFSET 0 ROWS";
							sql += " FETCH NEXT " + Convert.ToString(nrows) + " ROWS ONLY";
						}
					} else { // Select top
						if (nrows > 0) {
							if (offset > 0)
								nrows += offset;
							sql = Regex.Replace(sql, @"(^\s*select\s+(distinct)?)", @"$1 TOP " + Convert.ToString(nrows) + " ", RegexOptions.IgnoreCase); // DN
						}
					}
				} else if (IsMySQL) {
					sOffset = (offset >= 0) ? Convert.ToString(offset) + "," : "";
					sLimit = (nrows < 0) ? "18446744073709551615" : Convert.ToString(nrows);
					sql += " LIMIT " + sOffset + sLimit;
				} else if (IsPostgreSQL) {
					sOffset = (offset >= 0) ? " OFFSET " + Convert.ToString(offset) : "";
					sLimit = (nrows >= 0) ? " LIMIT " + Convert.ToString(nrows) : "";
					sql += sLimit + sOffset;
				} else if (IsOracle) { // Select top
					if (nrows > 0) {
						if (offset > 0)
							nrows += offset;
						sql = "select * from (" + sql + ") where rownum <= " + Convert.ToString(nrows);
					}
				} else if (IsAccess) { // Select top
					if (nrows > 0) {
						if (offset > 0)
							nrows += offset;
						sql = Regex.Replace(sql, @"(^\s*select\s+(distinctrow|distinct)?)", @"$1 TOP " + Convert.ToString(nrows) + " ", RegexOptions.IgnoreCase); // DN
					}
				}
				if (!SelectOffset) {
					return GetDataReader(sql) ?? GetDataReader(sSql); // If SQL fails due to being too complex, use original SQL.
				} else {
					return GetDataReader(sql);
				}
			}

			// Supports select offset
			public bool SelectOffset {
				get {
					return IsMySQL || IsPostgreSQL || IsMsSQL2012;
				}
			}

			// Execute SQL for the main connection
			public int Execute(string sql)
			{
				using (var cmd = GetCommand(sql))
					return cmd.ExecuteNonQuery();
			}

			// Execute SQL for a specified connection
			public int Execute(string sql, DbConnection c)
			{
				using (var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, c }))
					return cmd.ExecuteNonQuery();
			}

			// Execute SQL
			public int ExecuteNonQuery(string sql)
			{
				using (var cmd = OpenCommand(sql)) // Use new connection
					return cmd.ExecuteNonQuery();
			}

			// Execute SQL and return first value of first row
			public object ExecuteScalar(string sql, bool main = false)
			{
				if (main) {
					using (var cmd = GetCommand(sql)) // Use main connection
						return cmd.ExecuteScalar();
				} else {
					using (var cmd = OpenCommand(sql)) // Use new connection
						return cmd.ExecuteScalar();
				}
			}

			// Execute the query, and return the row(s) as JSON
			public string ExecuteJson(string sql, bool firstOnly = true, bool main = false) {
				if (firstOnly) {
					var row = GetRow(sql, main);
					return (row != null) ? ew_ArrayToJson(row) : "false";
				} else {
					var ar = GetRows(sql, main);
					return (ar != null) ? ew_ArrayToJson(ar) : "false";
				}
			}

			// Get last insert ID
			public object GetLastInsertId()
			{
				var sql = ew_GetLastInsertIdSql(DBID);
				if (sql != "") {
					using (var cmd = GetCommand(sql)) // Use main connection
						return cmd.ExecuteScalar();
				}
				return System.DBNull.Value;
			}

			// Get data reader
			public R GetDataReader(string sql)
			{
				try {
					var cmd = GetCommand(sql); // Use main connection
					return (R)cmd.ExecuteReader();
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
					return null;
				}
			}

			// Get a new connection
			public virtual N OpenConnection()
			{
				var connstr = Info["connectionstring"];
				if (IsAccess) {
					var relpath = Info["relpath"];
					var dbname = Info["dbname"];
					if (ew_Empty(relpath)) {
						AppDomain.CurrentDomain.SetData("DataDirectory", ew_AppRoot()); // Use wwwroot by default
						connstr += @"|DataDirectory|" + dbname;
					} else if (relpath.StartsWith(@"\\") || relpath.Contains(@":\")) { // Physical path
						connstr += relpath + dbname;
					} else { // Relative to wwwroot
						connstr += ew_ServerMapPath(relpath) + dbname;
					}
					ConnectionString = connstr;
				}
				Database_Connecting(ref connstr);
				var c = (N)Activator.CreateInstance(typeof(N), new object[] { connstr });
				c.Open();
				if (IsOracle) {
					if (Info["schema"] != "")
						Execute("ALTER SESSION SET CURRENT_SCHEMA = " + ew_QuotedName(Info["schema"], Info["id"]), c); // Set current schema
					Execute("ALTER SESSION SET NLS_TIMESTAMP_FORMAT = 'yyyy-mm-dd hh24:mi:ss'", c);
					Execute("ALTER SESSION SET NLS_TIMESTAMP_TZ_FORMAT = 'yyyy-mm-dd hh24:mi:ss'", c);
				} else if (IsMsSQL && EW_DATE_FORMAT_ID > 0) { // DN
					Execute("SET DATEFORMAT ymd", c);
				} else if (IsPostgreSQL && Info["schema"] != "") {
					Execute("SET search_path TO " + ew_QuotedName(Info["schema"], Info["id"]), c); // Set current schema
				}
				Database_Connected(c);
				return c;
			}

			// Get command
			public M GetCommand(string sql)
			{
				if (EW_DEBUG_ENABLED)
					ew_SetDebugMsg(sql);
				var Cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, Conn });
				if (Trans != null)
					Cmd.Transaction = Trans;
				return Cmd;
			}

			// Get a new command
			public M OpenCommand(string sql)
			{
				try {
					_Conn = _Conn ?? OpenConnection(); // Use secondary connection
					if (EW_DEBUG_ENABLED)
						ew_SetDebugMsg(sql);
					var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, _Conn });
					_Command.Add(cmd);
					return cmd;
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
					return null;
				}
			}

			// Get command for stored procedure
			public M GetStoredProcCommand(string name)
			{
				if (EW_DEBUG_ENABLED)
					ew_SetDebugMsg(name);
				var Cmd = (M)Activator.CreateInstance(typeof(M), new object[] { name, Conn });
				Cmd.CommandType = CommandType.StoredProcedure;
				if (Trans != null)
					Cmd.Transaction = Trans;
				return Cmd;
			}

			// Get a new command
			public M OpenStoredProcCommand(string name)
			{
				try {
					_Conn = _Conn ?? OpenConnection(); // Use secondary connection
					if (EW_DEBUG_ENABLED)
						ew_SetDebugMsg(name);
					var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { name, _Conn });
					cmd.CommandType = CommandType.StoredProcedure;
					_Command.Add(cmd);
					return cmd;
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
					return null;
				}
			}

			// Get a new data reader
			public R OpenDataReader(string sql)
			{
				try {
					var cmd = OpenCommand(sql); // Use secondary connection
					var r = (R)cmd.ExecuteReader();
					_DataReader.Add(r);
					return r;
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
					return null;
				}
			}

			// Get a row as OrderedDictionary from data reader
			public OrderedDictionary GetRow(DbDataReader dr)
			{
				return ew_GetRow(dr);
			}

			// Get a row as OrderedDictionary by SQL
			public OrderedDictionary GetRow(string sql, bool main = false)
			{
				using (var dr = (main) ? GetDataReader(sql) : OpenDataReader(sql)) {
					if (dr != null && dr.Read())
						return GetRow(dr);
				}
				return null;
			}

			// Get a row as string[] from data reader
			public string[] GetRow2(DbDataReader dr)
			{
				if (dr != null && dr.FieldCount > 0) {
					var ar = new string[dr.FieldCount];
					for (int i = 0; i < dr.FieldCount; i++)
						ar[i] = Convert.ToString(dr[i]);
					return ar;
				}
				return null;
			}

			// Get rows as List<OrderedDictionary>
			public List<OrderedDictionary> GetRows(DbDataReader dr)
			{
				if (dr != null) {
					var rows = new List<OrderedDictionary>();
					while (dr.Read())
						rows.Add(GetRow(dr));
					dr.Close();
					dr.Dispose();
					return rows;
				}
				return null;
			}

			// Get rows as List<OrderedDictionary> by SQL
			public List<OrderedDictionary> GetRows(string sql, bool main = false)
			{
				var dr = (main) ? GetDataReader(sql) : OpenDataReader(sql);
				return GetRows(dr);
			}

			// Get rows as List<string[]> by SQL
			public List<string[]> GetRows2(string sql)
			{
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						var rows = new List<string[]>();
						while (dr.Read())
							rows.Add(GetRow2(dr));
						return rows;
					}
				}
				return null;
			}

			// Get rows as List<DbDataRecord> by SQL
			public List<DbDataRecord> GetRecords(string sql)
			{
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						var list = new List<DbDataRecord>();
						foreach (DbDataRecord record in dr)
							list.Add(record);
						return list;
					}
				}
				return null;
			}

			// Get first row as DbDataRecord by SQL
			public DbDataRecord GetRecord(string sql)
			{
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						foreach (DbDataRecord r in dr)
							return r;
					}
				}
				return null;
			}

			// Get count (by dataset)
			public int GetCount(string sql)
			{
				int cnt = 0;
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						while (dr.Read())
							cnt++;
					}
				}
				return cnt;
			}

			// Begin transaction
			public void BeginTrans()
			{
				try {
					if (IsOracle)
						Trans = Conn.BeginTransaction();
					else
						Trans = Conn.BeginTransaction(IsolationLevel.ReadUncommitted);
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
				}
			}

			// Commit transaction
			public void CommitTrans()
			{
				try {
					Trans?.Commit();
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
				}
			}

			// Rollback transaction
			public void RollbackTrans()
			{
				try {
					Trans?.Rollback();
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
				}
			}

			// Concat // DN
			public string Concat(params string[] list)
			{
				if (IsAccess) {
					return String.Join(" & ", list);
				} else if (IsMsSQL) {
					return String.Join(" + ", list);
				} else if (IsOracle || IsPostgreSQL) {
					return String.Join(" || ", list);
				} else {
					return "CONCAT(" + String.Join(", ", list) + ")";
				}
			}

			// Table CSS class name
			public string TableClass = "table table-bordered table-striped ewDbTable";

			// Get result in HTML table by SQL (see below for options)
			public IHtmlContent ExecuteHtml(string sql, Dictionary<string, object> options = null) {
				using (var rs = OpenDataReader(sql)) {
					return ExecuteHtml(rs);
				}
			}

			// Get result in HTML table by DbCommand (see below for options)
			public IHtmlContent ExecuteHtml(DbCommand cmd, Dictionary<string, object> options = null) {
				return ExecuteHtml(cmd.ExecuteReader(), options);
			}

			// Get result in HTML table by DbDataReader
			// options(Dictionary<string, object>):
			// - fieldcaption(bool|Dictionary<string, string>)
			// - horizontal(bool)
			// - tablename(string|List<string>)
			// - tableclass(string)

			public IHtmlContent ExecuteHtml(DbDataReader rs, Dictionary<string, object> options = null) {
				if (rs == null || !rs.HasRows || rs.FieldCount < 1)
					return null;
				options = options ?? new Dictionary<string, object>();
				var horizontal = options.ContainsKey("horizontal") && Convert.ToBoolean(options["horizontal"]);
				string html = "", vhtml = "";
				int cnt = 0;
				string classname = (options.ContainsKey("tableclass") && ew_NotEmpty(options["tableclass"])) ? Convert.ToString(options["tableclass"]) : TableClass;
				if (rs.Read()) { // First row
					cnt++;

					// Vertical table
					vhtml = "<table class=\"" + classname + "\"><tbody>";
					for (var i = 0; i < rs.FieldCount; i++) {
						vhtml += "<tr>";
						vhtml += "<td>" + GetFieldCaption(rs.GetName(i), options) + "</td>";
						vhtml += "<td>" + Convert.ToString(rs[i]) + "</td></tr>";
					}
					vhtml += "</tbody></table>";

					// Horizontal table
					html = "<table class=\"" + classname + "\">";
					html += "<thead><tr>";
					for (var i = 0; i < rs.FieldCount; i++)
						html += "<th>" + GetFieldCaption(rs.GetName(i), options) + "</th>";
					html += "</tr></thead>";
					html += "<tbody>";
					html += "<tr>";
					for (var i = 0; i < rs.FieldCount; i++)
						html += "<td>" + Convert.ToString(rs[i]) + "</td>";
					html += "</tr>";
				}
				while (rs.Read()) { // Other rows
					cnt++;
					html += "<tr>";
					for (var i = 0; i < rs.FieldCount; i++)
						html += "<td>" + Convert.ToString(rs[i]) + "</td>";
					html += "</tr>";
				}
				if (html != "")
					html += "</tbody></table>";
				var str = (cnt > 1 || horizontal) ? html : vhtml;
				return new HtmlString(str);
			}
			public string GetFieldCaption(string key, Dictionary<string, object> options) {
				if (options == null)
					return key;
				object tablename = options.ContainsKey("tablename") ? options["tablename"] : null;
				string caption = "";
				bool usecaption = options.ContainsKey("fieldcaption") && options["fieldcaption"] != null;
				if (usecaption) {
					if (ew_IsList(options["fieldcaption"])) {
						caption = ((Dictionary<string, string>)options["fieldcaption"])[key];
					} else if (ew_NotEmpty(Language)) {
						if (ew_IsList(tablename)) {
							foreach (var tbl in (List<string>)tablename) {
								caption = Language.FieldPhrase(tbl, key, "FldCaption");
								if (ew_NotEmpty(caption))
									break;
							}
						} else if (ew_NotEmpty(tablename)) {
							caption = Language.FieldPhrase(Convert.ToString(tablename), key, "FldCaption");
						}
					}
				}
				return (ew_NotEmpty(caption)) ? caption : key;
			}

			// Dispose
			public void Dispose()
			{
				Trans?.Dispose();
				Conn.Close();
				Conn.Dispose();
				foreach (R dr in _DataReader) {
					dr?.Close();
					dr?.Dispose();
				}
				foreach (M cmd in _Command)
					cmd?.Dispose();
				_Conn?.Close();
				_Conn?.Dispose();
			}

			// Close
			public void Close()
			{
				Dispose();
			}

			// Database Connecting event
			public virtual void Database_Connecting(ref string Connstr) {

				// Check Info["id"] for database ID if more than one database
			}

			// Database Connected event
			public virtual void Database_Connected(DbConnection Cnn) {

				//Execute("Your SQL", Cnn);
			}
		}
		public class cWebImage
		{

			// Default resolution to use when getting bitmap from image
			private const float FixedResolution = 96f;
			private static readonly IDictionary<Guid, ImageFormat> _imageFormatLookup = new[]
			{
				 System.Drawing.Imaging.ImageFormat.Bmp, System.Drawing.Imaging.ImageFormat.Emf, System.Drawing.Imaging.ImageFormat.Exif,
				 System.Drawing.Imaging.ImageFormat.Gif, System.Drawing.Imaging.ImageFormat.Icon, System.Drawing.Imaging.ImageFormat.Jpeg,
				 System.Drawing.Imaging.ImageFormat.MemoryBmp, System.Drawing.Imaging.ImageFormat.Png, System.Drawing.Imaging.ImageFormat.Tiff,
				 System.Drawing.Imaging.ImageFormat.Wmf
			}.ToDictionary(format => format.Guid, format => format);
			private static readonly Func<string, byte[]> _defaultReadAction = File.ReadAllBytes;

			// Initial format is the format of the image when it was constructed.
			// Current format is the format currently stored in the content buffer. This can
			// be different than initial format since image transformations can change format.

			private readonly ImageFormat _initialFormat;
			private readonly List<ImageTransformation> _transformations = new List<ImageTransformation>();
			private ImageFormat _currentFormat;
			private byte[] _content;
			private string _fileName;
			private int _height = -1;
			private int _width = -1;
			private PropertyItem[] _properties; // image metadata
			public cWebImage(byte[] content)
			{
				_initialFormat = ValidateImageContent(content, "content");
				_currentFormat = _initialFormat;
				_content = (byte[])content.Clone();
			}
			public cWebImage(string filePath)
			{
				_fileName = filePath;
				_content = _defaultReadAction(filePath);
				_initialFormat = ValidateImageContent(_content, "filePath");
				_currentFormat = _initialFormat;
			}
			public cWebImage(Stream imageStream)
			{
				if (imageStream.CanSeek)
				{
					imageStream.Seek(0, SeekOrigin.Begin);
					_content = new byte[imageStream.Length];
					using (BinaryReader reader = new BinaryReader(imageStream))
					{
						reader.Read(_content, 0, (int)imageStream.Length);
					}
				}
				else
				{
					List<byte[]> chunks = new List<byte[]>();
					int totalSize = 0;
					using (BinaryReader reader = new BinaryReader(imageStream))
					{

						// Pick some size for chunks that is still under limit
						// that causes them to be placed on the large object heap.

						int chunkSizeInBytes = 1024 * 50;
						byte[] nextChunk = null;
						do
						{
							nextChunk = reader.ReadBytes(chunkSizeInBytes);
							totalSize += nextChunk.Length;
							chunks.Add(nextChunk);
						}
						while (nextChunk.Length == chunkSizeInBytes);
					}
					_content = new byte[totalSize];
					int startIndex = 0;
					foreach (var chunk in chunks)
					{
						chunk.CopyTo(_content, startIndex);
						startIndex += chunk.Length;
					}
				}
				_initialFormat = ValidateImageContent(_content, "imageStream");
				_currentFormat = _initialFormat;
			}
			public int Height
			{
				get
				{
					if ((_transformations.Count > 0) || (_height < 0))
					{
						ApplyTransformationsAndSetProperties();
					}
					return _height;
				}
			}
			public int Width
			{
				get
				{
					if ((_transformations.Count > 0) || (_width < 0))
					{
						ApplyTransformationsAndSetProperties();
					}
					return _width;
				}
			}
			public string FileName
			{
				get { return _fileName; }
				set { _fileName = value; }
			 }
			internal static string ToString<T>(T obj)
			{
				Type type = typeof(T);
				if (type.IsEnum)
				{
					return obj.ToString();
				}
				System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
				if ((converter != null) && (converter.CanConvertTo(typeof(string))))
				{
					return converter.ConvertToInvariantString(obj);
				}
				return null;
			}
			public string ImageFormat
			{
				get
				{
					if (_transformations.Any())
					{
						ApplyTransformationsAndSetProperties();
					}
					return ToString(_currentFormat).ToLowerInvariant();
				}
			}
			public byte[] GetBytes(string requestedFormat = null)
			{
				if (_transformations.Count > 0)
				{
					ApplyTransformationsAndSetProperties();
				}
				ImageFormat requestedImageFormat = null;
				if (!String.IsNullOrEmpty(requestedFormat))
				{

					// This will throw if image format is incorrect.
					requestedImageFormat = GetImageFormat(requestedFormat);
				}
				requestedImageFormat = requestedImageFormat ?? _initialFormat;
				if (requestedImageFormat.Equals(_currentFormat))
				{
					return (byte[])_content.Clone();
				}

				// Conversion from one format to another
				using (MemoryStream sourceBuffer = new MemoryStream(_content))
				{
					using (System.Drawing.Image image = System.Drawing.Image.FromStream(sourceBuffer))
					{

						// if _properties are not initialized that means image did not go through any
						// transformations yet and original byte array contains all metadata available

						if (_properties != null)
						{
							CopyMetadata(_properties, image);
						}
						using (MemoryStream destinationBuffer = new MemoryStream())
						{
							image.Save(destinationBuffer, requestedImageFormat);
							return destinationBuffer.ToArray();
						}
					}
				}
			}
			public cWebImage Resize(int width, int height, bool preserveAspectRatio = true, bool preventEnlarge = false)
			{
				if (width <= 0)
				{
					throw new ArgumentOutOfRangeException("width", "Argument must be greater than 0");
				}
				if (height <= 0)
				{
					throw new ArgumentOutOfRangeException("height", "Argument must be greater than 0");
				}
				ResizeTransformation trans = new ResizeTransformation(height, width, preserveAspectRatio, preventEnlarge);
				_transformations.Add(trans);
				return this;
			}
			public cWebImage Crop(int top = 0, int left = 0, int bottom = 0, int right = 0)
			{
				if (top < 0)
				{
					throw new ArgumentOutOfRangeException(
						"top",
						"Argument must be greater than or equal to 0");
				}
				if (left < 0)
				{
					throw new ArgumentOutOfRangeException(
						"left",
						"Argument must be greater than or equal to 0");
				}
				if (bottom < 0)
				{
					throw new ArgumentOutOfRangeException(
						"bottom",
						"Argument must be greater than or equal to 0");
				}
				if (right < 0)
				{
					throw new ArgumentOutOfRangeException(
						"right",
						"Argument must be greater than or equal to 0");
				}
				CropTransformation crop = new CropTransformation(top, right, bottom, left);
				_transformations.Add(crop);
				return this;
			}
			public cWebImage Write(string requestedFormat = null)
			{

				// GetBytes takes care of executing pending transformations and
				// determining current image format if we didn't have it set before.
				// todo: this could be made more efficient by avoiding cloning array
				// when format is same

				requestedFormat = requestedFormat ?? _initialFormat.ToString();
				byte[] content = GetBytes(requestedFormat);
				string requestedFormatWithPrefix;
				if (requestedFormat.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
				{
					requestedFormatWithPrefix = requestedFormat;
				}
				else
				{
					requestedFormatWithPrefix = "image/" + requestedFormat;
				}
				ew_Response.ContentType = requestedFormatWithPrefix;
				ew_BinaryWrite(content);
				return this;
			}
			public cWebImage Save(string filePath = null, string imageFormat = null, bool forceCorrectExtension = true)
			{
				return Save(File.WriteAllBytes, filePath, imageFormat, forceCorrectExtension);
			}
			internal static string NormalizeImageFormat(string value)
			{
				value = value.ToLowerInvariant();
				switch (value)
				{
					case "jpeg":
					case "jpg":
					case "pjpeg":
						return "jpeg";
					case "png":
					case "x-png":
						return "png";
					case "icon":
					case "ico":
					case "x-icon":
						return "icon";
				}
				return value;
			}
			internal static bool TryFromStringToImageFormat(string value, out ImageFormat result)
			{
				result = default(ImageFormat);
				if (String.IsNullOrEmpty(value))
				{
					return false;
				}
				if (value.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
				{
					value = value.Substring("image/".Length);
				}
				value = NormalizeImageFormat(value);
				System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(ImageFormat));
				try
				{
					result = (ImageFormat)converter.ConvertFromInvariantString(value);
				}
				catch (NotSupportedException)
				{
					return false;
				}
				return true;
			}
			internal cWebImage Save(Action<string, byte[]> saveAction, string filePath, string imageFormat, bool forceWellKnownExtension)
			{
				filePath = filePath ?? FileName;

				// GetBytes takes care of executing pending transformations.
				// todo: this could be made more efficient by avoiding cloning array
				// when format is same

				byte[] content = GetBytes(imageFormat);
				if (forceWellKnownExtension)
				{
					ImageFormat saveImageFormat;
					ImageFormat requestedImageFormat = String.IsNullOrEmpty(imageFormat) ? _initialFormat : GetImageFormat(imageFormat);
					var extension = Path.GetExtension(filePath).TrimStart('.');

					// TryFromStringToImageFormat accepts mime types and image names. For images supported by System.Drawing.Imaging, the image name maps to the extension.
					// Replace the extension with the current format in the following two events:
					// * The extension format cannot be converted to a known format
					// * The format does not match.

					if (!TryFromStringToImageFormat(extension, out saveImageFormat) || !saveImageFormat.Equals(requestedImageFormat))
					{
						extension = requestedImageFormat.ToString().ToLowerInvariant();
						filePath = filePath + "." + extension;
					}
				}
				saveAction(filePath, content);

				// Update the FileName since it may have changed whilst saving.
				FileName = filePath;
				return this;
			}

			// Constructs a System.Drawing.Image instance from the content which validates the contents of the image.
			private static ImageFormat ValidateImageContent(byte[] content, string paramName)
			{
				try
				{
					using (MemoryStream stream = new MemoryStream(content))
					{
						using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream, useEmbeddedColorManagement: false))
						{
							var rawFormat = image.RawFormat;
							ImageFormat actualFormat;

							// RawFormat returns a ImageFormat instance with the same Guid as the predefined types
							// This instance is not very useful when it comes to printing human readable strings and file extensions.
							// Therefore, lookup the predefined instance

							if (!_imageFormatLookup.TryGetValue(rawFormat.Guid, out actualFormat))
							{
								actualFormat = rawFormat;
							}
							return actualFormat;
						}
					}
				}
				catch (ArgumentException exception)
				{
					throw new ArgumentException("Invalid image contents", paramName, exception);
				}
			}
			private static ImageFormat GetImageFormat(string format)
			{
				ImageFormat result;
				if (!TryFromStringToImageFormat(format, out result))
				{
					throw new ArgumentException("Incorrect image format", "format");
				}
				return result;
			}
			private void GetContentFromImageAndUpdateFormat(System.Drawing.Image image)
			{
				using (MemoryStream buffer = new MemoryStream())
				{
					if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
					{

						// Memory Bmps are an in-memory format and do not have encoders to save to disk / stream.
						// Save it in the current format whenever we encounter which ensures we preserve image information such as transparency.

						image.Save(buffer, _currentFormat);
					}
					else
					{

						// If the RawFormat has an encoder, save it as-is to prevent the cost of encoding it to another format such as the initial or current format.
						image.Save(buffer, image.RawFormat);
						_currentFormat = image.RawFormat;
					}
					_content = buffer.ToArray();
				}
			}
			private void ApplyTransformationsAndSetProperties()
			{
				MemoryStream stream = null;
				System.Drawing.Image image = null;
				try
				{
					stream = new MemoryStream(_content);
					image = System.Drawing.Image.FromStream(stream);
					if (_properties == null)
					{

						// makes sure properties is never null after initialization
						_properties = image.PropertyItems ?? new PropertyItem[0];
					}
					foreach (ImageTransformation trans in _transformations)
					{
						System.Drawing.Image tempImage = trans.ApplyTransformation(image);

						// ApplyTransformation could return the same image if no transformations are made or if
						// transformations are made on the image itself.

						if (tempImage != image)
						{
							if (stream != null)
							{
								stream.Dispose();
								stream = null;
							}
							image.Dispose();
							image = tempImage;
						}

						// This is just to keep FxCop happy. Otherwise it thinks that tempImage could be diposed twice.
						tempImage = null;
					}

					// If there were any transformations we need to get new content. This will also update the current format to the RawFormat.
					if (_transformations.Any())
					{
						GetContentFromImageAndUpdateFormat(image);
						_transformations.Clear();
					}
					_height = image.Size.Height;
					_width = image.Size.Width;
				}
				finally
				{
					if (image != null)
					{
						image.Dispose();
					}
					if (stream != null)
					{
						stream.Dispose();
					}
				}
			}
			private static Bitmap GetBitmapFromImage(System.Drawing.Image image, int width, int height, bool preserveResolution = true)
			{
				bool indexed = (image.PixelFormat == PixelFormat.Format1bppIndexed ||
					image.PixelFormat == PixelFormat.Format4bppIndexed ||
					image.PixelFormat == PixelFormat.Format8bppIndexed ||
					image.PixelFormat == PixelFormat.Indexed);
				Bitmap bitmap = indexed ? new Bitmap(width, height) : new Bitmap(width, height, image.PixelFormat);
				if (preserveResolution)
				{
					bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
				}
				else
				{
					bitmap.SetResolution(FixedResolution, FixedResolution);
				}
				using (Graphics graphic = Graphics.FromImage(bitmap))
				{
					if (indexed)
					{
						graphic.FillRectangle(Brushes.White, 0, 0, width, height);
					}
					graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphic.DrawImage(image, 0, 0, width, height);
				}
				return bitmap;
			}
			private static void CopyMetadata(PropertyItem[] properties, System.Drawing.Image target)
			{
				foreach (PropertyItem property in properties)
				{
					try
					{
						target.SetPropertyItem(property);
					}
					catch (ArgumentException)
					{

						// just ignore it; on some configurations this fails
					}
				}
			}
			private class CropTransformation : ImageTransformation
			{
				public CropTransformation(int top, int right, int bottom, int left)
				{
					Top = top;
					Right = right;
					Bottom = bottom;
					Left = left;
				}
				public int Top { get; set; }
				public int Right { get; set; }
				public int Bottom { get; set; }
				public int Left { get; set; }
				public override System.Drawing.Image ApplyTransformation(System.Drawing.Image image)
				{
					if ((Top + Bottom > image.Height) || (Left + Right > image.Width))
					{

						// If Crop arguments are too big (i.e. whole image is cropped) we don't make any changes.
						return image;
					}
					int width = image.Width - (Left + Right);
					int height = image.Height - (Top + Bottom);
					RectangleF rect = new RectangleF(Left, Top, width, height);

					// todo: check if we can guarantee that rect is inside the image at this point
					using (Bitmap bitmap = GetBitmapFromImage(image, image.Width, image.Height))
					{
						try
						{
							return bitmap.Clone(rect, image.PixelFormat);
						}
						catch (OutOfMemoryException)
						{

							// Bitmap.Clone unfortunately throws OOM exception when rect is
							// outside of the source bitmap bounds

							return image;
						}
					}
				}
			}
			private abstract class ImageTransformation
			{
				public abstract System.Drawing.Image ApplyTransformation(System.Drawing.Image image);
			}
			private class ResizeTransformation : ImageTransformation
			{
				public ResizeTransformation(int height, int width, bool preserveAspectRatio, bool preventEnlarge)
				{
					Height = height;
					Width = width;
					PreserveAspectRatio = preserveAspectRatio;
					PreventEnlarge = preventEnlarge;
				}
				public int Height { get; set; }
				public int Width { get; set; }
				public bool PreserveAspectRatio { get; set; }
				public bool PreventEnlarge { get; set; }
				public override System.Drawing.Image ApplyTransformation(System.Drawing.Image image)
				{
					int height = Height;
					int width = Width;
					if (PreserveAspectRatio)
					{
						double heightRatio = (height * 100.0) / image.Height;
						double widthRatio = (width * 100.0) / image.Width;
						if (heightRatio > widthRatio)
						{
							height = (int)Math.Round((widthRatio * image.Height) / 100);
						}
						else if (heightRatio < widthRatio)
						{
							width = (int)Math.Round((heightRatio * image.Width) / 100);
						}
					}
					if (PreventEnlarge)
					{
						if (height > image.Height)
						{
							height = image.Height;
						}
						if (width > image.Width)
						{
							width = image.Width;
						}
					}
					if ((image.Height == height) && (image.Width == width))
					{
						return image;
					}
					return GetBitmapFromImage(image, width, height);
				}
			}
		}

		// Resize binary to thumbnail (interpolation deprecated)
		public static bool ew_ResizeBinary(ref byte[] filedata, ref int width, ref int height, int interpolation = -1)
		{
			if (width <= 0 && height <= 0)
				return true; // No resize, just use the original file data
			try {
				cWebImage img = new cWebImage(filedata);
				if (ew_SameText(img.ImageFormat, "gif") && EW_RESIZE_CONVERT_GIF_TO_PNG)
					img = new cWebImage(img.GetBytes("png"));
				ew_GetResizeDimension(img.Width, img.Height, ref width, ref height);
				if (width > 0 && height > 0) {
					img.Resize(width, height, EW_RESIZE_PRESERVE_ASPECT_RATIO, EW_RESIZE_PREVENT_ENLARGE);
					filedata = img.GetBytes();
					width = img.Width;
					height = img.Height;
				}
				return true;
			} catch {

				//if (EW_DEBUG_ENABLED) throw;
				return false;
			}
		}

		// Resize file to thumbnail file (interpolation deprecated)
		public static bool ew_ResizeFile(string fn, string tn, ref int width, ref int height, int interpolation = -1)
		{
			if (!File.Exists(fn))
				return false;
			try {
				cWebImage img = new cWebImage(fn);
				if (ew_SameText(img.ImageFormat, "gif") && EW_RESIZE_CONVERT_GIF_TO_PNG)
					img = new cWebImage(img.GetBytes("png"));
				ew_GetResizeDimension(img.Width, img.Height, ref width, ref height);
				if (width > 0 && height > 0) {
					img.Resize(width, height, EW_RESIZE_PRESERVE_ASPECT_RATIO, EW_RESIZE_PREVENT_ENLARGE);
					img.Save(fn, img.ImageFormat, false);
					width = img.Width;
					height = img.Height;
				} else {
					File.Copy(fn, tn); // No resize, just use the original file
				}
				return true;
			} catch {

				//if (EW_DEBUG_ENABLED) throw;
				return false;
			}
		}

		// Resize file to binary (interpolation deprecated)
		public static byte[] ew_ResizeFileToBinary(string fn, ref int width, ref int height, int interpolation = -1)
		{
			if (!File.Exists(fn))
				return null;
			try {
				cWebImage img = new cWebImage(fn);
				if (ew_SameText(img.ImageFormat, "gif") && EW_RESIZE_CONVERT_GIF_TO_PNG)
					img = new cWebImage(img.GetBytes("png"));
				if (width > 0 || height > 0) {
					ew_GetResizeDimension(img.Width, img.Height, ref width, ref height);
					img.Resize(width, height, EW_RESIZE_PRESERVE_ASPECT_RATIO, EW_RESIZE_PREVENT_ENLARGE);
					width = img.Width;
					height = img.Height;
				}
				return img.GetBytes();
			} catch {

				//if (EW_DEBUG_ENABLED) throw;
				return null;
			}
		}

		// Set up resize width/height
		private static void ew_GetResizeDimension(int ImageWidth, int ImageHeight, ref int ResizeWidth, ref int ResizeHeight) {
			if (ResizeWidth <= 0) { // maintain aspect ratio
				ResizeWidth = ew_ConvertToInt(ImageWidth * ResizeHeight / ImageHeight);
			} else if (ResizeHeight <= 0) { // maintain aspect ratio
				ResizeHeight = ew_ConvertToInt(ImageHeight * ResizeWidth / ImageWidth);
			}
		}

		//
		// Basic Search class
		//

		public class cBasicSearch {
			public string TblVar = "";
			public bool BasicSearchAnyFields = EW_BASIC_SEARCH_ANY_FIELDS;
			public string KeywordDefault = "";
			public string TypeDefault = "";
			private string _Prefix = "";
			public string Keyword = "";
			public string Type = "";

			// Constructor
			public cBasicSearch(string tblvar) {
				TblVar = tblvar;
				_Prefix = EW_PROJECT_NAME + "_" + tblvar + "_";
			}

			// Session variable name
			public string GetSessionName(string suffix) {
				return _Prefix + suffix;
			}

			// Load default
			public void LoadDefault() {
				Keyword = KeywordDefault;
				Type = TypeDefault;
				if (ew_Session[GetSessionName(EW_TABLE_BASIC_SEARCH_TYPE)] == null && ew_NotEmpty(TypeDefault)) // Save default to session
					SessionType = TypeDefault;
			}

			// Unset session
			public void UnsetSession() {
				ew_Session.Remove(GetSessionName(EW_TABLE_BASIC_SEARCH_TYPE));
				ew_Session.Remove(GetSessionName(EW_TABLE_BASIC_SEARCH));
			}

			// Isset session
			public bool IssetSession {
				get {
					return ew_Session[GetSessionName(EW_TABLE_BASIC_SEARCH)] != null;
				}
			}

			// Keyword
			public string SessionKeyword {
				get {
					return Convert.ToString(ew_Session[GetSessionName(EW_TABLE_BASIC_SEARCH)]);
				}
				set {
					Keyword = value;
					ew_Session[GetSessionName(EW_TABLE_BASIC_SEARCH)] = value;
				}
			}

			// Type
			public string SessionType {
				get {
					return Convert.ToString(ew_Session[GetSessionName(EW_TABLE_BASIC_SEARCH_TYPE)]);
				}
				set {
					Type = value;
					ew_Session[GetSessionName(EW_TABLE_BASIC_SEARCH_TYPE)] = value;
				}
			}

			// Get type name
			public string TypeName() {
				switch (SessionType) {
					case "=": return Language.Phrase("QuickSearchExact");
					case "AND": return Language.Phrase("QuickSearchAll");
					case "OR": return Language.Phrase("QuickSearchAny");
					default: return Language.Phrase("QuickSearchAuto");
				}
			}

			// Get short type name
			public string TypeNameShort() {
				string typname;
				switch (SessionType) {
					case "=": typname = Language.Phrase("QuickSearchExactShort"); break;
					case "AND": typname = Language.Phrase("QuickSearchAllShort"); break;
					case "OR": typname = Language.Phrase("QuickSearchAnyShort"); break;
					default: typname = Language.Phrase("QuickSearchAutoShort"); break;
				}
				if (ew_NotEmpty(typname))
					typname += "&nbsp;";
				return typname;
			}

			// save
			public void Save() {
				SessionKeyword = Keyword;
				SessionType = Type;
			}
			public void Load() {
				Keyword = SessionKeyword;
				Type = SessionType;
			}
		}

		//
		// Advanced Search class
		//

		public class cAdvancedSearch
		{
			public string TblVar = "";
			public string FldVar = "";
			public string SearchValue = null; // Search value
			public string ViewValue = ""; // View value
			public string SearchOperator = "="; // Search operator
			public string SearchCondition = "AND"; // Search condition
			public string SearchValue2 = null; // Search value 2
			public string ViewValue2 = ""; // View value 2
			public string SearchOperator2 = "="; // Search operator 2
			public string SearchValueDefault = null; // Search value default
			public string SearchOperatorDefault = ""; // Search operator default
			public string SearchConditionDefault = ""; // Search condition default
			public string SearchValue2Default = null; // Search value 2 default
			public string SearchOperator2Default = ""; // Search operator 2 default
			private string _Prefix = "";
			private string _Suffix = "";

			// Constructor
			public cAdvancedSearch(string tblvar, string fldvar) {
				TblVar = tblvar;
				FldVar = fldvar;
				_Prefix = EW_PROJECT_NAME + "_" + tblvar + "_" + EW_TABLE_ADVANCED_SEARCH + "_";
				_Suffix = "_" + fldvar.Substring(2);
			}

			// Session variable name
			public string GetSessionName(string infix) {
				return _Prefix + infix + _Suffix;
			}

			// Unset session
			public void UnsetSession() {
				ew_Session.Remove(GetSessionName("x"));
				ew_Session.Remove(GetSessionName("z"));
				ew_Session.Remove(GetSessionName("v"));
				ew_Session.Remove(GetSessionName("y"));
				ew_Session.Remove(GetSessionName("w"));
			}

			// Isset session
			public bool IssetSession {
				get { return ew_Session[GetSessionName("x")] != null || ew_Session[GetSessionName("y")] != null; }
			}

			// Save to session
			public void Save() {
				if (!ew_SameStr(ew_Session[GetSessionName("x")], SearchValue))
					ew_Session[GetSessionName("x")] = SearchValue;
				if (!ew_SameStr(ew_Session[GetSessionName("y")], SearchValue2))
					ew_Session[GetSessionName("y")] = SearchValue2;
				if (!ew_SameStr(ew_Session[GetSessionName("z")], SearchOperator))
					ew_Session[GetSessionName("z")] = SearchOperator;
				if (!ew_SameStr(ew_Session[GetSessionName("v")], SearchCondition))
					ew_Session[GetSessionName("v")] = SearchCondition;
				if (!ew_SameStr(ew_Session[GetSessionName("w")], SearchOperator2))
					ew_Session[GetSessionName("w")] = SearchOperator2;
			}

			// Load from session
			public void Load() {
				SearchValue = Convert.ToString(ew_Session[GetSessionName("x")]);
				SearchOperator = Convert.ToString(ew_Session[GetSessionName("z")]);
				SearchCondition = Convert.ToString(ew_Session[GetSessionName("v")]);
				SearchValue2 = Convert.ToString(ew_Session[GetSessionName("y")]);
				SearchOperator2 = Convert.ToString(ew_Session[GetSessionName("w")]);
			}

			// Get value
			public string GetValue(string infix) {
				return Convert.ToString(ew_Session[GetSessionName(infix)]);
			}

			// Load default values
			public void LoadDefault() {
				if (ew_NotEmpty(SearchValueDefault)) SearchValue = SearchValueDefault;
				if (ew_NotEmpty(SearchOperatorDefault)) SearchOperator = SearchOperatorDefault;
				if (ew_NotEmpty(SearchConditionDefault)) SearchCondition = SearchConditionDefault;
				if (ew_NotEmpty(SearchValue2Default)) SearchValue2 = SearchValue2Default;
				if (ew_NotEmpty(SearchOperator2Default)) SearchOperator2 = SearchOperator2Default;
			}

			// Convert to JSON
			public string ToJSON() {
				if (ew_NotEmpty(SearchValue) || ew_NotEmpty(SearchValue2)) {
					return "\"x" + _Suffix + "\":\"" + ew_JsEncode2(SearchValue) + "\"," +
					"\"z" + _Suffix + "\":\"" + ew_JsEncode2(SearchOperator) + "\"," +
					"\"v" + _Suffix + "\":\"" + ew_JsEncode2(SearchCondition) + "\"," +
					"\"y" + _Suffix + "\":\"" + ew_JsEncode2(SearchValue2) + "\"," +
					"\"w" + _Suffix + "\":\"" + ew_JsEncode2(SearchOperator2) + "\"";
				} else {
					return "";
				}
			}
		}

		//
		// Upload class
		//

		public class cUpload
		{
			public int Index = -1; // Index to handle multiple form elements
			public string TblVar; // Table variable
			public string FldVar; // Field variable
			public object DbValue = System.DBNull.Value; // Value from database // DN
			public string Message = ""; // Error message
			public object Value; // Upload value
			public string FileName = ""; // Upload file name
			public long FileSize; // Upload file size
			public string ContentType = ""; // File content type
			public int ImageWidth = -1; // Image width
			public int ImageHeight = -1; // Image height
			public bool UploadMultiple = false; // Multiple upload
			public bool KeepFile = true; // Keep old file
			public List<string> Plugins = new List<string>(); // Plugins for Resize()

			// Contructor
			public cUpload(string ATblVar, string AFldVar)
			{
				TblVar = ATblVar;
				FldVar = AFldVar;
			}
			public bool IsEmpty {
				get {
					return DbValue == System.DBNull.Value;
				}
			}

			// Check the file type of the uploaded file
			private bool UploadAllowedFileExt(string FileName)
			{
				return ew_CheckFileType(FileName);
			}

			// Get upload file
			public bool UploadFile()
			{
				try {
					Value = System.DBNull.Value; // Reset first
					Message = ""; // Reset first
					var fldvar = (Index < 0) ? FldVar : FldVar.Substring(0, 1) + Index + FldVar.Substring(1);
					var wrkvar = "fn_" + fldvar;
					FileName = ew_Post(wrkvar); // Get file name
					wrkvar = "fa_" + fldvar;
					KeepFile = ew_Post(wrkvar) == "1"; // Check if keep old file
					if (!KeepFile && ew_NotEmpty(FileName) && !UploadMultiple) {
						var f = ew_UploadTempPath(fldvar, TblVar) + EW_PATH_DELIMITER + FileName;
						var fi = new FileInfo(f);
						if (fi.Exists) {
							Value = File.ReadAllBytes(f);
							FileSize = fi.Length;
							ContentType = ew_ContentType(((byte[])Value).Take(11), f);
							try {
								System.Drawing.Image img = System.Drawing.Image.FromFile(f);
								ImageWidth = Convert.ToInt32(img.PhysicalDimension.Width);
								ImageHeight = Convert.ToInt32(img.PhysicalDimension.Height);
							} catch {}
						}
					}
					return true;
				} catch (Exception e) {
					Message = e.Message;
					return false;
				}
			}

			// Resize image
			// Note: Interpolation is deprecated, kept for backward compatibility only.

			public bool Resize(int Width, int Height, int Interpolation = -1)
			{
				bool result = false;
				if (!Convert.IsDBNull(Value)) {
					int wrkWidth = Width;
					int wrkHeight = Height;
					byte[] data = (byte[])Value;
					result = ew_ResizeBinary(ref data, ref wrkWidth, ref wrkHeight);
					if (result) {
						Value = data;
						if (wrkWidth > 0 && wrkHeight > 0) {
							ImageWidth = wrkWidth;
							ImageHeight = wrkHeight;
						}
						FileSize = data.Length;
					}
				}
				return result;
			}

			// Get file count
			public int Count() {
				if (!UploadMultiple && !ew_Empty(Value)) {
					return 1;
				} else if (UploadMultiple && FileName != "") {
					var ar = FileName.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
					return ar.Length;
				}
				return 0;
			}

			// Get temp file path as string or string[]
			public string GetTempFile(int idx) {
				string fldvar = (Index < 0) ? FldVar : FldVar.Substring(0, 1) + Index + FldVar.Substring(1);
				if (FileName != "") {
					if (UploadMultiple) {
						var ar = FileName.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
						if (idx > -1 && idx < ar.Length) {
							return ew_UploadTempPath(fldvar, TblVar) + EW_PATH_DELIMITER + ar[idx];
						} else {
							return null;
						}
					} else {
						return ew_UploadTempPath(fldvar, TblVar) + EW_PATH_DELIMITER + FileName;
					}
				}
				return null;
			}

			// Get temp file path as List<string>
			public List<string> GetTempFiles() {
				string fldvar = FldVar;
				var files = new List<string>();
				if (FileName != "") {
					if (UploadMultiple) {
						var ar = FileName.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
						foreach (string fn in ar)
							files.Add(ew_UploadTempPath(fldvar, TblVar) + EW_PATH_DELIMITER + fn);
					} else {
						files.Add(ew_UploadTempPath(fldvar, TblVar) + EW_PATH_DELIMITER + FileName);
					}
					return files;
				}
				return null;
			}

			// Save uploaded data to file (Path relative to application root)
			public bool SaveToFile(string Path, string NewFileName, bool Overwrite, int Idx = -1)
			{
				if (!Convert.IsDBNull(Value)) {
					Path = ew_UploadPathEx(true, Path);
					if (ew_Empty(NewFileName)) NewFileName = FileName;
					byte[] data = (byte[])Value;
					if (!Overwrite)
						NewFileName = ew_UploadFileNameEx(Path, NewFileName);
					return ew_SaveFile(Path, NewFileName, ref data);
				} else if (Idx >= 0) { // Use file from upload temp folder
					var file = (string)GetTempFile(Idx);
					if (File.Exists(file)) {
						if (!Overwrite)
							NewFileName = ew_UploadFileNameEx(Path, NewFileName);
						return ew_CopyFile(Path, NewFileName, file, Overwrite); // DN
					}
				}
				return false;
			}

			// Resize and save uploaded data to file (Path relative to application root)
			// Note: Interpolation is deprecated, kept for backward compatibility only.

			public bool ResizeAndSaveToFile(int Width, int Height, int Interpolation, string Path, string NewFileName, bool Overwrite, int Idx = -1)
			{
				var bResult = false;
				if (!Convert.IsDBNull(Value)) {

					// Save old values
					var OldValue = Value;
					var OldWidth = ImageWidth;
					var OldHeight = ImageHeight;
					var OldFileSize = FileSize;
					try {
						Resize(Width, Height);
						bResult = SaveToFile(Path, NewFileName, Overwrite);
					} finally { // Restore old values
						Value = OldValue;
						ImageWidth = OldWidth;
						ImageHeight = OldHeight;
						FileSize = OldFileSize;
					}
				} else if (Idx >= 0) { // Use file from upload temp folder
					var file = GetTempFile(Idx);
					if (File.Exists(file)) {
						Value = File.ReadAllBytes(file);
						Resize(Width, Height);
						try {
							bResult = SaveToFile(Path, NewFileName, Overwrite);
						} finally {
							Value = System.DBNull.Value;
						}
					}
				}
				return bResult;
			}
		}

		// Find byte pattern
		public static string ew_FindBytePattern(IEnumerable<byte> input, int type = 2) {
			if (ew_Empty(input))
				return "";
			byte[] patternDoc = new byte[] { 0x57, 0x6F, 0x72, 0x64, 0x2E, 0x44, 0x6F, 0x63, 0x75,0x6D, 0x65, 0x6E, 0x74 }; // find pattern "Word.Document"
			byte[] patternXls = new byte[] { 0x77, 0x6F, 0x72, 0x6B, 0x62, 0x6F, 0x6F, 0x6B }; // find pattern "workbook"
			byte[] patternDocx = new byte[] { 0x77, 0x6F, 0x72, 0x64, 0x2F, 0x5F, 0x72, 0x65, 0x6C }; // find pattern "word/_rel"
			byte[] patternXlsx = new byte[] { 0x78, 0x6C, 0x2F, 0x77, 0x6F, 0x72, 0x6B, 0x62, 0x6F, 0x6F, 0x6B }; // find pattern "x1/workbook"
			List<byte[]> searchPattern = new List<byte[]>();
			if (type == 1) {
				searchPattern = new List<byte[]>() {patternDoc, patternXls};
			} else if (type == 2) {
				searchPattern = new List<byte[]>() {patternDocx, patternXlsx};
			}
			byte[] compare = input.ToArray<byte>();
			bool found = false;
			for (int k = 0; k < searchPattern.Count; k++) {
				for (int i = 0; i < compare.Length - searchPattern[k].Length; i++) {
					if (searchPattern[k][0] == compare[i]) {
						found = true;
						for (int j = 0; j < searchPattern[k].Length; j++) {
							if (compare[i+j] != searchPattern[k][j]) {
								found = false;
								break;
							}
						}
						if (found) {
							if (type == 1 && k == 0) return "doc";
							if (type == 1 && k == 1) return "xls";
							if (type == 2 && k == 0) return "docx";
							if (type == 2 && k == 1) return "xlsx";
						}
					}
				}
			}
			return "";
		}

		// Get content type
		public static string ew_ContentType(IEnumerable<byte> data, string fn = "") {

			// http://en.wikipedia.org/wiki/List_of_file_signatures
			if (data != null && (data.Take(6).SequenceEqual(new byte[] {0x47, 0x49, 0x46, 0x38, 0x37, 0x61}) || data.Take(6).SequenceEqual(new byte[] {0x47, 0x49, 0x46, 0x38, 0x39, 0x61}))) { // gif
				return "image/gif";

			//} else if (data != null && data.Take(4).SequenceEqual(new byte[] {0xFF, 0xD8, 0xFF, 0xE0}) && data.Skip(6).Take(5).SequenceEqual(new byte[] {0x4A, 0x46, 0x49, 0x46, 0x00})) { // jpg
			} else if (data != null && data.Take(4).SequenceEqual(new byte[] {0xFF, 0xD8, 0xFF, 0xE0})) {
				return "image/jpeg";
			} else if (data != null && data.Take(8).SequenceEqual(new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A})) { // png
				return "image/png";
			} else if (data != null && data.Take(2).SequenceEqual(new byte[] {0x42, 0x4D})) { // bmp
				return "image/bmp";
			} else if (data != null && data.Take(4).SequenceEqual(new byte[] {0x25, 0x50, 0x44, 0x46})) { // pdf
				return "application/pdf";
			} else if (data != null && data.Take(8).SequenceEqual(new byte[] {0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1})) { // xls/doc/ppt
				if (ew_FindBytePattern(data, 1) == "xls") // xls
					return "application/vnd.ms-excel";
				else if (ew_FindBytePattern(data, 1) == "doc") // doc
					return "application/msword";
				else	
					return "application/octet-stream";
			} else if (data != null && data.Take(4).SequenceEqual(new byte[] {0x50, 0x4B, 0x03, 0x04})) { // docx/xlsx/pptx/zip
				if (ew_FindBytePattern(data, 2) == "xlsx") // xlsx
					return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				else if (ew_FindBytePattern(data, 2) == "docx") // docx
					return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
				else
					return "application/octet-stream";
			} else if (fn != "") {
				return ew_ContentType(fn);
			} else {
				return "application/octet-stream"; // DN
			}
		}

		// Get content type by file name // DN
		public static string ew_ContentType(string fn) {
			string contentType;
			var provider = ew_StaticFileOptions.ContentTypeProvider;
			return provider.TryGetContentType(fn, out contentType) ? contentType : "";
		}

		// Return multi-value search SQL
		public static string ew_GetMultiSearchSql(cField Fld, string FldOpr, string FldVal, string dbid = "DB")
		{
			if (FldOpr == "IS NULL" || FldOpr == "IS NOT NULL") {
				return Fld.FldExpression + " " + FldOpr;
			} else {
				string sSql;
				string sWrk = "";
				string[] arVal = FldVal.Split(',');
				string dbtype = ew_GetConnectionType(dbid);
				if (FldOpr == "LIKE")
					sWrk = "";
				else
					sWrk = Fld.FldExpression + ew_SearchString(FldOpr, FldVal, EW_DATATYPE_STRING, dbid);
				for (int i = 0; i < arVal.Length; i++) {
					string sVal = arVal[i].Trim();
					if (sVal == EW_NULL_VALUE) {
						sSql = Fld.FldExpression + " IS NULL";
					} else if (sVal == EW_NOT_NULL_VALUE) {
						sSql = Fld.FldExpression + " IS NOT NULL";
					} else {
						if (FldOpr == "LIKE") {
							if (dbtype == "MYSQL") {
								sSql = "FIND_IN_SET('" + ew_AdjustSql(sVal, dbid) + "', " + Fld.FldExpression + ")";
							} else {
								if (arVal.Length == 1 || EW_SEARCH_MULTI_VALUE_OPTION == 3) {
									sSql = Fld.FldExpression + " = '" + ew_AdjustSql(sVal, dbid) + "' OR " + ew_GetMultiSearchSqlPart(Fld, sVal, dbid);
								} else {
									sSql = ew_GetMultiSearchSqlPart(Fld, sVal, dbid);
								}
							}
						} else {
							sSql = Fld.FldExpression + ew_SearchString(FldOpr, sVal, EW_DATATYPE_STRING, dbid);
						}
					}
					if (ew_NotEmpty(sWrk)) {
						if (EW_SEARCH_MULTI_VALUE_OPTION == 2) {
							sWrk = sWrk + " AND ";
						} else if (EW_SEARCH_MULTI_VALUE_OPTION == 3) {
							sWrk = sWrk + " OR ";
						}
					}
					sWrk = sWrk + "(" + sSql + ")";
				}
				return sWrk;
			}
		}

		// Get multi search SQL part
		public static string ew_GetMultiSearchSqlPart(cField Fld, string FldVal, string dbid = "DB")
		{
			return Fld.FldExpression + ew_Like("'" + ew_AdjustSql(FldVal, dbid) + ",%'", dbid) + " OR " +
				Fld.FldExpression + ew_Like("'%," + ew_AdjustSql(FldVal, dbid) + ",%'", dbid) + " OR " +
				Fld.FldExpression + ew_Like("'%," + ew_AdjustSql(FldVal, dbid) + "'", dbid);
		}

		// Check if float format
		public static bool ew_IsFloatFormat(int FldType) {
			return (FldType == 4 || FldType == 5 || FldType == 131 || FldType == 6);
		}

		// Get search SQL
		public static string ew_GetSearchSql(cField Fld, string FldVal, string FldOpr, string FldCond, string FldVal2, string FldOpr2, string dbid = "DB")
		{
			string sSql = "";
			bool bVirtual = (Fld.FldIsVirtual && Fld.FldVirtualSearch);
			string sFldExpression = (bVirtual) ? Fld.FldVirtualExpression : Fld.FldExpression;
			int FldDataType = Fld.FldDataType;
			if (ew_IsFloatFormat(Fld.FldType)) {
				FldVal = ew_StrToFloat(FldVal);
				FldVal2 = ew_StrToFloat(FldVal2);
			}
			if (bVirtual)
				FldDataType = EW_DATATYPE_STRING;
			if (FldDataType == EW_DATATYPE_NUMBER) { // Fix wrong operator
				if (FldOpr == "LIKE" || FldOpr == "STARTS WITH" || FldOpr == "ENDS WITH") {
					FldOpr = "=";
				} else if (FldOpr == "NOT LIKE") {
					FldOpr = "<>";
				}
				if (FldOpr2 == "LIKE" || FldOpr2 == "STARTS WITH" || FldOpr == "ENDS WITH") {
					FldOpr2 = "=";
				} else if (FldOpr2 == "NOT LIKE") {
					FldOpr2 = "<>";
				}
			}
			if (FldOpr == "BETWEEN") {
				var IsValidValue = (FldDataType != EW_DATATYPE_NUMBER) ||
					(FldDataType == EW_DATATYPE_NUMBER && ew_IsNumeric(FldVal) && ew_IsNumeric(FldVal2));
				if (ew_NotEmpty(FldVal) && ew_NotEmpty(FldVal2) && IsValidValue)
					sSql = sFldExpression + " BETWEEN " + ew_QuotedValue(FldVal, FldDataType, dbid) +
						" AND " + ew_QuotedValue(FldVal2, FldDataType, dbid);
			} else {

				// Handle first value
				if (FldVal == EW_NULL_VALUE || FldOpr == "IS NULL") {
					sSql = Fld.FldExpression + " IS NULL";
				} else if (FldVal == EW_NOT_NULL_VALUE || FldOpr == "IS NOT NULL") {
					sSql = Fld.FldExpression + " IS NOT NULL";
				} else {
					var IsValidValue = (FldDataType != EW_DATATYPE_NUMBER) ||
						(FldDataType == EW_DATATYPE_NUMBER && ew_IsNumeric(FldVal));
					if (ew_NotEmpty(FldVal) && IsValidValue && ew_IsValidOpr(FldOpr, FldDataType)) {
						sSql = sFldExpression + ew_SearchString(FldOpr, FldVal, FldDataType, dbid);
						if (Fld.FldDataType == EW_DATATYPE_BOOLEAN && FldVal == Fld.FalseValue && FldOpr == "=")
							sSql = "(" + sSql + " OR " + sFldExpression + " IS NULL)";
					}
				}

				// Handle second value
				string sSql2 = "";
				if (FldVal2 == EW_NULL_VALUE || FldOpr2 == "IS NULL") {
					sSql2 = Fld.FldExpression + " IS NULL";
				} else if (FldVal2 == EW_NOT_NULL_VALUE || FldOpr2 == "IS NOT NULL") {
					sSql2 = Fld.FldExpression + " IS NOT NULL";
				} else {
					var IsValidValue = (FldDataType != EW_DATATYPE_NUMBER) ||
						(FldDataType == EW_DATATYPE_NUMBER && ew_IsNumeric(FldVal2));
					if (ew_NotEmpty(FldVal2) && IsValidValue && ew_IsValidOpr(FldOpr2, FldDataType)) {
						sSql2 = sFldExpression + ew_SearchString(FldOpr2, FldVal2, FldDataType, dbid);
						if (Fld.FldDataType == EW_DATATYPE_BOOLEAN && FldVal2 == Fld.FalseValue && FldOpr2 == "=")
							sSql2 = "(" + sSql2 + " OR " + sFldExpression + " IS NULL)";
					}
				}

				// Combine SQL
				if (ew_NotEmpty(sSql2)) {
					if (ew_NotEmpty(sSql))
						sSql = "(" + sSql + " " + ((FldCond == "OR") ? "OR" : "AND") + " " + sSql2 + ")";
					else
						sSql = sSql2;
				}
			}
			return sSql;
		}

		// Return search string
		public static string ew_SearchString(string FldOpr, string FldVal, int FldType, string dbid = "DB")
		{
			if (FldVal == EW_NULL_VALUE || FldOpr == "IS NULL") {
				return " IS NULL";
			} else if (FldVal == EW_NOT_NULL_VALUE || FldOpr == "IS NOT NULL") {
				return " IS NOT NULL";
			} else if (FldOpr == "LIKE") {
				return ew_Like(ew_QuotedValue("%" + FldVal + "%", FldType, dbid), dbid);
			} else if (FldOpr == "NOT LIKE") {
				return " NOT " + ew_Like(ew_QuotedValue("%" + FldVal + "%", FldType, dbid), dbid);
			} else if (FldOpr == "STARTS WITH") {
				return ew_Like(ew_QuotedValue(FldVal + "%", FldType, dbid), dbid);
			} else if (FldOpr == "ENDS WITH") {
				return ew_Like(ew_QuotedValue("%" + FldVal, FldType, dbid), dbid);
			} else {
				if (FldType == EW_DATATYPE_NUMBER && !ew_IsNumeric(FldVal)) // Invalid field value
					return " = -1 AND 1 = 0"; // Always false
				else
					return " " + FldOpr + " " + ew_QuotedValue(FldVal, FldType, dbid);
			}
		}

		// Check if valid operator
		public static bool ew_IsValidOpr(string Opr, int FldType)
		{
			bool valid = (Opr == "=" || Opr == "<" || Opr == "<=" || Opr == ">" || Opr == ">=" || Opr == "<>");
			if (FldType == EW_DATATYPE_STRING || FldType == EW_DATATYPE_MEMO) {
				valid = valid || Opr == "LIKE" || Opr == "NOT LIKE" || Opr == "STARTS WITH" || Opr == "ENDS WITH";
			}
			return valid;
		}

		// Quoted table/field name based on dbid
		public static string ew_QuotedName(string Name, string dbid = "DB")
		{
			var db = Db(dbid);
			return db["qs"] + Name.Replace(db["qe"], db["qe"] + db["qe"]) + db["qe"];
		}

		// Quoted field value based on dbid
		public static string ew_QuotedValue(object Value, int FldType, string dbid = "DB")
		{
			string dbtype = ew_GetConnectionType(dbid);
			switch (FldType) {
				case EW_DATATYPE_STRING:
				case EW_DATATYPE_MEMO:
					if (EW_REMOVE_XSS)
						Value = ew_RemoveXSS(Value);
					if (dbtype == "MSSQL")
						return "N'" + ew_AdjustSql(Value, dbid) + "'";
					else
						return "'" + ew_AdjustSql(Value, dbid) + "'";
				case EW_DATATYPE_GUID:
					if (dbtype == "ACCESS") {
						if (Convert.ToString(Value).StartsWith("{")) {
							return Convert.ToString(Value);
						} else {
							return "{" + ew_AdjustSql(Value, dbid) + "}";
						}
					} else {
						return "'" + ew_AdjustSql(Value, dbid) + "'";
					}
				case EW_DATATYPE_DATE:
				case EW_DATATYPE_TIME:
					if (dbtype == "ACCESS") {
						return "#" + ew_AdjustSql(Value, dbid) + "#";
					} else if (dbtype == "ORACLE") {
						return "TO_DATE('" + ew_AdjustSql(Value, dbid) + "', 'YYYY/MM/DD HH24:MI:SS')";
					} else {
						return "'" + ew_AdjustSql(Value, dbid) + "'";
					}
				case EW_DATATYPE_BOOLEAN:
					 if (dbtype == "MYSQL" || dbtype == "ORACLE") { // ENUM('Y','N'), ENUM('y','n'), ENUM('1'/'0')
											 return "'" + ew_AdjustSql(Value, dbid) + "'";
									 } else { // Boolean
											 return Convert.ToString(Value);
									 }
				case EW_DATATYPE_NUMBER:
					if (ew_IsNumeric(Value))
						return Convert.ToString(Value);
					else
						return "null"; // Treat as null
				default:
					return Convert.ToString(Value);
			}
		}

		// Pad zeros before number
		public static string ew_ZeroPad(object m, int t)
		{
			return Convert.ToString(m).PadLeft(t, '0');
		}

		// Cast date/time field for LIKE
		public static string ew_CastDateFieldForLike(string fld, int namedformat, string dbid = "DB")
		{
			string dbtype = ew_GetConnectionType(dbid);
			bool isDateTime = false; // Date/Time
			if (namedformat == 0 || namedformat == 1 || namedformat == 2 || namedformat == 8) {
				isDateTime = (namedformat == 1 || namedformat == 8);
				namedformat = EW_DATE_FORMAT_ID;
			}
			bool shortYear = (namedformat >= 12 && namedformat <= 17);
			isDateTime = isDateTime || new List<int> { 9, 10, 11, 15, 16, 17 }.Contains(namedformat);
			string dateFormat = "";
			switch (namedformat) {
				case 3:
					if (dbtype == "MYSQL") {
						dateFormat = "%h" + EW_TIME_SEPARATOR + "%i" + EW_TIME_SEPARATOR + "%s %p";
					} else if (dbtype == "ACCESS") {
						dateFormat = "hh" + EW_TIME_SEPARATOR + "nn" + EW_TIME_SEPARATOR + "ss AM/PM";
					} else if (dbtype == "MSSQL") {
						dateFormat = "REPLACE(LTRIM(RIGHT(CONVERT(VARCHAR(19), %s, 0), 7)), ':', '" + EW_TIME_SEPARATOR + "')"; // Use hh:miAM (or PM) only or SQL too lengthy
					} else if (dbtype == "ORACLE") {
						dateFormat = "HH" + EW_TIME_SEPARATOR + "MI" + EW_TIME_SEPARATOR + "SS AM";
					}
					break;
				case 4:
					if (dbtype == "MYSQL") {
						dateFormat = "%H" + EW_TIME_SEPARATOR + "%i" + EW_TIME_SEPARATOR + "%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = "hh" + EW_TIME_SEPARATOR + "nn" + EW_TIME_SEPARATOR + "ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = "REPLACE(CONVERT(VARCHAR(8), %s, 108), ':', '" + EW_TIME_SEPARATOR + "')";
					} else if (dbtype == "ORACLE") {
						dateFormat = "HH24" + EW_TIME_SEPARATOR + "MI" + EW_TIME_SEPARATOR + "SS";
					}
					break;
				case 5: case 9: case 12: case 15:
					if (dbtype == "MYSQL") {
						dateFormat = (shortYear ? "%y" : "%Y") + EW_DATE_SEPARATOR + "%m" + EW_DATE_SEPARATOR + "%d";
						if (isDateTime) dateFormat += " %H" + EW_TIME_SEPARATOR + "%i" + EW_TIME_SEPARATOR + "%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = (shortYear ? "yy" : "yyyy") + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + "dd";
						if (isDateTime) dateFormat += " hh" + EW_TIME_SEPARATOR + "nn" + EW_TIME_SEPARATOR + "ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = "REPLACE(" + (shortYear ? "CONVERT(VARCHAR(8), %s, 2)" : "CONVERT(VARCHAR(10), %s, 102)") + ", '+', '" + EW_DATE_SEPARATOR + "')";
						if (isDateTime) dateFormat = "(" + dateFormat + " + ' ' + REPLACE(CONVERT(VARCHAR(8), %s, 108), ':', '" + EW_TIME_SEPARATOR + "'))";
					} else if (dbtype == "ORACLE") {
						dateFormat = (shortYear ? "YY" : "YYYY") + EW_DATE_SEPARATOR + "MM" + EW_DATE_SEPARATOR + "DD";
						if (isDateTime) dateFormat += " HH24" + EW_TIME_SEPARATOR + "MI" + EW_TIME_SEPARATOR + "SS";
					}
					break;
				case 6: case 10: case 13: case 16:
					if (dbtype == "MYSQL") {
						dateFormat = "%m" + EW_DATE_SEPARATOR + "%d" + EW_DATE_SEPARATOR + (shortYear ? "%y" : "%Y");
						if (isDateTime) dateFormat += " %H" + EW_TIME_SEPARATOR + "%i" + EW_TIME_SEPARATOR + "%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = "mm" + EW_DATE_SEPARATOR + "dd" + EW_DATE_SEPARATOR + (shortYear ? "yy" : "yyyy");
						if (isDateTime) dateFormat += " hh" + EW_TIME_SEPARATOR + "nn" + EW_TIME_SEPARATOR + "ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = "REPLACE(" + (shortYear ? "CONVERT(VARCHAR(8), %s, 1)" : "CONVERT(VARCHAR(10), %s, 101)") + ", '/', '" + EW_DATE_SEPARATOR + "')";
						if (isDateTime) dateFormat = "(" + dateFormat + " + ' ' + REPLACE(CONVERT(VARCHAR(8), %s, 108), ':', '" + EW_TIME_SEPARATOR + "'))";
					} else if (dbtype == "ORACLE") {
						dateFormat = "MM" + EW_DATE_SEPARATOR + "DD" + EW_DATE_SEPARATOR + (shortYear ? "YY" : "YYYY");
						if (isDateTime) dateFormat += " HH24" + EW_TIME_SEPARATOR + "MI" + EW_TIME_SEPARATOR + "SS";
					}
					break;
				case 7: case 11: case 14: case 17:
					if (dbtype == "MYSQL") {
						dateFormat = "%d" + EW_DATE_SEPARATOR + "%m" + EW_DATE_SEPARATOR + (shortYear ? "%y" : "%Y");
						if (isDateTime) dateFormat += " %H" + EW_TIME_SEPARATOR + "%i" + EW_TIME_SEPARATOR + "%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = "dd" + EW_DATE_SEPARATOR + "mm" + EW_DATE_SEPARATOR + (shortYear ? "yy" : "yyyy");
						if (isDateTime) dateFormat += " hh" + EW_TIME_SEPARATOR + "nn" + EW_TIME_SEPARATOR + "ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = "REPLACE(" + (shortYear ? "CONVERT(VARCHAR(8), %s, 3)" : "CONVERT(VARCHAR(10), %s, 103)") + ", '/', '" + EW_DATE_SEPARATOR + "')";
						if (isDateTime) dateFormat = "(" + dateFormat + " + ' ' + REPLACE(CONVERT(VARCHAR(8), %s, 108), ':', '" + EW_TIME_SEPARATOR + "'))";
					} else if (dbtype == "ORACLE") {
						dateFormat = "DD" + EW_DATE_SEPARATOR + "MM" + EW_DATE_SEPARATOR + (shortYear ? "YY" : "YYYY");
						if (isDateTime) dateFormat += " HH24" + EW_TIME_SEPARATOR + "MI" + EW_TIME_SEPARATOR + "SS";
					}
					break;
			}
			if (dateFormat != "") {
				if (dbtype == "MYSQL") {
					return "DATE_FORMAT(" + fld + ", '" + dateFormat + "')";
				} else if (dbtype == "ACCESS") {
					return "FORMAT(" + fld + ", '" + dateFormat + "')";
				} else if (dbtype == "MSSQL") {
					return dateFormat.Replace("%s", fld);
				} else if (dbtype == "ORACLE") {
					return "TO_CHAR(" + fld + ", '" + dateFormat + "')";
				}
			}
			return fld;
		}

		// Append like operator
		public static string ew_Like(string pat, string dbid = "DB") {
			string dbtype = ew_GetConnectionType(dbid);
			if (dbtype == "POSTGRESQL") {
				return ((EW_USE_ILIKE_FOR_POSTGRESQL) ? " ILIKE " : " LIKE ") + pat;
			} else if (dbtype == "MYSQL") {
				if (ew_NotEmpty(EW_LIKE_COLLATION_FOR_MYSQL)) {
					return " LIKE " + pat + " COLLATE " + EW_LIKE_COLLATION_FOR_MYSQL;
				} else {
					return " LIKE " + pat;
				}
			} else if (dbtype == "MSSQL") {
				if (ew_NotEmpty(EW_LIKE_COLLATION_FOR_MSSQL)) {
					return " COLLATE " + EW_LIKE_COLLATION_FOR_MSSQL + " LIKE " + pat;
				} else {
					return " LIKE " + pat;
				}
			} else {
				return " LIKE " + pat;
			}
		}

		// Get script name
		public static string ew_ScriptName() {
			return ew_Request.Path;
		}

		// Convert numeric value
		public static object ew_Conv(object v, int t)
		{
			if (Convert.IsDBNull(v) || v == null) // DN
				return System.DBNull.Value;
			switch (t) {
				case 20: // adBigInt
					return Convert.ToInt64(v);
				case 21: // adUnsignedBigInt
					return Convert.ToUInt64(v);
				case 2:
				case 16: // adSmallInt/adTinyInt
					return Convert.ToInt16(v);
				case 3: // adInteger
					return Convert.ToInt32(v);
				case 17:
				case 18: // adUnsignedTinyInt/adUnsignedSmallInt
					return Convert.ToUInt16(v);
				case 19: // adUnsignedInt
					return Convert.ToUInt32(v);
				case 4: // adSingle
					return Convert.ToSingle(v, System.Globalization.CultureInfo.InvariantCulture);
				case 5:
				case 6:
				case 131:
				case 139: // adDouble/adCurrency/adNumeric/adVarNumeric
					return Convert.ToDouble(v, System.Globalization.CultureInfo.InvariantCulture);
				default:
					return v;
			}
		}

		// Convert string to float format string
		public static string ew_StrToFloat(object value)
		{
			string val = Convert.ToString(value);
			val = val.Replace(" ", "");
			if (EW_THOUSANDS_SEP != "")
				val = val.Replace(EW_THOUSANDS_SEP, "");
			val = val.Replace(EW_DECIMAL_POINT, ".");
			return val;
		}

		// Concat string
		public static string ew_Concat(string str1, string str2, string sep)
		{
			str1 = str1.Trim();
			str2 = str2.Trim();
			if (str1 != "" && sep != "" && !str1.EndsWith(sep))
				str1 += sep;
			return str1 + str2;
		}

		// Trace (for debug only)
		public static void ew_Trace(object Msg)
		{
			try {
				string FileName = ew_MapPath("debug.txt");
				StreamWriter sw = File.AppendText(FileName);
				sw.WriteLine(Convert.ToString(Msg));
				sw.Close();
			} catch {
				if (EW_DEBUG_ENABLED) throw;
			}
		}

		// Calculate elapsed time // DN
		public static IHtmlContent ElapsedTime(long tm)
		{
			string str = "";
			if (EW_DEBUG_ENABLED) {
				double endTimer = Environment.TickCount;
				str = "<div class=\"alert alert-info ewAlert\">page processing time: " + Convert.ToString((endTimer - tm) / 1000) + " seconds</div>";
			}
			return new HtmlString(str);
		}

		// Compare values with special handling for null values
		public static bool ew_CompareValue(object v1, object v2)
		{
			if (Convert.IsDBNull(v1) && Convert.IsDBNull(v2)) {
				return true;
			} else if (Convert.IsDBNull(v1) || Convert.IsDBNull(v2)) {
				return false;
			} else {
				return ew_SameStr(v1, v2);
			}
		}

		// Adjust SQL for special characters
		public static string ew_AdjustSql(object value, string dbid = "DB")
		{
			string dbtype = ew_GetConnectionType(dbid);
			if (dbtype == "MYSQL") {
				return Convert.ToString(value).Trim().Replace("'", "\'"); // Adjust for single quote
			} else {
				return Convert.ToString(value).Trim().Replace("'", "''"); // Adjust for single quote
			}
		}

		// Adjust GUID for MS Access // DN
		public static string ew_AdjustGuid(object value, string dbid = "DB")
		{
			var dbtype = ew_GetConnectionType(dbid);
			var str = Convert.ToString(value).Trim();
			if (dbtype == "ACCESS" && !str.StartsWith("{"))
				str = "{" + str + "}"; // Add curly braces
			return str;
		}

		// Build SELECT SQL based on different SQL part
		public static string ew_BuildSelectSql(string sSelect, string sWhere, string sGroupBy, string sHaving, string sOrderBy, string sFilter, string sSort)
		{
			string sDbWhere = sWhere;
			ew_AddFilter(ref sDbWhere, sFilter);
			string sDbOrderBy = sOrderBy;
			if (ew_NotEmpty(sSort))
				sDbOrderBy = sSort;
			string sSql = sSelect;
			if (ew_NotEmpty(sDbWhere))
				sSql += " WHERE " + sDbWhere;
			if (ew_NotEmpty(sGroupBy))
				sSql += " GROUP BY " + sGroupBy;
			if (ew_NotEmpty(sHaving))
				sSql += " HAVING " + sHaving;
			if (ew_NotEmpty(sDbOrderBy))
				sSql += " ORDER BY " + sDbOrderBy;
			return sSql;
		}

		// Load a text file
		public static string ew_LoadTxt(string fn)
		{
			string sTxt = "";
			if (ew_NotEmpty(fn)) {
				fn = ew_MapPath(fn); // Relative to wwwroot
				StreamReader sw = File.OpenText(fn); // Note: The file fn should be UTF-8 encoded text file.
				sTxt = sw.ReadToEnd();
				sw.Close();
			}
			return sTxt;
		}

		// Write audit trail (insert/update/delete)
		public static void ew_WriteAuditTrail(string pfx, string dt, string scrpt, string user, string action, string table = "", string field = "", object keyvalue = null, object oldvalue = null, object newvalue = null)
		{
			if (table == EW_AUDIT_TRAIL_TABLE_NAME)
				return;
			try {
				string usrwrk = user;
				OrderedDictionary rsnew = null;
				if (ew_Empty(usrwrk))
					usrwrk = "-1"; // assume Administrator if no user
				if (EW_AUDIT_TRAIL_TO_DATABASE)
					rsnew = new OrderedDictionary() {{EW_AUDIT_TRAIL_FIELD_NAME_DATETIME, dt}, {EW_AUDIT_TRAIL_FIELD_NAME_SCRIPT, scrpt}, {EW_AUDIT_TRAIL_FIELD_NAME_USER, usrwrk}, {EW_AUDIT_TRAIL_FIELD_NAME_ACTION, action}, {EW_AUDIT_TRAIL_FIELD_NAME_TABLE, table}, {EW_AUDIT_TRAIL_FIELD_NAME_FIELD, field}, {EW_AUDIT_TRAIL_FIELD_NAME_KEYVALUE, Convert.ToString(keyvalue)}, {EW_AUDIT_TRAIL_FIELD_NAME_OLDVALUE, Convert.ToString(oldvalue)}, {EW_AUDIT_TRAIL_FIELD_NAME_NEWVALUE, Convert.ToString(newvalue)}};
				else
					rsnew = new OrderedDictionary() {{"datetime", dt}, {"script", scrpt}, {"user", usrwrk}, {"action", action}, {"table", table}, {"field", field}, {"keyvalue", Convert.ToString(keyvalue)}, {"oldvalue", Convert.ToString(oldvalue)}, {"newvalue", Convert.ToString(newvalue)}};

				// Call AuditTrail Inserting event
				bool bWriteAuditTrail = AuditTrail_Inserting(rsnew);
				if (bWriteAuditTrail) {
					if (!EW_AUDIT_TRAIL_TO_DATABASE) { // Write audit trail to log file
						string sHeader = "date/time" + "\t" + "script" + "\t" + "user" + "\t" + "action" + "\t" + "table" + "\t" + "field" + "\t" + "key value" + "\t" + "old value" + "\t" + "new value";
						string sMsg = rsnew["datetime"] + "\t" + rsnew["script"] + "\t" + rsnew["user"] + "\t" + rsnew["action"] + "\t" + rsnew["table"] + "\t" + rsnew["field"] + "\t" + Convert.ToString(rsnew["keyvalue"]) + "\t" + Convert.ToString(rsnew["oldvalue"]) + "\t" + Convert.ToString(rsnew["newvalue"]);
						string sFolder = EW_AUDIT_TRAIL_PATH;
						string sFn = pfx + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
						bool bWriteHeader = !File.Exists(ew_UploadPathEx(true, sFolder) + sFn);
						StreamWriter sw = File.AppendText(ew_UploadPathEx(true, sFolder) + sFn);
						if (bWriteHeader) sw.WriteLine(sHeader);
						sw.WriteLine(sMsg);
						sw.Close();
					} else if (ew_NotEmpty(EW_AUDIT_TRAIL_TABLE_NAME)) { // DN
						var tbl = ew_CreateInstance("c" + EW_AUDIT_TRAIL_TABLE_VAR);
						if (tbl.Row_Inserting(null, ref rsnew)) {
							if (tbl.Insert(rsnew) > 0)
								tbl.Row_Inserted(null, rsnew);
						}
					}
				}
			} catch {
				if (EW_DEBUG_ENABLED) throw;
			}
		}

		// Unformat date time based on format type
		public static string ew_UnformatDateTime(object ADate, int ANamedFormat)
		{
			string sDT;
			string sDate = Convert.ToString(ADate).Trim();
			if (ew_Empty(sDate)) // DN
				return "";
			if (sDate.StartsWith("#") && sDate.EndsWith("#") && sDate.Length > 2) // MS Access // DN
				sDate = sDate.Substring(1, sDate.Length - 2);
			while (sDate.Contains("  "))
				sDate = sDate.Replace("  ", " ");
			if (Regex.IsMatch(sDate, @"^([0-9]{4})/([0][1-9]|[1][0-2])/([0][1-9]|[1|2][0-9]|[3][0|1])( (0[0-9]|1[0-9]|2[0-3]):([0-5][0-9])(:([0-5][0-9]))?)?$")) // DN
				return sDate;
			ANamedFormat = ew_GetFormatDateTimeID(ANamedFormat);
			var arDateTime = sDate.Split(new char[] { 'T', ' ' }, 2); // DN
			var arDatePt = arDateTime[0].Split(Convert.ToChar(EW_DATE_SEPARATOR));
			if (arDatePt.Length == 3) {
				switch (ANamedFormat) {
					case 5:
					case 9: //yyyymmdd
						if (ew_CheckDate(arDateTime[0])) {
							sDT = arDatePt[0] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[2].PadLeft(2, '0');
							break;
						} else {
							return sDate;
						}
					case 6:
					case 10: //mmddyyyy
						if (ew_CheckUSDate(arDateTime[0])) {
							sDT = arDatePt[2].PadLeft(2, '0') + "/" + arDatePt[0].PadLeft(2, '0') + "/" + arDatePt[1];
							break;
						} else {
							return sDate;
						}
					case 7:
					case 11: //ddmmyyyy
						if (ew_CheckEuroDate(arDateTime[0])) {
							sDT = arDatePt[2].PadLeft(2, '0') + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[0];
							break;
						} else {
							return sDate;
						}
					case 12:
					case 15: //yymmdd
						if (ew_CheckShortDate(arDateTime[0])) {
							arDatePt[0] = ew_UnformatYear(arDatePt[0]);
							sDT = arDatePt[0] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[2].PadLeft(2, '0');
							break;
						} else {
							return sDate;
						}
					case 13:
					case 16: //mmddyy
						if (ew_CheckShortUSDate(arDateTime[0])) {
							arDatePt[2] = ew_UnformatYear(arDatePt[2]);
							sDT = arDatePt[2] + "/" + arDatePt[0].PadLeft(2, '0') + "/" + arDatePt[1].PadLeft(2, '0');
							break;
						} else {
							return sDate;
						}
					case 14:
					case 17: //ddmmyy
						if (ew_CheckShortEuroDate(arDateTime[0])) {
							arDatePt[2] = ew_UnformatYear(arDatePt[2]);
							sDT = arDatePt[2] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[0].PadLeft(2, '0');
							break;
						} else {
							return sDate;
						}
					default:
						return sDate;
				}
				if (arDateTime.Length > 1) {
					arDateTime[1] = arDateTime[1].Trim().Replace(EW_TIME_SEPARATOR, ":");
					if (ew_IsDate(arDateTime[1])) // Is time
						sDT += " " + arDateTime[1];
				}
				return sDT;
			} else {
				if (ANamedFormat == 3 || ANamedFormat == 4)
					sDate = sDate.Replace(EW_TIME_SEPARATOR, ":");
				return sDate;
			}
		}

		// Output SCRIPT tag
		public static void ew_AddClientScript(string src, Dictionary<string, string> attrs = null)
		{
			var atts = new Dictionary<string, string>() {{"type", "text/javascript"}, {"src", src}};
			if (attrs != null) {
				foreach (KeyValuePair<string, string> kvp in attrs)
					atts.Add(kvp.Key, kvp.Value);
			}
			ew_Write(ew_HtmlElement("script", atts, "", true) + "\n");
		}

		// Output LINK tag
		public static void ew_AddStylesheet(string href, Dictionary<string, string> attrs = null)
		{
			var atts = new Dictionary<string, string>() {{"rel", "stylesheet"}, {"type", "text/css"}, {"href", href}};
			if (attrs != null) {
				foreach (KeyValuePair<string, string> kvp in attrs)
					atts.Add(kvp.Key, kvp.Value);
			}
			ew_Write(ew_HtmlElement("link", atts, "", false) + "\n");
		}

		// Is Boolean attribute
		public static bool ew_IsBooleanAttr(string attr) {
			return EW_BOOLEAN_HTML_ATTRIBUTES.Contains(attr, StringComparer.InvariantCultureIgnoreCase);
		}

		// Build HTML element
		public static string ew_HtmlElement(string tagname, Dictionary<string, string> attrs, string innerhtml = "", bool endtag = true)
		{
			string html = "<" + tagname;
			if (attrs != null) {
				foreach (KeyValuePair<string, string> kvp in attrs) {
					if (ew_NotEmpty(kvp.Key) && (ew_NotEmpty(kvp.Value) || ew_IsBooleanAttr(kvp.Key))) { // Allow boolean attributes, e.g. "disabled"
						html += " " + kvp.Key;
						if (ew_NotEmpty(kvp.Value))
							html += "=\"" + ew_HtmlEncode(kvp.Value) + "\"";
					}
				}
			}
			html += ">";
			if (ew_NotEmpty(innerhtml))
				html += innerhtml;
			if (endtag)
				html += "</" + tagname + ">";
			return html;
		}

		// XML tag name
		public static string ew_XmlTagName(string name)
		{
			name = name.Replace(" ", "_");
			var regEx = new Regex(@"^(?!XML)[a-z][\w-]*$", RegexOptions.IgnoreCase);
			if (!regEx.IsMatch(name))
				name = "_" + name;
			return name;
		}

		// Encode HTML
		public static string ew_HtmlEncode(object Expression)
		{
			return WebUtility.HtmlEncode(Convert.ToString(Expression));
		}

		// Decode HTML
		public static string ew_HtmlDecode(object Expression)
		{
			return WebUtility.HtmlDecode(Convert.ToString(Expression));
		}

		// Get title
		public static string ew_HtmlTitle(string name)
		{
			Match m = Regex.Match(name, @"\s+title\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase); // Match title='title'
			Match m1 = Regex.Match(name, @"\s+data-caption\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase); // Match data-caption='caption'
			return (m.Success) ? m.Groups[1].Value: ((m1.Success) ? m1.Groups[1].Value : name);
		}

		// Get title and image
		public static string ew_HtmlImageAndText(string name)
		{
			string title = "";
			if (Regex.IsMatch(name, @"<span([^>]*)>([\s\S]*?)<\/span\s*>", RegexOptions.IgnoreCase) || Regex.IsMatch(name, @"<img([^>]*)>", RegexOptions.IgnoreCase))
				title = ew_HtmlTitle(name);
			else
				title = name;
			return (title != name) ? name + "&nbsp;" + title : name;
		}

		// Encode URL
		public static string ew_UrlEncode(object expression)
		{
			return WebUtility.UrlEncode(Convert.ToString(expression));
		}

		// Encode URL
		public static string ew_RawUrlEncode(object expression)
		{
			return WebUtility.UrlEncode(Convert.ToString(expression)).Replace("+", "%20");
		}

		// Decode URL
		public static string ew_UrlDecode(object expression)
		{
			return WebUtility.UrlDecode(Convert.ToString(expression));
		}

		// Format sequence number
		public static string ew_FormatSeqNo(object seq) {
			return Language.Phrase("SequenceNumber").Replace("%s", Convert.ToString(seq));
		}

		// Encode value for single-quoted JavaScript string
		public static string ew_JsEncode(object val)
		{
			string outstr = Convert.ToString(val).Replace("\\", "\\\\");
			outstr = outstr.Replace("'", "\\'");
			outstr = outstr.Replace("\r\n", "<br>");
			outstr = outstr.Replace("\r", "<br>");
			outstr = outstr.Replace("\n", "<br>");
			return outstr;
		}

		// Encode value for double-quoted JavaScript string
		public static string ew_JsEncode2(object val)
		{
			string outstr = Convert.ToString(val).Replace("\\", "\\\\");
			outstr = outstr.Replace("\"", "\\\"");
			outstr = outstr.Replace("\t", "\\t");
			outstr = outstr.Replace("\r", "\\r");
			outstr = outstr.Replace("\n", "\\n");
			return outstr;
		}

		// Encode value to single-quoted Javascript string for HTML attributes
		public static string ew_JsEncode3(object val)
		{
			string outstr = Convert.ToString(val).Replace("\\", "\\\\");
			outstr = outstr.Replace("'", "\\'");
			outstr = outstr.Replace("\"", "&quot;");
			return outstr;
		}

		// Convert a value to JSON value
		// type: string/boolean

		public static string ew_VarToJson(object val, string type = "", string sep = "\"")
		{
			if (Convert.IsDBNull(val) || val == null) {
				return "null";
			} else if (ew_SameText(type, "number")) {
				return Convert.ToString(val);
			} else if (ew_SameText(type, "boolean") || val is bool) {
				return (ew_ConvertToBool(val)) ? "true" : "false";
			} else if (ew_SameText(type, "string") || val is string) {
				if (sep == "'")
					return "'" + ew_JsEncode3(val) + "'";
				else
					return "\"" + ew_JsEncode2(val) + "\"";
			}
			return Convert.ToString(val);
		}

		// Convert array to JSON for HTML attributes
		public static string ew_ArrayToJsonAttr(Dictionary<string, string> ar)
		{
			var Str = "{";
			foreach (var kvp in ar)
				Str += kvp.Key + ":'" + ew_JsEncode3(kvp.Value) + "',";
			if (Str.EndsWith(","))
				Str = Str.Substring(0, Str.Length - 1);
			Str += "}";
			return Str;
		}

		// Display field value separator
		// idx (int) display field index (1|2|3)
		// fld (object) field object

		public static string ew_ValueSeparator(int idx, cField fld)
		{
			object sep = fld?.DisplayValueSeparator ?? ", ";
			return (ew_IsList(sep)) ? ((string[])sep)[idx - 1] : Convert.ToString(sep);
		}

		// Delimited values separator (for select-multiple or checkbox)
		// idx (int) zero based value index

		public static string ew_ViewOptionSeparator(int idx = -1)
		{
			return ", ";
		}

		// Get temp upload path
		public static string ew_UploadTempPath(string fldvar = "", string tblvar = "")
		{
			string path = (ew_ConvertToBool(EW_UPLOAD_TEMP_PATH)) ? ew_IncludeTrailingDelimiter(EW_UPLOAD_TEMP_PATH, true) : ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH);
			path += EW_UPLOAD_TEMP_FOLDER_PREFIX + ew_Session.SessionID;
			if (ew_NotEmpty(tblvar))
				path += EW_PATH_DELIMITER + tblvar;
			if (ew_NotEmpty(fldvar))
				path += EW_PATH_DELIMITER + fldvar;
			return path;
		}

		// Render upload field to temp path
		public static void ew_RenderUploadField(cField fld, int idx = -1)
		{
			var fldvar = (idx < 0) ? fld.FldVar : fld.FldVar.Substring(0, 1) + idx + fld.FldVar.Substring(1);
			var folder = ew_UploadTempPath(fldvar, fld.TblVar);
			ew_CleanUploadTempPaths(); // Clean all old temp folders
			ew_CleanPath(folder); // Clean the upload folder
			if (!Directory.Exists(folder)) {
				if (!ew_CreateFolder(folder))
					ew_End("Cannot create folder: " + folder);
			}
			var thumbnailfolder = ew_PathCombine(folder, EW_UPLOAD_THUMBNAIL_FOLDER, true);
			if (!Directory.Exists(thumbnailfolder)) {
				if (!ew_CreateFolder(thumbnailfolder))
					ew_End("Cannot create folder: " + thumbnailfolder);
			}
			if (fld.FldDataType == EW_DATATYPE_BLOB) { // Blob field
				if (ew_NotEmpty(fld.Upload.DbValue)) {

					// Create upload file
					var filename = (ew_NotEmpty(fld.Upload.FileName)) ? fld.Upload.FileName : fld.FldVar.Substring(2);
					var f = ew_IncludeTrailingDelimiter(folder, true) + filename;
					ew_CreateUploadFile(ref f, (byte[])fld.Upload.DbValue);

					// Create thumbnail file
					f = ew_IncludeTrailingDelimiter(thumbnailfolder, true) + filename;
					byte[] data = (byte[])fld.Upload.DbValue;
					var width = EW_UPLOAD_THUMBNAIL_WIDTH;
					var height = EW_UPLOAD_THUMBNAIL_HEIGHT;
					ew_ResizeBinary(ref data, ref width, ref height);
					ew_CreateUploadFile(ref f, data);
					fld.Upload.FileName = Path.GetFileName(f); // Update file name
				}
			} else { // Upload to folder
				fld.Upload.FileName = Convert.ToString(fld.Upload.DbValue); // Update file name
				if (ew_NotEmpty(fld.Upload.FileName)) {

					// Create upload file
					var filename = fld.Upload.FileName;
					string[] files;
					if (fld.UploadMultiple)
						files = filename.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
					else
						files = new string[] {filename};
					for (var i = 0; i < files.Length; i++) {
						filename = files[i];
						if (ew_NotEmpty(filename)) {
							var srcfile = ew_UploadPathEx(true, fld.UploadPath) + filename;
							var f = ew_IncludeTrailingDelimiter(folder, true) + filename;
							byte[] data;
							if (!Directory.Exists(srcfile) && File.Exists(srcfile)) {
								data = File.ReadAllBytes(srcfile);
								ew_CreateUploadFile(ref f, data);
							} else {
								ew_CreateImageFromText(Language.Phrase("FileNotFound"), f);
								data = File.ReadAllBytes(f);
							}

							// Create thumbnail file
							f = ew_IncludeTrailingDelimiter(thumbnailfolder, true) + filename;
							var width = EW_UPLOAD_THUMBNAIL_WIDTH;
							var height = EW_UPLOAD_THUMBNAIL_HEIGHT;
							ew_ResizeBinary(ref data, ref width, ref height);
							ew_CreateUploadFile(ref f, data);
						}
					}
				}
			}
		}

		// Write uploaded file
		public static void ew_CreateUploadFile(ref string f, byte[] data)
		{
			File.WriteAllBytes(f, data);
			var ext = Path.GetExtension(f);
			if (ew_Empty(ext)) {
				var ct = ew_ContentType(data.Take(11));
				switch (ct) {
					case "image/gif":
						ew_RenameFile(f, f += ".gif"); break;
					case "image/jpeg":
						ew_RenameFile(f, f += ".jpg"); break;
					case "image/png":
						ew_RenameFile(f, f += ".png"); break;
				}
			}
		}

		// Create image from text
		public static void ew_CreateImageFromText(string txt, string file, int width = EW_UPLOAD_THUMBNAIL_WIDTH, int height = 0, string font = EW_TMP_IMAGE_FONT)
		{
			int pt = (int)Math.Round(EW_FONT_SIZE/1.33); // 1pt = 1.33px
			double fs = Convert.ToDouble(EW_FONT_SIZE);
			int h = (height > 0) ? height : (int)Math.Round(fs / 14 * 20);
			System.Drawing.Image bitmap = new Bitmap(width, h);
			var g = Graphics.FromImage(bitmap);
			g.Clear(Color.White);
			Brush b = new SolidBrush(Color.Red);
			g.DrawString(txt, new System.Drawing.Font(font, pt), b, 0, (int)Math.Round(Convert.ToDouble((h - fs)/2)));
			bitmap.Save(file);
			b.Dispose();
			g.Dispose();
		}

		// Clean temp upload folders
		public static void ew_CleanUploadTempPaths(string sessionid = "")
		{
			var folder = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH);
			if (!Directory.Exists(folder))
				return;
			var dir = new DirectoryInfo(folder);
			var subDirs = dir.GetDirectories(EW_UPLOAD_TEMP_FOLDER_PREFIX + "*");
			foreach (var dirInfo in subDirs) {
				var subfolder = dirInfo.Name;
				var tempfolder = dirInfo.FullName;
				if (EW_UPLOAD_TEMP_FOLDER_PREFIX + sessionid == subfolder) { // Clean session folder
					ew_CleanPath(tempfolder, true);
				} else {
					if (EW_UPLOAD_TEMP_FOLDER_PREFIX + ew_Session.SessionID != subfolder) {
						if (ew_IsEmptyPath(tempfolder)) { // Empty folder
							ew_CleanPath(tempfolder, true);
						} else { // Old folder
							var lastmdtime = dirInfo.CreationTime;
							var files = dirInfo.GetFiles("*.*");
							if (((TimeSpan)(DateTime.Now - lastmdtime)).Minutes / 60 > EW_UPLOAD_TEMP_FOLDER_TIME_LIMIT || files != null && files.Length == 0)
								ew_CleanPath(tempfolder, true);
						}
					}
				}
			}
		}

		// Clean temp upload folder
		public static void ew_CleanUploadTempPath(cField fld, int idx = -1)
		{
			var fldvar = (idx < 0) ? fld.FldVar : fld.FldVar.Substring(0, 1) + idx + fld.FldVar.Substring(1);
			var folder = ew_UploadTempPath(fldvar, fld.TblVar);
			ew_CleanPath(folder, true); // Clean the upload folder

			// Remove table temp folder if empty
			folder = ew_UploadTempPath("", fld.TblVar);
			if (Directory.Exists(folder)) {
				var files = Directory.GetFiles(folder, "*.*", System.IO.SearchOption.AllDirectories);
				if (files != null && files.Length == 0)
					ew_CleanPath(folder, true);
			}

			// Remove complete temp folder if empty
			folder = ew_UploadTempPath();
			if (Directory.Exists(folder)) {
				var files = Directory.GetFiles(folder, "*.*", System.IO.SearchOption.AllDirectories);
				if (files != null && files.Length == 0)
					ew_CleanPath(folder, true);
			}
		}

		// Clean folder
		public static void ew_CleanPath(string folder, bool delete = false)
		{
			folder = ew_IncludeTrailingDelimiter(folder, true);
			try {
				if (Directory.Exists(folder)) {
					ew_GCCollect(); // DN

					// Delete files in the folder
					var files = Directory.GetFiles(folder);
					foreach (string f in files)
						File.Delete(f);

					// Clear sub folders
					var dirs = Directory.GetDirectories(folder);
					foreach (var tempfolder in dirs)
						ew_CleanPath(tempfolder, delete);
					if (delete) {
						Directory.Delete(folder);
					}
				}
			} catch {
				if (EW_DEBUG_ENABLED)
					throw;
			} finally {
				ew_GCCollect(); // DN
			}
		}

		// Check if empty folder
		public static bool ew_IsEmptyPath(string folder)
		{
			var IsEmptyPath = true;

			// Check folder
			folder = ew_IncludeTrailingDelimiter(folder, true);
			if (Directory.Exists(folder)) {
				var files = Directory.GetFiles(folder);
				if (files != null && files.Length > 0)
					return false;
				var dirs = Directory.GetDirectories(folder);
				foreach (var tempfolder in dirs) {
					IsEmptyPath = ew_IsEmptyPath(tempfolder);
					if (!IsEmptyPath)
						return false; // No need to check further
				}
			} else {
				IsEmptyPath = false;
			}
			return IsEmptyPath;
		}

		// Get file count in folder
		public static int ew_FolderFileCount(string folder)
		{
			folder = ew_IncludeTrailingDelimiter(folder, true);
			if (Directory.Exists(folder)) {
				var files = Directory.GetFiles(folder);
				if (files != null)
					return files.Length;
			}
			return 0;
		}

		// Truncate memo field based on specified length, string truncated to nearest space or CrLf
		public static string ew_TruncateMemo(string memostr, int ln, bool removehtml)
		{
			string str = (removehtml) ? ew_RemoveHtml(memostr) : memostr;
			if (str.Length > 0 && str.Length > ln) {
				int k = 0;
				while (k >= 0 && k < str.Length) {
					int i = str.IndexOf(" ", k);
					int j = str.IndexOf("\r\n", k);
					if (i < 0 && j < 0) {	// Unable to truncate
						return str;
					} else {	// Get nearest space or CrLf
						if (i > 0 && j > 0) {
							k = (i < j) ? i : j;
						} else if (i > 0) {
							k = i;
						} else if (j > 0) {
							k = j;
						}

						// Get truncated text
						if (k >= ln) {
							return str.Substring(0, k) + "...";
						} else {
							k = k + 1;
						}
					}
				}
			}
			return str;
		}

		// Remove HTML tags from text
		public static string ew_RemoveHtml(string str)
		{
			return Regex.Replace(str, "<[^>]*>", string.Empty);
		}

		// Extract JavaScript from HTML and return converted script
		public static string ew_ExtractScript(ref string html, string classname = "")
		{
			MatchCollection matches = Regex.Matches(html, @"<script([^>]*)>([\s\S]*?)<\/script\s*>", RegexOptions.IgnoreCase);
			if (matches.Count == 0)
				return "";
			string scripts = "";
			foreach (Match match in matches) {
				if (Regex.IsMatch(match.Groups[1].Value, @"(\s+type\s*=\s*['""]*(text|application)\/(java|ecma)script['""]*)|^((?!\s+type\s*=).)*$", RegexOptions.IgnoreCase)) { // JavaScript
					html = html.Replace(match.Value, ""); // Remove the script from HTML
					scripts += ew_HtmlElement("script", new Dictionary<string, string>() {{"type", "text/html"}, {"class", classname}}, match.Groups[2].Value); // Convert script type and add CSS class, if specified
				}
			}
			return scripts; // Return converted scripts
		}

		// Clean email content
		public static string ew_CleanEmailContent(string content)
		{
			content = content.Replace("class=\"panel panel-default ewGrid\"", "");
			content = content.Replace("class=\"table-responsive ewGridMiddlePanel\"", "");
			content = content.Replace("table ewTable", "ewExportTable");
			return content;
		}

		// Send email
		public static bool ew_SendEmail(string sFrEmail, string sToEmail, string sCcEmail, string sBccEmail, string sSubject, string sMail, string sFormat, string sCharset, bool bSsl = false, List<string> arAttachments = null, List<string> arImages = null, SmtpClient smtp = null)
		{
			var mail = new MailMessage();
			if (ew_NotEmpty(sFrEmail)) {
				var m = Regex.Match(sFrEmail.Trim(), @"^(.+)<([\w.%+-]+@[\w.-]+\.[A-Z]{2,6})>$", RegexOptions.IgnoreCase);
				if (m.Success) {
					mail.From = new MailAddress(m.Groups[2].Value, m.Groups[1].Value);
				} else {
					mail.From = new MailAddress(sFrEmail);
				}
			}
			if (ew_NotEmpty(sToEmail)) {
				sToEmail = sToEmail.Replace(',', ';');
				string[] arTo = sToEmail.Split(';');
				foreach (string strTo in arTo)
					mail.To.Add(strTo);
			}
			if (ew_NotEmpty(sCcEmail)) {
				sCcEmail = sCcEmail.Replace(',', ';');
				string[] arCC = sCcEmail.Split(';');
				foreach (string strCC in arCC)
					mail.CC.Add(strCC);
			}
			if (ew_NotEmpty(sBccEmail)) {
				sBccEmail = sBccEmail.Replace(',', ';');
				string[] arBcc = sBccEmail.Split(';');
				foreach (string strBcc in arBcc)
					mail.Bcc.Add(strBcc);
			}
			mail.Subject = sSubject;
			mail.IsBodyHtml = ew_SameText(sFormat, "html");
			mail.Body = sMail;
			Encoding enc = null;
			if (ew_NotEmpty(sCharset) && !ew_SameText(sCharset, "us-ascii")) { // DN
				enc = Encoding.GetEncoding(sCharset);
				mail.BodyEncoding = enc;
			}
			if (smtp == null) {
				smtp = new SmtpClient();
				smtp.Host = ew_NotEmpty(EW_SMTP_SERVER) ? EW_SMTP_SERVER : "localhost";
				if (EW_SMTP_SERVER_PORT > 0)
					smtp.Port = EW_SMTP_SERVER_PORT;
				smtp.EnableSsl = bSsl;
				if (ew_NotEmpty(EW_SMTP_SERVER_USERNAME) && ew_NotEmpty(EW_SMTP_SERVER_PASSWORD)) {
					var smtpuser = new NetworkCredential();
					smtpuser.UserName = EW_SMTP_SERVER_USERNAME;
					smtpuser.Password = EW_SMTP_SERVER_PASSWORD;
					smtp.UseDefaultCredentials = false;
					smtp.Credentials = smtpuser;
				}
			}
			if (arAttachments != null && arAttachments.Count > 0) {
				foreach (string fn in arAttachments)
					mail.Attachments.Add(new Attachment(fn));
			}
			if (mail.IsBodyHtml && arImages != null && arImages.Count > 0) {
				string html = mail.Body;
				foreach (string tmpimage in arImages) {
					string file = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH) + tmpimage;
					string cid = ew_TmpImageLnk(tmpimage, "email");
					html = html.Replace(file, cid);
				}
				var htmlview = AlternateView.CreateAlternateViewFromString(html, enc, "text/html");
				foreach (string tmpimage in arImages) {
					string file = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH) + tmpimage;
					if (File.Exists(file)) {
						LinkedResource res = new LinkedResource(file);
						res.ContentId = ew_TmpImageLnk(tmpimage, "cid");
						htmlview.LinkedResources.Add(res);
					}
				}
				mail.AlternateViews.Add(htmlview);
			}
			try {
				smtp.Send(mail);
				return true;
			} catch (Exception e) {
				gsEmailErrDesc = e.Message;
				if (EW_DEBUG_ENABLED)
					throw;
				return false;
			}
		}

		// Return path of the uploaded file relative to wwwroot // DN
		public static string ew_UploadPathEx(bool PhyPath, string DestPath)
		{
			return ew_MapPath(DestPath, PhyPath); // DN
		}

		// Change the file name of the uploaded file
		public static string ew_UploadFileNameEx(string Folder, string FileName)
		{
			string OutFileName;

			// By default, ewUniqueFileName() is used to get an unique file name.
			// Amend your logic here

			OutFileName = ew_UniqueFileName(Folder, FileName);

			// Return computed output file name
			return OutFileName;
		}

		// Return path of the uploaded file
		// returns global upload folder, for backward compatibility only

		public static string ew_UploadPath(bool PhyPath)
		{
			return ew_UploadPathEx(PhyPath, EW_UPLOAD_DEST_PATH);
		}

		// Change the file name of the uploaded file
		// use global upload folder, for backward compatibility only

		public static string ew_UploadFileName(string FileName)
		{
			return ew_UploadFileNameEx(ew_UploadPath(true), FileName);
		}

		// Generate an unique file name (filename(n).ext)
		public static string ew_UniqueFileName(string folder, string orifn, bool indexed = false)
		{
			if (ew_Empty(orifn))
				orifn = DateTime.Now.ToString("yyyyMMddHHmmss") + ".bin";
			if (orifn == ".")
				throw new Exception("Invalid file name: " + orifn);
			if (ew_Empty(folder))
				throw new Exception("Unspecified folder");
			string ext = Path.GetExtension(orifn);
			string oldfn = Path.GetFileNameWithoutExtension(orifn);
			folder = ew_IncludeTrailingDelimiter(folder, true);
			if (!Directory.Exists(folder) && !ew_CreateFolder(folder))
				throw new Exception("Folder does not exist: " + folder);
			int i = 0;
			string suffix = "";
				if (indexed) {
						Match m = Regex.Match(@"\(\d+\)$", oldfn);
				if (m.Success) {
								i = ew_ConvertToInt(m.Groups[1].Value);
								i++;
						} else {
								i = 1;
						}
						suffix = "(" + Convert.ToString(i) + ")";
				}

			// Check to see if filename exists
			string name = Regex.Replace(oldfn, @"\(\d+\)$", ""); // Remove "(n)" at the end of the file name
			while (File.Exists(folder + name + suffix + ext)) {
				i++;
				suffix = "(" + Convert.ToString(i) + ")";
			}
			return name + suffix + ext;
		}

		// Field data type
		public static int ew_FieldDataType(int fldtype) {
			switch (fldtype) {
				case 20:
				case 3:
				case 2:
				case 16:
				case 4:
				case 5:
				case 131:
				case 139:
				case 6:
				case 17:
				case 18:
				case 19:
				case 21: // Numeric
					return EW_DATATYPE_NUMBER;
				case 7:
				case 133:
				case 135: // Date
				case 146: // DateTiemOffset
					return EW_DATATYPE_DATE;
				case 134: // Time
				case 145: // Time
					return EW_DATATYPE_TIME;
				case 201:
				case 203: // Memo
					return EW_DATATYPE_MEMO;
				case 129:
				case 130:
				case 200:
				case 202: // String
					return EW_DATATYPE_STRING;
				case 11: // Boolean
					return EW_DATATYPE_BOOLEAN;
				case 72: // GUID
					return EW_DATATYPE_GUID;
				case 128:
				case 204:
				case 205: // Binary
					return EW_DATATYPE_BLOB;
				case 141: // XML
					return EW_DATATYPE_XML;
				default:
					return EW_DATATYPE_OTHER;
			}
		}

		// Current path (returns wwwroot) // DN
		public static string ew_CurrentPath(bool PhyPath = true)
		{
			string p = "";
			if (PhyPath) { // Physical path
				p = ew_WebRootPath;
			} else { // Path relative to host
				p = ew_Request.PathBase.ToString();
			}
			return ew_IncludeTrailingDelimiter(p, PhyPath);
		}

		// Web root (returns wwwroot, NOT approot) // DN
		public static string ew_AppRoot(bool PhyPath = true)
		{
			return ew_CurrentPath(PhyPath);
		}

		// Get path relative to wwwroot // DN
		public static string ew_MapPath(string p, bool PhyPath = true)
		{
			if (Path.IsPathRooted(p) || p.ToLower().StartsWith("http:") || p.ToLower().StartsWith("https:"))
				return p;
			p = p.Trim().Replace("\\", "/");
			p = Regex.Replace(p, @"^[~/]+", ""); // DN
			return ew_PathCombine(ew_AppRoot(PhyPath), p, PhyPath); // relative to wwwroot
		}

		// Get physical path relative to wwwroot (for backward compatibility) // DN
		public static string ew_ServerMapPath(string p) {
			return ew_MapPath(p, true);
		}

		// Get path relative to a base path
		public static string ew_PathCombine(string BasePath, string RelPath, bool PhyPath)
		{
			string Delimiter = (PhyPath) ? EW_PATH_DELIMITER : "/";
			if (BasePath != Delimiter) // If BasePath = root, do not remove delimiter
				BasePath = ew_RemoveTrailingDelimiter(BasePath, PhyPath);
			RelPath = (PhyPath) ? RelPath.Replace("/", EW_PATH_DELIMITER) : RelPath.Replace("\\", "/");
			if (RelPath.EndsWith(".")) // DN
				RelPath = ew_IncludeTrailingDelimiter(RelPath, PhyPath);
			int p1 = RelPath.IndexOf(Delimiter);
			string Path2 = "";
			while (p1 > -1) {
				string Path = RelPath.Substring(0, p1 + 1);
				if (Path == Delimiter || Path == "." + Delimiter) { // Skip
				} else if (Path == ".." + Delimiter) {
					int p2 = BasePath.LastIndexOf(Delimiter);
					if (p2 == 0) // BasePath = "/xxx", cannot move up
						BasePath = Delimiter;
					else if (p2 > -1 && !BasePath.EndsWith(".."))
						BasePath = BasePath.Substring(0, p2);
					else if (ew_NotEmpty(BasePath) && BasePath != "..")
						BasePath = "";
					else
						Path2 += ".." + Delimiter;
				} else {
					Path2 += Path;
				}
				try {
					RelPath = RelPath.Substring(p1 + 1);
				} catch {
					RelPath = "";
				}
				p1 = RelPath.IndexOf(Delimiter);
			}
			return ((ew_NotEmpty(BasePath) && BasePath != ".") ? ew_IncludeTrailingDelimiter(BasePath, PhyPath) : "") + Path2 + RelPath;
		}

		// Remove the last delimiter for a path
		public static string ew_RemoveTrailingDelimiter(string Path, bool PhyPath)
		{
			string Delimiter = (PhyPath) ? EW_PATH_DELIMITER : "/";
			while (Path.EndsWith(Delimiter))
				Path = Path.Substring(0, Path.Length - 1);
			return Path;
		}

		// Include the last delimiter for a path
		public static string ew_IncludeTrailingDelimiter(string Path, bool PhyPath)
		{
			string Delimiter = (PhyPath) ? EW_PATH_DELIMITER : "/";
			Path = ew_RemoveTrailingDelimiter(Path, PhyPath);
			return Path + Delimiter;
		}

		// Write the paths for config/debug only
		public static void ew_WriteUploadPaths()
		{
			ew_Write("wwwroot: " + ew_AppRoot() + "<br>");
			ew_Write("Global upload folder (relative to wwwroot): " + EW_UPLOAD_DEST_PATH + "<br>");
			ew_Write("Global upload folder (physical): " + ew_UploadPath(true) + "<br>");
		}

		// Write info for config/debug only
		public static void ew_Info()
		{
			ew_WriteUploadPaths();
			ew_Write("ew_User.Identity.Name = " + Convert.ToString(ew_User?.Identity?.Name) + "<br>");
			ew_Write("CurrentUserName() = " + CurrentUserName() + "<br>");
			ew_Write("CurrentUserID() = " + CurrentUserID() + "<br>");
			ew_Write("CurrentParentUserID() = " + CurrentParentUserID() + "<br>");
			ew_Write("IsLoggedIn() = " + (IsLoggedIn() ? "TRUE" : "FALSE") + "<br>");
			ew_Write("IsAdmin() = " + (IsAdmin() ? "TRUE" : "FALSE") + "<br>");
			ew_Write("IsSysAdmin() = " + (IsSysAdmin() ? "TRUE" : "FALSE") + "<br>");
			Security?.ShowUserLevelInfo();
		}

		// Get current page name only
		public static string ew_CurrentPage()
		{
			return Convert.ToString(ew_Controller?.RouteData.Values["action"]); // Returns action (file name) only
		}

		// Get refer URL
		public static string ew_ReferUrl()
		{
			string url = ew_Request.Headers[HeaderNames.Referer];
			if (ew_NotEmpty(url)) {
				var p = ew_Request.Host.ToString() + ew_Request.PathBase.ToString() + "/";
				var i = url.LastIndexOf(p);
				if (i > -1)
					url = url.Substring(i + p.Length); // Remove path base
				return url;
			}
			return "";
		}

		// Get refer page name
		public static string ew_ReferPage()
		{
			return ew_GetPageName(ew_ReferUrl());
		}

		// Get page name // DN
		// param url, must contain action only, e.g. /xxxlist, not /xxx/list // DN

		public static string ew_GetPageName(string url)
		{
			if (ew_NotEmpty(url)) {
				if (url.Contains("?"))
					url = url.Substring(0, url.LastIndexOf("?")); // Remove querystring first
				var p = ew_Request.Host.ToString() + ew_Request.PathBase.ToString() + "/";
				var i = url.LastIndexOf(p);
				if (i > -1)
					url = url.Substring(i + p.Length); // Remove path base
				if (EW_AREA_NAME != "" && url.StartsWith(EW_AREA_NAME + "/"))
					url = url.Substring(EW_AREA_NAME.Length + 1); // Remove area
				var ar = url.Split('/');
				return ar[0]; // Remove parameters
			}
			return "";
		}

		// Get full URL
		public static string ew_FullUrl()
		{
			var request = ew_Request;
			return string.Concat(request.Scheme, "://",
				request.Host.ToString(),
				request.PathBase.ToString(),
				request.Path.ToString(),
				request.QueryString.ToString());
		}

		// Get CSS file
		public static string ew_CssFile(string f) {
			if (EW_CSS_FLIP)
				return Regex.Replace(f, @"(.css)$", "-rtl.css", RegexOptions.IgnoreCase);
			else
				return f;
		}

		// Check if HTTPS
		public static bool ew_IsHttps() {
			return ew_Request.IsHttps;
		}

		// Get current URL
		public static string ew_CurrentUrl()
		{
			var request = ew_Request;
			return string.Concat(request.PathBase.ToString(),
				request.Path.ToString(),
				request.QueryString.ToString());
		}

		// Get application path (relative to host)
		public static string ew_AppPath(string url = "", bool useArea = true) // DN
		{
			if (url.StartsWith("javascript:"))
				return url;
			var pathBase = ew_Request.PathBase.ToString();
			pathBase = ew_IncludeTrailingDelimiter(pathBase, false);
			if (ew_Empty(url)) {
				return pathBase;
			} else {
				if (useArea && EW_AREA_NAME != "") {
					if (url.StartsWith(EW_AREA_NAME + "/"))
						url = url.Substring(EW_AREA_NAME.Length + 1);
					pathBase += EW_AREA_NAME + "/";
				}
				return url.StartsWith(pathBase) || url.StartsWith("cid:") ? url : pathBase + url;
			}
		}

		// Convert to full URL
		public static string ew_ConvertFullUrl(string url)
		{
			if (ew_Empty(url)) {
				return "";
			} else if (url.Contains("://")) {
				return url;
			} else {
				return string.Concat(ew_Request.Scheme, "://",
					ew_Request.Host.ToString(),
					ew_AppPath(url));
			}
		}

		// Get relative URL
		public static string ew_GetUrl(string url)
		{
			return url; // No relative URL in MVC // DN
		}

		// Check if responsive layout
		public static bool ew_IsResponsiveLayout()
		{
			return EW_USE_RESPONSIVE_LAYOUT;
		}

		// Check if folder exists
		public static bool ew_FolderExists(string folder)
		{
			return Directory.Exists(folder);
		}

		// Check if file exists
		public static bool ew_FileExists(string file, string folder = "")
		{
			if (folder == "") { // "file" is full path
				return File.Exists(file);
			} else { // "file" is under "folder"
				return File.Exists(ew_IncludeTrailingDelimiter(folder, true) + file);
			}
		}

		// Delete file
		public static void ew_DeleteFile(string FilePath)
		{
			try {
				if (File.Exists(FilePath))
					File.Delete(FilePath);
			} finally {
				ew_GCCollect(); // DN
			}
		}

		// Rename file
		public static void ew_RenameFile(string OldFile, string NewFile)
		{
			ew_GCCollect(); // DN
			File.Move(OldFile, NewFile);
		}

		// Copy file // DN
		public static void ew_CopyFile(string SrcFile, string DestFile)
		{
			ew_GCCollect(); // DN
			File.Copy(SrcFile, DestFile);
		}

		// Copy file // DN
		public static bool ew_CopyFile(string folder, string fn, string file, bool overwrite) {
			if (File.Exists(file)) {
				var newfile = ew_UploadPathEx(true, folder) + fn;
				if (ew_CreateFolder(Path.GetDirectoryName(newfile))) {
					ew_GCCollect(); // DN
					File.Copy(file, newfile, overwrite);
					return true;
				}
			}
			return false;
		}

		// Create folder
		public static bool ew_CreateFolder(string folder)
		{
			try {
				DirectoryInfo di = Directory.CreateDirectory(folder);
				return (di != null);
			} catch {
				return false;
			}
		}

		// Check if field exists in a data reader
		public static bool ew_InDataReader(ref DbDataReader dr, string FldName)
		{
			try {
				return (dr.GetOrdinal(FldName) >= 0);
			} catch {
				return false;
			}
		}

		// Calculate Hash for recordset // ASP.NET
		public static string ew_GetFldHash(object Value, int FldType)
		{
			return MD5(ew_GetFldValueAsString(Value, FldType));
		}

		// Get field value as string
		public static string ew_GetFldValueAsString(object Value, int FldType)
		{
			if (Convert.IsDBNull(Value)) {
				return "";
			} else if (FldType == EW_DATATYPE_BLOB) {
				return ew_ByteToString((byte[])Value, EW_BLOB_FIELD_BYTE_COUNT);
			} else {
				return Convert.ToString(Value);
			}
		}

		// Convert byte to string
		public static string ew_ByteToString(object b, int l)
		{
			if (EW_BLOB_FIELD_BYTE_COUNT > 0) {
				return BitConverter.ToString((byte[])b, 0, EW_BLOB_FIELD_BYTE_COUNT);
			} else {
				return BitConverter.ToString((byte[])b);
			}
		}

		// Create temp image file from binary data
		public static string ew_TmpImage(byte[] filedata) {
			if (filedata == null)
				return ""; // DN
			string export = "";
			if (ew_NotEmpty(ew_Get("export")))
				export = ew_Get("export");
			else if (ew_NotEmpty(ew_Post("export")))
				export = ew_Post("export");
			else if (ew_NotEmpty(ew_Post("exporttype")))
				export = ew_Post("exporttype");
			string folder = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH);
			string f = folder + Path.GetRandomFileName();
			using (var ms = new MemoryStream(filedata)) {
				try {
					using (var img = System.Drawing.Image.FromStream(ms)) { // DN
						if (img.RawFormat.Equals(ImageFormat.Gif)) {
							f = Path.ChangeExtension(f, ".gif");
						} else if (img.RawFormat.Equals(ImageFormat.Jpeg)) {
							f = Path.ChangeExtension(f, ".jpg");
						} else if (img.RawFormat.Equals(ImageFormat.Png)) {
							f = Path.ChangeExtension(f, ".png");
						} else if (img.RawFormat.Equals(ImageFormat.Bmp)) {
							f = Path.ChangeExtension(f, ".bmp");
						} else {
							return "";
						}
						img.Save(f);
					}
				} catch {}
			}
			string tmpimage = Path.GetFileName(f);
			gTmpImages.Add(tmpimage);
			return ew_TmpImageLnk(tmpimage, export);
		}

		// Garbage collection
		public static void ew_GCCollect() {

			// Force garbage collection
			GC.Collect();

			// Wait for all finalizers to complete before continuing.
			// Without this call to GC.WaitForPendingFinalizers,
			// the worker loop below might execute at the same time
			// as the finalizers.
			// With this call, the worker loop executes only after
			// all finalizers have been called.

			GC.WaitForPendingFinalizers();

			// Do another garbage collection
			GC.Collect();
		}

		// Delete temp images
		public static void ew_DeleteTmpImages() {
			ew_GCCollect();
			if (gTmpImages != null) {
				string folder = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH);
				foreach (string tmpimage in gTmpImages) {
					string f = folder + tmpimage;
					ew_DeleteFile(f);
				}
				gTmpImages.Clear(); // DN
			}
		}

		// Create temp file
		public static string ew_TmpFile(string afile) {
			if (File.Exists(afile)) { // Copy only
				string export = "";
				if (ew_NotEmpty(ew_Get("export")))
					export = ew_Get("export");
				else if (ew_NotEmpty(ew_Post("export")))
					export = ew_Post("export");
				string folder = ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH);
				string f = folder + Path.GetRandomFileName();
				string ext = Path.GetExtension(afile);
				if (ew_NotEmpty(ext))
					f += ext;
				ew_CopyFile(afile, f);
				string tmpimage = Path.GetFileName(f);
				gTmpImages.Add(tmpimage);
				return ew_TmpImageLnk(tmpimage, export);
			} else {
				return "";
			}
		}

		// Get temp image link
		public static string ew_TmpImageLnk(string file, string lnktype = "") {
			if (ew_Empty(file))
				return "";
			if (lnktype == "email" || lnktype == "cid") {
				string[] ar = file.Split('.');
				var list = new List<string>(ar);
				list.RemoveAt(list.Count - 1);
				ar = list.ToArray();
				string lnk = String.Join(".", ar);
				if (lnktype == "email") lnk = "cid:" + lnk;
				return lnk;
			} else {
				return ew_UploadPathEx(true, EW_UPLOAD_DEST_PATH) + file; // Use physical path // DN
			}
		}

		// Invoke static method project class // DN
		public static object ew_Invoke(string name, object[] parameters = null) {
			MethodInfo mi = typeof(DEX).GetMethod(name);
			return mi?.Invoke(null, parameters);
		}

		// Invoke method of an object // DN
		public static object ew_Invoke(object obj, string name, object[] parameters = null) {
			MethodInfo mi = obj.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
			return mi?.Invoke(obj, parameters);
		}

		// Web request // DN
		public static byte[] ew_WebRequest(string url, string postData = "") {
			byte[] buffer = new byte[4096];
			WebRequest request = WebRequest.Create(url);
			if (ew_NotEmpty(postData)) {
				request.Method = "POST";
				byte[] byteArray = Encoding.UTF8.GetBytes(postData);
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = byteArray.Length;
				using (Stream dataStream = request.GetRequestStream()) {
					dataStream.Write(byteArray, 0, byteArray.Length);
				}
			}
			using (WebResponse response = request.GetResponse()) {
				using (Stream responseStream = response.GetResponseStream()) {
					using (MemoryStream memoryStream = new MemoryStream()) {
						int count = 0;
						do {
							count = responseStream.Read(buffer, 0, buffer.Length);
							memoryStream.Write(buffer, 0, count);
						} while(count != 0);
						return memoryStream.ToArray();
					}
				}
			}
		}

		// Get Hash URL
		public static string ew_GetHashUrl(string url, string hash) {
			return url + "#" + hash;
		}

		// Add querystring to url
		public static string ew_AddQueryStringToUrl(string url, string qry) {
			return url + (url.IndexOf("?") >= 0 ? "&" : "?") + qry;
		}

		//
		// Form object class
		//

		public class cFormObj
		{
			public int Index = -1; // Index to handle multiple form elements
			public string FormName = "";

			// Get form element name based on index
			public string GetIndexedName(string name)
			{
				int Pos;
				if (Index < 0) {
					return name;
				} else {
					Pos = name.IndexOf("_");
					if (Pos == 1 || Pos == 2) {
						return name.Substring(0, Pos) + Index + name.Substring(Pos);
					} else {
						return name;
					}
				}
			}

			// Has value for form element
			public bool HasValue(string name) {
				string wrkname = GetIndexedName(name);
				if (Regex.IsMatch(name, @"^(fn_)?(x|o)\d*_") && FormName != "") {
					if (ew_Form.ContainsKey(FormName + "$" + wrkname))
						return true;
				}
				return ew_Form.ContainsKey(wrkname);
			}

			// Get value for form element
			public string GetValue(string name)
			{
				string wrkname = GetIndexedName(name);
				var value = ew_Post(wrkname);
				if (Regex.IsMatch(name, @"^(fn_)?(x|o)\d*_") && FormName != "") {
					wrkname = FormName + "$" + wrkname;
					if (ew_Form.ContainsKey(wrkname))
						value = ew_Post(wrkname);
				}
				return value;
			}

			// Get upload file size
			public long GetUploadFileSize(string name)
			{
				string wrkname = GetIndexedName(name);
				if (ew_Files[wrkname] != null) {
					return ew_Files[wrkname].Length;
				} else {
					return -1;
				}
			}

			// Get upload file name
			public string GetUploadFileName(string name)
			{
				string wrkname = GetIndexedName(name);
				if (ew_Files[wrkname] != null) {
					var file = ew_Files[wrkname];
					var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
					return Path.GetFileName(fileName);
				} else {
					return "";
				}
			}

			// Get file content type
			public string GetUploadFileContentType(string name)
			{
				string wrkname = GetIndexedName(name);
				if (ew_Files[wrkname] != null) {
					var ct = ew_Files[wrkname].ContentType;
					var ct2 = ew_ContentType(GetUploadFileName(name));
					return (ct2 != "") ? ct2 : ct;
				} else {
					return "";
				}
			}

			// Get upload file data
			public object GetUploadFileData(string name)
			{
				string wrkname = GetIndexedName(name);
				var file = ew_Files[wrkname];
				if (file != null && file.Length > 0) {
					int filelength = (int)file.Length;
					byte[] bindata = new byte[filelength];
					Stream fs = file.OpenReadStream();
					fs.Seek(0, SeekOrigin.Begin);
					fs.Read(bindata, 0, filelength);
					return bindata;
				} else {
					return System.DBNull.Value;
				}
			}

			// Get file image width
			public int GetUploadImageWidth(string name)
			{
				string wrkname = GetIndexedName(name);
				var file = ew_Files[wrkname];
				if (file != null && file.Length > 0) {
					try {
						System.Drawing.Image img = System.Drawing.Image.FromStream(file.OpenReadStream());
						return Convert.ToInt32(img.PhysicalDimension.Width);
					} catch {
						return -1;
					}
				} else {
					return -1;
				}
			}

			// Get file image height
			public int GetUploadImageHeight(string name)
			{
				string wrkname = GetIndexedName(name);
				var file = ew_Files[wrkname];
				if (file != null && file.Length > 0) {
					try {
						System.Drawing.Image img = System.Drawing.Image.FromStream(file.OpenReadStream());
						return Convert.ToInt32(img.PhysicalDimension.Height);
					} catch {
						return -1;
					}
				} else {
					return -1;
				}
			}
		}

		//
		// Advanced Security class
		//

		public class cAdvancedSecurityBase {
			private List<string[]> UserLevel = new List<string[]>();
			private List<string[]> UserLevelPriv = new List<string[]>();
			public List<int> UserLevelID = new List<int>();
			public List<string> UserID = new List<string>();
			public int CurrentUserLevelID;
			public int CurrentUserLevel;
			public string CurrentUserID;
			public string CurrentParentUserID;
			public List<Claim> Claims = new List<Claim>(); // DN

			// Init
			public cAdvancedSecurityBase() {

				// User table
				// Init User Level

				if (IsLoggedIn) {
					CurrentUserLevelID = SessionUserLevelID;
					if (ew_IsNumeric(CurrentUserLevelID)) {
						if (CurrentUserLevelID >= -2)
							UserLevelID.Add(CurrentUserLevelID);
					}
				} else { // Anonymous user
					CurrentUserLevelID = -2;
					UserLevelID.Add(CurrentUserLevelID);
				}
				ew_Session[EW_SESSION_USER_LEVEL_LIST] = UserLevelList();

				// Init User ID
				CurrentUserID = Convert.ToString(SessionUserID);
				CurrentParentUserID = Convert.ToString(SessionParentUserID);

				// Load user level
				LoadUserLevel();
			}

			// Session User ID
			public object SessionUserID {
				get { return Convert.ToString(ew_Session[EW_SESSION_USER_ID]); }
				set {
					ew_Session[EW_SESSION_USER_ID] = Convert.ToString(value).Trim();
					CurrentUserID = Convert.ToString(value).Trim();
				}
			}

			// Session parent User ID
			public object SessionParentUserID {
				get { return Convert.ToString(ew_Session[EW_SESSION_PARENT_USER_ID]); }
				set {
					ew_Session[EW_SESSION_PARENT_USER_ID] = Convert.ToString(value).Trim();
					CurrentParentUserID = Convert.ToString(value).Trim();
				}
			}

			// Current user name
			public string CurrentUserName {
				get { return Convert.ToString(ew_Session[EW_SESSION_USER_NAME]); }
				set { ew_Session[EW_SESSION_USER_NAME] = value; }
			}

			// Session User Level ID
			public int SessionUserLevelID {
				get { return Convert.ToInt32(ew_Session[EW_SESSION_USER_LEVEL_ID]); }
				set {
					ew_Session[EW_SESSION_USER_LEVEL_ID] = value;
					CurrentUserLevelID = value;
					if (ew_IsNumeric(CurrentUserLevelID)) {
						if (CurrentUserLevelID >= -2) {
							UserLevelID.Clear();
							UserLevelID.Add(CurrentUserLevelID);
						}
					}
				}
			}

			// Session User Level value
			public int SessionUserLevel {
				get { return Convert.ToInt32(ew_Session[EW_SESSION_USER_LEVEL]); }
				set {
					ew_Session[EW_SESSION_USER_LEVEL] = value;
					CurrentUserLevel = value;
					if (ew_IsNumeric(CurrentUserLevelID)) {
						if (CurrentUserLevelID >= -1) {
							UserLevelID.Clear();
							UserLevelID.Add(CurrentUserLevelID);
						}
					}
				}
			}

			// Can add
			public bool CanAdd {
				get { return ((CurrentUserLevel & EW_ALLOW_ADD) == EW_ALLOW_ADD); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_ADD);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_ADD));
					}
				}
			}

			// Can delete
			public bool CanDelete {
				get { return ((CurrentUserLevel & EW_ALLOW_DELETE) == EW_ALLOW_DELETE); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_DELETE);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_DELETE));
					}
				}
			}

			// Can edit
			public bool CanEdit {
				get { return ((CurrentUserLevel & EW_ALLOW_EDIT) == EW_ALLOW_EDIT); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_EDIT);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_EDIT));
					}
				}
			}

			// Can view
			public bool CanView {
				get { return ((CurrentUserLevel & EW_ALLOW_VIEW) == EW_ALLOW_VIEW); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_VIEW);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_VIEW));
					}
				}
			}

			// Can list
			public bool CanList {
				get { return ((CurrentUserLevel & EW_ALLOW_LIST) == EW_ALLOW_LIST); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_LIST);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_LIST));
					}
				}
			}

			// Can report
			public bool CanReport {
				get { return ((CurrentUserLevel & EW_ALLOW_REPORT) == EW_ALLOW_REPORT); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_REPORT);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_REPORT));
					}
				}
			}

			// Can search
			public bool CanSearch {
				get { return ((CurrentUserLevel & EW_ALLOW_SEARCH) == EW_ALLOW_SEARCH); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_SEARCH);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_SEARCH));
					}
				}
			}

			// Can admin
			public bool CanAdmin {
				get { return ((CurrentUserLevel & EW_ALLOW_ADMIN) == EW_ALLOW_ADMIN); }
				set {
					if (value) {
						CurrentUserLevel = (CurrentUserLevel | EW_ALLOW_ADMIN);
					} else {
						CurrentUserLevel = (CurrentUserLevel & (~EW_ALLOW_ADMIN));
					}
				}
			}

			// Last URL
			public string LastUrl {
				get { return ew_Cookie["lasturl"]; }
			}

			// Save last URL
			public void SaveLastUrl()
			{
				string s = ew_Request.PathBase.ToString() + ew_Request.Path.ToString();
				string q = ew_Request.QueryString.ToString();
				if (ew_NotEmpty(q)) s += q;
				if (LastUrl == s) s = "";
				ew_Cookie["lasturl"] = s;
			}

			// Auto login
			public bool AutoLogin()
			{
				bool bValid = false, enc;
				string sUsr = "", sPwd = "";
				if (!bValid && ew_SameStr(ew_Cookie["autologin"], "autologin")) {
					sUsr = ew_Cookie["username"];
					sPwd = ew_Cookie["password"];
					sUsr = ew_Decrypt(sUsr);
					sPwd = ew_Decrypt(sPwd);
					bValid = ValidateUser(ref sUsr, ref sPwd, true, false);
				}
				if (!bValid && EW_ALLOW_LOGIN_BY_URL && ew_Get("username") != "") {
					sUsr = ew_RemoveXSS(ew_Get("username"));
					sPwd = ew_RemoveXSS(ew_Get("password"));
					enc = ew_NotEmpty(ew_Get("encrypted"));
					bValid = ValidateUser(ref sUsr, ref sPwd, true, enc);
				}
				if (!bValid && EW_ALLOW_LOGIN_BY_SESSION && ew_Session["Username"] != null) {
					sUsr = Convert.ToString(ew_Session["Username"]);
					sPwd = Convert.ToString(ew_Session["Password"]);
					enc = !ew_Empty(ew_Session["Encrypted"]);
					bValid = ValidateUser(ref sUsr, ref sPwd, true, enc);
				}
				return bValid;
			}

			// Login user
			public void LoginUser(string userName = null, object userID = null, object parentUserID = null, int? userLevel = null)
			{
				ew_Session[EW_SESSION_STATUS] = "login";
				if (userName != null)
					CurrentUserName = userName;
				if (userID != null)
					SessionUserID = userID;
				if (parentUserID != null)
					SessionParentUserID = parentUserID;
				if (userLevel != null) {
					SessionUserLevelID = (int)userLevel;
					SetUpUserLevel();
				}
			}
			#pragma warning disable 168

			// Validate user
			public bool ValidateUser(ref string usr, ref string pwd, bool autologin, bool encrypted = false)
			{
				bool result = false;
				string sFilter;
				string sSql;
				bool customValidateUser = false;

				// Call User Custom Validate event
				if (EW_USE_CUSTOM_LOGIN) {
					customValidateUser = User_CustomValidate(ref usr, ref pwd);
					if (customValidateUser) {

						//ew_Session[EW_SESSION_STATUS] = "login"; // To be setup below
						CurrentUserName = usr; // Load user name
					}
				}
				if (customValidateUser)
					User_Validated(null);
				if (customValidateUser)
					return customValidateUser;
				if (!result && !IsPasswordExpired())
					ew_Session[EW_SESSION_STATUS] = ""; // Clear login status
				return result;
			}
			#pragma warning restore 168

			// Load user level from config file
			public void LoadUserLevelFromConfigFile(ref List<string[]> arUserLevel, ref List<string[]> arUserLevelPriv, ref List<string[]> arTable, bool userpriv = false) {

				// User Level definitions
				arUserLevel.Clear();
				arUserLevelPriv.Clear();
				arTable.Clear();

				// Load user level from config files
				var doc = new cXMLDocument();
				string folder = ew_AppRoot() + EW_CONFIG_FILE_FOLDER;

				// Load user level settings from main config file
				string ProjectID = CurrentProjectID;
				string file = ew_IncludeTrailingDelimiter(folder, true) + ProjectID + ".xml";
				XmlElement projnode;
				if (File.Exists(file) && doc.Load(file) != null && ((projnode = doc.SelectSingleNode("//configuration/project")) != null)) {
					EW_RELATED_PROJECT_ID = doc.GetAttribute(ref projnode, "relatedid");
					var userlevel = doc.GetAttribute(ref projnode, "userlevel");
					var usergroup = userlevel.Split(';');
					foreach (var group in usergroup) {
						var ar = group.Split(',');
						if (ew_IsArray(ar) && ar.Length > 2) {
							var id = ar[0];
							var name = ar[1];

							// Remove quotes
							if (name.Length >= 2 && name.StartsWith("\"") && name.EndsWith("\""))
								name = name.Substring(1, name.Length-2);
							arUserLevel.Add(new string[] {id, name});
						}
					}

					// Load from main config file
					LoadUserLevelFromXml(folder, doc, ref arUserLevelPriv, ref arTable, userpriv);

					// Load from related config file
					if (ew_NotEmpty(EW_RELATED_PROJECT_ID))
						LoadUserLevelFromXml(folder, EW_RELATED_PROJECT_ID + ".xml", ref arUserLevelPriv, ref arTable, userpriv);
				}

				// Warn user if user level not setup
				if (arUserLevel.Count == 0)
					ew_End("Unable to load user level from config file: " + file);

				// Load user priv settings from all config files
				string[] files = Directory.GetFiles(folder, "*.xml");
				foreach (string f in files) {
					file = Path.GetFileName(f);
					if (file != ProjectID + ".xml" && file != EW_RELATED_PROJECT_ID + ".xml")
						LoadUserLevelFromXml(folder, file, ref arUserLevelPriv, ref arTable, userpriv);
				}
			}

			// Load user level from XML
			public void LoadUserLevelFromXml(string folder, object file, ref List<string[]> arUserLevelPriv, ref List<string[]> arTable, bool userpriv) {
				cXMLDocument doc = null;
				if (file is string) {
					file = ew_IncludeTrailingDelimiter(folder, true) + file;
					doc = new cXMLDocument();
					doc.Load(Convert.ToString(file));
				} else if (file is cXMLDocument) {
					doc = (cXMLDocument)file;
				}
				if (doc is cXMLDocument) {

					// Load project ID
					string projid = "";
					string projfile = "";
					XmlElement projnode;
					if ((projnode = doc.SelectSingleNode("//configuration/project")) != null) {
						projid = doc.GetAttribute(ref projnode, "id");
						projfile = doc.GetAttribute(ref projnode, "file");
						if (projid == EW_RELATED_PROJECT_ID)
							EW_RELATED_LANGUAGE_FOLDER = ew_IncludeTrailingDelimiter(doc.GetAttribute(ref projnode, "languagefolder"), true);
					}

					// Load user priv
					var tablelist = doc.SelectNodes("//configuration/project/table");
					foreach (XmlElement tbl in tablelist) {
						XmlElement table = tbl;
						var tablevar = doc.GetAttribute(ref table, "id");
						var tablename = doc.GetAttribute(ref table, "name");
						var tablecaption = doc.GetAttribute(ref table, "caption");
						var userlevel = doc.GetAttribute(ref table, "userlevel");
						var priv = doc.GetAttribute(ref table, "priv");
						if (!userpriv || (userpriv && priv == "1")) {
							var usergroup = userlevel.Split(';');
							foreach (var group in usergroup) {
								var ar = group.Split(',');
								if (ar.Length >= 3)
									arUserLevelPriv.Add(new string[] {projid + tablename, ar[0], ar[2]});
							}
							arTable.Add(new string[] {tablename, tablevar, tablecaption, priv, projid, projfile});
						}
					}
				}
			}

			// No user level security
			public void SetUpUserLevel() {
			}

			// Add user permission
			public void AddUserPermission(string UserLevelName, string TableName, int UserPermission)
			{
				string UserLevelID = "";

				// Get user level ID from user name
				if (ew_IsList(UserLevel)) {
					foreach (string[] row in UserLevel) {
						if (ew_SameStr(UserLevelName, row[1])) {
							UserLevelID = Convert.ToString(row[0]);
							break;
						}
					}
				}
				if (ew_IsList(UserLevelPriv) && ew_NotEmpty(UserLevelID)) {
					foreach (string[] row in UserLevelPriv) {
						if (ew_SameStr(row[0], EW_PROJECT_ID + TableName) && ew_SameStr(row[1], UserLevelID)) {
							row[2] = Convert.ToString(ew_ConvertToInt(row[2]) | UserPermission);	// Add permission
							break;
						}
					}
				}
			}

			// Delete user permission
			public void DeleteUserPermission(string UserLevelName, string TableName, int UserPermission)
			{
				string UserLevelID = "";

				// Get user level ID from user name
				if (ew_IsList(UserLevel)) {
					foreach (string[] Row in UserLevel) {
						if (ew_SameStr(UserLevelName, Row[1])) {
							UserLevelID = Convert.ToString(Row[0]);
							break;
						}
					}
				}
				if (ew_IsList(UserLevelPriv) && ew_NotEmpty(UserLevelID)) {
					foreach (string[] Row in UserLevelPriv) {
						if (ew_SameStr(Row[0], EW_PROJECT_ID + TableName) && ew_SameStr(Row[1], UserLevelID)) {
							Row[2] = Convert.ToString(ew_ConvertToInt(Row[2]) & (127 - UserPermission)); // Remove permission
							break;
						}
					}
				}
			}

			// Load current user level
			public void LoadCurrentUserLevel(string Table)
			{

				// Load again if user level list changed
				if (ew_NotEmpty(ew_Session[EW_SESSION_USER_LEVEL_LIST_LOADED]) &&
					!ew_SameStr(ew_Session[EW_SESSION_USER_LEVEL_LIST_LOADED], ew_Session[EW_SESSION_USER_LEVEL_LIST])) { // Compare strings, not objects // DN
					ew_Session[EW_SESSION_AR_USER_LEVEL_PRIV] = "";
				}
				LoadUserLevel();
				SessionUserLevel = CurrentUserLevelPriv(Table);
			}

			// Get current user privilege
			private int CurrentUserLevelPriv(string TableName)
			{
				if (IsLoggedIn) {
					return 127;
				} else { // Anonymous
					return GetUserLevelPrivEx(TableName, -2);
				}
			}

			// Get user level ID by user level name
			public int GetUserLevelID(string UserLevelName)
			{
				if (ew_SameStr(UserLevelName, "Anonymous") || ew_SameStr(UserLevelName, Language.Phrase("UserAnonymous"))) {
					return -2;
				} else if (ew_SameStr(UserLevelName, "Administrator") || ew_SameStr(UserLevelName, Language.Phrase("UserAdministrator"))) {
					return -1;
				} else if (ew_SameStr(UserLevelName, "Default") || ew_SameStr(UserLevelName, Language.Phrase("UserDefault"))) {
					return 0;
				} else if (ew_NotEmpty(UserLevelName)) {
					if (ew_IsList(UserLevel)) {
						foreach (string[] Row in UserLevel) {
							if (ew_SameStr(Row[1], UserLevelName) || ew_SameStr(Language.Phrase(Row[1]), UserLevelName))
								return ew_ConvertToInt(Row[0]);
						}
					}
				}
				return -2;	// Unknown
			}

			// Add Add User Level by name
			public void AddUserLevel(string UserLevelName)
			{
				if (ew_Empty(UserLevelName))
					return;
				int id = GetUserLevelID(UserLevelName);
				AddUserLevelID(id);
			}

			// Add User Level by ID
			public void AddUserLevelID(int id)
			{
				if (!ew_IsNumeric(id) || id < -1)
					return;
				if (UserLevelID.IndexOf(id) < 0) {
					UserLevelID.Add(id);
					ew_Session[EW_SESSION_USER_LEVEL_LIST] = UserLevelList(); // Update session variable
				}
			}

			// Delete User Level by name
			public void DeleteUserLevel(string UserLevelName)
			{
				if (ew_Empty(UserLevelName)) return;
				int id = GetUserLevelID(UserLevelName);
				DeleteUserLevelID(id);
			}

			// Delete User Level by ID
			public void DeleteUserLevelID(int id) {
				if (!ew_IsNumeric(id) || id < -1)
					return;
				int index = UserLevelID.IndexOf(id);
				if (index > -1) {
					UserLevelID.RemoveAt(index);
					ew_Session[EW_SESSION_USER_LEVEL_LIST] = UserLevelList(); // Update session variable
				}
			}

			// User level list
			public string UserLevelList()
			{
				return String.Join(", ", UserLevelID);
			}

			// User level name list
			public string UserLevelNameList()
			{
				string List = "";
				return List;
			}

			// Get user privilege based on table name and user level
			public int GetUserLevelPrivEx(string TableName, int UserLevelID)
			{
				if (ew_SameStr(UserLevelID, "-1")) { // System Administrator
					if (EW_USER_LEVEL_COMPAT) {
						return 31;	// Use old user level values
					} else {
						return 127;	// Use new user level values (separate View/Search)
					}
				} else if (UserLevelID >= 0 || ew_SameStr(UserLevelID, "-2")) {
					if (ew_IsList(UserLevelPriv)) {
						foreach (string[] Row in UserLevelPriv) {
							if (ew_SameStr(Row[0], TableName) && ew_SameStr(Row[1], UserLevelID))
								return ew_ConvertToInt(Row[2]);
						}
					}
				}
				return 0;
			}

			// Get current user level name
			public string CurrentUserLevelName
			{
				get {
					return GetUserLevelName(CurrentUserLevelID);
				}
			}

			// Get user level name based on user level
			public string GetUserLevelName(int UserLevelID, bool Lang = true)
			{
				if (ew_SameStr(UserLevelID, "-2")) {
					return (Lang) ? Language.Phrase("UserAnonymous") : "Anonymous";
				} else if (ew_SameStr(UserLevelID, "-1")) {
					return (Lang) ? Language.Phrase("UserAdministrator") : "Administrator";
				} else if (ew_SameStr(UserLevelID, "0")) {
					return (Lang) ? Language.Phrase("UserDefault") : "Default";
				} else if (UserLevelID > 0) {
					if (ew_IsList(UserLevel)) {
						foreach (string[] Row in UserLevel) {
							if (ew_SameStr(Row[0], UserLevelID)) {
								string UserLevelName = "";
								if (Lang)
									UserLevelName = Language.Phrase(Convert.ToString(Row[1]));
								return (UserLevelName != "") ? UserLevelName : Convert.ToString(Row[1]);
							}
						}
					}
				}
				return "";
			}

			// Display all the User Level settings (for debug only)
			public void ShowUserLevelInfo()
			{
				if (ew_IsList(UserLevel)) {
					ew_Write("User Levels:<br>");
					ew_Write("UserLevelId, UserLevelName<br>");
					foreach (string[] Row in UserLevel)
						ew_Write("&nbsp;&nbsp;" + String.Join(", ", Row) + "<br>");
				} else {
					ew_Write("No User Level definitions." + "<br>");
				}
				if (ew_IsList(UserLevelPriv)) {
					ew_Write("User Level Privs:<br>");
					ew_Write("TableName, UserLevelId, UserLevelPriv<br>");
					foreach (string[] Row in UserLevelPriv)
						ew_Write("&nbsp;&nbsp;" + String.Join(", ", Row) + "<br>");
				} else {
					ew_Write("No User Level privilege settings." + "<br>");
				}
				ew_Write("CurrentUserLevel = " + CurrentUserLevel + "<br>");
				ew_Write("User Levels = " + UserLevelList() + "<br>");
			}

			// Check privilege for List page (for menu items)
			public bool AllowList(string TableName)
			{
				return ew_ConvertToBool(CurrentUserLevelPriv(TableName) & EW_ALLOW_LIST);
			}

			// Check privilege for View page (for Allow-View / Detail-View)
			public bool AllowView(string TableName) {
				return ew_ConvertToBool(CurrentUserLevelPriv(TableName) & EW_ALLOW_VIEW);
			}

			// Check privilege for Add page (for Allow-Add / Detail-Add)
			public bool AllowAdd(string TableName)
			{
				return ew_ConvertToBool(CurrentUserLevelPriv(TableName) & EW_ALLOW_ADD);
			}

			// Check privilege for Edit page (for Detail-Edit)
			public bool AllowEdit(string TableName) {
				return ew_ConvertToBool(CurrentUserLevelPriv(TableName) & EW_ALLOW_EDIT);
			}

			// Check if user password expired
			public bool IsPasswordExpired
			{
				get {
					return ew_SameStr(ew_Session[EW_SESSION_STATUS], "passwordexpired");
				}
			}

			// Set session password expired
			public void SetSessionPasswordExpired() {
				ew_Session[EW_SESSION_STATUS] = "passwordexpired";
			}

			// Set login status
			public void SetLoginStatus(string status = "") {
				ew_Session[EW_SESSION_STATUS] = status;
			}

			// Check if user is logging in (after changing password)
			public bool IsLoggingIn
			{
				get {
					return ew_SameStr(ew_Session[EW_SESSION_STATUS], "loggingin");
				}
			}

			// Check if user is logged in
			public bool IsLoggedIn
			{
				get {
					return ew_SameStr(ew_Session[EW_SESSION_STATUS], "login");
				}
			}

			// Check if user is system administrator
			public bool IsSysAdmin
			{
				get {
					return (Convert.ToInt32(ew_Session[EW_SESSION_SYS_ADMIN]) == 1);
				}
			}

			// Check if user is administrator
			public bool IsAdmin
			{
				get {
					bool res = IsSysAdmin;
					return res;
				}
			}

			// Save user level to session
			public void SaveUserLevel()
			{
				ew_Session[EW_SESSION_AR_USER_LEVEL] = JsonConvert.SerializeObject(UserLevel); // DN
				ew_Session[EW_SESSION_AR_USER_LEVEL_PRIV] = JsonConvert.SerializeObject(UserLevelPriv); // DN
			}

			// Load user level from session // DN
			public void LoadUserLevel()
			{
				if (ew_Empty(ew_Session[EW_SESSION_AR_USER_LEVEL]) || ew_Empty(ew_Session[EW_SESSION_AR_USER_LEVEL_PRIV])) { // DN
					SetUpUserLevel();
				} else {
					UserLevel = JsonConvert.DeserializeObject<List<string[]>>(Convert.ToString(ew_Session[EW_SESSION_AR_USER_LEVEL]));
					UserLevelPriv = JsonConvert.DeserializeObject<List<string[]>>(Convert.ToString(ew_Session[EW_SESSION_AR_USER_LEVEL_PRIV]));
				}
			}

			// UserID Loading event
			public virtual void UserID_Loading() {

				//ew_Write("UserID Loading: " + CurrentUserID + "<br>");
			}

			// UserID Loaded event
			public virtual void UserID_Loaded() {

				//ew_Write("UserID Loaded: " + UserIDList() + "<br>");
			}

			// User Level Loaded event
			public virtual void UserLevel_Loaded() {

				//AddUserPermission(<UserLevelName>, <TableName>, <UserPermission>);
				//DeleteUserPermission(<UserLevelName>, <TableName>, <UserPermission>);

			}

			// Table Permission Loading event
			public virtual void TablePermission_Loading() {

				//ew_Write("Table Permission Loading: " + CurrentUserLevelID + "<br>");
			}

			// Table Permission Loaded event
			public virtual void TablePermission_Loaded() {

				//ew_Write("Table Permission Loaded: " + CurrentUserLevel + "<br>");
			}

			// User Custom Validate event
			public virtual bool User_CustomValidate(ref string usr, ref string pwd) {

				// Enter your custom code to validate user, return TRUE if valid.
				return false;
			}

			// User Validated event
			public virtual void User_Validated(DbDataReader rs) {

				//ew_Session["UserEmail"] = rs["Email"];
			}

			// User PasswordExpired event
			public virtual void User_PasswordExpired(DbDataReader rs) {

				//ew_Write("User_PasswordExpired");
			}
		}

		// Remove elements from OrderedDictionary by an array of keys and return the removed elements as OrderedDictionary
		public static OrderedDictionary ew_Splice(OrderedDictionary od, string[] keys) {
			var res = new OrderedDictionary();
			foreach (string key in keys) {
				if (od.Contains(key)) {
					res.Add(key, od[key]);
					od.Remove(key);
				}
			}
			return res;
		}

		// Extract elements from OrderedDictionary by an array of keys
		public static OrderedDictionary ew_Slice(OrderedDictionary od, string[] keys) {
			var res = new OrderedDictionary();
			foreach (string key in keys) {
				if (od.Contains(key))
					res.Add(key, od[key]);
			}
			return res;
		}

		// Menu class
		public class cMenuBase
		{
			public object Id;
			public string MenuBarClassName = EW_MENUBAR_CLASSNAME;
			public string MenuClassName = EW_MENU_CLASSNAME;
			public string SubMenuClassName = EW_SUBMENU_CLASSNAME;
			public string SubMenuDropdownImage = EW_SUBMENU_DROPDOWN_IMAGE;
			public string SubMenuDropdownIconClassName = EW_SUBMENU_DROPDOWN_ICON_CLASSNAME;
			public string MenuDividerClassName = EW_MENU_DIVIDER_CLASSNAME;
			public string MenuItemClassName = EW_MENU_ITEM_CLASSNAME;
			public string SubMenuItemClassName = EW_SUBMENU_ITEM_CLASSNAME;
			public string MenuActiveItemClassName = EW_MENU_ACTIVE_ITEM_CLASS;
			public string SubMenuActiveItemClassName = EW_SUBMENU_ACTIVE_ITEM_CLASS;
			public bool MenuRootGroupTitleAsSubMenu = EW_MENU_ROOT_GROUP_TITLE_AS_SUBMENU;
			public bool ShowRightMenu = EW_SHOW_RIGHT_MENU;
			public string MenuLinkDropdownClass = "";
			public string MenuLinkClassName = "";
			public bool IsMobile = false;
			public bool IsRoot = false;
			public List<cMenuItem> ItemData = new List<cMenuItem>(); // List of cMenuItem

			// Constructor
			public cMenuBase(object MenuId, bool Mobile = false)
			{
				Id = MenuId;
				IsMobile = Mobile;
			}

			// Add a menu item
			public void AddMenuItem(int Id, string Name, string Text, string Url, int ParentId = -1, string Src = "", bool Allowed = true, bool GroupTitle = false, bool CustomUrl = false)
			{
				cMenuItem oParentMenu = null;
				var item = new cMenuItem(Id, Name, Text, ew_NotEmpty(Url) ? ew_AppPath(Url) : "", ParentId, Src, Allowed, GroupTitle, CustomUrl); // DN ***
				item.Parent = this;
				if (!MenuItem_Adding(ref item))
					return;
				if (item.ParentId < 0) {
					AddItem(ref item);
				} else {
					if (FindItem(item.ParentId, ref oParentMenu))
						oParentMenu.AddItem(ref item, IsMobile);
				}
			}

			// Add a menu item (for backward compatibility) // DN
			public void AddMenuItem(int Id, string Text, string Url, int ParentId = -1, string Src = "", bool Allowed = true, bool GroupTitle = false, bool CustomUrl = false)
			{
				string Name = "mi_" + Convert.ToString(Id);
				AddMenuItem(Id, Name, Text, Url, ParentId, Src, Allowed, GroupTitle, CustomUrl);
			}

			// Get menu item count
			public int Count
			{
				get {
					return ItemData.Count;
				}
			}

			// Add item
			public void AddItem(ref cMenuItem item)
			{
				ItemData.Add(item);
			}

			// Clear all menu items
			public void Clear() {
				ItemData.Clear();
			}

			// Find item
			public bool FindItem(int id, ref cMenuItem outitem)
			{
				bool result = false;
				foreach (cMenuItem item in ItemData) {
					if (item.Id == id) {
						outitem = item;
						return true;
					} else if (item.SubMenu != null) {
						if (item.SubMenu.FindItem(id, ref outitem))
							return true;
					}
				}
				return result;
			}

			// Find item by menu text
			public bool FindItemByText(string txt, ref cMenuItem outitem)
			{
				bool result = false;
				foreach (cMenuItem item in ItemData) {
					if (item.Text == txt) {
						outitem = item;
						return true;
					} else if (item.SubMenu != null) {
						if (item.SubMenu.FindItemByText(txt, ref outitem))
							return true;
					}
				}
				return result;
			}

			// Move item to position
			public void MoveItem(string Text, int Pos)
			{
				int oldpos = 0;
				int newpos = Pos;
				bool bfound = false;
				if (Pos < 0) {
					Pos = 0;
				} else if (Pos >= ItemData.Count) {
					Pos = ItemData.Count - 1;
				}
				cMenuItem CurItem = null;
				foreach (cMenuItem item in ItemData) {
					if (ew_SameStr(item.Text, Text)) {
						CurItem = item;
						break;
					}
				}
				if (CurItem != null) {
					bfound = true;
					oldpos = ItemData.IndexOf(CurItem);
				} else {
					bfound = false;
				}
				if (bfound && Pos != oldpos) {
					ItemData.RemoveAt(oldpos); // Remove old item
					if (oldpos < Pos)
						newpos -= 1; // Adjust new position
					ItemData.Insert(newpos, CurItem); // Insert new item
				}
			}

			// Check if sub menu should be shown
			public bool RenderSubMenu(cMenuItem item) {
				if (item.SubMenu != null) {
					foreach (cMenuItem subitem in item.SubMenu.ItemData) {
						if (item.SubMenu.RenderItem(subitem))
							return true;
					}
				}
				return false;
			}

			// Check if a menu item should be shown
			public bool RenderItem(cMenuItem item) {
				if (item.SubMenu != null) {
					foreach (cMenuItem subitem in item.SubMenu.ItemData) {
						if (item.SubMenu.RenderItem(subitem))
							return true;
					}
				}
				return (item.Allowed && ew_NotEmpty(item.Url) && item.Url != ew_AppPath()); // DN
			}

			// Check if this menu should be rendered
			public bool RenderMenu() {
				foreach (cMenuItem item in ItemData) {
					if (RenderItem(item))
						return true;
				}
				return false;
			}
			#pragma warning disable 162

			// Render the menu
			public virtual string Render(bool ret = false) {
				if (!RenderMenu())
					return "";
				string str = "";
				if (!IsMobile) {
					if (IsRoot) {
						str = "<ul";
						if (ew_NotEmpty(Id)) {
							if (ew_IsNumeric(Id)) {
								str += " id=\"menu_" + Id + "\"";
							} else {
								str += " id=\"" + Id + "\"";
							}
						}
						str += " class=\"" + MenuClassName + "\">";
					} else {
						str = "<ul class=\"" + SubMenuClassName + "\" role=\"menu\">\n";
					}
				}
				int gcnt = 0; // Group count
				bool gtitle = false; // Last item is group title
				int i = 0; // Menu item count
				string cururl = ew_CurrentUrl();
				cururl = cururl.Substring(cururl.LastIndexOf("/") + 1);
				foreach (cMenuItem item in ItemData) {
					if (RenderItem(item)) {
						i++;
						string aclass = "", liclass = "";
						if (!IsMobile && gtitle && (gcnt >= 1 || IsRoot)) // add divider for previous group
							str += "<li class=\"" + MenuDividerClassName + "\"></li>\n";
						if (item.GroupTitle && (!IsRoot || !MenuRootGroupTitleAsSubMenu)) { // Group title
							gtitle = true;
							gcnt += 1;
							if (ew_NotEmpty(item.Text)) {
								if (IsMobile)
									str += "<li data-role=\"list-divider\">" + item.Text + "</li>\n";
								else
									str += "<li class=\"dropdown-header\">" + item.Text + "</li>\n";
							}
							if (item.SubMenu != null) {
								foreach (cMenuItem subitem in item.SubMenu.ItemData) {
									liclass = (subitem.SubMenu != null && RenderSubMenu(subitem)) ? SubMenuItemClassName : "";
									aclass = "";
									if (!subitem.IsCustomUrl && ew_CurrentPage() == ew_GetPageName(subitem.Url) || subitem.IsCustomUrl && cururl == subitem.Url) {
										liclass = ew_AppendClass(liclass, MenuActiveItemClassName);
										subitem.Url = "javascript:void(0);";
									}
									if (RenderItem(subitem)) {
										if (IsMobile && item.GroupTitle)
											aclass = ew_AppendClass(aclass, "ewIndent");
										str += subitem.Render(aclass, liclass, IsMobile) + "\n"; // Create <LI>
									}
								}
							}
						} else {
							gtitle = false;
							liclass = (item.SubMenu != null && RenderSubMenu(item)) ? (IsRoot ? MenuItemClassName : SubMenuItemClassName) : "";
							aclass = "";
							if (!item.IsCustomUrl && ew_CurrentPage() == ew_GetPageName(item.Url) || item.IsCustomUrl && cururl == item.Url) {
								if (IsRoot)
									liclass = ew_AppendClass(liclass, MenuActiveItemClassName);
								else
									liclass = ew_AppendClass(liclass, SubMenuActiveItemClassName);
								item.Url = "javascript:void(0);";
							}
							str += item.Render(aclass, liclass, IsMobile) + "\n"; // Create <LI>
						}
					}
				}
				if (IsMobile) {
					str = "<ul data-role=\"listview\" data-filter=\"true\">" + str + "</ul>\n";
				} else if (IsRoot) {
					str += "</ul>\n";
					if (EW_MENUBAR_BRAND != "") {
						var brandhref = (EW_MENUBAR_BRAND_HYPERLINK == "") ? "#" : EW_MENUBAR_BRAND_HYPERLINK;
						str = "<a class=\"navbar-brand hidden-xs\" href=\"" + ew_HtmlEncode(brandhref) + "\">" + EW_MENUBAR_BRAND + "</a>" + str;
					}

					// Add right menu
					if (ShowRightMenu)
						str += "<ul class=\"nav navbar-nav navbar-right\"></ul>";
					if (MenuBarClassName != "")
						str = "<div class=\"" + MenuBarClassName + "\">" + str + "</div>";
				} else {
					str += "</ul>\n";
				}
				if (ret) { // Return as string
					return str;
				} else { // Output
					ew_Write(str);
					return "";
				}
			}
			#pragma warning restore 162

			// Render the menu
			public IHtmlContent Render() {
				return new HtmlString(Render(true));
			}

			// MenuItem_Adding
			public virtual bool MenuItem_Adding(ref cMenuItem Item) {
				return true;
			}

			// Menu_Rendering
			public virtual void Menu_Rendering(cMenuBase Menu) {
			}
		}

		// Menu item class
		public class cMenuItem
		{
			public int Id;
			public string Name;
			public string Text;
			public string Url;
			public int ParentId;
			public cMenuBase SubMenu = null;
			public string Source = "";
			public bool Allowed = true;
			public string Target = "";
			public bool GroupTitle = false;
			public bool IsCustomUrl = false;
			public cMenuBase Parent;

			// Constructor
			public cMenuItem(int aId, string aName, string aText, string aUrl, int aParentId = -1, string aSource = "", bool aAllowed = true, bool aGroupTitle = false, bool aCustomUrl = false)
			{
				Id = aId;
				Name = aName;
				Text = aText;
				Url = aUrl;
				ParentId = aParentId;
				Source = aSource;
				Allowed = aAllowed;
				GroupTitle = aGroupTitle;
				IsCustomUrl = aCustomUrl;
			}

			// Add submenu item
			public void AddItem(ref cMenuItem item, bool mobile = false)
			{
				SubMenu = SubMenu ?? new cMenuBase(Id, mobile);
				SubMenu.MenuBarClassName = Parent.MenuBarClassName;
				SubMenu.MenuClassName = Parent.MenuClassName;
				SubMenu.SubMenuClassName = Parent.SubMenuClassName;
				SubMenu.SubMenuDropdownImage = Parent.SubMenuDropdownImage;
				SubMenu.SubMenuDropdownIconClassName = Parent.SubMenuDropdownIconClassName;
				SubMenu.MenuDividerClassName = Parent.MenuDividerClassName;
				SubMenu.MenuItemClassName = Parent.MenuItemClassName;
				SubMenu.SubMenuItemClassName = Parent.SubMenuItemClassName;
				SubMenu.MenuActiveItemClassName = Parent.MenuActiveItemClassName;
				SubMenu.SubMenuActiveItemClassName = Parent.SubMenuActiveItemClassName;
				SubMenu.MenuRootGroupTitleAsSubMenu = Parent.MenuRootGroupTitleAsSubMenu;
				SubMenu.MenuLinkDropdownClass = Parent.MenuLinkDropdownClass;
				SubMenu.MenuLinkClassName = Parent.MenuLinkClassName;
				SubMenu.AddItem(ref item);
			}

			// Render
			public string Render(string aclass, string liclass, bool mobile = false) {

				// Create <A>
				Dictionary<string, string> attrs;
				var url = ew_GetUrl(Url);
				string text2;
				string submenuhtml = SubMenu?.Render(true) ?? "";
				if (mobile) {
					url = url.Replace("#", "?chart=");
					if (url == "") url = "#";
					attrs = new cAttributes() {{"class", aclass}, {"rel",(url != "#") ? "external" : ""}, {"href", url}, {"target", Target}};
				} else {
					if (url == "") url = "#";
					if (SubMenu != null && SubMenu.MenuLinkDropdownClass != "" && submenuhtml != "")
					aclass = ew_PrependClass(aclass, SubMenu.MenuLinkDropdownClass); // DN
					attrs = new cAttributes() {{"class", aclass}, {"href", url}, {"target", Target}};
				}
				var text = Text;
				if (SubMenu != null && submenuhtml != "") {
					if (Parent.SubMenuDropdownIconClassName != "")
						text += "<span class=\"" + Parent.SubMenuDropdownIconClassName + "\"></span>";
					if (Parent.SubMenuDropdownImage != "" && ParentId == -1)
						text += Parent.SubMenuDropdownImage;
				}
				string innerhtml = ew_HtmlElement("a", attrs, text, true);
				if (SubMenu != null) {
					if (url != "#" && SubMenu.MenuLinkClassName != "" && submenuhtml != "") { // Add click link for mobile menu
						var attrs2 = new Dictionary<string, string>() {{"class", aclass}, {"href", url}};
						attrs2["class"] = "ewMenuLink";
						attrs2["href"] = url;
						text2 = "<span class=\"" + SubMenu.MenuLinkClassName + "\"></span>";
						innerhtml = ew_HtmlElement("a", attrs2, text2) + innerhtml;
					}
					if (mobile && Url != "#")
						innerhtml += innerhtml;
					innerhtml += submenuhtml;
				}

				// Create <LI>
				attrs.Clear();
				attrs.Add("id", Name);
				attrs.Add("class", liclass);
				return ew_HtmlElement("li", attrs, innerhtml, true);
			}
		}

		// Allow list
		public static bool AllowList(string TableName)
		{
			return Security?.AllowList(TableName) ?? true;
		}

		// Allow add
		public static bool AllowAdd(string TableName)
		{
			return Security?.AllowAdd(TableName) ?? true;
		}

		//
		// Validation functions
		//
		// Check date format
		// format: std/stdshort/us/usshort/euro/euroshort

		public static bool ew_CheckDateEx(string value, string format, string sep)
		{
			if (ew_Empty(value)) return true;
			while (value.Contains("  "))
				value = value.Replace("  ", " ");
			value = value.Trim();
			string[] arDT;
			string[] arD;
			string pattern = "";
			string sYear = "";
			string sMonth = "";
			string sDay = "";
			arDT = value.Split(' ');
			if (arDT.Length > 0) {
				Match m;
				if ((m = Regex.Match(arDT[0], @"^([0-9]{4})/([0][1-9]|[1][0-2])/([0][1-9]|[1|2][0-9]|[3][0|1])$")) != null && m.Success) { // Accept yyyy/mm/dd
					sYear = m.Groups[1].Value;
					sMonth = m.Groups[2].Value;
					sDay = m.Groups[3].Value;
				} else {
					string wrksep = "\\" + sep;
					switch (format) {
						case "std":
							pattern = "^([0-9]{4})" + wrksep + "([0]?[1-9]|[1][0-2])" + wrksep + "([0]?[1-9]|[1|2][0-9]|[3][0|1])$";
							break;
						case "us":
							pattern = "^([0]?[1-9]|[1][0-2])" + wrksep + "([0]?[1-9]|[1|2][0-9]|[3][0|1])" + wrksep + "([0-9]{4})$";
							break;
						case "euro":
							pattern = "^([0]?[1-9]|[1|2][0-9]|[3][0|1])" + wrksep + "([0]?[1-9]|[1][0-2])" + wrksep + "([0-9]{4})$";
							break;
					}
					if (!Regex.IsMatch(arDT[0], pattern)) return false;
					arD = arDT[0].Split(Convert.ToChar(sep));
					switch (format) {
						case "std":
						case "stdshort":
							sYear = ew_UnformatYear(arD[0]);
							sMonth = arD[1];
							sDay = arD[2];
							break;
						case "us":
						case "usshort":
							sYear = ew_UnformatYear(arD[2]);
							sMonth = arD[0];
							sDay = arD[1];
							break;
						case "euro":
						case "euroshort":
							sYear = ew_UnformatYear(arD[2]);
							sMonth = arD[1];
							sDay = arD[0];
							break;
					}
				}
				if (!ew_CheckDay(ew_ConvertToInt(sYear), ew_ConvertToInt(sMonth), ew_ConvertToInt(sDay))) return false;
			}
			if (arDT.Length > 1 && !ew_CheckTime(arDT[1])) return false;
			return true;
		}

		// Unformat 2 digit year to 4 digit year
		public static string ew_UnformatYear(object year) {
			string yr = Convert.ToString(year);
			if (ew_IsNumeric(yr) && yr.Length == 2) {
				if (ew_ConvertToInt(yr) > EW_UNFORMAT_YEAR)
					return "19" + yr;
				else
					return "20" + yr;
			} else {
				return yr;
			}
		}

		// Check Date format (yyyy/mm/dd)
		public static bool ew_CheckDate(string value)
		{
			return ew_CheckDateEx(value, "std", EW_DATE_SEPARATOR);
		}

		// Check Date format (yy/mm/dd)
		public static bool ew_CheckShortDate(string value) {
			return ew_CheckDateEx(value, "stdshort", EW_DATE_SEPARATOR);
		}

		// Check US Date format (mm/dd/yyyy)
		public static bool ew_CheckUSDate(string value)
		{
			return ew_CheckDateEx(value, "us", EW_DATE_SEPARATOR);
		}

		// Check US Date format (mm/dd/yy)
		public static bool ew_CheckShortUSDate(string value) {
			return ew_CheckDateEx(value, "usshort", EW_DATE_SEPARATOR);
		}

		// Check Euro Date format (dd/mm/yyyy)
		public static bool ew_CheckEuroDate(string value)
		{
			return ew_CheckDateEx(value, "euro", EW_DATE_SEPARATOR);
		}

		// Check Euro Date format (dd/mm/yy)
		public static bool ew_CheckShortEuroDate(string value) {
			return ew_CheckDateEx(value, "euroshort", EW_DATE_SEPARATOR);
		}

		// Check default date format
		public static bool ew_CheckDateDef(string value) {
			if (Regex.IsMatch(EW_DATE_FORMAT, "^yyyy/"))
				return ew_CheckDate(value);
			else if (Regex.IsMatch(EW_DATE_FORMAT, "^yy"))
				return ew_CheckShortDate(value);
			else if (Regex.IsMatch(EW_DATE_FORMAT, "^m") && Regex.IsMatch(EW_DATE_FORMAT, "yyyy$"))
				return ew_CheckUSDate(value);
			else if (Regex.IsMatch(EW_DATE_FORMAT, "^m") && Regex.IsMatch(EW_DATE_FORMAT, "yy$"))
				return ew_CheckShortUSDate(value);
			else if (Regex.IsMatch(EW_DATE_FORMAT, "^d") && Regex.IsMatch(EW_DATE_FORMAT, "yyyy$"))
				return ew_CheckEuroDate(value);
			else if (Regex.IsMatch(EW_DATE_FORMAT, "^d") && Regex.IsMatch(EW_DATE_FORMAT, "yy$"))
				return ew_CheckShortEuroDate(value);
			return false;
		}

		// Check day
		public static bool ew_CheckDay(int checkYear, int checkMonth, int checkDay)
		{
			int maxDay = 31;
			if (checkMonth == 4 || checkMonth == 6 || checkMonth == 9 || checkMonth == 11) {
				maxDay = 30;
			} else if (checkMonth == 2) {
				if (checkYear % 4 > 0) {
					maxDay = 28;
				} else if (checkYear % 100 == 0 && checkYear % 400 > 0) {
					maxDay = 28;
				} else {
					maxDay = 29;
				}
			}
			return (checkDay >= 1 && checkDay <= maxDay);
		}

		// Check integer
		public static bool ew_CheckInteger(string value)
		{
			if (ew_Empty(value)) return true;
			if (value.Contains(EW_DECIMAL_POINT))
				return false;
			return ew_CheckNumber(value);
		}

		// Check number
		public static bool ew_CheckNumber(string value)
		{
			if (ew_Empty(value)) return true;
			string pat = @"^[+-]?(\d{1,3}(" + ((EW_THOUSANDS_SEP != "") ? @"\" + EW_THOUSANDS_SEP + "?" : "") + @"\d{3})*(\" +
				EW_DECIMAL_POINT + @"\d+)?|\" + EW_DECIMAL_POINT + @"\d+)$"; // Note: Do not use ew_NotEmpty(EW_THOUSANDS_SEP).
			return Regex.IsMatch(value, pat);
		}

		// Check range
		public static bool ew_CheckRange(string value, object min, object max)
		{
			if (ew_Empty(value)) return true;
			if (ew_IsNumeric(min) || ew_IsNumeric(max)) { // Number
				if (ew_CheckNumber(value)) {
					double val = Convert.ToDouble(ew_StrToFloat(value));
					if (min != null && val < Convert.ToDouble(min) || max != null && val > Convert.ToDouble(max))
						return false;
				}
			} else if (min != null && String.Compare(value, Convert.ToString(min)) < 0 || max != null && String.Compare(value, Convert.ToString(max)) > 0) // String
				return false;
			return true;
		}

		// Check time
		public static bool ew_CheckTime(string value)
		{
			if (ew_Empty(value)) return true;
			return Regex.IsMatch(value, "^(0[0-9]|1[0-9]|2[0-3])[" + Regex.Escape(EW_TIME_SEPARATOR) + "][0-5][0-9]([" + Regex.Escape(EW_TIME_SEPARATOR) + "][0-5][0-9])?");
		}

		// Check US phone number
		public static bool ew_CheckPhone(string value)
		{
			if (ew_Empty(value)) return true;
			return Regex.IsMatch(value, "^\\(\\d{3}\\) ?\\d{3}( |-)?\\d{4}|^\\d{3}( |-)?\\d{3}( |-)?\\d{4}");
		}

		// Check US zip code
		public static bool ew_CheckZip(string value)
		{
			if (ew_Empty(value)) return true;
			return Regex.IsMatch(value, "^\\d{5}|^\\d{5}-\\d{4}");
		}

		// Check credit card
		public static bool ew_CheckCreditCard(string value)
		{
			if (ew_Empty(value)) return true;
			var creditcard = new Dictionary<string, string>() {
				{"visa", "^4\\d{3}[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"mastercard", "^5[1-5]\\d{2}[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"discover", "^6011[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"amex", "^3[4,7]\\d{13}"},
				{"diners", "^3[0,6,8]\\d{12}"},
				{"bankcard", "^5610[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"jcb", "^[3088|3096|3112|3158|3337|3528]\\d{12}"},
				{"enroute", "^[2014|2149]\\d{11}"},
				{"switch", "^[4903|4911|4936|5641|6333|6759|6334|6767]\\d{12}"}
			};
			foreach (var kvp in creditcard) {
				if (Regex.IsMatch(value, kvp.Value))
					return ew_CheckSum(value);
			}
			return false;
		}

		// Check sum
		public static bool ew_CheckSum(string value)
		{
			int checksum;
			byte digit;
			value = value.Replace("-", "");
			value = value.Replace(" ", "");
			checksum = 0;
			for (int i = 2 - (value.Length % 2); i <= value.Length; i += 2) {
				checksum = checksum + Convert.ToByte(value[i - 1]);
			}
			for (int i = (value.Length % 2) + 1; i <= value.Length; i += 2) {
				digit = Convert.ToByte(Convert.ToByte(value[i - 1]) * 2);
				checksum = checksum + ((digit < 10) ? digit : (digit - 9));
			}
			return (checksum % 10 == 0);
		}

		// Check US social security number
		public static bool ew_CheckSSC(string value)
		{
			if (ew_Empty(value)) return true;
			return Regex.IsMatch(value, "^(?!000)([0-6]\\d{2}|7([0-6]\\d|7[012]))([ -]?)(?!00)\\d\\d\\3(?!0000)\\d{4}");
		}

		// Check email
		public static bool ew_CheckEmail(string value)
		{
			if (ew_Empty(value)) return true;
			return Regex.IsMatch(value.Trim(), @"^[\w.%+-]+@[\w.-]+\.[A-Z]{2,18}$", RegexOptions.IgnoreCase);
		}

		// Check emails
		public static bool ew_CheckEmailList(string value, int cnt)
		{
			if (ew_Empty(value)) return true;
			string emailList = value.Replace(",", ";");
			string[] arEmails = emailList.Split(';');
			if (arEmails.Length > cnt && cnt > 0)
				return false;
			foreach (string email in arEmails) {
				if (!ew_CheckEmail(email))
					return false;
			}
			return true;
		}

		// Check GUID
		public static bool ew_CheckGUID(string value)
		{
			if (ew_Empty(value)) return true;
			string pattern = "([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}";
					return Regex.IsMatch(value, "^{" + pattern + "}") || Regex.IsMatch(value, "^" + pattern);
		}

		// Check file extension
		public static bool ew_CheckFileType(string value, string exts = EW_UPLOAD_ALLOWED_FILE_EXT)
		{
			if (ew_Empty(value) || ew_Empty(EW_UPLOAD_ALLOWED_FILE_EXT)) return true;
			var extension = Path.GetExtension(value).Substring(1);
			var allowExt = new List<string>(exts.Split(','));
			return allowExt.Contains(extension, StringComparer.OrdinalIgnoreCase);
		}

		// Check empty string
		public static bool ew_EmptyStr(object value) {
			string str = Convert.ToString(value).Replace("&nbsp;", "");
			return ew_Empty(str);
		}

		// Check by regular expression
		public static bool ew_CheckByRegEx(string value, string pattern)
		{
			if (ew_Empty(value)) return true;
			return Regex.IsMatch(value, pattern);
		}

		// Check by regular expression
		public static bool ew_CheckByRegEx(string value, string pattern, RegexOptions options)
		{
			if (ew_Empty(value)) return true;
			return Regex.IsMatch(value, pattern, options);
		}

		// Get current date in standard format (yyyy/mm/dd)
		public static string ew_StdCurrentDate() {
			return DateTime.Today.ToString("yyyy'/'MM'/'dd");
		}

		// Get date in standard format (yyyy/mm/dd)
		public static string ew_StdDate(DateTime dt) {
			return dt.ToString("yyyy'/'MM'/'dd");
		}

		// Get current date and time in standard format (yyyy/mm/dd hh:mm:ss)
		public static string ew_StdCurrentDateTime() {
			return DateTime.Now.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");
		}

		// Get date/time in standard format (yyyy/mm/dd hh:mm:ss)
		public static string ew_StdDateTime(DateTime dt) {
			return dt.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");
		}

		// Encrypt password
		public static string ew_EncryptPassword(string input) {
			return MD5(input);
		}

		// Compare password
		public static bool ew_ComparePassword(string pwd, string input, bool encrypted = false) {
			if (encrypted)
				return pwd == input;
			if (EW_CASE_SENSITIVE_PASSWORD) {
				if (EW_ENCRYPTED_PASSWORD) {
					return (pwd == ew_EncryptPassword(input));
				} else {
					return ew_SameStr(pwd, input);
				}
			} else {
				if (EW_ENCRYPTED_PASSWORD) {
					return (pwd == ew_EncryptPassword(input.ToLower()));
				} else {
					return ew_SameText(pwd, input);
				}
			}
		}

		// MD5
		public static string MD5(string InputStr)
		{
			var Md5Hasher = new MD5CryptoServiceProvider();
			byte[] Data = Md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(InputStr));
			var sBuilder = new StringBuilder();
			for (int i = 0; i <= Data.Length - 1; i++) {
				sBuilder.Append(Data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}

		// CRC32
		public static uint CRC32(string InputStr) {
			byte[] bytes = Encoding.Unicode.GetBytes(InputStr);
			uint crc = 0xffffffff;
			uint poly = 0xedb88320;
			uint[] table = new uint[256];
			uint temp = 0;
			for (uint i = 0; i < table.Length; ++i) {
				temp = i;
				for (int j = 8; j > 0; --j) {
					if ((temp & 1) == 1) {
						temp = (uint)((temp >> 1) ^ poly);
					} else {
						temp >>= 1;
					}
				}
				table[i] = temp;
			}
			for (int i = 0; i < bytes.Length; ++i) {
				byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
				crc = (uint)((crc >> 8) ^ table[index]);
			}
			return ~crc;
		}

		// Check if object is array
		public static bool ew_IsArray(object obj)
		{
			return (obj != null) && (obj is Array);
		}

		// Check if is numeric
		public static bool ew_IsNumeric(object expression)
		{
			long i; ulong j; double k; decimal l;
			NumberFormatInfo info;
			bool result;
			string s = Convert.ToString(expression);
			NumberStyles style = NumberStyles.Any;

			// Check for english locale
			info = (NumberFormatInfo)CultureInfo.GetCultureInfo("en-US").NumberFormat;
			result = System.Double.TryParse(s, style, info, out k) || System.Int64.TryParse(s, style, info, out i) || System.UInt64.TryParse(s, style, info, out j) || System.Decimal.TryParse(s, style, info, out l);

			// Check for current language locale
			if (!result) {
				info = (NumberFormatInfo)CurrentNumberFormatInfo.Clone();
				result = System.Double.TryParse(s, style, info, out k) || System.Int64.TryParse(s, style, info, out i) || System.UInt64.TryParse(s, style, info, out j) || System.Decimal.TryParse(s, style, info, out l);
			}
			return result;
		}

		// Check if is date
		public static bool ew_IsDate(object expression)
		{
			DateTime dt;
			string s = Convert.ToString(expression);
			return DateTime.TryParse(s, out dt);
		}
		public static int ew_GetFormatDateTimeID(int ANamedFormat)
		{
			switch (ANamedFormat) {
				case 0:
				case 8:
					return EW_DATE_FORMAT_ID;
				case 1:
					switch (EW_DATE_FORMAT_ID) {
						case 5:
							return 9;
						case 6:
							return 10;
						case 7:
							return 11;
						case 12:
							return 15;
						case 13:
							return 16;
						case 14:
							return 17;
						default:
							return EW_DATE_FORMAT_ID;
					}
				case 2:
					switch (EW_DATE_FORMAT_ID) {
						case 9:
							return 5;
						case 10:
							return 6;
						case 11:
							return 7;
						case 15:
							return 12;
						case 16:
							return 13;
						case 17:
							return 14;
						default:
							return EW_DATE_FORMAT_ID;
					}
				default:
					return ANamedFormat;
			}
		}

		// Format data time
		// 0 = General date
		// 1 = Long date
		// 2 = Short date (default date format)
		// 3 = Long time
		// 4 = Short time (hh:mm:ss)
		// 5 = "yyyy/mm/dd"
		// 6 = "mm/dd/yyyy"
		// 7 = "dd/mm/yyyy"
		// 8 = Short Date (default date format) + Short Time (hh:mm:ss)
		// 9 = "yyyy/mm/dd hh:mm:ss"
		// 10 = "mm/dd/yyyy hh:mm:ss"
		// 11 = "dd/mm/yyyy hh:mm:ss"
		// 12 - Short Date - 2 digit year (yy/mm/dd)
		// 13 - Short Date - 2 digit year (mm/dd/yy)
		// 14 - Short Date - 2 digit year (dd/mm/yy)
		// 15 - Short Date (yy/mm/dd) + Short Time (hh:mm:ss)
		// 16 - Short Date (mm/dd/yy) + Short Time (hh:mm:ss)
		// 17 - Short Date (dd/mm/yy) + Short Time (hh:mm:ss)
		// Format date time based on format type

		public static string ew_FormatDateTime(object ADate, int ANamedFormat)
		{
			string sDT;
			if (ew_Empty(ADate))
				return "";
			if (ew_IsDate(ADate)) {
				DateTime DT = ew_ConvertToDateTime(ADate);
				if (ANamedFormat == 8) {
					if (DT.Hour != 0 || DT.Minute != 0 || DT.Second != 0)
						ANamedFormat = 1;
					else
						ANamedFormat = 2;
				}
				ANamedFormat = ew_GetFormatDateTimeID(ANamedFormat);
				if (ANamedFormat == 3) {
					if (DT.Hour == 0) {
						if (DT.Minute == 0 && DT.Second == 0) {
							sDT = "12 " + Language.Phrase("Midnight");
						} else {
							sDT = "12" + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Minute, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Second, 2) + " " + Language.Phrase("AM");
						}
					} else if (DT.Hour > 0 && DT.Hour < 12) {
						sDT = ew_ZeroPad(DT.Hour, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Minute, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Second, 2) + " " + Language.Phrase("AM");
					} else if (DT.Hour == 12) {
						if (DT.Minute == 0 && DT.Second == 0) {
							sDT = "12 " + Language.Phrase("Noon");
						} else {
							sDT = ew_ZeroPad(DT.Hour, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Minute, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Second, 2) + " " + Language.Phrase("PM");
						}
					} else if (DT.Hour > 12 && DT.Hour <= 23) {
						sDT = ew_ZeroPad(DT.Hour-12, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Minute, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Second, 2) + " " + Language.Phrase("PM");
					} else {
						sDT = ew_ZeroPad(DT.Hour, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Minute, 2) + EW_TIME_SEPARATOR + ew_ZeroPad(DT.Second, 2);
					}
				} else if (ANamedFormat == 4) {
					sDT = DT.ToString("HH'" + EW_TIME_SEPARATOR + "'mm'" + EW_TIME_SEPARATOR + "'ss");
				} else if (ANamedFormat == 5 || ANamedFormat == 9) {
					sDT = DT.ToString("yyyy'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'dd");
				} else if (ANamedFormat == 6 || ANamedFormat == 10) {
					sDT = DT.ToString("MM'" + EW_DATE_SEPARATOR + "'dd'" + EW_DATE_SEPARATOR + "'yyyy");
				} else if (ANamedFormat == 7 || ANamedFormat == 11) {
					sDT = DT.ToString("dd'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'yyyy");
				} else if (ANamedFormat == 8) {
					sDT = DT.ToString(EW_DATE_FORMAT);
					if (DT.Hour != 0 || DT.Minute != 0 || DT.Second != 0)
						sDT += " " + DT.ToString("HH'" + EW_TIME_SEPARATOR + "'mm'" + EW_TIME_SEPARATOR + "'ss");
				} else if (ANamedFormat == 12 || ANamedFormat == 15) {
					sDT = DT.ToString("yy'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'dd");
				} else if (ANamedFormat == 13 || ANamedFormat == 16) {
					sDT = DT.ToString("MM'" + EW_DATE_SEPARATOR + "'dd'" + EW_DATE_SEPARATOR + "'yy");
				} else if (ANamedFormat == 14 || ANamedFormat == 17) {
					sDT = DT.ToString("dd'" + EW_DATE_SEPARATOR + "'MM'" + EW_DATE_SEPARATOR + "'yy");
				} else {
					return DT.ToString();
				}
				if ((ANamedFormat >= 9 && ANamedFormat <= 11) || (ANamedFormat >= 15 && ANamedFormat <= 17))
					sDT += " " + DT.ToString("HH'" + EW_TIME_SEPARATOR + "'mm'" + EW_TIME_SEPARATOR + "'ss");
					return sDT;
			} else {
				return Convert.ToString(ADate);
			}
		}

		// Setup NumberFormatInfo // DN
		public static void ew_SetupNumberFormatInfo(NumberFormatInfo info = null)
		{
			info = info ?? CurrentNumberFormatInfo;

			// CurrencyPositivePattern // DN
			// 0 $n
			// 1 n$
			// 2 $ n
			// 3 n $
			// Note: EW_POSITIVE_SIGN and EW_P_SIGN_POSN are not supported

			if (EW_P_SEP_BY_SPACE == 0 && EW_P_CS_PRECEDES == 1) {
				info.CurrencyPositivePattern = 0; // 0 $n
			} else if (EW_P_SEP_BY_SPACE == 0 && EW_P_CS_PRECEDES == 0) {
				info.CurrencyPositivePattern = 1; // 1 n$
			} else if (EW_P_SEP_BY_SPACE == 1 && EW_P_CS_PRECEDES == 1) {
				info.CurrencyPositivePattern = 2; // 2 $ n
			} else if (EW_P_SEP_BY_SPACE == 1 && EW_P_CS_PRECEDES == 0) {
				info.CurrencyPositivePattern = 3; // 3 n $
			}

			// CurrencyNegativePattern // DN
			// 0 ($n)
			// 1 -$n
			// 2 $-n
			// 3 $n-
			// 4 (n$)
			// 5 -n$
			// 6 n-$
			// 7 n$-
			// 8 -n $
			// 9 -$ n
			// 10 n $-
			// 11 $ n-
			// 12 $ -n
			// 13 n- $
			// 14 ($ n)
			// 15 (n $)

			if (EW_N_SIGN_POSN == 0 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 0; // 0 ($n)
			else if (EW_N_SIGN_POSN == 0 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 4; // 4 (n$)
			else if (EW_N_SIGN_POSN == 0 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 14; // 14 ($ n)
			else if (EW_N_SIGN_POSN == 0 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 15; // 15 (n $)
			else if (EW_N_SIGN_POSN == 1 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 1; // 1 -$n
			else if (EW_N_SIGN_POSN == 1 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 5; // 5 -n$
			else if (EW_N_SIGN_POSN == 1 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 9; // 9 -$ n
			else if (EW_N_SIGN_POSN == 1 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 8; // 8 -n $
			else if (EW_N_SIGN_POSN == 2 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 3; // 3 $n-
			else if (EW_N_SIGN_POSN == 2 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 7; // 7 n$-
			else if (EW_N_SIGN_POSN == 2 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 11; // 11 $ n-
			else if (EW_N_SIGN_POSN == 2 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 10; // 10 n $-
			else if (EW_N_SIGN_POSN == 3 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 1; // 1 -$n
			else if (EW_N_SIGN_POSN == 3 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 6; // 6 n-$
			else if (EW_N_SIGN_POSN == 3 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 9; // 9 -$ n
			else if (EW_N_SIGN_POSN == 3 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 13; // 13 n- $
			else if (EW_N_SIGN_POSN == 4 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 7; // 7 n$-
			else if (EW_N_SIGN_POSN == 4 && EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 2; // 2 $-n
			else if (EW_N_SIGN_POSN == 4 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 1)
				info.CurrencyNegativePattern = 12; // 12 $ -n
			else if (EW_N_SIGN_POSN == 4 && EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 0)
				info.CurrencyNegativePattern = 10; // 10 n $-

			// NumberNegativePattern // DN
			// 0 (n)
			// 1 -n
			// 2 - n
			// 3 n-
			// 4 n -

			if (EW_N_SIGN_POSN == 0)
				info.NumberNegativePattern = 0; // 0 (n)
			else if ((EW_N_SIGN_POSN == 1 || EW_N_SIGN_POSN == 3) && EW_N_SEP_BY_SPACE == 0)
				info.NumberNegativePattern = 1; // 1 -n
			else if ((EW_N_SIGN_POSN == 2 || EW_N_SIGN_POSN == 3) && EW_N_SEP_BY_SPACE == 1)
				info.NumberNegativePattern = 2; // 2 - n
			else if ((EW_N_SIGN_POSN == 2 || EW_N_SIGN_POSN == 4) && EW_N_SEP_BY_SPACE == 0)
				info.NumberNegativePattern = 3; // 3 n-
			else if ((EW_N_SIGN_POSN == 2 || EW_N_SIGN_POSN == 4) && EW_N_SEP_BY_SPACE == 1)
				info.NumberNegativePattern = 4; // 4 n -

			// PercentPositivePattern // DN
			// 0 n %
			// 1 n%
			// 2 %n
			// 3 % n

			if (EW_P_SEP_BY_SPACE == 0)
				info.PercentPositivePattern = 1; // 1 n%
			else
				info.PercentPositivePattern = 0; // 0 n %

			// PercentNegativePattern //DN
			// 0 -n %
			// 1 -n%
			// 2 -%n
			// 3 %-n
			// 4 %n-
			// 5 n-%
			// 6 n%-
			// 7 -% n
			// 8 n %-
			// 9 % n-
			// 10 % -n
			// 11 n- %

			if (EW_N_SIGN_POSN == 0 && EW_N_SEP_BY_SPACE == 0)
				info.PercentNegativePattern = 2; // 2 -n%
			else if (EW_N_SIGN_POSN == 0 && EW_N_SEP_BY_SPACE == 1)
				info.PercentNegativePattern = 0; // 0 -n %
			else if (EW_N_SIGN_POSN == 1 && EW_N_SEP_BY_SPACE == 0)
				info.PercentNegativePattern = 1; // 1 -n%
			else if (EW_N_SIGN_POSN == 1 && EW_N_SEP_BY_SPACE == 1)
				info.PercentNegativePattern = 0; // 0 -n %
			else if (EW_N_SIGN_POSN == 2 && EW_N_SEP_BY_SPACE == 0)
				info.PercentNegativePattern = 6; // 6 n%-
			else if (EW_N_SIGN_POSN == 2 && EW_N_SEP_BY_SPACE == 1)
				info.PercentNegativePattern = 8; // 8 n %-
			else if (EW_N_SIGN_POSN == 3 && EW_N_SEP_BY_SPACE == 0)
				info.PercentNegativePattern = 5; // 2 n-%
			else if (EW_N_SIGN_POSN == 3 && EW_N_SEP_BY_SPACE == 1)
				info.PercentNegativePattern = 11; // 11 n- %
			else if (EW_N_SIGN_POSN == 4 && EW_N_SEP_BY_SPACE == 0)
				info.PercentNegativePattern = 6; // 6 n%-
			else if (EW_N_SIGN_POSN == 4 && EW_N_SEP_BY_SPACE == 1)
				info.PercentNegativePattern = 8; // 8 n %-
		}

		// Format number
		// -2 Retain all values after decimal place

		public static string ew_FormatNumber(object Expression, int NumDigitsAfterDecimal = -1, int IncludeLeadingDigit = -2, int UseParensForNegativeNumbers = -2, int GroupDigits = -2)
		{
			if (Convert.IsDBNull(Expression))
				return String.Empty;
			NumberFormatInfo info = (NumberFormatInfo)CurrentNumberFormatInfo.Clone();
			string sExpression = Convert.ToString(Expression, info);
			if (!ew_IsNumeric(sExpression))
				return sExpression;

			// Check NumDigitsAfterDecimal
			if (NumDigitsAfterDecimal == -2) { // Use all values after decimal point
				if (sExpression.Contains(info.NumberDecimalSeparator)) {
					info.NumberDecimalDigits = sExpression.Length - sExpression.LastIndexOf(info.NumberDecimalSeparator) - 1;
				} else {
					info.NumberDecimalDigits = 0;
				}
			} else if (NumDigitsAfterDecimal > -1) {
				info.NumberDecimalDigits = NumDigitsAfterDecimal;
			}

			// Check GroupDigits
			if (GroupDigits == 0)
				info.NumberGroupSeparator = "";

			// Check UseParensForNegativeNumbers
			if (UseParensForNegativeNumbers == -1)
				info.NumberNegativePattern = 0;
			else if (UseParensForNegativeNumbers == 0 && info.NumberNegativePattern == 0)
				info.NumberNegativePattern = 1; // Assume NumberNegativePattern = 1
			var val = Convert.ToDouble(Expression).ToString("N", info);

			// Check IncludeLeadingDigit
			if (IncludeLeadingDigit == 0 && Regex.IsMatch(val, @"^[\(" + Regex.Escape(info.NegativeSign) + @"]\s?0" + Regex.Escape(info.NumberDecimalSeparator)))
				val = val.Replace("0" + info.NumberDecimalSeparator, info.NumberDecimalSeparator);
			return val;
		}
		public static string ew_FormatPercent(object Expression, int NumDigitsAfterDecimal = -1, int IncludeLeadingDigit = -2, int UseParensForNegativeNumbers = -2, int GroupDigits = -2)
		{
			if (Convert.IsDBNull(Expression))
				return String.Empty;
			NumberFormatInfo info = (NumberFormatInfo)CurrentNumberFormatInfo.Clone();
			string sExpression = Convert.ToString(Expression, info);
			if (!ew_IsNumeric(sExpression))
				return sExpression;

			// Check NumDigitsAfterDecimal
			if (NumDigitsAfterDecimal == -2) { // Use all values after decimal point
				if (sExpression.Contains(info.PercentDecimalSeparator)) {
					info.PercentDecimalDigits = sExpression.Length - sExpression.LastIndexOf(info.PercentDecimalSeparator) - 1;
				} else {
					info.PercentDecimalDigits = 0;
				}
			} else if (NumDigitsAfterDecimal > -1) {
				info.PercentDecimalDigits = NumDigitsAfterDecimal;
			}

			// Check GroupDigits
			if (GroupDigits == 0)
				info.PercentGroupSeparator = "";
			var val = Convert.ToDouble(Expression).ToString("P", info);

			// Check IncludeLeadingDigit
			if (IncludeLeadingDigit == 0 && Convert.ToDouble(Expression) > -0.01 && Convert.ToDouble(Expression) < 0.01)
				val = val.Replace("0" + info.PercentDecimalSeparator, info.PercentDecimalSeparator);
			if (val.Contains(info.NegativeSign) && UseParensForNegativeNumbers == -1) {
				val = "(" + val.Replace(info.NegativeSign, "") + ")";
			}
			return val;
		}

		// Format currency
		public static string ew_FormatCurrency(object Expression, int NumDigitsAfterDecimal = -1, int IncludeLeadingDigit = -2, int UseParensForNegativeNumbers = -2, int GroupDigits = -2)
		{
			if (Convert.IsDBNull(Expression))
				return String.Empty;
			NumberFormatInfo info = (NumberFormatInfo)CurrentNumberFormatInfo.Clone();
			string sExpression = Convert.ToString(Expression, info);
			if (!ew_IsNumeric(sExpression))
				return sExpression;

			// Check NumDigitsAfterDecimal
			if (NumDigitsAfterDecimal == -2) { // Use all values after decimal point
				if (sExpression.Contains(info.CurrencyDecimalSeparator)) {
					info.CurrencyDecimalDigits = sExpression.Length - sExpression.LastIndexOf(info.CurrencyDecimalSeparator) - 1;
				} else {
					info.CurrencyDecimalDigits = 0;
				}
			} else if (NumDigitsAfterDecimal > -1) {
				info.CurrencyDecimalDigits = NumDigitsAfterDecimal;
			}

			// Check GroupDigits
			if (GroupDigits == 0)
				info.CurrencyGroupSeparator = "";

			// Check UseParensForNegativeNumbers
			if (UseParensForNegativeNumbers == -1) {
				if (EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 1)
					info.CurrencyNegativePattern = 0; // ($n)
				else if (EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 0)
					info.CurrencyNegativePattern = 4; // (n$)
				else if (EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 1)
					info.CurrencyNegativePattern = 14; // ($ n)
				else if (EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 0)
					info.CurrencyNegativePattern = 15; // (n $)
			} else if (UseParensForNegativeNumbers == 0) {
				if (EW_N_SIGN_POSN == 0) {
					if (EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 1)
						info.CurrencyNegativePattern = 1; // 1 -$n
					else if (EW_N_SEP_BY_SPACE == 0 && EW_N_CS_PRECEDES == 0)
						info.CurrencyNegativePattern = 6; // 6 n-$
					else if (EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 1)
						info.CurrencyNegativePattern = 9; // 9 -$ n
					else if (EW_N_SEP_BY_SPACE == 1 && EW_N_CS_PRECEDES == 0)
						info.CurrencyNegativePattern = 13; // 13 n- $
				}
			}
			var val = Convert.ToDouble(Expression).ToString("C", info);

			// Check IncludeLeadingDigit
			if (IncludeLeadingDigit == 0 && Convert.ToDouble(Expression) > -1 && Convert.ToDouble(Expression) < 1)
				val = val.Replace("0" + info.CurrencyDecimalSeparator, info.CurrencyDecimalSeparator);
			return val;
		}

		// Check if object is IList
		public static bool ew_IsList(object obj)
		{
			return (obj != null) && (obj is IList);
		}

		// Check if object is IDictionary
		public static bool ew_IsDictionary(object obj)
		{
			return (obj != null) && (obj is IDictionary);
		}

		// check if a value is in a string array
		public static bool ew_Contains(object str, string[] ar)
		{
			return (new List<string>(ar)).Contains(Convert.ToString(str));
		}

		// check if a value is in an integer array
		public static bool ew_Contains(object i, int[] ar)
		{
			try {
				return (new List<int>(ar)).Contains(Convert.ToInt32(i));
			} catch {
				return false;
			}
		}

		// Global random
		private static Random GlobalRandom = new Random();

		// Generate random number
		public static int ew_Random()
		{
			lock (GlobalRandom) {
				var newRandom = new Random(GlobalRandom.Next());
				return newRandom.Next();
			}
		}

		// Generate a random code with specified number of digits
		public static string ew_Random(int n)
		{
			lock (GlobalRandom) {
				var newRandom = new Random(GlobalRandom.Next());
				var s = "";
				for (int i = 0; i < n; i++)
					s = String.Concat(s, newRandom.Next(10).ToString());
				return s;
			}
		}

		// Get query string value
		public static string ew_Get(string name)
		{
			return ew_QueryString[name].ToString();
		}

		// Get form value
		public static string ew_Post(string name)
		{
			return ew_Form?[name].ToString() ?? "";
		}

		// Cookie
		public static cCookie ew_Cookie;

		// Cookie class
		public class cCookie
		{
			public string this[string name] {
				get {
					return ew_Request.Cookies[EW_PROJECT_NAME + "_" + name];
				}
				set {
					name = EW_PROJECT_NAME + "_" + name;

					//ew_Response.Cookies.Delete(name);
					var options = new CookieOptions();
					options.Path = ew_AppPath();
					options.Expires = EW_COOKIE_EXPIRY_TIME;
					ew_Response.Cookies.Append(name, value, options);
				}
			}
		}

		// Session
		public static cSession ew_Session;

		// Session class
		public class cSession
		{
			public ISession _session {
				get {
					return ew_HttpContext.Session;
				}
			}
			public IEnumerable<string> Keys {
				get {
					return _session.Keys;
				}
			}
			public string SessionID {
				get {
					return _session.Id;
				}
			}
			public void Remove(string key) {
				_session.Remove(key);
			}
			public void Clear() {
				_session.Clear();
			}

			// Serialize and set
			public void Set(string key, object value) {
				if (value == null)
					Remove(key);
				else
					_session.SetString(key, JsonConvert.SerializeObject(value));
			}

			// Get as object
			public object Get(string key) {
				try {
					var data = _session.GetString(key);
					if (data != null)
						return JsonConvert.DeserializeObject(data);
					return null;
				} catch {
					return null;
				}
			}

			// Get as type T
			public T Get<T>(string key) {
				try {
					var data = _session.GetString(key);
					if (data != null)
						return JsonConvert.DeserializeObject<T>(data);
					return default(T);
				} catch {
					return default(T);
				}
			}

			// Get/Set as string
			public object this[string name] {
				get {
					return _session.GetString(name);
				}
				set {
					_session.SetString(name, Convert.ToString(value));
				}
			}
		}

		// Write literal
		public static void ew_Write(object value) {
			ew_View?.WriteLiteral(value);
		}

		// Write binary data to response
		public static void ew_BinaryWrite(byte[] value) {
			ew_Response.Body.WriteAsync(value, 0, value.Length).Wait();
		}

		// Write string to response
		public static void ew_ResponseWrite(string str, string enc = null) {
			ew_Response.WriteAsync(str, Encoding.GetEncoding(enc ?? EW_CHARSET)).Wait();
		}

		// Clear response body only (not headers)
		public static void ew_ResponseClear() {
			if (ew_Response.Body.CanSeek)
				ew_Response.Body.SetLength(0);
		}

		// Export object info as JSON string
		public static string ew_VarExport(params object[] list) {
			string str = "";
			foreach (object value in list) {
				try {
					str += "<pre>" + ew_HtmlEncode(JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented,
						new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })) + "</pre>";
				} catch (Exception e) {
					str += "<pre>" + ew_HtmlEncode(e.Message) + "</pre>";
					continue;
				}
			}
			return str;
			}

		// Write object info
		public static void ew_VarDump(params object[] list) {
			var str = ew_VarExport(list);
			if (ew_View != null)
				ew_Write(str);
			else
				ew_ResponseWrite(str);
		}

		// Write object info (alias of ew_VarDump())
		public static void ew_VarPrint(params object[] list) {
			ew_VarDump(list);
		}
		/*

		// Memory cache ticket store
		public class MemoryCacheTicketStore : ITicketStore
		{
			private const string KeyPrefix = "AuthSessionStore-";
			private IMemoryCache _cache;
			public MemoryCacheTicketStore()
			{
				_cache = new MemoryCache(new MemoryCacheOptions());
			}
			public async Task<string> StoreAsync(AuthenticationTicket ticket)
			{
				var guid = Guid.NewGuid();
				var key = KeyPrefix + guid.ToString();
				await RenewAsync(key, ticket);
				return key;
			}
			public Task RenewAsync(string key, AuthenticationTicket ticket)
			{
				var options = new MemoryCacheEntryOptions();
				var expiresUtc = ticket.Properties.ExpiresUtc;
				if (expiresUtc.HasValue)
					options.SetAbsoluteExpiration(expiresUtc.Value);
				options.SetSlidingExpiration(TimeSpan.FromMinutes(EW_SESSION_TIMEOUT));
				_cache.Set(key, ticket, options);
				return Task.FromResult(0);
			}
			public Task<AuthenticationTicket> RetrieveAsync(string key)
			{
				AuthenticationTicket ticket;
				_cache.TryGetValue(key, out ticket);
				return Task.FromResult(ticket);
			}
			public Task RemoveAsync(string key)
			{
				_cache.Remove(key);
				return Task.FromResult(0);
			}
		}
		*/

		// Error exception // DN
		public class ContentException : Exception {
			public ContentException() {}
			public ContentException(string message) : base(message) {}
			public ContentException(string message, Exception inner) : base(message, inner) {}
		}

		// Redirect exception // DN
		public class RedirectException : Exception {
			public RedirectException() {}
			public RedirectException(string message) : base(message) {}
			public RedirectException(string message, Exception inner) : base(message, inner) {}
		}

		// Exception handler middleware
		public class ExceptionHandler {
			private readonly RequestDelegate _next;
			public ExceptionHandler(RequestDelegate next) {
					_next = next;
			}
			public async Task Invoke(HttpContext context) {
				try {
					await _next(context);
				} catch (RedirectException e) {
					if (!context.Response.HasStarted)
						context.Response.Redirect(e.Message);
				} catch (ContentException e) {
					if (ew_NotEmpty(e.Message)) {
						context.Response.Clear();
						context.Response.StatusCode = 500;
						context.Response.ContentType = "text/html";
						await context.Response.WriteAsync("<html><body>\r\n" + e.Message + "</body></html>\r\n");
						await context.Response.WriteAsync(new string(' ', 512)); // Padding for IE
					}
				}
			}
		}

		// Exit page // DN
		public static void ew_End(object value = null) {
			var result = "";
			if (value is string) { // String => Message
				result = (string)value;
			} else if (value != null) { // Object
				result = ew_VarExport(value);
			}
			CurrentPage?.Page_Terminate();
			ew_GCCollect();
			throw new ContentException(result);
		}

		// App URL //DN
		public static string ew_AppUrl(string url) {
			var path = ew_AppPath();
			if (!url.StartsWith(path))
				url = ew_AppPath(url);
			return url;
		}

		// Redirect // DN
		public static void ew_Redirect(string url) {
			throw new RedirectException(ew_AppUrl(url));
		}

		// Write HTTP header
		public static void ew_Header(bool cache, string charset = EW_CHARSET) {
			string export = ew_Get("export");
			if (cache || ew_IsHttps() && ew_NotEmpty(export) && export != "print") { // Allow cache
				ew_AddHeader(HeaderNames.CacheControl, "private, must-revalidate");
				ew_AddHeader(HeaderNames.Pragma, "public");
			} else { // No cache
				ew_AddHeader(HeaderNames.Expires, "-1");
				ew_AddHeader(HeaderNames.LastModified, DateTime.Now.ToString("R")); // Always modified
				ew_AddHeader(HeaderNames.CacheControl, "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
				ew_AddHeader(HeaderNames.Pragma, "no-cache");
			}
			ew_AddHeader("X-UA-Compatible", "IE=edge");
			ew_Response.ContentType = "text/html" + (ew_NotEmpty(charset) ? "; charset=" + charset : ""); // DN
		}

		// Get content file extension
		public static string ew_ContentExt(IEnumerable<byte> data) {
			var ct = ew_ContentType(data.Take(11));
			switch (ct) {
				case "image/gif":
					return ".gif"; // Return gif
				case "image/jpeg":
					return ".jpg"; // Return jpg
				case "image/png":
					return ".png"; // Return png
				case "image/bmp":
					return ".bmp"; // Return bmp
				case "application/pdf":
					return ".pdf"; // Return pdf
				default:
					return ""; // Unknown extension
			}
		}

		// Add HTTP header
		public static void ew_AddHeader(string name, string value) {
			if (ew_NotEmpty(name)) { // value can be empty
				if (ew_Response.Headers.ContainsKey(name)) {
					ew_Response.Headers[name] = value;
				} else {
					ew_Response.Headers.Add(name, value);
				}
			}
		}

		// check if allow add/delete row
		public static bool ew_AllowAddDeleteRow() {
			return true;
		}

		// Mobile Detect class // DN
		// Based on https://github.com/serbanghita/Mobile-Detect

		public class cMobileDetect
		{
			public JObject data;
			private string _userAgent;
			private Dictionary<string, StringValues> httpHeaders;
			private IEnumerable<JToken> _rules;

			// Constructor
			public cMobileDetect(string userAgent = "")
			{
				LoadJsonData();
				_userAgent = userAgent;
				httpHeaders = ew_Request.Headers.ToDictionary(kvp => "HTTP_" + kvp.Key.Replace("-", "_").ToUpper(), kvp => kvp.Value); // Convert header to HTTP_*
			 }

			// Check if the device is mobile
			public bool IsMobile
			{
				get
				{
					return CheckHttpHeadersForMobile() || MatchDetectionRulesAgainstUa();
				}
			}

			// To access the user agent, retrieved from http headers if not explicitly set
			public string UserAgent
			{
				get
				{
					if (ew_Empty(_userAgent))
						_userAgent = ParseHeadersForUserAgent();
					return _userAgent;
				}
				set
				{
					_userAgent = value;
				}
			}

			// Check if the device is a tablet
			public bool IsTablet
			{
				get
				{
					var tablets = data["uaMatch"]["tablets"];
					return MatchDetectionRulesAgainstUa(tablets);
				}
			}

			// Checks if the device is conforming to the provided key
			// e.g .Is("ios") / .Is("androidos") / .Is("iphone")

			public bool Is(string key)
			{
				 var rules = Rules.Where(rule => ew_SameText(((JProperty)rule).Name, key));
				 if (rules.Count() > 0)
					return Match((string)rules.First());
				return false;
			}

			// Load JSON data
			private void LoadJsonData()
			{
				data = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(ew_ServerMapPath("js/Mobile_Detect.json")));
			}

			// UA HTTP headers
			private List<string> UaHttpHeaders
			{
				get
				{
					return data["uaHttpHeaders"].Select(h => (string)h).ToList();
				}
			}

			// Parse the headers for the user agent - uses a list of possible keys as provided by upstream
			// returns a concatenated list of possible user agents, should be just 1

			private string ParseHeadersForUserAgent()
			{
				IEnumerable<StringValues> ar = httpHeaders.Where(kvp => UaHttpHeaders.Contains(kvp.Key)).Select(kvp => kvp.Value);
				return String.Join(" ", ar).Trim();
			}

			// Rules
			private IEnumerable<JToken> Rules
			{
				get
				{
					if (_rules == null) {
						var phones = data["uaMatch"]["phones"];
						var tablets = data["uaMatch"]["tablets"];
						var browsers = data["uaMatch"]["browsers"];
						var os = data["uaMatch"]["os"];
						_rules = phones.Concat(tablets).Concat(browsers).Concat(os);
					}
					return _rules;
				}
			}

			// Check the HTTP headers for signs of mobile
			private bool CheckHttpHeadersForMobile()
			{
				var headerMatch = data["headerMatch"];
				foreach (JProperty token in headerMatch) {
					var mobileHeader = token.Name;
					var matchType = token.Value;
					if (httpHeaders.ContainsKey(mobileHeader)) {
						if (matchType == null || !(matchType["matches"] is JArray))
							return false;
						var matches = matchType["matches"].Select(m => (string)m).ToList();
						foreach (var match in matches) {
							if (httpHeaders[mobileHeader].ToString().Contains(match))
								return true;
						}
						return false;
					}
				}
				return false;
			}

			// Check custom regexes against the User-Agent string
			private bool Match(JToken keyRegex, string uaString = "")
			{
				if (ew_Empty(uaString))
					uaString = UserAgent;
				string regex = (string)keyRegex;
				regex = regex.Replace("/", "\\/"); // Escape the special character which is the delimiter
				return Regex.IsMatch(uaString, regex, RegexOptions.IgnoreCase);
			}

			// Find a detection rule that matches the current User-agent
			private bool MatchDetectionRulesAgainstUa(IEnumerable<JToken> rules = null)
			{
				rules = rules ?? Rules;
				foreach (var regex in rules)
				{
					if (this.Match(regex))
						return true;
				}
				return false;
			}
		}

		// Check if mobile device
		public static bool ew_IsMobile()
		{
			var md = new cMobileDetect();
			return md.IsMobile;
		}

		// Convert List<OrderedDictionary> to JSON array of array
		public static string ew_ArrayToJson(List<OrderedDictionary> list, int offset = 0)
		{
			return JsonConvert.SerializeObject(list.Skip(offset).Select(od => od.Values));
		}

		// Convert object (e.g. IList<T> or IDictionary<TKey, TValue>) to JSON
		public static string ew_ArrayToJson(object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		// Create instance by class name
		public static dynamic ew_CreateInstance(string name, object[] args = null, Type[] types = null) {
			Type t = Type.GetType(EW_PROJECT_CLASSNAME + "+" + name) ?? // This class
				Type.GetType(EW_PROJECT_CLASSNAME + "_base+" + name); // Base class
			if (types != null)
				t = t.MakeGenericType(types);
			return Activator.CreateInstance(t, args);
		}

		//
		// Export document classes
		//
		// Get export document object

		public static dynamic ew_ExportDocument(cTable tbl, string style) {
			return ew_CreateInstance(EW_EXPORT[gsExport.ToLower()], new object[] {tbl, style});
		}

		//
		// Base class for export
		//

		public class cExportBase {
			public dynamic Table;
			public string Line = "";
			public string Header = "";
			public string Style = "h"; // "v"(Vertical) or "h"(Horizontal)
			public bool Horizontal = true; // Horizontal
			public int RowCnt = 0;
			public int FldCnt = 0;
			public bool ExportCustom = false;
			public StringBuilder Text = new StringBuilder();

			// Constructor
			public cExportBase(cTable tbl = null, string style = "") {
				Table = tbl;
				SetStyle(style);
			}

			// Style
			public virtual void SetStyle(string style) {
				if (ew_SameStr(style, "v") || ew_SameStr(style, "h"))
					Style = style.ToLower();
				Horizontal = !ew_SameStr(Style, "v");
			}

			// Field caption
			public virtual void ExportCaption(cField fld) {
				FldCnt++;
				ExportValueEx(fld, fld.ExportCaption);
			}

			// Field value
			public virtual void ExportValue(cField fld) {
				ExportValueEx(fld, fld.ExportValue);
			}

			// Field aggregate
			public virtual void ExportAggregate(cField fld, string type) {
				FldCnt++;
				if (Horizontal) {
					var val = "";
					if ((new List<string>() {"TOTAL", "COUNT", "AVERAGE"}).Contains(type))
						val = Language.Phrase(type) + ": " + fld.ExportValue;
					ExportValueEx(fld, val);
				}
			}

			// Get meta tag for charset
			public virtual string CharsetMetaTag() {
				return "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=" + EW_CHARSET + "\">\r\n";
			}

			// Table header
			public virtual void ExportTableHeader() {
				Text.Append("<table class=\"ewExportTable\">");
			}

			// Cell styles
			public virtual string CellStyles(cField fld, bool usestyle = true) {
				return (usestyle && EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "";
			}

			// Row styles
			public virtual string RowStyles(bool usestyle = true) {
				return (usestyle && EW_EXPORT_CSS_STYLES) ? Table.RowStyles : "";
			}

			// Export a value (caption, field value, or aggregate)
			public virtual void ExportValueEx(cField fld, object val, bool usestyle = true) {
				Text.Append("<td" + CellStyles(fld, usestyle) + ">");
				Text.Append(Convert.ToString(val));
				Text.Append("</td>");
			}

			// Begin a row
			public virtual void BeginExportRow(int rowcnt = 0, bool usestyle = true) {
				RowCnt++;
				FldCnt = 0;
				if (Horizontal) {
					if (rowcnt == -1) {
						Table.CssClass = "ewExportTableFooter";
					} else if (rowcnt == 0) {
						Table.CssClass = "ewExportTableHeader";
					} else {
						Table.CssClass = ((rowcnt % 2) == 1) ? "ewExportTableRow" : "ewExportTableAltRow";
					}
					Text.Append("<tr" + RowStyles(usestyle) + ">");
				}
			}

			// End a row
			public virtual void EndExportRow() {
				if (Horizontal)
					Text.Append("</tr>");
			}

			// Empty row
			public virtual void ExportEmptyRow() {
				RowCnt++;
				Text.Append("<br>");
			}

			// Page break
			public virtual void ExportPageBreak() {
			}

			// Export a field
			public virtual void ExportField(cField fld) {
				FldCnt++;
				var wrkExportValue = "";
				if (ew_NotEmpty(fld.HrefValue2) && fld.Upload != null) { // Upload field
					if (ew_NotEmpty(fld.Upload.DbValue))
						wrkExportValue = ew_GetFileATag(fld, fld.HrefValue2);
				} else {
					wrkExportValue = fld.ExportValue;
				}
				if (Horizontal) {
					ExportValueEx(fld, wrkExportValue);
				} else { // Vertical, export as a row
					RowCnt++;
					Text.Append("<tr class=\"" + ((FldCnt % 2 == 1) ? "ewExportTableRow" : "ewExportTableAltRow") + "\">" +
						"<td>" + fld.ExportCaption + "</td>");
					Text.Append("<td" + CellStyles(fld) + ">" + fld.ExportValue + "</td></tr>");
				}
			}

			// Table Footer
			public virtual void ExportTableFooter() {
				Text.Append("</table>");
			}

			// Add HTML tags
			public virtual void ExportHeaderAndFooter() {
				string header = "<html><head>\r\n";
				header += CharsetMetaTag();
				if (EW_EXPORT_CSS_STYLES && ew_NotEmpty(EW_PROJECT_STYLESHEET_FILENAME))
					header += "<style type=\"text/css\">" + ew_LoadTxt(EW_PROJECT_STYLESHEET_FILENAME) + "</style>\r\n";
				header += "</" + "head>\r\n<body>\r\n";
				Text.Insert(0, header);
				Text.Append("</body></html>");
			}

			// Export
			public virtual void Export() {
				if (!EW_DEBUG_ENABLED)
					ew_Response.Clear();
				if (ew_SameText(EW_CHARSET, "utf-8"))
					ew_BinaryWrite(new byte[] {0xEF, 0xBB, 0xBF});
				ew_ResponseWrite(Text.ToString());
			}
		}

		// Get file IMG tag
		public static string ew_GetFileImgTag(cField fld, string fn) {
			var html = "";
			if (ew_NotEmpty(fn)) {
				if (fld.UploadMultiple) {
					var wrkfiles = fn.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
					foreach (var wrkfile in wrkfiles) {
						if (ew_NotEmpty(wrkfile)) {
							if (ew_NotEmpty(html))
								html += "<br>";
							html += "<img class=\"ewImage\" src=\"" + wrkfile + "\" alt=\"\">";
						}
					}
				} else {
					html = "<img class=\"ewImage\" src=\"" + fn + "\" alt=\"\">";
				}
			}
			return html;
		}

		// Get file anchor tag
		public static string ew_GetFileATag(cField fld, object fileName) {
			string[] wrkfiles = {};
			var wrkpath = "";
			var html = "";
			var fn = Convert.ToString(fileName);
			if (fld.FldDataType == EW_DATATYPE_BLOB) {
				if (ew_NotEmpty(fld.Upload.DbValue))
					wrkfiles = new string[] {fn};
			} else if (fld.UploadMultiple) {
				wrkfiles = fn.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
				var pos = wrkfiles[0].LastIndexOf("/");
				if (pos > -1) {
					wrkpath = wrkfiles[0].Substring(0, pos + 1); // Get path from first file name
					wrkfiles[0] = wrkfiles[0].Substring(pos + 1);
				}
			} else {
				if (ew_NotEmpty(fld.Upload.DbValue))
					wrkfiles = new string[] {fn};
			}
			foreach (var wrkfile in wrkfiles) {
				if (ew_NotEmpty(wrkfile)) {
					if (ew_NotEmpty(html))
						html += "<br>";
					var attrs = new cAttributes() {{"href", ew_ConvertFullUrl(wrkpath + wrkfile)}};
					html += ew_HtmlElement("a", attrs, fld.FldCaption);
				}
			}
			return html;
		}

		// Get file temp image
		public static string ew_GetFileTempImage(cField fld, string val) {
			if (fld.UploadMultiple) {
				var files = val.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
				string images = "";
				for (var i = 0; i < files.Count(); i++) {
					if (files[i] != "") {
						var tmpimage = File.ReadAllBytes(ew_UploadPathEx(true, fld.UploadPath) + files[i]);
						if (fld.ImageResize)
							ew_ResizeBinary(ref tmpimage, ref fld.ImageWidth, ref fld.ImageHeight);
						if (images != "") images += EW_MULTIPLE_UPLOAD_SEPARATOR;
						images += ew_TmpImage(tmpimage);
					}
				}
				return images;
			} else {
				if (fld.FldDataType == EW_DATATYPE_BLOB) {
					if (!Convert.IsDBNull(fld.Upload.DbValue)) { // DN
						byte[] tmpimage = (byte[])fld.Upload.DbValue;
						if (fld.ImageResize)
							ew_ResizeBinary(ref tmpimage, ref fld.ImageWidth, ref fld.ImageHeight);
						return ew_TmpImage(tmpimage);
					}
				} else if (val != "") {
					var tmpimage = File.ReadAllBytes(ew_UploadPathEx(true, fld.UploadPath) + val);
					if (fld.ImageResize)
						ew_ResizeBinary(ref tmpimage, ref fld.ImageWidth, ref fld.ImageHeight);
					return ew_TmpImage(tmpimage);
				}
			}
			return "";
		}

		// Get file upload url
		public static string ew_GetFileUploadUrl(cField fld, string val, bool resize = false, bool encrypt = EW_ENCRYPT_FILE_PATH, bool urlencode = true) {
			string path, key, fn;
			if (!ew_EmptyStr(val)) {
				path = (encrypt || resize) ? ew_IncludeTrailingDelimiter(fld.UploadPath, false) : ew_UploadPathEx(false, fld.UploadPath);
				if (encrypt) {
					key = EW_RANDOM_KEY + ew_Session.SessionID;
					fn = EW_FILE_URL + "?t=" + ew_Encrypt(fld.TblName, key) + "&fn=" + ew_Encrypt(path + val, key);
					if (resize)
						fn += "&width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
				} else if (resize) {
					fn = EW_FILE_URL + "?t=" + ew_UrlEncode(fld.TblName) + "&fn=" + ew_UrlEncodeFilePath(path + val) + "&width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
				} else {
					fn = path + val;
					if (urlencode)
						fn = ew_UrlEncodeFilePath(fn);
				}
				return fn;
			} else {
				return "";
			}
		}

		// URL-encode file name
		public static string ew_UrlEncodeFilename(string fn) {
			string path, filename;
			if (fn.Contains("?")) {
				var arf = fn.Split('?');
				fn = arf[1];
				var ar = fn.Split('&');
				for (var i = 0; i < ar.Length; i++) {
					var p = ar[i];
					if (p.StartsWith("fn=")) {
						ar[i] = "fn=" + ew_UrlEncode(p.Substring(3));
						break;
					}
				}
				return arf[0] + "?" + String.Join("&", ar);
			}
			if (fn.Contains("/")) {
				path = Path.GetDirectoryName(fn).Replace("\\", "/");
				filename = Path.GetFileName(fn);
			} else {
				path = "";
				filename = fn;
			}
			if (path != "")
				path = ew_IncludeTrailingDelimiter(path, false);
			return path + ew_UrlEncode(filename).Replace("+", " "); // Do not encode spaces
		}

		// URL Encode file path
		public static string ew_UrlEncodeFilePath(string path) {
			var ar = path.Split('/');
			for (var i = 0; i < ar.Count(); i++)
				ar[i] = ew_RawUrlEncode(ar[i]);
			return String.Join("/", ar);
		}

		// Get file view tag
		public static string ew_GetFileViewTag(cField fld, string val) {
			if (ew_EmptyStr(val))
				return "";
			string[] wrkfiles = null;
			string[] wrknames = null;
			string url, fn, image, images = "";
			if (fld.FldDataType == EW_DATATYPE_BLOB) {
				wrknames = new string[] {val};
				wrkfiles = new string[] {val};
			} else if (fld.UploadMultiple) {
				wrknames = val.Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
				wrkfiles = Convert.ToString(fld.Upload.DbValue).Split(EW_MULTIPLE_UPLOAD_SEPARATOR);
			} else {
				wrknames = new string[] {val};
				wrkfiles = new string[] {Convert.ToString(fld.Upload.DbValue)};
			}
			bool bMultiple = (wrkfiles.Count() > 1);
			string href = Convert.ToString(fld.HrefValue).Trim();
			bool bExport = (CurrentPage?.TableType == "REPORT" && (CurrentPage?.Export == "excel" || CurrentPage?.Export == "word") ||
				CurrentPage?.TableType != "REPORT" && (CurrentPage?.CustomExport == "pdf" || CurrentPage?.CustomExport == "email"));
			int wrkcnt = 0;
			foreach (var wrkfile in wrkfiles) {
				image = "";
				if (bExport && fld.FldViewTag == "IMAGE")
					fn = ew_GetFileTempImage(fld, wrkfile);
				else if (fld.FldDataType == EW_DATATYPE_BLOB)
					fn = val;
				else
					fn = ew_GetFileUploadUrl(fld, wrkfile, fld.ImageResize);
				if (fld.FldViewTag == "IMAGE" && (fld.IsBlobImage || ew_IsImageFile(wrkfile))) {
					if (href == "" && !fld.UseColorbox) {
						if (fn != "")
							image = "<img class=\"ewImage img-thumbnail\" src=\"" + ew_AppPath(fn) + "\"" + fld.ViewAttributes + ">"; // DN *** duplicate alt class=\"ewImage\" alt=\"\"
					} else {
						if (fld.UploadMultiple && href.Contains("%u"))
							fld.HrefValue = ew_AppPath(href.Replace("%u", ew_GetFileUploadUrl(fld, wrkfile)));
						if (fn != "")
							image = "<a" + fld.LinkAttributes + "><img class=\"ewImage img-thumbnail\" src=\"" + ew_AppPath(fn) + "\"" + fld.ViewAttributes + "></a>"; // DN *** duplicate alt class=\"ewImage\" alt=\"\"
					}
				} else {
					var name = "";
					if (fld.FldDataType == EW_DATATYPE_BLOB) {
						url = href;
						name = (ew_NotEmpty(fld.Upload.FileName)) ? fld.Upload.FileName : fld.FldCaption;
					} else {
						url = ew_GetFileUploadUrl(fld, wrkfile);
						name = (wrkcnt < wrknames.Count()) ? wrknames[wrkcnt] : wrknames[wrknames.Count()-1];
					}
					if (url != "") {
						if (fld.UploadMultiple && href.Contains("%u"))
							fld.HrefValue = ew_AppPath(href.Replace("%u", url));
						image = "<a" + fld.LinkAttributes + "\">" + name + "</a>"; // DN
					}
				}
				if (image != "") {
					if (bMultiple)
						images += "<li>" + image + "</li>";
					else
						images += image;
				}
				wrkcnt += 1;
			}
			if (bMultiple && images != "")
				images = "<ul class=\"list-inline\">" + images + "</ul>";
			return images;
		}

		// Get image view tag
		public static string ew_GetImgViewTag(cField fld, string val) {
			if (!ew_EmptyStr(val)) {
				string href = Convert.ToString(fld.HrefValue);
				string image = val;
				string fn = "";
				if (val != "" && val.IndexOf("://") < 0 && val.IndexOf("\\") < 0 && val.IndexOf("javascript:") < 0)
					fn = ew_GetImageUrl(fld, val, fld.ImageResize);
				else
					fn = val;
				if (ew_IsImageFile(val)) {
					if (href == "" && !fld.UseColorbox) {
						if (fn != "")
							image = "<img class=\"ewImage img-thumbnail\" alt=\"\" src=\"" + ew_AppPath(fn) + "\"" + fld.ViewAttributes + ">";
						} else {
							if (fn != "")
								image = "<a" + fld.LinkAttributes + "><img class=\"ewImage img-thumbnail\" alt=\"\" src=\"" + ew_AppPath(fn) + "\"" + fld.ViewAttributes + "></a>";
						}
				} else {
					string name = val;
					if (href != "")
						image = "<a" + fld.LinkAttributes + ">" + name + "</a>";
					else
						image = name;
				}
				return image;
			} else {
				return "";
			}
		}

		// Get image url
		public static string ew_GetImageUrl(cField fld, string val, bool resize = false, bool encrypt = EW_ENCRYPT_FILE_PATH, bool urlencode = true) {
			if (!ew_EmptyStr(val)) {
				string fn;
				if (encrypt) {
					string key = EW_RANDOM_KEY + ew_Session.SessionID;
					fn = "ewfile?t=" + ew_Encrypt(fld.TblName, key) + "&fn=" + ew_Encrypt(val, key);
					if (resize)
						fn += "&width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
				} else if (resize) {
					fn = "ewfile?t=" + ew_UrlEncode(fld.TblName) + "&fn=" + ew_UrlEncodeFilePath(val) +
				"&width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
				} else {
					fn = val;
					if (urlencode)
						fn = ew_UrlEncodeFilePath(fn);
				}
				return fn;
			} else {
				return "";
			}
		}

		// Check if image file
		public static bool ew_IsImageFile(string fn) {
			if (ew_NotEmpty(fn)) {
				fn = ew_ImageNameFromUrl(fn);
				var ext = Path.GetExtension(fn).Replace(".", "");
				return EW_IMAGE_ALLOWED_FILE_EXT.Contains(ext, StringComparer.InvariantCultureIgnoreCase);
			} else {
				return false;
			}
		}

		// Get image file name from URL
		public static string ew_ImageNameFromUrl(string fn) {
			if (ew_NotEmpty(fn)) {
				if (fn.Contains("?")) { // Thumbnail URL
					fn = fn.Split('?')[1];
					var ar = fn.Split('&');
					foreach (var p in ar) {
						if (p.StartsWith("fn="))
							return ew_UrlDecode(p.Substring(3));
					}
				}
			}
			return fn;
		}

		//
		// Class for export to email
		//

		public class cExportEmail: cExportBase {

			// Constructor
			public cExportEmail(cTable tbl = null, string style = ""): base(tbl, style) {}

			// Table header
			public override void ExportTableHeader() {
				Text.Append("<table style=\"border-collapse: collapse;\">"); // Use inline style for Gmail
			}

			// Table border styles
			private string _cellStyles = "border: 1px solid #dddddd; padding: 5px;";

			// Cell styles
			public override string CellStyles(cField fld, bool usestyle = true) {
				fld.CellAttrs.Prepend("style", _cellStyles); // Use inline style for Gmail
				return (usestyle && EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "";
			}

			// Export a field
			public override void ExportField(cField fld) {
				FldCnt++;
				string ExportValue = fld.ExportValue;
				if (fld.FldViewTag == "IMAGE") {
					if (fld.ImageResize) {
						ExportValue = ew_GetFileImgTag(fld, fld.GetTempImage());
					} else if (ew_NotEmpty(fld.HrefValue2) && fld.Upload != null) {
						if (ew_NotEmpty(fld.Upload.DbValue))
							ExportValue = ew_GetFileATag(fld, fld.HrefValue2);
					}
				} else if (ew_IsDictionary(fld.HrefValue2)) { // Export Custom View Tag
					var ar = (Dictionary<string, string>)fld.HrefValue2;
					string fn = (ar != null && ar.ContainsKey("exportfn")) ? ar["exportfn"] : ""; // Get export function name
					ExportValue = Convert.ToString(ew_Invoke(fn, new object[] { ar, "email" })); // DN
				}
				if (Horizontal) {
					ExportValueEx(fld, ExportValue);
				} else { // Vertical, export as a row
					RowCnt++;
					Text.Append("<tr class=\"" + ((FldCnt % 2 == 1) ? "ewExportTableRow" : "ewExportTableAltRow") + "\">" +
						"<td style=\"" + _cellStyles + "\">" + fld.ExportCaption + "</td>");
					Text.Append("<td" + CellStyles(fld) + ">" + ExportValue + "</td></tr>");
				}
			}

			// Export
			public override void Export() {
				if (!EW_DEBUG_ENABLED)
					ew_Response.Clear();
				ew_ResponseWrite(Text.ToString());
			}
		}

		//
		// Class for export to HTML
		//

		public class cExportHtml: cExportBase {

			// Constructor
			public cExportHtml(cTable tbl = null, string style = ""): base(tbl, style) {}

			// Export
			public override void Export() {
				if (!EW_DEBUG_ENABLED)
					ew_Response.Clear();
				ew_Response.ContentType = "text/html";
				ew_ResponseWrite(Text.ToString());
			}
		}

		//
		// Class for export to Word
		//

		public class cExportWord: cExportBase {

			// Constructor
			public cExportWord(cTable tbl = null, string style = ""): base(tbl, style) {}

			// Export
			public override void Export() {
				if (!EW_DEBUG_ENABLED)
					ew_Response.Clear();
				ew_Response.ContentType = "application/vnd.ms-word" + ((ew_NotEmpty(EW_CHARSET)) ? ";charset=" + EW_CHARSET : "");
				ew_AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + gsExportFile + ".doc");
				if (ew_SameText(EW_CHARSET, "utf-8"))
					ew_BinaryWrite(new byte[] {0xEF, 0xBB, 0xBF});
				ew_ResponseWrite(Text.ToString());
			}
		}

		//
		// Class for export to Excel
		//

		public class cExportExcel: cExportBase {

			// Constructor
			public cExportExcel(cTable tbl = null, string style = ""): base(tbl, style) {}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValueEx(cField fld, object val, bool usestyle = true) {
				if ((fld.FldDataType == EW_DATATYPE_STRING || fld.FldDataType == EW_DATATYPE_MEMO) && ew_IsNumeric(val))
					val = "=\"" + Convert.ToString(val) + "\"";
				base.ExportValueEx(fld, val, usestyle);
			}

			// Export
			public override void Export() {
				if (!EW_DEBUG_ENABLED)
					ew_Response.Clear();
				ew_Response.ContentType = "application/vnd.ms-excel" + ((ew_NotEmpty(EW_CHARSET)) ? ";charset=" + EW_CHARSET : "");
				ew_AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + gsExportFile + ".xls");
				if (ew_SameStr(EW_CHARSET, "utf-8"))
					ew_BinaryWrite(new byte[] {0xEF, 0xBB, 0xBF});
				ew_ResponseWrite(Text.ToString());
			}
		}

		//
		// Class for export to CSV
		//

		public class cExportCsv: cExportBase {
			public string QuoteChar = "\"";

			// Constructor
			public cExportCsv(cTable tbl = null, string style = ""): base(tbl, style) {}

			// Style
			public void ChangeStyle(string style) {
				Horizontal = true;
			}

			// Table header
			public override void ExportTableHeader() {

				// Skip
			}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValueEx(cField fld, object val, bool usestyle = true) {
				if (fld.FldDataType != EW_DATATYPE_BLOB) {
					if (ew_NotEmpty(Line))
						Line += ",";
					Line += QuoteChar + Convert.ToString(val).Replace(QuoteChar, QuoteChar + QuoteChar) + QuoteChar;
				}
			}

			// Begin a row
			public override void BeginExportRow(int rowcnt = 0, bool usestyle = true) {
				Line = "";
			}

			// End a row
			public override void EndExportRow() {
				Text.AppendLine(Line);
			}

			// Empty line
			public void ExportEmptyLine() {

				// Skip
			}

			// Export a field
			public override void ExportField(cField fld) {
				if (fld.UploadMultiple)
					ExportValueEx(fld, fld.Upload.DbValue);
				else
					ExportValue(fld);
			}

			// Table Footer
			public override void ExportTableFooter() {

				// Skip
			}

			// Add HTML tags
			public override void ExportHeaderAndFooter() {

				// Skip
			}

			// Export
			public override void Export() {
				if (!EW_DEBUG_ENABLED)
					ew_Response.Clear();
				ew_Response.ContentType = "text/csv";
				ew_AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + gsExportFile + ".csv");
				if (ew_SameText(EW_CHARSET, "utf-8"))
					ew_BinaryWrite(new byte[] {0xEF, 0xBB, 0xBF});
				ew_ResponseWrite(Text.ToString());
			}
		}

		//
		// Class for export to XML
		//

		public class cExportXml: cExportBase {
			public cXMLDocument XmlDoc = new cXMLDocument();
			public bool HasParent;

			// Constructor
			public cExportXml(cTable tbl = null, string style = ""): base(tbl, style) {}

			// Style
			public override void SetStyle(string style) {}

			// Field caption
			public override void ExportCaption(cField fld) {}

			// Field value
			public override void ExportValue(cField fld) {}

			// Field aggregate
			public override void ExportAggregate(cField fld, string type) {}

			// Get meta tag for charset
			public override string CharsetMetaTag() {
				return "";
			}

			// Table header
			public override void ExportTableHeader() {
				HasParent = XmlDoc.DocumentElement != null;
				if (!HasParent)
					XmlDoc.AddRoot(Table.TableVar);
			}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValueEx(cField fld, object val, bool usestyle = true) {}

			// Begin a row
			public override void BeginExportRow(int rowcnt = 0, bool usestyle = true) {
				if (rowcnt <= 0)
					return;
				if (HasParent)
					XmlDoc.AddRow(Table.TableVar);
				else
					XmlDoc.AddRow();
			}

			// End a row
			public override void EndExportRow() {}

			// Empty row
			public override void ExportEmptyRow() {}

			// Page break
			public override void ExportPageBreak() {}

			// Export a field
			public override void ExportField(cField fld) {
				if (fld.FldDataType != EW_DATATYPE_BLOB) {
					object ExportValue;
					if (fld.UploadMultiple)
						ExportValue = fld.Upload.DbValue;
					else
						ExportValue = fld.ExportValue;
					if (Convert.IsDBNull(ExportValue))
						ExportValue = "<Null>";
					XmlDoc.AddField(fld.FldVar.Substring(2), ExportValue);
				}
			}

			// Table Footer
			public override void ExportTableFooter() {}

			// Add HTML tags
			public override void ExportHeaderAndFooter() {}

			// Export
			public override void Export() {
				if (!EW_DEBUG_ENABLED)
					ew_Response.Clear();
				ew_Response.ContentType = "text/xml";
				ew_ResponseWrite(XmlDoc.XML());
			}
		}
		#pragma warning disable 612

		// Image processor
		class cPdfImageProcessor : IImageProcessor {
			public bool Process(iTextSharp.text.Image img, IDictionary<String, String> attrs, ChainedProperties chain, IDocListener doc) {
				img.ScaleToFitLineWhenOverflow = true;
				return false; // Continue to add the image
			}
		}

		//
		// Class HTML to PDF Builder
		//

		public class cHtmlToPdfBuilder {

			// Properties
			private iTextSharp.text.Rectangle _Size;
			private StyleSheet _Styles;
			private List<float[]> _ColWidths = new List<float[]>();
			public cCssParser CssParser;
			private List<string> Pages;

			// Constructor
			public cHtmlToPdfBuilder() {
				Pages = new List<string>();
				CssParser = new cCssParser();
				_Styles = new StyleSheet();
			}

			// Set paper size and orientation
			public void SetPaper(string size, string orientation) {
				string rect = size.ToUpper();

				//if (ew_NotEmpty(orientation))
					//rect += "_" + orientation.ToUpper(); // iTextSharp bug? Rectangle *_LANDSCAPE not work

				try {
					_Size = PageSize.GetRectangle(rect);
					if (ew_SameText(orientation, "landscape")) // Landscape
						_Size = _Size.Rotate();
				} catch {
					_Size = PageSize.GetRectangle(size.ToUpper());
				}
			}

			// Set column widths
			public void SetColumnWidths(float[] widths) {
				_ColWidths.Add(widths);
			}

			// Load HTML
			public void LoadHtml(string html) {

				// Get the style block(s)
				string pattern = @"<style type=""text/css"">([\S\s]*)</style>"; // Note: MUST match ExportHeaderAndFooter()
				foreach (Match match in Regex.Matches(html, pattern, RegexOptions.IgnoreCase)) {
					string styleblock = match.Groups[1].Value;
					AddStyles(styleblock);
				}

				// Remove the style block(s)
				html = Regex.Replace(html, pattern, "");
				string[] StringSeparators = new string[] {"<p style=\"page-break-after:always;\" />\r\n"}; // Note: MUST match ExportPageBreak()
				Pages.AddRange(html.Split(StringSeparators, StringSplitOptions.None));
			}

			// Add styles by CSS string
			public void AddStyles(string content) {
				CssParser.Clear();
				CssParser.ParseStr(content);
				_AddStyles();
			}

			// Import a stylesheet into the document
			public void ImportStylesheet(string file) {
				CssParser.Clear();
				CssParser.ParseFile(file);
				_AddStyles();
			}

			// Add styles to _Styles
			private void _AddStyles() {
				Dictionary<string, Dictionary<string, string>> css = CssParser.css;
				foreach (KeyValuePair<string, Dictionary<string, string>> kvp in css) {
					string key = kvp.Key;
					if (key.StartsWith(".") && !key.Contains(" ")) { // Cass name
						_Styles.LoadStyle(key.Substring(1), kvp.Value);
					} else if (!key.Contains(".") && !key.Contains(" ")) { // Tag
						_Styles.LoadTagStyle(key, kvp.Value);
					} else {

						// Not supported by iTextSharp yet
					}
				}
			}

			// Renders the PDF to an array of bytes
			public byte[] RenderPdf() {

				// Document is built-in class available in iTextSharp
				MemoryStream file = new MemoryStream();
				Document document = new Document(_Size);
				PdfWriter writer = PdfWriter.GetInstance(document, file);

				// Open
				document.Open();

				// Render each page that has been added
				foreach (string page in Pages) {
					document.NewPage();

					// Generate this page of text
					MemoryStream output = new MemoryStream();
					StreamWriter html = new StreamWriter(output, Encoding.UTF8);

					// Get the page output
					html.Write(page);
					html.Close();
					html.Dispose();

					// Read the created stream
					MemoryStream generate = new MemoryStream(output.ToArray());
					StreamReader reader = new StreamReader(generate);
					Dictionary<string, object> providers = new Dictionary<string, object>();
					providers.Add(HTMLWorker.IMG_PROCESSOR, new cPdfImageProcessor());
					int i = -1;
					foreach (IElement el in HTMLWorker.ParseToList(reader, _Styles, providers)) {
						if (el.ToString() == "iTextSharp.text.pdf.PdfPTable") {
							i++;
							PdfPTable tbl = (PdfPTable)el;
							if (i < _ColWidths.Count && _ColWidths[i].Length == tbl.NumberOfColumns)
								tbl.SetWidths(_ColWidths[i]);
						}
						document.Add(el);
					}

					// Cleanup these streams
					html.Dispose();
					reader.Dispose();
					output.Dispose();
					generate.Dispose();
				}

				// Return the rendered PDF
				document.Close();
				return file.ToArray();
			}
		}
		#pragma warning restore 612

		//
		// Class for export to PDF
		//

		public class cExportPdf: cExportBase {
			public cHtmlToPdfBuilder pdfbuilder = new cHtmlToPdfBuilder();

			// Constructor
			public cExportPdf(cTable tbl, string style): base(tbl, style) {
				pdfbuilder.SetPaper(tbl.ExportPageSize, tbl.ExportPageOrientation);
				if (tbl.ExportColumnWidths.Length > 0)
					pdfbuilder.SetColumnWidths(tbl.ExportColumnWidths);
			}

			// Table header
			public override void ExportTableHeader() {
				Text.AppendLine("<table cellspacing=\"0\" class=\"ewTablePdf\">");
			}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValueEx(cField fld, object val, bool usestyle = true) {
				Text.AppendLine("<td" + ((usestyle && EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "") + ">" + Convert.ToString(val) + "</td>");
			}

			// Begin a row
			public override void BeginExportRow(int rowcnt = 0, bool usestyle = true) {
				FldCnt = 0;
				if (Horizontal) {
					if (rowcnt == -1) {
						Table.CssClass = "ewTablePdfFooter";
					} else if (rowcnt == 0) {
						Table.CssClass = "ewTablePdfHeader";
					} else {
						Table.CssClass = ((rowcnt % 2) == 1) ? "ewTableRow" : "ewTableAltRow";
					}
					Text.Append("<tr" + ((usestyle && EW_EXPORT_CSS_STYLES) ? Table.RowStyles : "") + ">");
				}
			}

			// End a row
			public override void EndExportRow() {
				if (Horizontal) {
					Text.Append("</tr>");
				}
			}

			// Page break
			public override void ExportPageBreak() {
				if (Horizontal) {
					Text.AppendLine("</table>"); // End current table
					Text.AppendLine("<p style=\"page-break-after:always;\" />"); // Page break
					Text.AppendLine("<table class=\"ewTablePdf\">"); // New page header
				}
			}

			// Export a field
			public override void ExportField(cField fld) {
				string value = fld.ExportValue;
				if (fld.FldViewTag == "IMAGE") {
					value = ew_GetFileImgTag(fld, fld.GetTempImage());
				} else if (ew_IsDictionary(fld.HrefValue2)) { // Export custom view tag
					var ar = (Dictionary<string, object>)fld.HrefValue2;
					var fn = (ar != null && ar.ContainsKey("exportfn")) ? Convert.ToString(ar["exportfn"]) : ""; // Get export function name
					value = Convert.ToString(ew_Invoke(fn, new object[] { ar, "pdf" })); // DN
				} else {
					value = value.Replace("<br>", "\r\n");
					value = ew_RemoveHtml(value);
					value = value.Replace("\r\n", "<br>");
				}
				if (Horizontal) {
					ExportValueEx(fld, value);
				} else { // Vertical, export as a row
					FldCnt++;
					fld.CellCssClass = (FldCnt % 2 == 1) ? "ewTableRow" : "ewTableAltRow";
					Text.Append("<tr><td" + ((EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "") + ">" + fld.ExportCaption + "</td>");
					Text.Append("<td" + ((EW_EXPORT_CSS_STYLES) ? fld.CellStyles : "") + ">" + value + "</td></tr>");
				}
			}

			// Add HTML tags
			public override void ExportHeaderAndFooter() {
				string header = "<html><head>\r\n";
				header += CharsetMetaTag();
				if (EW_EXPORT_CSS_STYLES && ew_NotEmpty(EW_PDF_STYLESHEET_FILENAME))
					header += "<style type=\"text/css\">" + ew_LoadTxt(EW_PDF_STYLESHEET_FILENAME) + "</style>\r\n";
				header += "</" + "head>\r\n<body>\r\n";
				Text.Insert(0, header);
				Text.Append("</body></html>");
			}

			// Export
			public override void Export() {
				try {
					pdfbuilder.LoadHtml(Text.ToString());
					byte[] pdffile = pdfbuilder.RenderPdf();
					ew_Response.Clear();
					ew_Response.ContentType = "application/pdf";
					ew_AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + gsExportFile + ".pdf");
					ew_BinaryWrite(pdffile);
				} finally {
					ew_DeleteTmpImages();
				}
			}
		}
	}
}
