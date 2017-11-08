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

		//
		// Global variables
		//
		// Conn

		public static dynamic Conn {
			get {
				return (dynamic)ew_ViewData["Conn"];
			}
			set {
				ew_ViewData["Conn"] = value;
			}
		}

		// Connections
		public static Dictionary<string, dynamic> Connections {
			get {
				ew_ViewData["Connections"] = ew_ViewData["Connections"] ?? new Dictionary<string, dynamic>();
				return (Dictionary<string, dynamic>)ew_ViewData["Connections"];
			}
			set {
				ew_ViewData["Connections"] = value;
			}
		}

		// Security
		public static cAdvancedSecurityBase Security {
			get {
				return (cAdvancedSecurityBase)ew_ViewData["Security"];
			}
			set {
				ew_ViewData["Security"] = value;
			}
		}

		// ObjForm
		public static cFormObj ObjForm {
			get {
				return (cFormObj)ew_ViewData["ObjForm"];
			}
			set {
				ew_ViewData["ObjForm"] = value;
			}
		}

		// Language
		public static cLanguage Language {
			get {
				return (cLanguage)ew_ViewData["Language"];
			}
			set {
				ew_ViewData["Language"] = value;
			}
		}

		// Breadcrumb
		public static cBreadcrumb Breadcrumb {
			get {
				return (cBreadcrumb)ew_ViewData["Breadcrumb"];
			}
			set {
				ew_ViewData["Breadcrumb"] = value;
			}
		}

		// RootMenu
		public static cMenuBase RootMenu {
			get {
				return (cMenuBase)ew_ViewData["RootMenu"];
			}
			set {
				ew_ViewData["RootMenu"] = value;
			}
		}

		// gsLanguage
		public static string gsLanguage {
			get {
				return Convert.ToString(ew_ViewData["gsLanguage"]);
			}
			set {
				ew_ViewData["gsLanguage"] = value;
			}
		}

		// gbSkipHeaderFooter
		public static bool gbSkipHeaderFooter {
			get {
				return ew_ConvertToBool(ew_ViewData["gbSkipHeaderFooter"]);
			}
			set {
				ew_ViewData["gbSkipHeaderFooter"] = value;
			}
		}

		// StartTime
		public static long StartTime {
			get {
				return (long)ew_ViewData["StartTime"];
			}
			set {
				ew_ViewData["StartTime"] = value;
			}
		}

		// CurrentPage
		public static dynamic CurrentPage {
			get {
				return (dynamic)ew_ViewData["CurrentPage"];
			}
			set {
				ew_ViewData["CurrentPage"] = value;
			}
		}

		// UserTable
		public static dynamic UserTable {
			get {
				return (dynamic)ew_ViewData["UserTable"];
			}
			set {
				ew_ViewData["UserTable"] = value;
			}
		}

		// UserTableConn
		public static cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> UserTableConn {
			get {
				return (cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>)ew_ViewData["UserTableConn"];
			}
			set {
				ew_ViewData["UserTableConn"] = value;
			}
		}

		// gsFormError
		public static string gsFormError {
			get {
				return Convert.ToString(ew_ViewData["gsFormError"]);
			}
			set {
				ew_ViewData["gsFormError"] = value;
			}
		}

		// gsSearchError
		public static string gsSearchError {
			get {
				return Convert.ToString(ew_ViewData["gsSearchError"]);
			}
			set {
				ew_ViewData["gsSearchError"] = value;
			}
		}

		// gsExport
		public static string gsExport {
			get {
				return Convert.ToString(ew_ViewData["gsExport"]);
			}
			set {
				ew_ViewData["gsExport"] = value;
			}
		}

		// gsExportFile
		public static string gsExportFile {
			get {
				return Convert.ToString(ew_ViewData["gsExportFile"]);
			}
			set {
				ew_ViewData["gsExportFile"] = value;
			}
		}

		// gsCustomExport
		public static string gsCustomExport {
			get {
				return Convert.ToString(ew_ViewData["gsCustomExport"]);
			}
			set {
				ew_ViewData["gsCustomExport"] = value;
			}
		}

		// gsEmailErrDesc
		public static string gsEmailErrDesc {
			get {
				return Convert.ToString(ew_ViewData["gsEmailErrDesc"]);
			}
			set {
				ew_ViewData["gsEmailErrDesc"] = value;
			}
		}

		// gsDebugMsg
		public static string gsDebugMsg {
			get {
				return Convert.ToString(ew_ViewData["gsDebugMsg"]);
			}
			set {
				ew_ViewData["gsDebugMsg"] = value;
			}
		}

		// gsToken
		public static string gsToken {
			get {
				return Convert.ToString(ew_ViewData["gsToken"]);
			}
			set {
				ew_ViewData["gsToken"] = value;
			}
		}

		// gsHeaderRowClass
		public static string gsHeaderRowClass {
			get {
				return Convert.ToString(ew_ViewData["gsHeaderRowClass"]);
			}
			set {
				ew_ViewData["gsHeaderRowClass"] = value;
			}
		}

		// gsMenuColumnClass
		public static string gsMenuColumnClass {
			get {
				return Convert.ToString(ew_ViewData["gsMenuColumnClass"]);
			}
			set {
				ew_ViewData["gsMenuColumnClass"] = value;
			}
		}

		// gsSiteTitleClass
		public static string gsSiteTitleClass {
			get {
				return Convert.ToString(ew_ViewData["gsSiteTitleClass"]);
			}
			set {
				ew_ViewData["gsSiteTitleClass"] = value;
			}
		}

		// gTmpImages
		public static List<string> gTmpImages {
			get {
				ew_ViewData["gTmpImages"] = ew_ViewData["gTmpImages"] ?? new List<string>();
				return (List<string>)ew_ViewData["gTmpImages"];
			}
			set {
				ew_ViewData["gTmpImages"] = value;
			}
		}

		// CurrentNumberFormatInfo
		public static NumberFormatInfo CurrentNumberFormatInfo {
			get {
				ew_ViewData["CurrentNumberFormatInfo"] = ew_ViewData["CurrentNumberFormatInfo"] ?? new NumberFormatInfo();
				return (NumberFormatInfo)ew_ViewData["CurrentNumberFormatInfo"];
			}
			set {
				ew_ViewData["CurrentNumberFormatInfo"] = value;
			}
		}

		// EW_P_CS_PRECEDES
		public static int EW_P_CS_PRECEDES {
			get {
				return (int)ew_ViewData["EW_P_CS_PRECEDES"];
			}
			set {
				ew_ViewData["EW_P_CS_PRECEDES"] = value;
			}
		}

		// EW_P_SEP_BY_SPACE
		public static int EW_P_SEP_BY_SPACE {
			get {
				return (int)ew_ViewData["EW_P_SEP_BY_SPACE"];
			}
			set {
				ew_ViewData["EW_P_SEP_BY_SPACE"] = value;
			}
		}

		// EW_N_CS_PRECEDES
		public static int EW_N_CS_PRECEDES {
			get {
				return (int)ew_ViewData["EW_N_CS_PRECEDES"];
			}
			set {
				ew_ViewData["EW_N_CS_PRECEDES"] = value;
			}
		}

		// EW_N_SEP_BY_SPACE
		public static int EW_N_SEP_BY_SPACE {
			get {
				return (int)ew_ViewData["EW_N_SEP_BY_SPACE"];
			}
			set {
				ew_ViewData["EW_N_SEP_BY_SPACE"] = value;
			}
		}

		// EW_P_SIGN_POSN
		public static int EW_P_SIGN_POSN {
			get {
				return (int)ew_ViewData["EW_P_SIGN_POSN"];
			}
			set {
				ew_ViewData["EW_P_SIGN_POSN"] = value;
			}
		}

		// EW_N_SIGN_POSN
		public static int EW_N_SIGN_POSN {
			get {
				return (int)ew_ViewData["EW_N_SIGN_POSN"];
			}
			set {
				ew_ViewData["EW_N_SIGN_POSN"] = value;
			}
		}

		// EW_DATE_SEPARATOR
		public static string EW_DATE_SEPARATOR {
			get {
				return Convert.ToString(ew_ViewData["EW_DATE_SEPARATOR"]);
			}
			set {
				ew_ViewData["EW_DATE_SEPARATOR"] = value;
			}
		}

		// EW_TIME_SEPARATOR
		public static string EW_TIME_SEPARATOR {
			get {
				return Convert.ToString(ew_ViewData["EW_TIME_SEPARATOR"]);
			}
			set {
				ew_ViewData["EW_TIME_SEPARATOR"] = value;
			}
		}

		// EW_DATE_FORMAT
		public static string EW_DATE_FORMAT {
			get {
				return Convert.ToString(ew_ViewData["EW_DATE_FORMAT"]);
			}
			set {
				ew_ViewData["EW_DATE_FORMAT"] = value;
			}
		}

		// EW_DATE_FORMAT_ID
		public static int EW_DATE_FORMAT_ID {
			get {
				return (int)ew_ViewData["EW_DATE_FORMAT_ID"];
			}
			set {
				ew_ViewData["EW_DATE_FORMAT_ID"] = value;
			}
		}

		// NumberDecimalSeparator // DN
		public static string EW_DECIMAL_POINT {
			get {
				return CurrentNumberFormatInfo.NumberDecimalSeparator;
			}
			set {
				CurrentNumberFormatInfo.NumberDecimalSeparator = value;
				CurrentNumberFormatInfo.PercentDecimalSeparator = value;
			}
		}

		// NumberGroupSeparator // DN
		public static string EW_THOUSANDS_SEP {
			get {
				return CurrentNumberFormatInfo.NumberGroupSeparator;
			}
			set {
				CurrentNumberFormatInfo.NumberGroupSeparator = value;
				CurrentNumberFormatInfo.PercentGroupSeparator = value;
			}
		}

		// CurrencySymbol // DN
		public static string EW_CURRENCY_SYMBOL {
			get {
				return CurrentNumberFormatInfo.CurrencySymbol;
			}
			set {
				CurrentNumberFormatInfo.CurrencySymbol = value;
			}
		}

		// CurrencyDecimalSeparator // DN
		public static string EW_MON_DECIMAL_POINT {
			get {
				return CurrentNumberFormatInfo.CurrencyDecimalSeparator;
			}
			set {
				CurrentNumberFormatInfo.CurrencyDecimalSeparator = value;
			}
		}

		// CurrencyDecimalSeparator // DN
		public static string EW_MON_THOUSANDS_SEP {
			get {
				return CurrentNumberFormatInfo.CurrencyGroupSeparator;
			}
			set {
				CurrentNumberFormatInfo.CurrencyGroupSeparator = value;
			}
		}

		// PositiveSign // DN
		public static string EW_POSITIVE_SIGN {
			get {
				return CurrentNumberFormatInfo.PositiveSign;
			}
			set {
				CurrentNumberFormatInfo.PositiveSign = value;
			}
		}

		// NegativeSign // DN
		public static string EW_NEGATIVE_SIGN {
			get {
				return CurrentNumberFormatInfo.NegativeSign;
			}
			set {
				CurrentNumberFormatInfo.NegativeSign = value;
			}
		}

		// NumberDecimalDigits // DN
		public static int EW_FRAC_DIGITS {
			get {
				return CurrentNumberFormatInfo.NumberDecimalDigits;
			}
			set {
				CurrentNumberFormatInfo.NumberDecimalDigits = value;
				CurrentNumberFormatInfo.CurrencyDecimalDigits = value;
				CurrentNumberFormatInfo.PercentDecimalDigits = value;
			}
		}

		// Route data values // DN
		public static IDictionary<string, object> RouteValues {
			get {
				return ew_Controller?.RouteData.Values;
			}
		}

		// Current Table // DN
		public static dynamic CurrentTable {
			get {
				return CurrentPage;
			}
		}
	}
}
