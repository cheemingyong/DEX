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

		// CustomerCurrencyRule_delete
		public static cCustomerCurrencyRule_delete CustomerCurrencyRule_delete {
			get { return (cCustomerCurrencyRule_delete)ew_ViewData["CustomerCurrencyRule_delete"]; }
			set { ew_ViewData["CustomerCurrencyRule_delete"] = value; }
		}

		//
		// Page class for CustomerCurrencyRule
		//

		public class cCustomerCurrencyRule_delete : cCustomerCurrencyRule_delete_base
		{

			// Construtor
			public cCustomerCurrencyRule_delete(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cCustomerCurrencyRule_delete_base : cCustomerCurrencyRule, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "delete";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "CustomerCurrencyRule";

			// Page object name
			public string PageObjName = "CustomerCurrencyRule_delete";

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

			public cCustomerCurrencyRule_delete_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (CustomerCurrencyRule)
				if (CustomerCurrencyRule == null || CustomerCurrencyRule is cCustomerCurrencyRule)
					CustomerCurrencyRule = this;

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
				id.SetVisibility();
				id.Visible = !IsAdd && !IsCopy && !IsGridAdd;
				RuleType.SetVisibility();
				CurrencyCode.SetVisibility();
				TransactionType.SetVisibility();
				TransactionTypeCondition.SetVisibility();
				SingleTransactionBuyAmount.SetVisibility();
				TransactionPeriodType.SetVisibility();
				PeriodStart.SetVisibility();
				PeriodEnd.SetVisibility();
				TransactionPeriodCondition.SetVisibility();
				PeriodBuyAmount.SetVisibility();
				NoOfTransactions.SetVisibility();
				FeeCost.SetVisibility();
				CreatedDate.SetVisibility();
				CreatedBy.SetVisibility();

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
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { CustomerCurrencyRule, "" }); // DN
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
					return Page_Terminate("CustomerCurrencyRulelist"); // Prevent SQL injection, return to List page

				// Set up filter (SQL WHERE clause)
				// SQL constructor in CustomerCurrencyRule class, CustomerCurrencyRuleinfo.cs

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
							return Page_Terminate("CustomerCurrencyRulelist"); // Return to list
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
				id.DbValue = row["id"];
				RuleType.DbValue = row["RuleType"];
				CurrencyCode.DbValue = row["CurrencyCode"];
				TransactionType.DbValue = row["TransactionType"];
				TransactionTypeCondition.DbValue = row["TransactionTypeCondition"];
				SingleTransactionBuyAmount.DbValue = row["SingleTransactionBuyAmount"];
				TransactionPeriodType.DbValue = row["TransactionPeriodType"];
				PeriodStart.DbValue = row["PeriodStart"];
				PeriodEnd.DbValue = row["PeriodEnd"];
				TransactionPeriodCondition.DbValue = row["TransactionPeriodCondition"];
				PeriodBuyAmount.DbValue = row["PeriodBuyAmount"];
				NoOfTransactions.DbValue = row["NoOfTransactions"];
				FeeCost.DbValue = row["FeeCost"];
				CreatedDate.DbValue = row["CreatedDate"];
				CreatedBy.DbValue = row["CreatedBy"];
			}
			#pragma warning restore 162, 168

			// Load DbValue from recordset
			public void LoadDbValues(OrderedDictionary row) {
				if (row == null)
					return;
				id.SetDbValue(row["id"]);
				RuleType.SetDbValue(row["RuleType"]);
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				TransactionType.SetDbValue(row["TransactionType"]);
				TransactionTypeCondition.SetDbValue(row["TransactionTypeCondition"]);
				SingleTransactionBuyAmount.SetDbValue(row["SingleTransactionBuyAmount"]);
				TransactionPeriodType.SetDbValue(row["TransactionPeriodType"]);
				PeriodStart.SetDbValue(row["PeriodStart"]);
				PeriodEnd.SetDbValue(row["PeriodEnd"]);
				TransactionPeriodCondition.SetDbValue(row["TransactionPeriodCondition"]);
				PeriodBuyAmount.SetDbValue(row["PeriodBuyAmount"]);
				NoOfTransactions.SetDbValue(row["NoOfTransactions"]);
				FeeCost.SetDbValue(row["FeeCost"]);
				CreatedDate.SetDbValue(row["CreatedDate"]);
				CreatedBy.SetDbValue(row["CreatedBy"]);
			}

			// Render row values based on field settings
			public void RenderRow() {

				// Convert decimal values if posted back
				if (ew_SameStr(SingleTransactionBuyAmount.FormValue, SingleTransactionBuyAmount.CurrentValue) && ew_IsNumeric(ew_StrToFloat(SingleTransactionBuyAmount.CurrentValue)))
					SingleTransactionBuyAmount.CurrentValue = ew_StrToFloat(SingleTransactionBuyAmount.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(PeriodBuyAmount.FormValue, PeriodBuyAmount.CurrentValue) && ew_IsNumeric(ew_StrToFloat(PeriodBuyAmount.CurrentValue)))
					PeriodBuyAmount.CurrentValue = ew_StrToFloat(PeriodBuyAmount.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(FeeCost.FormValue, FeeCost.CurrentValue) && ew_IsNumeric(ew_StrToFloat(FeeCost.CurrentValue)))
					FeeCost.CurrentValue = ew_StrToFloat(FeeCost.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// id
				// RuleType
				// CurrencyCode
				// TransactionType
				// TransactionTypeCondition
				// SingleTransactionBuyAmount
				// TransactionPeriodType
				// PeriodStart
				// PeriodEnd
				// TransactionPeriodCondition
				// PeriodBuyAmount
				// NoOfTransactions
				// FeeCost
				// CreatedDate
				// CreatedBy

				if (RowType == EW_ROWTYPE_VIEW) { // View row

					// id
					id.ViewValue = id.CurrentValue;

					// RuleType
					if (Convert.ToString(RuleType.CurrentValue) != "") {
							RuleType.ViewValue = RuleType.OptionCaption(Convert.ToString(RuleType.CurrentValue));
					} else {
						RuleType.ViewValue = System.DBNull.Value;
					}

					// CurrencyCode
					if (ew_NotEmpty(CurrencyCode.CurrentValue)) {
						arwrk = Convert.ToString(CurrencyCode.CurrentValue).Split(',');
						sFilterWrk = "";
						foreach (string val in arwrk) {
							if (sFilterWrk != "") sFilterWrk += " OR ";
							sFilterWrk += "[CurrencyCode]" + ew_SearchString("=", val.Trim(), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT DISTINCT [CurrencyCode], [CurrencyCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Currency]";
						sWhereWrk = "";
						CurrencyCode.LookupFilters = new Dictionary<string, string>() {{"dx1", "[CurrencyCode]"}};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(CurrencyCode, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrencyCode] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							CurrencyCode.ViewValue = "";
							for (var ari = 0; ari < rswrk.Count; ari++) {
								odwrk = rswrk[ari];
								odwrk[1] = Convert.ToString(odwrk[1]);
								CurrencyCode.ViewValue = String.Concat(CurrencyCode.ViewValue, CurrencyCode.DisplayValue(odwrk));
								if (ari < rswrk.Count - 1)
									CurrencyCode.ViewValue = String.Concat(CurrencyCode.ViewValue, ew_ViewOptionSeparator(ari)); // Separate Options
							}
						} else {
							CurrencyCode.ViewValue = CurrencyCode.CurrentValue;
						}
					} else {
						CurrencyCode.ViewValue = System.DBNull.Value;
					}

					// TransactionType
					if (Convert.ToString(TransactionType.CurrentValue) != "") {
							TransactionType.ViewValue = TransactionType.OptionCaption(Convert.ToString(TransactionType.CurrentValue));
					} else {
						TransactionType.ViewValue = System.DBNull.Value;
					}

					// TransactionTypeCondition
					if (Convert.ToString(TransactionTypeCondition.CurrentValue) != "") {
							TransactionTypeCondition.ViewValue = TransactionTypeCondition.OptionCaption(Convert.ToString(TransactionTypeCondition.CurrentValue));
					} else {
						TransactionTypeCondition.ViewValue = System.DBNull.Value;
					}

					// SingleTransactionBuyAmount
					SingleTransactionBuyAmount.ViewValue = SingleTransactionBuyAmount.CurrentValue;

					// TransactionPeriodType
					if (Convert.ToString(TransactionPeriodType.CurrentValue) != "") {
							TransactionPeriodType.ViewValue = TransactionPeriodType.OptionCaption(Convert.ToString(TransactionPeriodType.CurrentValue));
					} else {
						TransactionPeriodType.ViewValue = System.DBNull.Value;
					}

					// PeriodStart
					PeriodStart.ViewValue = PeriodStart.CurrentValue;
					PeriodStart.ViewValue = ew_FormatDateTime(PeriodStart.ViewValue, 0);

					// PeriodEnd
					PeriodEnd.ViewValue = PeriodEnd.CurrentValue;
					PeriodEnd.ViewValue = ew_FormatDateTime(PeriodEnd.ViewValue, 0);

					// TransactionPeriodCondition
					if (Convert.ToString(TransactionPeriodCondition.CurrentValue) != "") {
							TransactionPeriodCondition.ViewValue = TransactionPeriodCondition.OptionCaption(Convert.ToString(TransactionPeriodCondition.CurrentValue));
					} else {
						TransactionPeriodCondition.ViewValue = System.DBNull.Value;
					}

					// PeriodBuyAmount
					PeriodBuyAmount.ViewValue = PeriodBuyAmount.CurrentValue;

					// NoOfTransactions
					NoOfTransactions.ViewValue = NoOfTransactions.CurrentValue;

					// FeeCost
					FeeCost.ViewValue = FeeCost.CurrentValue;

					// CreatedDate
					CreatedDate.ViewValue = CreatedDate.CurrentValue;
					CreatedDate.ViewValue = ew_FormatDateTime(CreatedDate.ViewValue, 0);

					// CreatedBy
					CreatedBy.ViewValue = CreatedBy.CurrentValue;

					// id
					id.LinkCustomAttributes = id.FldTagACustomAttributes; // DN
					id.HrefValue = "";
					id.TooltipValue = "";

					// RuleType
					RuleType.LinkCustomAttributes = RuleType.FldTagACustomAttributes; // DN
					RuleType.HrefValue = "";
					RuleType.TooltipValue = "";

					// CurrencyCode
					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// TransactionType
					TransactionType.LinkCustomAttributes = TransactionType.FldTagACustomAttributes; // DN
					TransactionType.HrefValue = "";
					TransactionType.TooltipValue = "";

					// TransactionTypeCondition
					TransactionTypeCondition.LinkCustomAttributes = TransactionTypeCondition.FldTagACustomAttributes; // DN
					TransactionTypeCondition.HrefValue = "";
					TransactionTypeCondition.TooltipValue = "";

					// SingleTransactionBuyAmount
					SingleTransactionBuyAmount.LinkCustomAttributes = SingleTransactionBuyAmount.FldTagACustomAttributes; // DN
					SingleTransactionBuyAmount.HrefValue = "";
					SingleTransactionBuyAmount.TooltipValue = "";

					// TransactionPeriodType
					TransactionPeriodType.LinkCustomAttributes = TransactionPeriodType.FldTagACustomAttributes; // DN
					TransactionPeriodType.HrefValue = "";
					TransactionPeriodType.TooltipValue = "";

					// PeriodStart
					PeriodStart.LinkCustomAttributes = PeriodStart.FldTagACustomAttributes; // DN
					PeriodStart.HrefValue = "";
					PeriodStart.TooltipValue = "";

					// PeriodEnd
					PeriodEnd.LinkCustomAttributes = PeriodEnd.FldTagACustomAttributes; // DN
					PeriodEnd.HrefValue = "";
					PeriodEnd.TooltipValue = "";

					// TransactionPeriodCondition
					TransactionPeriodCondition.LinkCustomAttributes = TransactionPeriodCondition.FldTagACustomAttributes; // DN
					TransactionPeriodCondition.HrefValue = "";
					TransactionPeriodCondition.TooltipValue = "";

					// PeriodBuyAmount
					PeriodBuyAmount.LinkCustomAttributes = PeriodBuyAmount.FldTagACustomAttributes; // DN
					PeriodBuyAmount.HrefValue = "";
					PeriodBuyAmount.TooltipValue = "";

					// NoOfTransactions
					NoOfTransactions.LinkCustomAttributes = NoOfTransactions.FldTagACustomAttributes; // DN
					NoOfTransactions.HrefValue = "";
					NoOfTransactions.TooltipValue = "";

					// FeeCost
					FeeCost.LinkCustomAttributes = FeeCost.FldTagACustomAttributes; // DN
					FeeCost.HrefValue = "";
					FeeCost.TooltipValue = "";

					// CreatedDate
					CreatedDate.LinkCustomAttributes = CreatedDate.FldTagACustomAttributes; // DN
					CreatedDate.HrefValue = "";
					CreatedDate.TooltipValue = "";

					// CreatedBy
					CreatedBy.LinkCustomAttributes = CreatedBy.FldTagACustomAttributes; // DN
					CreatedBy.HrefValue = "";
					CreatedBy.TooltipValue = "";
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
				CustomerCurrencyRule = CustomerCurrencyRule ?? new cCustomerCurrencyRule();
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
							sThisKey += Convert.ToString(row["id"]);
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
				Breadcrumb.Add("list", TableVar, ew_AppPath(AddMasterUrl("CustomerCurrencyRulelist")), "", TableVar, true);
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
