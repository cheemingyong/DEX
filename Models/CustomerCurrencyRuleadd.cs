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

		// CustomerCurrencyRule_add
		public static cCustomerCurrencyRule_add CustomerCurrencyRule_add {
			get { return (cCustomerCurrencyRule_add)ew_ViewData["CustomerCurrencyRule_add"]; }
			set { ew_ViewData["CustomerCurrencyRule_add"] = value; }
		}

		//
		// Page class for CustomerCurrencyRule
		//

		public class cCustomerCurrencyRule_add : cCustomerCurrencyRule_add_base
		{

			// Construtor
			public cCustomerCurrencyRule_add(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cCustomerCurrencyRule_add_base : cCustomerCurrencyRule, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "add";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "CustomerCurrencyRule";

			// Page object name
			public string PageObjName = "CustomerCurrencyRule_add";

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

			public cCustomerCurrencyRule_add_base(Controller controller = null) { // DN
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

				// Create form object
				ObjForm = new cFormObj();
				CurrentAction = (ew_Get("a") != "") ? ew_Get("a") : ew_Post("a_list"); // Set up current action
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
			public string FormClassName = "form-horizontal ewForm ewAddForm";
			public bool IsModal = false;
			public string DbMasterFilter = "";
			public string DbDetailFilter = "";
			public int StartRec;
			public int Priv = 0;
			public DbDataReader OldRecordset = null;
			public DbDataReader Recordset = null; // Reserved // DN
			public bool CopyRecord;

			//
			// Page main
			//

			public IActionResult Page_Main() {

				// Check modal
				IsModal = (ew_Get("modal") == "1" || ew_Post("modal") == "1");
				if (IsModal)
					gbSkipHeaderFooter = true;

				// Process form if post back
				if (ew_NotEmpty(ew_Post("a_add"))) {
					CurrentAction = ew_Post("a_add"); // Get form action
					CopyRecord = LoadOldRecord(); // Load old recordset
					LoadFormValues(); // Load form values
				} else { // Not post back

					// Load key from QueryString
					CopyRecord = true;
					if (RouteValues.ContainsKey("id") && ew_NotEmpty(RouteValues["id"])) { // DN
						id.QueryStringValue = Convert.ToString(RouteValues["id"]);
						SetKey("id", id.CurrentValue); // Set up key
					} else if (ew_NotEmpty(ew_Get("id"))) {
						id.QueryStringValue = ew_Get("id");
						SetKey("id", id.CurrentValue); // Set up key
					} else {
						SetKey("id", ""); // Clear key
						CopyRecord = false;
					}
					if (CopyRecord) {
						CurrentAction = "C"; // Copy record
					} else {
						CurrentAction = "I"; // Display blank record
					}
				}

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Validate form if post back
				if (ew_NotEmpty(ew_Post("a_add"))) {
					if (!ValidateForm()) {
						CurrentAction = "I"; // Form error, reset action
						EventCancelled = true; // Event cancelled
						RestoreFormValues(); // Restore form values
						FailureMessage = gsFormError;
					}
				} else {
					if (CurrentAction == "I") // Load default values for blank record
						LoadDefaultValues();
				}

				// Perform action based on action code
				switch (CurrentAction) {
					case "I": // Blank record, no action required
						break;
					case "C": // Copy an existing record
						if (!LoadRow()) { // Load record based on key
							if (ew_Empty(FailureMessage))
								FailureMessage = Language.Phrase("NoRecord"); // No record found
							return Page_Terminate("CustomerCurrencyRulelist"); // No matching record, return to List page // DN
						}
						break;
					case "A": // Add new record
						SendEmail = true; // Send email on add success
						var rsold = Connection.GetRow(OldRecordset);
						OldRecordset?.Close();
						OldRecordset?.Dispose();
						if (AddRow(rsold)) { // Add successful
							if (ew_Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
							string sReturnUrl = "";
							sReturnUrl = ReturnUrl;
							if (ew_GetPageName(sReturnUrl) == "CustomerCurrencyRulelist")
								sReturnUrl = AddMasterUrl(ListUrl); // List page, return to list page with correct master key if necessary
							else if (ew_GetPageName(sReturnUrl) == "CustomerCurrencyRuleview")
								sReturnUrl = ViewUrl; // View page, return to view page with key URL directly
							return Page_Terminate(sReturnUrl); // Clean up and return // DN
						} else {
							EventCancelled = true; // Event cancelled
							RestoreFormValues(); // Add failed, restore form values
						}
						break;
				}

				// Render row based on row type
				RowType = EW_ROWTYPE_ADD; // Render add type

				// Render row
				ResetAttrs();
				RenderRow();
				return ew_Controller.View();
			}

			// Confirm page
			public bool ConfirmPage = false;  // DN

			// Get upload files
			public void GetUploadFiles() {

				// Get upload data
			}

			// Load default values
			public void LoadDefaultValues() {
				RuleType.CurrentValue = System.DBNull.Value;
				RuleType.OldValue = RuleType.CurrentValue;
				CurrencyCode.CurrentValue = System.DBNull.Value;
				CurrencyCode.OldValue = CurrencyCode.CurrentValue;
				TransactionType.CurrentValue = System.DBNull.Value;
				TransactionType.OldValue = TransactionType.CurrentValue;
				TransactionTypeCondition.CurrentValue = System.DBNull.Value;
				TransactionTypeCondition.OldValue = TransactionTypeCondition.CurrentValue;
				SingleTransactionBuyAmount.CurrentValue = System.DBNull.Value;
				SingleTransactionBuyAmount.OldValue = SingleTransactionBuyAmount.CurrentValue;
				TransactionPeriodType.CurrentValue = System.DBNull.Value;
				TransactionPeriodType.OldValue = TransactionPeriodType.CurrentValue;
				PeriodStart.CurrentValue = System.DBNull.Value;
				PeriodStart.OldValue = PeriodStart.CurrentValue;
				PeriodEnd.CurrentValue = System.DBNull.Value;
				PeriodEnd.OldValue = PeriodEnd.CurrentValue;
				TransactionPeriodCondition.CurrentValue = System.DBNull.Value;
				TransactionPeriodCondition.OldValue = TransactionPeriodCondition.CurrentValue;
				PeriodBuyAmount.CurrentValue = System.DBNull.Value;
				PeriodBuyAmount.OldValue = PeriodBuyAmount.CurrentValue;
				NoOfTransactions.CurrentValue = System.DBNull.Value;
				NoOfTransactions.OldValue = NoOfTransactions.CurrentValue;
				FeeCost.CurrentValue = System.DBNull.Value;
				FeeCost.OldValue = FeeCost.CurrentValue;
				CreatedDate.CurrentValue = System.DBNull.Value;
				CreatedDate.OldValue = CreatedDate.CurrentValue;
				CreatedBy.CurrentValue = System.DBNull.Value;
				CreatedBy.OldValue = CreatedBy.CurrentValue;
			}

			// Load form values
			public void LoadFormValues() {
				if (!RuleType.FldIsDetailKey) {
					RuleType.FormValue = ObjForm.GetValue("x_RuleType");
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
					PeriodStart.CurrentValue = ew_UnformatDateTime(PeriodStart.CurrentValue, 0);
				}
				if (!PeriodEnd.FldIsDetailKey) {
					PeriodEnd.FormValue = ObjForm.GetValue("x_PeriodEnd");
					PeriodEnd.CurrentValue = ew_UnformatDateTime(PeriodEnd.CurrentValue, 0);
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
					CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 0);
				}
				if (!CreatedBy.FldIsDetailKey) {
					CreatedBy.FormValue = ObjForm.GetValue("x_CreatedBy");
				}
			}

			// Restore form values
			public void RestoreFormValues() {
				LoadOldRecord();
				RuleType.CurrentValue = RuleType.FormValue;
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				TransactionType.CurrentValue = TransactionType.FormValue;
				TransactionTypeCondition.CurrentValue = TransactionTypeCondition.FormValue;
				SingleTransactionBuyAmount.CurrentValue = SingleTransactionBuyAmount.FormValue;
				TransactionPeriodType.CurrentValue = TransactionPeriodType.FormValue;
				PeriodStart.CurrentValue = PeriodStart.FormValue;
				PeriodStart.CurrentValue = ew_UnformatDateTime(PeriodStart.CurrentValue, 0);
				PeriodEnd.CurrentValue = PeriodEnd.FormValue;
				PeriodEnd.CurrentValue = ew_UnformatDateTime(PeriodEnd.CurrentValue, 0);
				TransactionPeriodCondition.CurrentValue = TransactionPeriodCondition.FormValue;
				PeriodBuyAmount.CurrentValue = PeriodBuyAmount.FormValue;
				NoOfTransactions.CurrentValue = NoOfTransactions.FormValue;
				FeeCost.CurrentValue = FeeCost.FormValue;
				CreatedDate.CurrentValue = CreatedDate.FormValue;
				CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 0);
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
			#pragma warning disable 618

			// Load old record
			public bool LoadOldRecord(cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {

				// Load key values from Session
				bool bValidKey = true;
				if (ew_NotEmpty(GetKey("id")))
					id.CurrentValue = GetKey("id"); // id
				else
					bValidKey = false;

				// Load old recordset
				if (bValidKey) {
					CurrentFilter = KeyFilter;
					string sSql = SQL;
					try {
						OldRecordset = cnn?.OpenDataReader(sSql) ?? Connection.OpenDataReader(sSql);
						if (OldRecordset != null && OldRecordset.Read())
							LoadRowValues(OldRecordset); // Load row values
						return true;
					} catch { return false; }
				} else {
					OldRecordset = null;
				}
				return bValidKey;
			}
			#pragma warning restore 618

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
				} else if (RowType == EW_ROWTYPE_ADD) { // Add row

					// RuleType
					RuleType.EditValue = RuleType.Options(false);

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
					TransactionType.EditAttrs["class"] = "form-control";
					TransactionType.EditValue = TransactionType.Options(true);

					// TransactionTypeCondition
					TransactionTypeCondition.EditAttrs["class"] = "form-control";
					TransactionTypeCondition.EditValue = TransactionTypeCondition.Options(true);

					// SingleTransactionBuyAmount
					SingleTransactionBuyAmount.EditAttrs["class"] = "form-control";
					SingleTransactionBuyAmount.EditValue = SingleTransactionBuyAmount.CurrentValue; // DN
					SingleTransactionBuyAmount.PlaceHolder = ew_RemoveHtml(SingleTransactionBuyAmount.FldCaption);
					if (ew_NotEmpty(SingleTransactionBuyAmount.EditValue) && ew_IsNumeric(Convert.ToString(SingleTransactionBuyAmount.EditValue))) SingleTransactionBuyAmount.EditValue = ew_FormatNumber(SingleTransactionBuyAmount.EditValue, -2, -1, -2, 0);

					// TransactionPeriodType
					TransactionPeriodType.EditAttrs["class"] = "form-control";
					TransactionPeriodType.EditValue = TransactionPeriodType.Options(true);

					// PeriodStart
					PeriodStart.EditAttrs["class"] = "form-control";
					PeriodStart.EditValue = ew_FormatDateTime(PeriodStart.CurrentValue, 8); // DN
					PeriodStart.PlaceHolder = ew_RemoveHtml(PeriodStart.FldCaption);

					// PeriodEnd
					PeriodEnd.EditAttrs["class"] = "form-control";
					PeriodEnd.EditValue = ew_FormatDateTime(PeriodEnd.CurrentValue, 8); // DN
					PeriodEnd.PlaceHolder = ew_RemoveHtml(PeriodEnd.FldCaption);

					// TransactionPeriodCondition
					TransactionPeriodCondition.EditAttrs["class"] = "form-control";
					TransactionPeriodCondition.EditValue = TransactionPeriodCondition.Options(true);

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
					CreatedDate.EditValue = ew_FormatDateTime(CreatedDate.CurrentValue, 8); // DN
					CreatedDate.PlaceHolder = ew_RemoveHtml(CreatedDate.FldCaption);

					// CreatedBy
					CreatedBy.EditAttrs["class"] = "form-control";
					CreatedBy.EditValue = CreatedBy.CurrentValue; // DN
					CreatedBy.PlaceHolder = ew_RemoveHtml(CreatedBy.FldCaption);

					// Add refer script
					// RuleType

					RuleType.LinkCustomAttributes = RuleType.FldTagACustomAttributes; // DN
					RuleType.HrefValue = "";

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
				if (!ew_CheckDateDef(PeriodStart.FormValue))
					gsFormError = ew_AddMessage(gsFormError, PeriodStart.FldErrMsg);
				if (!ew_CheckDateDef(PeriodEnd.FormValue))
					gsFormError = ew_AddMessage(gsFormError, PeriodEnd.FldErrMsg);
				if (!ew_CheckNumber(PeriodBuyAmount.FormValue))
					gsFormError = ew_AddMessage(gsFormError, PeriodBuyAmount.FldErrMsg);
				if (!ew_CheckInteger(NoOfTransactions.FormValue))
					gsFormError = ew_AddMessage(gsFormError, NoOfTransactions.FldErrMsg);
				if (!ew_CheckNumber(FeeCost.FormValue))
					gsFormError = ew_AddMessage(gsFormError, FeeCost.FldErrMsg);
				if (!ew_CheckDateDef(CreatedDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreatedDate.FldErrMsg);

				// Return validate result
				bool valid = (ew_Empty(gsFormError));

				// Call Form_CustomValidate event
				string sFormCustomError = "";
				valid = valid && Form_CustomValidate(ref sFormCustomError);
				gsFormError = ew_AddMessage(gsFormError, sFormCustomError);
				return valid;
			}

			// Add record
			#pragma warning disable 168, 219
			public bool AddRow(OrderedDictionary rsold = null) {
				bool result = false;
				var rsnew = new OrderedDictionary();

				// Load db values from rsold
				if (rsold != null) {
					LoadDbValues(rsold);
				}
				try {

					// RuleType
					RuleType.SetDbValue(ref rsnew, RuleType.CurrentValue, System.DBNull.Value, false);

					// CurrencyCode
					CurrencyCode.SetDbValue(ref rsnew, CurrencyCode.CurrentValue, System.DBNull.Value, false);

					// TransactionType
					TransactionType.SetDbValue(ref rsnew, TransactionType.CurrentValue, System.DBNull.Value, false);

					// TransactionTypeCondition
					TransactionTypeCondition.SetDbValue(ref rsnew, TransactionTypeCondition.CurrentValue, System.DBNull.Value, false);

					// SingleTransactionBuyAmount
					SingleTransactionBuyAmount.SetDbValue(ref rsnew, SingleTransactionBuyAmount.CurrentValue, System.DBNull.Value, false);

					// TransactionPeriodType
					TransactionPeriodType.SetDbValue(ref rsnew, TransactionPeriodType.CurrentValue, System.DBNull.Value, false);

					// PeriodStart
					PeriodStart.SetDbValue(ref rsnew, ew_UnformatDateTime(PeriodStart.CurrentValue, 0), System.DBNull.Value, false);

					// PeriodEnd
					PeriodEnd.SetDbValue(ref rsnew, ew_UnformatDateTime(PeriodEnd.CurrentValue, 0), System.DBNull.Value, false);

					// TransactionPeriodCondition
					TransactionPeriodCondition.SetDbValue(ref rsnew, TransactionPeriodCondition.CurrentValue, System.DBNull.Value, false);

					// PeriodBuyAmount
					PeriodBuyAmount.SetDbValue(ref rsnew, PeriodBuyAmount.CurrentValue, System.DBNull.Value, false);

					// NoOfTransactions
					NoOfTransactions.SetDbValue(ref rsnew, NoOfTransactions.CurrentValue, System.DBNull.Value, false);

					// FeeCost
					FeeCost.SetDbValue(ref rsnew, FeeCost.CurrentValue, System.DBNull.Value, false);

					// CreatedDate
					CreatedDate.SetDbValue(ref rsnew, ew_UnformatDateTime(CreatedDate.CurrentValue, 0), System.DBNull.Value, false);

					// CreatedBy
					CreatedBy.SetDbValue(ref rsnew, CreatedBy.CurrentValue, System.DBNull.Value, false);
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw;
					FailureMessage = e.Message;
					return false;
				}

				// Call Row Inserting event
				bool bInsertRow = Row_Inserting(rsold, ref rsnew);
				if (bInsertRow) {
					try {
						Insert(rsnew);
						result = true;
					} catch (Exception e) {
						if (EW_DEBUG_ENABLED) throw;
						FailureMessage = e.Message;
						result = false;
					}
					if (result) {
					}
				} else {
					if (SuccessMessage != "" || FailureMessage != "") {

						// Use the message, do nothing
					} else if (CancelMessage != "") {
						FailureMessage = CancelMessage;
						CancelMessage = "";
					} else {
						FailureMessage = Language.Phrase("InsertCancelled");
					}
					result = false;
				}
				if (result) {

					// Call Row Inserted event
					Row_Inserted(rsold, rsnew);
				}
				return result;
			}

			// Set up Breadcrumb
			public void SetupBreadcrumb() {
				Breadcrumb = new cBreadcrumb();
				var url = ew_CurrentUrl();
				Breadcrumb.Add("list", TableVar, ew_AppPath(AddMasterUrl("CustomerCurrencyRulelist")), "", TableVar, true);
				var PageId = (CurrentAction == "C") ? "Copy" : "Add";
				Breadcrumb.Add("add", PageId, url);
			}
			#pragma warning disable 168, 1522

			// Setup lookup filters of a field
			public override void SetupLookupFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
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
