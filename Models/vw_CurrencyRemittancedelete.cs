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

		// vw_CurrencyRemittance_delete
		public static cvw_CurrencyRemittance_delete vw_CurrencyRemittance_delete {
			get { return (cvw_CurrencyRemittance_delete)ew_ViewData["vw_CurrencyRemittance_delete"]; }
			set { ew_ViewData["vw_CurrencyRemittance_delete"] = value; }
		}

		//
		// Page class for vw_CurrencyRemittance
		//

		public class cvw_CurrencyRemittance_delete : cvw_CurrencyRemittance_delete_base
		{

			// Construtor
			public cvw_CurrencyRemittance_delete(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cvw_CurrencyRemittance_delete_base : cvw_CurrencyRemittance, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "delete";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "vw_CurrencyRemittance";

			// Page object name
			public string PageObjName = "vw_CurrencyRemittance_delete";

			// Page terminated // DN
			private bool _terminated = false;

			// Page name
			public string PageName {
				get {
					return ew_CurrentPage();
				}
			}

			// Page URL
			public string PageUrl {
				get {
					string url = ew_CurrentPage() + "?";
					return url;
				}
			}

			// Message
			public string Message {
				get {
					return Convert.ToString(ew_Session[EW_SESSION_MESSAGE]);
				}
				set {
					ew_Session[EW_SESSION_MESSAGE] = ew_AddMessage(Convert.ToString(ew_Session[EW_SESSION_MESSAGE]), value);
				}
			}

			// Failure Message
			public string FailureMessage {
				get {
					return Convert.ToString(ew_Session[EW_SESSION_FAILURE_MESSAGE]);
				}
				set {
					ew_Session[EW_SESSION_FAILURE_MESSAGE] = ew_AddMessage(Convert.ToString(ew_Session[EW_SESSION_FAILURE_MESSAGE]), value);
				}
			}

			// Success Message
			public string SuccessMessage {
				get {
					return Convert.ToString(ew_Session[EW_SESSION_SUCCESS_MESSAGE]);
				}
				set {
					ew_Session[EW_SESSION_SUCCESS_MESSAGE] = ew_AddMessage(Convert.ToString(ew_Session[EW_SESSION_SUCCESS_MESSAGE]), value);
				}
			}

			// Warning Message
			public string WarningMessage {
				get {
					return Convert.ToString(ew_Session[EW_SESSION_WARNING_MESSAGE]);
				}
				set {
					ew_Session[EW_SESSION_WARNING_MESSAGE] = ew_AddMessage(Convert.ToString(ew_Session[EW_SESSION_WARNING_MESSAGE]), value);
				}
			}

			// Methods to clear message
			public void ClearMessage() {
				ew_Session[EW_SESSION_MESSAGE] = "";
			}
			public void ClearFailureMessage() {
				ew_Session[EW_SESSION_FAILURE_MESSAGE] = "";
			}
			public void ClearSuccessMessage() {
				ew_Session[EW_SESSION_SUCCESS_MESSAGE] = "";
			}
			public void ClearWarningMessage() {
				ew_Session[EW_SESSION_WARNING_MESSAGE] = "";
			}
			public void ClearMessages() {
				ew_Session[EW_SESSION_MESSAGE] = "";
				ew_Session[EW_SESSION_FAILURE_MESSAGE] = "";
				ew_Session[EW_SESSION_SUCCESS_MESSAGE] = "";
				ew_Session[EW_SESSION_WARNING_MESSAGE] = "";
			}

			// Show message
			public string ShowMessage(bool write = true) { // DN
				bool hidden = false;
				string html = "";

				// Message
				string sMessage = Message;
				Message_Showing(ref sMessage, "");
				if (sMessage != "") { // Message in Session, display
					if (!hidden)
						sMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + sMessage;
					html += "<div class=\"alert alert-info ewInfo\">" + sMessage + "</div>";
					ew_Session[EW_SESSION_MESSAGE] = ""; // Clear message in Session
				}

				// Warning message
				var sWarningMessage = WarningMessage;
				Message_Showing(ref sWarningMessage, "warning");
				if (sWarningMessage != "") { // Message in Session, display
					if (!hidden)
						sWarningMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + sWarningMessage;
					html += "<div class=\"alert alert-warning ewWarning\">" + sWarningMessage + "</div>";
					ew_Session[EW_SESSION_WARNING_MESSAGE] = ""; // Clear message in Session
				}

				// Success message
				var sSuccessMessage = SuccessMessage;
				Message_Showing(ref sSuccessMessage, "success");
				if (sSuccessMessage != "") { // Message in Session, display
					if (!hidden)
						sSuccessMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + sSuccessMessage;
					html += "<div class=\"alert alert-success ewSuccess\">" + sSuccessMessage + "</div>";
					ew_Session[EW_SESSION_SUCCESS_MESSAGE] = ""; // Clear message in Session
				}

				// Failure message
				var sErrorMessage = FailureMessage;
				Message_Showing(ref sErrorMessage, "failure");
				if (sErrorMessage != "") { // Message in Session, display
					if (!hidden)
						sErrorMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + sErrorMessage;
					html += "<div class=\"alert alert-danger ewError\">" + sErrorMessage + "</div>";
					ew_Session[EW_SESSION_FAILURE_MESSAGE] = ""; // Clear message in Session
				}
				html = "<div class=\"ewMessageDialog\"" + ((hidden) ? " style=\"display: none;\"" : "") + ">" + html + "</div>"; // DN
				if (write) {
					ew_Write(html);
					return "";
				} else {
					return html;
				}
			}
			public string PageHeader = "";
			public string PageFooter = "";

			// Show Page Header
			public IHtmlContent ShowPageHeader() {
				string sHeader = PageHeader;
				Page_DataRendering(ref sHeader);
				if (ew_NotEmpty(sHeader)) // Header exists, display
					return new HtmlString("<p>" + sHeader + "</p>");
				return null;
			}

			// Show Page Footer
			public IHtmlContent ShowPageFooter() {
				string sFooter = PageFooter;
				Page_DataRendered(ref sFooter);
				if (ew_NotEmpty(sFooter)) // Fotoer exists, display
					return new HtmlString("<p>" + sFooter + "</p>");
				return null;
			}

			// Validate page request
			public bool IsPageRequest {
				get {
					return true;
				}
			}

			// Token
			public string Token = "";
			public int TokenTimeout = 0;
			public bool CheckToken = EW_CHECK_TOKEN;

			// Valid Post
			public bool ValidPost() {
				if (!CheckToken || !IsPost)
					return true;
				if (ew_Post(EW_TOKEN_NAME) == null)
					return false;
				return Convert.ToBoolean(ew_CheckToken(ew_Post(EW_TOKEN_NAME), TokenTimeout));
			}

			// Create Token
			public void CreateToken() {
				if (CheckToken) {
					if (ew_Empty(Token) && CheckToken) // Create token
						Token = ew_CreateToken();
					gsToken = Token; // Save to global variable
				}
			}

			//
			// Page class constructor
			//

			public cvw_CurrencyRemittance_delete_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (vw_CurrencyRemittance)
				if (vw_CurrencyRemittance == null || vw_CurrencyRemittance is cvw_CurrencyRemittance)
					vw_CurrencyRemittance = this;

				// Start time
				StartTime = Environment.TickCount;

				// Open connection
				Conn = Connection; // DN
			}

			//
			// Page_Init
			//

			public IActionResult Page_Init() {

				// Header
				ew_Header(EW_CACHE);
				CurrentAction = (ew_Get("a") != "") ? ew_Get("a") : ew_Post("a_list"); // Set up current action
				CurrencyCode.SetVisibility();
				TTSellRateType.SetVisibility();
				TTSellRate.SetVisibility();
				TTSellMinBid.SetVisibility();
				TTSellMaxBid.SetVisibility();
				TTBuyRateType.SetVisibility();
				TTBuyRate.SetVisibility();
				TTBuyMinBid.SetVisibility();
				TTBuyMaxBid.SetVisibility();

				// Global Page Loading event
				Page_Loading();

				// Page Load event
				Page_Load();

				// Check token
				if (!ValidPost()) {
					ew_End(Language.Phrase("InvalidPostRequest")); // DN
					return null;
				}

				// Create Token
				CreateToken();
				return null;
			}

			//
			// Page_Terminate
			//

			public IActionResult Page_Terminate(string url = "") { // DN
				if (_terminated) // DN
					return new EmptyResult();

				// Page Unload event
				Page_Unload();

				// Global Page Unloaded event
				Page_Unloaded();

				// Export
				if (ew_NotEmpty(CustomExport) && CustomExport == Export && EW_EXPORT.ContainsKey(CustomExport)) {
					var sContent = "";
					sContent = Regex.Match(ew_View.Output.ToString(), @"<html>[\s\S]+</html>", RegexOptions.IgnoreCase).Value;
					if (ew_Empty(gsExportFile))
						gsExportFile = TableVar;
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { vw_CurrencyRemittance, "" }); // DN
					doc.Text.Append(sContent);
					if (Export == "email") {
					} else {
						doc.Export();
					}
					ew_DeleteTmpImages(); // Delete temp images
				}
				Page_Redirecting(ref url);

				 // Close connection
				ew_CloseConn();

				// Gargage collection
				ew_GCCollect(); // DN

				// Terminate
				_terminated = true; // DN

				// Go to URL if specified
				if (ew_NotEmpty(url)) {
					if (!EW_DEBUG_ENABLED)
						ew_ResponseClear();
					if (ew_View == null) { // Not in views
						if (!ew_Response.HasStarted)
							return ew_Controller.Redirect(ew_AppUrl(url));
					} else { // Cannot redirect in views, use RedirectException
						ew_Redirect(url);
					}
				}
				return new EmptyResult();
			}
			public string DbMasterFilter = "";
			public string DbDetailFilter = "";
			public int StartRec;
			public int TotalRecs;
			public int RecCnt;
			public List<string> RecKeys;
			public DbDataReader Recordset;

			//public int RowCnt = 0; // DN
			public int StartRowCnt = 1;

			//
			// Page main
			//

			public IActionResult Page_Main() {

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Load key parameters
				RecKeys = GetRecordKeys(); // Load record keys
				string sFilter = GetKeyFilter();
				if (ew_Empty(sFilter))
					return Page_Terminate("vw_CurrencyRemittancelist"); // Prevent SQL injection, return to List page

				// Set up filter (SQL WHERE clause)
				// SQL constructor in vw_CurrencyRemittance class, vw_CurrencyRemittanceinfo.cs

				CurrentFilter = sFilter;

				// Get action
				if (ew_NotEmpty(ew_Post("a_delete"))) {
					CurrentAction = ew_Post("a_delete");
				} else if (ew_Get("a_delete") == "1") {
					CurrentAction = "D"; // Delete record directly
				} else {
					CurrentAction = "I"; // Display record
				}
				if (CurrentAction == "D") {
					SendEmail = true; // Send email on delete success
					if (DeleteRows()) { // Delete rows
						if (ew_Empty(SuccessMessage))
							SuccessMessage = Language.Phrase("DeleteSuccess"); // Set up success message
						return Page_Terminate(ReturnUrl); // Return to caller
					} else { // Delete failed
						CurrentAction = "I"; // Display record
					}
				}
				if (CurrentAction == "I") { // Load records for display // DN
						Recordset = LoadRecordset();
						TotalRecs = SelectRecordCount(); // Get record count
						if (TotalRecs <= 0) { // No record found, exit
							CloseRecordset(); // DN
							return Page_Terminate("vw_CurrencyRemittancelist"); // Return to list
						}
				}
				return ew_Controller.View();
			}

			// Load recordset // DN
			public DbDataReader LoadRecordset(int offset = -1, int rowcnt = -1) {

				// Load list page SQL
				string sSql = SelectSQL;

				// Load recordset (Recordset_Selected event not supported) // DN
				return Connection.SelectLimit(sSql, rowcnt, offset, ew_NotEmpty(OrderBy) || ew_NotEmpty(SessionOrderBy));
			}

			// Load row based on key values
			public bool LoadRow() {
				var sFilter = KeyFilter;

				// Call Row Selecting event
				Row_Selecting(ref sFilter);

				// Load SQL based on filter
				CurrentFilter = sFilter;
				var sSql = SQL;
				var res = false;
				try {
					using (var rsrow = Connection.OpenDataReader(sSql)) {
						if (rsrow != null && rsrow.Read()) {
							LoadRowValues(rsrow);
							res = true;
						} else {
							return false;
						}
					}
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
				}
				return res;
			}
			#pragma warning disable 162, 168

			// Load row values from recordset
			public void LoadRowValues(DbDataReader dr) {
				if (dr == null)
					return;
				var row = Connection.GetRow(dr); // DN

				// Call Row Selected event
				Row_Selected(ref row);
				CurrencyCode.DbValue = row["CurrencyCode"];
				TTSellRateType.DbValue = row["TTSellRateType"];
				TTSellRate.DbValue = row["TTSellRate"];
				TTSellMinBid.DbValue = row["TTSellMinBid"];
				TTSellMaxBid.DbValue = row["TTSellMaxBid"];
				TTBuyRateType.DbValue = row["TTBuyRateType"];
				TTBuyRate.DbValue = row["TTBuyRate"];
				TTBuyMinBid.DbValue = row["TTBuyMinBid"];
				TTBuyMaxBid.DbValue = row["TTBuyMaxBid"];
			}
			#pragma warning restore 162, 168

			// Load DbValue from recordset
			public void LoadDbValues(OrderedDictionary row) {
				if (row == null)
					return;
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				TTSellRateType.SetDbValue(row["TTSellRateType"]);
				TTSellRate.SetDbValue(row["TTSellRate"]);
				TTSellMinBid.SetDbValue(row["TTSellMinBid"]);
				TTSellMaxBid.SetDbValue(row["TTSellMaxBid"]);
				TTBuyRateType.SetDbValue(row["TTBuyRateType"]);
				TTBuyRate.SetDbValue(row["TTBuyRate"]);
				TTBuyMinBid.SetDbValue(row["TTBuyMinBid"]);
				TTBuyMaxBid.SetDbValue(row["TTBuyMaxBid"]);
			}

			// Render row values based on field settings
			public void RenderRow() {

				// Convert decimal values if posted back
				if (ew_SameStr(TTSellRate.FormValue, TTSellRate.CurrentValue) && ew_IsNumeric(ew_StrToFloat(TTSellRate.CurrentValue)))
					TTSellRate.CurrentValue = ew_StrToFloat(TTSellRate.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(TTSellMinBid.FormValue, TTSellMinBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(TTSellMinBid.CurrentValue)))
					TTSellMinBid.CurrentValue = ew_StrToFloat(TTSellMinBid.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(TTSellMaxBid.FormValue, TTSellMaxBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(TTSellMaxBid.CurrentValue)))
					TTSellMaxBid.CurrentValue = ew_StrToFloat(TTSellMaxBid.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(TTBuyRate.FormValue, TTBuyRate.CurrentValue) && ew_IsNumeric(ew_StrToFloat(TTBuyRate.CurrentValue)))
					TTBuyRate.CurrentValue = ew_StrToFloat(TTBuyRate.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(TTBuyMinBid.FormValue, TTBuyMinBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(TTBuyMinBid.CurrentValue)))
					TTBuyMinBid.CurrentValue = ew_StrToFloat(TTBuyMinBid.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(TTBuyMaxBid.FormValue, TTBuyMaxBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(TTBuyMaxBid.CurrentValue)))
					TTBuyMaxBid.CurrentValue = ew_StrToFloat(TTBuyMaxBid.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// CurrencyCode
				// TTSellRateType
				// TTSellRate
				// TTSellMinBid
				// TTSellMaxBid
				// TTBuyRateType
				// TTBuyRate
				// TTBuyMinBid
				// TTBuyMaxBid

				if (RowType == EW_ROWTYPE_VIEW) { // View row

					// CurrencyCode
					CurrencyCode.ViewValue = CurrencyCode.CurrentValue;

					// TTSellRateType
					if (Convert.ToString(TTSellRateType.CurrentValue) != "") {
							TTSellRateType.ViewValue = TTSellRateType.OptionCaption(Convert.ToString(TTSellRateType.CurrentValue));
					} else {
						TTSellRateType.ViewValue = System.DBNull.Value;
					}

					// TTSellRate
					TTSellRate.ViewValue = TTSellRate.CurrentValue;

					// TTSellMinBid
					TTSellMinBid.ViewValue = TTSellMinBid.CurrentValue;

					// TTSellMaxBid
					TTSellMaxBid.ViewValue = TTSellMaxBid.CurrentValue;

					// TTBuyRateType
					if (Convert.ToString(TTBuyRateType.CurrentValue) != "") {
							TTBuyRateType.ViewValue = TTBuyRateType.OptionCaption(Convert.ToString(TTBuyRateType.CurrentValue));
					} else {
						TTBuyRateType.ViewValue = System.DBNull.Value;
					}

					// TTBuyRate
					TTBuyRate.ViewValue = TTBuyRate.CurrentValue;

					// TTBuyMinBid
					TTBuyMinBid.ViewValue = TTBuyMinBid.CurrentValue;

					// TTBuyMaxBid
					TTBuyMaxBid.ViewValue = TTBuyMaxBid.CurrentValue;

					// CurrencyCode
					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// TTSellRateType
					TTSellRateType.LinkCustomAttributes = TTSellRateType.FldTagACustomAttributes; // DN
					TTSellRateType.HrefValue = "";
					TTSellRateType.TooltipValue = "";

					// TTSellRate
					TTSellRate.LinkCustomAttributes = TTSellRate.FldTagACustomAttributes; // DN
					TTSellRate.HrefValue = "";
					TTSellRate.TooltipValue = "";

					// TTSellMinBid
					TTSellMinBid.LinkCustomAttributes = TTSellMinBid.FldTagACustomAttributes; // DN
					TTSellMinBid.HrefValue = "";
					TTSellMinBid.TooltipValue = "";

					// TTSellMaxBid
					TTSellMaxBid.LinkCustomAttributes = TTSellMaxBid.FldTagACustomAttributes; // DN
					TTSellMaxBid.HrefValue = "";
					TTSellMaxBid.TooltipValue = "";

					// TTBuyRateType
					TTBuyRateType.LinkCustomAttributes = TTBuyRateType.FldTagACustomAttributes; // DN
					TTBuyRateType.HrefValue = "";
					TTBuyRateType.TooltipValue = "";

					// TTBuyRate
					TTBuyRate.LinkCustomAttributes = TTBuyRate.FldTagACustomAttributes; // DN
					TTBuyRate.HrefValue = "";
					TTBuyRate.TooltipValue = "";

					// TTBuyMinBid
					TTBuyMinBid.LinkCustomAttributes = TTBuyMinBid.FldTagACustomAttributes; // DN
					TTBuyMinBid.HrefValue = "";
					TTBuyMinBid.TooltipValue = "";

					// TTBuyMaxBid
					TTBuyMaxBid.LinkCustomAttributes = TTBuyMaxBid.FldTagACustomAttributes; // DN
					TTBuyMaxBid.HrefValue = "";
					TTBuyMaxBid.TooltipValue = "";
				}

				// Call Row Rendered event
				if (RowType != EW_ROWTYPE_AGGREGATEINIT)
					Row_Rendered();
			}

			//
			// Delete records based on current filter
			//

			public bool DeleteRows() {
				var result = true;
				List<OrderedDictionary> rsold = null;
				try {
					string sSql = SQL;
					using (var rs = Connection.GetDataReader(sSql)) {
						if (rs == null) {
							return false;
						} else if (!rs.HasRows) {
							FailureMessage = Language.Phrase("NoRecord"); // No record found
							return false;
						} else { // Clone old rows
							rsold = Connection.GetRows(rs);
						}
					}
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED)
						throw;
					FailureMessage = e.Message;
					return false;
				}
				vw_CurrencyRemittance = vw_CurrencyRemittance ?? new cvw_CurrencyRemittance();
				Connection.BeginTrans();
				var sKey = "";
				try {

					// Call row deleting event
					if (result) {
						foreach (OrderedDictionary row in rsold) {
							result = Row_Deleting(row);
							if (!result)
								break;
						}
					}
					if (result) {
						foreach (OrderedDictionary row in rsold) {
							var sThisKey = "";
							if (ew_NotEmpty(sThisKey)) sThisKey += EW_COMPOSITE_KEY_SEPARATOR;
							sThisKey += Convert.ToString(row["CurrencyCode"]);
							try {
								Delete(row);
							} catch (Exception e) {
								if (EW_DEBUG_ENABLED) throw;
								FailureMessage = e.Message; // Set up error message
								result = false;
								break;
							}
							if (ew_NotEmpty(sKey)) sKey += ", ";
							sKey += sThisKey;
						}
					} else {

						// Set up error message
						if (ew_NotEmpty(SuccessMessage) || ew_NotEmpty(FailureMessage)) {

							// Use the message, do nothing
						} else if (ew_NotEmpty(CancelMessage)) {
							FailureMessage = CancelMessage;
							CancelMessage = "";
						} else {
							FailureMessage = Language.Phrase("DeleteCancelled");
						}
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					result = false;
				}
				if (result) {
					Connection.CommitTrans(); // Commit the changes
				} else {
					Connection.RollbackTrans(); // Rollback changes
				}

				// Call Row Deleted event
				if (result) {
					foreach (OrderedDictionary row in rsold)
						Row_Deleted(row);
				}
				return result;
			}

			// Set up Breadcrumb
			public void SetupBreadcrumb() {
				Breadcrumb = new cBreadcrumb();
				var url = ew_CurrentUrl();
				Breadcrumb.Add("list", TableVar, ew_AppPath(AddMasterUrl("vw_CurrencyRemittancelist")), "", TableVar, true);
				var PageId = "delete";
				Breadcrumb.Add("delete", PageId, url);
			}
			#pragma warning disable 168, 1522

			// Setup lookup filters of a field
			public override void SetupLookupFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
				}
			}

			// Setup AutoSuggest filters of a field
			public override void SetupAutoSuggestFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
				}
			}
			#pragma warning restore 168, 1522
			public void CloseRecordset() {
				Recordset?.Close();
				Recordset?.Dispose();
			}

			// Page Load event
			public virtual void Page_Load() {

				//ew_Write("Page Load");
			}

			// Page Unload event
			public virtual void Page_Unload() {

				//ew_Write("Page Unload");
			}

			// Page Redirecting event
			public virtual void Page_Redirecting(ref string url) {

				//url = newurl;
			}

			// Message Showing event
			// type = ""|"success"|"failure"|"warning"

			public virtual void Message_Showing(ref string msg, string type) {

				// Note: Do not change msg outside the following 4 cases.
				if (type == "success") {

					//msg = "your success message";
				} else if (type == "failure") {

					//msg = "your failure message";
				} else if (type == "warning") {

					//msg = "your warning message";
				} else {

					//msg = "your message";
				}
			}

			// Page Load event
			public virtual void Page_Render() {

				//ew_Write("Page Render");
			}

			// Page Data Rendering event
			public virtual void Page_DataRendering(ref string header) {

				// Example:
				//header = "your header";

			}

			// Page Data Rendered event
			public virtual void Page_DataRendered(ref string footer) {

				// Example:
				//footer = "your footer";

			}
		}
	}
}
