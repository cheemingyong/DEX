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

		// CurrencyRule_edit
		public static cCurrencyRule_edit CurrencyRule_edit {
			get { return (cCurrencyRule_edit)ew_ViewData["CurrencyRule_edit"]; }
			set { ew_ViewData["CurrencyRule_edit"] = value; }
		}

		//
		// Page class for CurrencyRule
		//

		public class cCurrencyRule_edit : cCurrencyRule_edit_base
		{

			// Construtor
			public cCurrencyRule_edit(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cCurrencyRule_edit_base : cCurrencyRule, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "edit";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "CurrencyRule";

			// Page object name
			public string PageObjName = "CurrencyRule_edit";

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

			public cCurrencyRule_edit_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (CurrencyRule)
				if (CurrencyRule == null || CurrencyRule is cCurrencyRule)
					CurrencyRule = this;

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

				// Create form object
				ObjForm = new cFormObj();
				CurrentAction = (ew_Get("a") != "") ? ew_Get("a") : ew_Post("a_list"); // Set up current action
				id.SetVisibility();
				id.Visible = !IsAdd && !IsCopy && !IsGridAdd;
				RuleType.SetVisibility();
				AgentId.SetVisibility();
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

				// Process auto fill
				if (ew_Post("ajax") == "autofill") {
					var results = GetAutoFill(ew_Post("name"), ew_Post("q"));
					if (ew_NotEmpty(results)) {

						// Clean output buffer
						if (!EW_DEBUG_ENABLED)
							ew_Response.Clear();
						return ew_Controller.Content(results, "text/plain", Encoding.UTF8); // Returns utf-8 data
					}
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
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { CurrencyRule, "" }); // DN
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

							// Handle modal response
							if (IsModal) {
								return ew_Controller.Content(ew_ArrayToJson(new List<Dictionary<string, string>> { new Dictionary<string, string> {{ "url", url }}}), "text/plain", Encoding.UTF8); // Returns utf-8 data
							} else {
								return ew_Controller.Redirect(ew_AppUrl(url));
							}
					} else { // Cannot redirect in views, use RedirectException
						ew_Redirect(url);
					}
				}
				return new EmptyResult();
			}
			public int DisplayRecs = 1; // Number of display records
			public int StartRec;
			public int StopRec;
			public int TotalRecs = -1;
			public int RecRange = 10;
			public int RecCnt;
			public Dictionary<string, string> RecKey = new Dictionary<string, string>();
			public string FormClassName = "form-horizontal ewForm ewEditForm";
			public bool IsModal = false;
			public string DbMasterFilter = "";
			public string DbDetailFilter = "";
			public DbDataReader Recordset; // DN
			#pragma warning disable 219

			//
			// Page main
			//

			public IActionResult Page_Main() {

				// Check modal
				IsModal = (ew_Get("modal") == "1" || ew_Post("modal") == "1");
				if (IsModal)
					gbSkipHeaderFooter = true;
				string sReturnUrl = "";
				bool bMatchRecord = false;

				// Load key from QueryString
				if (RouteValues.ContainsKey("id") && ew_NotEmpty(RouteValues["id"])) { // DN
					id.QueryStringValue = Convert.ToString(RouteValues["id"]);
					RecKey["id"] = id.QueryStringValue;
				} else if (ew_NotEmpty(ew_Get("id"))) {
					id.QueryStringValue = ew_Get("id");
					RecKey["id"] = id.QueryStringValue;
				} else {
					sReturnUrl = "CurrencyRulelist"; // Return to list
				}

				// Process form if post back
				if (ew_NotEmpty(ew_Post("a_edit"))) {
					CurrentAction = ew_Post("a_edit"); // Get action code
					LoadFormValues(); // Get form values
				} else {
					CurrentAction = "I"; // Default action is display
				}

				// Check if valid key
				if (ew_Empty(id.CurrentValue)) {
					return Page_Terminate("CurrencyRulelist"); // Invalid key, return to list
				}

				// Validate form if post back
				if (ew_NotEmpty(ew_Post("a_edit"))) {
					if (!ValidateForm()) {
						CurrentAction = ""; // Form error, reset action
						FailureMessage = gsFormError;
						EventCancelled = true; // Event cancelled
						RestoreFormValues();
					}
				}
				switch (CurrentAction) {
					case "I": // Get a record to display
						if (!LoadRow()) { // Load record based on key
							if (ew_Empty(FailureMessage))
								FailureMessage = Language.Phrase("NoRecord"); // No record found
							return Page_Terminate("CurrencyRulelist"); // No matching record, return to list
						}
						break;
					case "U": // Update
						CloseRecordset(); // DN
						sReturnUrl = ReturnUrl;
						if (ew_GetPageName(sReturnUrl) == "CurrencyRulelist")
							sReturnUrl = AddMasterUrl(ListUrl); // List page, return to list page with correct master key if necessary
						SendEmail = true; // Send email on update success
						if (EditRow()) { // Update record based on key
							if (ew_Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
							return Page_Terminate(sReturnUrl); // Return to caller
						} else if (FailureMessage == Language.Phrase("NoRecord")) {
							return Page_Terminate(sReturnUrl); // Return to caller
						} else {
							EventCancelled = true; // Event cancelled
							RestoreFormValues(); // Restore form values if update failed
						}
						break;
				}

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Render the record
				RowType = EW_ROWTYPE_EDIT; // Render as Edit
				ResetAttrs();
				RenderRow();
				return ew_Controller.View();
			}
			#pragma warning restore 219

			// Set up starting record parameters
			public void SetUpStartRec() {
				int PageNo;

				// Exit if DisplayRecs = 0
				if (DisplayRecs == 0)
					return;
				if (IsPageRequest) { // Validate request

					// Check for a "start" parameter
					if (ew_NotEmpty(ew_Get(EW_TABLE_START_REC)) && ew_IsNumeric(ew_Get(EW_TABLE_START_REC))) {
						StartRec = ew_ConvertToInt(ew_Get(EW_TABLE_START_REC));
						StartRecordNumber = StartRec;
					} else if (ew_NotEmpty(ew_Get(EW_TABLE_PAGE_NO)) && ew_IsNumeric(ew_Get(EW_TABLE_PAGE_NO))) {
						PageNo = ew_ConvertToInt(ew_Get(EW_TABLE_PAGE_NO));
						StartRec = (PageNo - 1) * DisplayRecs + 1;
						if (StartRec <= 0) {
							StartRec = 1;
						} else if (StartRec >= ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1) {
							StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1;
						}
						StartRecordNumber = StartRec;
					}
				}
				StartRec = StartRecordNumber;

				// Check if correct start record counter
				if (StartRec <= 0) { // Avoid invalid start record counter
					StartRec = 1; // Reset start record counter
					StartRecordNumber = StartRec;
				} else if (StartRec > TotalRecs) { // Avoid starting record > total records
					StartRec = ((TotalRecs - 1) / DisplayRecs) * DisplayRecs + 1; // Point to last page first record
					StartRecordNumber = StartRec;
				} else if ((StartRec - 1) % DisplayRecs != 0) {
					StartRec = ((StartRec - 1) / DisplayRecs) * DisplayRecs + 1; // Point to page boundary
					StartRecordNumber = StartRec;
				}
			}

			// Confirm page
			public bool ConfirmPage = false;  // DN

			// Get upload files
			public void GetUploadFiles() {

				// Get upload data
			}

			// Load form values
			public void LoadFormValues() {
				if (!id.FldIsDetailKey)
					id.FormValue = ObjForm.GetValue("x_id");
				if (!RuleType.FldIsDetailKey) {
					RuleType.FormValue = ObjForm.GetValue("x_RuleType");
				}
				if (!AgentId.FldIsDetailKey) {
					AgentId.FormValue = ObjForm.GetValue("x_AgentId");
				}
				if (!CurrencyCode.FldIsDetailKey) {
					CurrencyCode.FormValue = ObjForm.GetValue("x_CurrencyCode");
				}
				if (!TransactionType.FldIsDetailKey) {
					TransactionType.FormValue = ObjForm.GetValue("x_TransactionType");
				}
				if (!TransactionTypeCondition.FldIsDetailKey) {
					TransactionTypeCondition.FormValue = ObjForm.GetValue("x_TransactionTypeCondition");
				}
				if (!SingleTransactionBuyAmount.FldIsDetailKey) {
					SingleTransactionBuyAmount.FormValue = ObjForm.GetValue("x_SingleTransactionBuyAmount");
				}
				if (!TransactionPeriodType.FldIsDetailKey) {
					TransactionPeriodType.FormValue = ObjForm.GetValue("x_TransactionPeriodType");
				}
				if (!PeriodStart.FldIsDetailKey) {
					PeriodStart.FormValue = ObjForm.GetValue("x_PeriodStart");
					PeriodStart.CurrentValue = ew_UnformatDateTime(PeriodStart.CurrentValue, 7);
				}
				if (!PeriodEnd.FldIsDetailKey) {
					PeriodEnd.FormValue = ObjForm.GetValue("x_PeriodEnd");
					PeriodEnd.CurrentValue = ew_UnformatDateTime(PeriodEnd.CurrentValue, 7);
				}
				if (!TransactionPeriodCondition.FldIsDetailKey) {
					TransactionPeriodCondition.FormValue = ObjForm.GetValue("x_TransactionPeriodCondition");
				}
				if (!PeriodBuyAmount.FldIsDetailKey) {
					PeriodBuyAmount.FormValue = ObjForm.GetValue("x_PeriodBuyAmount");
				}
				if (!NoOfTransactions.FldIsDetailKey) {
					NoOfTransactions.FormValue = ObjForm.GetValue("x_NoOfTransactions");
				}
				if (!FeeCost.FldIsDetailKey) {
					FeeCost.FormValue = ObjForm.GetValue("x_FeeCost");
				}
				if (!CreatedDate.FldIsDetailKey) {
					CreatedDate.FormValue = ObjForm.GetValue("x_CreatedDate");
					CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 7);
				}
				if (!CreatedBy.FldIsDetailKey) {
					CreatedBy.FormValue = ObjForm.GetValue("x_CreatedBy");
				}
			}

			// Restore form values
			public void RestoreFormValues() {
				LoadRow();
				id.CurrentValue = id.FormValue;
				RuleType.CurrentValue = RuleType.FormValue;
				AgentId.CurrentValue = AgentId.FormValue;
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				TransactionType.CurrentValue = TransactionType.FormValue;
				TransactionTypeCondition.CurrentValue = TransactionTypeCondition.FormValue;
				SingleTransactionBuyAmount.CurrentValue = SingleTransactionBuyAmount.FormValue;
				TransactionPeriodType.CurrentValue = TransactionPeriodType.FormValue;
				PeriodStart.CurrentValue = PeriodStart.FormValue;
				PeriodStart.CurrentValue = ew_UnformatDateTime(PeriodStart.CurrentValue, 7);
				PeriodEnd.CurrentValue = PeriodEnd.FormValue;
				PeriodEnd.CurrentValue = ew_UnformatDateTime(PeriodEnd.CurrentValue, 7);
				TransactionPeriodCondition.CurrentValue = TransactionPeriodCondition.FormValue;
				PeriodBuyAmount.CurrentValue = PeriodBuyAmount.FormValue;
				NoOfTransactions.CurrentValue = NoOfTransactions.FormValue;
				FeeCost.CurrentValue = FeeCost.FormValue;
				CreatedDate.CurrentValue = CreatedDate.FormValue;
				CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 7);
				CreatedBy.CurrentValue = CreatedBy.FormValue;
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
				AgentId.DbValue = row["AgentId"];
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
				AgentId.SetDbValue(row["AgentId"]);
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
				// AgentId
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

					// AgentId
					AgentId.ViewValue = AgentId.CurrentValue;
					if (ew_NotEmpty(AgentId.CurrentValue)) {
						sFilterWrk = "[AgentId]" + ew_SearchString("=", Convert.ToString(AgentId.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT [AgentId], [AgentName] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Agent]";
						sWhereWrk = "";
						AgentId.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentName]"}};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(AgentId, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							AgentId.ViewValue = AgentId.DisplayValue(odwrk);
						} else {
							AgentId.ViewValue = AgentId.CurrentValue;
						}
					} else {
						AgentId.ViewValue = System.DBNull.Value;
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
					PeriodStart.ViewValue = ew_FormatDateTime(PeriodStart.ViewValue, 7);

					// PeriodEnd
					PeriodEnd.ViewValue = PeriodEnd.CurrentValue;
					PeriodEnd.ViewValue = ew_FormatDateTime(PeriodEnd.ViewValue, 7);

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
					CreatedDate.ViewValue = ew_FormatDateTime(CreatedDate.ViewValue, 7);

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

					// AgentId
					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";
					AgentId.TooltipValue = "";

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
				} else if (RowType == EW_ROWTYPE_EDIT) { // Edit row

					// id
					id.EditAttrs["class"] = "form-control";
					id.EditValue = id.CurrentValue;

					// RuleType
					RuleType.EditValue = RuleType.Options(false);

					// AgentId
					AgentId.EditAttrs["class"] = "form-control";
					AgentId.EditValue = AgentId.CurrentValue; // DN
					if (ew_NotEmpty(AgentId.CurrentValue)) {
						sFilterWrk = "[AgentId]" + ew_SearchString("=", Convert.ToString(AgentId.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT [AgentId], [AgentName] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Agent]";
						sWhereWrk = "";
						AgentId.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentName]"}};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(AgentId, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(ew_HtmlEncode(odwrk[1]));
							AgentId.EditValue = AgentId.DisplayValue(odwrk);
						} else {
							AgentId.EditValue = ew_HtmlEncode(AgentId.CurrentValue);
						}
					} else {
						AgentId.EditValue = System.DBNull.Value;
					}
					AgentId.PlaceHolder = ew_RemoveHtml(AgentId.FldCaption);

					// CurrencyCode
						if (ew_Empty(CurrencyCode.CurrentValue)) {
							sFilterWrk = "0=1";
						} else {
							arwrk = Convert.ToString(CurrencyCode.CurrentValue).Split(',');
							sFilterWrk = "";
							for (int ari = 0; ari < arwrk.Length; ari++) {
								if (sFilterWrk != "") sFilterWrk += " OR ";
								sFilterWrk += "[CurrencyCode]" + ew_SearchString("=", arwrk[ari].Trim(), EW_DATATYPE_STRING, "");
							}
						}
						sSqlWrk = "SELECT DISTINCT [CurrencyCode], [CurrencyCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[Currency]";
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
							odwrk[1] = Convert.ToString(ew_HtmlEncode(odwrk[1]));
							CurrencyCode.ViewValue = String.Concat(CurrencyCode.ViewValue, CurrencyCode.DisplayValue(odwrk));
							if (ari < rswrk.Count - 1) CurrencyCode.ViewValue = String.Concat(CurrencyCode.ViewValue, ew_ViewOptionSeparator(ari)); // Separate Options
						}
						foreach (var od in rswrk) {
						}
					} else {
						CurrencyCode.ViewValue = @Language.Phrase("PleaseSelect");
					}
					CurrencyCode.EditValue = rswrk;

					// TransactionType
					TransactionType.EditValue = TransactionType.Options(false);

					// TransactionTypeCondition
					TransactionTypeCondition.EditValue = TransactionTypeCondition.Options(false);

					// SingleTransactionBuyAmount
					SingleTransactionBuyAmount.EditAttrs["class"] = "form-control";
					SingleTransactionBuyAmount.EditValue = SingleTransactionBuyAmount.CurrentValue; // DN
					SingleTransactionBuyAmount.PlaceHolder = ew_RemoveHtml(SingleTransactionBuyAmount.FldCaption);
					if (ew_NotEmpty(SingleTransactionBuyAmount.EditValue) && ew_IsNumeric(Convert.ToString(SingleTransactionBuyAmount.EditValue))) SingleTransactionBuyAmount.EditValue = ew_FormatNumber(SingleTransactionBuyAmount.EditValue, -2, -1, -2, 0);

					// TransactionPeriodType
					TransactionPeriodType.EditValue = TransactionPeriodType.Options(false);

					// PeriodStart
					PeriodStart.EditAttrs["class"] = "form-control";
					PeriodStart.EditValue = ew_FormatDateTime(PeriodStart.CurrentValue, 7); // DN
					PeriodStart.PlaceHolder = ew_RemoveHtml(PeriodStart.FldCaption);

					// PeriodEnd
					PeriodEnd.EditAttrs["class"] = "form-control";
					PeriodEnd.EditValue = ew_FormatDateTime(PeriodEnd.CurrentValue, 7); // DN
					PeriodEnd.PlaceHolder = ew_RemoveHtml(PeriodEnd.FldCaption);

					// TransactionPeriodCondition
					TransactionPeriodCondition.EditValue = TransactionPeriodCondition.Options(false);

					// PeriodBuyAmount
					PeriodBuyAmount.EditAttrs["class"] = "form-control";
					PeriodBuyAmount.EditValue = PeriodBuyAmount.CurrentValue; // DN
					PeriodBuyAmount.PlaceHolder = ew_RemoveHtml(PeriodBuyAmount.FldCaption);
					if (ew_NotEmpty(PeriodBuyAmount.EditValue) && ew_IsNumeric(Convert.ToString(PeriodBuyAmount.EditValue))) PeriodBuyAmount.EditValue = ew_FormatNumber(PeriodBuyAmount.EditValue, -2, -1, -2, 0);

					// NoOfTransactions
					NoOfTransactions.EditAttrs["class"] = "form-control";
					NoOfTransactions.EditValue = NoOfTransactions.CurrentValue; // DN
					NoOfTransactions.PlaceHolder = ew_RemoveHtml(NoOfTransactions.FldCaption);

					// FeeCost
					FeeCost.EditAttrs["class"] = "form-control";
					FeeCost.EditValue = FeeCost.CurrentValue; // DN
					FeeCost.PlaceHolder = ew_RemoveHtml(FeeCost.FldCaption);
					if (ew_NotEmpty(FeeCost.EditValue) && ew_IsNumeric(Convert.ToString(FeeCost.EditValue))) FeeCost.EditValue = ew_FormatNumber(FeeCost.EditValue, -2, -1, -2, 0);

					// CreatedDate
					CreatedDate.EditAttrs["class"] = "form-control";
					CreatedDate.EditValue = ew_FormatDateTime(CreatedDate.CurrentValue, 7); // DN
					CreatedDate.PlaceHolder = ew_RemoveHtml(CreatedDate.FldCaption);

					// CreatedBy
					CreatedBy.EditAttrs["class"] = "form-control";
					CreatedBy.EditValue = CreatedBy.CurrentValue; // DN
					CreatedBy.PlaceHolder = ew_RemoveHtml(CreatedBy.FldCaption);

					// Edit refer script
					// id

					id.LinkCustomAttributes = id.FldTagACustomAttributes; // DN
					id.HrefValue = "";

					// RuleType
					RuleType.LinkCustomAttributes = RuleType.FldTagACustomAttributes; // DN
					RuleType.HrefValue = "";

					// AgentId
					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";

					// CurrencyCode
					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";

					// TransactionType
					TransactionType.LinkCustomAttributes = TransactionType.FldTagACustomAttributes; // DN
					TransactionType.HrefValue = "";

					// TransactionTypeCondition
					TransactionTypeCondition.LinkCustomAttributes = TransactionTypeCondition.FldTagACustomAttributes; // DN
					TransactionTypeCondition.HrefValue = "";

					// SingleTransactionBuyAmount
					SingleTransactionBuyAmount.LinkCustomAttributes = SingleTransactionBuyAmount.FldTagACustomAttributes; // DN
					SingleTransactionBuyAmount.HrefValue = "";

					// TransactionPeriodType
					TransactionPeriodType.LinkCustomAttributes = TransactionPeriodType.FldTagACustomAttributes; // DN
					TransactionPeriodType.HrefValue = "";

					// PeriodStart
					PeriodStart.LinkCustomAttributes = PeriodStart.FldTagACustomAttributes; // DN
					PeriodStart.HrefValue = "";

					// PeriodEnd
					PeriodEnd.LinkCustomAttributes = PeriodEnd.FldTagACustomAttributes; // DN
					PeriodEnd.HrefValue = "";

					// TransactionPeriodCondition
					TransactionPeriodCondition.LinkCustomAttributes = TransactionPeriodCondition.FldTagACustomAttributes; // DN
					TransactionPeriodCondition.HrefValue = "";

					// PeriodBuyAmount
					PeriodBuyAmount.LinkCustomAttributes = PeriodBuyAmount.FldTagACustomAttributes; // DN
					PeriodBuyAmount.HrefValue = "";

					// NoOfTransactions
					NoOfTransactions.LinkCustomAttributes = NoOfTransactions.FldTagACustomAttributes; // DN
					NoOfTransactions.HrefValue = "";

					// FeeCost
					FeeCost.LinkCustomAttributes = FeeCost.FldTagACustomAttributes; // DN
					FeeCost.HrefValue = "";

					// CreatedDate
					CreatedDate.LinkCustomAttributes = CreatedDate.FldTagACustomAttributes; // DN
					CreatedDate.HrefValue = "";

					// CreatedBy
					CreatedBy.LinkCustomAttributes = CreatedBy.FldTagACustomAttributes; // DN
					CreatedBy.HrefValue = "";
				}
				if (RowType == EW_ROWTYPE_ADD ||
					RowType == EW_ROWTYPE_EDIT ||
					RowType == EW_ROWTYPE_SEARCH) { // Add / Edit / Search row
					SetupFieldTitles();
				}

				// Call Row Rendered event
				if (RowType != EW_ROWTYPE_AGGREGATEINIT)
					Row_Rendered();
			}

			// Validate form
			public bool ValidateForm() {

				// Initialize form error message
				gsFormError = "";

				// Check if validation required
				if (!EW_SERVER_VALIDATE)
					return (gsFormError == "");
				if (!ew_CheckNumber(SingleTransactionBuyAmount.FormValue))
					gsFormError = ew_AddMessage(gsFormError, SingleTransactionBuyAmount.FldErrMsg);
				if (!ew_CheckEuroDate(PeriodStart.FormValue))
					gsFormError = ew_AddMessage(gsFormError, PeriodStart.FldErrMsg);
				if (!ew_CheckEuroDate(PeriodEnd.FormValue))
					gsFormError = ew_AddMessage(gsFormError, PeriodEnd.FldErrMsg);
				if (!ew_CheckNumber(PeriodBuyAmount.FormValue))
					gsFormError = ew_AddMessage(gsFormError, PeriodBuyAmount.FldErrMsg);
				if (!ew_CheckInteger(NoOfTransactions.FormValue))
					gsFormError = ew_AddMessage(gsFormError, NoOfTransactions.FldErrMsg);
				if (!ew_CheckNumber(FeeCost.FormValue))
					gsFormError = ew_AddMessage(gsFormError, FeeCost.FldErrMsg);
				if (!ew_CheckEuroDate(CreatedDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreatedDate.FldErrMsg);

				// Return validate result
				bool valid = (ew_Empty(gsFormError));

				// Call Form_CustomValidate event
				string sFormCustomError = "";
				valid = valid && Form_CustomValidate(ref sFormCustomError);
				gsFormError = ew_AddMessage(gsFormError, sFormCustomError);
				return valid;
			}

			// Update record based on key values
			#pragma warning disable 168, 219
			public bool EditRow() {
				var result = false;
				OrderedDictionary rsold = null;
				var rsnew = new OrderedDictionary();
				var sFilter = KeyFilter;
				sFilter = ApplyUserIDFilters(sFilter);
				CurrentFilter = sFilter;
				string sSql = SQL;
				try {
					using (var rsedit = Connection.GetDataReader(sSql)) { // Use primary connection // DN
						if (rsedit == null || !rsedit.Read()) {
							FailureMessage = Language.Phrase("NoRecord"); // Set no record message
							return false;
						}
						rsold = Connection.GetRow(rsedit);
						LoadDbValues(rsold);
					}
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED)
						throw;
					FailureMessage = e.Message;
					return false;
				}

				// id
				// RuleType

				RuleType.SetDbValue(ref rsnew, RuleType.CurrentValue, System.DBNull.Value, RuleType.ReadOnly);

				// AgentId
				AgentId.SetDbValue(ref rsnew, AgentId.CurrentValue, System.DBNull.Value, AgentId.ReadOnly);

				// CurrencyCode
				CurrencyCode.SetDbValue(ref rsnew, CurrencyCode.CurrentValue, System.DBNull.Value, CurrencyCode.ReadOnly);

				// TransactionType
				TransactionType.SetDbValue(ref rsnew, TransactionType.CurrentValue, System.DBNull.Value, TransactionType.ReadOnly);

				// TransactionTypeCondition
				TransactionTypeCondition.SetDbValue(ref rsnew, TransactionTypeCondition.CurrentValue, System.DBNull.Value, TransactionTypeCondition.ReadOnly);

				// SingleTransactionBuyAmount
				SingleTransactionBuyAmount.SetDbValue(ref rsnew, SingleTransactionBuyAmount.CurrentValue, System.DBNull.Value, SingleTransactionBuyAmount.ReadOnly);

				// TransactionPeriodType
				TransactionPeriodType.SetDbValue(ref rsnew, TransactionPeriodType.CurrentValue, System.DBNull.Value, TransactionPeriodType.ReadOnly);

				// PeriodStart
				PeriodStart.SetDbValue(ref rsnew, ew_UnformatDateTime(PeriodStart.CurrentValue, 7), System.DBNull.Value, PeriodStart.ReadOnly);

				// PeriodEnd
				PeriodEnd.SetDbValue(ref rsnew, ew_UnformatDateTime(PeriodEnd.CurrentValue, 7), System.DBNull.Value, PeriodEnd.ReadOnly);

				// TransactionPeriodCondition
				TransactionPeriodCondition.SetDbValue(ref rsnew, TransactionPeriodCondition.CurrentValue, System.DBNull.Value, TransactionPeriodCondition.ReadOnly);

				// PeriodBuyAmount
				PeriodBuyAmount.SetDbValue(ref rsnew, PeriodBuyAmount.CurrentValue, System.DBNull.Value, PeriodBuyAmount.ReadOnly);

				// NoOfTransactions
				NoOfTransactions.SetDbValue(ref rsnew, NoOfTransactions.CurrentValue, System.DBNull.Value, NoOfTransactions.ReadOnly);

				// FeeCost
				FeeCost.SetDbValue(ref rsnew, FeeCost.CurrentValue, System.DBNull.Value, FeeCost.ReadOnly);

				// CreatedDate
				CreatedDate.SetDbValue(ref rsnew, ew_UnformatDateTime(CreatedDate.CurrentValue, 7), System.DBNull.Value, CreatedDate.ReadOnly);

				// CreatedBy
				CreatedBy.SetDbValue(ref rsnew, CreatedBy.CurrentValue, System.DBNull.Value, CreatedBy.ReadOnly);

				// Call Row Updating event
				var bUpdateRow = Row_Updating(rsold, ref rsnew);
				if (bUpdateRow) {
					try {
						if (rsnew.Count > 0)
							result = Update(rsnew, "", rsold) > 0;
						else
							result = true;
						if (result) {
						}
					} catch (Exception e) {
						if (EW_DEBUG_ENABLED) throw;
						FailureMessage = e.Message;
						return false;
					}
				} else {
					if (ew_NotEmpty(SuccessMessage) || ew_NotEmpty(FailureMessage)) {

						// Use the message, do nothing
					} else if (ew_NotEmpty(CancelMessage)) {
						FailureMessage = CancelMessage;
						CancelMessage = "";
					} else {
						FailureMessage = Language.Phrase("UpdateCancelled");
					}
					result = false;
				}

				// Call Row_Updated event
				if (result)
					Row_Updated(rsold, rsnew);
				return result;
			}

			// Set up Breadcrumb
			public void SetupBreadcrumb() {
				Breadcrumb = new cBreadcrumb();
				var url = ew_CurrentUrl();
				Breadcrumb.Add("list", TableVar, ew_AppPath(AddMasterUrl("CurrencyRulelist")), "", TableVar, true);
				var PageId = "edit";
				Breadcrumb.Add("edit", PageId, url);
			}
			#pragma warning disable 168, 1522

			// Setup lookup filters of a field
			public override void SetupLookupFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
					case "x_AgentId":
						sSqlWrk = "";
							sSqlWrk = "SELECT [AgentId] AS [LinkFld], [AgentName] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Agent]";
							sWhereWrk = "{filter}";
							AgentId.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentName]"}};
						AgentId.LookupFilters.Add("s", sSqlWrk);
						AgentId.LookupFilters.Add("d", "");
						AgentId.LookupFilters.Add("f0", "[AgentId] = {filter_value}");
						AgentId.LookupFilters.Add("t0", "200");
						AgentId.LookupFilters.Add("fn0", "");
						sSqlWrk = "";
						Lookup_Selecting(AgentId, ref sWhereWrk);
							if (sWhereWrk != "") {
								sSqlWrk += " WHERE " + sWhereWrk;
							}
						sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							AgentId.LookupFilters["s"] += sSqlWrk;
						break;
					case "x_CurrencyCode":
						sSqlWrk = "";
							sSqlWrk = "SELECT DISTINCT [CurrencyCode] AS [LinkFld], [CurrencyCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Currency]";
							sWhereWrk = "{filter}";
							CurrencyCode.LookupFilters = new Dictionary<string, string>() {{"dx1", "[CurrencyCode]"}};
						CurrencyCode.LookupFilters.Add("s", sSqlWrk);
						CurrencyCode.LookupFilters.Add("d", "");
						CurrencyCode.LookupFilters.Add("f0", "[CurrencyCode] = {filter_value}");
						CurrencyCode.LookupFilters.Add("t0", "200");
						CurrencyCode.LookupFilters.Add("fn0", "");
						sSqlWrk = "";
						Lookup_Selecting(CurrencyCode, ref sWhereWrk);
							if (sWhereWrk != "") {
								sSqlWrk += " WHERE " + sWhereWrk;
							}
						sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrencyCode] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							CurrencyCode.LookupFilters["s"] += sSqlWrk;
						break;
				}
			}

			// Setup AutoSuggest filters of a field
			public override void SetupAutoSuggestFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
					case "x_AgentId":
						sSqlWrk = "";
						sSqlWrk = "SELECT  TOP " + EW_AUTO_SUGGEST_MAX_ENTRIES + " [AgentId], [AgentName] AS [DispFld] FROM [dbo].[Agent]";
						sWhereWrk = "" + "[AgentName]" + " LIKE '{query_value}%'";
						AgentId.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentName]"}};
						fld.LookupFilters = new Dictionary<string, string>() {{"s", sSqlWrk}, {"d", ""}}; // DN
						sSqlWrk = "";
					Lookup_Selecting(AgentId, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							fld.LookupFilters["s"] += sSqlWrk;
						break;
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

			// Form Custom Validate event
			public virtual bool Form_CustomValidate(ref string CustomError) {

				//Return error message in CustomError
				return true;
			}
		}
	}
}
