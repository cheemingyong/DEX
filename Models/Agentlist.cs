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

		// Agent_list
		public static cAgent_list Agent_list {
			get { return (cAgent_list)ew_ViewData["Agent_list"]; }
			set { ew_ViewData["Agent_list"] = value; }
		}

		//
		// Page class for Agent
		//

		public class cAgent_list : cAgent_list_base
		{

			// Construtor
			public cAgent_list(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cAgent_list_base : cAgent, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "Agent";

			// Page object name
			public string PageObjName = "Agent_list";

			// Page terminated // DN
			private bool _terminated = false;

			// Grid form hidden field names
			public string FormName = "fAgentlist";
			public string FormActionName = "k_action";
			public string FormKeyName = "k_key";
			public string FormOldKeyName = "k_oldkey";
			public string FormBlankRowName = "k_blankrow";
			public string FormKeyCountName = "key_count";

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

			// Export URLs
			public string ExportPrintUrl = "";
			public string ExportHtmlUrl = "";
			public string ExportExcelUrl = "";
			public string ExportWordUrl = "";
			public string ExportXmlUrl = "";
			public string ExportCsvUrl = "";
			public string ExportPdfUrl = "";

			// Update URLs
			public string InlineAddUrl = "";
			public string GridAddUrl = "";
			public string GridEditUrl = "";
			public string MultiDeleteUrl = "";
			public string MultiUpdateUrl = "";

			// Custom export
			public bool ExportExcelCustom = false;
			public bool ExportWordCustom = false;
			public bool ExportPdfCustom = false; // Not supported // DN
			public bool ExportEmailCustom = false;

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

			public cAgent_list_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (Agent)
				if (Agent == null || Agent is cAgent)
					Agent = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				ExportPdfUrl = PageUrl + "export=pdf";
				AddUrl = "Agentadd?" + EW_TABLE_SHOW_DETAIL + "=";
				InlineAddUrl = PageUrl + "a=add";
				GridAddUrl = PageUrl + "a=gridadd";
				GridEditUrl = PageUrl + "a=gridedit";
				MultiDeleteUrl = "Agentdelete";
				MultiUpdateUrl = "Agentupdate";

				// Start time
				StartTime = Environment.TickCount;

				// Open connection
				Conn = Connection; // DN

				// List options
				ListOptions = new cListOptions();
				ListOptions.TableVar = TableVar;

				// Export options
				ExportOptions = new cListOptions();
				ExportOptions.Tag = "div";
				ExportOptions.TagClassName = "ewExportOption";

				// Other options
				OtherOptions["addedit"] = new cListOptions();
				OtherOptions["addedit"].Tag = "div";
				OtherOptions["addedit"].TagClassName = "ewAddEditOption";
				OtherOptions["detail"] = new cListOptions();
				OtherOptions["detail"].Tag = "div";
				OtherOptions["detail"].TagClassName = "ewDetailOption";
				OtherOptions["action"] = new cListOptions();
				OtherOptions["action"].Tag = "div";
				OtherOptions["action"].TagClassName = "ewActionOption";

				// Filter options
				FilterOptions = new cListOptions();
				FilterOptions.Tag = "div";
				FilterOptions.TagClassName = "ewFilterOption fAgentlistsrch";

				// List actions
				ListActions = new cListActions();
			}

			//
			// Page_Init
			//

			public IActionResult Page_Init() {

				// Header
				ew_Header(EW_CACHE);

				// Create form object
				ObjForm = new cFormObj();

				// Get export parameters
				string custom = "";
				if (ew_NotEmpty(ew_Get("export"))) {
					Export = ew_Get("export");
					custom = ew_Get("custom");
				} else if (ew_NotEmpty(ew_Post("export"))) {
					Export = ew_Post("export");
					custom = ew_Post("custom");
				} else if (IsPost) {
					if (ew_NotEmpty(ew_Post("exporttype")))
						Export = ew_Post("exporttype");
					custom = ew_Post("custom");
				} else {
					ExportReturnUrl = ew_CurrentUrl();
				}
				gsExportFile = TableVar; // Get export file, used in header

				// Get custom export parameters
				if (Export != "" && custom != "") {
					CustomExport = Export;
					Export = "print";
				}
				gsCustomExport = CustomExport;
				gsExport = Export; // Get export parameter, used in header

				// Update Export URLs
				if (ExportExcelCustom)
					ExportExcelUrl += "&amp;custom=1";
				if (ExportWordCustom)
					ExportWordUrl += "&amp;custom=1";
				if (ExportPdfCustom)
					ExportPdfUrl += "&amp;custom=1";
				CurrentAction = (ew_Get("a") != "") ? ew_Get("a") : ew_Post("a_list"); // Set up current action

				// Get grid add count
				int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
				if (gridaddcnt > 0)
					GridAddRowCount = gridaddcnt;

				// Set up list options
				SetupListOptions();

				// Setup export options
				SetupExportOptions();
				AgentId.SetVisibility();
				AgentName.SetVisibility();
				AgentRiskRating.SetVisibility();
				AgentRiskCredit.SetVisibility();
				Address1.SetVisibility();
				Address2.SetVisibility();
				Address3.SetVisibility();
				Country.SetVisibility();
				ZipCode.SetVisibility();
				Fax.SetVisibility();
				Phone.SetVisibility();
				Mobile.SetVisibility();
				BuzType.SetVisibility();
				ClassType.SetVisibility();
				DefContactPName.SetVisibility();
				DefContactPNric.SetVisibility();
				DefContactPNation.SetVisibility();
				DefContactPOccupation.SetVisibility();
				TermsId.SetVisibility();
				LedgerBal.SetVisibility();
				AvailableBal.SetVisibility();
				_Email.SetVisibility();
				URL.SetVisibility();
				CustType.SetVisibility();
				RemittanceLicNO.SetVisibility();
				MCLicNo.SetVisibility();
				BankYesNo.SetVisibility();
				BankODLimit.SetVisibility();
				BankAcctNO.SetVisibility();
				CreditLimit.SetVisibility();
				ReferBy.SetVisibility();
				AgentImageName.SetVisibility();
				status.SetVisibility();
				CreatedBy.SetVisibility();
				CreatedDate.SetVisibility();
				ModifiedUser.SetVisibility();
				ModifiedDate.SetVisibility();
				PPExpiryDate.SetVisibility();
				TTExpiryDate.SetVisibility();
				MCExpiryDate.SetVisibility();
				Action.SetVisibility();
				Remark.SetVisibility();
				MCType.SetVisibility();
				CustDOB.SetVisibility();
				DefContactDOB.SetVisibility();
				ScanImage.SetVisibility();
				BizNature.SetVisibility();
				DefContactPOB.SetVisibility();
				NewTran.SetVisibility();
				BizRegNo.SetVisibility();
				BizRegDate.SetVisibility();
				BizRegPlace.SetVisibility();
				BizRegExpDate.SetVisibility();
				UnIncorpExec.SetVisibility();
				DefContactAuthorzLetter.SetVisibility();
				Politician.SetVisibility();
				BizPartnerNo.SetVisibility();
				Remark2.SetVisibility();
				BannedListRemark.SetVisibility();

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

					// Process auto fill for detail table 'AgentBank'
					if (ew_Post("grid") == "fAgentBankgrid") {
						AgentBank_grid = AgentBank_grid ?? new cAgentBank_grid();
						return AgentBank_grid.Page_Init();
					}
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

				// Setup other options
				SetupOtherOptions();

				// Set up custom action (compatible with old version)
				foreach (KeyValuePair<string, string> kvp in CustomActions)
					ListActions.Add(kvp.Key, kvp.Value);

				// Show checkbox column if multiple action
				foreach (KeyValuePair<string, cListAction> kvp in ListActions.Items) {
					if (kvp.Value.Select == EW_ACTION_MULTIPLE && kvp.Value.Allow) {
						ListOptions["checkbox"].Visible = true;
						break;
					}
				}
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
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { Agent, "" }); // DN
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

			// Class properties
			public cListOptions ListOptions; // List options
			public cListOptions ExportOptions; // Export options
			public cListOptions SearchOptions; // Search options
			public Dictionary<string, cListOptions> OtherOptions = new Dictionary<string, cListOptions>(); // Other options
			public cListOptions FilterOptions; // Filter options
			public cListActions ListActions; // List actions
			public int SelectedCount = 0;
			public int SelectedIndex = 0;
			public int DisplayRecs = 20; // Number of display records
			public int StartRec;
			public int StopRec;
			public int TotalRecs = -1;
			public int RecRange = 10;
			public dynamic Pager;
			public bool AutoHidePager = EW_AUTO_HIDE_PAGER;
			public bool AutoHidePageSizeSelector = EW_AUTO_HIDE_PAGE_SIZE_SELECTOR;
			public string DefaultSearchWhere = ""; // Default search WHERE clause
			public string SearchWhere = ""; // Search WHERE clause
			public int RecCnt = 0; // Record count
			public int EditRowCnt;
			public int StartRowCnt = 1;

			//public int RowCnt = 0; // DN
			public Dictionary<int, dynamic> Attrs = new Dictionary<int, dynamic>(); // Row attributes and cell attributes
			public object RowIndex = 0; // Row index
			public int KeyCount = 0; // Key count
			public string RowAction = ""; // Row action
			public string RowOldKey = ""; // Row old key (for copy)
			public int RecPerRow = 0;
			public string MultiColumnClass;
			public string MultiColumnEditClass = "col-sm-12";
			public int MultiColumnCnt = 12;
			public int MultiColumnEditCnt = 12;
			public int GridCnt = 0;
			public int ColCnt = 0;
			public string DbMasterFilter = ""; // Master filter
			public string DbDetailFilter = ""; // Detail filter
			public bool MasterRecordExists;
			public string MultiSelectKey = "";
			public bool RestoreSearch = false;
			public int Priv = 0;
			public cSubPages DetailPages;
			public DbDataReader Recordset;
			public DbDataReader OldRecordset;

			//
			// Page main
			//

			public IActionResult Page_Main() {

				// Search filters
				string sSrchAdvanced = ""; // Advanced search filter
				string sFilter = "";

				// Get command
				Command = ew_Get("cmd").ToLower();
				if (IsPageRequest) { // Validate request

					// Process list action first
					var actionresult = ProcessListAction();
					if (ew_NotEmpty(actionresult)) { // Ajax request

						// Clean output buffer
						if (!EW_DEBUG_ENABLED)
							ew_Response.Clear();
						return ew_Controller.Content(actionresult, "text/plain", Encoding.UTF8);
					}

					// Set up records per page
					SetUpDisplayRecs();

					// Handle reset command
					ResetCmd();

					// Set up Breadcrumb
					if (ew_Empty(Export))
						SetupBreadcrumb();
					if (!IsPost) {

						// Check QueryString parameters
						if (ew_NotEmpty(ew_Get("a"))) {
							CurrentAction = ew_Get("a");

							// Clear inline mode
							if (CurrentAction == "cancel")
								ClearInlineMode();

							// Switch to grid edit mode
							if (CurrentAction == "gridedit")
								GridEditMode();

							// Switch to grid add mode
							if (CurrentAction == "gridadd")
								GridAddMode();
						}
					} else {
						if (ew_NotEmpty(ew_Post("a_list"))) {
							CurrentAction = ew_Post("a_list"); // Get action
							var bGridUpdate = false;

							// Grid Update
							if ((CurrentAction == "gridupdate" || CurrentAction == "gridoverwrite") && ew_SameStr(ew_Session[EW_SESSION_INLINE_MODE], "gridedit")) {
								if (ValidateGridForm()) {
									bGridUpdate = GridUpdate();
								} else {
									bGridUpdate = false;
									FailureMessage = gsFormError;
								}
								if (!bGridUpdate) {
									EventCancelled = true;
									CurrentAction = "gridedit"; // Stay in Grid Edit mode
								}
							}

							// Grid Insert
							var bGridInsert = false;
							if (CurrentAction == "gridinsert" && ew_SameStr(ew_Session[EW_SESSION_INLINE_MODE], "gridadd")) {
								if (ValidateGridForm()) {
									bGridInsert = GridInsert();
								} else {
									bGridInsert = false;
									FailureMessage = gsFormError;
								}
								if (!bGridInsert) {
									EventCancelled = true;
									CurrentAction = "gridadd"; // Stay in Grid Add mode
								}
							}
						}
					}

					// Hide list options
					if (ew_NotEmpty(Export)) {
						ListOptions.HideAllOptions(new List<string>() {"sequence"});
						ListOptions.UseDropDownButton = false; // Disable drop down button
						ListOptions.UseButtonGroup = false; // Disable button group
					} else if (CurrentAction == "gridadd" || CurrentAction == "gridedit") {
						ListOptions.HideAllOptions();
						ListOptions.UseDropDownButton = false; // Disable drop down button
						ListOptions.UseButtonGroup = false; // Disable button group
					}

					// Hide options
					if (ew_NotEmpty(Export) || ew_NotEmpty(CurrentAction)) {
						ExportOptions.HideAllOptions();
						FilterOptions.HideAllOptions();
					}

					// Hide other options
					if (ew_NotEmpty(Export)) {
						foreach (var kvp in OtherOptions)
							kvp.Value.HideAllOptions();
					}

					// Show grid delete link for grid add / grid edit
					if (AllowAddDeleteRow) {
						if (CurrentAction == "gridadd" || CurrentAction == "gridedit") {
							var item = ListOptions["griddelete"];
							if (item != null) item.Visible = true;
						}
					}

					// Get default search criteria
					ew_AddFilter(ref DefaultSearchWhere, AdvancedSearchWhere(true));

					// Get and validate search values for advanced search
					LoadSearchValues(); // Get search values
					if (!ValidateSearch())
						FailureMessage = gsSearchError;

					// Restore search parms from Session if not searching / reset / export
					if ((Export != "" || Command != "search" && Command != "reset" && Command != "resetall") && CheckSearchParms())
						RestoreSearchParms();

					// Call Recordset SearchValidated event
					Recordset_SearchValidated();

					// Set up sorting order
					SetUpSortOrder();

					// Get search criteria for advanced search
					if (ew_Empty(gsSearchError))
						sSrchAdvanced = AdvancedSearchWhere();
				}

				// Restore display records
				if (RecordsPerPage == -1 || RecordsPerPage > 0) {
					DisplayRecs = RecordsPerPage; // Restore from Session
				} else {
					DisplayRecs = 20; // Load default
				}

				// Load Sorting Order
				LoadSortOrder();

				// Load search default if no existing search criteria
				if (!CheckSearchParms()) {

					// Load advanced search from default
					if (LoadAdvancedSearchDefault())
						sSrchAdvanced = AdvancedSearchWhere();
				}

				// Build search criteria
				ew_AddFilter(ref SearchWhere, sSrchAdvanced);
				if (ew_NotEmpty(sSrchAdvanced) && ew_Empty(ew_Get(EW_TABLE_BASIC_SEARCH))) { //DN
				} //DN

				// Call Recordset_Searching event
				Recordset_Searching(ref SearchWhere);

				// Save search criteria
				if (Command == "search" && !RestoreSearch) {
					SessionSearchWhere = SearchWhere; // Save to Session // *** rename as SessionSearchWhere property
					StartRec = 1; // Reset start record counter
					StartRecordNumber = StartRec;
				} else {
					SearchWhere = SessionSearchWhere;
				}

				// Build filter
				sFilter = "";
				ew_AddFilter(ref sFilter, DbDetailFilter);
				ew_AddFilter(ref sFilter, SearchWhere);

				// Set up filter in session
				SessionWhere = sFilter;
				CurrentFilter = "";

				// Export data only
				if (CustomExport == "" && (new List<string>() {"html","word","excel","xml","csv","email","pdf"}).Contains(Export)) {
					ExportData();
					return Page_Terminate(); // DN
				}

				// Load record count first
				if (!IsAddOrEdit) // DN
					TotalRecs = SelectRecordCount();

				// Search options
				SetupSearchOptions();
				return ew_Controller.View();
			}

			// Set up number of records displayed per page
			public void SetUpDisplayRecs() {
				string sWrk = ew_Get(EW_TABLE_REC_PER_PAGE);
				if (ew_NotEmpty(sWrk)) {
					if (ew_IsNumeric(sWrk)) {
						DisplayRecs = ew_ConvertToInt(sWrk);
					} else {
						if (ew_SameText(sWrk, "all")) {	// Display all records
							DisplayRecs = -1;
						} else {
							DisplayRecs = 20;	// Non-numeric, load default
						}
					}
					RecordsPerPage = DisplayRecs;	// Save to Session

					// Reset start position
					StartRec = 1;
					StartRecordNumber = StartRec;
				}
			}

			// Exit inline mode
			public void ClearInlineMode() {
				AgentRiskCredit.SetFormValue("", false); // Clear form value
				LedgerBal.SetFormValue("", false); // Clear form value
				AvailableBal.SetFormValue("", false); // Clear form value
				BankODLimit.SetFormValue("", false); // Clear form value
				CreditLimit.SetFormValue("", false); // Clear form value
				LastAction = CurrentAction; // Save last action
				CurrentAction = ""; // Clear action
				ew_Session[EW_SESSION_INLINE_MODE] = ""; // Clear inline mode
			}

			// Switch to Grid Add Mode
			public void GridAddMode() {
				ew_Session[EW_SESSION_INLINE_MODE] = "gridadd"; // Enabled grid add
			}

			// Switch to Grid Edit Mode
			public void GridEditMode() {
				ew_Session[EW_SESSION_INLINE_MODE] = "gridedit"; // Enabled grid edit
			}

			// Perform update to grid
			public bool GridUpdate() {
				bool bGridUpdate = true;

				// Get old recordset
				CurrentFilter = BuildKeyFilter();
				if (ew_Empty(CurrentFilter))
					CurrentFilter = "0=1";
				string sSql = SQL;
				List<OrderedDictionary> rsold = Connection.GetRows(sSql);

				// Call Grid Updating event
				if (!Grid_Updating(rsold)) {
					if (ew_Empty(FailureMessage))
						FailureMessage = Language.Phrase("GridEditCancelled"); // Set grid edit cancelled message
					return false;
				}

				// Begin transaction
				Connection.BeginTrans();
				string sKey = "";

				// Update row index and get row key
				ObjForm.Index = -1;
				var rowcnt = ew_ConvertToInt(ObjForm.GetValue(FormKeyCountName));
				if (ew_Empty(rowcnt) || !ew_IsNumeric(rowcnt))
					rowcnt = 0;

				// Update all rows based on key
				try {
					for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
						ObjForm.Index = rowindex;
						string rowkey = ObjForm.GetValue(FormKeyName);
						string rowaction = ObjForm.GetValue(FormActionName);

						// Load all values and keys
						if (rowaction != "insertdelete") { // Skip insert then deleted rows
							LoadFormValues(); // Get form values
							if (ew_Empty(rowaction) || rowaction == "edit" || rowaction == "delete") {
								bGridUpdate = SetupKeyValues(rowkey); // Set up key values
							} else {
								bGridUpdate = true;
							}

							// Skip empty row
							if (rowaction == "insert" && EmptyRow()) {

								// No action required
							// Validate form and insert/update/delete record

							} else if (bGridUpdate) {
								if (rowaction == "delete") {
									CurrentFilter = KeyFilter;
									bGridUpdate = DeleteRows(); // Delete this row
								} else if (!ValidateForm()) {
									bGridUpdate = false; // Form error, reset action
									FailureMessage = gsFormError;
								} else {
									if (rowaction == "insert") {
										bGridUpdate = AddRow(); // Insert this row
									} else {
										if (ew_NotEmpty(rowkey)) {
											SendEmail = false; // Do not send email on update success
											bGridUpdate = EditRow(); // Update this row
										}
									} // End update
								}
							}
							if (bGridUpdate) {
								if (ew_NotEmpty(sKey)) sKey += ", ";
								sKey += rowkey;
							} else {
								break;
							}
						}
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					bGridUpdate = false;
				}
				if (bGridUpdate) {
					Connection.CommitTrans(); // Commit transaction

					// Get new recordset
					List<OrderedDictionary> rsnew = Connection.GetRows(sSql, true); // Use main connection (faster) // DN

				// Call Grid_Updated event
				Grid_Updated(rsold, rsnew);
					if (ew_Empty(SuccessMessage))
						SuccessMessage = Language.Phrase("UpdateSuccess"); // Set up update success message
					ClearInlineMode(); // Clear inline edit mode
				} else {
					Connection.RollbackTrans(); // Rollback transaction
					if (ew_Empty(FailureMessage))
						FailureMessage = Language.Phrase("UpdateFailed"); // Set update failed message
				}
				return bGridUpdate;
			}

			// Build filter for all keys
			public string BuildKeyFilter() {
				string sWrkFilter = "";

				// Update row index and get row key
				int rowindex = 1;
				ObjForm.Index = rowindex;
				string sThisKey = ObjForm.GetValue(FormKeyName);
				while (ew_NotEmpty(sThisKey)) {
					if (SetupKeyValues(sThisKey)) {
						string sFilter = KeyFilter;
						if (ew_NotEmpty(sWrkFilter)) sWrkFilter += " OR ";
						sWrkFilter += sFilter;
					} else {
						sWrkFilter = "0=1";
						break;
					}

					// Update row index and get row key
					rowindex++; // next row
					ObjForm.Index = rowindex;
					sThisKey = ObjForm.GetValue(FormKeyName);
				}
				return sWrkFilter;
			}

			// Set up key values
			public bool SetupKeyValues(string key) {
				var arKeyFlds = key.Split(Convert.ToChar(EW_COMPOSITE_KEY_SEPARATOR));
				if (arKeyFlds.Length >= 1) {
					Agent.AgentId.FormValue = arKeyFlds[0];
				}
				return true;
			}

			// Perform Grid Add
			#pragma warning disable 168, 219
			public bool GridInsert() {
				int addcnt = 0;
				bool bGridInsert = false;

				// Call Grid Inserting event
				if (!Grid_Inserting()) {
					if (ew_Empty(FailureMessage)) {
						FailureMessage = Language.Phrase("GridAddCancelled"); // Set grid add cancelled message
					}
				return false;
				}

				// Begin transaction
				Connection.BeginTrans();

				// Init key filter
				string sWrkFilter = "";
				string sKey = "";

				// Get row count
				ObjForm.Index = -1;
				int rowcnt = ew_ConvertToInt(ObjForm.GetValue(FormKeyCountName));

				// Insert all rows
				try {
					for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {

						// Load current row values
						ObjForm.Index = rowindex;
						string rowaction = ObjForm.GetValue(FormActionName);
						if (ew_NotEmpty(rowaction) && rowaction != "insert")
							continue; // Skip
						LoadFormValues(); // Get form values
						if (!EmptyRow()) {
							addcnt++;
							SendEmail = false; // Do not send email on insert success

							// Validate form
							if (!ValidateForm()) {
								bGridInsert = false; // Form error, reset action
								FailureMessage = gsFormError;
							} else {
								bGridInsert = AddRow(Connection.GetRow(OldRecordset)); // Insert this row
							}
							if (bGridInsert) {
								if (ew_NotEmpty(sKey))
									sKey += EW_COMPOSITE_KEY_SEPARATOR;
								sKey += AgentId.CurrentValue;

								// Add filter for this record
								string sFilter = KeyFilter;
								if (ew_NotEmpty(sWrkFilter)) sWrkFilter += " OR ";
								sWrkFilter += sFilter;
							} else {
								break;
							}
						}
					}
					if (addcnt == 0) { // No record inserted
						FailureMessage = Language.Phrase("NoAddRecord");
						bGridInsert = false;
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					bGridInsert = false;
				}
				if (bGridInsert) {
					Connection.CommitTrans(); // Commit transaction

					// Get new recordset
					CurrentFilter = sWrkFilter;
					string sSql = SQL;
					List<OrderedDictionary> rsnew = Connection.GetRows(sSql, true); // Use main connection (faster) // DN

					// Call Grid_Inserted event
					Grid_Inserted(rsnew);
					if (ew_Empty(SuccessMessage))
						SuccessMessage = Language.Phrase("InsertSuccess"); // Set up insert success message
					ClearInlineMode(); // Clear grid add mode
				} else {
					Connection.RollbackTrans(); // Rollback transaction
					if (ew_Empty(FailureMessage))
						FailureMessage = Language.Phrase("InsertFailed"); // Set insert failed message
				}
				return bGridInsert;
			}
			#pragma warning restore 168, 219

			// Check if empty row
			public bool EmptyRow() {
				if (ObjForm.HasValue("x_AgentId") && ObjForm.HasValue("o_AgentId") && !ew_SameStr(AgentId.CurrentValue, AgentId.OldValue))
					return false;
				if (ObjForm.HasValue("x_AgentName") && ObjForm.HasValue("o_AgentName") && !ew_SameStr(AgentName.CurrentValue, AgentName.OldValue))
					return false;
				if (ObjForm.HasValue("x_AgentRiskRating") && ObjForm.HasValue("o_AgentRiskRating") && !ew_SameStr(AgentRiskRating.CurrentValue, AgentRiskRating.OldValue))
					return false;
				if (ObjForm.HasValue("x_AgentRiskCredit") && ObjForm.HasValue("o_AgentRiskCredit") && !ew_SameStr(AgentRiskCredit.CurrentValue, AgentRiskCredit.OldValue))
					return false;
				if (ObjForm.HasValue("x_Address1") && ObjForm.HasValue("o_Address1") && !ew_SameStr(Address1.CurrentValue, Address1.OldValue))
					return false;
				if (ObjForm.HasValue("x_Address2") && ObjForm.HasValue("o_Address2") && !ew_SameStr(Address2.CurrentValue, Address2.OldValue))
					return false;
				if (ObjForm.HasValue("x_Address3") && ObjForm.HasValue("o_Address3") && !ew_SameStr(Address3.CurrentValue, Address3.OldValue))
					return false;
				if (ObjForm.HasValue("x_Country") && ObjForm.HasValue("o_Country") && !ew_SameStr(Country.CurrentValue, Country.OldValue))
					return false;
				if (ObjForm.HasValue("x_ZipCode") && ObjForm.HasValue("o_ZipCode") && !ew_SameStr(ZipCode.CurrentValue, ZipCode.OldValue))
					return false;
				if (ObjForm.HasValue("x_Fax") && ObjForm.HasValue("o_Fax") && !ew_SameStr(Fax.CurrentValue, Fax.OldValue))
					return false;
				if (ObjForm.HasValue("x_Phone") && ObjForm.HasValue("o_Phone") && !ew_SameStr(Phone.CurrentValue, Phone.OldValue))
					return false;
				if (ObjForm.HasValue("x_Mobile") && ObjForm.HasValue("o_Mobile") && !ew_SameStr(Mobile.CurrentValue, Mobile.OldValue))
					return false;
				if (ObjForm.HasValue("x_BuzType") && ObjForm.HasValue("o_BuzType") && !ew_SameStr(BuzType.CurrentValue, BuzType.OldValue))
					return false;
				if (ObjForm.HasValue("x_ClassType") && ObjForm.HasValue("o_ClassType") && !ew_SameStr(ClassType.CurrentValue, ClassType.OldValue))
					return false;
				if (ObjForm.HasValue("x_DefContactPName") && ObjForm.HasValue("o_DefContactPName") && !ew_SameStr(DefContactPName.CurrentValue, DefContactPName.OldValue))
					return false;
				if (ObjForm.HasValue("x_DefContactPNric") && ObjForm.HasValue("o_DefContactPNric") && !ew_SameStr(DefContactPNric.CurrentValue, DefContactPNric.OldValue))
					return false;
				if (ObjForm.HasValue("x_DefContactPNation") && ObjForm.HasValue("o_DefContactPNation") && !ew_SameStr(DefContactPNation.CurrentValue, DefContactPNation.OldValue))
					return false;
				if (ObjForm.HasValue("x_DefContactPOccupation") && ObjForm.HasValue("o_DefContactPOccupation") && !ew_SameStr(DefContactPOccupation.CurrentValue, DefContactPOccupation.OldValue))
					return false;
				if (ObjForm.HasValue("x_TermsId") && ObjForm.HasValue("o_TermsId") && !ew_SameStr(TermsId.CurrentValue, TermsId.OldValue))
					return false;
				if (ObjForm.HasValue("x_LedgerBal") && ObjForm.HasValue("o_LedgerBal") && !ew_SameStr(LedgerBal.CurrentValue, LedgerBal.OldValue))
					return false;
				if (ObjForm.HasValue("x_AvailableBal") && ObjForm.HasValue("o_AvailableBal") && !ew_SameStr(AvailableBal.CurrentValue, AvailableBal.OldValue))
					return false;
				if (ObjForm.HasValue("x__Email") && ObjForm.HasValue("o__Email") && !ew_SameStr(_Email.CurrentValue, _Email.OldValue))
					return false;
				if (ObjForm.HasValue("x_URL") && ObjForm.HasValue("o_URL") && !ew_SameStr(URL.CurrentValue, URL.OldValue))
					return false;
				if (ObjForm.HasValue("x_CustType") && ObjForm.HasValue("o_CustType") && !ew_SameStr(CustType.CurrentValue, CustType.OldValue))
					return false;
				if (ObjForm.HasValue("x_RemittanceLicNO") && ObjForm.HasValue("o_RemittanceLicNO") && !ew_SameStr(RemittanceLicNO.CurrentValue, RemittanceLicNO.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCLicNo") && ObjForm.HasValue("o_MCLicNo") && !ew_SameStr(MCLicNo.CurrentValue, MCLicNo.OldValue))
					return false;
				if (ObjForm.HasValue("x_BankYesNo") && ObjForm.HasValue("o_BankYesNo") && ew_ConvertToBool(BankYesNo.CurrentValue) != ew_ConvertToBool(BankYesNo.OldValue))
					return false;
				if (ObjForm.HasValue("x_BankODLimit") && ObjForm.HasValue("o_BankODLimit") && !ew_SameStr(BankODLimit.CurrentValue, BankODLimit.OldValue))
					return false;
				if (ObjForm.HasValue("x_BankAcctNO") && ObjForm.HasValue("o_BankAcctNO") && !ew_SameStr(BankAcctNO.CurrentValue, BankAcctNO.OldValue))
					return false;
				if (ObjForm.HasValue("x_CreditLimit") && ObjForm.HasValue("o_CreditLimit") && !ew_SameStr(CreditLimit.CurrentValue, CreditLimit.OldValue))
					return false;
				if (ObjForm.HasValue("x_ReferBy") && ObjForm.HasValue("o_ReferBy") && !ew_SameStr(ReferBy.CurrentValue, ReferBy.OldValue))
					return false;
				if (ObjForm.HasValue("x_AgentImageName") && ObjForm.HasValue("o_AgentImageName") && !ew_SameStr(AgentImageName.CurrentValue, AgentImageName.OldValue))
					return false;
				if (ObjForm.HasValue("x_status") && ObjForm.HasValue("o_status") && !ew_SameStr(status.CurrentValue, status.OldValue))
					return false;
				if (ObjForm.HasValue("x_CreatedBy") && ObjForm.HasValue("o_CreatedBy") && !ew_SameStr(CreatedBy.CurrentValue, CreatedBy.OldValue))
					return false;
				if (ObjForm.HasValue("x_CreatedDate") && ObjForm.HasValue("o_CreatedDate") && !ew_SameStr(ew_FormatDateTime(CreatedDate.CurrentValue, 0), ew_FormatDateTime(CreatedDate.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_ModifiedUser") && ObjForm.HasValue("o_ModifiedUser") && !ew_SameStr(ModifiedUser.CurrentValue, ModifiedUser.OldValue))
					return false;
				if (ObjForm.HasValue("x_ModifiedDate") && ObjForm.HasValue("o_ModifiedDate") && !ew_SameStr(ew_FormatDateTime(ModifiedDate.CurrentValue, 0), ew_FormatDateTime(ModifiedDate.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_PPExpiryDate") && ObjForm.HasValue("o_PPExpiryDate") && !ew_SameStr(ew_FormatDateTime(PPExpiryDate.CurrentValue, 0), ew_FormatDateTime(PPExpiryDate.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_TTExpiryDate") && ObjForm.HasValue("o_TTExpiryDate") && !ew_SameStr(ew_FormatDateTime(TTExpiryDate.CurrentValue, 0), ew_FormatDateTime(TTExpiryDate.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_MCExpiryDate") && ObjForm.HasValue("o_MCExpiryDate") && !ew_SameStr(ew_FormatDateTime(MCExpiryDate.CurrentValue, 0), ew_FormatDateTime(MCExpiryDate.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_Action") && ObjForm.HasValue("o_Action") && !ew_SameStr(Action.CurrentValue, Action.OldValue))
					return false;
				if (ObjForm.HasValue("x_Remark") && ObjForm.HasValue("o_Remark") && !ew_SameStr(Remark.CurrentValue, Remark.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCType") && ObjForm.HasValue("o_MCType") && !ew_SameStr(MCType.CurrentValue, MCType.OldValue))
					return false;
				if (ObjForm.HasValue("x_CustDOB") && ObjForm.HasValue("o_CustDOB") && !ew_SameStr(ew_FormatDateTime(CustDOB.CurrentValue, 0), ew_FormatDateTime(CustDOB.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_DefContactDOB") && ObjForm.HasValue("o_DefContactDOB") && !ew_SameStr(ew_FormatDateTime(DefContactDOB.CurrentValue, 0), ew_FormatDateTime(DefContactDOB.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_ScanImage") && ObjForm.HasValue("o_ScanImage") && !ew_SameStr(ScanImage.CurrentValue, ScanImage.OldValue))
					return false;
				if (ObjForm.HasValue("x_BizNature") && ObjForm.HasValue("o_BizNature") && !ew_SameStr(BizNature.CurrentValue, BizNature.OldValue))
					return false;
				if (ObjForm.HasValue("x_DefContactPOB") && ObjForm.HasValue("o_DefContactPOB") && !ew_SameStr(DefContactPOB.CurrentValue, DefContactPOB.OldValue))
					return false;
				if (ObjForm.HasValue("x_NewTran") && ObjForm.HasValue("o_NewTran") && !ew_SameStr(NewTran.CurrentValue, NewTran.OldValue))
					return false;
				if (ObjForm.HasValue("x_BizRegNo") && ObjForm.HasValue("o_BizRegNo") && !ew_SameStr(BizRegNo.CurrentValue, BizRegNo.OldValue))
					return false;
				if (ObjForm.HasValue("x_BizRegDate") && ObjForm.HasValue("o_BizRegDate") && !ew_SameStr(ew_FormatDateTime(BizRegDate.CurrentValue, 0), ew_FormatDateTime(BizRegDate.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_BizRegPlace") && ObjForm.HasValue("o_BizRegPlace") && !ew_SameStr(BizRegPlace.CurrentValue, BizRegPlace.OldValue))
					return false;
				if (ObjForm.HasValue("x_BizRegExpDate") && ObjForm.HasValue("o_BizRegExpDate") && !ew_SameStr(ew_FormatDateTime(BizRegExpDate.CurrentValue, 0), ew_FormatDateTime(BizRegExpDate.OldValue, 0)))
					return false;
				if (ObjForm.HasValue("x_UnIncorpExec") && ObjForm.HasValue("o_UnIncorpExec") && !ew_SameStr(UnIncorpExec.CurrentValue, UnIncorpExec.OldValue))
					return false;
				if (ObjForm.HasValue("x_DefContactAuthorzLetter") && ObjForm.HasValue("o_DefContactAuthorzLetter") && !ew_SameStr(DefContactAuthorzLetter.CurrentValue, DefContactAuthorzLetter.OldValue))
					return false;
				if (ObjForm.HasValue("x_Politician") && ObjForm.HasValue("o_Politician") && !ew_SameStr(Politician.CurrentValue, Politician.OldValue))
					return false;
				if (ObjForm.HasValue("x_BizPartnerNo") && ObjForm.HasValue("o_BizPartnerNo") && !ew_SameStr(BizPartnerNo.CurrentValue, BizPartnerNo.OldValue))
					return false;
				if (ObjForm.HasValue("x_Remark2") && ObjForm.HasValue("o_Remark2") && !ew_SameStr(Remark2.CurrentValue, Remark2.OldValue))
					return false;
				if (ObjForm.HasValue("x_BannedListRemark") && ObjForm.HasValue("o_BannedListRemark") && !ew_SameStr(BannedListRemark.CurrentValue, BannedListRemark.OldValue))
					return false;
				return true;
			}

			// Validate grid form
			public bool ValidateGridForm() {

				// Get row count
				ObjForm.Index = -1;
				int rowcnt = ew_ConvertToInt(ObjForm.GetValue(FormKeyCountName));

				// Validate all records
				for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {

					// Load current row values
					ObjForm.Index = rowindex;
					string rowaction = ObjForm.GetValue(FormActionName);
					if (rowaction != "delete" && rowaction != "insertdelete") {
						LoadFormValues(); // Get form values
						if (rowaction == "insert" && EmptyRow()) {

							// Ignore
						} else if (!ValidateForm()) {
							return false;
						}
					}
				}
				return true;
			}

			// Get all form values of the grid
			public List<OrderedDictionary> GetGridFormValues() {

				// Get row count
				ObjForm.Index = -1;
				int rowcnt = ew_ConvertToInt(ObjForm.GetValue(FormKeyCountName));
				if (ew_Empty(rowcnt) || !ew_IsNumeric(rowcnt))
					rowcnt = 0;
				var rows = new List<OrderedDictionary>();

				// Loop through all records
				for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {

					// Load current row values
					ObjForm.Index = rowindex;
					string rowaction = ObjForm.GetValue(FormActionName);
					if (rowaction != "delete" && rowaction != "insertdelete") {
						LoadFormValues(); // Get form values
						if (rowaction == "insert" && EmptyRow()) {

							// Ignore
						} else {
							rows.Add(GetFieldValues("FormValue")); // Return row as array
						}
					}
				}
				return rows; // Return as array of array
			}

			// Restore form values for current row
			public void RestoreCurrentRowFormValues(object index) {

				// Get row based on current index
				ObjForm.Index = ew_ConvertToInt(index);
				LoadFormValues(); // Load form values
			}
			#pragma warning disable 162

			// Get list of filters
			public string GetFilterList() {

				// Initialize
				string sFilterList = "";
				sFilterList = ew_Concat(sFilterList, AgentId.AdvancedSearch.ToJSON(), ","); // Field AgentId
				sFilterList = ew_Concat(sFilterList, AgentName.AdvancedSearch.ToJSON(), ","); // Field AgentName
				sFilterList = ew_Concat(sFilterList, AgentRiskRating.AdvancedSearch.ToJSON(), ","); // Field AgentRiskRating
				sFilterList = ew_Concat(sFilterList, AgentRiskCredit.AdvancedSearch.ToJSON(), ","); // Field AgentRiskCredit
				sFilterList = ew_Concat(sFilterList, Address1.AdvancedSearch.ToJSON(), ","); // Field Address1
				sFilterList = ew_Concat(sFilterList, Address2.AdvancedSearch.ToJSON(), ","); // Field Address2
				sFilterList = ew_Concat(sFilterList, Address3.AdvancedSearch.ToJSON(), ","); // Field Address3
				sFilterList = ew_Concat(sFilterList, Country.AdvancedSearch.ToJSON(), ","); // Field Country
				sFilterList = ew_Concat(sFilterList, ZipCode.AdvancedSearch.ToJSON(), ","); // Field ZipCode
				sFilterList = ew_Concat(sFilterList, Fax.AdvancedSearch.ToJSON(), ","); // Field Fax
				sFilterList = ew_Concat(sFilterList, Phone.AdvancedSearch.ToJSON(), ","); // Field Phone
				sFilterList = ew_Concat(sFilterList, Mobile.AdvancedSearch.ToJSON(), ","); // Field Mobile
				sFilterList = ew_Concat(sFilterList, BuzType.AdvancedSearch.ToJSON(), ","); // Field BuzType
				sFilterList = ew_Concat(sFilterList, ClassType.AdvancedSearch.ToJSON(), ","); // Field ClassType
				sFilterList = ew_Concat(sFilterList, DefContactPName.AdvancedSearch.ToJSON(), ","); // Field DefContactPName
				sFilterList = ew_Concat(sFilterList, DefContactPNric.AdvancedSearch.ToJSON(), ","); // Field DefContactPNric
				sFilterList = ew_Concat(sFilterList, DefContactPNation.AdvancedSearch.ToJSON(), ","); // Field DefContactPNation
				sFilterList = ew_Concat(sFilterList, DefContactPOccupation.AdvancedSearch.ToJSON(), ","); // Field DefContactPOccupation
				sFilterList = ew_Concat(sFilterList, TermsId.AdvancedSearch.ToJSON(), ","); // Field TermsId
				sFilterList = ew_Concat(sFilterList, LedgerBal.AdvancedSearch.ToJSON(), ","); // Field LedgerBal
				sFilterList = ew_Concat(sFilterList, AvailableBal.AdvancedSearch.ToJSON(), ","); // Field AvailableBal
				sFilterList = ew_Concat(sFilterList, _Email.AdvancedSearch.ToJSON(), ","); // Field Email
				sFilterList = ew_Concat(sFilterList, URL.AdvancedSearch.ToJSON(), ","); // Field URL
				sFilterList = ew_Concat(sFilterList, CustType.AdvancedSearch.ToJSON(), ","); // Field CustType
				sFilterList = ew_Concat(sFilterList, RemittanceLicNO.AdvancedSearch.ToJSON(), ","); // Field RemittanceLicNO
				sFilterList = ew_Concat(sFilterList, MCLicNo.AdvancedSearch.ToJSON(), ","); // Field MCLicNo
				sFilterList = ew_Concat(sFilterList, BankYesNo.AdvancedSearch.ToJSON(), ","); // Field BankYesNo
				sFilterList = ew_Concat(sFilterList, BankODLimit.AdvancedSearch.ToJSON(), ","); // Field BankODLimit
				sFilterList = ew_Concat(sFilterList, BankAcctNO.AdvancedSearch.ToJSON(), ","); // Field BankAcctNO
				sFilterList = ew_Concat(sFilterList, CreditLimit.AdvancedSearch.ToJSON(), ","); // Field CreditLimit
				sFilterList = ew_Concat(sFilterList, ReferBy.AdvancedSearch.ToJSON(), ","); // Field ReferBy
				sFilterList = ew_Concat(sFilterList, AgentImageName.AdvancedSearch.ToJSON(), ","); // Field AgentImageName
				sFilterList = ew_Concat(sFilterList, status.AdvancedSearch.ToJSON(), ","); // Field status
				sFilterList = ew_Concat(sFilterList, CreatedBy.AdvancedSearch.ToJSON(), ","); // Field CreatedBy
				sFilterList = ew_Concat(sFilterList, CreatedDate.AdvancedSearch.ToJSON(), ","); // Field CreatedDate
				sFilterList = ew_Concat(sFilterList, ModifiedUser.AdvancedSearch.ToJSON(), ","); // Field ModifiedUser
				sFilterList = ew_Concat(sFilterList, ModifiedDate.AdvancedSearch.ToJSON(), ","); // Field ModifiedDate
				sFilterList = ew_Concat(sFilterList, PPExpiryDate.AdvancedSearch.ToJSON(), ","); // Field PPExpiryDate
				sFilterList = ew_Concat(sFilterList, TTExpiryDate.AdvancedSearch.ToJSON(), ","); // Field TTExpiryDate
				sFilterList = ew_Concat(sFilterList, MCExpiryDate.AdvancedSearch.ToJSON(), ","); // Field MCExpiryDate
				sFilterList = ew_Concat(sFilterList, Action.AdvancedSearch.ToJSON(), ","); // Field Action
				sFilterList = ew_Concat(sFilterList, Remark.AdvancedSearch.ToJSON(), ","); // Field Remark
				sFilterList = ew_Concat(sFilterList, MCType.AdvancedSearch.ToJSON(), ","); // Field MCType
				sFilterList = ew_Concat(sFilterList, CustDOB.AdvancedSearch.ToJSON(), ","); // Field CustDOB
				sFilterList = ew_Concat(sFilterList, DefContactDOB.AdvancedSearch.ToJSON(), ","); // Field DefContactDOB
				sFilterList = ew_Concat(sFilterList, ScanImage.AdvancedSearch.ToJSON(), ","); // Field ScanImage
				sFilterList = ew_Concat(sFilterList, BizNature.AdvancedSearch.ToJSON(), ","); // Field BizNature
				sFilterList = ew_Concat(sFilterList, DefContactPOB.AdvancedSearch.ToJSON(), ","); // Field DefContactPOB
				sFilterList = ew_Concat(sFilterList, NewTran.AdvancedSearch.ToJSON(), ","); // Field NewTran
				sFilterList = ew_Concat(sFilterList, BizRegNo.AdvancedSearch.ToJSON(), ","); // Field BizRegNo
				sFilterList = ew_Concat(sFilterList, BizRegDate.AdvancedSearch.ToJSON(), ","); // Field BizRegDate
				sFilterList = ew_Concat(sFilterList, BizRegPlace.AdvancedSearch.ToJSON(), ","); // Field BizRegPlace
				sFilterList = ew_Concat(sFilterList, BizRegExpDate.AdvancedSearch.ToJSON(), ","); // Field BizRegExpDate
				sFilterList = ew_Concat(sFilterList, UnIncorpExec.AdvancedSearch.ToJSON(), ","); // Field UnIncorpExec
				sFilterList = ew_Concat(sFilterList, DefContactAuthorzLetter.AdvancedSearch.ToJSON(), ","); // Field DefContactAuthorzLetter
				sFilterList = ew_Concat(sFilterList, Politician.AdvancedSearch.ToJSON(), ","); // Field Politician
				sFilterList = ew_Concat(sFilterList, BizPartnerNo.AdvancedSearch.ToJSON(), ","); // Field BizPartnerNo
				sFilterList = ew_Concat(sFilterList, Remark2.AdvancedSearch.ToJSON(), ","); // Field Remark2
				sFilterList = ew_Concat(sFilterList, BannedListRemark.AdvancedSearch.ToJSON(), ","); // Field BannedListRemark
				sFilterList = Regex.Replace(sFilterList, ",$", "");

				// Return filter list in json
				if (sFilterList != "")
					sFilterList = "\"data\":{" + sFilterList + "}";
				return (sFilterList != "") ? "{" + sFilterList + "}" : "null";
			}
			#pragma warning restore 162

			// Restore list of filters
			public bool RestoreFilterList() {

				// Return if not reset filter
				if (ew_Post("cmd") != "resetfilter")
					return false;
				Dictionary<string, string> filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(ew_Post("filter"));
				Command = "search";

				// Field AgentId
				if (filter.ContainsKey("x_AgentId")) {
					AgentId.AdvancedSearch.SearchValue = filter["x_AgentId"];
					AgentId.AdvancedSearch.SearchOperator = filter["z_AgentId"];
					AgentId.AdvancedSearch.SearchCondition = filter["v_AgentId"];
					AgentId.AdvancedSearch.SearchValue2 = filter["y_AgentId"];
					AgentId.AdvancedSearch.SearchOperator2 = filter["w_AgentId"];
					AgentId.AdvancedSearch.Save();
				}

				// Field AgentName
				if (filter.ContainsKey("x_AgentName")) {
					AgentName.AdvancedSearch.SearchValue = filter["x_AgentName"];
					AgentName.AdvancedSearch.SearchOperator = filter["z_AgentName"];
					AgentName.AdvancedSearch.SearchCondition = filter["v_AgentName"];
					AgentName.AdvancedSearch.SearchValue2 = filter["y_AgentName"];
					AgentName.AdvancedSearch.SearchOperator2 = filter["w_AgentName"];
					AgentName.AdvancedSearch.Save();
				}

				// Field AgentRiskRating
				if (filter.ContainsKey("x_AgentRiskRating")) {
					AgentRiskRating.AdvancedSearch.SearchValue = filter["x_AgentRiskRating"];
					AgentRiskRating.AdvancedSearch.SearchOperator = filter["z_AgentRiskRating"];
					AgentRiskRating.AdvancedSearch.SearchCondition = filter["v_AgentRiskRating"];
					AgentRiskRating.AdvancedSearch.SearchValue2 = filter["y_AgentRiskRating"];
					AgentRiskRating.AdvancedSearch.SearchOperator2 = filter["w_AgentRiskRating"];
					AgentRiskRating.AdvancedSearch.Save();
				}

				// Field AgentRiskCredit
				if (filter.ContainsKey("x_AgentRiskCredit")) {
					AgentRiskCredit.AdvancedSearch.SearchValue = filter["x_AgentRiskCredit"];
					AgentRiskCredit.AdvancedSearch.SearchOperator = filter["z_AgentRiskCredit"];
					AgentRiskCredit.AdvancedSearch.SearchCondition = filter["v_AgentRiskCredit"];
					AgentRiskCredit.AdvancedSearch.SearchValue2 = filter["y_AgentRiskCredit"];
					AgentRiskCredit.AdvancedSearch.SearchOperator2 = filter["w_AgentRiskCredit"];
					AgentRiskCredit.AdvancedSearch.Save();
				}

				// Field Address1
				if (filter.ContainsKey("x_Address1")) {
					Address1.AdvancedSearch.SearchValue = filter["x_Address1"];
					Address1.AdvancedSearch.SearchOperator = filter["z_Address1"];
					Address1.AdvancedSearch.SearchCondition = filter["v_Address1"];
					Address1.AdvancedSearch.SearchValue2 = filter["y_Address1"];
					Address1.AdvancedSearch.SearchOperator2 = filter["w_Address1"];
					Address1.AdvancedSearch.Save();
				}

				// Field Address2
				if (filter.ContainsKey("x_Address2")) {
					Address2.AdvancedSearch.SearchValue = filter["x_Address2"];
					Address2.AdvancedSearch.SearchOperator = filter["z_Address2"];
					Address2.AdvancedSearch.SearchCondition = filter["v_Address2"];
					Address2.AdvancedSearch.SearchValue2 = filter["y_Address2"];
					Address2.AdvancedSearch.SearchOperator2 = filter["w_Address2"];
					Address2.AdvancedSearch.Save();
				}

				// Field Address3
				if (filter.ContainsKey("x_Address3")) {
					Address3.AdvancedSearch.SearchValue = filter["x_Address3"];
					Address3.AdvancedSearch.SearchOperator = filter["z_Address3"];
					Address3.AdvancedSearch.SearchCondition = filter["v_Address3"];
					Address3.AdvancedSearch.SearchValue2 = filter["y_Address3"];
					Address3.AdvancedSearch.SearchOperator2 = filter["w_Address3"];
					Address3.AdvancedSearch.Save();
				}

				// Field Country
				if (filter.ContainsKey("x_Country")) {
					Country.AdvancedSearch.SearchValue = filter["x_Country"];
					Country.AdvancedSearch.SearchOperator = filter["z_Country"];
					Country.AdvancedSearch.SearchCondition = filter["v_Country"];
					Country.AdvancedSearch.SearchValue2 = filter["y_Country"];
					Country.AdvancedSearch.SearchOperator2 = filter["w_Country"];
					Country.AdvancedSearch.Save();
				}

				// Field ZipCode
				if (filter.ContainsKey("x_ZipCode")) {
					ZipCode.AdvancedSearch.SearchValue = filter["x_ZipCode"];
					ZipCode.AdvancedSearch.SearchOperator = filter["z_ZipCode"];
					ZipCode.AdvancedSearch.SearchCondition = filter["v_ZipCode"];
					ZipCode.AdvancedSearch.SearchValue2 = filter["y_ZipCode"];
					ZipCode.AdvancedSearch.SearchOperator2 = filter["w_ZipCode"];
					ZipCode.AdvancedSearch.Save();
				}

				// Field Fax
				if (filter.ContainsKey("x_Fax")) {
					Fax.AdvancedSearch.SearchValue = filter["x_Fax"];
					Fax.AdvancedSearch.SearchOperator = filter["z_Fax"];
					Fax.AdvancedSearch.SearchCondition = filter["v_Fax"];
					Fax.AdvancedSearch.SearchValue2 = filter["y_Fax"];
					Fax.AdvancedSearch.SearchOperator2 = filter["w_Fax"];
					Fax.AdvancedSearch.Save();
				}

				// Field Phone
				if (filter.ContainsKey("x_Phone")) {
					Phone.AdvancedSearch.SearchValue = filter["x_Phone"];
					Phone.AdvancedSearch.SearchOperator = filter["z_Phone"];
					Phone.AdvancedSearch.SearchCondition = filter["v_Phone"];
					Phone.AdvancedSearch.SearchValue2 = filter["y_Phone"];
					Phone.AdvancedSearch.SearchOperator2 = filter["w_Phone"];
					Phone.AdvancedSearch.Save();
				}

				// Field Mobile
				if (filter.ContainsKey("x_Mobile")) {
					Mobile.AdvancedSearch.SearchValue = filter["x_Mobile"];
					Mobile.AdvancedSearch.SearchOperator = filter["z_Mobile"];
					Mobile.AdvancedSearch.SearchCondition = filter["v_Mobile"];
					Mobile.AdvancedSearch.SearchValue2 = filter["y_Mobile"];
					Mobile.AdvancedSearch.SearchOperator2 = filter["w_Mobile"];
					Mobile.AdvancedSearch.Save();
				}

				// Field BuzType
				if (filter.ContainsKey("x_BuzType")) {
					BuzType.AdvancedSearch.SearchValue = filter["x_BuzType"];
					BuzType.AdvancedSearch.SearchOperator = filter["z_BuzType"];
					BuzType.AdvancedSearch.SearchCondition = filter["v_BuzType"];
					BuzType.AdvancedSearch.SearchValue2 = filter["y_BuzType"];
					BuzType.AdvancedSearch.SearchOperator2 = filter["w_BuzType"];
					BuzType.AdvancedSearch.Save();
				}

				// Field ClassType
				if (filter.ContainsKey("x_ClassType")) {
					ClassType.AdvancedSearch.SearchValue = filter["x_ClassType"];
					ClassType.AdvancedSearch.SearchOperator = filter["z_ClassType"];
					ClassType.AdvancedSearch.SearchCondition = filter["v_ClassType"];
					ClassType.AdvancedSearch.SearchValue2 = filter["y_ClassType"];
					ClassType.AdvancedSearch.SearchOperator2 = filter["w_ClassType"];
					ClassType.AdvancedSearch.Save();
				}

				// Field DefContactPName
				if (filter.ContainsKey("x_DefContactPName")) {
					DefContactPName.AdvancedSearch.SearchValue = filter["x_DefContactPName"];
					DefContactPName.AdvancedSearch.SearchOperator = filter["z_DefContactPName"];
					DefContactPName.AdvancedSearch.SearchCondition = filter["v_DefContactPName"];
					DefContactPName.AdvancedSearch.SearchValue2 = filter["y_DefContactPName"];
					DefContactPName.AdvancedSearch.SearchOperator2 = filter["w_DefContactPName"];
					DefContactPName.AdvancedSearch.Save();
				}

				// Field DefContactPNric
				if (filter.ContainsKey("x_DefContactPNric")) {
					DefContactPNric.AdvancedSearch.SearchValue = filter["x_DefContactPNric"];
					DefContactPNric.AdvancedSearch.SearchOperator = filter["z_DefContactPNric"];
					DefContactPNric.AdvancedSearch.SearchCondition = filter["v_DefContactPNric"];
					DefContactPNric.AdvancedSearch.SearchValue2 = filter["y_DefContactPNric"];
					DefContactPNric.AdvancedSearch.SearchOperator2 = filter["w_DefContactPNric"];
					DefContactPNric.AdvancedSearch.Save();
				}

				// Field DefContactPNation
				if (filter.ContainsKey("x_DefContactPNation")) {
					DefContactPNation.AdvancedSearch.SearchValue = filter["x_DefContactPNation"];
					DefContactPNation.AdvancedSearch.SearchOperator = filter["z_DefContactPNation"];
					DefContactPNation.AdvancedSearch.SearchCondition = filter["v_DefContactPNation"];
					DefContactPNation.AdvancedSearch.SearchValue2 = filter["y_DefContactPNation"];
					DefContactPNation.AdvancedSearch.SearchOperator2 = filter["w_DefContactPNation"];
					DefContactPNation.AdvancedSearch.Save();
				}

				// Field DefContactPOccupation
				if (filter.ContainsKey("x_DefContactPOccupation")) {
					DefContactPOccupation.AdvancedSearch.SearchValue = filter["x_DefContactPOccupation"];
					DefContactPOccupation.AdvancedSearch.SearchOperator = filter["z_DefContactPOccupation"];
					DefContactPOccupation.AdvancedSearch.SearchCondition = filter["v_DefContactPOccupation"];
					DefContactPOccupation.AdvancedSearch.SearchValue2 = filter["y_DefContactPOccupation"];
					DefContactPOccupation.AdvancedSearch.SearchOperator2 = filter["w_DefContactPOccupation"];
					DefContactPOccupation.AdvancedSearch.Save();
				}

				// Field TermsId
				if (filter.ContainsKey("x_TermsId")) {
					TermsId.AdvancedSearch.SearchValue = filter["x_TermsId"];
					TermsId.AdvancedSearch.SearchOperator = filter["z_TermsId"];
					TermsId.AdvancedSearch.SearchCondition = filter["v_TermsId"];
					TermsId.AdvancedSearch.SearchValue2 = filter["y_TermsId"];
					TermsId.AdvancedSearch.SearchOperator2 = filter["w_TermsId"];
					TermsId.AdvancedSearch.Save();
				}

				// Field LedgerBal
				if (filter.ContainsKey("x_LedgerBal")) {
					LedgerBal.AdvancedSearch.SearchValue = filter["x_LedgerBal"];
					LedgerBal.AdvancedSearch.SearchOperator = filter["z_LedgerBal"];
					LedgerBal.AdvancedSearch.SearchCondition = filter["v_LedgerBal"];
					LedgerBal.AdvancedSearch.SearchValue2 = filter["y_LedgerBal"];
					LedgerBal.AdvancedSearch.SearchOperator2 = filter["w_LedgerBal"];
					LedgerBal.AdvancedSearch.Save();
				}

				// Field AvailableBal
				if (filter.ContainsKey("x_AvailableBal")) {
					AvailableBal.AdvancedSearch.SearchValue = filter["x_AvailableBal"];
					AvailableBal.AdvancedSearch.SearchOperator = filter["z_AvailableBal"];
					AvailableBal.AdvancedSearch.SearchCondition = filter["v_AvailableBal"];
					AvailableBal.AdvancedSearch.SearchValue2 = filter["y_AvailableBal"];
					AvailableBal.AdvancedSearch.SearchOperator2 = filter["w_AvailableBal"];
					AvailableBal.AdvancedSearch.Save();
				}

				// Field Email
				if (filter.ContainsKey("x__Email")) {
					_Email.AdvancedSearch.SearchValue = filter["x__Email"];
					_Email.AdvancedSearch.SearchOperator = filter["z__Email"];
					_Email.AdvancedSearch.SearchCondition = filter["v__Email"];
					_Email.AdvancedSearch.SearchValue2 = filter["y__Email"];
					_Email.AdvancedSearch.SearchOperator2 = filter["w__Email"];
					_Email.AdvancedSearch.Save();
				}

				// Field URL
				if (filter.ContainsKey("x_URL")) {
					URL.AdvancedSearch.SearchValue = filter["x_URL"];
					URL.AdvancedSearch.SearchOperator = filter["z_URL"];
					URL.AdvancedSearch.SearchCondition = filter["v_URL"];
					URL.AdvancedSearch.SearchValue2 = filter["y_URL"];
					URL.AdvancedSearch.SearchOperator2 = filter["w_URL"];
					URL.AdvancedSearch.Save();
				}

				// Field CustType
				if (filter.ContainsKey("x_CustType")) {
					CustType.AdvancedSearch.SearchValue = filter["x_CustType"];
					CustType.AdvancedSearch.SearchOperator = filter["z_CustType"];
					CustType.AdvancedSearch.SearchCondition = filter["v_CustType"];
					CustType.AdvancedSearch.SearchValue2 = filter["y_CustType"];
					CustType.AdvancedSearch.SearchOperator2 = filter["w_CustType"];
					CustType.AdvancedSearch.Save();
				}

				// Field RemittanceLicNO
				if (filter.ContainsKey("x_RemittanceLicNO")) {
					RemittanceLicNO.AdvancedSearch.SearchValue = filter["x_RemittanceLicNO"];
					RemittanceLicNO.AdvancedSearch.SearchOperator = filter["z_RemittanceLicNO"];
					RemittanceLicNO.AdvancedSearch.SearchCondition = filter["v_RemittanceLicNO"];
					RemittanceLicNO.AdvancedSearch.SearchValue2 = filter["y_RemittanceLicNO"];
					RemittanceLicNO.AdvancedSearch.SearchOperator2 = filter["w_RemittanceLicNO"];
					RemittanceLicNO.AdvancedSearch.Save();
				}

				// Field MCLicNo
				if (filter.ContainsKey("x_MCLicNo")) {
					MCLicNo.AdvancedSearch.SearchValue = filter["x_MCLicNo"];
					MCLicNo.AdvancedSearch.SearchOperator = filter["z_MCLicNo"];
					MCLicNo.AdvancedSearch.SearchCondition = filter["v_MCLicNo"];
					MCLicNo.AdvancedSearch.SearchValue2 = filter["y_MCLicNo"];
					MCLicNo.AdvancedSearch.SearchOperator2 = filter["w_MCLicNo"];
					MCLicNo.AdvancedSearch.Save();
				}

				// Field BankYesNo
				if (filter.ContainsKey("x_BankYesNo")) {
					BankYesNo.AdvancedSearch.SearchValue = filter["x_BankYesNo"];
					BankYesNo.AdvancedSearch.SearchOperator = filter["z_BankYesNo"];
					BankYesNo.AdvancedSearch.SearchCondition = filter["v_BankYesNo"];
					BankYesNo.AdvancedSearch.SearchValue2 = filter["y_BankYesNo"];
					BankYesNo.AdvancedSearch.SearchOperator2 = filter["w_BankYesNo"];
					BankYesNo.AdvancedSearch.Save();
				}

				// Field BankODLimit
				if (filter.ContainsKey("x_BankODLimit")) {
					BankODLimit.AdvancedSearch.SearchValue = filter["x_BankODLimit"];
					BankODLimit.AdvancedSearch.SearchOperator = filter["z_BankODLimit"];
					BankODLimit.AdvancedSearch.SearchCondition = filter["v_BankODLimit"];
					BankODLimit.AdvancedSearch.SearchValue2 = filter["y_BankODLimit"];
					BankODLimit.AdvancedSearch.SearchOperator2 = filter["w_BankODLimit"];
					BankODLimit.AdvancedSearch.Save();
				}

				// Field BankAcctNO
				if (filter.ContainsKey("x_BankAcctNO")) {
					BankAcctNO.AdvancedSearch.SearchValue = filter["x_BankAcctNO"];
					BankAcctNO.AdvancedSearch.SearchOperator = filter["z_BankAcctNO"];
					BankAcctNO.AdvancedSearch.SearchCondition = filter["v_BankAcctNO"];
					BankAcctNO.AdvancedSearch.SearchValue2 = filter["y_BankAcctNO"];
					BankAcctNO.AdvancedSearch.SearchOperator2 = filter["w_BankAcctNO"];
					BankAcctNO.AdvancedSearch.Save();
				}

				// Field CreditLimit
				if (filter.ContainsKey("x_CreditLimit")) {
					CreditLimit.AdvancedSearch.SearchValue = filter["x_CreditLimit"];
					CreditLimit.AdvancedSearch.SearchOperator = filter["z_CreditLimit"];
					CreditLimit.AdvancedSearch.SearchCondition = filter["v_CreditLimit"];
					CreditLimit.AdvancedSearch.SearchValue2 = filter["y_CreditLimit"];
					CreditLimit.AdvancedSearch.SearchOperator2 = filter["w_CreditLimit"];
					CreditLimit.AdvancedSearch.Save();
				}

				// Field ReferBy
				if (filter.ContainsKey("x_ReferBy")) {
					ReferBy.AdvancedSearch.SearchValue = filter["x_ReferBy"];
					ReferBy.AdvancedSearch.SearchOperator = filter["z_ReferBy"];
					ReferBy.AdvancedSearch.SearchCondition = filter["v_ReferBy"];
					ReferBy.AdvancedSearch.SearchValue2 = filter["y_ReferBy"];
					ReferBy.AdvancedSearch.SearchOperator2 = filter["w_ReferBy"];
					ReferBy.AdvancedSearch.Save();
				}

				// Field AgentImageName
				if (filter.ContainsKey("x_AgentImageName")) {
					AgentImageName.AdvancedSearch.SearchValue = filter["x_AgentImageName"];
					AgentImageName.AdvancedSearch.SearchOperator = filter["z_AgentImageName"];
					AgentImageName.AdvancedSearch.SearchCondition = filter["v_AgentImageName"];
					AgentImageName.AdvancedSearch.SearchValue2 = filter["y_AgentImageName"];
					AgentImageName.AdvancedSearch.SearchOperator2 = filter["w_AgentImageName"];
					AgentImageName.AdvancedSearch.Save();
				}

				// Field status
				if (filter.ContainsKey("x_status")) {
					status.AdvancedSearch.SearchValue = filter["x_status"];
					status.AdvancedSearch.SearchOperator = filter["z_status"];
					status.AdvancedSearch.SearchCondition = filter["v_status"];
					status.AdvancedSearch.SearchValue2 = filter["y_status"];
					status.AdvancedSearch.SearchOperator2 = filter["w_status"];
					status.AdvancedSearch.Save();
				}

				// Field CreatedBy
				if (filter.ContainsKey("x_CreatedBy")) {
					CreatedBy.AdvancedSearch.SearchValue = filter["x_CreatedBy"];
					CreatedBy.AdvancedSearch.SearchOperator = filter["z_CreatedBy"];
					CreatedBy.AdvancedSearch.SearchCondition = filter["v_CreatedBy"];
					CreatedBy.AdvancedSearch.SearchValue2 = filter["y_CreatedBy"];
					CreatedBy.AdvancedSearch.SearchOperator2 = filter["w_CreatedBy"];
					CreatedBy.AdvancedSearch.Save();
				}

				// Field CreatedDate
				if (filter.ContainsKey("x_CreatedDate")) {
					CreatedDate.AdvancedSearch.SearchValue = filter["x_CreatedDate"];
					CreatedDate.AdvancedSearch.SearchOperator = filter["z_CreatedDate"];
					CreatedDate.AdvancedSearch.SearchCondition = filter["v_CreatedDate"];
					CreatedDate.AdvancedSearch.SearchValue2 = filter["y_CreatedDate"];
					CreatedDate.AdvancedSearch.SearchOperator2 = filter["w_CreatedDate"];
					CreatedDate.AdvancedSearch.Save();
				}

				// Field ModifiedUser
				if (filter.ContainsKey("x_ModifiedUser")) {
					ModifiedUser.AdvancedSearch.SearchValue = filter["x_ModifiedUser"];
					ModifiedUser.AdvancedSearch.SearchOperator = filter["z_ModifiedUser"];
					ModifiedUser.AdvancedSearch.SearchCondition = filter["v_ModifiedUser"];
					ModifiedUser.AdvancedSearch.SearchValue2 = filter["y_ModifiedUser"];
					ModifiedUser.AdvancedSearch.SearchOperator2 = filter["w_ModifiedUser"];
					ModifiedUser.AdvancedSearch.Save();
				}

				// Field ModifiedDate
				if (filter.ContainsKey("x_ModifiedDate")) {
					ModifiedDate.AdvancedSearch.SearchValue = filter["x_ModifiedDate"];
					ModifiedDate.AdvancedSearch.SearchOperator = filter["z_ModifiedDate"];
					ModifiedDate.AdvancedSearch.SearchCondition = filter["v_ModifiedDate"];
					ModifiedDate.AdvancedSearch.SearchValue2 = filter["y_ModifiedDate"];
					ModifiedDate.AdvancedSearch.SearchOperator2 = filter["w_ModifiedDate"];
					ModifiedDate.AdvancedSearch.Save();
				}

				// Field PPExpiryDate
				if (filter.ContainsKey("x_PPExpiryDate")) {
					PPExpiryDate.AdvancedSearch.SearchValue = filter["x_PPExpiryDate"];
					PPExpiryDate.AdvancedSearch.SearchOperator = filter["z_PPExpiryDate"];
					PPExpiryDate.AdvancedSearch.SearchCondition = filter["v_PPExpiryDate"];
					PPExpiryDate.AdvancedSearch.SearchValue2 = filter["y_PPExpiryDate"];
					PPExpiryDate.AdvancedSearch.SearchOperator2 = filter["w_PPExpiryDate"];
					PPExpiryDate.AdvancedSearch.Save();
				}

				// Field TTExpiryDate
				if (filter.ContainsKey("x_TTExpiryDate")) {
					TTExpiryDate.AdvancedSearch.SearchValue = filter["x_TTExpiryDate"];
					TTExpiryDate.AdvancedSearch.SearchOperator = filter["z_TTExpiryDate"];
					TTExpiryDate.AdvancedSearch.SearchCondition = filter["v_TTExpiryDate"];
					TTExpiryDate.AdvancedSearch.SearchValue2 = filter["y_TTExpiryDate"];
					TTExpiryDate.AdvancedSearch.SearchOperator2 = filter["w_TTExpiryDate"];
					TTExpiryDate.AdvancedSearch.Save();
				}

				// Field MCExpiryDate
				if (filter.ContainsKey("x_MCExpiryDate")) {
					MCExpiryDate.AdvancedSearch.SearchValue = filter["x_MCExpiryDate"];
					MCExpiryDate.AdvancedSearch.SearchOperator = filter["z_MCExpiryDate"];
					MCExpiryDate.AdvancedSearch.SearchCondition = filter["v_MCExpiryDate"];
					MCExpiryDate.AdvancedSearch.SearchValue2 = filter["y_MCExpiryDate"];
					MCExpiryDate.AdvancedSearch.SearchOperator2 = filter["w_MCExpiryDate"];
					MCExpiryDate.AdvancedSearch.Save();
				}

				// Field Action
				if (filter.ContainsKey("x_Action")) {
					Action.AdvancedSearch.SearchValue = filter["x_Action"];
					Action.AdvancedSearch.SearchOperator = filter["z_Action"];
					Action.AdvancedSearch.SearchCondition = filter["v_Action"];
					Action.AdvancedSearch.SearchValue2 = filter["y_Action"];
					Action.AdvancedSearch.SearchOperator2 = filter["w_Action"];
					Action.AdvancedSearch.Save();
				}

				// Field Remark
				if (filter.ContainsKey("x_Remark")) {
					Remark.AdvancedSearch.SearchValue = filter["x_Remark"];
					Remark.AdvancedSearch.SearchOperator = filter["z_Remark"];
					Remark.AdvancedSearch.SearchCondition = filter["v_Remark"];
					Remark.AdvancedSearch.SearchValue2 = filter["y_Remark"];
					Remark.AdvancedSearch.SearchOperator2 = filter["w_Remark"];
					Remark.AdvancedSearch.Save();
				}

				// Field MCType
				if (filter.ContainsKey("x_MCType")) {
					MCType.AdvancedSearch.SearchValue = filter["x_MCType"];
					MCType.AdvancedSearch.SearchOperator = filter["z_MCType"];
					MCType.AdvancedSearch.SearchCondition = filter["v_MCType"];
					MCType.AdvancedSearch.SearchValue2 = filter["y_MCType"];
					MCType.AdvancedSearch.SearchOperator2 = filter["w_MCType"];
					MCType.AdvancedSearch.Save();
				}

				// Field CustDOB
				if (filter.ContainsKey("x_CustDOB")) {
					CustDOB.AdvancedSearch.SearchValue = filter["x_CustDOB"];
					CustDOB.AdvancedSearch.SearchOperator = filter["z_CustDOB"];
					CustDOB.AdvancedSearch.SearchCondition = filter["v_CustDOB"];
					CustDOB.AdvancedSearch.SearchValue2 = filter["y_CustDOB"];
					CustDOB.AdvancedSearch.SearchOperator2 = filter["w_CustDOB"];
					CustDOB.AdvancedSearch.Save();
				}

				// Field DefContactDOB
				if (filter.ContainsKey("x_DefContactDOB")) {
					DefContactDOB.AdvancedSearch.SearchValue = filter["x_DefContactDOB"];
					DefContactDOB.AdvancedSearch.SearchOperator = filter["z_DefContactDOB"];
					DefContactDOB.AdvancedSearch.SearchCondition = filter["v_DefContactDOB"];
					DefContactDOB.AdvancedSearch.SearchValue2 = filter["y_DefContactDOB"];
					DefContactDOB.AdvancedSearch.SearchOperator2 = filter["w_DefContactDOB"];
					DefContactDOB.AdvancedSearch.Save();
				}

				// Field ScanImage
				if (filter.ContainsKey("x_ScanImage")) {
					ScanImage.AdvancedSearch.SearchValue = filter["x_ScanImage"];
					ScanImage.AdvancedSearch.SearchOperator = filter["z_ScanImage"];
					ScanImage.AdvancedSearch.SearchCondition = filter["v_ScanImage"];
					ScanImage.AdvancedSearch.SearchValue2 = filter["y_ScanImage"];
					ScanImage.AdvancedSearch.SearchOperator2 = filter["w_ScanImage"];
					ScanImage.AdvancedSearch.Save();
				}

				// Field BizNature
				if (filter.ContainsKey("x_BizNature")) {
					BizNature.AdvancedSearch.SearchValue = filter["x_BizNature"];
					BizNature.AdvancedSearch.SearchOperator = filter["z_BizNature"];
					BizNature.AdvancedSearch.SearchCondition = filter["v_BizNature"];
					BizNature.AdvancedSearch.SearchValue2 = filter["y_BizNature"];
					BizNature.AdvancedSearch.SearchOperator2 = filter["w_BizNature"];
					BizNature.AdvancedSearch.Save();
				}

				// Field DefContactPOB
				if (filter.ContainsKey("x_DefContactPOB")) {
					DefContactPOB.AdvancedSearch.SearchValue = filter["x_DefContactPOB"];
					DefContactPOB.AdvancedSearch.SearchOperator = filter["z_DefContactPOB"];
					DefContactPOB.AdvancedSearch.SearchCondition = filter["v_DefContactPOB"];
					DefContactPOB.AdvancedSearch.SearchValue2 = filter["y_DefContactPOB"];
					DefContactPOB.AdvancedSearch.SearchOperator2 = filter["w_DefContactPOB"];
					DefContactPOB.AdvancedSearch.Save();
				}

				// Field NewTran
				if (filter.ContainsKey("x_NewTran")) {
					NewTran.AdvancedSearch.SearchValue = filter["x_NewTran"];
					NewTran.AdvancedSearch.SearchOperator = filter["z_NewTran"];
					NewTran.AdvancedSearch.SearchCondition = filter["v_NewTran"];
					NewTran.AdvancedSearch.SearchValue2 = filter["y_NewTran"];
					NewTran.AdvancedSearch.SearchOperator2 = filter["w_NewTran"];
					NewTran.AdvancedSearch.Save();
				}

				// Field BizRegNo
				if (filter.ContainsKey("x_BizRegNo")) {
					BizRegNo.AdvancedSearch.SearchValue = filter["x_BizRegNo"];
					BizRegNo.AdvancedSearch.SearchOperator = filter["z_BizRegNo"];
					BizRegNo.AdvancedSearch.SearchCondition = filter["v_BizRegNo"];
					BizRegNo.AdvancedSearch.SearchValue2 = filter["y_BizRegNo"];
					BizRegNo.AdvancedSearch.SearchOperator2 = filter["w_BizRegNo"];
					BizRegNo.AdvancedSearch.Save();
				}

				// Field BizRegDate
				if (filter.ContainsKey("x_BizRegDate")) {
					BizRegDate.AdvancedSearch.SearchValue = filter["x_BizRegDate"];
					BizRegDate.AdvancedSearch.SearchOperator = filter["z_BizRegDate"];
					BizRegDate.AdvancedSearch.SearchCondition = filter["v_BizRegDate"];
					BizRegDate.AdvancedSearch.SearchValue2 = filter["y_BizRegDate"];
					BizRegDate.AdvancedSearch.SearchOperator2 = filter["w_BizRegDate"];
					BizRegDate.AdvancedSearch.Save();
				}

				// Field BizRegPlace
				if (filter.ContainsKey("x_BizRegPlace")) {
					BizRegPlace.AdvancedSearch.SearchValue = filter["x_BizRegPlace"];
					BizRegPlace.AdvancedSearch.SearchOperator = filter["z_BizRegPlace"];
					BizRegPlace.AdvancedSearch.SearchCondition = filter["v_BizRegPlace"];
					BizRegPlace.AdvancedSearch.SearchValue2 = filter["y_BizRegPlace"];
					BizRegPlace.AdvancedSearch.SearchOperator2 = filter["w_BizRegPlace"];
					BizRegPlace.AdvancedSearch.Save();
				}

				// Field BizRegExpDate
				if (filter.ContainsKey("x_BizRegExpDate")) {
					BizRegExpDate.AdvancedSearch.SearchValue = filter["x_BizRegExpDate"];
					BizRegExpDate.AdvancedSearch.SearchOperator = filter["z_BizRegExpDate"];
					BizRegExpDate.AdvancedSearch.SearchCondition = filter["v_BizRegExpDate"];
					BizRegExpDate.AdvancedSearch.SearchValue2 = filter["y_BizRegExpDate"];
					BizRegExpDate.AdvancedSearch.SearchOperator2 = filter["w_BizRegExpDate"];
					BizRegExpDate.AdvancedSearch.Save();
				}

				// Field UnIncorpExec
				if (filter.ContainsKey("x_UnIncorpExec")) {
					UnIncorpExec.AdvancedSearch.SearchValue = filter["x_UnIncorpExec"];
					UnIncorpExec.AdvancedSearch.SearchOperator = filter["z_UnIncorpExec"];
					UnIncorpExec.AdvancedSearch.SearchCondition = filter["v_UnIncorpExec"];
					UnIncorpExec.AdvancedSearch.SearchValue2 = filter["y_UnIncorpExec"];
					UnIncorpExec.AdvancedSearch.SearchOperator2 = filter["w_UnIncorpExec"];
					UnIncorpExec.AdvancedSearch.Save();
				}

				// Field DefContactAuthorzLetter
				if (filter.ContainsKey("x_DefContactAuthorzLetter")) {
					DefContactAuthorzLetter.AdvancedSearch.SearchValue = filter["x_DefContactAuthorzLetter"];
					DefContactAuthorzLetter.AdvancedSearch.SearchOperator = filter["z_DefContactAuthorzLetter"];
					DefContactAuthorzLetter.AdvancedSearch.SearchCondition = filter["v_DefContactAuthorzLetter"];
					DefContactAuthorzLetter.AdvancedSearch.SearchValue2 = filter["y_DefContactAuthorzLetter"];
					DefContactAuthorzLetter.AdvancedSearch.SearchOperator2 = filter["w_DefContactAuthorzLetter"];
					DefContactAuthorzLetter.AdvancedSearch.Save();
				}

				// Field Politician
				if (filter.ContainsKey("x_Politician")) {
					Politician.AdvancedSearch.SearchValue = filter["x_Politician"];
					Politician.AdvancedSearch.SearchOperator = filter["z_Politician"];
					Politician.AdvancedSearch.SearchCondition = filter["v_Politician"];
					Politician.AdvancedSearch.SearchValue2 = filter["y_Politician"];
					Politician.AdvancedSearch.SearchOperator2 = filter["w_Politician"];
					Politician.AdvancedSearch.Save();
				}

				// Field BizPartnerNo
				if (filter.ContainsKey("x_BizPartnerNo")) {
					BizPartnerNo.AdvancedSearch.SearchValue = filter["x_BizPartnerNo"];
					BizPartnerNo.AdvancedSearch.SearchOperator = filter["z_BizPartnerNo"];
					BizPartnerNo.AdvancedSearch.SearchCondition = filter["v_BizPartnerNo"];
					BizPartnerNo.AdvancedSearch.SearchValue2 = filter["y_BizPartnerNo"];
					BizPartnerNo.AdvancedSearch.SearchOperator2 = filter["w_BizPartnerNo"];
					BizPartnerNo.AdvancedSearch.Save();
				}

				// Field Remark2
				if (filter.ContainsKey("x_Remark2")) {
					Remark2.AdvancedSearch.SearchValue = filter["x_Remark2"];
					Remark2.AdvancedSearch.SearchOperator = filter["z_Remark2"];
					Remark2.AdvancedSearch.SearchCondition = filter["v_Remark2"];
					Remark2.AdvancedSearch.SearchValue2 = filter["y_Remark2"];
					Remark2.AdvancedSearch.SearchOperator2 = filter["w_Remark2"];
					Remark2.AdvancedSearch.Save();
				}

				// Field BannedListRemark
				if (filter.ContainsKey("x_BannedListRemark")) {
					BannedListRemark.AdvancedSearch.SearchValue = filter["x_BannedListRemark"];
					BannedListRemark.AdvancedSearch.SearchOperator = filter["z_BannedListRemark"];
					BannedListRemark.AdvancedSearch.SearchCondition = filter["v_BannedListRemark"];
					BannedListRemark.AdvancedSearch.SearchValue2 = filter["y_BannedListRemark"];
					BannedListRemark.AdvancedSearch.SearchOperator2 = filter["w_BannedListRemark"];
					BannedListRemark.AdvancedSearch.Save();
				}
				return true;
			}

			// Advanced search WHERE clause based on QueryString
			public string AdvancedSearchWhere(bool Def = false) {
				string sWhere = "";
				BuildSearchSql(ref sWhere, AgentId, Def, false); // AgentId
				BuildSearchSql(ref sWhere, AgentName, Def, false); // AgentName
				BuildSearchSql(ref sWhere, AgentRiskRating, Def, false); // AgentRiskRating
				BuildSearchSql(ref sWhere, AgentRiskCredit, Def, false); // AgentRiskCredit
				BuildSearchSql(ref sWhere, Address1, Def, false); // Address1
				BuildSearchSql(ref sWhere, Address2, Def, false); // Address2
				BuildSearchSql(ref sWhere, Address3, Def, false); // Address3
				BuildSearchSql(ref sWhere, Country, Def, false); // Country
				BuildSearchSql(ref sWhere, ZipCode, Def, false); // ZipCode
				BuildSearchSql(ref sWhere, Fax, Def, false); // Fax
				BuildSearchSql(ref sWhere, Phone, Def, false); // Phone
				BuildSearchSql(ref sWhere, Mobile, Def, false); // Mobile
				BuildSearchSql(ref sWhere, BuzType, Def, false); // BuzType
				BuildSearchSql(ref sWhere, ClassType, Def, false); // ClassType
				BuildSearchSql(ref sWhere, DefContactPName, Def, false); // DefContactPName
				BuildSearchSql(ref sWhere, DefContactPNric, Def, false); // DefContactPNric
				BuildSearchSql(ref sWhere, DefContactPNation, Def, false); // DefContactPNation
				BuildSearchSql(ref sWhere, DefContactPOccupation, Def, false); // DefContactPOccupation
				BuildSearchSql(ref sWhere, TermsId, Def, false); // TermsId
				BuildSearchSql(ref sWhere, LedgerBal, Def, false); // LedgerBal
				BuildSearchSql(ref sWhere, AvailableBal, Def, false); // AvailableBal
				BuildSearchSql(ref sWhere, _Email, Def, false); // _Email
				BuildSearchSql(ref sWhere, URL, Def, false); // URL
				BuildSearchSql(ref sWhere, CustType, Def, false); // CustType
				BuildSearchSql(ref sWhere, RemittanceLicNO, Def, false); // RemittanceLicNO
				BuildSearchSql(ref sWhere, MCLicNo, Def, false); // MCLicNo
				BuildSearchSql(ref sWhere, BankYesNo, Def, false); // BankYesNo
				BuildSearchSql(ref sWhere, BankODLimit, Def, false); // BankODLimit
				BuildSearchSql(ref sWhere, BankAcctNO, Def, false); // BankAcctNO
				BuildSearchSql(ref sWhere, CreditLimit, Def, false); // CreditLimit
				BuildSearchSql(ref sWhere, ReferBy, Def, false); // ReferBy
				BuildSearchSql(ref sWhere, AgentImageName, Def, false); // AgentImageName
				BuildSearchSql(ref sWhere, status, Def, false); // status
				BuildSearchSql(ref sWhere, CreatedBy, Def, false); // CreatedBy
				BuildSearchSql(ref sWhere, CreatedDate, Def, false); // CreatedDate
				BuildSearchSql(ref sWhere, ModifiedUser, Def, false); // ModifiedUser
				BuildSearchSql(ref sWhere, ModifiedDate, Def, false); // ModifiedDate
				BuildSearchSql(ref sWhere, PPExpiryDate, Def, false); // PPExpiryDate
				BuildSearchSql(ref sWhere, TTExpiryDate, Def, false); // TTExpiryDate
				BuildSearchSql(ref sWhere, MCExpiryDate, Def, false); // MCExpiryDate
				BuildSearchSql(ref sWhere, Action, Def, false); // Action
				BuildSearchSql(ref sWhere, Remark, Def, false); // Remark
				BuildSearchSql(ref sWhere, MCType, Def, false); // MCType
				BuildSearchSql(ref sWhere, CustDOB, Def, false); // CustDOB
				BuildSearchSql(ref sWhere, DefContactDOB, Def, false); // DefContactDOB
				BuildSearchSql(ref sWhere, ScanImage, Def, false); // ScanImage
				BuildSearchSql(ref sWhere, BizNature, Def, false); // BizNature
				BuildSearchSql(ref sWhere, DefContactPOB, Def, false); // DefContactPOB
				BuildSearchSql(ref sWhere, NewTran, Def, false); // NewTran
				BuildSearchSql(ref sWhere, BizRegNo, Def, false); // BizRegNo
				BuildSearchSql(ref sWhere, BizRegDate, Def, false); // BizRegDate
				BuildSearchSql(ref sWhere, BizRegPlace, Def, false); // BizRegPlace
				BuildSearchSql(ref sWhere, BizRegExpDate, Def, false); // BizRegExpDate
				BuildSearchSql(ref sWhere, UnIncorpExec, Def, false); // UnIncorpExec
				BuildSearchSql(ref sWhere, DefContactAuthorzLetter, Def, false); // DefContactAuthorzLetter
				BuildSearchSql(ref sWhere, Politician, Def, false); // Politician
				BuildSearchSql(ref sWhere, BizPartnerNo, Def, false); // BizPartnerNo
				BuildSearchSql(ref sWhere, Remark2, Def, false); // Remark2
				BuildSearchSql(ref sWhere, BannedListRemark, Def, false); // BannedListRemark

				// Set up search parm
				if (!Def && ew_NotEmpty(sWhere))
					Command = "search";
				if (!Def && Command == "search") {
					AgentId.AdvancedSearch.Save(); // AgentId
					AgentName.AdvancedSearch.Save(); // AgentName
					AgentRiskRating.AdvancedSearch.Save(); // AgentRiskRating
					AgentRiskCredit.AdvancedSearch.Save(); // AgentRiskCredit
					Address1.AdvancedSearch.Save(); // Address1
					Address2.AdvancedSearch.Save(); // Address2
					Address3.AdvancedSearch.Save(); // Address3
					Country.AdvancedSearch.Save(); // Country
					ZipCode.AdvancedSearch.Save(); // ZipCode
					Fax.AdvancedSearch.Save(); // Fax
					Phone.AdvancedSearch.Save(); // Phone
					Mobile.AdvancedSearch.Save(); // Mobile
					BuzType.AdvancedSearch.Save(); // BuzType
					ClassType.AdvancedSearch.Save(); // ClassType
					DefContactPName.AdvancedSearch.Save(); // DefContactPName
					DefContactPNric.AdvancedSearch.Save(); // DefContactPNric
					DefContactPNation.AdvancedSearch.Save(); // DefContactPNation
					DefContactPOccupation.AdvancedSearch.Save(); // DefContactPOccupation
					TermsId.AdvancedSearch.Save(); // TermsId
					LedgerBal.AdvancedSearch.Save(); // LedgerBal
					AvailableBal.AdvancedSearch.Save(); // AvailableBal
					_Email.AdvancedSearch.Save(); // _Email
					URL.AdvancedSearch.Save(); // URL
					CustType.AdvancedSearch.Save(); // CustType
					RemittanceLicNO.AdvancedSearch.Save(); // RemittanceLicNO
					MCLicNo.AdvancedSearch.Save(); // MCLicNo
					BankYesNo.AdvancedSearch.Save(); // BankYesNo
					BankODLimit.AdvancedSearch.Save(); // BankODLimit
					BankAcctNO.AdvancedSearch.Save(); // BankAcctNO
					CreditLimit.AdvancedSearch.Save(); // CreditLimit
					ReferBy.AdvancedSearch.Save(); // ReferBy
					AgentImageName.AdvancedSearch.Save(); // AgentImageName
					status.AdvancedSearch.Save(); // status
					CreatedBy.AdvancedSearch.Save(); // CreatedBy
					CreatedDate.AdvancedSearch.Save(); // CreatedDate
					ModifiedUser.AdvancedSearch.Save(); // ModifiedUser
					ModifiedDate.AdvancedSearch.Save(); // ModifiedDate
					PPExpiryDate.AdvancedSearch.Save(); // PPExpiryDate
					TTExpiryDate.AdvancedSearch.Save(); // TTExpiryDate
					MCExpiryDate.AdvancedSearch.Save(); // MCExpiryDate
					Action.AdvancedSearch.Save(); // Action
					Remark.AdvancedSearch.Save(); // Remark
					MCType.AdvancedSearch.Save(); // MCType
					CustDOB.AdvancedSearch.Save(); // CustDOB
					DefContactDOB.AdvancedSearch.Save(); // DefContactDOB
					ScanImage.AdvancedSearch.Save(); // ScanImage
					BizNature.AdvancedSearch.Save(); // BizNature
					DefContactPOB.AdvancedSearch.Save(); // DefContactPOB
					NewTran.AdvancedSearch.Save(); // NewTran
					BizRegNo.AdvancedSearch.Save(); // BizRegNo
					BizRegDate.AdvancedSearch.Save(); // BizRegDate
					BizRegPlace.AdvancedSearch.Save(); // BizRegPlace
					BizRegExpDate.AdvancedSearch.Save(); // BizRegExpDate
					UnIncorpExec.AdvancedSearch.Save(); // UnIncorpExec
					DefContactAuthorzLetter.AdvancedSearch.Save(); // DefContactAuthorzLetter
					Politician.AdvancedSearch.Save(); // Politician
					BizPartnerNo.AdvancedSearch.Save(); // BizPartnerNo
					Remark2.AdvancedSearch.Save(); // Remark2
					BannedListRemark.AdvancedSearch.Save(); // BannedListRemark
				}
				return sWhere;
			}

			// Build search SQL
			public void BuildSearchSql(ref string Where, cField Fld, bool Def, bool MultiValue) {
				string FldParm = Fld.FldVar.Substring(2);
				string FldVal = (Def) ? Convert.ToString(Fld.AdvancedSearch.SearchValueDefault) : Convert.ToString(Fld.AdvancedSearch.SearchValue);
				string FldOpr = (Def) ? Fld.AdvancedSearch.SearchOperatorDefault : Fld.AdvancedSearch.SearchOperator;
				string FldCond = (Def) ? Fld.AdvancedSearch.SearchConditionDefault : Fld.AdvancedSearch.SearchCondition;
				string FldVal2 = (Def) ? Convert.ToString(Fld.AdvancedSearch.SearchValue2Default) : Convert.ToString(Fld.AdvancedSearch.SearchValue2);
				string FldOpr2 = (Def) ? Fld.AdvancedSearch.SearchOperator2Default : Fld.AdvancedSearch.SearchOperator2;
				string sWrk = "";
				FldOpr = FldOpr.Trim().ToUpper();
				if (ew_Empty(FldOpr)) FldOpr = "=";
				FldOpr2 = FldOpr2.Trim().ToUpper();
				if (ew_Empty(FldOpr2)) FldOpr2 = "=";
				if (EW_SEARCH_MULTI_VALUE_OPTION == 1)
					MultiValue = false;
				if (MultiValue) {
					string sWrk1 = (ew_NotEmpty(FldVal)) ? ew_GetMultiSearchSql(Fld, FldOpr, FldVal, DBID) : ""; // Field value 1
					string sWrk2 = (ew_NotEmpty(FldVal2)) ? ew_GetMultiSearchSql(Fld, FldOpr2, FldVal2, DBID) : ""; // Field value 2
					sWrk = sWrk1; // Build final SQL
					if (ew_NotEmpty(sWrk2))
						sWrk = (ew_NotEmpty(sWrk)) ? "(" + sWrk + ") " + FldCond + " (" + sWrk2 + ")" : sWrk2;
				} else {
					FldVal = ConvertSearchValue(Fld, FldVal);
					FldVal2 = ConvertSearchValue(Fld, FldVal2);
					sWrk = ew_GetSearchSql(Fld, FldVal, FldOpr, FldCond, FldVal2, FldOpr2, DBID);
				}
				ew_AddFilter(ref Where, sWrk);
			}

			// Convert search value
			public string ConvertSearchValue(cField Fld, string FldVal) {
				if (FldVal == EW_NULL_VALUE || FldVal == EW_NOT_NULL_VALUE)
					return FldVal;
				string Value = FldVal;
				if (Fld.FldDataType == EW_DATATYPE_BOOLEAN) {
				} else if (Fld.FldDataType == EW_DATATYPE_DATE || Fld.FldDataType == EW_DATATYPE_TIME) {
					if (ew_NotEmpty(FldVal)) Value = ew_UnformatDateTime(FldVal, Fld.FldDateTimeFormat);
				}
				return Value;
			}

			// Check if search parm exists
			public bool CheckSearchParms() {
				if (AgentId.AdvancedSearch.IssetSession)
					return true;
				if (AgentName.AdvancedSearch.IssetSession)
					return true;
				if (AgentRiskRating.AdvancedSearch.IssetSession)
					return true;
				if (AgentRiskCredit.AdvancedSearch.IssetSession)
					return true;
				if (Address1.AdvancedSearch.IssetSession)
					return true;
				if (Address2.AdvancedSearch.IssetSession)
					return true;
				if (Address3.AdvancedSearch.IssetSession)
					return true;
				if (Country.AdvancedSearch.IssetSession)
					return true;
				if (ZipCode.AdvancedSearch.IssetSession)
					return true;
				if (Fax.AdvancedSearch.IssetSession)
					return true;
				if (Phone.AdvancedSearch.IssetSession)
					return true;
				if (Mobile.AdvancedSearch.IssetSession)
					return true;
				if (BuzType.AdvancedSearch.IssetSession)
					return true;
				if (ClassType.AdvancedSearch.IssetSession)
					return true;
				if (DefContactPName.AdvancedSearch.IssetSession)
					return true;
				if (DefContactPNric.AdvancedSearch.IssetSession)
					return true;
				if (DefContactPNation.AdvancedSearch.IssetSession)
					return true;
				if (DefContactPOccupation.AdvancedSearch.IssetSession)
					return true;
				if (TermsId.AdvancedSearch.IssetSession)
					return true;
				if (LedgerBal.AdvancedSearch.IssetSession)
					return true;
				if (AvailableBal.AdvancedSearch.IssetSession)
					return true;
				if (_Email.AdvancedSearch.IssetSession)
					return true;
				if (URL.AdvancedSearch.IssetSession)
					return true;
				if (CustType.AdvancedSearch.IssetSession)
					return true;
				if (RemittanceLicNO.AdvancedSearch.IssetSession)
					return true;
				if (MCLicNo.AdvancedSearch.IssetSession)
					return true;
				if (BankYesNo.AdvancedSearch.IssetSession)
					return true;
				if (BankODLimit.AdvancedSearch.IssetSession)
					return true;
				if (BankAcctNO.AdvancedSearch.IssetSession)
					return true;
				if (CreditLimit.AdvancedSearch.IssetSession)
					return true;
				if (ReferBy.AdvancedSearch.IssetSession)
					return true;
				if (AgentImageName.AdvancedSearch.IssetSession)
					return true;
				if (status.AdvancedSearch.IssetSession)
					return true;
				if (CreatedBy.AdvancedSearch.IssetSession)
					return true;
				if (CreatedDate.AdvancedSearch.IssetSession)
					return true;
				if (ModifiedUser.AdvancedSearch.IssetSession)
					return true;
				if (ModifiedDate.AdvancedSearch.IssetSession)
					return true;
				if (PPExpiryDate.AdvancedSearch.IssetSession)
					return true;
				if (TTExpiryDate.AdvancedSearch.IssetSession)
					return true;
				if (MCExpiryDate.AdvancedSearch.IssetSession)
					return true;
				if (Action.AdvancedSearch.IssetSession)
					return true;
				if (Remark.AdvancedSearch.IssetSession)
					return true;
				if (MCType.AdvancedSearch.IssetSession)
					return true;
				if (CustDOB.AdvancedSearch.IssetSession)
					return true;
				if (DefContactDOB.AdvancedSearch.IssetSession)
					return true;
				if (ScanImage.AdvancedSearch.IssetSession)
					return true;
				if (BizNature.AdvancedSearch.IssetSession)
					return true;
				if (DefContactPOB.AdvancedSearch.IssetSession)
					return true;
				if (NewTran.AdvancedSearch.IssetSession)
					return true;
				if (BizRegNo.AdvancedSearch.IssetSession)
					return true;
				if (BizRegDate.AdvancedSearch.IssetSession)
					return true;
				if (BizRegPlace.AdvancedSearch.IssetSession)
					return true;
				if (BizRegExpDate.AdvancedSearch.IssetSession)
					return true;
				if (UnIncorpExec.AdvancedSearch.IssetSession)
					return true;
				if (DefContactAuthorzLetter.AdvancedSearch.IssetSession)
					return true;
				if (Politician.AdvancedSearch.IssetSession)
					return true;
				if (BizPartnerNo.AdvancedSearch.IssetSession)
					return true;
				if (Remark2.AdvancedSearch.IssetSession)
					return true;
				if (BannedListRemark.AdvancedSearch.IssetSession)
					return true;
				return false;
			}

			// Clear all search parameters
			public void ResetSearchParms() {
				SearchWhere = "";
				SessionSearchWhere = SearchWhere;

				// Clear advanced search parameters
				ResetAdvancedSearchParms();
			}

			// Load advanced search default values
			public bool LoadAdvancedSearchDefault() {
				return false;
			}

			// Clear all advanced search parameters
			public void ResetAdvancedSearchParms() {
				AgentId.AdvancedSearch.UnsetSession();
				AgentName.AdvancedSearch.UnsetSession();
				AgentRiskRating.AdvancedSearch.UnsetSession();
				AgentRiskCredit.AdvancedSearch.UnsetSession();
				Address1.AdvancedSearch.UnsetSession();
				Address2.AdvancedSearch.UnsetSession();
				Address3.AdvancedSearch.UnsetSession();
				Country.AdvancedSearch.UnsetSession();
				ZipCode.AdvancedSearch.UnsetSession();
				Fax.AdvancedSearch.UnsetSession();
				Phone.AdvancedSearch.UnsetSession();
				Mobile.AdvancedSearch.UnsetSession();
				BuzType.AdvancedSearch.UnsetSession();
				ClassType.AdvancedSearch.UnsetSession();
				DefContactPName.AdvancedSearch.UnsetSession();
				DefContactPNric.AdvancedSearch.UnsetSession();
				DefContactPNation.AdvancedSearch.UnsetSession();
				DefContactPOccupation.AdvancedSearch.UnsetSession();
				TermsId.AdvancedSearch.UnsetSession();
				LedgerBal.AdvancedSearch.UnsetSession();
				AvailableBal.AdvancedSearch.UnsetSession();
				_Email.AdvancedSearch.UnsetSession();
				URL.AdvancedSearch.UnsetSession();
				CustType.AdvancedSearch.UnsetSession();
				RemittanceLicNO.AdvancedSearch.UnsetSession();
				MCLicNo.AdvancedSearch.UnsetSession();
				BankYesNo.AdvancedSearch.UnsetSession();
				BankODLimit.AdvancedSearch.UnsetSession();
				BankAcctNO.AdvancedSearch.UnsetSession();
				CreditLimit.AdvancedSearch.UnsetSession();
				ReferBy.AdvancedSearch.UnsetSession();
				AgentImageName.AdvancedSearch.UnsetSession();
				status.AdvancedSearch.UnsetSession();
				CreatedBy.AdvancedSearch.UnsetSession();
				CreatedDate.AdvancedSearch.UnsetSession();
				ModifiedUser.AdvancedSearch.UnsetSession();
				ModifiedDate.AdvancedSearch.UnsetSession();
				PPExpiryDate.AdvancedSearch.UnsetSession();
				TTExpiryDate.AdvancedSearch.UnsetSession();
				MCExpiryDate.AdvancedSearch.UnsetSession();
				Action.AdvancedSearch.UnsetSession();
				Remark.AdvancedSearch.UnsetSession();
				MCType.AdvancedSearch.UnsetSession();
				CustDOB.AdvancedSearch.UnsetSession();
				DefContactDOB.AdvancedSearch.UnsetSession();
				ScanImage.AdvancedSearch.UnsetSession();
				BizNature.AdvancedSearch.UnsetSession();
				DefContactPOB.AdvancedSearch.UnsetSession();
				NewTran.AdvancedSearch.UnsetSession();
				BizRegNo.AdvancedSearch.UnsetSession();
				BizRegDate.AdvancedSearch.UnsetSession();
				BizRegPlace.AdvancedSearch.UnsetSession();
				BizRegExpDate.AdvancedSearch.UnsetSession();
				UnIncorpExec.AdvancedSearch.UnsetSession();
				DefContactAuthorzLetter.AdvancedSearch.UnsetSession();
				Politician.AdvancedSearch.UnsetSession();
				BizPartnerNo.AdvancedSearch.UnsetSession();
				Remark2.AdvancedSearch.UnsetSession();
				BannedListRemark.AdvancedSearch.UnsetSession();
			}

			// Restore all search parameters
			public void RestoreSearchParms() {
				RestoreSearch = true;

				// Restore advanced search values
				AgentId.AdvancedSearch.Load();
				AgentName.AdvancedSearch.Load();
				AgentRiskRating.AdvancedSearch.Load();
				AgentRiskCredit.AdvancedSearch.Load();
				Address1.AdvancedSearch.Load();
				Address2.AdvancedSearch.Load();
				Address3.AdvancedSearch.Load();
				Country.AdvancedSearch.Load();
				ZipCode.AdvancedSearch.Load();
				Fax.AdvancedSearch.Load();
				Phone.AdvancedSearch.Load();
				Mobile.AdvancedSearch.Load();
				BuzType.AdvancedSearch.Load();
				ClassType.AdvancedSearch.Load();
				DefContactPName.AdvancedSearch.Load();
				DefContactPNric.AdvancedSearch.Load();
				DefContactPNation.AdvancedSearch.Load();
				DefContactPOccupation.AdvancedSearch.Load();
				TermsId.AdvancedSearch.Load();
				LedgerBal.AdvancedSearch.Load();
				AvailableBal.AdvancedSearch.Load();
				_Email.AdvancedSearch.Load();
				URL.AdvancedSearch.Load();
				CustType.AdvancedSearch.Load();
				RemittanceLicNO.AdvancedSearch.Load();
				MCLicNo.AdvancedSearch.Load();
				BankYesNo.AdvancedSearch.Load();
				BankODLimit.AdvancedSearch.Load();
				BankAcctNO.AdvancedSearch.Load();
				CreditLimit.AdvancedSearch.Load();
				ReferBy.AdvancedSearch.Load();
				AgentImageName.AdvancedSearch.Load();
				status.AdvancedSearch.Load();
				CreatedBy.AdvancedSearch.Load();
				CreatedDate.AdvancedSearch.Load();
				ModifiedUser.AdvancedSearch.Load();
				ModifiedDate.AdvancedSearch.Load();
				PPExpiryDate.AdvancedSearch.Load();
				TTExpiryDate.AdvancedSearch.Load();
				MCExpiryDate.AdvancedSearch.Load();
				Action.AdvancedSearch.Load();
				Remark.AdvancedSearch.Load();
				MCType.AdvancedSearch.Load();
				CustDOB.AdvancedSearch.Load();
				DefContactDOB.AdvancedSearch.Load();
				ScanImage.AdvancedSearch.Load();
				BizNature.AdvancedSearch.Load();
				DefContactPOB.AdvancedSearch.Load();
				NewTran.AdvancedSearch.Load();
				BizRegNo.AdvancedSearch.Load();
				BizRegDate.AdvancedSearch.Load();
				BizRegPlace.AdvancedSearch.Load();
				BizRegExpDate.AdvancedSearch.Load();
				UnIncorpExec.AdvancedSearch.Load();
				DefContactAuthorzLetter.AdvancedSearch.Load();
				Politician.AdvancedSearch.Load();
				BizPartnerNo.AdvancedSearch.Load();
				Remark2.AdvancedSearch.Load();
				BannedListRemark.AdvancedSearch.Load();
			}

			// Set up sort parameters
			public void SetUpSortOrder() {

				// Check for "order" parameter
				if (ew_NotEmpty(ew_Get("order"))) {
					CurrentOrder = ew_Get("order");
					CurrentOrderType = ew_Get("ordertype");
					UpdateSort(AgentId); // AgentId
					UpdateSort(AgentName); // AgentName
					UpdateSort(AgentRiskRating); // AgentRiskRating
					UpdateSort(AgentRiskCredit); // AgentRiskCredit
					UpdateSort(Address1); // Address1
					UpdateSort(Address2); // Address2
					UpdateSort(Address3); // Address3
					UpdateSort(Country); // Country
					UpdateSort(ZipCode); // ZipCode
					UpdateSort(Fax); // Fax
					UpdateSort(Phone); // Phone
					UpdateSort(Mobile); // Mobile
					UpdateSort(BuzType); // BuzType
					UpdateSort(ClassType); // ClassType
					UpdateSort(DefContactPName); // DefContactPName
					UpdateSort(DefContactPNric); // DefContactPNric
					UpdateSort(DefContactPNation); // DefContactPNation
					UpdateSort(DefContactPOccupation); // DefContactPOccupation
					UpdateSort(TermsId); // TermsId
					UpdateSort(LedgerBal); // LedgerBal
					UpdateSort(AvailableBal); // AvailableBal
					UpdateSort(_Email); // _Email
					UpdateSort(URL); // URL
					UpdateSort(CustType); // CustType
					UpdateSort(RemittanceLicNO); // RemittanceLicNO
					UpdateSort(MCLicNo); // MCLicNo
					UpdateSort(BankYesNo); // BankYesNo
					UpdateSort(BankODLimit); // BankODLimit
					UpdateSort(BankAcctNO); // BankAcctNO
					UpdateSort(CreditLimit); // CreditLimit
					UpdateSort(ReferBy); // ReferBy
					UpdateSort(AgentImageName); // AgentImageName
					UpdateSort(status); // status
					UpdateSort(CreatedBy); // CreatedBy
					UpdateSort(CreatedDate); // CreatedDate
					UpdateSort(ModifiedUser); // ModifiedUser
					UpdateSort(ModifiedDate); // ModifiedDate
					UpdateSort(PPExpiryDate); // PPExpiryDate
					UpdateSort(TTExpiryDate); // TTExpiryDate
					UpdateSort(MCExpiryDate); // MCExpiryDate
					UpdateSort(Action); // Action
					UpdateSort(Remark); // Remark
					UpdateSort(MCType); // MCType
					UpdateSort(CustDOB); // CustDOB
					UpdateSort(DefContactDOB); // DefContactDOB
					UpdateSort(ScanImage); // ScanImage
					UpdateSort(BizNature); // BizNature
					UpdateSort(DefContactPOB); // DefContactPOB
					UpdateSort(NewTran); // NewTran
					UpdateSort(BizRegNo); // BizRegNo
					UpdateSort(BizRegDate); // BizRegDate
					UpdateSort(BizRegPlace); // BizRegPlace
					UpdateSort(BizRegExpDate); // BizRegExpDate
					UpdateSort(UnIncorpExec); // UnIncorpExec
					UpdateSort(DefContactAuthorzLetter); // DefContactAuthorzLetter
					UpdateSort(Politician); // Politician
					UpdateSort(BizPartnerNo); // BizPartnerNo
					UpdateSort(Remark2); // Remark2
					UpdateSort(BannedListRemark); // BannedListRemark
					StartRecordNumber = 1; // Reset start position
				}
			}

			// Load sort order parameters
			public void LoadSortOrder() {
				string sOrderBy = SessionOrderBy; // Get Order By from Session
				if (ew_Empty(sOrderBy)) {
					if (ew_NotEmpty(SqlOrderBy)) {
						sOrderBy = SqlOrderBy;
						SessionOrderBy = sOrderBy;
					}
				}
			}

			// Reset command
			// cmd=reset (Reset search parameters)
			// cmd=resetall (Reset search and master/detail parameters)
			// cmd=resetsort (Reset sort parameters)

			public void ResetCmd() {

				// Get reset cmd
				if (Command.ToLower().StartsWith("reset")) {

					// Reset search criteria
					if (ew_SameText(Command, "reset") || ew_SameText(Command, "resetall"))
						ResetSearchParms();

					// Reset sorting order
					if (ew_SameText(Command, "resetsort")) {
						string sOrderBy = "";
						SessionOrderBy = sOrderBy;
						AgentId.Sort = "";
						AgentName.Sort = "";
						AgentRiskRating.Sort = "";
						AgentRiskCredit.Sort = "";
						Address1.Sort = "";
						Address2.Sort = "";
						Address3.Sort = "";
						Country.Sort = "";
						ZipCode.Sort = "";
						Fax.Sort = "";
						Phone.Sort = "";
						Mobile.Sort = "";
						BuzType.Sort = "";
						ClassType.Sort = "";
						DefContactPName.Sort = "";
						DefContactPNric.Sort = "";
						DefContactPNation.Sort = "";
						DefContactPOccupation.Sort = "";
						TermsId.Sort = "";
						LedgerBal.Sort = "";
						AvailableBal.Sort = "";
						_Email.Sort = "";
						URL.Sort = "";
						CustType.Sort = "";
						RemittanceLicNO.Sort = "";
						MCLicNo.Sort = "";
						BankYesNo.Sort = "";
						BankODLimit.Sort = "";
						BankAcctNO.Sort = "";
						CreditLimit.Sort = "";
						ReferBy.Sort = "";
						AgentImageName.Sort = "";
						status.Sort = "";
						CreatedBy.Sort = "";
						CreatedDate.Sort = "";
						ModifiedUser.Sort = "";
						ModifiedDate.Sort = "";
						PPExpiryDate.Sort = "";
						TTExpiryDate.Sort = "";
						MCExpiryDate.Sort = "";
						Action.Sort = "";
						Remark.Sort = "";
						MCType.Sort = "";
						CustDOB.Sort = "";
						DefContactDOB.Sort = "";
						ScanImage.Sort = "";
						BizNature.Sort = "";
						DefContactPOB.Sort = "";
						NewTran.Sort = "";
						BizRegNo.Sort = "";
						BizRegDate.Sort = "";
						BizRegPlace.Sort = "";
						BizRegExpDate.Sort = "";
						UnIncorpExec.Sort = "";
						DefContactAuthorzLetter.Sort = "";
						Politician.Sort = "";
						BizPartnerNo.Sort = "";
						Remark2.Sort = "";
						BannedListRemark.Sort = "";
					}

					// Reset start position
					StartRec = 1;
					StartRecordNumber = StartRec;
				}
			}

			// Set up list options
			public void SetupListOptions() {
				cListOption item;

				// "griddelete"
				if (AllowAddDeleteRow) {
					item = ListOptions.Add("griddelete");
					item.CssStyle = "white-space: nowrap;";
					item.OnLeft = true;
					item.Visible = false; // Default hidden
				}

				// Add group option item
				item = ListOptions.Add(ListOptions.GroupOptionName);
				item.Body = "";
				item.OnLeft = true;
				item.Visible = false;

				// "detail_AgentBank"
				item = ListOptions.Add("detail_AgentBank");
				item.CssStyle = "white-space: nowrap;";
				item.Visible = true && !ShowMultipleDetails;
				item.OnLeft = true;
				item.ShowInButtonGroup = false;
				AgentBank_grid = AgentBank_grid ?? new cAgentBank_grid();

				// Multiple details
				if (ShowMultipleDetails) {
					item = ListOptions.Add("details");
					item.CssStyle = "white-space: nowrap;";
					item.Visible = ShowMultipleDetails;
					item.OnLeft = true;
					item.ShowInButtonGroup = false;
				}

				// Set up detail pages
				var _pages = new cSubPages();
				_pages.Add("AgentBank");
				DetailPages = _pages;

				// List actions
				item = ListOptions.Add("listactions");
				item.CssStyle = "white-space: nowrap;";
				item.OnLeft = true;
				item.Visible = false;
				item.ShowInButtonGroup = false;
				item.ShowInDropDown = false;

				// "checkbox"
				item = ListOptions.Add("checkbox");
				item.CssStyle = "white-space: nowrap; text-align: center; vertical-align: middle; margin: 0px;";
				item.Visible = false;
				item.OnLeft = true;
				item.Header = "<input type=\"checkbox\" name=\"key\" id=\"key\" onclick=\"ew_SelectAllKey(this);\">";
				item.MoveTo(0);
				item.ShowInDropDown = false;
				item.ShowInButtonGroup = false;

				// Drop down button for ListOptions
				ListOptions.UseImageAndText = true;
				ListOptions.UseDropDownButton = false;
				ListOptions.DropDownButtonPhrase = Language.Phrase("ButtonListOptions");
				ListOptions.UseButtonGroup = false;
				if (ListOptions.UseButtonGroup && ew_IsMobile())
					ListOptions.UseDropDownButton = true;
				ListOptions.ButtonClass = "btn-sm"; // Class for button group

				// Call ListOptions_Load event
				ListOptions_Load();
				SetupListOptionsExt();
				item = ListOptions[ListOptions.GroupOptionName];
				item.Visible = ListOptions.GroupOptionVisible;
			}

			// Render list options
			#pragma warning disable 168, 219
			public void RenderListOptions() {
				cListOption oListOpt;
				var isVisible = false; // DN
				ListOptions.LoadDefault();
				string KeyName = "";

				// Set up row action and key
				if (ew_IsNumeric(RowIndex) && CurrentMode != "view") {
					ObjForm.Index = ew_ConvertToInt(RowIndex);
					var ActionName = FormActionName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					var OldKeyName = FormOldKeyName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					KeyName = FormKeyName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					var BlankRowName = FormBlankRowName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					if (ew_NotEmpty(RowAction))
						MultiSelectKey += "<input type=\"hidden\" name=\"" + ActionName + "\" id=\"" + ActionName + "\" value=\"" + RowAction + "\">";
					if (RowAction == "delete") {
						string rowkey = ObjForm.GetValue(FormKeyName);
						SetupKeyValues(rowkey);
					}
					if (RowAction == "insert" && CurrentAction == "F" && EmptyRow())
						MultiSelectKey += "<input type=\"hidden\" name=\"" + BlankRowName + "\" id=\"" + BlankRowName + "\" value=\"1\">";
				}

				// "delete"
				if (AllowAddDeleteRow) {
					if (CurrentAction == "gridadd" || CurrentAction == "gridedit") {
						var option = ListOptions;
						option.UseButtonGroup = true; // Use button group for grid delete button
						option.UseImageAndText = true; // Use image and text for grid delete button
						oListOpt = option["griddelete"];
						if (ew_IsNumeric(RowIndex) && (RowAction == "" || RowAction == "edit")) { // Do not allow delete existing record
							oListOpt.Body = "&nbsp;";
						} else {
							oListOpt.Body = "<a class=\"ewGridLink ewGridDelete\" title=\"" + ew_HtmlTitle(Language.Phrase("DeleteLink")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("DeleteLink")) + "\" onclick=\"return ew_DeleteGridRow(this, " + RowIndex + ");\">" + Language.Phrase("DeleteLink") + "</a>";
						}
					}
				}

				// Set up list action buttons
				oListOpt = ListOptions["listactions"];
				if (oListOpt != null && Export == "" && CurrentAction == "") {
					string body = "";
					var links = new List<string>();
					foreach (KeyValuePair<string, cListAction> kvp in ListActions.Items) {
						if (kvp.Value.Select == EW_ACTION_SINGLE && kvp.Value.Allow) {
							var action = kvp.Value.Action;
							string caption = kvp.Value.Caption;
							var icon = (kvp.Value.Icon != "") ? "<span class=\"" + ew_HtmlEncode(kvp.Value.Icon.Replace(" ewIcon", "")) + "\" data-caption=\"" + ew_HtmlTitle(caption) + "\"></span> " : "";
							links.Add("<li><a class=\"ewAction ewListAction\" data-action=\"" + ew_HtmlEncode(action) + "\" data-caption=\"" + ew_HtmlTitle(caption) + "\" href=\"\" onclick=\"ew_SubmitAction(event,jQuery.extend({key:" + KeyToJson() + "}," + kvp.Value.ToJson(true) + ")); return false;\">" + icon + kvp.Value.Caption + "</a></li>");
							if (links.Count == 1) // Single button
								body = "<a class=\"ewAction ewListAction\" data-action=\"" + ew_HtmlEncode(action) + "\" title=\"" + ew_HtmlTitle(caption) + "\" data-caption=\"" + ew_HtmlTitle(caption) + "\" href=\"\" onclick=\"ew_SubmitAction(event,jQuery.extend({key:" + KeyToJson() + "}," + kvp.Value.ToJson(true) + ")); return false;\">" + Language.Phrase("ListActionButton") + "</a>";
						}
					}
					if (links.Count > 1) { // More than one buttons, use dropdown
						body = "<button class=\"dropdown-toggle btn btn-default btn-sm ewActions\" title=\"" + ew_HtmlTitle(Language.Phrase("ListActionButton")) + "\" data-toggle=\"dropdown\">" + Language.Phrase("ListActionButton") + "<b class=\"caret\"></b></button>";
						string content = "";
						foreach (var link in links)
							content += "<li>" + link + "</li>";
						body += "<ul class=\"dropdown-menu" + (oListOpt.OnLeft ? "" : " dropdown-menu-right") + "\">" + content + "</ul>";
						body = "<div class=\"btn-group\">" + body + "</div>";
					}
					if (links.Count > 0) {
						oListOpt.Body = body;
						oListOpt.Visible = true;
					}
				}
				var DetailViewTblVar = "";
				var DetailCopyTblVar = "";
				var DetailEditTblVar = "";

				// "detail_AgentBank"
				oListOpt = ListOptions["detail_AgentBank"];
				isVisible = true;
				if (isVisible) {
					var body = Language.Phrase("DetailLink") + Language.TablePhrase("AgentBank", "TblCaption");
					body = "<a class=\"btn btn-default btn-sm ewRowLink ewDetailList\" data-action=\"list\" href=\"" + ew_HtmlEncode(ew_AppPath("AgentBanklist?" + EW_TABLE_SHOW_MASTER + "=Agent&fk_AgentId=" + ew_UrlEncode(Convert.ToString(AgentId.CurrentValue)) + "")) + "\">" + body + "</a>";
					string links = "";
					if (ew_NotEmpty(links)) {
						body += "<button class=\"dropdown-toggle btn btn-default btn-sm ewDetail\" data-toggle=\"dropdown\"><b class=\"caret\"></b></button>";
						body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
					}
					body = "<div class=\"btn-group\">" + body + "</div>";
					oListOpt.Body = body;
					if (ShowMultipleDetails) oListOpt.Visible = false;
				}
				if (ShowMultipleDetails) {
					var body = Language.Phrase("MultipleMasterDetails");
					body = "<div class=\"btn-group\">";
					string links = "";
					if (ew_NotEmpty(DetailViewTblVar)) {
						links += "<li><a class=\"ewRowLink ewDetailView\" data-action=\"view\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("MasterDetailViewLink")) + "\" href=\"" + ew_HtmlEncode(ew_AppPath(GetViewUrl(EW_TABLE_SHOW_DETAIL + "=" + DetailViewTblVar))) + "\">" + ew_HtmlImageAndText(Language.Phrase("MasterDetailViewLink")) + "</a></li>";
					}
					if (ew_NotEmpty(DetailEditTblVar)) {
						links += "<li><a class=\"ewRowLink ewDetailEdit\" data-action=\"edit\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("MasterDetailEditLink")) + "\" href=\"" + ew_HtmlEncode(ew_AppPath(GetEditUrl(EW_TABLE_SHOW_DETAIL + "=" + DetailEditTblVar))) + "\">" + ew_HtmlImageAndText(Language.Phrase("MasterDetailEditLink")) + "</a></li>";
					}
					if (ew_NotEmpty(DetailCopyTblVar)) {
						links += "<li><a class=\"ewRowLink ewDetailCopy\" data-action=\"add\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("MasterDetailCopyLink")) + "\" href=\"" + ew_HtmlEncode(ew_AppPath(GetCopyUrl(EW_TABLE_SHOW_DETAIL + "=" + DetailCopyTblVar))) + "\">" + ew_HtmlImageAndText(Language.Phrase("MasterDetailCopyLink")) + "</a></li>";
					}
					if (ew_NotEmpty(links)) {
						body += "<button class=\"dropdown-toggle btn btn-default btn-sm ewMasterDetail\" title=\"" + ew_HtmlTitle(Language.Phrase("MultipleMasterDetails")) + "\" data-toggle=\"dropdown\">" + Language.Phrase("MultipleMasterDetails") + "<b class=\"caret\"></b></button>";
						body += "<ul class=\"dropdown-menu ewMenu\">" + links + "</ul>";
					}
					body += "</div>";

					// Multiple details
					oListOpt = ListOptions["details"];
					oListOpt.Body = body;
				}

				// "checkbox"
				oListOpt = ListOptions["checkbox"];
				oListOpt.Body = "<input type=\"checkbox\" name=\"key_m\" value=\"" + ew_HtmlEncode(AgentId.CurrentValue) + "\" onclick='ew_ClickMultiCheckbox(event);'>";
				if (CurrentAction == "gridedit" && ew_IsNumeric(RowIndex)) {
					MultiSelectKey += "<input type=\"hidden\" name=\"" + KeyName + "\" id=\"" + KeyName + "\" value=\"" + AgentId.CurrentValue + "\">";
				}
				RenderListOptionsExt();

				// Call ListOptions_Rendered event
				ListOptions_Rendered();
			}

			// Set up other options
			public void SetupOtherOptions() {
				cListOptions option;
				cListOption item;
				var options = OtherOptions;
				option = options["addedit"];
				item = option.Add("gridadd");
				item.Body = "<a class=\"ewAddEdit ewGridAdd\" title=\"" + ew_HtmlTitle(Language.Phrase("GridAddLink")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("GridAddLink")) + "\" href=\"" + ew_HtmlEncode(ew_AppPath(GridAddUrl)) + "\">" + Language.Phrase("GridAddLink") + "</a>";
				item.Visible = (GridAddUrl != "");

				// Add grid edit
				option = options["addedit"];
				item = option.Add("gridedit");
				item.Body = "<a class=\"ewAddEdit ewGridEdit\" title=\"" + ew_HtmlTitle(Language.Phrase("GridEditLink")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("GridEditLink")) + "\" href=\"" + ew_HtmlEncode(ew_AppPath(GridEditUrl)) + "\">" + Language.Phrase("GridEditLink") + "</a>";
				item.Visible = (GridEditUrl != "");
				option = options["action"];

				// Set up options default
				foreach (var kvp in options) {
					var opt = kvp.Value;
					opt.UseImageAndText = true;
					opt.UseDropDownButton = false;
					opt.UseButtonGroup = true;
					opt.ButtonClass = "btn-sm"; // Class for button group
					item = opt.Add(opt.GroupOptionName);
					item.Body = "";
					item.Visible = false;
				}
				options["addedit"].DropDownButtonPhrase = Language.Phrase("ButtonAddEdit");
				options["detail"].DropDownButtonPhrase = Language.Phrase("ButtonDetails");
				options["action"].DropDownButtonPhrase = Language.Phrase("ButtonActions");

				// Filter button
				item = FilterOptions.Add("savecurrentfilter");
				item.Body = "<a class=\"ewSaveFilter\" data-form=\"fAgentlistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ewDeleteFilter\" data-form=\"fAgentlistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
				item.Visible = true;
				FilterOptions.UseDropDownButton = true;
				FilterOptions.UseButtonGroup = !FilterOptions.UseDropDownButton;
				FilterOptions.DropDownButtonPhrase = Language.Phrase("Filters");

				// Add group option item
				item = FilterOptions.Add(FilterOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;
			}

			// Render all other options
			public void ForEachOtherOption(Action<KeyValuePair<string, cListOptions>> action) {
				OtherOptions.ToList().ForEach(action);
			}

			// Render other options
			public void RenderOtherOptions() {
				cListOptions option;
				cListOption item;
				var options = OtherOptions;
				if (CurrentAction != "gridadd" && CurrentAction != "gridedit") { // Not grid add/edit mode
					option = options["action"];

					// Set up list action buttons
					foreach (KeyValuePair<string, cListAction> kvp in ListActions.Items) {
						if (kvp.Value.Select == EW_ACTION_MULTIPLE) {
							item = option.Add("custom_" + kvp.Value.Action);
							string caption = kvp.Value.Caption;
							var icon = (kvp.Value.Icon != "") ? "<span class=\"" + ew_HtmlEncode(kvp.Value.Icon) + "\" data-caption=\"" + ew_HtmlEncode(caption) + "\"></span> " : caption;
							item.Body = "<a class=\"ewAction ewListAction\" title=\"" + ew_HtmlEncode(caption) + "\" data-caption=\"" + ew_HtmlEncode(caption) + "\" href=\"\" onclick=\"ew_SubmitAction(event,jQuery.extend({f:document.fAgentlist}," + kvp.Value.ToJson(true) + ")); return false;\">" + icon + "</a>";
							item.Visible = kvp.Value.Allow;
						}
					}

					// Hide grid edit and other options
					if (TotalRecs <= 0) {
						option = options["addedit"];
						item = option["gridedit"];
						if (item != null)
							item.Visible = false;
						option = options["action"];
						option.HideAllOptions();
					}
				} else { // Grid add/edit mode

					// Hide all options first
					foreach (var kvp in options)
						kvp.Value.HideAllOptions();
					if (CurrentAction == "gridadd") {
						if (AllowAddDeleteRow) {

							// Add add blank row
							option = options["addedit"];
							option.UseDropDownButton = false;
							option.UseImageAndText = true;
							item = option.Add("addblankrow");
							item.Body = "<a class=\"ewAddEdit ewAddBlankRow\" title=\"" + ew_HtmlTitle(Language.Phrase("AddBlankRow")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("AddBlankRow")) + "\" href=\"javascript:void(0);\" onclick=\"ew_AddGridRow(this);\">" + Language.Phrase("AddBlankRow") + "</a>";
							item.Visible = true;
						}
						option = options["action"];
						option.UseDropDownButton = false;
						option.UseImageAndText = true;

						// Add grid insert
						item = option.Add("gridinsert");
						item.Body = "<a class=\"ewAction ewGridInsert\" title=\"" + ew_HtmlTitle(Language.Phrase("GridInsertLink")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("GridInsertLink")) + "\" href=\"\" onclick=\"return ewForms(this).Submit('" + PageName + "');\">" + Language.Phrase("GridInsertLink") + "</a>";

						// Add grid cancel
						item = option.Add("gridcancel");
						item.Body = "<a class=\"ewAction ewGridCancel\" title=\"" + ew_HtmlTitle(Language.Phrase("GridCancelLink")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("GridCancelLink")) + "\" href=\"" + ew_HtmlEncode(ew_AppPath(AddMasterUrl(PageUrl + "a=cancel"))) + "\">" + Language.Phrase("GridCancelLink") + "</a>";
					}
					if (CurrentAction == "gridedit") {
						if (AllowAddDeleteRow) {

							// Add add blank row
							option = options["addedit"];
							option.UseDropDownButton = false;
							option.UseImageAndText = true;
							item = option.Add("addblankrow");
							item.Body = "<a class=\"ewAddEdit ewAddBlankRow\" title=\"" + ew_HtmlTitle(Language.Phrase("AddBlankRow")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("AddBlankRow")) + "\" href=\"javascript:void(0);\" onclick=\"ew_AddGridRow(this);\">" + Language.Phrase("AddBlankRow") + "</a>";
							item.Visible = true;
						}
						option = options["action"];
						option.UseDropDownButton = false;
						option.UseImageAndText = true;
							item = option.Add("gridsave");
							item.Body = "<a class=\"ewAction ewGridSave\" title=\"" + ew_HtmlTitle(Language.Phrase("GridSaveLink")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("GridSaveLink")) + "\" href=\"\" onclick=\"return ewForms(this).Submit('" + PageName + "');\">" + Language.Phrase("GridSaveLink") + "</a>";
							item = option.Add("gridcancel");
							item.Body = "<a class=\"ewAction ewGridCancel\" title=\"" + ew_HtmlTitle(Language.Phrase("GridCancelLink")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("GridCancelLink")) + "\" href=\"" + ew_HtmlEncode(ew_AppPath(AddMasterUrl(PageUrl + "a=cancel"))) + "\">" + Language.Phrase("GridCancelLink") + "</a>";
					}
				}
			}

			// Process list action
			public string ProcessListAction() {
				string errmsg;
				var sFilter = GetKeyFilter();
				var UserAction = ew_Post("useraction");
				if (sFilter != "" && UserAction != "") {

					// Check permission first
					var ActionCaption = UserAction;
					foreach (KeyValuePair<string, cListAction> kvp in ListActions.Items) {
						if (ew_SameStr(kvp.Value, UserAction)) {
							ActionCaption = kvp.Value.Caption;
							if (!kvp.Value.Allow) {
								errmsg = Language.Phrase("CustomActionNotAllowed").Replace("%s", ActionCaption);
								if (ew_Post("ajax") == UserAction) // Ajax
									return "<p class=\"text-danger\">" + errmsg + "</p>";
								else
									FailureMessage = errmsg;
								return "";
							}
						}
					}
					CurrentFilter = sFilter;
					var sSql = SQL;
					var rsuser = Connection.GetRows(sSql);
					CurrentAction = UserAction;

					// Call row custom action event
					if (rsuser != null) {
						Connection.BeginTrans();
						bool Processed = true;
						SelectedCount = rsuser.Count();
						SelectedIndex = 0;
						foreach (var row in rsuser) {
							SelectedIndex++;
							Processed = Row_CustomAction(UserAction, row);
							if (!Processed) break;
						}
						if (Processed) {
							Connection.CommitTrans(); // Commit the changes
							if (ew_Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("CustomActionCompleted").Replace("%s", UserAction); // Set up success message
							} else {
								Connection.RollbackTrans(); // Rollback changes

							// Set up error message
							if (ew_NotEmpty(SuccessMessage) || ew_NotEmpty(FailureMessage)) {

								// Use the message, do nothing
							} else if (ew_NotEmpty(CancelMessage)) {
								FailureMessage = CancelMessage;
								CancelMessage = "";
							} else {
								FailureMessage = Language.Phrase("CustomActionFailed").Replace("%s", ActionCaption);
							}
						}
					}
					CurrentAction = ""; // Clear action
					if (ew_Post("ajax") == UserAction) { // Ajax
						string msg = "";
						if (SuccessMessage != "") {
							msg = "<p class=\"text-success\">" + SuccessMessage + "</p>";
							ClearSuccessMessage(); // Clear message
						}
						if (FailureMessage != "") {
							msg = "<p class=\"text-danger\">" + FailureMessage + "</p>";
							ClearFailureMessage(); // Clear message
						}
						return msg;
					}
				}
				return ""; // Not ajax request
			}

			// Set up search options
			public void SetupSearchOptions() {
				cListOption item;
				SearchOptions = new cListOptions();
				SearchOptions.Tag = "div";
				SearchOptions.TagClassName = "ewSearchOption";

				// Search button
				item = SearchOptions.Add("searchtoggle");
				var SearchToggleClass = (ew_NotEmpty(SearchWhere)) ? " active" : "";
				item.Body = "<button type=\"button\" class=\"btn btn-default ewSearchToggle" + SearchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fAgentlistsrch\">" + Language.Phrase("SearchBtn") + "</button>";
				item.Visible = true;

				// Show all button
				item = SearchOptions.Add("showall");
				item.Body = "<a class=\"btn btn-default ewShowAll\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" href=\"" + ew_AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
				item.Visible = (SearchWhere != DefaultSearchWhere && SearchWhere != "0=101");

				// Button group for search
				SearchOptions.UseDropDownButton = false;
				SearchOptions.UseImageAndText = true;
				SearchOptions.UseButtonGroup = true;
				SearchOptions.DropDownButtonPhrase = Language.Phrase("ButtonSearch");

				// Add group option item
				item = SearchOptions.Add(SearchOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Hide search options
				if (ew_NotEmpty(Export) || CurrentAction == "gridadd" || CurrentAction == "gridedit")
					SearchOptions.HideAllOptions();
			}
			public void SetupListOptionsExt() {
			}
			public void RenderListOptionsExt() {
			}

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

			// Load default values
			public void LoadDefaultValues() {
				AgentId.CurrentValue = System.DBNull.Value;
				AgentId.OldValue = AgentId.CurrentValue;
				AgentName.CurrentValue = System.DBNull.Value;
				AgentName.OldValue = AgentName.CurrentValue;
				AgentRiskRating.CurrentValue = AgentRiskRating.FldDefault;
				AgentRiskRating.OldValue = AgentRiskRating.CurrentValue;
				AgentRiskCredit.CurrentValue = AgentRiskCredit.FldDefault;
				AgentRiskCredit.OldValue = AgentRiskCredit.CurrentValue;
				Address1.CurrentValue = System.DBNull.Value;
				Address1.OldValue = Address1.CurrentValue;
				Address2.CurrentValue = System.DBNull.Value;
				Address2.OldValue = Address2.CurrentValue;
				Address3.CurrentValue = System.DBNull.Value;
				Address3.OldValue = Address3.CurrentValue;
				Country.CurrentValue = System.DBNull.Value;
				Country.OldValue = Country.CurrentValue;
				ZipCode.CurrentValue = System.DBNull.Value;
				ZipCode.OldValue = ZipCode.CurrentValue;
				Fax.CurrentValue = System.DBNull.Value;
				Fax.OldValue = Fax.CurrentValue;
				Phone.CurrentValue = System.DBNull.Value;
				Phone.OldValue = Phone.CurrentValue;
				Mobile.CurrentValue = System.DBNull.Value;
				Mobile.OldValue = Mobile.CurrentValue;
				BuzType.CurrentValue = System.DBNull.Value;
				BuzType.OldValue = BuzType.CurrentValue;
				ClassType.CurrentValue = System.DBNull.Value;
				ClassType.OldValue = ClassType.CurrentValue;
				DefContactPName.CurrentValue = System.DBNull.Value;
				DefContactPName.OldValue = DefContactPName.CurrentValue;
				DefContactPNric.CurrentValue = System.DBNull.Value;
				DefContactPNric.OldValue = DefContactPNric.CurrentValue;
				DefContactPNation.CurrentValue = System.DBNull.Value;
				DefContactPNation.OldValue = DefContactPNation.CurrentValue;
				DefContactPOccupation.CurrentValue = System.DBNull.Value;
				DefContactPOccupation.OldValue = DefContactPOccupation.CurrentValue;
				TermsId.CurrentValue = System.DBNull.Value;
				TermsId.OldValue = TermsId.CurrentValue;
				LedgerBal.CurrentValue = System.DBNull.Value;
				LedgerBal.OldValue = LedgerBal.CurrentValue;
				AvailableBal.CurrentValue = System.DBNull.Value;
				AvailableBal.OldValue = AvailableBal.CurrentValue;
				_Email.CurrentValue = System.DBNull.Value;
				_Email.OldValue = _Email.CurrentValue;
				URL.CurrentValue = System.DBNull.Value;
				URL.OldValue = URL.CurrentValue;
				CustType.CurrentValue = System.DBNull.Value;
				CustType.OldValue = CustType.CurrentValue;
				RemittanceLicNO.CurrentValue = System.DBNull.Value;
				RemittanceLicNO.OldValue = RemittanceLicNO.CurrentValue;
				MCLicNo.CurrentValue = System.DBNull.Value;
				MCLicNo.OldValue = MCLicNo.CurrentValue;
				BankYesNo.CurrentValue = System.DBNull.Value;
				BankYesNo.OldValue = BankYesNo.CurrentValue;
				BankODLimit.CurrentValue = System.DBNull.Value;
				BankODLimit.OldValue = BankODLimit.CurrentValue;
				BankAcctNO.CurrentValue = System.DBNull.Value;
				BankAcctNO.OldValue = BankAcctNO.CurrentValue;
				CreditLimit.CurrentValue = CreditLimit.FldDefault;
				CreditLimit.OldValue = CreditLimit.CurrentValue;
				ReferBy.CurrentValue = System.DBNull.Value;
				ReferBy.OldValue = ReferBy.CurrentValue;
				AgentImageName.CurrentValue = System.DBNull.Value;
				AgentImageName.OldValue = AgentImageName.CurrentValue;
				status.CurrentValue = status.FldDefault;
				status.OldValue = status.CurrentValue;
				CreatedBy.CurrentValue = System.DBNull.Value;
				CreatedBy.OldValue = CreatedBy.CurrentValue;
				CreatedDate.CurrentValue = System.DBNull.Value;
				CreatedDate.OldValue = CreatedDate.CurrentValue;
				ModifiedUser.CurrentValue = System.DBNull.Value;
				ModifiedUser.OldValue = ModifiedUser.CurrentValue;
				ModifiedDate.CurrentValue = System.DBNull.Value;
				ModifiedDate.OldValue = ModifiedDate.CurrentValue;
				PPExpiryDate.CurrentValue = System.DBNull.Value;
				PPExpiryDate.OldValue = PPExpiryDate.CurrentValue;
				TTExpiryDate.CurrentValue = System.DBNull.Value;
				TTExpiryDate.OldValue = TTExpiryDate.CurrentValue;
				MCExpiryDate.CurrentValue = System.DBNull.Value;
				MCExpiryDate.OldValue = MCExpiryDate.CurrentValue;
				Action.CurrentValue = System.DBNull.Value;
				Action.OldValue = Action.CurrentValue;
				Remark.CurrentValue = System.DBNull.Value;
				Remark.OldValue = Remark.CurrentValue;
				MCType.CurrentValue = MCType.FldDefault;
				MCType.OldValue = MCType.CurrentValue;
				CustDOB.CurrentValue = System.DBNull.Value;
				CustDOB.OldValue = CustDOB.CurrentValue;
				DefContactDOB.CurrentValue = System.DBNull.Value;
				DefContactDOB.OldValue = DefContactDOB.CurrentValue;
				ScanImage.CurrentValue = System.DBNull.Value;
				ScanImage.OldValue = ScanImage.CurrentValue;
				BizNature.CurrentValue = System.DBNull.Value;
				BizNature.OldValue = BizNature.CurrentValue;
				DefContactPOB.CurrentValue = System.DBNull.Value;
				DefContactPOB.OldValue = DefContactPOB.CurrentValue;
				NewTran.CurrentValue = NewTran.FldDefault;
				NewTran.OldValue = NewTran.CurrentValue;
				BizRegNo.CurrentValue = System.DBNull.Value;
				BizRegNo.OldValue = BizRegNo.CurrentValue;
				BizRegDate.CurrentValue = System.DBNull.Value;
				BizRegDate.OldValue = BizRegDate.CurrentValue;
				BizRegPlace.CurrentValue = System.DBNull.Value;
				BizRegPlace.OldValue = BizRegPlace.CurrentValue;
				BizRegExpDate.CurrentValue = System.DBNull.Value;
				BizRegExpDate.OldValue = BizRegExpDate.CurrentValue;
				UnIncorpExec.CurrentValue = UnIncorpExec.FldDefault;
				UnIncorpExec.OldValue = UnIncorpExec.CurrentValue;
				DefContactAuthorzLetter.CurrentValue = DefContactAuthorzLetter.FldDefault;
				DefContactAuthorzLetter.OldValue = DefContactAuthorzLetter.CurrentValue;
				Politician.CurrentValue = Politician.FldDefault;
				Politician.OldValue = Politician.CurrentValue;
				BizPartnerNo.CurrentValue = BizPartnerNo.FldDefault;
				BizPartnerNo.OldValue = BizPartnerNo.CurrentValue;
				Remark2.CurrentValue = System.DBNull.Value;
				Remark2.OldValue = Remark2.CurrentValue;
				BannedListRemark.CurrentValue = System.DBNull.Value;
				BannedListRemark.OldValue = BannedListRemark.CurrentValue;
			}

			//  Load search values for validation // DN
			public void LoadSearchValues() {

				// AgentId
				if (ew_QueryString.ContainsKey("x_AgentId"))
					AgentId.AdvancedSearch.SearchValue = ew_Get("x_AgentId");
				if (ew_NotEmpty(AgentId.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_AgentId"))
					AgentId.AdvancedSearch.SearchOperator = ew_Get("z_AgentId");

				// AgentName
				if (ew_QueryString.ContainsKey("x_AgentName"))
					AgentName.AdvancedSearch.SearchValue = ew_Get("x_AgentName");
				if (ew_NotEmpty(AgentName.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_AgentName"))
					AgentName.AdvancedSearch.SearchOperator = ew_Get("z_AgentName");

				// AgentRiskRating
				if (ew_QueryString.ContainsKey("x_AgentRiskRating"))
					AgentRiskRating.AdvancedSearch.SearchValue = ew_Get("x_AgentRiskRating");
				if (ew_NotEmpty(AgentRiskRating.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_AgentRiskRating"))
					AgentRiskRating.AdvancedSearch.SearchOperator = ew_Get("z_AgentRiskRating");

				// AgentRiskCredit
				if (ew_QueryString.ContainsKey("x_AgentRiskCredit"))
					AgentRiskCredit.AdvancedSearch.SearchValue = ew_Get("x_AgentRiskCredit");
				if (ew_NotEmpty(AgentRiskCredit.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_AgentRiskCredit"))
					AgentRiskCredit.AdvancedSearch.SearchOperator = ew_Get("z_AgentRiskCredit");

				// Address1
				if (ew_QueryString.ContainsKey("x_Address1"))
					Address1.AdvancedSearch.SearchValue = ew_Get("x_Address1");
				if (ew_NotEmpty(Address1.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Address1"))
					Address1.AdvancedSearch.SearchOperator = ew_Get("z_Address1");

				// Address2
				if (ew_QueryString.ContainsKey("x_Address2"))
					Address2.AdvancedSearch.SearchValue = ew_Get("x_Address2");
				if (ew_NotEmpty(Address2.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Address2"))
					Address2.AdvancedSearch.SearchOperator = ew_Get("z_Address2");

				// Address3
				if (ew_QueryString.ContainsKey("x_Address3"))
					Address3.AdvancedSearch.SearchValue = ew_Get("x_Address3");
				if (ew_NotEmpty(Address3.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Address3"))
					Address3.AdvancedSearch.SearchOperator = ew_Get("z_Address3");

				// Country
				if (ew_QueryString.ContainsKey("x_Country"))
					Country.AdvancedSearch.SearchValue = ew_Get("x_Country");
				if (ew_NotEmpty(Country.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Country"))
					Country.AdvancedSearch.SearchOperator = ew_Get("z_Country");

				// ZipCode
				if (ew_QueryString.ContainsKey("x_ZipCode"))
					ZipCode.AdvancedSearch.SearchValue = ew_Get("x_ZipCode");
				if (ew_NotEmpty(ZipCode.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_ZipCode"))
					ZipCode.AdvancedSearch.SearchOperator = ew_Get("z_ZipCode");

				// Fax
				if (ew_QueryString.ContainsKey("x_Fax"))
					Fax.AdvancedSearch.SearchValue = ew_Get("x_Fax");
				if (ew_NotEmpty(Fax.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Fax"))
					Fax.AdvancedSearch.SearchOperator = ew_Get("z_Fax");

				// Phone
				if (ew_QueryString.ContainsKey("x_Phone"))
					Phone.AdvancedSearch.SearchValue = ew_Get("x_Phone");
				if (ew_NotEmpty(Phone.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Phone"))
					Phone.AdvancedSearch.SearchOperator = ew_Get("z_Phone");

				// Mobile
				if (ew_QueryString.ContainsKey("x_Mobile"))
					Mobile.AdvancedSearch.SearchValue = ew_Get("x_Mobile");
				if (ew_NotEmpty(Mobile.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Mobile"))
					Mobile.AdvancedSearch.SearchOperator = ew_Get("z_Mobile");

				// BuzType
				if (ew_QueryString.ContainsKey("x_BuzType"))
					BuzType.AdvancedSearch.SearchValue = ew_Get("x_BuzType");
				if (ew_NotEmpty(BuzType.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BuzType"))
					BuzType.AdvancedSearch.SearchOperator = ew_Get("z_BuzType");

				// ClassType
				if (ew_QueryString.ContainsKey("x_ClassType"))
					ClassType.AdvancedSearch.SearchValue = ew_Get("x_ClassType");
				if (ew_NotEmpty(ClassType.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_ClassType"))
					ClassType.AdvancedSearch.SearchOperator = ew_Get("z_ClassType");

				// DefContactPName
				if (ew_QueryString.ContainsKey("x_DefContactPName"))
					DefContactPName.AdvancedSearch.SearchValue = ew_Get("x_DefContactPName");
				if (ew_NotEmpty(DefContactPName.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_DefContactPName"))
					DefContactPName.AdvancedSearch.SearchOperator = ew_Get("z_DefContactPName");

				// DefContactPNric
				if (ew_QueryString.ContainsKey("x_DefContactPNric"))
					DefContactPNric.AdvancedSearch.SearchValue = ew_Get("x_DefContactPNric");
				if (ew_NotEmpty(DefContactPNric.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_DefContactPNric"))
					DefContactPNric.AdvancedSearch.SearchOperator = ew_Get("z_DefContactPNric");

				// DefContactPNation
				if (ew_QueryString.ContainsKey("x_DefContactPNation"))
					DefContactPNation.AdvancedSearch.SearchValue = ew_Get("x_DefContactPNation");
				if (ew_NotEmpty(DefContactPNation.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_DefContactPNation"))
					DefContactPNation.AdvancedSearch.SearchOperator = ew_Get("z_DefContactPNation");

				// DefContactPOccupation
				if (ew_QueryString.ContainsKey("x_DefContactPOccupation"))
					DefContactPOccupation.AdvancedSearch.SearchValue = ew_Get("x_DefContactPOccupation");
				if (ew_NotEmpty(DefContactPOccupation.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_DefContactPOccupation"))
					DefContactPOccupation.AdvancedSearch.SearchOperator = ew_Get("z_DefContactPOccupation");

				// TermsId
				if (ew_QueryString.ContainsKey("x_TermsId"))
					TermsId.AdvancedSearch.SearchValue = ew_Get("x_TermsId");
				if (ew_NotEmpty(TermsId.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_TermsId"))
					TermsId.AdvancedSearch.SearchOperator = ew_Get("z_TermsId");

				// LedgerBal
				if (ew_QueryString.ContainsKey("x_LedgerBal"))
					LedgerBal.AdvancedSearch.SearchValue = ew_Get("x_LedgerBal");
				if (ew_NotEmpty(LedgerBal.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_LedgerBal"))
					LedgerBal.AdvancedSearch.SearchOperator = ew_Get("z_LedgerBal");

				// AvailableBal
				if (ew_QueryString.ContainsKey("x_AvailableBal"))
					AvailableBal.AdvancedSearch.SearchValue = ew_Get("x_AvailableBal");
				if (ew_NotEmpty(AvailableBal.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_AvailableBal"))
					AvailableBal.AdvancedSearch.SearchOperator = ew_Get("z_AvailableBal");

				// _Email
				if (ew_QueryString.ContainsKey("x__Email"))
					_Email.AdvancedSearch.SearchValue = ew_Get("x__Email");
				if (ew_NotEmpty(_Email.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z__Email"))
					_Email.AdvancedSearch.SearchOperator = ew_Get("z__Email");

				// URL
				if (ew_QueryString.ContainsKey("x_URL"))
					URL.AdvancedSearch.SearchValue = ew_Get("x_URL");
				if (ew_NotEmpty(URL.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_URL"))
					URL.AdvancedSearch.SearchOperator = ew_Get("z_URL");

				// CustType
				if (ew_QueryString.ContainsKey("x_CustType"))
					CustType.AdvancedSearch.SearchValue = ew_Get("x_CustType");
				if (ew_NotEmpty(CustType.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_CustType"))
					CustType.AdvancedSearch.SearchOperator = ew_Get("z_CustType");

				// RemittanceLicNO
				if (ew_QueryString.ContainsKey("x_RemittanceLicNO"))
					RemittanceLicNO.AdvancedSearch.SearchValue = ew_Get("x_RemittanceLicNO");
				if (ew_NotEmpty(RemittanceLicNO.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_RemittanceLicNO"))
					RemittanceLicNO.AdvancedSearch.SearchOperator = ew_Get("z_RemittanceLicNO");

				// MCLicNo
				if (ew_QueryString.ContainsKey("x_MCLicNo"))
					MCLicNo.AdvancedSearch.SearchValue = ew_Get("x_MCLicNo");
				if (ew_NotEmpty(MCLicNo.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_MCLicNo"))
					MCLicNo.AdvancedSearch.SearchOperator = ew_Get("z_MCLicNo");

				// BankYesNo
				if (ew_QueryString.ContainsKey("x_BankYesNo"))
					BankYesNo.AdvancedSearch.SearchValue = ew_Get("x_BankYesNo");
				if (ew_NotEmpty(BankYesNo.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BankYesNo"))
					BankYesNo.AdvancedSearch.SearchOperator = ew_Get("z_BankYesNo");

				// BankODLimit
				if (ew_QueryString.ContainsKey("x_BankODLimit"))
					BankODLimit.AdvancedSearch.SearchValue = ew_Get("x_BankODLimit");
				if (ew_NotEmpty(BankODLimit.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BankODLimit"))
					BankODLimit.AdvancedSearch.SearchOperator = ew_Get("z_BankODLimit");

				// BankAcctNO
				if (ew_QueryString.ContainsKey("x_BankAcctNO"))
					BankAcctNO.AdvancedSearch.SearchValue = ew_Get("x_BankAcctNO");
				if (ew_NotEmpty(BankAcctNO.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BankAcctNO"))
					BankAcctNO.AdvancedSearch.SearchOperator = ew_Get("z_BankAcctNO");

				// CreditLimit
				if (ew_QueryString.ContainsKey("x_CreditLimit"))
					CreditLimit.AdvancedSearch.SearchValue = ew_Get("x_CreditLimit");
				if (ew_NotEmpty(CreditLimit.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_CreditLimit"))
					CreditLimit.AdvancedSearch.SearchOperator = ew_Get("z_CreditLimit");

				// ReferBy
				if (ew_QueryString.ContainsKey("x_ReferBy"))
					ReferBy.AdvancedSearch.SearchValue = ew_Get("x_ReferBy");
				if (ew_NotEmpty(ReferBy.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_ReferBy"))
					ReferBy.AdvancedSearch.SearchOperator = ew_Get("z_ReferBy");

				// AgentImageName
				if (ew_QueryString.ContainsKey("x_AgentImageName"))
					AgentImageName.AdvancedSearch.SearchValue = ew_Get("x_AgentImageName");
				if (ew_NotEmpty(AgentImageName.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_AgentImageName"))
					AgentImageName.AdvancedSearch.SearchOperator = ew_Get("z_AgentImageName");

				// status
				if (ew_QueryString.ContainsKey("x_status"))
					status.AdvancedSearch.SearchValue = ew_Get("x_status");
				if (ew_NotEmpty(status.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_status"))
					status.AdvancedSearch.SearchOperator = ew_Get("z_status");

				// CreatedBy
				if (ew_QueryString.ContainsKey("x_CreatedBy"))
					CreatedBy.AdvancedSearch.SearchValue = ew_Get("x_CreatedBy");
				if (ew_NotEmpty(CreatedBy.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_CreatedBy"))
					CreatedBy.AdvancedSearch.SearchOperator = ew_Get("z_CreatedBy");

				// CreatedDate
				if (ew_QueryString.ContainsKey("x_CreatedDate"))
					CreatedDate.AdvancedSearch.SearchValue = ew_Get("x_CreatedDate");
				if (ew_NotEmpty(CreatedDate.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_CreatedDate"))
					CreatedDate.AdvancedSearch.SearchOperator = ew_Get("z_CreatedDate");

				// ModifiedUser
				if (ew_QueryString.ContainsKey("x_ModifiedUser"))
					ModifiedUser.AdvancedSearch.SearchValue = ew_Get("x_ModifiedUser");
				if (ew_NotEmpty(ModifiedUser.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_ModifiedUser"))
					ModifiedUser.AdvancedSearch.SearchOperator = ew_Get("z_ModifiedUser");

				// ModifiedDate
				if (ew_QueryString.ContainsKey("x_ModifiedDate"))
					ModifiedDate.AdvancedSearch.SearchValue = ew_Get("x_ModifiedDate");
				if (ew_NotEmpty(ModifiedDate.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_ModifiedDate"))
					ModifiedDate.AdvancedSearch.SearchOperator = ew_Get("z_ModifiedDate");

				// PPExpiryDate
				if (ew_QueryString.ContainsKey("x_PPExpiryDate"))
					PPExpiryDate.AdvancedSearch.SearchValue = ew_Get("x_PPExpiryDate");
				if (ew_NotEmpty(PPExpiryDate.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_PPExpiryDate"))
					PPExpiryDate.AdvancedSearch.SearchOperator = ew_Get("z_PPExpiryDate");

				// TTExpiryDate
				if (ew_QueryString.ContainsKey("x_TTExpiryDate"))
					TTExpiryDate.AdvancedSearch.SearchValue = ew_Get("x_TTExpiryDate");
				if (ew_NotEmpty(TTExpiryDate.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_TTExpiryDate"))
					TTExpiryDate.AdvancedSearch.SearchOperator = ew_Get("z_TTExpiryDate");

				// MCExpiryDate
				if (ew_QueryString.ContainsKey("x_MCExpiryDate"))
					MCExpiryDate.AdvancedSearch.SearchValue = ew_Get("x_MCExpiryDate");
				if (ew_NotEmpty(MCExpiryDate.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_MCExpiryDate"))
					MCExpiryDate.AdvancedSearch.SearchOperator = ew_Get("z_MCExpiryDate");

				// Action
				if (ew_QueryString.ContainsKey("x_Action"))
					Action.AdvancedSearch.SearchValue = ew_Get("x_Action");
				if (ew_NotEmpty(Action.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Action"))
					Action.AdvancedSearch.SearchOperator = ew_Get("z_Action");

				// Remark
				if (ew_QueryString.ContainsKey("x_Remark"))
					Remark.AdvancedSearch.SearchValue = ew_Get("x_Remark");
				if (ew_NotEmpty(Remark.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Remark"))
					Remark.AdvancedSearch.SearchOperator = ew_Get("z_Remark");

				// MCType
				if (ew_QueryString.ContainsKey("x_MCType"))
					MCType.AdvancedSearch.SearchValue = ew_Get("x_MCType");
				if (ew_NotEmpty(MCType.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_MCType"))
					MCType.AdvancedSearch.SearchOperator = ew_Get("z_MCType");

				// CustDOB
				if (ew_QueryString.ContainsKey("x_CustDOB"))
					CustDOB.AdvancedSearch.SearchValue = ew_Get("x_CustDOB");
				if (ew_NotEmpty(CustDOB.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_CustDOB"))
					CustDOB.AdvancedSearch.SearchOperator = ew_Get("z_CustDOB");

				// DefContactDOB
				if (ew_QueryString.ContainsKey("x_DefContactDOB"))
					DefContactDOB.AdvancedSearch.SearchValue = ew_Get("x_DefContactDOB");
				if (ew_NotEmpty(DefContactDOB.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_DefContactDOB"))
					DefContactDOB.AdvancedSearch.SearchOperator = ew_Get("z_DefContactDOB");

				// ScanImage
				if (ew_QueryString.ContainsKey("x_ScanImage"))
					ScanImage.AdvancedSearch.SearchValue = ew_Get("x_ScanImage");
				if (ew_NotEmpty(ScanImage.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_ScanImage"))
					ScanImage.AdvancedSearch.SearchOperator = ew_Get("z_ScanImage");

				// BizNature
				if (ew_QueryString.ContainsKey("x_BizNature"))
					BizNature.AdvancedSearch.SearchValue = ew_Get("x_BizNature");
				if (ew_NotEmpty(BizNature.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BizNature"))
					BizNature.AdvancedSearch.SearchOperator = ew_Get("z_BizNature");

				// DefContactPOB
				if (ew_QueryString.ContainsKey("x_DefContactPOB"))
					DefContactPOB.AdvancedSearch.SearchValue = ew_Get("x_DefContactPOB");
				if (ew_NotEmpty(DefContactPOB.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_DefContactPOB"))
					DefContactPOB.AdvancedSearch.SearchOperator = ew_Get("z_DefContactPOB");

				// NewTran
				if (ew_QueryString.ContainsKey("x_NewTran"))
					NewTran.AdvancedSearch.SearchValue = ew_Get("x_NewTran");
				if (ew_NotEmpty(NewTran.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_NewTran"))
					NewTran.AdvancedSearch.SearchOperator = ew_Get("z_NewTran");

				// BizRegNo
				if (ew_QueryString.ContainsKey("x_BizRegNo"))
					BizRegNo.AdvancedSearch.SearchValue = ew_Get("x_BizRegNo");
				if (ew_NotEmpty(BizRegNo.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BizRegNo"))
					BizRegNo.AdvancedSearch.SearchOperator = ew_Get("z_BizRegNo");

				// BizRegDate
				if (ew_QueryString.ContainsKey("x_BizRegDate"))
					BizRegDate.AdvancedSearch.SearchValue = ew_Get("x_BizRegDate");
				if (ew_NotEmpty(BizRegDate.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BizRegDate"))
					BizRegDate.AdvancedSearch.SearchOperator = ew_Get("z_BizRegDate");

				// BizRegPlace
				if (ew_QueryString.ContainsKey("x_BizRegPlace"))
					BizRegPlace.AdvancedSearch.SearchValue = ew_Get("x_BizRegPlace");
				if (ew_NotEmpty(BizRegPlace.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BizRegPlace"))
					BizRegPlace.AdvancedSearch.SearchOperator = ew_Get("z_BizRegPlace");

				// BizRegExpDate
				if (ew_QueryString.ContainsKey("x_BizRegExpDate"))
					BizRegExpDate.AdvancedSearch.SearchValue = ew_Get("x_BizRegExpDate");
				if (ew_NotEmpty(BizRegExpDate.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BizRegExpDate"))
					BizRegExpDate.AdvancedSearch.SearchOperator = ew_Get("z_BizRegExpDate");

				// UnIncorpExec
				if (ew_QueryString.ContainsKey("x_UnIncorpExec"))
					UnIncorpExec.AdvancedSearch.SearchValue = ew_Get("x_UnIncorpExec");
				if (ew_NotEmpty(UnIncorpExec.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_UnIncorpExec"))
					UnIncorpExec.AdvancedSearch.SearchOperator = ew_Get("z_UnIncorpExec");

				// DefContactAuthorzLetter
				if (ew_QueryString.ContainsKey("x_DefContactAuthorzLetter"))
					DefContactAuthorzLetter.AdvancedSearch.SearchValue = ew_Get("x_DefContactAuthorzLetter");
				if (ew_NotEmpty(DefContactAuthorzLetter.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_DefContactAuthorzLetter"))
					DefContactAuthorzLetter.AdvancedSearch.SearchOperator = ew_Get("z_DefContactAuthorzLetter");

				// Politician
				if (ew_QueryString.ContainsKey("x_Politician"))
					Politician.AdvancedSearch.SearchValue = ew_Get("x_Politician");
				if (ew_NotEmpty(Politician.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Politician"))
					Politician.AdvancedSearch.SearchOperator = ew_Get("z_Politician");

				// BizPartnerNo
				if (ew_QueryString.ContainsKey("x_BizPartnerNo"))
					BizPartnerNo.AdvancedSearch.SearchValue = ew_Get("x_BizPartnerNo");
				if (ew_NotEmpty(BizPartnerNo.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BizPartnerNo"))
					BizPartnerNo.AdvancedSearch.SearchOperator = ew_Get("z_BizPartnerNo");

				// Remark2
				if (ew_QueryString.ContainsKey("x_Remark2"))
					Remark2.AdvancedSearch.SearchValue = ew_Get("x_Remark2");
				if (ew_NotEmpty(Remark2.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_Remark2"))
					Remark2.AdvancedSearch.SearchOperator = ew_Get("z_Remark2");

				// BannedListRemark
				if (ew_QueryString.ContainsKey("x_BannedListRemark"))
					BannedListRemark.AdvancedSearch.SearchValue = ew_Get("x_BannedListRemark");
				if (ew_NotEmpty(BannedListRemark.AdvancedSearch.SearchValue))
					Command = "search";
				if (ew_QueryString.ContainsKey("z_BannedListRemark"))
					BannedListRemark.AdvancedSearch.SearchOperator = ew_Get("z_BannedListRemark");
			}

			// Load form values
			public void LoadFormValues() {
				if (!AgentId.FldIsDetailKey) {
					AgentId.FormValue = ObjForm.GetValue("x_AgentId");
				}
				if (ObjForm.HasValue("o_AgentId"))
					AgentId.OldValue = ObjForm.GetValue("o_AgentId");
				if (!AgentName.FldIsDetailKey) {
					AgentName.FormValue = ObjForm.GetValue("x_AgentName");
				}
				if (ObjForm.HasValue("o_AgentName"))
					AgentName.OldValue = ObjForm.GetValue("o_AgentName");
				if (!AgentRiskRating.FldIsDetailKey) {
					AgentRiskRating.FormValue = ObjForm.GetValue("x_AgentRiskRating");
				}
				if (ObjForm.HasValue("o_AgentRiskRating"))
					AgentRiskRating.OldValue = ObjForm.GetValue("o_AgentRiskRating");
				if (!AgentRiskCredit.FldIsDetailKey) {
					AgentRiskCredit.FormValue = ObjForm.GetValue("x_AgentRiskCredit");
				}
				if (ObjForm.HasValue("o_AgentRiskCredit"))
					AgentRiskCredit.OldValue = ObjForm.GetValue("o_AgentRiskCredit");
				if (!Address1.FldIsDetailKey) {
					Address1.FormValue = ObjForm.GetValue("x_Address1");
				}
				if (ObjForm.HasValue("o_Address1"))
					Address1.OldValue = ObjForm.GetValue("o_Address1");
				if (!Address2.FldIsDetailKey) {
					Address2.FormValue = ObjForm.GetValue("x_Address2");
				}
				if (ObjForm.HasValue("o_Address2"))
					Address2.OldValue = ObjForm.GetValue("o_Address2");
				if (!Address3.FldIsDetailKey) {
					Address3.FormValue = ObjForm.GetValue("x_Address3");
				}
				if (ObjForm.HasValue("o_Address3"))
					Address3.OldValue = ObjForm.GetValue("o_Address3");
				if (!Country.FldIsDetailKey) {
					Country.FormValue = ObjForm.GetValue("x_Country");
				}
				if (ObjForm.HasValue("o_Country"))
					Country.OldValue = ObjForm.GetValue("o_Country");
				if (!ZipCode.FldIsDetailKey) {
					ZipCode.FormValue = ObjForm.GetValue("x_ZipCode");
				}
				if (ObjForm.HasValue("o_ZipCode"))
					ZipCode.OldValue = ObjForm.GetValue("o_ZipCode");
				if (!Fax.FldIsDetailKey) {
					Fax.FormValue = ObjForm.GetValue("x_Fax");
				}
				if (ObjForm.HasValue("o_Fax"))
					Fax.OldValue = ObjForm.GetValue("o_Fax");
				if (!Phone.FldIsDetailKey) {
					Phone.FormValue = ObjForm.GetValue("x_Phone");
				}
				if (ObjForm.HasValue("o_Phone"))
					Phone.OldValue = ObjForm.GetValue("o_Phone");
				if (!Mobile.FldIsDetailKey) {
					Mobile.FormValue = ObjForm.GetValue("x_Mobile");
				}
				if (ObjForm.HasValue("o_Mobile"))
					Mobile.OldValue = ObjForm.GetValue("o_Mobile");
				if (!BuzType.FldIsDetailKey) {
					BuzType.FormValue = ObjForm.GetValue("x_BuzType");
				}
				if (ObjForm.HasValue("o_BuzType"))
					BuzType.OldValue = ObjForm.GetValue("o_BuzType");
				if (!ClassType.FldIsDetailKey) {
					ClassType.FormValue = ObjForm.GetValue("x_ClassType");
				}
				if (ObjForm.HasValue("o_ClassType"))
					ClassType.OldValue = ObjForm.GetValue("o_ClassType");
				if (!DefContactPName.FldIsDetailKey) {
					DefContactPName.FormValue = ObjForm.GetValue("x_DefContactPName");
				}
				if (ObjForm.HasValue("o_DefContactPName"))
					DefContactPName.OldValue = ObjForm.GetValue("o_DefContactPName");
				if (!DefContactPNric.FldIsDetailKey) {
					DefContactPNric.FormValue = ObjForm.GetValue("x_DefContactPNric");
				}
				if (ObjForm.HasValue("o_DefContactPNric"))
					DefContactPNric.OldValue = ObjForm.GetValue("o_DefContactPNric");
				if (!DefContactPNation.FldIsDetailKey) {
					DefContactPNation.FormValue = ObjForm.GetValue("x_DefContactPNation");
				}
				if (ObjForm.HasValue("o_DefContactPNation"))
					DefContactPNation.OldValue = ObjForm.GetValue("o_DefContactPNation");
				if (!DefContactPOccupation.FldIsDetailKey) {
					DefContactPOccupation.FormValue = ObjForm.GetValue("x_DefContactPOccupation");
				}
				if (ObjForm.HasValue("o_DefContactPOccupation"))
					DefContactPOccupation.OldValue = ObjForm.GetValue("o_DefContactPOccupation");
				if (!TermsId.FldIsDetailKey) {
					TermsId.FormValue = ObjForm.GetValue("x_TermsId");
				}
				if (ObjForm.HasValue("o_TermsId"))
					TermsId.OldValue = ObjForm.GetValue("o_TermsId");
				if (!LedgerBal.FldIsDetailKey) {
					LedgerBal.FormValue = ObjForm.GetValue("x_LedgerBal");
				}
				if (ObjForm.HasValue("o_LedgerBal"))
					LedgerBal.OldValue = ObjForm.GetValue("o_LedgerBal");
				if (!AvailableBal.FldIsDetailKey) {
					AvailableBal.FormValue = ObjForm.GetValue("x_AvailableBal");
				}
				if (ObjForm.HasValue("o_AvailableBal"))
					AvailableBal.OldValue = ObjForm.GetValue("o_AvailableBal");
				if (!_Email.FldIsDetailKey) {
					_Email.FormValue = ObjForm.GetValue("x__Email");
				}
				if (ObjForm.HasValue("o__Email"))
					_Email.OldValue = ObjForm.GetValue("o__Email");
				if (!URL.FldIsDetailKey) {
					URL.FormValue = ObjForm.GetValue("x_URL");
				}
				if (ObjForm.HasValue("o_URL"))
					URL.OldValue = ObjForm.GetValue("o_URL");
				if (!CustType.FldIsDetailKey) {
					CustType.FormValue = ObjForm.GetValue("x_CustType");
				}
				if (ObjForm.HasValue("o_CustType"))
					CustType.OldValue = ObjForm.GetValue("o_CustType");
				if (!RemittanceLicNO.FldIsDetailKey) {
					RemittanceLicNO.FormValue = ObjForm.GetValue("x_RemittanceLicNO");
				}
				if (ObjForm.HasValue("o_RemittanceLicNO"))
					RemittanceLicNO.OldValue = ObjForm.GetValue("o_RemittanceLicNO");
				if (!MCLicNo.FldIsDetailKey) {
					MCLicNo.FormValue = ObjForm.GetValue("x_MCLicNo");
				}
				if (ObjForm.HasValue("o_MCLicNo"))
					MCLicNo.OldValue = ObjForm.GetValue("o_MCLicNo");
				if (!BankYesNo.FldIsDetailKey) {
					BankYesNo.FormValue = ObjForm.GetValue("x_BankYesNo");
				}
				if (ObjForm.HasValue("o_BankYesNo"))
					BankYesNo.OldValue = ObjForm.GetValue("o_BankYesNo");
				if (!BankODLimit.FldIsDetailKey) {
					BankODLimit.FormValue = ObjForm.GetValue("x_BankODLimit");
				}
				if (ObjForm.HasValue("o_BankODLimit"))
					BankODLimit.OldValue = ObjForm.GetValue("o_BankODLimit");
				if (!BankAcctNO.FldIsDetailKey) {
					BankAcctNO.FormValue = ObjForm.GetValue("x_BankAcctNO");
				}
				if (ObjForm.HasValue("o_BankAcctNO"))
					BankAcctNO.OldValue = ObjForm.GetValue("o_BankAcctNO");
				if (!CreditLimit.FldIsDetailKey) {
					CreditLimit.FormValue = ObjForm.GetValue("x_CreditLimit");
				}
				if (ObjForm.HasValue("o_CreditLimit"))
					CreditLimit.OldValue = ObjForm.GetValue("o_CreditLimit");
				if (!ReferBy.FldIsDetailKey) {
					ReferBy.FormValue = ObjForm.GetValue("x_ReferBy");
				}
				if (ObjForm.HasValue("o_ReferBy"))
					ReferBy.OldValue = ObjForm.GetValue("o_ReferBy");
				if (!AgentImageName.FldIsDetailKey) {
					AgentImageName.FormValue = ObjForm.GetValue("x_AgentImageName");
				}
				if (ObjForm.HasValue("o_AgentImageName"))
					AgentImageName.OldValue = ObjForm.GetValue("o_AgentImageName");
				if (!status.FldIsDetailKey) {
					status.FormValue = ObjForm.GetValue("x_status");
				}
				if (ObjForm.HasValue("o_status"))
					status.OldValue = ObjForm.GetValue("o_status");
				if (!CreatedBy.FldIsDetailKey) {
					CreatedBy.FormValue = ObjForm.GetValue("x_CreatedBy");
				}
				if (ObjForm.HasValue("o_CreatedBy"))
					CreatedBy.OldValue = ObjForm.GetValue("o_CreatedBy");
				if (!CreatedDate.FldIsDetailKey) {
					CreatedDate.FormValue = ObjForm.GetValue("x_CreatedDate");
					CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 0);
				}
				CreatedDate.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_CreatedDate"), 0);
				if (!ModifiedUser.FldIsDetailKey) {
					ModifiedUser.FormValue = ObjForm.GetValue("x_ModifiedUser");
				}
				if (ObjForm.HasValue("o_ModifiedUser"))
					ModifiedUser.OldValue = ObjForm.GetValue("o_ModifiedUser");
				if (!ModifiedDate.FldIsDetailKey) {
					ModifiedDate.FormValue = ObjForm.GetValue("x_ModifiedDate");
					ModifiedDate.CurrentValue = ew_UnformatDateTime(ModifiedDate.CurrentValue, 0);
				}
				ModifiedDate.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_ModifiedDate"), 0);
				if (!PPExpiryDate.FldIsDetailKey) {
					PPExpiryDate.FormValue = ObjForm.GetValue("x_PPExpiryDate");
					PPExpiryDate.CurrentValue = ew_UnformatDateTime(PPExpiryDate.CurrentValue, 0);
				}
				PPExpiryDate.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_PPExpiryDate"), 0);
				if (!TTExpiryDate.FldIsDetailKey) {
					TTExpiryDate.FormValue = ObjForm.GetValue("x_TTExpiryDate");
					TTExpiryDate.CurrentValue = ew_UnformatDateTime(TTExpiryDate.CurrentValue, 0);
				}
				TTExpiryDate.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_TTExpiryDate"), 0);
				if (!MCExpiryDate.FldIsDetailKey) {
					MCExpiryDate.FormValue = ObjForm.GetValue("x_MCExpiryDate");
					MCExpiryDate.CurrentValue = ew_UnformatDateTime(MCExpiryDate.CurrentValue, 0);
				}
				MCExpiryDate.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_MCExpiryDate"), 0);
				if (!Action.FldIsDetailKey) {
					Action.FormValue = ObjForm.GetValue("x_Action");
				}
				if (ObjForm.HasValue("o_Action"))
					Action.OldValue = ObjForm.GetValue("o_Action");
				if (!Remark.FldIsDetailKey) {
					Remark.FormValue = ObjForm.GetValue("x_Remark");
				}
				if (ObjForm.HasValue("o_Remark"))
					Remark.OldValue = ObjForm.GetValue("o_Remark");
				if (!MCType.FldIsDetailKey) {
					MCType.FormValue = ObjForm.GetValue("x_MCType");
				}
				if (ObjForm.HasValue("o_MCType"))
					MCType.OldValue = ObjForm.GetValue("o_MCType");
				if (!CustDOB.FldIsDetailKey) {
					CustDOB.FormValue = ObjForm.GetValue("x_CustDOB");
					CustDOB.CurrentValue = ew_UnformatDateTime(CustDOB.CurrentValue, 0);
				}
				CustDOB.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_CustDOB"), 0);
				if (!DefContactDOB.FldIsDetailKey) {
					DefContactDOB.FormValue = ObjForm.GetValue("x_DefContactDOB");
					DefContactDOB.CurrentValue = ew_UnformatDateTime(DefContactDOB.CurrentValue, 0);
				}
				DefContactDOB.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_DefContactDOB"), 0);
				if (!ScanImage.FldIsDetailKey) {
					ScanImage.FormValue = ObjForm.GetValue("x_ScanImage");
				}
				if (ObjForm.HasValue("o_ScanImage"))
					ScanImage.OldValue = ObjForm.GetValue("o_ScanImage");
				if (!BizNature.FldIsDetailKey) {
					BizNature.FormValue = ObjForm.GetValue("x_BizNature");
				}
				if (ObjForm.HasValue("o_BizNature"))
					BizNature.OldValue = ObjForm.GetValue("o_BizNature");
				if (!DefContactPOB.FldIsDetailKey) {
					DefContactPOB.FormValue = ObjForm.GetValue("x_DefContactPOB");
				}
				if (ObjForm.HasValue("o_DefContactPOB"))
					DefContactPOB.OldValue = ObjForm.GetValue("o_DefContactPOB");
				if (!NewTran.FldIsDetailKey) {
					NewTran.FormValue = ObjForm.GetValue("x_NewTran");
				}
				if (ObjForm.HasValue("o_NewTran"))
					NewTran.OldValue = ObjForm.GetValue("o_NewTran");
				if (!BizRegNo.FldIsDetailKey) {
					BizRegNo.FormValue = ObjForm.GetValue("x_BizRegNo");
				}
				if (ObjForm.HasValue("o_BizRegNo"))
					BizRegNo.OldValue = ObjForm.GetValue("o_BizRegNo");
				if (!BizRegDate.FldIsDetailKey) {
					BizRegDate.FormValue = ObjForm.GetValue("x_BizRegDate");
					BizRegDate.CurrentValue = ew_UnformatDateTime(BizRegDate.CurrentValue, 0);
				}
				BizRegDate.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_BizRegDate"), 0);
				if (!BizRegPlace.FldIsDetailKey) {
					BizRegPlace.FormValue = ObjForm.GetValue("x_BizRegPlace");
				}
				if (ObjForm.HasValue("o_BizRegPlace"))
					BizRegPlace.OldValue = ObjForm.GetValue("o_BizRegPlace");
				if (!BizRegExpDate.FldIsDetailKey) {
					BizRegExpDate.FormValue = ObjForm.GetValue("x_BizRegExpDate");
					BizRegExpDate.CurrentValue = ew_UnformatDateTime(BizRegExpDate.CurrentValue, 0);
				}
				BizRegExpDate.OldValue = ew_UnformatDateTime(ObjForm.GetValue("o_BizRegExpDate"), 0);
				if (!UnIncorpExec.FldIsDetailKey) {
					UnIncorpExec.FormValue = ObjForm.GetValue("x_UnIncorpExec");
				}
				if (ObjForm.HasValue("o_UnIncorpExec"))
					UnIncorpExec.OldValue = ObjForm.GetValue("o_UnIncorpExec");
				if (!DefContactAuthorzLetter.FldIsDetailKey) {
					DefContactAuthorzLetter.FormValue = ObjForm.GetValue("x_DefContactAuthorzLetter");
				}
				if (ObjForm.HasValue("o_DefContactAuthorzLetter"))
					DefContactAuthorzLetter.OldValue = ObjForm.GetValue("o_DefContactAuthorzLetter");
				if (!Politician.FldIsDetailKey) {
					Politician.FormValue = ObjForm.GetValue("x_Politician");
				}
				if (ObjForm.HasValue("o_Politician"))
					Politician.OldValue = ObjForm.GetValue("o_Politician");
				if (!BizPartnerNo.FldIsDetailKey) {
					BizPartnerNo.FormValue = ObjForm.GetValue("x_BizPartnerNo");
				}
				if (ObjForm.HasValue("o_BizPartnerNo"))
					BizPartnerNo.OldValue = ObjForm.GetValue("o_BizPartnerNo");
				if (!Remark2.FldIsDetailKey) {
					Remark2.FormValue = ObjForm.GetValue("x_Remark2");
				}
				if (ObjForm.HasValue("o_Remark2"))
					Remark2.OldValue = ObjForm.GetValue("o_Remark2");
				if (!BannedListRemark.FldIsDetailKey) {
					BannedListRemark.FormValue = ObjForm.GetValue("x_BannedListRemark");
				}
				if (ObjForm.HasValue("o_BannedListRemark"))
					BannedListRemark.OldValue = ObjForm.GetValue("o_BannedListRemark");
			}

			// Restore form values
			public void RestoreFormValues() {
				AgentId.CurrentValue = AgentId.FormValue;
				AgentName.CurrentValue = AgentName.FormValue;
				AgentRiskRating.CurrentValue = AgentRiskRating.FormValue;
				AgentRiskCredit.CurrentValue = AgentRiskCredit.FormValue;
				Address1.CurrentValue = Address1.FormValue;
				Address2.CurrentValue = Address2.FormValue;
				Address3.CurrentValue = Address3.FormValue;
				Country.CurrentValue = Country.FormValue;
				ZipCode.CurrentValue = ZipCode.FormValue;
				Fax.CurrentValue = Fax.FormValue;
				Phone.CurrentValue = Phone.FormValue;
				Mobile.CurrentValue = Mobile.FormValue;
				BuzType.CurrentValue = BuzType.FormValue;
				ClassType.CurrentValue = ClassType.FormValue;
				DefContactPName.CurrentValue = DefContactPName.FormValue;
				DefContactPNric.CurrentValue = DefContactPNric.FormValue;
				DefContactPNation.CurrentValue = DefContactPNation.FormValue;
				DefContactPOccupation.CurrentValue = DefContactPOccupation.FormValue;
				TermsId.CurrentValue = TermsId.FormValue;
				LedgerBal.CurrentValue = LedgerBal.FormValue;
				AvailableBal.CurrentValue = AvailableBal.FormValue;
				_Email.CurrentValue = _Email.FormValue;
				URL.CurrentValue = URL.FormValue;
				CustType.CurrentValue = CustType.FormValue;
				RemittanceLicNO.CurrentValue = RemittanceLicNO.FormValue;
				MCLicNo.CurrentValue = MCLicNo.FormValue;
				BankYesNo.CurrentValue = BankYesNo.FormValue;
				BankODLimit.CurrentValue = BankODLimit.FormValue;
				BankAcctNO.CurrentValue = BankAcctNO.FormValue;
				CreditLimit.CurrentValue = CreditLimit.FormValue;
				ReferBy.CurrentValue = ReferBy.FormValue;
				AgentImageName.CurrentValue = AgentImageName.FormValue;
				status.CurrentValue = status.FormValue;
				CreatedBy.CurrentValue = CreatedBy.FormValue;
				CreatedDate.CurrentValue = CreatedDate.FormValue;
				CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 0);
				ModifiedUser.CurrentValue = ModifiedUser.FormValue;
				ModifiedDate.CurrentValue = ModifiedDate.FormValue;
				ModifiedDate.CurrentValue = ew_UnformatDateTime(ModifiedDate.CurrentValue, 0);
				PPExpiryDate.CurrentValue = PPExpiryDate.FormValue;
				PPExpiryDate.CurrentValue = ew_UnformatDateTime(PPExpiryDate.CurrentValue, 0);
				TTExpiryDate.CurrentValue = TTExpiryDate.FormValue;
				TTExpiryDate.CurrentValue = ew_UnformatDateTime(TTExpiryDate.CurrentValue, 0);
				MCExpiryDate.CurrentValue = MCExpiryDate.FormValue;
				MCExpiryDate.CurrentValue = ew_UnformatDateTime(MCExpiryDate.CurrentValue, 0);
				Action.CurrentValue = Action.FormValue;
				Remark.CurrentValue = Remark.FormValue;
				MCType.CurrentValue = MCType.FormValue;
				CustDOB.CurrentValue = CustDOB.FormValue;
				CustDOB.CurrentValue = ew_UnformatDateTime(CustDOB.CurrentValue, 0);
				DefContactDOB.CurrentValue = DefContactDOB.FormValue;
				DefContactDOB.CurrentValue = ew_UnformatDateTime(DefContactDOB.CurrentValue, 0);
				ScanImage.CurrentValue = ScanImage.FormValue;
				BizNature.CurrentValue = BizNature.FormValue;
				DefContactPOB.CurrentValue = DefContactPOB.FormValue;
				NewTran.CurrentValue = NewTran.FormValue;
				BizRegNo.CurrentValue = BizRegNo.FormValue;
				BizRegDate.CurrentValue = BizRegDate.FormValue;
				BizRegDate.CurrentValue = ew_UnformatDateTime(BizRegDate.CurrentValue, 0);
				BizRegPlace.CurrentValue = BizRegPlace.FormValue;
				BizRegExpDate.CurrentValue = BizRegExpDate.FormValue;
				BizRegExpDate.CurrentValue = ew_UnformatDateTime(BizRegExpDate.CurrentValue, 0);
				UnIncorpExec.CurrentValue = UnIncorpExec.FormValue;
				DefContactAuthorzLetter.CurrentValue = DefContactAuthorzLetter.FormValue;
				Politician.CurrentValue = Politician.FormValue;
				BizPartnerNo.CurrentValue = BizPartnerNo.FormValue;
				Remark2.CurrentValue = Remark2.FormValue;
				BannedListRemark.CurrentValue = BannedListRemark.FormValue;
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
				AgentId.DbValue = row["AgentId"];
				AgentName.DbValue = row["AgentName"];
				AgentRiskRating.DbValue = row["AgentRiskRating"];
				AgentRiskCredit.DbValue = row["AgentRiskCredit"];
				Address1.DbValue = row["Address1"];
				Address2.DbValue = row["Address2"];
				Address3.DbValue = row["Address3"];
				Country.DbValue = row["Country"];
				ZipCode.DbValue = row["ZipCode"];
				Fax.DbValue = row["Fax"];
				Phone.DbValue = row["Phone"];
				Mobile.DbValue = row["Mobile"];
				BuzType.DbValue = row["BuzType"];
				ClassType.DbValue = row["ClassType"];
				DefContactPName.DbValue = row["DefContactPName"];
				DefContactPNric.DbValue = row["DefContactPNric"];
				DefContactPNation.DbValue = row["DefContactPNation"];
				DefContactPOccupation.DbValue = row["DefContactPOccupation"];
				TermsId.DbValue = row["TermsId"];
				LedgerBal.DbValue = row["LedgerBal"];
				AvailableBal.DbValue = row["AvailableBal"];
				_Email.DbValue = row["Email"];
				URL.DbValue = row["URL"];
				CustType.DbValue = row["CustType"];
				RemittanceLicNO.DbValue = row["RemittanceLicNO"];
				MCLicNo.DbValue = row["MCLicNo"];
				BankYesNo.DbValue = ((ew_ConvertToBool(row["BankYesNo"])) ? "1" : "0");
				BankODLimit.DbValue = row["BankODLimit"];
				BankAcctNO.DbValue = row["BankAcctNO"];
				CreditLimit.DbValue = row["CreditLimit"];
				ReferBy.DbValue = row["ReferBy"];
				AgentImageName.DbValue = row["AgentImageName"];
				status.DbValue = row["status"];
				CreatedBy.DbValue = row["CreatedBy"];
				CreatedDate.DbValue = row["CreatedDate"];
				ModifiedUser.DbValue = row["ModifiedUser"];
				ModifiedDate.DbValue = row["ModifiedDate"];
				PPExpiryDate.DbValue = row["PPExpiryDate"];
				TTExpiryDate.DbValue = row["TTExpiryDate"];
				MCExpiryDate.DbValue = row["MCExpiryDate"];
				Action.DbValue = row["Action"];
				Remark.DbValue = row["Remark"];
				MCType.DbValue = row["MCType"];
				CustDOB.DbValue = row["CustDOB"];
				DefContactDOB.DbValue = row["DefContactDOB"];
				ScanImage.DbValue = row["ScanImage"];
				BizNature.DbValue = row["BizNature"];
				DefContactPOB.DbValue = row["DefContactPOB"];
				NewTran.DbValue = row["NewTran"];
				BizRegNo.DbValue = row["BizRegNo"];
				BizRegDate.DbValue = row["BizRegDate"];
				BizRegPlace.DbValue = row["BizRegPlace"];
				BizRegExpDate.DbValue = row["BizRegExpDate"];
				UnIncorpExec.DbValue = row["UnIncorpExec"];
				DefContactAuthorzLetter.DbValue = row["DefContactAuthorzLetter"];
				Politician.DbValue = row["Politician"];
				BizPartnerNo.DbValue = row["BizPartnerNo"];
				Remark2.DbValue = row["Remark2"];
				BannedListRemark.DbValue = row["BannedListRemark"];
			}
			#pragma warning restore 162, 168

			// Load DbValue from recordset
			public void LoadDbValues(OrderedDictionary row) {
				if (row == null)
					return;
				AgentId.SetDbValue(row["AgentId"]);
				AgentName.SetDbValue(row["AgentName"]);
				AgentRiskRating.SetDbValue(row["AgentRiskRating"]);
				AgentRiskCredit.SetDbValue(row["AgentRiskCredit"]);
				Address1.SetDbValue(row["Address1"]);
				Address2.SetDbValue(row["Address2"]);
				Address3.SetDbValue(row["Address3"]);
				Country.SetDbValue(row["Country"]);
				ZipCode.SetDbValue(row["ZipCode"]);
				Fax.SetDbValue(row["Fax"]);
				Phone.SetDbValue(row["Phone"]);
				Mobile.SetDbValue(row["Mobile"]);
				BuzType.SetDbValue(row["BuzType"]);
				ClassType.SetDbValue(row["ClassType"]);
				DefContactPName.SetDbValue(row["DefContactPName"]);
				DefContactPNric.SetDbValue(row["DefContactPNric"]);
				DefContactPNation.SetDbValue(row["DefContactPNation"]);
				DefContactPOccupation.SetDbValue(row["DefContactPOccupation"]);
				TermsId.SetDbValue(row["TermsId"]);
				LedgerBal.SetDbValue(row["LedgerBal"]);
				AvailableBal.SetDbValue(row["AvailableBal"]);
				_Email.SetDbValue(row["Email"]);
				URL.SetDbValue(row["URL"]);
				CustType.SetDbValue(row["CustType"]);
				RemittanceLicNO.SetDbValue(row["RemittanceLicNO"]);
				MCLicNo.SetDbValue(row["MCLicNo"]);
				BankYesNo.SetDbValue(((ew_ConvertToBool(row["BankYesNo"])) ? "1" : "0"));
				BankODLimit.SetDbValue(row["BankODLimit"]);
				BankAcctNO.SetDbValue(row["BankAcctNO"]);
				CreditLimit.SetDbValue(row["CreditLimit"]);
				ReferBy.SetDbValue(row["ReferBy"]);
				AgentImageName.SetDbValue(row["AgentImageName"]);
				status.SetDbValue(row["status"]);
				CreatedBy.SetDbValue(row["CreatedBy"]);
				CreatedDate.SetDbValue(row["CreatedDate"]);
				ModifiedUser.SetDbValue(row["ModifiedUser"]);
				ModifiedDate.SetDbValue(row["ModifiedDate"]);
				PPExpiryDate.SetDbValue(row["PPExpiryDate"]);
				TTExpiryDate.SetDbValue(row["TTExpiryDate"]);
				MCExpiryDate.SetDbValue(row["MCExpiryDate"]);
				Action.SetDbValue(row["Action"]);
				Remark.SetDbValue(row["Remark"]);
				MCType.SetDbValue(row["MCType"]);
				CustDOB.SetDbValue(row["CustDOB"]);
				DefContactDOB.SetDbValue(row["DefContactDOB"]);
				ScanImage.SetDbValue(row["ScanImage"]);
				BizNature.SetDbValue(row["BizNature"]);
				DefContactPOB.SetDbValue(row["DefContactPOB"]);
				NewTran.SetDbValue(row["NewTran"]);
				BizRegNo.SetDbValue(row["BizRegNo"]);
				BizRegDate.SetDbValue(row["BizRegDate"]);
				BizRegPlace.SetDbValue(row["BizRegPlace"]);
				BizRegExpDate.SetDbValue(row["BizRegExpDate"]);
				UnIncorpExec.SetDbValue(row["UnIncorpExec"]);
				DefContactAuthorzLetter.SetDbValue(row["DefContactAuthorzLetter"]);
				Politician.SetDbValue(row["Politician"]);
				BizPartnerNo.SetDbValue(row["BizPartnerNo"]);
				Remark2.SetDbValue(row["Remark2"]);
				BannedListRemark.SetDbValue(row["BannedListRemark"]);
			}
			#pragma warning disable 618

			// Load old record
			public bool LoadOldRecord(cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {

				// Load key values from Session
				bool bValidKey = true;
				if (ew_NotEmpty(GetKey("AgentId")))
					AgentId.CurrentValue = GetKey("AgentId"); // AgentId
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
				if (ew_SameStr(AgentRiskCredit.FormValue, AgentRiskCredit.CurrentValue) && ew_IsNumeric(ew_StrToFloat(AgentRiskCredit.CurrentValue)))
					AgentRiskCredit.CurrentValue = ew_StrToFloat(AgentRiskCredit.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(LedgerBal.FormValue, LedgerBal.CurrentValue) && ew_IsNumeric(ew_StrToFloat(LedgerBal.CurrentValue)))
					LedgerBal.CurrentValue = ew_StrToFloat(LedgerBal.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(AvailableBal.FormValue, AvailableBal.CurrentValue) && ew_IsNumeric(ew_StrToFloat(AvailableBal.CurrentValue)))
					AvailableBal.CurrentValue = ew_StrToFloat(AvailableBal.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(BankODLimit.FormValue, BankODLimit.CurrentValue) && ew_IsNumeric(ew_StrToFloat(BankODLimit.CurrentValue)))
					BankODLimit.CurrentValue = ew_StrToFloat(BankODLimit.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(CreditLimit.FormValue, CreditLimit.CurrentValue) && ew_IsNumeric(ew_StrToFloat(CreditLimit.CurrentValue)))
					CreditLimit.CurrentValue = ew_StrToFloat(CreditLimit.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// AgentId
				// AgentName
				// AgentRiskRating
				// AgentRiskCredit
				// Address1
				// Address2
				// Address3
				// Country
				// ZipCode
				// Fax
				// Phone
				// Mobile
				// BuzType
				// ClassType
				// DefContactPName
				// DefContactPNric
				// DefContactPNation
				// DefContactPOccupation
				// TermsId
				// LedgerBal
				// AvailableBal
				// _Email
				// URL
				// CustType
				// RemittanceLicNO
				// MCLicNo
				// BankYesNo
				// BankODLimit
				// BankAcctNO
				// CreditLimit
				// ReferBy
				// AgentImageName
				// status
				// CreatedBy
				// CreatedDate
				// ModifiedUser
				// ModifiedDate
				// PPExpiryDate
				// TTExpiryDate
				// MCExpiryDate
				// Action
				// Remark
				// MCType
				// CustDOB
				// DefContactDOB
				// ScanImage
				// BizNature
				// DefContactPOB
				// NewTran
				// BizRegNo
				// BizRegDate
				// BizRegPlace
				// BizRegExpDate
				// UnIncorpExec
				// DefContactAuthorzLetter
				// Politician
				// BizPartnerNo
				// Remark2
				// BannedListRemark

				if (RowType == EW_ROWTYPE_VIEW) { // View row

					// AgentId
					AgentId.ViewValue = AgentId.CurrentValue;

					// AgentName
					AgentName.ViewValue = AgentName.CurrentValue;

					// AgentRiskRating
					if (Convert.ToString(AgentRiskRating.CurrentValue) != "") {
							AgentRiskRating.ViewValue = AgentRiskRating.OptionCaption(Convert.ToString(AgentRiskRating.CurrentValue));
					} else {
						AgentRiskRating.ViewValue = System.DBNull.Value;
					}

					// AgentRiskCredit
					AgentRiskCredit.ViewValue = AgentRiskCredit.CurrentValue;

					// Address1
					Address1.ViewValue = Address1.CurrentValue;

					// Address2
					Address2.ViewValue = Address2.CurrentValue;

					// Address3
					Address3.ViewValue = Address3.CurrentValue;

					// Country
					if (ew_NotEmpty(Country.CurrentValue)) {
						sFilterWrk = "[varCountryCode]" + ew_SearchString("=", Convert.ToString(Country.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT [varCountryCode], [varCountryCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[tblStdCountry]";
						sWhereWrk = "";
						Country.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(Country, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [varCountry] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							Country.ViewValue = Country.DisplayValue(odwrk);
						} else {
							Country.ViewValue = Country.CurrentValue;
						}
					} else {
						Country.ViewValue = System.DBNull.Value;
					}

					// ZipCode
					ZipCode.ViewValue = ZipCode.CurrentValue;

					// Fax
					Fax.ViewValue = Fax.CurrentValue;

					// Phone
					Phone.ViewValue = Phone.CurrentValue;

					// Mobile
					Mobile.ViewValue = Mobile.CurrentValue;

					// BuzType
					BuzType.ViewValue = BuzType.CurrentValue;

					// ClassType
					ClassType.ViewValue = ClassType.CurrentValue;

					// DefContactPName
					DefContactPName.ViewValue = DefContactPName.CurrentValue;

					// DefContactPNric
					DefContactPNric.ViewValue = DefContactPNric.CurrentValue;

					// DefContactPNation
					DefContactPNation.ViewValue = DefContactPNation.CurrentValue;

					// DefContactPOccupation
					DefContactPOccupation.ViewValue = DefContactPOccupation.CurrentValue;

					// TermsId
					TermsId.ViewValue = TermsId.CurrentValue;

					// LedgerBal
					LedgerBal.ViewValue = LedgerBal.CurrentValue;

					// AvailableBal
					AvailableBal.ViewValue = AvailableBal.CurrentValue;

					// _Email
					_Email.ViewValue = _Email.CurrentValue;

					// URL
					URL.ViewValue = URL.CurrentValue;

					// CustType
					CustType.ViewValue = CustType.CurrentValue;

					// RemittanceLicNO
					RemittanceLicNO.ViewValue = RemittanceLicNO.CurrentValue;

					// MCLicNo
					MCLicNo.ViewValue = MCLicNo.CurrentValue;

					// BankYesNo
					if (ew_ConvertToBool(BankYesNo.CurrentValue)) {
						BankYesNo.ViewValue = (BankYesNo.FldTagCaption(1) != "") ? BankYesNo.FldTagCaption(1) : "Yes";
					} else {
						BankYesNo.ViewValue = (BankYesNo.FldTagCaption(2) != "") ? BankYesNo.FldTagCaption(2) : "No";
					}

					// BankODLimit
					BankODLimit.ViewValue = BankODLimit.CurrentValue;

					// BankAcctNO
					BankAcctNO.ViewValue = BankAcctNO.CurrentValue;

					// CreditLimit
					CreditLimit.ViewValue = CreditLimit.CurrentValue;

					// ReferBy
					ReferBy.ViewValue = ReferBy.CurrentValue;

					// AgentImageName
					AgentImageName.ViewValue = AgentImageName.CurrentValue;

					// status
					status.ViewValue = status.CurrentValue;

					// CreatedBy
					CreatedBy.ViewValue = CreatedBy.CurrentValue;

					// CreatedDate
					CreatedDate.ViewValue = CreatedDate.CurrentValue;
					CreatedDate.ViewValue = ew_FormatDateTime(CreatedDate.ViewValue, 0);

					// ModifiedUser
					ModifiedUser.ViewValue = ModifiedUser.CurrentValue;

					// ModifiedDate
					ModifiedDate.ViewValue = ModifiedDate.CurrentValue;
					ModifiedDate.ViewValue = ew_FormatDateTime(ModifiedDate.ViewValue, 0);

					// PPExpiryDate
					PPExpiryDate.ViewValue = PPExpiryDate.CurrentValue;
					PPExpiryDate.ViewValue = ew_FormatDateTime(PPExpiryDate.ViewValue, 0);

					// TTExpiryDate
					TTExpiryDate.ViewValue = TTExpiryDate.CurrentValue;
					TTExpiryDate.ViewValue = ew_FormatDateTime(TTExpiryDate.ViewValue, 0);

					// MCExpiryDate
					MCExpiryDate.ViewValue = MCExpiryDate.CurrentValue;
					MCExpiryDate.ViewValue = ew_FormatDateTime(MCExpiryDate.ViewValue, 0);

					// Action
					Action.ViewValue = Action.CurrentValue;

					// Remark
					Remark.ViewValue = Remark.CurrentValue;

					// MCType
					MCType.ViewValue = MCType.CurrentValue;

					// CustDOB
					CustDOB.ViewValue = CustDOB.CurrentValue;
					CustDOB.ViewValue = ew_FormatDateTime(CustDOB.ViewValue, 0);

					// DefContactDOB
					DefContactDOB.ViewValue = DefContactDOB.CurrentValue;
					DefContactDOB.ViewValue = ew_FormatDateTime(DefContactDOB.ViewValue, 0);

					// ScanImage
					ScanImage.ViewValue = ScanImage.CurrentValue;

					// BizNature
					BizNature.ViewValue = BizNature.CurrentValue;

					// DefContactPOB
					DefContactPOB.ViewValue = DefContactPOB.CurrentValue;

					// NewTran
					NewTran.ViewValue = NewTran.CurrentValue;

					// BizRegNo
					BizRegNo.ViewValue = BizRegNo.CurrentValue;

					// BizRegDate
					BizRegDate.ViewValue = BizRegDate.CurrentValue;
					BizRegDate.ViewValue = ew_FormatDateTime(BizRegDate.ViewValue, 0);

					// BizRegPlace
					BizRegPlace.ViewValue = BizRegPlace.CurrentValue;

					// BizRegExpDate
					BizRegExpDate.ViewValue = BizRegExpDate.CurrentValue;
					BizRegExpDate.ViewValue = ew_FormatDateTime(BizRegExpDate.ViewValue, 0);

					// UnIncorpExec
					UnIncorpExec.ViewValue = UnIncorpExec.CurrentValue;

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.ViewValue = DefContactAuthorzLetter.CurrentValue;

					// Politician
					Politician.ViewValue = Politician.CurrentValue;

					// BizPartnerNo
					BizPartnerNo.ViewValue = BizPartnerNo.CurrentValue;

					// Remark2
					Remark2.ViewValue = Remark2.CurrentValue;

					// BannedListRemark
					BannedListRemark.ViewValue = BannedListRemark.CurrentValue;

					// AgentId
					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";
					AgentId.TooltipValue = "";

					// AgentName
					AgentName.LinkCustomAttributes = AgentName.FldTagACustomAttributes; // DN
					AgentName.HrefValue = "";
					AgentName.TooltipValue = "";

					// AgentRiskRating
					AgentRiskRating.LinkCustomAttributes = AgentRiskRating.FldTagACustomAttributes; // DN
					AgentRiskRating.HrefValue = "";
					AgentRiskRating.TooltipValue = "";

					// AgentRiskCredit
					AgentRiskCredit.LinkCustomAttributes = AgentRiskCredit.FldTagACustomAttributes; // DN
					AgentRiskCredit.HrefValue = "";
					AgentRiskCredit.TooltipValue = "";

					// Address1
					Address1.LinkCustomAttributes = Address1.FldTagACustomAttributes; // DN
					Address1.HrefValue = "";
					Address1.TooltipValue = "";

					// Address2
					Address2.LinkCustomAttributes = Address2.FldTagACustomAttributes; // DN
					Address2.HrefValue = "";
					Address2.TooltipValue = "";

					// Address3
					Address3.LinkCustomAttributes = Address3.FldTagACustomAttributes; // DN
					Address3.HrefValue = "";
					Address3.TooltipValue = "";

					// Country
					Country.LinkCustomAttributes = Country.FldTagACustomAttributes; // DN
					Country.HrefValue = "";
					Country.TooltipValue = "";

					// ZipCode
					ZipCode.LinkCustomAttributes = ZipCode.FldTagACustomAttributes; // DN
					ZipCode.HrefValue = "";
					ZipCode.TooltipValue = "";

					// Fax
					Fax.LinkCustomAttributes = Fax.FldTagACustomAttributes; // DN
					Fax.HrefValue = "";
					Fax.TooltipValue = "";

					// Phone
					Phone.LinkCustomAttributes = Phone.FldTagACustomAttributes; // DN
					Phone.HrefValue = "";
					Phone.TooltipValue = "";

					// Mobile
					Mobile.LinkCustomAttributes = Mobile.FldTagACustomAttributes; // DN
					Mobile.HrefValue = "";
					Mobile.TooltipValue = "";

					// BuzType
					BuzType.LinkCustomAttributes = BuzType.FldTagACustomAttributes; // DN
					BuzType.HrefValue = "";
					BuzType.TooltipValue = "";

					// ClassType
					ClassType.LinkCustomAttributes = ClassType.FldTagACustomAttributes; // DN
					ClassType.HrefValue = "";
					ClassType.TooltipValue = "";

					// DefContactPName
					DefContactPName.LinkCustomAttributes = DefContactPName.FldTagACustomAttributes; // DN
					DefContactPName.HrefValue = "";
					DefContactPName.TooltipValue = "";

					// DefContactPNric
					DefContactPNric.LinkCustomAttributes = DefContactPNric.FldTagACustomAttributes; // DN
					DefContactPNric.HrefValue = "";
					DefContactPNric.TooltipValue = "";

					// DefContactPNation
					DefContactPNation.LinkCustomAttributes = DefContactPNation.FldTagACustomAttributes; // DN
					DefContactPNation.HrefValue = "";
					DefContactPNation.TooltipValue = "";

					// DefContactPOccupation
					DefContactPOccupation.LinkCustomAttributes = DefContactPOccupation.FldTagACustomAttributes; // DN
					DefContactPOccupation.HrefValue = "";
					DefContactPOccupation.TooltipValue = "";

					// TermsId
					TermsId.LinkCustomAttributes = TermsId.FldTagACustomAttributes; // DN
					TermsId.HrefValue = "";
					TermsId.TooltipValue = "";

					// LedgerBal
					LedgerBal.LinkCustomAttributes = LedgerBal.FldTagACustomAttributes; // DN
					LedgerBal.HrefValue = "";
					LedgerBal.TooltipValue = "";

					// AvailableBal
					AvailableBal.LinkCustomAttributes = AvailableBal.FldTagACustomAttributes; // DN
					AvailableBal.HrefValue = "";
					AvailableBal.TooltipValue = "";

					// _Email
					_Email.LinkCustomAttributes = _Email.FldTagACustomAttributes; // DN
					_Email.HrefValue = "";
					_Email.TooltipValue = "";

					// URL
					URL.LinkCustomAttributes = URL.FldTagACustomAttributes; // DN
					URL.HrefValue = "";
					URL.TooltipValue = "";

					// CustType
					CustType.LinkCustomAttributes = CustType.FldTagACustomAttributes; // DN
					CustType.HrefValue = "";
					CustType.TooltipValue = "";

					// RemittanceLicNO
					RemittanceLicNO.LinkCustomAttributes = RemittanceLicNO.FldTagACustomAttributes; // DN
					RemittanceLicNO.HrefValue = "";
					RemittanceLicNO.TooltipValue = "";

					// MCLicNo
					MCLicNo.LinkCustomAttributes = MCLicNo.FldTagACustomAttributes; // DN
					MCLicNo.HrefValue = "";
					MCLicNo.TooltipValue = "";

					// BankYesNo
					BankYesNo.LinkCustomAttributes = BankYesNo.FldTagACustomAttributes; // DN
					BankYesNo.HrefValue = "";
					BankYesNo.TooltipValue = "";

					// BankODLimit
					BankODLimit.LinkCustomAttributes = BankODLimit.FldTagACustomAttributes; // DN
					BankODLimit.HrefValue = "";
					BankODLimit.TooltipValue = "";

					// BankAcctNO
					BankAcctNO.LinkCustomAttributes = BankAcctNO.FldTagACustomAttributes; // DN
					BankAcctNO.HrefValue = "";
					BankAcctNO.TooltipValue = "";

					// CreditLimit
					CreditLimit.LinkCustomAttributes = CreditLimit.FldTagACustomAttributes; // DN
					CreditLimit.HrefValue = "";
					CreditLimit.TooltipValue = "";

					// ReferBy
					ReferBy.LinkCustomAttributes = ReferBy.FldTagACustomAttributes; // DN
					ReferBy.HrefValue = "";
					ReferBy.TooltipValue = "";

					// AgentImageName
					AgentImageName.LinkCustomAttributes = AgentImageName.FldTagACustomAttributes; // DN
					AgentImageName.HrefValue = "";
					AgentImageName.TooltipValue = "";

					// status
					status.LinkCustomAttributes = status.FldTagACustomAttributes; // DN
					status.HrefValue = "";
					status.TooltipValue = "";

					// CreatedBy
					CreatedBy.LinkCustomAttributes = CreatedBy.FldTagACustomAttributes; // DN
					CreatedBy.HrefValue = "";
					CreatedBy.TooltipValue = "";

					// CreatedDate
					CreatedDate.LinkCustomAttributes = CreatedDate.FldTagACustomAttributes; // DN
					CreatedDate.HrefValue = "";
					CreatedDate.TooltipValue = "";

					// ModifiedUser
					ModifiedUser.LinkCustomAttributes = ModifiedUser.FldTagACustomAttributes; // DN
					ModifiedUser.HrefValue = "";
					ModifiedUser.TooltipValue = "";

					// ModifiedDate
					ModifiedDate.LinkCustomAttributes = ModifiedDate.FldTagACustomAttributes; // DN
					ModifiedDate.HrefValue = "";
					ModifiedDate.TooltipValue = "";

					// PPExpiryDate
					PPExpiryDate.LinkCustomAttributes = PPExpiryDate.FldTagACustomAttributes; // DN
					PPExpiryDate.HrefValue = "";
					PPExpiryDate.TooltipValue = "";

					// TTExpiryDate
					TTExpiryDate.LinkCustomAttributes = TTExpiryDate.FldTagACustomAttributes; // DN
					TTExpiryDate.HrefValue = "";
					TTExpiryDate.TooltipValue = "";

					// MCExpiryDate
					MCExpiryDate.LinkCustomAttributes = MCExpiryDate.FldTagACustomAttributes; // DN
					MCExpiryDate.HrefValue = "";
					MCExpiryDate.TooltipValue = "";

					// Action
					Action.LinkCustomAttributes = Action.FldTagACustomAttributes; // DN
					Action.HrefValue = "";
					Action.TooltipValue = "";

					// Remark
					Remark.LinkCustomAttributes = Remark.FldTagACustomAttributes; // DN
					Remark.HrefValue = "";
					Remark.TooltipValue = "";

					// MCType
					MCType.LinkCustomAttributes = MCType.FldTagACustomAttributes; // DN
					MCType.HrefValue = "";
					MCType.TooltipValue = "";

					// CustDOB
					CustDOB.LinkCustomAttributes = CustDOB.FldTagACustomAttributes; // DN
					CustDOB.HrefValue = "";
					CustDOB.TooltipValue = "";

					// DefContactDOB
					DefContactDOB.LinkCustomAttributes = DefContactDOB.FldTagACustomAttributes; // DN
					DefContactDOB.HrefValue = "";
					DefContactDOB.TooltipValue = "";

					// ScanImage
					ScanImage.LinkCustomAttributes = ScanImage.FldTagACustomAttributes; // DN
					ScanImage.HrefValue = "";
					ScanImage.TooltipValue = "";

					// BizNature
					BizNature.LinkCustomAttributes = BizNature.FldTagACustomAttributes; // DN
					BizNature.HrefValue = "";
					BizNature.TooltipValue = "";

					// DefContactPOB
					DefContactPOB.LinkCustomAttributes = DefContactPOB.FldTagACustomAttributes; // DN
					DefContactPOB.HrefValue = "";
					DefContactPOB.TooltipValue = "";

					// NewTran
					NewTran.LinkCustomAttributes = NewTran.FldTagACustomAttributes; // DN
					NewTran.HrefValue = "";
					NewTran.TooltipValue = "";

					// BizRegNo
					BizRegNo.LinkCustomAttributes = BizRegNo.FldTagACustomAttributes; // DN
					BizRegNo.HrefValue = "";
					BizRegNo.TooltipValue = "";

					// BizRegDate
					BizRegDate.LinkCustomAttributes = BizRegDate.FldTagACustomAttributes; // DN
					BizRegDate.HrefValue = "";
					BizRegDate.TooltipValue = "";

					// BizRegPlace
					BizRegPlace.LinkCustomAttributes = BizRegPlace.FldTagACustomAttributes; // DN
					BizRegPlace.HrefValue = "";
					BizRegPlace.TooltipValue = "";

					// BizRegExpDate
					BizRegExpDate.LinkCustomAttributes = BizRegExpDate.FldTagACustomAttributes; // DN
					BizRegExpDate.HrefValue = "";
					BizRegExpDate.TooltipValue = "";

					// UnIncorpExec
					UnIncorpExec.LinkCustomAttributes = UnIncorpExec.FldTagACustomAttributes; // DN
					UnIncorpExec.HrefValue = "";
					UnIncorpExec.TooltipValue = "";

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.LinkCustomAttributes = DefContactAuthorzLetter.FldTagACustomAttributes; // DN
					DefContactAuthorzLetter.HrefValue = "";
					DefContactAuthorzLetter.TooltipValue = "";

					// Politician
					Politician.LinkCustomAttributes = Politician.FldTagACustomAttributes; // DN
					Politician.HrefValue = "";
					Politician.TooltipValue = "";

					// BizPartnerNo
					BizPartnerNo.LinkCustomAttributes = BizPartnerNo.FldTagACustomAttributes; // DN
					BizPartnerNo.HrefValue = "";
					BizPartnerNo.TooltipValue = "";

					// Remark2
					Remark2.LinkCustomAttributes = Remark2.FldTagACustomAttributes; // DN
					Remark2.HrefValue = "";
					Remark2.TooltipValue = "";

					// BannedListRemark
					BannedListRemark.LinkCustomAttributes = BannedListRemark.FldTagACustomAttributes; // DN
					BannedListRemark.HrefValue = "";
					BannedListRemark.TooltipValue = "";
				} else if (RowType == EW_ROWTYPE_ADD) { // Add row

					// AgentId
					AgentId.EditAttrs["class"] = "form-control";
					AgentId.EditValue = AgentId.CurrentValue; // DN
					AgentId.PlaceHolder = ew_RemoveHtml(AgentId.FldCaption);

					// AgentName
					AgentName.EditAttrs["class"] = "form-control";
					AgentName.EditValue = AgentName.CurrentValue; // DN
					AgentName.PlaceHolder = ew_RemoveHtml(AgentName.FldCaption);

					// AgentRiskRating
					AgentRiskRating.EditAttrs["class"] = "form-control";
					AgentRiskRating.EditValue = AgentRiskRating.Options(true);

					// AgentRiskCredit
					AgentRiskCredit.EditAttrs["class"] = "form-control";
					AgentRiskCredit.EditValue = AgentRiskCredit.CurrentValue; // DN
					AgentRiskCredit.PlaceHolder = ew_RemoveHtml(AgentRiskCredit.FldCaption);
					if (ew_NotEmpty(AgentRiskCredit.EditValue) && ew_IsNumeric(Convert.ToString(AgentRiskCredit.EditValue))) {
					AgentRiskCredit.EditValue = ew_FormatNumber(AgentRiskCredit.EditValue, -2, -1, -2, 0);
					AgentRiskCredit.OldValue = AgentRiskCredit.EditValue;
					}

					// Address1
					Address1.EditAttrs["class"] = "form-control";
					Address1.EditValue = Address1.CurrentValue; // DN
					Address1.PlaceHolder = ew_RemoveHtml(Address1.FldCaption);

					// Address2
					Address2.EditAttrs["class"] = "form-control";
					Address2.EditValue = Address2.CurrentValue; // DN
					Address2.PlaceHolder = ew_RemoveHtml(Address2.FldCaption);

					// Address3
					Address3.EditAttrs["class"] = "form-control";
					Address3.EditValue = Address3.CurrentValue; // DN
					Address3.PlaceHolder = ew_RemoveHtml(Address3.FldCaption);

					// Country
					Country.EditAttrs["class"] = "form-control";
						if (ew_Empty(Country.CurrentValue)) {
							sFilterWrk = "0=1";
						} else {
							sFilterWrk = "[varCountryCode]" + ew_SearchString("=", Convert.ToString(Country.CurrentValue), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT [varCountryCode], [varCountryCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[tblStdCountry]";
						sWhereWrk = "";
						Country.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(Country, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [varCountry] ASC/*EndOrderBy*/";
					rswrk = ew_GetConn("").GetRows(sSqlWrk);
					Country.EditValue = rswrk;

					// ZipCode
					ZipCode.EditAttrs["class"] = "form-control";
					ZipCode.EditValue = ZipCode.CurrentValue; // DN
					ZipCode.PlaceHolder = ew_RemoveHtml(ZipCode.FldCaption);

					// Fax
					Fax.EditAttrs["class"] = "form-control";
					Fax.EditValue = Fax.CurrentValue; // DN
					Fax.PlaceHolder = ew_RemoveHtml(Fax.FldCaption);

					// Phone
					Phone.EditAttrs["class"] = "form-control";
					Phone.EditValue = Phone.CurrentValue; // DN
					Phone.PlaceHolder = ew_RemoveHtml(Phone.FldCaption);

					// Mobile
					Mobile.EditAttrs["class"] = "form-control";
					Mobile.EditValue = Mobile.CurrentValue; // DN
					Mobile.PlaceHolder = ew_RemoveHtml(Mobile.FldCaption);

					// BuzType
					BuzType.EditAttrs["class"] = "form-control";
					BuzType.EditValue = BuzType.CurrentValue; // DN
					BuzType.PlaceHolder = ew_RemoveHtml(BuzType.FldCaption);

					// ClassType
					ClassType.EditAttrs["class"] = "form-control";
					ClassType.EditValue = ClassType.CurrentValue; // DN
					ClassType.PlaceHolder = ew_RemoveHtml(ClassType.FldCaption);

					// DefContactPName
					DefContactPName.EditAttrs["class"] = "form-control";
					DefContactPName.EditValue = DefContactPName.CurrentValue; // DN
					DefContactPName.PlaceHolder = ew_RemoveHtml(DefContactPName.FldCaption);

					// DefContactPNric
					DefContactPNric.EditAttrs["class"] = "form-control";
					DefContactPNric.EditValue = DefContactPNric.CurrentValue; // DN
					DefContactPNric.PlaceHolder = ew_RemoveHtml(DefContactPNric.FldCaption);

					// DefContactPNation
					DefContactPNation.EditAttrs["class"] = "form-control";
					DefContactPNation.EditValue = DefContactPNation.CurrentValue; // DN
					DefContactPNation.PlaceHolder = ew_RemoveHtml(DefContactPNation.FldCaption);

					// DefContactPOccupation
					DefContactPOccupation.EditAttrs["class"] = "form-control";
					DefContactPOccupation.EditValue = DefContactPOccupation.CurrentValue; // DN
					DefContactPOccupation.PlaceHolder = ew_RemoveHtml(DefContactPOccupation.FldCaption);

					// TermsId
					TermsId.EditAttrs["class"] = "form-control";
					TermsId.EditValue = TermsId.CurrentValue; // DN
					TermsId.PlaceHolder = ew_RemoveHtml(TermsId.FldCaption);

					// LedgerBal
					LedgerBal.EditAttrs["class"] = "form-control";
					LedgerBal.EditValue = LedgerBal.CurrentValue; // DN
					LedgerBal.PlaceHolder = ew_RemoveHtml(LedgerBal.FldCaption);
					if (ew_NotEmpty(LedgerBal.EditValue) && ew_IsNumeric(Convert.ToString(LedgerBal.EditValue))) {
					LedgerBal.EditValue = ew_FormatNumber(LedgerBal.EditValue, -2, -1, -2, 0);
					LedgerBal.OldValue = LedgerBal.EditValue;
					}

					// AvailableBal
					AvailableBal.EditAttrs["class"] = "form-control";
					AvailableBal.EditValue = AvailableBal.CurrentValue; // DN
					AvailableBal.PlaceHolder = ew_RemoveHtml(AvailableBal.FldCaption);
					if (ew_NotEmpty(AvailableBal.EditValue) && ew_IsNumeric(Convert.ToString(AvailableBal.EditValue))) {
					AvailableBal.EditValue = ew_FormatNumber(AvailableBal.EditValue, -2, -1, -2, 0);
					AvailableBal.OldValue = AvailableBal.EditValue;
					}

					// _Email
					_Email.EditAttrs["class"] = "form-control";
					_Email.EditValue = _Email.CurrentValue; // DN
					_Email.PlaceHolder = ew_RemoveHtml(_Email.FldCaption);

					// URL
					URL.EditAttrs["class"] = "form-control";
					URL.EditValue = URL.CurrentValue; // DN
					URL.PlaceHolder = ew_RemoveHtml(URL.FldCaption);

					// CustType
					CustType.EditAttrs["class"] = "form-control";
					CustType.EditValue = CustType.CurrentValue; // DN
					CustType.PlaceHolder = ew_RemoveHtml(CustType.FldCaption);

					// RemittanceLicNO
					RemittanceLicNO.EditAttrs["class"] = "form-control";
					RemittanceLicNO.EditValue = RemittanceLicNO.CurrentValue; // DN
					RemittanceLicNO.PlaceHolder = ew_RemoveHtml(RemittanceLicNO.FldCaption);

					// MCLicNo
					MCLicNo.EditAttrs["class"] = "form-control";
					MCLicNo.EditValue = MCLicNo.CurrentValue; // DN
					MCLicNo.PlaceHolder = ew_RemoveHtml(MCLicNo.FldCaption);

					// BankYesNo
					BankYesNo.EditValue = BankYesNo.Options(false);

					// BankODLimit
					BankODLimit.EditAttrs["class"] = "form-control";
					BankODLimit.EditValue = BankODLimit.CurrentValue; // DN
					BankODLimit.PlaceHolder = ew_RemoveHtml(BankODLimit.FldCaption);
					if (ew_NotEmpty(BankODLimit.EditValue) && ew_IsNumeric(Convert.ToString(BankODLimit.EditValue))) {
					BankODLimit.EditValue = ew_FormatNumber(BankODLimit.EditValue, -2, -1, -2, 0);
					BankODLimit.OldValue = BankODLimit.EditValue;
					}

					// BankAcctNO
					BankAcctNO.EditAttrs["class"] = "form-control";
					BankAcctNO.EditValue = BankAcctNO.CurrentValue; // DN
					BankAcctNO.PlaceHolder = ew_RemoveHtml(BankAcctNO.FldCaption);

					// CreditLimit
					CreditLimit.EditAttrs["class"] = "form-control";
					CreditLimit.EditValue = CreditLimit.CurrentValue; // DN
					CreditLimit.PlaceHolder = ew_RemoveHtml(CreditLimit.FldCaption);
					if (ew_NotEmpty(CreditLimit.EditValue) && ew_IsNumeric(Convert.ToString(CreditLimit.EditValue))) {
					CreditLimit.EditValue = ew_FormatNumber(CreditLimit.EditValue, -2, -1, -2, 0);
					CreditLimit.OldValue = CreditLimit.EditValue;
					}

					// ReferBy
					ReferBy.EditAttrs["class"] = "form-control";
					ReferBy.EditValue = ReferBy.CurrentValue; // DN
					ReferBy.PlaceHolder = ew_RemoveHtml(ReferBy.FldCaption);

					// AgentImageName
					AgentImageName.EditAttrs["class"] = "form-control";
					AgentImageName.EditValue = AgentImageName.CurrentValue; // DN
					AgentImageName.PlaceHolder = ew_RemoveHtml(AgentImageName.FldCaption);

					// status
					status.EditAttrs["class"] = "form-control";
					status.EditValue = status.CurrentValue; // DN
					status.PlaceHolder = ew_RemoveHtml(status.FldCaption);

					// CreatedBy
					CreatedBy.EditAttrs["class"] = "form-control";
					CreatedBy.EditValue = CreatedBy.CurrentValue; // DN
					CreatedBy.PlaceHolder = ew_RemoveHtml(CreatedBy.FldCaption);

					// CreatedDate
					CreatedDate.EditAttrs["class"] = "form-control";
					CreatedDate.EditValue = ew_FormatDateTime(CreatedDate.CurrentValue, 8); // DN
					CreatedDate.PlaceHolder = ew_RemoveHtml(CreatedDate.FldCaption);

					// ModifiedUser
					ModifiedUser.EditAttrs["class"] = "form-control";
					ModifiedUser.EditValue = ModifiedUser.CurrentValue; // DN
					ModifiedUser.PlaceHolder = ew_RemoveHtml(ModifiedUser.FldCaption);

					// ModifiedDate
					ModifiedDate.EditAttrs["class"] = "form-control";
					ModifiedDate.EditValue = ew_FormatDateTime(ModifiedDate.CurrentValue, 8); // DN
					ModifiedDate.PlaceHolder = ew_RemoveHtml(ModifiedDate.FldCaption);

					// PPExpiryDate
					PPExpiryDate.EditAttrs["class"] = "form-control";
					PPExpiryDate.EditValue = ew_FormatDateTime(PPExpiryDate.CurrentValue, 8); // DN
					PPExpiryDate.PlaceHolder = ew_RemoveHtml(PPExpiryDate.FldCaption);

					// TTExpiryDate
					TTExpiryDate.EditAttrs["class"] = "form-control";
					TTExpiryDate.EditValue = ew_FormatDateTime(TTExpiryDate.CurrentValue, 8); // DN
					TTExpiryDate.PlaceHolder = ew_RemoveHtml(TTExpiryDate.FldCaption);

					// MCExpiryDate
					MCExpiryDate.EditAttrs["class"] = "form-control";
					MCExpiryDate.EditValue = ew_FormatDateTime(MCExpiryDate.CurrentValue, 8); // DN
					MCExpiryDate.PlaceHolder = ew_RemoveHtml(MCExpiryDate.FldCaption);

					// Action
					Action.EditAttrs["class"] = "form-control";
					Action.EditValue = Action.CurrentValue; // DN
					Action.PlaceHolder = ew_RemoveHtml(Action.FldCaption);

					// Remark
					Remark.EditAttrs["class"] = "form-control";
					Remark.EditValue = Remark.CurrentValue; // DN
					Remark.PlaceHolder = ew_RemoveHtml(Remark.FldCaption);

					// MCType
					MCType.EditAttrs["class"] = "form-control";
					MCType.EditValue = MCType.CurrentValue; // DN
					MCType.PlaceHolder = ew_RemoveHtml(MCType.FldCaption);

					// CustDOB
					CustDOB.EditAttrs["class"] = "form-control";
					CustDOB.EditValue = ew_FormatDateTime(CustDOB.CurrentValue, 8); // DN
					CustDOB.PlaceHolder = ew_RemoveHtml(CustDOB.FldCaption);

					// DefContactDOB
					DefContactDOB.EditAttrs["class"] = "form-control";
					DefContactDOB.EditValue = ew_FormatDateTime(DefContactDOB.CurrentValue, 8); // DN
					DefContactDOB.PlaceHolder = ew_RemoveHtml(DefContactDOB.FldCaption);

					// ScanImage
					ScanImage.EditAttrs["class"] = "form-control";
					ScanImage.EditValue = ScanImage.CurrentValue; // DN
					ScanImage.PlaceHolder = ew_RemoveHtml(ScanImage.FldCaption);

					// BizNature
					BizNature.EditAttrs["class"] = "form-control";
					BizNature.EditValue = BizNature.CurrentValue; // DN
					BizNature.PlaceHolder = ew_RemoveHtml(BizNature.FldCaption);

					// DefContactPOB
					DefContactPOB.EditAttrs["class"] = "form-control";
					DefContactPOB.EditValue = DefContactPOB.CurrentValue; // DN
					DefContactPOB.PlaceHolder = ew_RemoveHtml(DefContactPOB.FldCaption);

					// NewTran
					NewTran.EditAttrs["class"] = "form-control";
					NewTran.EditValue = NewTran.CurrentValue; // DN
					NewTran.PlaceHolder = ew_RemoveHtml(NewTran.FldCaption);

					// BizRegNo
					BizRegNo.EditAttrs["class"] = "form-control";
					BizRegNo.EditValue = BizRegNo.CurrentValue; // DN
					BizRegNo.PlaceHolder = ew_RemoveHtml(BizRegNo.FldCaption);

					// BizRegDate
					BizRegDate.EditAttrs["class"] = "form-control";
					BizRegDate.EditValue = ew_FormatDateTime(BizRegDate.CurrentValue, 8); // DN
					BizRegDate.PlaceHolder = ew_RemoveHtml(BizRegDate.FldCaption);

					// BizRegPlace
					BizRegPlace.EditAttrs["class"] = "form-control";
					BizRegPlace.EditValue = BizRegPlace.CurrentValue; // DN
					BizRegPlace.PlaceHolder = ew_RemoveHtml(BizRegPlace.FldCaption);

					// BizRegExpDate
					BizRegExpDate.EditAttrs["class"] = "form-control";
					BizRegExpDate.EditValue = ew_FormatDateTime(BizRegExpDate.CurrentValue, 8); // DN
					BizRegExpDate.PlaceHolder = ew_RemoveHtml(BizRegExpDate.FldCaption);

					// UnIncorpExec
					UnIncorpExec.EditAttrs["class"] = "form-control";
					UnIncorpExec.EditValue = UnIncorpExec.CurrentValue; // DN
					UnIncorpExec.PlaceHolder = ew_RemoveHtml(UnIncorpExec.FldCaption);

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.EditAttrs["class"] = "form-control";
					DefContactAuthorzLetter.EditValue = DefContactAuthorzLetter.CurrentValue; // DN
					DefContactAuthorzLetter.PlaceHolder = ew_RemoveHtml(DefContactAuthorzLetter.FldCaption);

					// Politician
					Politician.EditAttrs["class"] = "form-control";
					Politician.EditValue = Politician.CurrentValue; // DN
					Politician.PlaceHolder = ew_RemoveHtml(Politician.FldCaption);

					// BizPartnerNo
					BizPartnerNo.EditAttrs["class"] = "form-control";
					BizPartnerNo.EditValue = BizPartnerNo.CurrentValue; // DN
					BizPartnerNo.PlaceHolder = ew_RemoveHtml(BizPartnerNo.FldCaption);

					// Remark2
					Remark2.EditAttrs["class"] = "form-control";
					Remark2.EditValue = Remark2.CurrentValue; // DN
					Remark2.PlaceHolder = ew_RemoveHtml(Remark2.FldCaption);

					// BannedListRemark
					BannedListRemark.EditAttrs["class"] = "form-control";
					BannedListRemark.EditValue = BannedListRemark.CurrentValue; // DN
					BannedListRemark.PlaceHolder = ew_RemoveHtml(BannedListRemark.FldCaption);

					// Add refer script
					// AgentId

					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";

					// AgentName
					AgentName.LinkCustomAttributes = AgentName.FldTagACustomAttributes; // DN
					AgentName.HrefValue = "";

					// AgentRiskRating
					AgentRiskRating.LinkCustomAttributes = AgentRiskRating.FldTagACustomAttributes; // DN
					AgentRiskRating.HrefValue = "";

					// AgentRiskCredit
					AgentRiskCredit.LinkCustomAttributes = AgentRiskCredit.FldTagACustomAttributes; // DN
					AgentRiskCredit.HrefValue = "";

					// Address1
					Address1.LinkCustomAttributes = Address1.FldTagACustomAttributes; // DN
					Address1.HrefValue = "";

					// Address2
					Address2.LinkCustomAttributes = Address2.FldTagACustomAttributes; // DN
					Address2.HrefValue = "";

					// Address3
					Address3.LinkCustomAttributes = Address3.FldTagACustomAttributes; // DN
					Address3.HrefValue = "";

					// Country
					Country.LinkCustomAttributes = Country.FldTagACustomAttributes; // DN
					Country.HrefValue = "";

					// ZipCode
					ZipCode.LinkCustomAttributes = ZipCode.FldTagACustomAttributes; // DN
					ZipCode.HrefValue = "";

					// Fax
					Fax.LinkCustomAttributes = Fax.FldTagACustomAttributes; // DN
					Fax.HrefValue = "";

					// Phone
					Phone.LinkCustomAttributes = Phone.FldTagACustomAttributes; // DN
					Phone.HrefValue = "";

					// Mobile
					Mobile.LinkCustomAttributes = Mobile.FldTagACustomAttributes; // DN
					Mobile.HrefValue = "";

					// BuzType
					BuzType.LinkCustomAttributes = BuzType.FldTagACustomAttributes; // DN
					BuzType.HrefValue = "";

					// ClassType
					ClassType.LinkCustomAttributes = ClassType.FldTagACustomAttributes; // DN
					ClassType.HrefValue = "";

					// DefContactPName
					DefContactPName.LinkCustomAttributes = DefContactPName.FldTagACustomAttributes; // DN
					DefContactPName.HrefValue = "";

					// DefContactPNric
					DefContactPNric.LinkCustomAttributes = DefContactPNric.FldTagACustomAttributes; // DN
					DefContactPNric.HrefValue = "";

					// DefContactPNation
					DefContactPNation.LinkCustomAttributes = DefContactPNation.FldTagACustomAttributes; // DN
					DefContactPNation.HrefValue = "";

					// DefContactPOccupation
					DefContactPOccupation.LinkCustomAttributes = DefContactPOccupation.FldTagACustomAttributes; // DN
					DefContactPOccupation.HrefValue = "";

					// TermsId
					TermsId.LinkCustomAttributes = TermsId.FldTagACustomAttributes; // DN
					TermsId.HrefValue = "";

					// LedgerBal
					LedgerBal.LinkCustomAttributes = LedgerBal.FldTagACustomAttributes; // DN
					LedgerBal.HrefValue = "";

					// AvailableBal
					AvailableBal.LinkCustomAttributes = AvailableBal.FldTagACustomAttributes; // DN
					AvailableBal.HrefValue = "";

					// _Email
					_Email.LinkCustomAttributes = _Email.FldTagACustomAttributes; // DN
					_Email.HrefValue = "";

					// URL
					URL.LinkCustomAttributes = URL.FldTagACustomAttributes; // DN
					URL.HrefValue = "";

					// CustType
					CustType.LinkCustomAttributes = CustType.FldTagACustomAttributes; // DN
					CustType.HrefValue = "";

					// RemittanceLicNO
					RemittanceLicNO.LinkCustomAttributes = RemittanceLicNO.FldTagACustomAttributes; // DN
					RemittanceLicNO.HrefValue = "";

					// MCLicNo
					MCLicNo.LinkCustomAttributes = MCLicNo.FldTagACustomAttributes; // DN
					MCLicNo.HrefValue = "";

					// BankYesNo
					BankYesNo.LinkCustomAttributes = BankYesNo.FldTagACustomAttributes; // DN
					BankYesNo.HrefValue = "";

					// BankODLimit
					BankODLimit.LinkCustomAttributes = BankODLimit.FldTagACustomAttributes; // DN
					BankODLimit.HrefValue = "";

					// BankAcctNO
					BankAcctNO.LinkCustomAttributes = BankAcctNO.FldTagACustomAttributes; // DN
					BankAcctNO.HrefValue = "";

					// CreditLimit
					CreditLimit.LinkCustomAttributes = CreditLimit.FldTagACustomAttributes; // DN
					CreditLimit.HrefValue = "";

					// ReferBy
					ReferBy.LinkCustomAttributes = ReferBy.FldTagACustomAttributes; // DN
					ReferBy.HrefValue = "";

					// AgentImageName
					AgentImageName.LinkCustomAttributes = AgentImageName.FldTagACustomAttributes; // DN
					AgentImageName.HrefValue = "";

					// status
					status.LinkCustomAttributes = status.FldTagACustomAttributes; // DN
					status.HrefValue = "";

					// CreatedBy
					CreatedBy.LinkCustomAttributes = CreatedBy.FldTagACustomAttributes; // DN
					CreatedBy.HrefValue = "";

					// CreatedDate
					CreatedDate.LinkCustomAttributes = CreatedDate.FldTagACustomAttributes; // DN
					CreatedDate.HrefValue = "";

					// ModifiedUser
					ModifiedUser.LinkCustomAttributes = ModifiedUser.FldTagACustomAttributes; // DN
					ModifiedUser.HrefValue = "";

					// ModifiedDate
					ModifiedDate.LinkCustomAttributes = ModifiedDate.FldTagACustomAttributes; // DN
					ModifiedDate.HrefValue = "";

					// PPExpiryDate
					PPExpiryDate.LinkCustomAttributes = PPExpiryDate.FldTagACustomAttributes; // DN
					PPExpiryDate.HrefValue = "";

					// TTExpiryDate
					TTExpiryDate.LinkCustomAttributes = TTExpiryDate.FldTagACustomAttributes; // DN
					TTExpiryDate.HrefValue = "";

					// MCExpiryDate
					MCExpiryDate.LinkCustomAttributes = MCExpiryDate.FldTagACustomAttributes; // DN
					MCExpiryDate.HrefValue = "";

					// Action
					Action.LinkCustomAttributes = Action.FldTagACustomAttributes; // DN
					Action.HrefValue = "";

					// Remark
					Remark.LinkCustomAttributes = Remark.FldTagACustomAttributes; // DN
					Remark.HrefValue = "";

					// MCType
					MCType.LinkCustomAttributes = MCType.FldTagACustomAttributes; // DN
					MCType.HrefValue = "";

					// CustDOB
					CustDOB.LinkCustomAttributes = CustDOB.FldTagACustomAttributes; // DN
					CustDOB.HrefValue = "";

					// DefContactDOB
					DefContactDOB.LinkCustomAttributes = DefContactDOB.FldTagACustomAttributes; // DN
					DefContactDOB.HrefValue = "";

					// ScanImage
					ScanImage.LinkCustomAttributes = ScanImage.FldTagACustomAttributes; // DN
					ScanImage.HrefValue = "";

					// BizNature
					BizNature.LinkCustomAttributes = BizNature.FldTagACustomAttributes; // DN
					BizNature.HrefValue = "";

					// DefContactPOB
					DefContactPOB.LinkCustomAttributes = DefContactPOB.FldTagACustomAttributes; // DN
					DefContactPOB.HrefValue = "";

					// NewTran
					NewTran.LinkCustomAttributes = NewTran.FldTagACustomAttributes; // DN
					NewTran.HrefValue = "";

					// BizRegNo
					BizRegNo.LinkCustomAttributes = BizRegNo.FldTagACustomAttributes; // DN
					BizRegNo.HrefValue = "";

					// BizRegDate
					BizRegDate.LinkCustomAttributes = BizRegDate.FldTagACustomAttributes; // DN
					BizRegDate.HrefValue = "";

					// BizRegPlace
					BizRegPlace.LinkCustomAttributes = BizRegPlace.FldTagACustomAttributes; // DN
					BizRegPlace.HrefValue = "";

					// BizRegExpDate
					BizRegExpDate.LinkCustomAttributes = BizRegExpDate.FldTagACustomAttributes; // DN
					BizRegExpDate.HrefValue = "";

					// UnIncorpExec
					UnIncorpExec.LinkCustomAttributes = UnIncorpExec.FldTagACustomAttributes; // DN
					UnIncorpExec.HrefValue = "";

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.LinkCustomAttributes = DefContactAuthorzLetter.FldTagACustomAttributes; // DN
					DefContactAuthorzLetter.HrefValue = "";

					// Politician
					Politician.LinkCustomAttributes = Politician.FldTagACustomAttributes; // DN
					Politician.HrefValue = "";

					// BizPartnerNo
					BizPartnerNo.LinkCustomAttributes = BizPartnerNo.FldTagACustomAttributes; // DN
					BizPartnerNo.HrefValue = "";

					// Remark2
					Remark2.LinkCustomAttributes = Remark2.FldTagACustomAttributes; // DN
					Remark2.HrefValue = "";

					// BannedListRemark
					BannedListRemark.LinkCustomAttributes = BannedListRemark.FldTagACustomAttributes; // DN
					BannedListRemark.HrefValue = "";
				} else if (RowType == EW_ROWTYPE_EDIT) { // Edit row

					// AgentId
					AgentId.EditAttrs["class"] = "form-control";
					AgentId.EditValue = AgentId.CurrentValue;

					// AgentName
					AgentName.EditAttrs["class"] = "form-control";
					AgentName.EditValue = AgentName.CurrentValue; // DN
					AgentName.PlaceHolder = ew_RemoveHtml(AgentName.FldCaption);

					// AgentRiskRating
					AgentRiskRating.EditAttrs["class"] = "form-control";
					AgentRiskRating.EditValue = AgentRiskRating.Options(true);

					// AgentRiskCredit
					AgentRiskCredit.EditAttrs["class"] = "form-control";
					AgentRiskCredit.EditValue = AgentRiskCredit.CurrentValue; // DN
					AgentRiskCredit.PlaceHolder = ew_RemoveHtml(AgentRiskCredit.FldCaption);
					if (ew_NotEmpty(AgentRiskCredit.EditValue) && ew_IsNumeric(Convert.ToString(AgentRiskCredit.EditValue))) {
					AgentRiskCredit.EditValue = ew_FormatNumber(AgentRiskCredit.EditValue, -2, -1, -2, 0);
					AgentRiskCredit.OldValue = AgentRiskCredit.EditValue;
					}

					// Address1
					Address1.EditAttrs["class"] = "form-control";
					Address1.EditValue = Address1.CurrentValue; // DN
					Address1.PlaceHolder = ew_RemoveHtml(Address1.FldCaption);

					// Address2
					Address2.EditAttrs["class"] = "form-control";
					Address2.EditValue = Address2.CurrentValue; // DN
					Address2.PlaceHolder = ew_RemoveHtml(Address2.FldCaption);

					// Address3
					Address3.EditAttrs["class"] = "form-control";
					Address3.EditValue = Address3.CurrentValue; // DN
					Address3.PlaceHolder = ew_RemoveHtml(Address3.FldCaption);

					// Country
					Country.EditAttrs["class"] = "form-control";
						if (ew_Empty(Country.CurrentValue)) {
							sFilterWrk = "0=1";
						} else {
							sFilterWrk = "[varCountryCode]" + ew_SearchString("=", Convert.ToString(Country.CurrentValue), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT [varCountryCode], [varCountryCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[tblStdCountry]";
						sWhereWrk = "";
						Country.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(Country, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [varCountry] ASC/*EndOrderBy*/";
					rswrk = ew_GetConn("").GetRows(sSqlWrk);
					Country.EditValue = rswrk;

					// ZipCode
					ZipCode.EditAttrs["class"] = "form-control";
					ZipCode.EditValue = ZipCode.CurrentValue; // DN
					ZipCode.PlaceHolder = ew_RemoveHtml(ZipCode.FldCaption);

					// Fax
					Fax.EditAttrs["class"] = "form-control";
					Fax.EditValue = Fax.CurrentValue; // DN
					Fax.PlaceHolder = ew_RemoveHtml(Fax.FldCaption);

					// Phone
					Phone.EditAttrs["class"] = "form-control";
					Phone.EditValue = Phone.CurrentValue; // DN
					Phone.PlaceHolder = ew_RemoveHtml(Phone.FldCaption);

					// Mobile
					Mobile.EditAttrs["class"] = "form-control";
					Mobile.EditValue = Mobile.CurrentValue; // DN
					Mobile.PlaceHolder = ew_RemoveHtml(Mobile.FldCaption);

					// BuzType
					BuzType.EditAttrs["class"] = "form-control";
					BuzType.EditValue = BuzType.CurrentValue; // DN
					BuzType.PlaceHolder = ew_RemoveHtml(BuzType.FldCaption);

					// ClassType
					ClassType.EditAttrs["class"] = "form-control";
					ClassType.EditValue = ClassType.CurrentValue; // DN
					ClassType.PlaceHolder = ew_RemoveHtml(ClassType.FldCaption);

					// DefContactPName
					DefContactPName.EditAttrs["class"] = "form-control";
					DefContactPName.EditValue = DefContactPName.CurrentValue; // DN
					DefContactPName.PlaceHolder = ew_RemoveHtml(DefContactPName.FldCaption);

					// DefContactPNric
					DefContactPNric.EditAttrs["class"] = "form-control";
					DefContactPNric.EditValue = DefContactPNric.CurrentValue; // DN
					DefContactPNric.PlaceHolder = ew_RemoveHtml(DefContactPNric.FldCaption);

					// DefContactPNation
					DefContactPNation.EditAttrs["class"] = "form-control";
					DefContactPNation.EditValue = DefContactPNation.CurrentValue; // DN
					DefContactPNation.PlaceHolder = ew_RemoveHtml(DefContactPNation.FldCaption);

					// DefContactPOccupation
					DefContactPOccupation.EditAttrs["class"] = "form-control";
					DefContactPOccupation.EditValue = DefContactPOccupation.CurrentValue; // DN
					DefContactPOccupation.PlaceHolder = ew_RemoveHtml(DefContactPOccupation.FldCaption);

					// TermsId
					TermsId.EditAttrs["class"] = "form-control";
					TermsId.EditValue = TermsId.CurrentValue; // DN
					TermsId.PlaceHolder = ew_RemoveHtml(TermsId.FldCaption);

					// LedgerBal
					LedgerBal.EditAttrs["class"] = "form-control";
					LedgerBal.EditValue = LedgerBal.CurrentValue; // DN
					LedgerBal.PlaceHolder = ew_RemoveHtml(LedgerBal.FldCaption);
					if (ew_NotEmpty(LedgerBal.EditValue) && ew_IsNumeric(Convert.ToString(LedgerBal.EditValue))) {
					LedgerBal.EditValue = ew_FormatNumber(LedgerBal.EditValue, -2, -1, -2, 0);
					LedgerBal.OldValue = LedgerBal.EditValue;
					}

					// AvailableBal
					AvailableBal.EditAttrs["class"] = "form-control";
					AvailableBal.EditValue = AvailableBal.CurrentValue; // DN
					AvailableBal.PlaceHolder = ew_RemoveHtml(AvailableBal.FldCaption);
					if (ew_NotEmpty(AvailableBal.EditValue) && ew_IsNumeric(Convert.ToString(AvailableBal.EditValue))) {
					AvailableBal.EditValue = ew_FormatNumber(AvailableBal.EditValue, -2, -1, -2, 0);
					AvailableBal.OldValue = AvailableBal.EditValue;
					}

					// _Email
					_Email.EditAttrs["class"] = "form-control";
					_Email.EditValue = _Email.CurrentValue; // DN
					_Email.PlaceHolder = ew_RemoveHtml(_Email.FldCaption);

					// URL
					URL.EditAttrs["class"] = "form-control";
					URL.EditValue = URL.CurrentValue; // DN
					URL.PlaceHolder = ew_RemoveHtml(URL.FldCaption);

					// CustType
					CustType.EditAttrs["class"] = "form-control";
					CustType.EditValue = CustType.CurrentValue; // DN
					CustType.PlaceHolder = ew_RemoveHtml(CustType.FldCaption);

					// RemittanceLicNO
					RemittanceLicNO.EditAttrs["class"] = "form-control";
					RemittanceLicNO.EditValue = RemittanceLicNO.CurrentValue; // DN
					RemittanceLicNO.PlaceHolder = ew_RemoveHtml(RemittanceLicNO.FldCaption);

					// MCLicNo
					MCLicNo.EditAttrs["class"] = "form-control";
					MCLicNo.EditValue = MCLicNo.CurrentValue; // DN
					MCLicNo.PlaceHolder = ew_RemoveHtml(MCLicNo.FldCaption);

					// BankYesNo
					BankYesNo.EditValue = BankYesNo.Options(false);

					// BankODLimit
					BankODLimit.EditAttrs["class"] = "form-control";
					BankODLimit.EditValue = BankODLimit.CurrentValue; // DN
					BankODLimit.PlaceHolder = ew_RemoveHtml(BankODLimit.FldCaption);
					if (ew_NotEmpty(BankODLimit.EditValue) && ew_IsNumeric(Convert.ToString(BankODLimit.EditValue))) {
					BankODLimit.EditValue = ew_FormatNumber(BankODLimit.EditValue, -2, -1, -2, 0);
					BankODLimit.OldValue = BankODLimit.EditValue;
					}

					// BankAcctNO
					BankAcctNO.EditAttrs["class"] = "form-control";
					BankAcctNO.EditValue = BankAcctNO.CurrentValue; // DN
					BankAcctNO.PlaceHolder = ew_RemoveHtml(BankAcctNO.FldCaption);

					// CreditLimit
					CreditLimit.EditAttrs["class"] = "form-control";
					CreditLimit.EditValue = CreditLimit.CurrentValue; // DN
					CreditLimit.PlaceHolder = ew_RemoveHtml(CreditLimit.FldCaption);
					if (ew_NotEmpty(CreditLimit.EditValue) && ew_IsNumeric(Convert.ToString(CreditLimit.EditValue))) {
					CreditLimit.EditValue = ew_FormatNumber(CreditLimit.EditValue, -2, -1, -2, 0);
					CreditLimit.OldValue = CreditLimit.EditValue;
					}

					// ReferBy
					ReferBy.EditAttrs["class"] = "form-control";
					ReferBy.EditValue = ReferBy.CurrentValue; // DN
					ReferBy.PlaceHolder = ew_RemoveHtml(ReferBy.FldCaption);

					// AgentImageName
					AgentImageName.EditAttrs["class"] = "form-control";
					AgentImageName.EditValue = AgentImageName.CurrentValue; // DN
					AgentImageName.PlaceHolder = ew_RemoveHtml(AgentImageName.FldCaption);

					// status
					status.EditAttrs["class"] = "form-control";
					status.EditValue = status.CurrentValue; // DN
					status.PlaceHolder = ew_RemoveHtml(status.FldCaption);

					// CreatedBy
					CreatedBy.EditAttrs["class"] = "form-control";
					CreatedBy.EditValue = CreatedBy.CurrentValue; // DN
					CreatedBy.PlaceHolder = ew_RemoveHtml(CreatedBy.FldCaption);

					// CreatedDate
					CreatedDate.EditAttrs["class"] = "form-control";
					CreatedDate.EditValue = ew_FormatDateTime(CreatedDate.CurrentValue, 8); // DN
					CreatedDate.PlaceHolder = ew_RemoveHtml(CreatedDate.FldCaption);

					// ModifiedUser
					ModifiedUser.EditAttrs["class"] = "form-control";
					ModifiedUser.EditValue = ModifiedUser.CurrentValue; // DN
					ModifiedUser.PlaceHolder = ew_RemoveHtml(ModifiedUser.FldCaption);

					// ModifiedDate
					ModifiedDate.EditAttrs["class"] = "form-control";
					ModifiedDate.EditValue = ew_FormatDateTime(ModifiedDate.CurrentValue, 8); // DN
					ModifiedDate.PlaceHolder = ew_RemoveHtml(ModifiedDate.FldCaption);

					// PPExpiryDate
					PPExpiryDate.EditAttrs["class"] = "form-control";
					PPExpiryDate.EditValue = ew_FormatDateTime(PPExpiryDate.CurrentValue, 8); // DN
					PPExpiryDate.PlaceHolder = ew_RemoveHtml(PPExpiryDate.FldCaption);

					// TTExpiryDate
					TTExpiryDate.EditAttrs["class"] = "form-control";
					TTExpiryDate.EditValue = ew_FormatDateTime(TTExpiryDate.CurrentValue, 8); // DN
					TTExpiryDate.PlaceHolder = ew_RemoveHtml(TTExpiryDate.FldCaption);

					// MCExpiryDate
					MCExpiryDate.EditAttrs["class"] = "form-control";
					MCExpiryDate.EditValue = ew_FormatDateTime(MCExpiryDate.CurrentValue, 8); // DN
					MCExpiryDate.PlaceHolder = ew_RemoveHtml(MCExpiryDate.FldCaption);

					// Action
					Action.EditAttrs["class"] = "form-control";
					Action.EditValue = Action.CurrentValue; // DN
					Action.PlaceHolder = ew_RemoveHtml(Action.FldCaption);

					// Remark
					Remark.EditAttrs["class"] = "form-control";
					Remark.EditValue = Remark.CurrentValue; // DN
					Remark.PlaceHolder = ew_RemoveHtml(Remark.FldCaption);

					// MCType
					MCType.EditAttrs["class"] = "form-control";
					MCType.EditValue = MCType.CurrentValue; // DN
					MCType.PlaceHolder = ew_RemoveHtml(MCType.FldCaption);

					// CustDOB
					CustDOB.EditAttrs["class"] = "form-control";
					CustDOB.EditValue = ew_FormatDateTime(CustDOB.CurrentValue, 8); // DN
					CustDOB.PlaceHolder = ew_RemoveHtml(CustDOB.FldCaption);

					// DefContactDOB
					DefContactDOB.EditAttrs["class"] = "form-control";
					DefContactDOB.EditValue = ew_FormatDateTime(DefContactDOB.CurrentValue, 8); // DN
					DefContactDOB.PlaceHolder = ew_RemoveHtml(DefContactDOB.FldCaption);

					// ScanImage
					ScanImage.EditAttrs["class"] = "form-control";
					ScanImage.EditValue = ScanImage.CurrentValue; // DN
					ScanImage.PlaceHolder = ew_RemoveHtml(ScanImage.FldCaption);

					// BizNature
					BizNature.EditAttrs["class"] = "form-control";
					BizNature.EditValue = BizNature.CurrentValue; // DN
					BizNature.PlaceHolder = ew_RemoveHtml(BizNature.FldCaption);

					// DefContactPOB
					DefContactPOB.EditAttrs["class"] = "form-control";
					DefContactPOB.EditValue = DefContactPOB.CurrentValue; // DN
					DefContactPOB.PlaceHolder = ew_RemoveHtml(DefContactPOB.FldCaption);

					// NewTran
					NewTran.EditAttrs["class"] = "form-control";
					NewTran.EditValue = NewTran.CurrentValue; // DN
					NewTran.PlaceHolder = ew_RemoveHtml(NewTran.FldCaption);

					// BizRegNo
					BizRegNo.EditAttrs["class"] = "form-control";
					BizRegNo.EditValue = BizRegNo.CurrentValue; // DN
					BizRegNo.PlaceHolder = ew_RemoveHtml(BizRegNo.FldCaption);

					// BizRegDate
					BizRegDate.EditAttrs["class"] = "form-control";
					BizRegDate.EditValue = ew_FormatDateTime(BizRegDate.CurrentValue, 8); // DN
					BizRegDate.PlaceHolder = ew_RemoveHtml(BizRegDate.FldCaption);

					// BizRegPlace
					BizRegPlace.EditAttrs["class"] = "form-control";
					BizRegPlace.EditValue = BizRegPlace.CurrentValue; // DN
					BizRegPlace.PlaceHolder = ew_RemoveHtml(BizRegPlace.FldCaption);

					// BizRegExpDate
					BizRegExpDate.EditAttrs["class"] = "form-control";
					BizRegExpDate.EditValue = ew_FormatDateTime(BizRegExpDate.CurrentValue, 8); // DN
					BizRegExpDate.PlaceHolder = ew_RemoveHtml(BizRegExpDate.FldCaption);

					// UnIncorpExec
					UnIncorpExec.EditAttrs["class"] = "form-control";
					UnIncorpExec.EditValue = UnIncorpExec.CurrentValue; // DN
					UnIncorpExec.PlaceHolder = ew_RemoveHtml(UnIncorpExec.FldCaption);

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.EditAttrs["class"] = "form-control";
					DefContactAuthorzLetter.EditValue = DefContactAuthorzLetter.CurrentValue; // DN
					DefContactAuthorzLetter.PlaceHolder = ew_RemoveHtml(DefContactAuthorzLetter.FldCaption);

					// Politician
					Politician.EditAttrs["class"] = "form-control";
					Politician.EditValue = Politician.CurrentValue; // DN
					Politician.PlaceHolder = ew_RemoveHtml(Politician.FldCaption);

					// BizPartnerNo
					BizPartnerNo.EditAttrs["class"] = "form-control";
					BizPartnerNo.EditValue = BizPartnerNo.CurrentValue; // DN
					BizPartnerNo.PlaceHolder = ew_RemoveHtml(BizPartnerNo.FldCaption);

					// Remark2
					Remark2.EditAttrs["class"] = "form-control";
					Remark2.EditValue = Remark2.CurrentValue; // DN
					Remark2.PlaceHolder = ew_RemoveHtml(Remark2.FldCaption);

					// BannedListRemark
					BannedListRemark.EditAttrs["class"] = "form-control";
					BannedListRemark.EditValue = BannedListRemark.CurrentValue; // DN
					BannedListRemark.PlaceHolder = ew_RemoveHtml(BannedListRemark.FldCaption);

					// Edit refer script
					// AgentId

					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";

					// AgentName
					AgentName.LinkCustomAttributes = AgentName.FldTagACustomAttributes; // DN
					AgentName.HrefValue = "";

					// AgentRiskRating
					AgentRiskRating.LinkCustomAttributes = AgentRiskRating.FldTagACustomAttributes; // DN
					AgentRiskRating.HrefValue = "";

					// AgentRiskCredit
					AgentRiskCredit.LinkCustomAttributes = AgentRiskCredit.FldTagACustomAttributes; // DN
					AgentRiskCredit.HrefValue = "";

					// Address1
					Address1.LinkCustomAttributes = Address1.FldTagACustomAttributes; // DN
					Address1.HrefValue = "";

					// Address2
					Address2.LinkCustomAttributes = Address2.FldTagACustomAttributes; // DN
					Address2.HrefValue = "";

					// Address3
					Address3.LinkCustomAttributes = Address3.FldTagACustomAttributes; // DN
					Address3.HrefValue = "";

					// Country
					Country.LinkCustomAttributes = Country.FldTagACustomAttributes; // DN
					Country.HrefValue = "";

					// ZipCode
					ZipCode.LinkCustomAttributes = ZipCode.FldTagACustomAttributes; // DN
					ZipCode.HrefValue = "";

					// Fax
					Fax.LinkCustomAttributes = Fax.FldTagACustomAttributes; // DN
					Fax.HrefValue = "";

					// Phone
					Phone.LinkCustomAttributes = Phone.FldTagACustomAttributes; // DN
					Phone.HrefValue = "";

					// Mobile
					Mobile.LinkCustomAttributes = Mobile.FldTagACustomAttributes; // DN
					Mobile.HrefValue = "";

					// BuzType
					BuzType.LinkCustomAttributes = BuzType.FldTagACustomAttributes; // DN
					BuzType.HrefValue = "";

					// ClassType
					ClassType.LinkCustomAttributes = ClassType.FldTagACustomAttributes; // DN
					ClassType.HrefValue = "";

					// DefContactPName
					DefContactPName.LinkCustomAttributes = DefContactPName.FldTagACustomAttributes; // DN
					DefContactPName.HrefValue = "";

					// DefContactPNric
					DefContactPNric.LinkCustomAttributes = DefContactPNric.FldTagACustomAttributes; // DN
					DefContactPNric.HrefValue = "";

					// DefContactPNation
					DefContactPNation.LinkCustomAttributes = DefContactPNation.FldTagACustomAttributes; // DN
					DefContactPNation.HrefValue = "";

					// DefContactPOccupation
					DefContactPOccupation.LinkCustomAttributes = DefContactPOccupation.FldTagACustomAttributes; // DN
					DefContactPOccupation.HrefValue = "";

					// TermsId
					TermsId.LinkCustomAttributes = TermsId.FldTagACustomAttributes; // DN
					TermsId.HrefValue = "";

					// LedgerBal
					LedgerBal.LinkCustomAttributes = LedgerBal.FldTagACustomAttributes; // DN
					LedgerBal.HrefValue = "";

					// AvailableBal
					AvailableBal.LinkCustomAttributes = AvailableBal.FldTagACustomAttributes; // DN
					AvailableBal.HrefValue = "";

					// _Email
					_Email.LinkCustomAttributes = _Email.FldTagACustomAttributes; // DN
					_Email.HrefValue = "";

					// URL
					URL.LinkCustomAttributes = URL.FldTagACustomAttributes; // DN
					URL.HrefValue = "";

					// CustType
					CustType.LinkCustomAttributes = CustType.FldTagACustomAttributes; // DN
					CustType.HrefValue = "";

					// RemittanceLicNO
					RemittanceLicNO.LinkCustomAttributes = RemittanceLicNO.FldTagACustomAttributes; // DN
					RemittanceLicNO.HrefValue = "";

					// MCLicNo
					MCLicNo.LinkCustomAttributes = MCLicNo.FldTagACustomAttributes; // DN
					MCLicNo.HrefValue = "";

					// BankYesNo
					BankYesNo.LinkCustomAttributes = BankYesNo.FldTagACustomAttributes; // DN
					BankYesNo.HrefValue = "";

					// BankODLimit
					BankODLimit.LinkCustomAttributes = BankODLimit.FldTagACustomAttributes; // DN
					BankODLimit.HrefValue = "";

					// BankAcctNO
					BankAcctNO.LinkCustomAttributes = BankAcctNO.FldTagACustomAttributes; // DN
					BankAcctNO.HrefValue = "";

					// CreditLimit
					CreditLimit.LinkCustomAttributes = CreditLimit.FldTagACustomAttributes; // DN
					CreditLimit.HrefValue = "";

					// ReferBy
					ReferBy.LinkCustomAttributes = ReferBy.FldTagACustomAttributes; // DN
					ReferBy.HrefValue = "";

					// AgentImageName
					AgentImageName.LinkCustomAttributes = AgentImageName.FldTagACustomAttributes; // DN
					AgentImageName.HrefValue = "";

					// status
					status.LinkCustomAttributes = status.FldTagACustomAttributes; // DN
					status.HrefValue = "";

					// CreatedBy
					CreatedBy.LinkCustomAttributes = CreatedBy.FldTagACustomAttributes; // DN
					CreatedBy.HrefValue = "";

					// CreatedDate
					CreatedDate.LinkCustomAttributes = CreatedDate.FldTagACustomAttributes; // DN
					CreatedDate.HrefValue = "";

					// ModifiedUser
					ModifiedUser.LinkCustomAttributes = ModifiedUser.FldTagACustomAttributes; // DN
					ModifiedUser.HrefValue = "";

					// ModifiedDate
					ModifiedDate.LinkCustomAttributes = ModifiedDate.FldTagACustomAttributes; // DN
					ModifiedDate.HrefValue = "";

					// PPExpiryDate
					PPExpiryDate.LinkCustomAttributes = PPExpiryDate.FldTagACustomAttributes; // DN
					PPExpiryDate.HrefValue = "";

					// TTExpiryDate
					TTExpiryDate.LinkCustomAttributes = TTExpiryDate.FldTagACustomAttributes; // DN
					TTExpiryDate.HrefValue = "";

					// MCExpiryDate
					MCExpiryDate.LinkCustomAttributes = MCExpiryDate.FldTagACustomAttributes; // DN
					MCExpiryDate.HrefValue = "";

					// Action
					Action.LinkCustomAttributes = Action.FldTagACustomAttributes; // DN
					Action.HrefValue = "";

					// Remark
					Remark.LinkCustomAttributes = Remark.FldTagACustomAttributes; // DN
					Remark.HrefValue = "";

					// MCType
					MCType.LinkCustomAttributes = MCType.FldTagACustomAttributes; // DN
					MCType.HrefValue = "";

					// CustDOB
					CustDOB.LinkCustomAttributes = CustDOB.FldTagACustomAttributes; // DN
					CustDOB.HrefValue = "";

					// DefContactDOB
					DefContactDOB.LinkCustomAttributes = DefContactDOB.FldTagACustomAttributes; // DN
					DefContactDOB.HrefValue = "";

					// ScanImage
					ScanImage.LinkCustomAttributes = ScanImage.FldTagACustomAttributes; // DN
					ScanImage.HrefValue = "";

					// BizNature
					BizNature.LinkCustomAttributes = BizNature.FldTagACustomAttributes; // DN
					BizNature.HrefValue = "";

					// DefContactPOB
					DefContactPOB.LinkCustomAttributes = DefContactPOB.FldTagACustomAttributes; // DN
					DefContactPOB.HrefValue = "";

					// NewTran
					NewTran.LinkCustomAttributes = NewTran.FldTagACustomAttributes; // DN
					NewTran.HrefValue = "";

					// BizRegNo
					BizRegNo.LinkCustomAttributes = BizRegNo.FldTagACustomAttributes; // DN
					BizRegNo.HrefValue = "";

					// BizRegDate
					BizRegDate.LinkCustomAttributes = BizRegDate.FldTagACustomAttributes; // DN
					BizRegDate.HrefValue = "";

					// BizRegPlace
					BizRegPlace.LinkCustomAttributes = BizRegPlace.FldTagACustomAttributes; // DN
					BizRegPlace.HrefValue = "";

					// BizRegExpDate
					BizRegExpDate.LinkCustomAttributes = BizRegExpDate.FldTagACustomAttributes; // DN
					BizRegExpDate.HrefValue = "";

					// UnIncorpExec
					UnIncorpExec.LinkCustomAttributes = UnIncorpExec.FldTagACustomAttributes; // DN
					UnIncorpExec.HrefValue = "";

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.LinkCustomAttributes = DefContactAuthorzLetter.FldTagACustomAttributes; // DN
					DefContactAuthorzLetter.HrefValue = "";

					// Politician
					Politician.LinkCustomAttributes = Politician.FldTagACustomAttributes; // DN
					Politician.HrefValue = "";

					// BizPartnerNo
					BizPartnerNo.LinkCustomAttributes = BizPartnerNo.FldTagACustomAttributes; // DN
					BizPartnerNo.HrefValue = "";

					// Remark2
					Remark2.LinkCustomAttributes = Remark2.FldTagACustomAttributes; // DN
					Remark2.HrefValue = "";

					// BannedListRemark
					BannedListRemark.LinkCustomAttributes = BannedListRemark.FldTagACustomAttributes; // DN
					BannedListRemark.HrefValue = "";
				} else if (RowType == EW_ROWTYPE_SEARCH) { // Search row

					// AgentId
					AgentId.EditAttrs["class"] = "form-control";
					AgentId.EditValue = AgentId.AdvancedSearch.SearchValue; // DN
					AgentId.PlaceHolder = ew_RemoveHtml(AgentId.FldCaption);

					// AgentName
					AgentName.EditAttrs["class"] = "form-control";
					AgentName.EditValue = AgentName.AdvancedSearch.SearchValue; // DN
					AgentName.PlaceHolder = ew_RemoveHtml(AgentName.FldCaption);

					// AgentRiskRating
					AgentRiskRating.EditAttrs["class"] = "form-control";
					AgentRiskRating.EditValue = AgentRiskRating.Options(true);

					// AgentRiskCredit
					AgentRiskCredit.EditAttrs["class"] = "form-control";
					AgentRiskCredit.EditValue = AgentRiskCredit.AdvancedSearch.SearchValue; // DN
					AgentRiskCredit.PlaceHolder = ew_RemoveHtml(AgentRiskCredit.FldCaption);
					if (ew_NotEmpty(AgentRiskCredit.EditValue) && ew_IsNumeric(Convert.ToString(AgentRiskCredit.EditValue))) {
					AgentRiskCredit.EditValue = ew_FormatNumber(AgentRiskCredit.EditValue, -2, -1, -2, 0);
					AgentRiskCredit.OldValue = AgentRiskCredit.EditValue;
					}

					// Address1
					Address1.EditAttrs["class"] = "form-control";
					Address1.EditValue = Address1.AdvancedSearch.SearchValue; // DN
					Address1.PlaceHolder = ew_RemoveHtml(Address1.FldCaption);

					// Address2
					Address2.EditAttrs["class"] = "form-control";
					Address2.EditValue = Address2.AdvancedSearch.SearchValue; // DN
					Address2.PlaceHolder = ew_RemoveHtml(Address2.FldCaption);

					// Address3
					Address3.EditAttrs["class"] = "form-control";
					Address3.EditValue = Address3.AdvancedSearch.SearchValue; // DN
					Address3.PlaceHolder = ew_RemoveHtml(Address3.FldCaption);

					// Country
					Country.EditAttrs["class"] = "form-control";
						if (ew_Empty(Country.AdvancedSearch.SearchValue)) {
							sFilterWrk = "0=1";
						} else {
							sFilterWrk = "[varCountryCode]" + ew_SearchString("=", Convert.ToString(Country.AdvancedSearch.SearchValue), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT [varCountryCode], [varCountryCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[tblStdCountry]";
						sWhereWrk = "";
						Country.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(Country, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [varCountry] ASC/*EndOrderBy*/";
					rswrk = ew_GetConn("").GetRows(sSqlWrk);
					Country.EditValue = rswrk;

					// ZipCode
					ZipCode.EditAttrs["class"] = "form-control";
					ZipCode.EditValue = ZipCode.AdvancedSearch.SearchValue; // DN
					ZipCode.PlaceHolder = ew_RemoveHtml(ZipCode.FldCaption);

					// Fax
					Fax.EditAttrs["class"] = "form-control";
					Fax.EditValue = Fax.AdvancedSearch.SearchValue; // DN
					Fax.PlaceHolder = ew_RemoveHtml(Fax.FldCaption);

					// Phone
					Phone.EditAttrs["class"] = "form-control";
					Phone.EditValue = Phone.AdvancedSearch.SearchValue; // DN
					Phone.PlaceHolder = ew_RemoveHtml(Phone.FldCaption);

					// Mobile
					Mobile.EditAttrs["class"] = "form-control";
					Mobile.EditValue = Mobile.AdvancedSearch.SearchValue; // DN
					Mobile.PlaceHolder = ew_RemoveHtml(Mobile.FldCaption);

					// BuzType
					BuzType.EditAttrs["class"] = "form-control";
					BuzType.EditValue = BuzType.AdvancedSearch.SearchValue; // DN
					BuzType.PlaceHolder = ew_RemoveHtml(BuzType.FldCaption);

					// ClassType
					ClassType.EditAttrs["class"] = "form-control";
					ClassType.EditValue = ClassType.AdvancedSearch.SearchValue; // DN
					ClassType.PlaceHolder = ew_RemoveHtml(ClassType.FldCaption);

					// DefContactPName
					DefContactPName.EditAttrs["class"] = "form-control";
					DefContactPName.EditValue = DefContactPName.AdvancedSearch.SearchValue; // DN
					DefContactPName.PlaceHolder = ew_RemoveHtml(DefContactPName.FldCaption);

					// DefContactPNric
					DefContactPNric.EditAttrs["class"] = "form-control";
					DefContactPNric.EditValue = DefContactPNric.AdvancedSearch.SearchValue; // DN
					DefContactPNric.PlaceHolder = ew_RemoveHtml(DefContactPNric.FldCaption);

					// DefContactPNation
					DefContactPNation.EditAttrs["class"] = "form-control";
					DefContactPNation.EditValue = DefContactPNation.AdvancedSearch.SearchValue; // DN
					DefContactPNation.PlaceHolder = ew_RemoveHtml(DefContactPNation.FldCaption);

					// DefContactPOccupation
					DefContactPOccupation.EditAttrs["class"] = "form-control";
					DefContactPOccupation.EditValue = DefContactPOccupation.AdvancedSearch.SearchValue; // DN
					DefContactPOccupation.PlaceHolder = ew_RemoveHtml(DefContactPOccupation.FldCaption);

					// TermsId
					TermsId.EditAttrs["class"] = "form-control";
					TermsId.EditValue = TermsId.AdvancedSearch.SearchValue; // DN
					TermsId.PlaceHolder = ew_RemoveHtml(TermsId.FldCaption);

					// LedgerBal
					LedgerBal.EditAttrs["class"] = "form-control";
					LedgerBal.EditValue = LedgerBal.AdvancedSearch.SearchValue; // DN
					LedgerBal.PlaceHolder = ew_RemoveHtml(LedgerBal.FldCaption);
					if (ew_NotEmpty(LedgerBal.EditValue) && ew_IsNumeric(Convert.ToString(LedgerBal.EditValue))) {
					LedgerBal.EditValue = ew_FormatNumber(LedgerBal.EditValue, -2, -1, -2, 0);
					LedgerBal.OldValue = LedgerBal.EditValue;
					}

					// AvailableBal
					AvailableBal.EditAttrs["class"] = "form-control";
					AvailableBal.EditValue = AvailableBal.AdvancedSearch.SearchValue; // DN
					AvailableBal.PlaceHolder = ew_RemoveHtml(AvailableBal.FldCaption);
					if (ew_NotEmpty(AvailableBal.EditValue) && ew_IsNumeric(Convert.ToString(AvailableBal.EditValue))) {
					AvailableBal.EditValue = ew_FormatNumber(AvailableBal.EditValue, -2, -1, -2, 0);
					AvailableBal.OldValue = AvailableBal.EditValue;
					}

					// _Email
					_Email.EditAttrs["class"] = "form-control";
					_Email.EditValue = _Email.AdvancedSearch.SearchValue; // DN
					_Email.PlaceHolder = ew_RemoveHtml(_Email.FldCaption);

					// URL
					URL.EditAttrs["class"] = "form-control";
					URL.EditValue = URL.AdvancedSearch.SearchValue; // DN
					URL.PlaceHolder = ew_RemoveHtml(URL.FldCaption);

					// CustType
					CustType.EditAttrs["class"] = "form-control";
					CustType.EditValue = CustType.AdvancedSearch.SearchValue; // DN
					CustType.PlaceHolder = ew_RemoveHtml(CustType.FldCaption);

					// RemittanceLicNO
					RemittanceLicNO.EditAttrs["class"] = "form-control";
					RemittanceLicNO.EditValue = RemittanceLicNO.AdvancedSearch.SearchValue; // DN
					RemittanceLicNO.PlaceHolder = ew_RemoveHtml(RemittanceLicNO.FldCaption);

					// MCLicNo
					MCLicNo.EditAttrs["class"] = "form-control";
					MCLicNo.EditValue = MCLicNo.AdvancedSearch.SearchValue; // DN
					MCLicNo.PlaceHolder = ew_RemoveHtml(MCLicNo.FldCaption);

					// BankYesNo
					BankYesNo.EditValue = BankYesNo.Options(false);

					// BankODLimit
					BankODLimit.EditAttrs["class"] = "form-control";
					BankODLimit.EditValue = BankODLimit.AdvancedSearch.SearchValue; // DN
					BankODLimit.PlaceHolder = ew_RemoveHtml(BankODLimit.FldCaption);
					if (ew_NotEmpty(BankODLimit.EditValue) && ew_IsNumeric(Convert.ToString(BankODLimit.EditValue))) {
					BankODLimit.EditValue = ew_FormatNumber(BankODLimit.EditValue, -2, -1, -2, 0);
					BankODLimit.OldValue = BankODLimit.EditValue;
					}

					// BankAcctNO
					BankAcctNO.EditAttrs["class"] = "form-control";
					BankAcctNO.EditValue = BankAcctNO.AdvancedSearch.SearchValue; // DN
					BankAcctNO.PlaceHolder = ew_RemoveHtml(BankAcctNO.FldCaption);

					// CreditLimit
					CreditLimit.EditAttrs["class"] = "form-control";
					CreditLimit.EditValue = CreditLimit.AdvancedSearch.SearchValue; // DN
					CreditLimit.PlaceHolder = ew_RemoveHtml(CreditLimit.FldCaption);
					if (ew_NotEmpty(CreditLimit.EditValue) && ew_IsNumeric(Convert.ToString(CreditLimit.EditValue))) {
					CreditLimit.EditValue = ew_FormatNumber(CreditLimit.EditValue, -2, -1, -2, 0);
					CreditLimit.OldValue = CreditLimit.EditValue;
					}

					// ReferBy
					ReferBy.EditAttrs["class"] = "form-control";
					ReferBy.EditValue = ReferBy.AdvancedSearch.SearchValue; // DN
					ReferBy.PlaceHolder = ew_RemoveHtml(ReferBy.FldCaption);

					// AgentImageName
					AgentImageName.EditAttrs["class"] = "form-control";
					AgentImageName.EditValue = AgentImageName.AdvancedSearch.SearchValue; // DN
					AgentImageName.PlaceHolder = ew_RemoveHtml(AgentImageName.FldCaption);

					// status
					status.EditAttrs["class"] = "form-control";
					status.EditValue = status.AdvancedSearch.SearchValue; // DN
					status.PlaceHolder = ew_RemoveHtml(status.FldCaption);

					// CreatedBy
					CreatedBy.EditAttrs["class"] = "form-control";
					CreatedBy.EditValue = CreatedBy.AdvancedSearch.SearchValue; // DN
					CreatedBy.PlaceHolder = ew_RemoveHtml(CreatedBy.FldCaption);

					// CreatedDate
					CreatedDate.EditAttrs["class"] = "form-control";
					CreatedDate.EditValue = ew_FormatDateTime(ew_UnformatDateTime(CreatedDate.AdvancedSearch.SearchValue, 0), 8); // DN
					CreatedDate.PlaceHolder = ew_RemoveHtml(CreatedDate.FldCaption);

					// ModifiedUser
					ModifiedUser.EditAttrs["class"] = "form-control";
					ModifiedUser.EditValue = ModifiedUser.AdvancedSearch.SearchValue; // DN
					ModifiedUser.PlaceHolder = ew_RemoveHtml(ModifiedUser.FldCaption);

					// ModifiedDate
					ModifiedDate.EditAttrs["class"] = "form-control";
					ModifiedDate.EditValue = ew_FormatDateTime(ew_UnformatDateTime(ModifiedDate.AdvancedSearch.SearchValue, 0), 8); // DN
					ModifiedDate.PlaceHolder = ew_RemoveHtml(ModifiedDate.FldCaption);

					// PPExpiryDate
					PPExpiryDate.EditAttrs["class"] = "form-control";
					PPExpiryDate.EditValue = ew_FormatDateTime(ew_UnformatDateTime(PPExpiryDate.AdvancedSearch.SearchValue, 0), 8); // DN
					PPExpiryDate.PlaceHolder = ew_RemoveHtml(PPExpiryDate.FldCaption);

					// TTExpiryDate
					TTExpiryDate.EditAttrs["class"] = "form-control";
					TTExpiryDate.EditValue = ew_FormatDateTime(ew_UnformatDateTime(TTExpiryDate.AdvancedSearch.SearchValue, 0), 8); // DN
					TTExpiryDate.PlaceHolder = ew_RemoveHtml(TTExpiryDate.FldCaption);

					// MCExpiryDate
					MCExpiryDate.EditAttrs["class"] = "form-control";
					MCExpiryDate.EditValue = ew_FormatDateTime(ew_UnformatDateTime(MCExpiryDate.AdvancedSearch.SearchValue, 0), 8); // DN
					MCExpiryDate.PlaceHolder = ew_RemoveHtml(MCExpiryDate.FldCaption);

					// Action
					Action.EditAttrs["class"] = "form-control";
					Action.EditValue = Action.AdvancedSearch.SearchValue; // DN
					Action.PlaceHolder = ew_RemoveHtml(Action.FldCaption);

					// Remark
					Remark.EditAttrs["class"] = "form-control";
					Remark.EditValue = Remark.AdvancedSearch.SearchValue; // DN
					Remark.PlaceHolder = ew_RemoveHtml(Remark.FldCaption);

					// MCType
					MCType.EditAttrs["class"] = "form-control";
					MCType.EditValue = MCType.AdvancedSearch.SearchValue; // DN
					MCType.PlaceHolder = ew_RemoveHtml(MCType.FldCaption);

					// CustDOB
					CustDOB.EditAttrs["class"] = "form-control";
					CustDOB.EditValue = ew_FormatDateTime(ew_UnformatDateTime(CustDOB.AdvancedSearch.SearchValue, 0), 8); // DN
					CustDOB.PlaceHolder = ew_RemoveHtml(CustDOB.FldCaption);

					// DefContactDOB
					DefContactDOB.EditAttrs["class"] = "form-control";
					DefContactDOB.EditValue = ew_FormatDateTime(ew_UnformatDateTime(DefContactDOB.AdvancedSearch.SearchValue, 0), 8); // DN
					DefContactDOB.PlaceHolder = ew_RemoveHtml(DefContactDOB.FldCaption);

					// ScanImage
					ScanImage.EditAttrs["class"] = "form-control";
					ScanImage.EditValue = ScanImage.AdvancedSearch.SearchValue; // DN
					ScanImage.PlaceHolder = ew_RemoveHtml(ScanImage.FldCaption);

					// BizNature
					BizNature.EditAttrs["class"] = "form-control";
					BizNature.EditValue = BizNature.AdvancedSearch.SearchValue; // DN
					BizNature.PlaceHolder = ew_RemoveHtml(BizNature.FldCaption);

					// DefContactPOB
					DefContactPOB.EditAttrs["class"] = "form-control";
					DefContactPOB.EditValue = DefContactPOB.AdvancedSearch.SearchValue; // DN
					DefContactPOB.PlaceHolder = ew_RemoveHtml(DefContactPOB.FldCaption);

					// NewTran
					NewTran.EditAttrs["class"] = "form-control";
					NewTran.EditValue = NewTran.AdvancedSearch.SearchValue; // DN
					NewTran.PlaceHolder = ew_RemoveHtml(NewTran.FldCaption);

					// BizRegNo
					BizRegNo.EditAttrs["class"] = "form-control";
					BizRegNo.EditValue = BizRegNo.AdvancedSearch.SearchValue; // DN
					BizRegNo.PlaceHolder = ew_RemoveHtml(BizRegNo.FldCaption);

					// BizRegDate
					BizRegDate.EditAttrs["class"] = "form-control";
					BizRegDate.EditValue = ew_FormatDateTime(ew_UnformatDateTime(BizRegDate.AdvancedSearch.SearchValue, 0), 8); // DN
					BizRegDate.PlaceHolder = ew_RemoveHtml(BizRegDate.FldCaption);

					// BizRegPlace
					BizRegPlace.EditAttrs["class"] = "form-control";
					BizRegPlace.EditValue = BizRegPlace.AdvancedSearch.SearchValue; // DN
					BizRegPlace.PlaceHolder = ew_RemoveHtml(BizRegPlace.FldCaption);

					// BizRegExpDate
					BizRegExpDate.EditAttrs["class"] = "form-control";
					BizRegExpDate.EditValue = ew_FormatDateTime(ew_UnformatDateTime(BizRegExpDate.AdvancedSearch.SearchValue, 0), 8); // DN
					BizRegExpDate.PlaceHolder = ew_RemoveHtml(BizRegExpDate.FldCaption);

					// UnIncorpExec
					UnIncorpExec.EditAttrs["class"] = "form-control";
					UnIncorpExec.EditValue = UnIncorpExec.AdvancedSearch.SearchValue; // DN
					UnIncorpExec.PlaceHolder = ew_RemoveHtml(UnIncorpExec.FldCaption);

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.EditAttrs["class"] = "form-control";
					DefContactAuthorzLetter.EditValue = DefContactAuthorzLetter.AdvancedSearch.SearchValue; // DN
					DefContactAuthorzLetter.PlaceHolder = ew_RemoveHtml(DefContactAuthorzLetter.FldCaption);

					// Politician
					Politician.EditAttrs["class"] = "form-control";
					Politician.EditValue = Politician.AdvancedSearch.SearchValue; // DN
					Politician.PlaceHolder = ew_RemoveHtml(Politician.FldCaption);

					// BizPartnerNo
					BizPartnerNo.EditAttrs["class"] = "form-control";
					BizPartnerNo.EditValue = BizPartnerNo.AdvancedSearch.SearchValue; // DN
					BizPartnerNo.PlaceHolder = ew_RemoveHtml(BizPartnerNo.FldCaption);

					// Remark2
					Remark2.EditAttrs["class"] = "form-control";
					Remark2.EditValue = Remark2.AdvancedSearch.SearchValue; // DN
					Remark2.PlaceHolder = ew_RemoveHtml(Remark2.FldCaption);

					// BannedListRemark
					BannedListRemark.EditAttrs["class"] = "form-control";
					BannedListRemark.EditValue = BannedListRemark.AdvancedSearch.SearchValue; // DN
					BannedListRemark.PlaceHolder = ew_RemoveHtml(BannedListRemark.FldCaption);
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

			// Validate search
			public bool ValidateSearch() {

				// Initialize
				gsSearchError = "";

				// Check if validation required
				if (!EW_SERVER_VALIDATE)
					return true;
				if (!ew_CheckDateDef(Convert.ToString(CustDOB.AdvancedSearch.SearchValue)))
					gsSearchError = ew_AddMessage(gsSearchError, CustDOB.FldErrMsg);

				// Return validate result
				bool valid = ew_Empty(gsSearchError);

				// Call Form_CustomValidate event
				string sFormCustomError = "";
				valid = valid && Form_CustomValidate(ref sFormCustomError);
				gsSearchError = ew_AddMessage(gsSearchError, sFormCustomError);
				return valid;
			}

			// Validate form
			public bool ValidateForm() {

				// Initialize form error message
				gsFormError = "";

				// Check if validation required
				if (!EW_SERVER_VALIDATE)
					return (gsFormError == "");
				if (!AgentId.FldIsDetailKey && ew_Empty(AgentId.FormValue))
					gsFormError = ew_AddMessage(gsFormError, AgentId.ReqErrMsg.Replace("%s", AgentId.FldCaption));
				if (!AgentName.FldIsDetailKey && ew_Empty(AgentName.FormValue))
					gsFormError = ew_AddMessage(gsFormError, AgentName.ReqErrMsg.Replace("%s", AgentName.FldCaption));
				if (!ew_CheckNumber(AgentRiskCredit.FormValue))
					gsFormError = ew_AddMessage(gsFormError, AgentRiskCredit.FldErrMsg);
				if (!Address1.FldIsDetailKey && ew_Empty(Address1.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Address1.ReqErrMsg.Replace("%s", Address1.FldCaption));
				if (!Country.FldIsDetailKey && ew_Empty(Country.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Country.ReqErrMsg.Replace("%s", Country.FldCaption));
				if (!ZipCode.FldIsDetailKey && ew_Empty(ZipCode.FormValue))
					gsFormError = ew_AddMessage(gsFormError, ZipCode.ReqErrMsg.Replace("%s", ZipCode.FldCaption));
				if (!Fax.FldIsDetailKey && ew_Empty(Fax.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Fax.ReqErrMsg.Replace("%s", Fax.FldCaption));
				if (!Phone.FldIsDetailKey && ew_Empty(Phone.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Phone.ReqErrMsg.Replace("%s", Phone.FldCaption));
				if (!Mobile.FldIsDetailKey && ew_Empty(Mobile.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Mobile.ReqErrMsg.Replace("%s", Mobile.FldCaption));
				if (!BuzType.FldIsDetailKey && ew_Empty(BuzType.FormValue))
					gsFormError = ew_AddMessage(gsFormError, BuzType.ReqErrMsg.Replace("%s", BuzType.FldCaption));
				if (!ClassType.FldIsDetailKey && ew_Empty(ClassType.FormValue))
					gsFormError = ew_AddMessage(gsFormError, ClassType.ReqErrMsg.Replace("%s", ClassType.FldCaption));
				if (!DefContactPName.FldIsDetailKey && ew_Empty(DefContactPName.FormValue))
					gsFormError = ew_AddMessage(gsFormError, DefContactPName.ReqErrMsg.Replace("%s", DefContactPName.FldCaption));
				if (!DefContactPNric.FldIsDetailKey && ew_Empty(DefContactPNric.FormValue))
					gsFormError = ew_AddMessage(gsFormError, DefContactPNric.ReqErrMsg.Replace("%s", DefContactPNric.FldCaption));
				if (!ew_CheckNumber(LedgerBal.FormValue))
					gsFormError = ew_AddMessage(gsFormError, LedgerBal.FldErrMsg);
				if (!ew_CheckNumber(AvailableBal.FormValue))
					gsFormError = ew_AddMessage(gsFormError, AvailableBal.FldErrMsg);
				if (!RemittanceLicNO.FldIsDetailKey && ew_Empty(RemittanceLicNO.FormValue))
					gsFormError = ew_AddMessage(gsFormError, RemittanceLicNO.ReqErrMsg.Replace("%s", RemittanceLicNO.FldCaption));
				if (!MCLicNo.FldIsDetailKey && ew_Empty(MCLicNo.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCLicNo.ReqErrMsg.Replace("%s", MCLicNo.FldCaption));
				if (!ew_CheckNumber(BankODLimit.FormValue))
					gsFormError = ew_AddMessage(gsFormError, BankODLimit.FldErrMsg);
				if (!ew_CheckNumber(CreditLimit.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreditLimit.FldErrMsg);
				if (!ReferBy.FldIsDetailKey && ew_Empty(ReferBy.FormValue))
					gsFormError = ew_AddMessage(gsFormError, ReferBy.ReqErrMsg.Replace("%s", ReferBy.FldCaption));
				if (!status.FldIsDetailKey && ew_Empty(status.FormValue))
					gsFormError = ew_AddMessage(gsFormError, status.ReqErrMsg.Replace("%s", status.FldCaption));
				if (!CreatedBy.FldIsDetailKey && ew_Empty(CreatedBy.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreatedBy.ReqErrMsg.Replace("%s", CreatedBy.FldCaption));
				if (!CreatedDate.FldIsDetailKey && ew_Empty(CreatedDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreatedDate.ReqErrMsg.Replace("%s", CreatedDate.FldCaption));
				if (!ew_CheckDateDef(CreatedDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CreatedDate.FldErrMsg);
				if (!ew_CheckDateDef(ModifiedDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, ModifiedDate.FldErrMsg);
				if (!ew_CheckDateDef(PPExpiryDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, PPExpiryDate.FldErrMsg);
				if (!TTExpiryDate.FldIsDetailKey && ew_Empty(TTExpiryDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, TTExpiryDate.ReqErrMsg.Replace("%s", TTExpiryDate.FldCaption));
				if (!ew_CheckDateDef(TTExpiryDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, TTExpiryDate.FldErrMsg);
				if (!MCExpiryDate.FldIsDetailKey && ew_Empty(MCExpiryDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCExpiryDate.ReqErrMsg.Replace("%s", MCExpiryDate.FldCaption));
				if (!ew_CheckDateDef(MCExpiryDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCExpiryDate.FldErrMsg);
				if (!ew_CheckDateDef(CustDOB.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CustDOB.FldErrMsg);
				if (!ew_CheckDateDef(DefContactDOB.FormValue))
					gsFormError = ew_AddMessage(gsFormError, DefContactDOB.FldErrMsg);
				if (!ew_CheckDateDef(BizRegDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, BizRegDate.FldErrMsg);
				if (!ew_CheckDateDef(BizRegExpDate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, BizRegExpDate.FldErrMsg);
				if (!UnIncorpExec.FldIsDetailKey && ew_Empty(UnIncorpExec.FormValue))
					gsFormError = ew_AddMessage(gsFormError, UnIncorpExec.ReqErrMsg.Replace("%s", UnIncorpExec.FldCaption));
				if (!ew_CheckInteger(UnIncorpExec.FormValue))
					gsFormError = ew_AddMessage(gsFormError, UnIncorpExec.FldErrMsg);
				if (!DefContactAuthorzLetter.FldIsDetailKey && ew_Empty(DefContactAuthorzLetter.FormValue))
					gsFormError = ew_AddMessage(gsFormError, DefContactAuthorzLetter.ReqErrMsg.Replace("%s", DefContactAuthorzLetter.FldCaption));
				if (!ew_CheckInteger(DefContactAuthorzLetter.FormValue))
					gsFormError = ew_AddMessage(gsFormError, DefContactAuthorzLetter.FldErrMsg);
				if (!Politician.FldIsDetailKey && ew_Empty(Politician.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Politician.ReqErrMsg.Replace("%s", Politician.FldCaption));
				if (!ew_CheckInteger(Politician.FormValue))
					gsFormError = ew_AddMessage(gsFormError, Politician.FldErrMsg);
				if (!BizPartnerNo.FldIsDetailKey && ew_Empty(BizPartnerNo.FormValue))
					gsFormError = ew_AddMessage(gsFormError, BizPartnerNo.ReqErrMsg.Replace("%s", BizPartnerNo.FldCaption));
				if (!ew_CheckInteger(BizPartnerNo.FormValue))
					gsFormError = ew_AddMessage(gsFormError, BizPartnerNo.FldErrMsg);

				// Return validate result
				bool valid = (ew_Empty(gsFormError));

				// Call Form_CustomValidate event
				string sFormCustomError = "";
				valid = valid && Form_CustomValidate(ref sFormCustomError);
				gsFormError = ew_AddMessage(gsFormError, sFormCustomError);
				return valid;
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
				Agent = Agent ?? new cAgent();
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
							sThisKey += Convert.ToString(row["AgentId"]);
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
				} else {
				}

				// Call Row Deleted event
				if (result) {
					foreach (OrderedDictionary row in rsold)
						Row_Deleted(row);
				}
				return result;
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

				// AgentId
				// AgentName

				AgentName.SetDbValue(ref rsnew, AgentName.CurrentValue, "", AgentName.ReadOnly);

				// AgentRiskRating
				AgentRiskRating.SetDbValue(ref rsnew, AgentRiskRating.CurrentValue, System.DBNull.Value, AgentRiskRating.ReadOnly);

				// AgentRiskCredit
				AgentRiskCredit.SetDbValue(ref rsnew, AgentRiskCredit.CurrentValue, System.DBNull.Value, AgentRiskCredit.ReadOnly);

				// Address1
				Address1.SetDbValue(ref rsnew, Address1.CurrentValue, "", Address1.ReadOnly);

				// Address2
				Address2.SetDbValue(ref rsnew, Address2.CurrentValue, System.DBNull.Value, Address2.ReadOnly);

				// Address3
				Address3.SetDbValue(ref rsnew, Address3.CurrentValue, System.DBNull.Value, Address3.ReadOnly);

				// Country
				Country.SetDbValue(ref rsnew, Country.CurrentValue, "", Country.ReadOnly);

				// ZipCode
				ZipCode.SetDbValue(ref rsnew, ZipCode.CurrentValue, "", ZipCode.ReadOnly);

				// Fax
				Fax.SetDbValue(ref rsnew, Fax.CurrentValue, "", Fax.ReadOnly);

				// Phone
				Phone.SetDbValue(ref rsnew, Phone.CurrentValue, "", Phone.ReadOnly);

				// Mobile
				Mobile.SetDbValue(ref rsnew, Mobile.CurrentValue, "", Mobile.ReadOnly);

				// BuzType
				BuzType.SetDbValue(ref rsnew, BuzType.CurrentValue, "", BuzType.ReadOnly);

				// ClassType
				ClassType.SetDbValue(ref rsnew, ClassType.CurrentValue, "", ClassType.ReadOnly);

				// DefContactPName
				DefContactPName.SetDbValue(ref rsnew, DefContactPName.CurrentValue, "", DefContactPName.ReadOnly);

				// DefContactPNric
				DefContactPNric.SetDbValue(ref rsnew, DefContactPNric.CurrentValue, "", DefContactPNric.ReadOnly);

				// DefContactPNation
				DefContactPNation.SetDbValue(ref rsnew, DefContactPNation.CurrentValue, System.DBNull.Value, DefContactPNation.ReadOnly);

				// DefContactPOccupation
				DefContactPOccupation.SetDbValue(ref rsnew, DefContactPOccupation.CurrentValue, System.DBNull.Value, DefContactPOccupation.ReadOnly);

				// TermsId
				TermsId.SetDbValue(ref rsnew, TermsId.CurrentValue, System.DBNull.Value, TermsId.ReadOnly);

				// LedgerBal
				LedgerBal.SetDbValue(ref rsnew, LedgerBal.CurrentValue, System.DBNull.Value, LedgerBal.ReadOnly);

				// AvailableBal
				AvailableBal.SetDbValue(ref rsnew, AvailableBal.CurrentValue, System.DBNull.Value, AvailableBal.ReadOnly);

				// _Email
				_Email.SetDbValue(ref rsnew, _Email.CurrentValue, System.DBNull.Value, _Email.ReadOnly);

				// URL
				URL.SetDbValue(ref rsnew, URL.CurrentValue, System.DBNull.Value, URL.ReadOnly);

				// CustType
				CustType.SetDbValue(ref rsnew, CustType.CurrentValue, System.DBNull.Value, CustType.ReadOnly);

				// RemittanceLicNO
				RemittanceLicNO.SetDbValue(ref rsnew, RemittanceLicNO.CurrentValue, "", RemittanceLicNO.ReadOnly);

				// MCLicNo
				MCLicNo.SetDbValue(ref rsnew, MCLicNo.CurrentValue, "", MCLicNo.ReadOnly);

				// BankYesNo
				BankYesNo.SetDbValue(ref rsnew, ew_ConvertToBool(BankYesNo.CurrentValue, "1", "0"), System.DBNull.Value, BankYesNo.ReadOnly); // DN1204

				// BankODLimit
				BankODLimit.SetDbValue(ref rsnew, BankODLimit.CurrentValue, System.DBNull.Value, BankODLimit.ReadOnly);

				// BankAcctNO
				BankAcctNO.SetDbValue(ref rsnew, BankAcctNO.CurrentValue, System.DBNull.Value, BankAcctNO.ReadOnly);

				// CreditLimit
				CreditLimit.SetDbValue(ref rsnew, CreditLimit.CurrentValue, System.DBNull.Value, CreditLimit.ReadOnly);

				// ReferBy
				ReferBy.SetDbValue(ref rsnew, ReferBy.CurrentValue, "", ReferBy.ReadOnly);

				// AgentImageName
				AgentImageName.SetDbValue(ref rsnew, AgentImageName.CurrentValue, System.DBNull.Value, AgentImageName.ReadOnly);

				// status
				status.SetDbValue(ref rsnew, status.CurrentValue, "", status.ReadOnly);

				// CreatedBy
				CreatedBy.SetDbValue(ref rsnew, CreatedBy.CurrentValue, "", CreatedBy.ReadOnly);

				// CreatedDate
				CreatedDate.SetDbValue(ref rsnew, ew_UnformatDateTime(CreatedDate.CurrentValue, 0), DateTime.Now, CreatedDate.ReadOnly);

				// ModifiedUser
				ModifiedUser.SetDbValue(ref rsnew, ModifiedUser.CurrentValue, System.DBNull.Value, ModifiedUser.ReadOnly);

				// ModifiedDate
				ModifiedDate.SetDbValue(ref rsnew, ew_UnformatDateTime(ModifiedDate.CurrentValue, 0), System.DBNull.Value, ModifiedDate.ReadOnly);

				// PPExpiryDate
				PPExpiryDate.SetDbValue(ref rsnew, ew_UnformatDateTime(PPExpiryDate.CurrentValue, 0), System.DBNull.Value, PPExpiryDate.ReadOnly);

				// TTExpiryDate
				TTExpiryDate.SetDbValue(ref rsnew, ew_UnformatDateTime(TTExpiryDate.CurrentValue, 0), DateTime.Now, TTExpiryDate.ReadOnly);

				// MCExpiryDate
				MCExpiryDate.SetDbValue(ref rsnew, ew_UnformatDateTime(MCExpiryDate.CurrentValue, 0), DateTime.Now, MCExpiryDate.ReadOnly);

				// Action
				Action.SetDbValue(ref rsnew, Action.CurrentValue, System.DBNull.Value, Action.ReadOnly);

				// Remark
				Remark.SetDbValue(ref rsnew, Remark.CurrentValue, System.DBNull.Value, Remark.ReadOnly);

				// MCType
				MCType.SetDbValue(ref rsnew, MCType.CurrentValue, System.DBNull.Value, MCType.ReadOnly);

				// CustDOB
				CustDOB.SetDbValue(ref rsnew, ew_UnformatDateTime(CustDOB.CurrentValue, 0), System.DBNull.Value, CustDOB.ReadOnly);

				// DefContactDOB
				DefContactDOB.SetDbValue(ref rsnew, ew_UnformatDateTime(DefContactDOB.CurrentValue, 0), System.DBNull.Value, DefContactDOB.ReadOnly);

				// ScanImage
				ScanImage.SetDbValue(ref rsnew, ScanImage.CurrentValue, System.DBNull.Value, ScanImage.ReadOnly);

				// BizNature
				BizNature.SetDbValue(ref rsnew, BizNature.CurrentValue, System.DBNull.Value, BizNature.ReadOnly);

				// DefContactPOB
				DefContactPOB.SetDbValue(ref rsnew, DefContactPOB.CurrentValue, System.DBNull.Value, DefContactPOB.ReadOnly);

				// NewTran
				NewTran.SetDbValue(ref rsnew, NewTran.CurrentValue, System.DBNull.Value, NewTran.ReadOnly);

				// BizRegNo
				BizRegNo.SetDbValue(ref rsnew, BizRegNo.CurrentValue, System.DBNull.Value, BizRegNo.ReadOnly);

				// BizRegDate
				BizRegDate.SetDbValue(ref rsnew, ew_UnformatDateTime(BizRegDate.CurrentValue, 0), System.DBNull.Value, BizRegDate.ReadOnly);

				// BizRegPlace
				BizRegPlace.SetDbValue(ref rsnew, BizRegPlace.CurrentValue, System.DBNull.Value, BizRegPlace.ReadOnly);

				// BizRegExpDate
				BizRegExpDate.SetDbValue(ref rsnew, ew_UnformatDateTime(BizRegExpDate.CurrentValue, 0), System.DBNull.Value, BizRegExpDate.ReadOnly);

				// UnIncorpExec
				UnIncorpExec.SetDbValue(ref rsnew, UnIncorpExec.CurrentValue, 0, UnIncorpExec.ReadOnly);

				// DefContactAuthorzLetter
				DefContactAuthorzLetter.SetDbValue(ref rsnew, DefContactAuthorzLetter.CurrentValue, 0, DefContactAuthorzLetter.ReadOnly);

				// Politician
				Politician.SetDbValue(ref rsnew, Politician.CurrentValue, 0, Politician.ReadOnly);

				// BizPartnerNo
				BizPartnerNo.SetDbValue(ref rsnew, BizPartnerNo.CurrentValue, 0, BizPartnerNo.ReadOnly);

				// Remark2
				Remark2.SetDbValue(ref rsnew, Remark2.CurrentValue, System.DBNull.Value, Remark2.ReadOnly);

				// BannedListRemark
				BannedListRemark.SetDbValue(ref rsnew, BannedListRemark.CurrentValue, System.DBNull.Value, BannedListRemark.ReadOnly);

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

			// Add record
			#pragma warning disable 168, 219
			public bool AddRow(OrderedDictionary rsold = null) {
				bool result = false;
				var rsnew = new OrderedDictionary();
				if (ew_NotEmpty(AgentId.CurrentValue)) { // Check field with unique index
					var sFilter = "(AgentId = '" + ew_AdjustSql(AgentId.CurrentValue, DBID) + "')";
					using (var rschk = LoadRs(sFilter)) {
						if (rschk != null && rschk.Read()) {
							FailureMessage = Language.Phrase("DupIndex").Replace("%f", Agent.AgentId.FldCaption).Replace("%v", Convert.ToString(Agent.AgentId.CurrentValue));
							return false;
						}
					}
				}

				// Load db values from rsold
				if (rsold != null) {
					LoadDbValues(rsold);
				}
				try {

					// AgentId
					AgentId.SetDbValue(ref rsnew, AgentId.CurrentValue, "", false);

					// AgentName
					AgentName.SetDbValue(ref rsnew, AgentName.CurrentValue, "", false);

					// AgentRiskRating
					AgentRiskRating.SetDbValue(ref rsnew, AgentRiskRating.CurrentValue, System.DBNull.Value, ew_Empty(AgentRiskRating.CurrentValue));

					// AgentRiskCredit
					AgentRiskCredit.SetDbValue(ref rsnew, AgentRiskCredit.CurrentValue, System.DBNull.Value, ew_Empty(AgentRiskCredit.CurrentValue));

					// Address1
					Address1.SetDbValue(ref rsnew, Address1.CurrentValue, "", false);

					// Address2
					Address2.SetDbValue(ref rsnew, Address2.CurrentValue, System.DBNull.Value, false);

					// Address3
					Address3.SetDbValue(ref rsnew, Address3.CurrentValue, System.DBNull.Value, false);

					// Country
					Country.SetDbValue(ref rsnew, Country.CurrentValue, "", false);

					// ZipCode
					ZipCode.SetDbValue(ref rsnew, ZipCode.CurrentValue, "", false);

					// Fax
					Fax.SetDbValue(ref rsnew, Fax.CurrentValue, "", false);

					// Phone
					Phone.SetDbValue(ref rsnew, Phone.CurrentValue, "", false);

					// Mobile
					Mobile.SetDbValue(ref rsnew, Mobile.CurrentValue, "", false);

					// BuzType
					BuzType.SetDbValue(ref rsnew, BuzType.CurrentValue, "", false);

					// ClassType
					ClassType.SetDbValue(ref rsnew, ClassType.CurrentValue, "", false);

					// DefContactPName
					DefContactPName.SetDbValue(ref rsnew, DefContactPName.CurrentValue, "", false);

					// DefContactPNric
					DefContactPNric.SetDbValue(ref rsnew, DefContactPNric.CurrentValue, "", false);

					// DefContactPNation
					DefContactPNation.SetDbValue(ref rsnew, DefContactPNation.CurrentValue, System.DBNull.Value, false);

					// DefContactPOccupation
					DefContactPOccupation.SetDbValue(ref rsnew, DefContactPOccupation.CurrentValue, System.DBNull.Value, false);

					// TermsId
					TermsId.SetDbValue(ref rsnew, TermsId.CurrentValue, System.DBNull.Value, false);

					// LedgerBal
					LedgerBal.SetDbValue(ref rsnew, LedgerBal.CurrentValue, System.DBNull.Value, false);

					// AvailableBal
					AvailableBal.SetDbValue(ref rsnew, AvailableBal.CurrentValue, System.DBNull.Value, false);

					// _Email
					_Email.SetDbValue(ref rsnew, _Email.CurrentValue, System.DBNull.Value, false);

					// URL
					URL.SetDbValue(ref rsnew, URL.CurrentValue, System.DBNull.Value, false);

					// CustType
					CustType.SetDbValue(ref rsnew, CustType.CurrentValue, System.DBNull.Value, false);

					// RemittanceLicNO
					RemittanceLicNO.SetDbValue(ref rsnew, RemittanceLicNO.CurrentValue, "", false);

					// MCLicNo
					MCLicNo.SetDbValue(ref rsnew, MCLicNo.CurrentValue, "", false);

					// BankYesNo
					BankYesNo.SetDbValue(ref rsnew, ew_ConvertToBool(BankYesNo.CurrentValue, "1", "0"), System.DBNull.Value, false); // DN1204

					// BankODLimit
					BankODLimit.SetDbValue(ref rsnew, BankODLimit.CurrentValue, System.DBNull.Value, false);

					// BankAcctNO
					BankAcctNO.SetDbValue(ref rsnew, BankAcctNO.CurrentValue, System.DBNull.Value, false);

					// CreditLimit
					CreditLimit.SetDbValue(ref rsnew, CreditLimit.CurrentValue, System.DBNull.Value, ew_Empty(CreditLimit.CurrentValue));

					// ReferBy
					ReferBy.SetDbValue(ref rsnew, ReferBy.CurrentValue, "", false);

					// AgentImageName
					AgentImageName.SetDbValue(ref rsnew, AgentImageName.CurrentValue, System.DBNull.Value, false);

					// status
					status.SetDbValue(ref rsnew, status.CurrentValue, "", ew_Empty(status.CurrentValue));

					// CreatedBy
					CreatedBy.SetDbValue(ref rsnew, CreatedBy.CurrentValue, "", false);

					// CreatedDate
					CreatedDate.SetDbValue(ref rsnew, ew_UnformatDateTime(CreatedDate.CurrentValue, 0), DateTime.Now, ew_Empty(CreatedDate.CurrentValue));

					// ModifiedUser
					ModifiedUser.SetDbValue(ref rsnew, ModifiedUser.CurrentValue, System.DBNull.Value, false);

					// ModifiedDate
					ModifiedDate.SetDbValue(ref rsnew, ew_UnformatDateTime(ModifiedDate.CurrentValue, 0), System.DBNull.Value, false);

					// PPExpiryDate
					PPExpiryDate.SetDbValue(ref rsnew, ew_UnformatDateTime(PPExpiryDate.CurrentValue, 0), System.DBNull.Value, false);

					// TTExpiryDate
					TTExpiryDate.SetDbValue(ref rsnew, ew_UnformatDateTime(TTExpiryDate.CurrentValue, 0), DateTime.Now, ew_Empty(TTExpiryDate.CurrentValue));

					// MCExpiryDate
					MCExpiryDate.SetDbValue(ref rsnew, ew_UnformatDateTime(MCExpiryDate.CurrentValue, 0), DateTime.Now, ew_Empty(MCExpiryDate.CurrentValue));

					// Action
					Action.SetDbValue(ref rsnew, Action.CurrentValue, System.DBNull.Value, false);

					// Remark
					Remark.SetDbValue(ref rsnew, Remark.CurrentValue, System.DBNull.Value, false);

					// MCType
					MCType.SetDbValue(ref rsnew, MCType.CurrentValue, System.DBNull.Value, ew_Empty(MCType.CurrentValue));

					// CustDOB
					CustDOB.SetDbValue(ref rsnew, ew_UnformatDateTime(CustDOB.CurrentValue, 0), System.DBNull.Value, ew_Empty(CustDOB.CurrentValue));

					// DefContactDOB
					DefContactDOB.SetDbValue(ref rsnew, ew_UnformatDateTime(DefContactDOB.CurrentValue, 0), System.DBNull.Value, ew_Empty(DefContactDOB.CurrentValue));

					// ScanImage
					ScanImage.SetDbValue(ref rsnew, ScanImage.CurrentValue, System.DBNull.Value, false);

					// BizNature
					BizNature.SetDbValue(ref rsnew, BizNature.CurrentValue, System.DBNull.Value, false);

					// DefContactPOB
					DefContactPOB.SetDbValue(ref rsnew, DefContactPOB.CurrentValue, System.DBNull.Value, false);

					// NewTran
					NewTran.SetDbValue(ref rsnew, NewTran.CurrentValue, System.DBNull.Value, ew_Empty(NewTran.CurrentValue));

					// BizRegNo
					BizRegNo.SetDbValue(ref rsnew, BizRegNo.CurrentValue, System.DBNull.Value, false);

					// BizRegDate
					BizRegDate.SetDbValue(ref rsnew, ew_UnformatDateTime(BizRegDate.CurrentValue, 0), System.DBNull.Value, false);

					// BizRegPlace
					BizRegPlace.SetDbValue(ref rsnew, BizRegPlace.CurrentValue, System.DBNull.Value, false);

					// BizRegExpDate
					BizRegExpDate.SetDbValue(ref rsnew, ew_UnformatDateTime(BizRegExpDate.CurrentValue, 0), System.DBNull.Value, false);

					// UnIncorpExec
					UnIncorpExec.SetDbValue(ref rsnew, UnIncorpExec.CurrentValue, 0, ew_Empty(UnIncorpExec.CurrentValue));

					// DefContactAuthorzLetter
					DefContactAuthorzLetter.SetDbValue(ref rsnew, DefContactAuthorzLetter.CurrentValue, 0, ew_Empty(DefContactAuthorzLetter.CurrentValue));

					// Politician
					Politician.SetDbValue(ref rsnew, Politician.CurrentValue, 0, ew_Empty(Politician.CurrentValue));

					// BizPartnerNo
					BizPartnerNo.SetDbValue(ref rsnew, BizPartnerNo.CurrentValue, 0, ew_Empty(BizPartnerNo.CurrentValue));

					// Remark2
					Remark2.SetDbValue(ref rsnew, Remark2.CurrentValue, System.DBNull.Value, false);

					// BannedListRemark
					BannedListRemark.SetDbValue(ref rsnew, BannedListRemark.CurrentValue, System.DBNull.Value, false);
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw;
					FailureMessage = e.Message;
					return false;
				}

				// Call Row Inserting event
				bool bInsertRow = Row_Inserting(rsold, ref rsnew);

				// Check if key value entered
				if (bInsertRow && ValidateKey && ew_Empty(rsnew["AgentId"])) {
					FailureMessage = Language.Phrase("InvalidKeyValue");
					bInsertRow = false;
				}

				// Check for duplicate key
				if (bInsertRow && ValidateKey) {
					string sFilter = KeyFilter;
					using (var rschk = LoadRs(sFilter)) {
						if (rschk != null && rschk.Read()) {
							FailureMessage = Language.Phrase("DupKey").Replace("%f", sFilter);
							bInsertRow = false;
						}
					}
				}
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

			// Load advanced search
			public void LoadAdvancedSearch() {
				AgentId.AdvancedSearch.Load();
				AgentName.AdvancedSearch.Load();
				AgentRiskRating.AdvancedSearch.Load();
				AgentRiskCredit.AdvancedSearch.Load();
				Address1.AdvancedSearch.Load();
				Address2.AdvancedSearch.Load();
				Address3.AdvancedSearch.Load();
				Country.AdvancedSearch.Load();
				ZipCode.AdvancedSearch.Load();
				Fax.AdvancedSearch.Load();
				Phone.AdvancedSearch.Load();
				Mobile.AdvancedSearch.Load();
				BuzType.AdvancedSearch.Load();
				ClassType.AdvancedSearch.Load();
				DefContactPName.AdvancedSearch.Load();
				DefContactPNric.AdvancedSearch.Load();
				DefContactPNation.AdvancedSearch.Load();
				DefContactPOccupation.AdvancedSearch.Load();
				TermsId.AdvancedSearch.Load();
				LedgerBal.AdvancedSearch.Load();
				AvailableBal.AdvancedSearch.Load();
				_Email.AdvancedSearch.Load();
				URL.AdvancedSearch.Load();
				CustType.AdvancedSearch.Load();
				RemittanceLicNO.AdvancedSearch.Load();
				MCLicNo.AdvancedSearch.Load();
				BankYesNo.AdvancedSearch.Load();
				BankODLimit.AdvancedSearch.Load();
				BankAcctNO.AdvancedSearch.Load();
				CreditLimit.AdvancedSearch.Load();
				ReferBy.AdvancedSearch.Load();
				AgentImageName.AdvancedSearch.Load();
				status.AdvancedSearch.Load();
				CreatedBy.AdvancedSearch.Load();
				CreatedDate.AdvancedSearch.Load();
				ModifiedUser.AdvancedSearch.Load();
				ModifiedDate.AdvancedSearch.Load();
				PPExpiryDate.AdvancedSearch.Load();
				TTExpiryDate.AdvancedSearch.Load();
				MCExpiryDate.AdvancedSearch.Load();
				Action.AdvancedSearch.Load();
				Remark.AdvancedSearch.Load();
				MCType.AdvancedSearch.Load();
				CustDOB.AdvancedSearch.Load();
				DefContactDOB.AdvancedSearch.Load();
				ScanImage.AdvancedSearch.Load();
				BizNature.AdvancedSearch.Load();
				DefContactPOB.AdvancedSearch.Load();
				NewTran.AdvancedSearch.Load();
				BizRegNo.AdvancedSearch.Load();
				BizRegDate.AdvancedSearch.Load();
				BizRegPlace.AdvancedSearch.Load();
				BizRegExpDate.AdvancedSearch.Load();
				UnIncorpExec.AdvancedSearch.Load();
				DefContactAuthorzLetter.AdvancedSearch.Load();
				Politician.AdvancedSearch.Load();
				BizPartnerNo.AdvancedSearch.Load();
				Remark2.AdvancedSearch.Load();
				BannedListRemark.AdvancedSearch.Load();
			}

			// Set up export options
			public void SetupExportOptions() {
				cListOption item;

				// Printer friendly
				item = ExportOptions.Add("print");
				item.Body = "<a href=\"" + ew_AppPath(ExportPrintUrl) + "\" class=\"ewExportLink ewPrint\" title=\"" + ew_HtmlEncode(Language.Phrase("PrinterFriendlyText")) + "\" data-caption=\"" + ew_HtmlEncode(Language.Phrase("PrinterFriendlyText")) + "\">" + Language.Phrase("PrinterFriendly") + "</a>";
				item.Visible = true;

				// Export to Excel
				item = ExportOptions.Add("excel");
				item.Body = "<a href=\"" + ew_AppPath(ExportExcelUrl) + "\" class=\"ewExportLink ewExcel\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToExcelText")) + "\" data-caption=\"" + ew_HtmlEncode(Language.Phrase("ExportToExcelText")) + "\">" + Language.Phrase("ExportToExcel") + "</a>";
				item.Visible = true;

				// Export to Word
				item = ExportOptions.Add("word");
				item.Body = "<a href=\"" + ew_AppPath(ExportWordUrl) + "\" class=\"ewExportLink ewWord\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToWordText")) + "\" data-caption=\"" + ew_HtmlEncode(Language.Phrase("ExportToWordText")) + "\">" + Language.Phrase("ExportToWord") + "</a>";
				item.Visible = false;

				// Export to Html
				item = ExportOptions.Add("html");
				item.Body = "<a href=\"" + ew_AppPath(ExportHtmlUrl) + "\" class=\"ewExportLink ewHtml\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToHtmlText")) + "\" data-caption=\"" + ew_HtmlEncode(Language.Phrase("ExportToHtmlText")) + "\">" + Language.Phrase("ExportToHtml") + "</a>";
				item.Visible = false;

				// Export to Xml
				item = ExportOptions.Add("xml");
				item.Body = "<a href=\"" + ew_AppPath(ExportXmlUrl) + "\" class=\"ewExportLink ewXml\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToXmlText")) + "\" data-caption=\"" + ew_HtmlEncode(Language.Phrase("ExportToXmlText")) + "\">" + Language.Phrase("ExportToXml") + "</a>";
				item.Visible = false;

				// Export to Csv
				item = ExportOptions.Add("csv");
				item.Body = "<a href=\"" + ew_AppPath(ExportCsvUrl) + "\" class=\"ewExportLink ewCsv\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToCsvText")) + "\" data-caption=\"" + ew_HtmlEncode(Language.Phrase("ExportToCsvText")) + "\">" + Language.Phrase("ExportToCsv") + "</a>";
				item.Visible = false;

				// Export to Pdf
				item = ExportOptions.Add("pdf");
				item.Body = "<a href=\"" + ew_AppPath(ExportPdfUrl) + "\" class=\"ewExportLink ewPdf\" title=\"" + ew_HtmlEncode(Language.Phrase("ExportToPDFText")) + "\" data-caption=\"" + ew_HtmlEncode(Language.Phrase("ExportToPDFText")) + "\">" + Language.Phrase("ExportToPDF") + "</a>";
				item.Visible = false;

				// Export to Email
				item = ExportOptions.Add("email");
				var url = "";
				item.Body = "<button id=\"emf_Agent\" class=\"ewExportLink ewEmail\" title=\"" + Language.Phrase("ExportToEmailText") + "\" data-caption=\"" + Language.Phrase("ExportToEmailText") + "\" onclick=\"ew_EmailDialogShow({lnk:'emf_Agent',hdr:ewLanguage.Phrase('ExportToEmailText'),f:document.fAgentlist,sel:false" + url + "});\">" + Language.Phrase("ExportToEmail") + "</button>";
				item.Visible = false;

				// Drop down button for export
				ExportOptions.UseButtonGroup = true;
				ExportOptions.UseImageAndText = true;
				ExportOptions.UseDropDownButton = false;
				if (ExportOptions.UseButtonGroup && ew_IsMobile())
					ExportOptions.UseDropDownButton = true;
				ExportOptions.DropDownButtonPhrase = Language.Phrase("ButtonExport");

				// Add group option item
				item = ExportOptions.Add(ExportOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;
			}
			#pragma warning disable 168

			// Export data in HTML/CSV/Word/Excel/XML/Email/PDF format
			public void ExportData() {
				var utf8 = ew_SameText(EW_CHARSET, "utf-8");
				var bSelectLimit = true; // List page // DN

				// Load recordset // DN
				DbDataReader rs = null;
				if (!bSelectLimit) { // View page
					if (Recordset == null) {
						Recordset = LoadRecordset();
						Recordset.Read(); // Move to the start record
					}
					rs = Recordset;
				}
				if (TotalRecs < 0)
					TotalRecs = SelectRecordCount();
				StartRec = 1;

				// Export all
				if (ExportAll) {
					DisplayRecs = TotalRecs;
					StopRec = TotalRecs;
				} else { // Export one page only
					SetUpStartRec(); // Set up start record position

					// Set the last record to display
					if (DisplayRecs < 0) {
						StopRec = TotalRecs;
					} else {
						StopRec = StartRec + DisplayRecs - 1;
					}
				}
				rs = LoadRecordset(StartRec - 1, (DisplayRecs <= 0) ? TotalRecs : DisplayRecs); // DN
				if (rs == null) {
					ew_AddHeader(HeaderNames.ContentType, ""); // Remove header
					ew_AddHeader(HeaderNames.ContentDisposition, "");
					ShowMessage();
					return;
				}
				ExportDoc = ew_ExportDocument(this, "h");
				var Doc = ExportDoc;

				// Call Page Exporting server event
				ExportDoc.ExportCustom = !Page_Exporting();

				// Page header
				string sHeader = PageHeader;
				Page_DataRendering(ref sHeader);
				Doc.Text.Append(sHeader);

				// Export
				ExportDocument(Doc, rs, StartRec, StopRec, "");

				// Page footer
				string sFooter = PageFooter;
				Page_DataRendered(ref sFooter);
				Doc.Text.Append(sFooter);

				// Close recordset
				rs.Close();
				rs.Dispose();

				// Call Page Exported server event
				Page_Exported();

				// Export header and footer
				Doc.ExportHeaderAndFooter();

				// Write debug message if enabled
				if (EW_DEBUG_ENABLED && Export != "pdf") {
					ew_Write(ew_DebugMsg());
				} else {
					ew_Response.Clear(); // Clean output buffer
				}

				// Output data
				Doc.Export();
			}
			#pragma warning restore 168

			// Set up Breadcrumb
			public void SetupBreadcrumb() {
				Breadcrumb = new cBreadcrumb();
				var url = ew_CurrentUrl();
				url = Regex.Replace(url, @"\?cmd=reset(all)?$", ""); // Remove cmd=reset / cmd=resetall
				Breadcrumb.Add("list", TableVar, url, "", TableVar, true);
			}
			#pragma warning disable 168, 1522

			// Setup lookup filters of a field
			public override void SetupLookupFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				if (pageId == "list") {
					switch (fld.FldVar) {
					case "x_Country":
						sSqlWrk = "";
						sSqlWrk = "SELECT [varCountryCode] AS [LinkFld], [varCountryCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[tblStdCountry]";
						sWhereWrk = "";
						Country.LookupFilters = new Dictionary<string, string>() {};
						Country.LookupFilters.Add("s", sSqlWrk);
						Country.LookupFilters.Add("d", "");
						Country.LookupFilters.Add("f0", "[varCountryCode] = {filter_value}");
						Country.LookupFilters.Add("t0", "129");
						Country.LookupFilters.Add("fn0", "");
						sSqlWrk = "";
					Lookup_Selecting(Country, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [varCountry] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							Country.LookupFilters["s"] += sSqlWrk;
						break;
					}
				} else if (pageId == "extbs") {
					switch (fld.FldVar) {
					case "x_Country":
						sSqlWrk = "";
						sSqlWrk = "SELECT [varCountryCode] AS [LinkFld], [varCountryCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[tblStdCountry]";
						sWhereWrk = "";
						Country.LookupFilters = new Dictionary<string, string>() {};
						Country.LookupFilters.Add("s", sSqlWrk);
						Country.LookupFilters.Add("d", "");
						Country.LookupFilters.Add("f0", "[varCountryCode] = {filter_value}");
						Country.LookupFilters.Add("t0", "129");
						Country.LookupFilters.Add("fn0", "");
						sSqlWrk = "";
					Lookup_Selecting(Country, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [varCountry] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							Country.LookupFilters["s"] += sSqlWrk;
						break;
					}
				}
			}

			// Setup AutoSuggest filters of a field
			public override void SetupAutoSuggestFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				if (pageId == "list") {
					switch (fld.FldVar) {
					}
				} else if (pageId == "extbs") {
					switch (fld.FldVar) {
					}
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

			// ListOptions Load event
			public virtual void ListOptions_Load() {

				// Example:
				//var opt = ListOptions.Add("new");
				//opt.Header = "xxx";
				//opt.OnLeft = true; // Link on left
				//opt.MoveTo(0); // Move to first column

			}

			// ListOptions Rendered event
			public virtual void ListOptions_Rendered() {

				//Example:
				//ListOptions["new"].Body = "xxx";

			}

			// Row Custom Action event
			public virtual bool Row_CustomAction(string action, OrderedDictionary row) {

				// Return false to abort
				return true;
			}

			// Grid Inserting event
			public virtual bool Grid_Inserting() {

				// Enter your code here
				// To reject grid insert, set return value to FALSE

				return true;
			}

			// Grid Inserted event
			public virtual void Grid_Inserted(List<OrderedDictionary> rsnew) {

				//ew_Write("Grid Inserted");
			}

			// Grid Updating event
			public virtual bool Grid_Updating(List<OrderedDictionary> rsold) {

				// Enter your code here
				// To reject grid update, set return value to FALSE

				return true;
			}

			// Grid Updated event
			public virtual void Grid_Updated(List<OrderedDictionary> rsold, List<OrderedDictionary> rsnew) {

				//ew_Write("Grid Updated");
			}
		}
	}
}
