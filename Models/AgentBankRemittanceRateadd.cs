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

		// AgentBankRemittanceRate_add
		public static cAgentBankRemittanceRate_add AgentBankRemittanceRate_add {
			get { return (cAgentBankRemittanceRate_add)ew_ViewData["AgentBankRemittanceRate_add"]; }
			set { ew_ViewData["AgentBankRemittanceRate_add"] = value; }
		}

		//
		// Page class for AgentBankRemittanceRate
		//

		public class cAgentBankRemittanceRate_add : cAgentBankRemittanceRate_add_base
		{

			// Construtor
			public cAgentBankRemittanceRate_add(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cAgentBankRemittanceRate_add_base : cAgentBankRemittanceRate, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "add";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "AgentBankRemittanceRate";

			// Page object name
			public string PageObjName = "AgentBankRemittanceRate_add";

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

			public cAgentBankRemittanceRate_add_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (AgentBankRemittanceRate)
				if (AgentBankRemittanceRate == null || AgentBankRemittanceRate is cAgentBankRemittanceRate)
					AgentBankRemittanceRate = this;

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
				ContractType.SetVisibility();
				CreatedDate.SetVisibility();
				CurrencyCode.SetVisibility();
				AgentId.SetVisibility();
				Rate.SetVisibility();
				Amount.SetVisibility();
				Balance.SetVisibility();
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
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { AgentBankRemittanceRate, "" }); // DN
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
							return Page_Terminate("AgentBankRemittanceRatelist"); // No matching record, return to List page // DN
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
							if (ew_GetPageName(sReturnUrl) == "AgentBankRemittanceRatelist")
								sReturnUrl = AddMasterUrl(ListUrl); // List page, return to list page with correct master key if necessary
							else if (ew_GetPageName(sReturnUrl) == "AgentBankRemittanceRateview")
								sReturnUrl = ViewUrl; // View page, return to view page with key URL directly
							return Page_Terminate(sReturnUrl); // Clean up and return // DN
						} else {
							EventCancelled = true; // Event cancelled
							RestoreFormValues(); // Add failed, restore form values
						}
						break;
				}

				// Render row based on row type
				if (CurrentAction == "F") { // Confirm page
					RowType = EW_ROWTYPE_VIEW; // Render view type
				} else {
					RowType = EW_ROWTYPE_ADD; // Render add type
				}

				// Render row
				ResetAttrs();
				RenderRow();
				return ew_Controller.View();
			}

			// Confirm page
			public bool ConfirmPage = true;  // DN

			// Get upload files
			public void GetUploadFiles() {

				// Get upload data
			}

			// Load default values
			public void LoadDefaultValues() {
				ContractType.CurrentValue = System.DBNull.Value;
				ContractType.OldValue = ContractType.CurrentValue;
				CreatedDate.CurrentValue = System.DBNull.Value;
				CreatedDate.OldValue = CreatedDate.CurrentValue;
				CurrencyCode.CurrentValue = System.DBNull.Value;
				CurrencyCode.OldValue = CurrencyCode.CurrentValue;
				AgentId.CurrentValue = System.DBNull.Value;
				AgentId.OldValue = AgentId.CurrentValue;
				Rate.CurrentValue = System.DBNull.Value;
				Rate.OldValue = Rate.CurrentValue;
				Amount.CurrentValue = System.DBNull.Value;
				Amount.OldValue = Amount.CurrentValue;
				Balance.CurrentValue = Balance.FldDefault;
				CreatedBy.CurrentValue = System.DBNull.Value;
				CreatedBy.OldValue = CreatedBy.CurrentValue;
			}

			// Load form values
			public void LoadFormValues() {
				if (!ContractType.FldIsDetailKey) {
					ContractType.FormValue = ObjForm.GetValue("x_ContractType");
				}
				if (!CreatedDate.FldIsDetailKey) {
					CreatedDate.FormValue = ObjForm.GetValue("x_CreatedDate");
					CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 0);
				}
				if (!CurrencyCode.FldIsDetailKey) {
					CurrencyCode.FormValue = ObjForm.GetValue("x_CurrencyCode");
				}
				if (!AgentId.FldIsDetailKey) {
					AgentId.FormValue = ObjForm.GetValue("x_AgentId");
				}
				if (!Rate.FldIsDetailKey) {
					Rate.FormValue = ObjForm.GetValue("x_Rate");
				}
				if (!Amount.FldIsDetailKey) {
					Amount.FormValue = ObjForm.GetValue("x_Amount");
				}
				if (!Balance.FldIsDetailKey) {
					Balance.FormValue = ObjForm.GetValue("x_Balance");
				}
				if (!CreatedBy.FldIsDetailKey) {
					CreatedBy.FormValue = ObjForm.GetValue("x_CreatedBy");
				}
			}

			// Restore form values
			public void RestoreFormValues() {
				LoadOldRecord();
				ContractType.CurrentValue = ContractType.FormValue;
				CreatedDate.CurrentValue = CreatedDate.FormValue;
				CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 0);
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				AgentId.CurrentValue = AgentId.FormValue;
				Rate.CurrentValue = Rate.FormValue;
				Amount.CurrentValue = Amount.FormValue;
				Balance.CurrentValue = Balance.FormValue;
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
				ContractType.DbValue = row["ContractType"];
				CreatedDate.DbValue = row["CreatedDate"];
				CurrencyCode.DbValue = row["CurrencyCode"];
				AgentId.DbValue = row["AgentId"];
				Rate.DbValue = row["Rate"];
				Amount.DbValue = row["Amount"];
				Balance.DbValue = row["Balance"];
				CreatedBy.DbValue = row["CreatedBy"];
			}
			#pragma warning restore 162, 168

			// Load DbValue from recordset
			public void LoadDbValues(OrderedDictionary row) {
				if (row == null)
					return;
				id.SetDbValue(row["id"]);
				ContractType.SetDbValue(row["ContractType"]);
				CreatedDate.SetDbValue(row["CreatedDate"]);
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				AgentId.SetDbValue(row["AgentId"]);
				Rate.SetDbValue(row["Rate"]);
				Amount.SetDbValue(row["Amount"]);
				Balance.SetDbValue(row["Balance"]);
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
				if (ew_SameStr(Rate.FormValue, Rate.CurrentValue) && ew_IsNumeric(ew_StrToFloat(Rate.CurrentValue)))
					Rate.CurrentValue = ew_StrToFloat(Rate.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(Amount.FormValue, Amount.CurrentValue) && ew_IsNumeric(ew_StrToFloat(Amount.CurrentValue)))
					Amount.CurrentValue = ew_StrToFloat(Amount.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(Balance.FormValue, Balance.CurrentValue) && ew_IsNumeric(ew_StrToFloat(Balance.CurrentValue)))
					Balance.CurrentValue = ew_StrToFloat(Balance.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// id
				// ContractType
				// CreatedDate
				// CurrencyCode
				// AgentId
				// Rate
				// Amount
				// Balance
				// CreatedBy

				if (RowType == EW_ROWTYPE_VIEW) { // View row

					// id
					id.ViewValue = id.CurrentValue;

					// ContractType
					if (Convert.ToString(ContractType.CurrentValue) != "") {
							ContractType.ViewValue = ContractType.OptionCaption(Convert.ToString(ContractType.CurrentValue));
					} else {
						ContractType.ViewValue = System.DBNull.Value;
					}

					// CreatedDate
					CreatedDate.ViewValue = CreatedDate.CurrentValue;
					CreatedDate.ViewValue = ew_FormatDateTime(CreatedDate.ViewValue, 0);

					// CurrencyCode
					if (ew_NotEmpty(CurrencyCode.CurrentValue)) {
						sFilterWrk = "[CurrCode]" + ew_SearchString("=", Convert.ToString(CurrencyCode.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT DISTINCT [CurrCode], [CurrCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[vw_AgentFCBal]";
						sWhereWrk = "";
						CurrencyCode.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(CurrencyCode, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrCode] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							CurrencyCode.ViewValue = CurrencyCode.DisplayValue(odwrk);
						} else {
							CurrencyCode.ViewValue = CurrencyCode.CurrentValue;
						}
					} else {
						CurrencyCode.ViewValue = System.DBNull.Value;
					}

					// AgentId
					if (ew_NotEmpty(AgentId.CurrentValue)) {
						sFilterWrk = "[AgentId]" + ew_SearchString("=", Convert.ToString(AgentId.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT DISTINCT [AgentId], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld], [Balance] AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[vw_AgentFCBal]";
						sWhereWrk = "";
						AgentId.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(AgentId, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							odwrk[2] = Convert.ToString(odwrk[2]);
							odwrk[3] = Convert.ToString(odwrk[3]);
							AgentId.ViewValue = AgentId.DisplayValue(odwrk);
						} else {
							AgentId.ViewValue = AgentId.CurrentValue;
						}
					} else {
						AgentId.ViewValue = System.DBNull.Value;
					}

					// Rate
					Rate.ViewValue = Rate.CurrentValue;

					// Amount
					Amount.ViewValue = Amount.CurrentValue;

					// Balance
					Balance.ViewValue = Balance.CurrentValue;

					// CreatedBy
					CreatedBy.ViewValue = CreatedBy.CurrentValue;

					// ContractType
					ContractType.LinkCustomAttributes = ContractType.FldTagACustomAttributes; // DN
					ContractType.HrefValue = "";
					ContractType.TooltipValue = "";

					// CreatedDate
					CreatedDate.LinkCustomAttributes = CreatedDate.FldTagACustomAttributes; // DN
					CreatedDate.HrefValue = "";
					CreatedDate.TooltipValue = "";

					// CurrencyCode
					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// AgentId
					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";
					AgentId.TooltipValue = "";

					// Rate
					Rate.LinkCustomAttributes = Rate.FldTagACustomAttributes; // DN
					Rate.HrefValue = "";
					Rate.TooltipValue = "";

					// Amount
					Amount.LinkCustomAttributes = Amount.FldTagACustomAttributes; // DN
					Amount.HrefValue = "";
					Amount.TooltipValue = "";

					// Balance
					Balance.LinkCustomAttributes = Balance.FldTagACustomAttributes; // DN
					Balance.HrefValue = "";
					Balance.TooltipValue = "";

					// CreatedBy
					CreatedBy.LinkCustomAttributes = CreatedBy.FldTagACustomAttributes; // DN
					CreatedBy.HrefValue = "";
					CreatedBy.TooltipValue = "";
				} else if (RowType == EW_ROWTYPE_ADD) { // Add row

					// ContractType
					ContractType.EditValue = ContractType.Options(false);

					// CreatedDate
					CreatedDate.EditAttrs["class"] = "form-control";
					CreatedDate.EditValue = ew_FormatDateTime(CreatedDate.CurrentValue, 8); // DN
					CreatedDate.PlaceHolder = ew_RemoveHtml(CreatedDate.FldCaption);

					// CurrencyCode
					CurrencyCode.EditAttrs["class"] = "form-control";
						if (ew_Empty(CurrencyCode.CurrentValue)) {
							sFilterWrk = "0=1";
						} else {
							sFilterWrk = "[CurrCode]" + ew_SearchString("=", Convert.ToString(CurrencyCode.CurrentValue), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT DISTINCT [CurrCode], [CurrCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[vw_AgentFCBal]";
						sWhereWrk = "";
						CurrencyCode.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(CurrencyCode, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrCode] ASC/*EndOrderBy*/";
					rswrk = ew_GetConn("").GetRows(sSqlWrk);
					CurrencyCode.EditValue = rswrk;

					// AgentId
						if (ew_Empty(AgentId.CurrentValue)) {
							sFilterWrk = "0=1";
						} else {
							sFilterWrk = "[AgentId]" + ew_SearchString("=", Convert.ToString(AgentId.CurrentValue), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT DISTINCT [AgentId], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld], [Balance] AS [Disp3Fld], '' AS [Disp4Fld], [CurrCode] AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[vw_AgentFCBal]";
						sWhereWrk = "";
						AgentId.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(AgentId, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					rswrk = ew_GetConn("").GetRows(sSqlWrk);
					if (rswrk != null && rswrk.Count > 0) { // Lookup values found
						odwrk = rswrk[0];
						odwrk[1] = Convert.ToString(ew_HtmlEncode(odwrk[1]));
						odwrk[2] = Convert.ToString(ew_HtmlEncode(odwrk[2]));
						odwrk[3] = Convert.ToString(ew_HtmlEncode(odwrk[3]));
						AgentId.ViewValue = String.Concat(AgentId.ViewValue, AgentId.DisplayValue(odwrk));
						foreach (var od in rswrk) {
						}
					} else {
						AgentId.ViewValue = @Language.Phrase("PleaseSelect");
					}
					AgentId.EditValue = rswrk;

					// Rate
					Rate.EditAttrs["class"] = "form-control";
					Rate.EditValue = Rate.CurrentValue; // DN
					Rate.PlaceHolder = ew_RemoveHtml(Rate.FldCaption);
					if (ew_NotEmpty(Rate.EditValue) && ew_IsNumeric(Convert.ToString(Rate.EditValue))) Rate.EditValue = ew_FormatNumber(Rate.EditValue, -2, -1, -2, 0);

					// Amount
					Amount.EditAttrs["class"] = "form-control";
					Amount.EditValue = Amount.CurrentValue; // DN
					Amount.PlaceHolder = ew_RemoveHtml(Amount.FldCaption);
					if (ew_NotEmpty(Amount.EditValue) && ew_IsNumeric(Convert.ToString(Amount.EditValue))) Amount.EditValue = ew_FormatNumber(Amount.EditValue, -2, -1, -2, 0);

					// Balance
					Balance.EditAttrs["class"] = "form-control";
					Balance.EditValue = Balance.CurrentValue; // DN
					Balance.PlaceHolder = ew_RemoveHtml(Balance.FldCaption);
					if (ew_NotEmpty(Balance.EditValue) && ew_IsNumeric(Convert.ToString(Balance.EditValue))) Balance.EditValue = ew_FormatNumber(Balance.EditValue, -2, -1, -2, 0);

					// CreatedBy
					CreatedBy.EditAttrs["class"] = "form-control";
					CreatedBy.EditValue = CreatedBy.CurrentValue; // DN
					CreatedBy.PlaceHolder = ew_RemoveHtml(CreatedBy.FldCaption);

					// Add refer script
					// ContractType

					ContractType.LinkCustomAttributes = ContractType.FldTagACustomAttributes; // DN
					ContractType.HrefValue = "";

					// CreatedDate
					CreatedDate.LinkCustomAttributes = CreatedDate.FldTagACustomAttributes; // DN
					CreatedDate.HrefValue = "";

					// CurrencyCode
					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";

					// AgentId
					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";

					// Rate
					Rate.LinkCustomAttributes = Rate.FldTagACustomAttributes; // DN
					Rate.HrefValue = "";

					// Amount
					Amount.LinkCustomAttributes = Amount.FldTagACustomAttributes; // DN
					Amount.HrefValue = "";

					// Balance
					Balance.LinkCustomAttributes = Balance.FldTagACustomAttributes; // DN
					Balance.HrefValue = "";

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
				if (!CreatedDate.FldIsDetailKey && ew_Empty(CreatedDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreatedDate.ReqErrMsg.Replace("%s", CreatedDate.FldCaption));
				if (!ew_CheckDateDef(CreatedDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreatedDate.FldErrMsg);
				if (!CurrencyCode.FldIsDetailKey && ew_Empty(CurrencyCode.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CurrencyCode.ReqErrMsg.Replace("%s", CurrencyCode.FldCaption));
				if (!AgentId.FldIsDetailKey && ew_Empty(AgentId.FormValue))
					gsFormError = ew_AddMessage(gsFormError, AgentId.ReqErrMsg.Replace("%s", AgentId.FldCaption));
				if (!Rate.FldIsDetailKey && ew_Empty(Rate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Rate.ReqErrMsg.Replace("%s", Rate.FldCaption));
				if (!ew_CheckNumber(Rate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Rate.FldErrMsg);
				if (!Amount.FldIsDetailKey && ew_Empty(Amount.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Amount.ReqErrMsg.Replace("%s", Amount.FldCaption));
				if (!ew_CheckNumber(Amount.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Amount.FldErrMsg);
				if (!ew_CheckNumber(Balance.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Balance.FldErrMsg);

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

					// ContractType
					ContractType.SetDbValue(ref rsnew, ContractType.CurrentValue, System.DBNull.Value, false);

					// CreatedDate
					CreatedDate.SetDbValue(ref rsnew, ew_UnformatDateTime(CreatedDate.CurrentValue, 0), DateTime.Now, false);

					// CurrencyCode
					CurrencyCode.SetDbValue(ref rsnew, CurrencyCode.CurrentValue, "", false);

					// AgentId
					AgentId.SetDbValue(ref rsnew, AgentId.CurrentValue, "", false);

					// Rate
					Rate.SetDbValue(ref rsnew, Rate.CurrentValue, 0, false);

					// Amount
					Amount.SetDbValue(ref rsnew, Amount.CurrentValue, 0, false);

					// Balance
					Balance.SetDbValue(ref rsnew, Balance.CurrentValue, System.DBNull.Value, ew_Empty(Balance.CurrentValue));

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
				Breadcrumb.Add("list", TableVar, ew_AppPath(AddMasterUrl("AgentBankRemittanceRatelist")), "", TableVar, true);
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
							sSqlWrk = "SELECT DISTINCT [CurrCode] AS [LinkFld], [CurrCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[vw_AgentFCBal]";
							sWhereWrk = "";
							CurrencyCode.LookupFilters = new Dictionary<string, string>() {};
						CurrencyCode.LookupFilters.Add("s", sSqlWrk);
						CurrencyCode.LookupFilters.Add("d", "");
						CurrencyCode.LookupFilters.Add("f0", "[CurrCode] = {filter_value}");
						CurrencyCode.LookupFilters.Add("t0", "200");
						CurrencyCode.LookupFilters.Add("fn0", "");
						sSqlWrk = "";
						Lookup_Selecting(CurrencyCode, ref sWhereWrk);
							if (sWhereWrk != "") {
								sSqlWrk += " WHERE " + sWhereWrk;
							}
						sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrCode] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							CurrencyCode.LookupFilters["s"] += sSqlWrk;
						break;
					case "x_AgentId":
						sSqlWrk = "";
							sSqlWrk = "SELECT DISTINCT [AgentId] AS [LinkFld], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld], [Balance] AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[vw_AgentFCBal]";
							sWhereWrk = "{filter}";
							AgentId.LookupFilters = new Dictionary<string, string>() {};
						AgentId.LookupFilters.Add("s", sSqlWrk);
						AgentId.LookupFilters.Add("d", "");
						AgentId.LookupFilters.Add("f0", "[AgentId] = {filter_value}");
						AgentId.LookupFilters.Add("t0", "200");
						AgentId.LookupFilters.Add("fn0", "");
						AgentId.LookupFilters.Add("f1", "[CurrCode] IN ({filter_value})");
						AgentId.LookupFilters.Add("t1", "200");
						AgentId.LookupFilters.Add("fn1", "");
						sSqlWrk = "";
						Lookup_Selecting(AgentId, ref sWhereWrk);
							if (sWhereWrk != "") {
								sSqlWrk += " WHERE " + sWhereWrk;
							}
						if (sSqlWrk != "")
							AgentId.LookupFilters["s"] += sSqlWrk;
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
