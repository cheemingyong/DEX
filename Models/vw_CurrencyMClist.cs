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

		// vw_CurrencyMC_list
		public static cvw_CurrencyMC_list vw_CurrencyMC_list {
			get { return (cvw_CurrencyMC_list)ew_ViewData["vw_CurrencyMC_list"]; }
			set { ew_ViewData["vw_CurrencyMC_list"] = value; }
		}

		//
		// Page class for vw_CurrencyMC
		//

		public class cvw_CurrencyMC_list : cvw_CurrencyMC_list_base
		{

			// Construtor
			public cvw_CurrencyMC_list(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cvw_CurrencyMC_list_base : cvw_CurrencyMC, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "vw_CurrencyMC";

			// Page object name
			public string PageObjName = "vw_CurrencyMC_list";

			// Page terminated // DN
			private bool _terminated = false;

			// Grid form hidden field names
			public string FormName = "fvw_CurrencyMClist";
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

			public cvw_CurrencyMC_list_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (vw_CurrencyMC)
				if (vw_CurrencyMC == null || vw_CurrencyMC is cvw_CurrencyMC)
					vw_CurrencyMC = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				ExportPdfUrl = PageUrl + "export=pdf";
				AddUrl = "vw_CurrencyMCadd";
				InlineAddUrl = PageUrl + "a=add";
				GridAddUrl = PageUrl + "a=gridadd";
				GridEditUrl = PageUrl + "a=gridedit";
				MultiDeleteUrl = "vw_CurrencyMCdelete";
				MultiUpdateUrl = "vw_CurrencyMCupdate";

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
				FilterOptions.TagClassName = "ewFilterOption fvw_CurrencyMClistsrch";

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
				CurrencyCode.SetVisibility();
				MCBuyRate.SetVisibility();
				MCBuyRateType.SetVisibility();
				MCBuyMinBid.SetVisibility();
				MCBuyMaxBid.SetVisibility();
				MCSellRate.SetVisibility();
				MCSellRateType.SetVisibility();
				MCSellMinBid.SetVisibility();
				MCSellMaxBid.SetVisibility();

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
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { vw_CurrencyMC, "" }); // DN
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
				string sSrchBasic = ""; // Basic search filter
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
					ew_AddFilter(ref DefaultSearchWhere, BasicSearchWhere(true));

					// Get basic search values
					LoadBasicSearchValues();

					// Restore search parms from Session if not searching / reset / export
					if ((Export != "" || Command != "search" && Command != "reset" && Command != "resetall") && CheckSearchParms())
						RestoreSearchParms();

					// Call Recordset SearchValidated event
					Recordset_SearchValidated();

					// Set up sorting order
					SetUpSortOrder();

					// Get basic search criteria
					if (ew_Empty(gsSearchError))
						sSrchBasic = BasicSearchWhere();
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

					// Load basic search from default
					BasicSearch.LoadDefault();
					if (ew_NotEmpty(BasicSearch.Keyword))
						sSrchBasic = BasicSearchWhere();
				}

				// Build search criteria
				ew_AddFilter(ref SearchWhere, sSrchBasic);

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
				MCBuyRate.SetFormValue("", false); // Clear form value
				MCBuyMinBid.SetFormValue("", false); // Clear form value
				MCBuyMaxBid.SetFormValue("", false); // Clear form value
				MCSellRate.SetFormValue("", false); // Clear form value
				MCSellMinBid.SetFormValue("", false); // Clear form value
				MCSellMaxBid.SetFormValue("", false); // Clear form value
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
					vw_CurrencyMC.CurrencyCode.FormValue = arKeyFlds[0];
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
								sKey += CurrencyCode.CurrentValue;

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
				if (ObjForm.HasValue("x_CurrencyCode") && ObjForm.HasValue("o_CurrencyCode") && !ew_SameStr(CurrencyCode.CurrentValue, CurrencyCode.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCBuyRate") && ObjForm.HasValue("o_MCBuyRate") && !ew_SameStr(MCBuyRate.CurrentValue, MCBuyRate.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCBuyRateType") && ObjForm.HasValue("o_MCBuyRateType") && !ew_SameStr(MCBuyRateType.CurrentValue, MCBuyRateType.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCBuyMinBid") && ObjForm.HasValue("o_MCBuyMinBid") && !ew_SameStr(MCBuyMinBid.CurrentValue, MCBuyMinBid.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCBuyMaxBid") && ObjForm.HasValue("o_MCBuyMaxBid") && !ew_SameStr(MCBuyMaxBid.CurrentValue, MCBuyMaxBid.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCSellRate") && ObjForm.HasValue("o_MCSellRate") && !ew_SameStr(MCSellRate.CurrentValue, MCSellRate.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCSellRateType") && ObjForm.HasValue("o_MCSellRateType") && !ew_SameStr(MCSellRateType.CurrentValue, MCSellRateType.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCSellMinBid") && ObjForm.HasValue("o_MCSellMinBid") && !ew_SameStr(MCSellMinBid.CurrentValue, MCSellMinBid.OldValue))
					return false;
				if (ObjForm.HasValue("x_MCSellMaxBid") && ObjForm.HasValue("o_MCSellMaxBid") && !ew_SameStr(MCSellMaxBid.CurrentValue, MCSellMaxBid.OldValue))
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
				sFilterList = ew_Concat(sFilterList, CurrencyCode.AdvancedSearch.ToJSON(), ","); // Field CurrencyCode
				sFilterList = ew_Concat(sFilterList, MCBuyRate.AdvancedSearch.ToJSON(), ","); // Field MCBuyRate
				sFilterList = ew_Concat(sFilterList, MCBuyRateType.AdvancedSearch.ToJSON(), ","); // Field MCBuyRateType
				sFilterList = ew_Concat(sFilterList, MCBuyMinBid.AdvancedSearch.ToJSON(), ","); // Field MCBuyMinBid
				sFilterList = ew_Concat(sFilterList, MCBuyMaxBid.AdvancedSearch.ToJSON(), ","); // Field MCBuyMaxBid
				sFilterList = ew_Concat(sFilterList, MCSellRate.AdvancedSearch.ToJSON(), ","); // Field MCSellRate
				sFilterList = ew_Concat(sFilterList, MCSellRateType.AdvancedSearch.ToJSON(), ","); // Field MCSellRateType
				sFilterList = ew_Concat(sFilterList, MCSellMinBid.AdvancedSearch.ToJSON(), ","); // Field MCSellMinBid
				sFilterList = ew_Concat(sFilterList, MCSellMaxBid.AdvancedSearch.ToJSON(), ","); // Field MCSellMaxBid
				if (BasicSearch.Keyword != "") {
					string sWrk = "\"" + EW_TABLE_BASIC_SEARCH + "\":\"" + ew_JsEncode2(BasicSearch.Keyword) + "\",\"" + EW_TABLE_BASIC_SEARCH_TYPE + "\":\"" + ew_JsEncode2(BasicSearch.Type) + "\"";
					sFilterList = ew_Concat(sFilterList, sWrk, ",");
				}
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

				// Field CurrencyCode
				if (filter.ContainsKey("x_CurrencyCode")) {
					CurrencyCode.AdvancedSearch.SearchValue = filter["x_CurrencyCode"];
					CurrencyCode.AdvancedSearch.SearchOperator = filter["z_CurrencyCode"];
					CurrencyCode.AdvancedSearch.SearchCondition = filter["v_CurrencyCode"];
					CurrencyCode.AdvancedSearch.SearchValue2 = filter["y_CurrencyCode"];
					CurrencyCode.AdvancedSearch.SearchOperator2 = filter["w_CurrencyCode"];
					CurrencyCode.AdvancedSearch.Save();
				}

				// Field MCBuyRate
				if (filter.ContainsKey("x_MCBuyRate")) {
					MCBuyRate.AdvancedSearch.SearchValue = filter["x_MCBuyRate"];
					MCBuyRate.AdvancedSearch.SearchOperator = filter["z_MCBuyRate"];
					MCBuyRate.AdvancedSearch.SearchCondition = filter["v_MCBuyRate"];
					MCBuyRate.AdvancedSearch.SearchValue2 = filter["y_MCBuyRate"];
					MCBuyRate.AdvancedSearch.SearchOperator2 = filter["w_MCBuyRate"];
					MCBuyRate.AdvancedSearch.Save();
				}

				// Field MCBuyRateType
				if (filter.ContainsKey("x_MCBuyRateType")) {
					MCBuyRateType.AdvancedSearch.SearchValue = filter["x_MCBuyRateType"];
					MCBuyRateType.AdvancedSearch.SearchOperator = filter["z_MCBuyRateType"];
					MCBuyRateType.AdvancedSearch.SearchCondition = filter["v_MCBuyRateType"];
					MCBuyRateType.AdvancedSearch.SearchValue2 = filter["y_MCBuyRateType"];
					MCBuyRateType.AdvancedSearch.SearchOperator2 = filter["w_MCBuyRateType"];
					MCBuyRateType.AdvancedSearch.Save();
				}

				// Field MCBuyMinBid
				if (filter.ContainsKey("x_MCBuyMinBid")) {
					MCBuyMinBid.AdvancedSearch.SearchValue = filter["x_MCBuyMinBid"];
					MCBuyMinBid.AdvancedSearch.SearchOperator = filter["z_MCBuyMinBid"];
					MCBuyMinBid.AdvancedSearch.SearchCondition = filter["v_MCBuyMinBid"];
					MCBuyMinBid.AdvancedSearch.SearchValue2 = filter["y_MCBuyMinBid"];
					MCBuyMinBid.AdvancedSearch.SearchOperator2 = filter["w_MCBuyMinBid"];
					MCBuyMinBid.AdvancedSearch.Save();
				}

				// Field MCBuyMaxBid
				if (filter.ContainsKey("x_MCBuyMaxBid")) {
					MCBuyMaxBid.AdvancedSearch.SearchValue = filter["x_MCBuyMaxBid"];
					MCBuyMaxBid.AdvancedSearch.SearchOperator = filter["z_MCBuyMaxBid"];
					MCBuyMaxBid.AdvancedSearch.SearchCondition = filter["v_MCBuyMaxBid"];
					MCBuyMaxBid.AdvancedSearch.SearchValue2 = filter["y_MCBuyMaxBid"];
					MCBuyMaxBid.AdvancedSearch.SearchOperator2 = filter["w_MCBuyMaxBid"];
					MCBuyMaxBid.AdvancedSearch.Save();
				}

				// Field MCSellRate
				if (filter.ContainsKey("x_MCSellRate")) {
					MCSellRate.AdvancedSearch.SearchValue = filter["x_MCSellRate"];
					MCSellRate.AdvancedSearch.SearchOperator = filter["z_MCSellRate"];
					MCSellRate.AdvancedSearch.SearchCondition = filter["v_MCSellRate"];
					MCSellRate.AdvancedSearch.SearchValue2 = filter["y_MCSellRate"];
					MCSellRate.AdvancedSearch.SearchOperator2 = filter["w_MCSellRate"];
					MCSellRate.AdvancedSearch.Save();
				}

				// Field MCSellRateType
				if (filter.ContainsKey("x_MCSellRateType")) {
					MCSellRateType.AdvancedSearch.SearchValue = filter["x_MCSellRateType"];
					MCSellRateType.AdvancedSearch.SearchOperator = filter["z_MCSellRateType"];
					MCSellRateType.AdvancedSearch.SearchCondition = filter["v_MCSellRateType"];
					MCSellRateType.AdvancedSearch.SearchValue2 = filter["y_MCSellRateType"];
					MCSellRateType.AdvancedSearch.SearchOperator2 = filter["w_MCSellRateType"];
					MCSellRateType.AdvancedSearch.Save();
				}

				// Field MCSellMinBid
				if (filter.ContainsKey("x_MCSellMinBid")) {
					MCSellMinBid.AdvancedSearch.SearchValue = filter["x_MCSellMinBid"];
					MCSellMinBid.AdvancedSearch.SearchOperator = filter["z_MCSellMinBid"];
					MCSellMinBid.AdvancedSearch.SearchCondition = filter["v_MCSellMinBid"];
					MCSellMinBid.AdvancedSearch.SearchValue2 = filter["y_MCSellMinBid"];
					MCSellMinBid.AdvancedSearch.SearchOperator2 = filter["w_MCSellMinBid"];
					MCSellMinBid.AdvancedSearch.Save();
				}

				// Field MCSellMaxBid
				if (filter.ContainsKey("x_MCSellMaxBid")) {
					MCSellMaxBid.AdvancedSearch.SearchValue = filter["x_MCSellMaxBid"];
					MCSellMaxBid.AdvancedSearch.SearchOperator = filter["z_MCSellMaxBid"];
					MCSellMaxBid.AdvancedSearch.SearchCondition = filter["v_MCSellMaxBid"];
					MCSellMaxBid.AdvancedSearch.SearchValue2 = filter["y_MCSellMaxBid"];
					MCSellMaxBid.AdvancedSearch.SearchOperator2 = filter["w_MCSellMaxBid"];
					MCSellMaxBid.AdvancedSearch.Save();
				}
				if (filter.ContainsKey(EW_TABLE_BASIC_SEARCH))
					BasicSearch.SessionKeyword = filter[EW_TABLE_BASIC_SEARCH];
				if (filter.ContainsKey(EW_TABLE_BASIC_SEARCH_TYPE))
					BasicSearch.SessionType = filter[EW_TABLE_BASIC_SEARCH_TYPE];
				return true;
			}

			// Return basic search SQL
			public string BasicSearchSQL(List<string> arKeywords, string type) {
				string sWhere = "";
				BuildBasicSearchSQL(ref sWhere, CurrencyCode, arKeywords, type);
				BuildBasicSearchSQL(ref sWhere, MCBuyRateType, arKeywords, type);
				BuildBasicSearchSQL(ref sWhere, MCSellRateType, arKeywords, type);
				return sWhere;
			}

			// Build basic search SQL
			public void BuildBasicSearchSQL(ref string Where, cField Fld, List<string> arKeywords, string type) {
				string sDefCond = (type == "OR") ? "OR" : "AND";
				var arSQL = new List<string>(); // Array for SQL parts
				var arCond = new List<string>(); // Array for search conditions
				int cnt = arKeywords.Count;
				int j = 0; // Number of SQL parts
				for (int i = 0; i < cnt; i++) {
					string Keyword = arKeywords[i];
					Keyword = Keyword.Trim();

					// try var ar = new List<dynamic>(); // try
					string[] ar = new string[] {};
					if (ew_NotEmpty(EW_BASIC_SEARCH_IGNORE_PATTERN)) {
						Keyword = Regex.Replace(Keyword, EW_BASIC_SEARCH_IGNORE_PATTERN, "\\");
						ar = Keyword.Split('\\');
					} else {
						ar = new string[] {Keyword};
					}
					foreach (var aKeyword in ar) {
						if (ew_NotEmpty(aKeyword)) {
							string sWrk = "";
							if (aKeyword == "OR" && type == "") {
								if (j > 0)
									arCond[j-1] = "OR";
							} else if (aKeyword == EW_NULL_VALUE) {
								sWrk = Fld.FldExpression + " IS NULL";
							} else if (aKeyword == EW_NOT_NULL_VALUE) {
								sWrk = Fld.FldExpression + " IS NOT NULL";
							} else if (Fld.FldIsVirtual) {
								sWrk = Fld.FldVirtualExpression + ew_Like(ew_QuotedValue("%" + aKeyword + "%", EW_DATATYPE_STRING, DBID), DBID);
							} else if (Fld.FldDataType != EW_DATATYPE_NUMBER || ew_IsNumeric(aKeyword)) {
								sWrk = Fld.FldBasicSearchExpression + ew_Like(ew_QuotedValue("%" + aKeyword + "%", EW_DATATYPE_STRING, DBID), DBID);
							}
							if (ew_NotEmpty(sWrk)) {
								arSQL.Add(sWrk); // DN
								arCond.Add(sDefCond); // DN
								j++;
							}
						}
					}
				}
				cnt = arSQL.Count;
				bool bQuoted = false;
				string sSql = "";
				if (cnt > 0) {
					for (int i = 0; i < cnt - 1; i++) {
						if (arCond[i] == "OR") {
							if (!bQuoted)
								sSql += "(";
							bQuoted = true;
						}
						sSql += arSQL[i];
						if (bQuoted && arCond[i] != "OR") {
							sSql += ")";
							bQuoted = false;
						}
						sSql += " " + arCond[i] + " ";
					}
					sSql += arSQL[cnt-1];
					if (bQuoted)
						sSql += ")";
				}
				if (ew_NotEmpty(sSql)) {
					if (ew_NotEmpty(Where))
						Where += " OR ";
					Where += "(" + sSql + ")";
				}
			}

			// Return basic search WHERE clause based on search keyword and type
			public string BasicSearchWhere(bool def = false) {
				string searchStr = "";
				string searchKeyword = (def) ? BasicSearch.KeywordDefault : BasicSearch.Keyword;
				string searchType = (def) ? BasicSearch.TypeDefault : BasicSearch.Type;
				if (ew_NotEmpty(searchKeyword)) {
					string search = searchKeyword.Trim();
					if (searchType != "=") {
						var ar = ew_GetKeywords(search);

						// Search keyword in any fields
						if ((searchType == "OR" || searchType == "AND") && ew_ConvertToBool(BasicSearch.BasicSearchAnyFields)) {
							foreach (var sKeyword in ar) {
								if (sKeyword != "") {
									if (searchStr != "")
										searchStr += " " + searchType + " ";
									searchStr += "(" + BasicSearchSQL(new List<string>() { sKeyword }, searchType) + ")";
								}
							}
						} else {
							searchStr = BasicSearchSQL(ar, searchType);
						}
					} else {
						searchStr = BasicSearchSQL(new List<string>() { search }, searchType);
					}
					if (!def)
						Command = "search";
				}
				if (!def && Command == "search") {
					BasicSearch.SessionKeyword = searchKeyword;
					BasicSearch.SessionType = searchType;
				}
				return searchStr;
			}

			// Check if search parm exists
			public bool CheckSearchParms() {

				// Check basic search
				if (BasicSearch.IssetSession)
					return true;
				return false;
			}

			// Clear all search parameters
			public void ResetSearchParms() {
				SearchWhere = "";
				SessionSearchWhere = SearchWhere;

				// Clear basic search parameters
				ResetBasicSearchParms();
			}

			// Load advanced search default values
			public bool LoadAdvancedSearchDefault() {
				return false;
			}

			// Clear all basic search parameters
			public void ResetBasicSearchParms() {
				BasicSearch.UnsetSession();
			}

			// Restore all search parameters
			public void RestoreSearchParms() {
				RestoreSearch = true;

				// Restore basic search values
				BasicSearch.Load();
			}

			// Set up sort parameters
			public void SetUpSortOrder() {

				// Check for "order" parameter
				if (ew_NotEmpty(ew_Get("order"))) {
					CurrentOrder = ew_Get("order");
					CurrentOrderType = ew_Get("ordertype");
					UpdateSort(CurrencyCode); // CurrencyCode
					UpdateSort(MCBuyRate); // MCBuyRate
					UpdateSort(MCBuyRateType); // MCBuyRateType
					UpdateSort(MCBuyMinBid); // MCBuyMinBid
					UpdateSort(MCBuyMaxBid); // MCBuyMaxBid
					UpdateSort(MCSellRate); // MCSellRate
					UpdateSort(MCSellRateType); // MCSellRateType
					UpdateSort(MCSellMinBid); // MCSellMinBid
					UpdateSort(MCSellMaxBid); // MCSellMaxBid
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
						CurrencyCode.Sort = "";
						MCBuyRate.Sort = "";
						MCBuyRateType.Sort = "";
						MCBuyMinBid.Sort = "";
						MCBuyMaxBid.Sort = "";
						MCSellRate.Sort = "";
						MCSellRateType.Sort = "";
						MCSellMinBid.Sort = "";
						MCSellMaxBid.Sort = "";
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

				// "checkbox"
				oListOpt = ListOptions["checkbox"];
				oListOpt.Body = "<input type=\"checkbox\" name=\"key_m\" value=\"" + ew_HtmlEncode(CurrencyCode.CurrentValue) + "\" onclick='ew_ClickMultiCheckbox(event);'>";
				if (CurrentAction == "gridedit" && ew_IsNumeric(RowIndex)) {
					MultiSelectKey += "<input type=\"hidden\" name=\"" + KeyName + "\" id=\"" + KeyName + "\" value=\"" + CurrencyCode.CurrentValue + "\">";
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
				item.Body = "<a class=\"ewSaveFilter\" data-form=\"fvw_CurrencyMClistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ewDeleteFilter\" data-form=\"fvw_CurrencyMClistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
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
							item.Body = "<a class=\"ewAction ewListAction\" title=\"" + ew_HtmlEncode(caption) + "\" data-caption=\"" + ew_HtmlEncode(caption) + "\" href=\"\" onclick=\"ew_SubmitAction(event,jQuery.extend({f:document.fvw_CurrencyMClist}," + kvp.Value.ToJson(true) + ")); return false;\">" + icon + "</a>";
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
				item.Body = "<button type=\"button\" class=\"btn btn-default ewSearchToggle" + SearchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fvw_CurrencyMClistsrch\">" + Language.Phrase("SearchBtn") + "</button>";
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
				CurrencyCode.CurrentValue = System.DBNull.Value;
				CurrencyCode.OldValue = CurrencyCode.CurrentValue;
				MCBuyRate.CurrentValue = System.DBNull.Value;
				MCBuyRate.OldValue = MCBuyRate.CurrentValue;
				MCBuyRateType.CurrentValue = System.DBNull.Value;
				MCBuyRateType.OldValue = MCBuyRateType.CurrentValue;
				MCBuyMinBid.CurrentValue = System.DBNull.Value;
				MCBuyMinBid.OldValue = MCBuyMinBid.CurrentValue;
				MCBuyMaxBid.CurrentValue = System.DBNull.Value;
				MCBuyMaxBid.OldValue = MCBuyMaxBid.CurrentValue;
				MCSellRate.CurrentValue = System.DBNull.Value;
				MCSellRate.OldValue = MCSellRate.CurrentValue;
				MCSellRateType.CurrentValue = System.DBNull.Value;
				MCSellRateType.OldValue = MCSellRateType.CurrentValue;
				MCSellMinBid.CurrentValue = System.DBNull.Value;
				MCSellMinBid.OldValue = MCSellMinBid.CurrentValue;
				MCSellMaxBid.CurrentValue = System.DBNull.Value;
				MCSellMaxBid.OldValue = MCSellMaxBid.CurrentValue;
			}

			// Load basic search values // DN
			public void LoadBasicSearchValues() {
				if (ew_QueryString.ContainsKey(EW_TABLE_BASIC_SEARCH)) {
	                BasicSearch.Keyword = ew_Get(EW_TABLE_BASIC_SEARCH);
	                Command = "search";
				}
	            if (ew_QueryString.ContainsKey(EW_TABLE_BASIC_SEARCH_TYPE))
				    BasicSearch.Type = ew_Get(EW_TABLE_BASIC_SEARCH_TYPE);
			}

			// Load form values
			public void LoadFormValues() {
				if (!CurrencyCode.FldIsDetailKey) {
					CurrencyCode.FormValue = ObjForm.GetValue("x_CurrencyCode");
				}
				if (ObjForm.HasValue("o_CurrencyCode"))
					CurrencyCode.OldValue = ObjForm.GetValue("o_CurrencyCode");
				if (!MCBuyRate.FldIsDetailKey) {
					MCBuyRate.FormValue = ObjForm.GetValue("x_MCBuyRate");
				}
				if (ObjForm.HasValue("o_MCBuyRate"))
					MCBuyRate.OldValue = ObjForm.GetValue("o_MCBuyRate");
				if (!MCBuyRateType.FldIsDetailKey) {
					MCBuyRateType.FormValue = ObjForm.GetValue("x_MCBuyRateType");
				}
				if (ObjForm.HasValue("o_MCBuyRateType"))
					MCBuyRateType.OldValue = ObjForm.GetValue("o_MCBuyRateType");
				if (!MCBuyMinBid.FldIsDetailKey) {
					MCBuyMinBid.FormValue = ObjForm.GetValue("x_MCBuyMinBid");
				}
				if (ObjForm.HasValue("o_MCBuyMinBid"))
					MCBuyMinBid.OldValue = ObjForm.GetValue("o_MCBuyMinBid");
				if (!MCBuyMaxBid.FldIsDetailKey) {
					MCBuyMaxBid.FormValue = ObjForm.GetValue("x_MCBuyMaxBid");
				}
				if (ObjForm.HasValue("o_MCBuyMaxBid"))
					MCBuyMaxBid.OldValue = ObjForm.GetValue("o_MCBuyMaxBid");
				if (!MCSellRate.FldIsDetailKey) {
					MCSellRate.FormValue = ObjForm.GetValue("x_MCSellRate");
				}
				if (ObjForm.HasValue("o_MCSellRate"))
					MCSellRate.OldValue = ObjForm.GetValue("o_MCSellRate");
				if (!MCSellRateType.FldIsDetailKey) {
					MCSellRateType.FormValue = ObjForm.GetValue("x_MCSellRateType");
				}
				if (ObjForm.HasValue("o_MCSellRateType"))
					MCSellRateType.OldValue = ObjForm.GetValue("o_MCSellRateType");
				if (!MCSellMinBid.FldIsDetailKey) {
					MCSellMinBid.FormValue = ObjForm.GetValue("x_MCSellMinBid");
				}
				if (ObjForm.HasValue("o_MCSellMinBid"))
					MCSellMinBid.OldValue = ObjForm.GetValue("o_MCSellMinBid");
				if (!MCSellMaxBid.FldIsDetailKey) {
					MCSellMaxBid.FormValue = ObjForm.GetValue("x_MCSellMaxBid");
				}
				if (ObjForm.HasValue("o_MCSellMaxBid"))
					MCSellMaxBid.OldValue = ObjForm.GetValue("o_MCSellMaxBid");
			}

			// Restore form values
			public void RestoreFormValues() {
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				MCBuyRate.CurrentValue = MCBuyRate.FormValue;
				MCBuyRateType.CurrentValue = MCBuyRateType.FormValue;
				MCBuyMinBid.CurrentValue = MCBuyMinBid.FormValue;
				MCBuyMaxBid.CurrentValue = MCBuyMaxBid.FormValue;
				MCSellRate.CurrentValue = MCSellRate.FormValue;
				MCSellRateType.CurrentValue = MCSellRateType.FormValue;
				MCSellMinBid.CurrentValue = MCSellMinBid.FormValue;
				MCSellMaxBid.CurrentValue = MCSellMaxBid.FormValue;
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
				MCBuyRate.DbValue = row["MCBuyRate"];
				MCBuyRateType.DbValue = row["MCBuyRateType"];
				MCBuyMinBid.DbValue = row["MCBuyMinBid"];
				MCBuyMaxBid.DbValue = row["MCBuyMaxBid"];
				MCSellRate.DbValue = row["MCSellRate"];
				MCSellRateType.DbValue = row["MCSellRateType"];
				MCSellMinBid.DbValue = row["MCSellMinBid"];
				MCSellMaxBid.DbValue = row["MCSellMaxBid"];
			}
			#pragma warning restore 162, 168

			// Load DbValue from recordset
			public void LoadDbValues(OrderedDictionary row) {
				if (row == null)
					return;
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				MCBuyRate.SetDbValue(row["MCBuyRate"]);
				MCBuyRateType.SetDbValue(row["MCBuyRateType"]);
				MCBuyMinBid.SetDbValue(row["MCBuyMinBid"]);
				MCBuyMaxBid.SetDbValue(row["MCBuyMaxBid"]);
				MCSellRate.SetDbValue(row["MCSellRate"]);
				MCSellRateType.SetDbValue(row["MCSellRateType"]);
				MCSellMinBid.SetDbValue(row["MCSellMinBid"]);
				MCSellMaxBid.SetDbValue(row["MCSellMaxBid"]);
			}
			#pragma warning disable 618

			// Load old record
			public bool LoadOldRecord(cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {

				// Load key values from Session
				bool bValidKey = true;
				if (ew_NotEmpty(GetKey("CurrencyCode")))
					CurrencyCode.CurrentValue = GetKey("CurrencyCode"); // CurrencyCode
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
				if (ew_SameStr(MCBuyRate.FormValue, MCBuyRate.CurrentValue) && ew_IsNumeric(ew_StrToFloat(MCBuyRate.CurrentValue)))
					MCBuyRate.CurrentValue = ew_StrToFloat(MCBuyRate.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(MCBuyMinBid.FormValue, MCBuyMinBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(MCBuyMinBid.CurrentValue)))
					MCBuyMinBid.CurrentValue = ew_StrToFloat(MCBuyMinBid.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(MCBuyMaxBid.FormValue, MCBuyMaxBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(MCBuyMaxBid.CurrentValue)))
					MCBuyMaxBid.CurrentValue = ew_StrToFloat(MCBuyMaxBid.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(MCSellRate.FormValue, MCSellRate.CurrentValue) && ew_IsNumeric(ew_StrToFloat(MCSellRate.CurrentValue)))
					MCSellRate.CurrentValue = ew_StrToFloat(MCSellRate.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(MCSellMinBid.FormValue, MCSellMinBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(MCSellMinBid.CurrentValue)))
					MCSellMinBid.CurrentValue = ew_StrToFloat(MCSellMinBid.CurrentValue);

				// Convert decimal values if posted back
				if (ew_SameStr(MCSellMaxBid.FormValue, MCSellMaxBid.CurrentValue) && ew_IsNumeric(ew_StrToFloat(MCSellMaxBid.CurrentValue)))
					MCSellMaxBid.CurrentValue = ew_StrToFloat(MCSellMaxBid.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// CurrencyCode
				// MCBuyRate
				// MCBuyRateType
				// MCBuyMinBid
				// MCBuyMaxBid
				// MCSellRate
				// MCSellRateType
				// MCSellMinBid
				// MCSellMaxBid

				if (RowType == EW_ROWTYPE_VIEW) { // View row

					// CurrencyCode
					CurrencyCode.ViewValue = CurrencyCode.CurrentValue;

					// MCBuyRate
					MCBuyRate.ViewValue = MCBuyRate.CurrentValue;

					// MCBuyRateType
					if (Convert.ToString(MCBuyRateType.CurrentValue) != "") {
							MCBuyRateType.ViewValue = MCBuyRateType.OptionCaption(Convert.ToString(MCBuyRateType.CurrentValue));
					} else {
						MCBuyRateType.ViewValue = System.DBNull.Value;
					}

					// MCBuyMinBid
					MCBuyMinBid.ViewValue = MCBuyMinBid.CurrentValue;

					// MCBuyMaxBid
					MCBuyMaxBid.ViewValue = MCBuyMaxBid.CurrentValue;

					// MCSellRate
					MCSellRate.ViewValue = MCSellRate.CurrentValue;

					// MCSellRateType
					if (Convert.ToString(MCSellRateType.CurrentValue) != "") {
							MCSellRateType.ViewValue = MCSellRateType.OptionCaption(Convert.ToString(MCSellRateType.CurrentValue));
					} else {
						MCSellRateType.ViewValue = System.DBNull.Value;
					}

					// MCSellMinBid
					MCSellMinBid.ViewValue = MCSellMinBid.CurrentValue;

					// MCSellMaxBid
					MCSellMaxBid.ViewValue = MCSellMaxBid.CurrentValue;

					// CurrencyCode
					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// MCBuyRate
					MCBuyRate.LinkCustomAttributes = MCBuyRate.FldTagACustomAttributes; // DN
					MCBuyRate.HrefValue = "";
					MCBuyRate.TooltipValue = "";

					// MCBuyRateType
					MCBuyRateType.LinkCustomAttributes = MCBuyRateType.FldTagACustomAttributes; // DN
					MCBuyRateType.HrefValue = "";
					MCBuyRateType.TooltipValue = "";

					// MCBuyMinBid
					MCBuyMinBid.LinkCustomAttributes = MCBuyMinBid.FldTagACustomAttributes; // DN
					MCBuyMinBid.HrefValue = "";
					MCBuyMinBid.TooltipValue = "";

					// MCBuyMaxBid
					MCBuyMaxBid.LinkCustomAttributes = MCBuyMaxBid.FldTagACustomAttributes; // DN
					MCBuyMaxBid.HrefValue = "";
					MCBuyMaxBid.TooltipValue = "";

					// MCSellRate
					MCSellRate.LinkCustomAttributes = MCSellRate.FldTagACustomAttributes; // DN
					MCSellRate.HrefValue = "";
					MCSellRate.TooltipValue = "";

					// MCSellRateType
					MCSellRateType.LinkCustomAttributes = MCSellRateType.FldTagACustomAttributes; // DN
					MCSellRateType.HrefValue = "";
					MCSellRateType.TooltipValue = "";

					// MCSellMinBid
					MCSellMinBid.LinkCustomAttributes = MCSellMinBid.FldTagACustomAttributes; // DN
					MCSellMinBid.HrefValue = "";
					MCSellMinBid.TooltipValue = "";

					// MCSellMaxBid
					MCSellMaxBid.LinkCustomAttributes = MCSellMaxBid.FldTagACustomAttributes; // DN
					MCSellMaxBid.HrefValue = "";
					MCSellMaxBid.TooltipValue = "";
				} else if (RowType == EW_ROWTYPE_ADD) { // Add row

					// CurrencyCode
					CurrencyCode.EditAttrs["class"] = "form-control";
					CurrencyCode.EditValue = CurrencyCode.CurrentValue; // DN
					CurrencyCode.PlaceHolder = ew_RemoveHtml(CurrencyCode.FldCaption);

					// MCBuyRate
					MCBuyRate.EditAttrs["class"] = "form-control";
					MCBuyRate.EditValue = MCBuyRate.CurrentValue; // DN
					MCBuyRate.PlaceHolder = ew_RemoveHtml(MCBuyRate.FldCaption);
					if (ew_NotEmpty(MCBuyRate.EditValue) && ew_IsNumeric(Convert.ToString(MCBuyRate.EditValue))) {
					MCBuyRate.EditValue = ew_FormatNumber(MCBuyRate.EditValue, -2, -1, -2, 0);
					MCBuyRate.OldValue = MCBuyRate.EditValue;
					}

					// MCBuyRateType
					MCBuyRateType.EditValue = MCBuyRateType.Options(false);

					// MCBuyMinBid
					MCBuyMinBid.EditAttrs["class"] = "form-control";
					MCBuyMinBid.EditValue = MCBuyMinBid.CurrentValue; // DN
					MCBuyMinBid.PlaceHolder = ew_RemoveHtml(MCBuyMinBid.FldCaption);
					if (ew_NotEmpty(MCBuyMinBid.EditValue) && ew_IsNumeric(Convert.ToString(MCBuyMinBid.EditValue))) {
					MCBuyMinBid.EditValue = ew_FormatNumber(MCBuyMinBid.EditValue, -2, -1, -2, 0);
					MCBuyMinBid.OldValue = MCBuyMinBid.EditValue;
					}

					// MCBuyMaxBid
					MCBuyMaxBid.EditAttrs["class"] = "form-control";
					MCBuyMaxBid.EditValue = MCBuyMaxBid.CurrentValue; // DN
					MCBuyMaxBid.PlaceHolder = ew_RemoveHtml(MCBuyMaxBid.FldCaption);
					if (ew_NotEmpty(MCBuyMaxBid.EditValue) && ew_IsNumeric(Convert.ToString(MCBuyMaxBid.EditValue))) {
					MCBuyMaxBid.EditValue = ew_FormatNumber(MCBuyMaxBid.EditValue, -2, -1, -2, 0);
					MCBuyMaxBid.OldValue = MCBuyMaxBid.EditValue;
					}

					// MCSellRate
					MCSellRate.EditAttrs["class"] = "form-control";
					MCSellRate.EditValue = MCSellRate.CurrentValue; // DN
					MCSellRate.PlaceHolder = ew_RemoveHtml(MCSellRate.FldCaption);
					if (ew_NotEmpty(MCSellRate.EditValue) && ew_IsNumeric(Convert.ToString(MCSellRate.EditValue))) {
					MCSellRate.EditValue = ew_FormatNumber(MCSellRate.EditValue, -2, -1, -2, 0);
					MCSellRate.OldValue = MCSellRate.EditValue;
					}

					// MCSellRateType
					MCSellRateType.EditValue = MCSellRateType.Options(false);

					// MCSellMinBid
					MCSellMinBid.EditAttrs["class"] = "form-control";
					MCSellMinBid.EditValue = MCSellMinBid.CurrentValue; // DN
					MCSellMinBid.PlaceHolder = ew_RemoveHtml(MCSellMinBid.FldCaption);
					if (ew_NotEmpty(MCSellMinBid.EditValue) && ew_IsNumeric(Convert.ToString(MCSellMinBid.EditValue))) {
					MCSellMinBid.EditValue = ew_FormatNumber(MCSellMinBid.EditValue, -2, -1, -2, 0);
					MCSellMinBid.OldValue = MCSellMinBid.EditValue;
					}

					// MCSellMaxBid
					MCSellMaxBid.EditAttrs["class"] = "form-control";
					MCSellMaxBid.EditValue = MCSellMaxBid.CurrentValue; // DN
					MCSellMaxBid.PlaceHolder = ew_RemoveHtml(MCSellMaxBid.FldCaption);
					if (ew_NotEmpty(MCSellMaxBid.EditValue) && ew_IsNumeric(Convert.ToString(MCSellMaxBid.EditValue))) {
					MCSellMaxBid.EditValue = ew_FormatNumber(MCSellMaxBid.EditValue, -2, -1, -2, 0);
					MCSellMaxBid.OldValue = MCSellMaxBid.EditValue;
					}

					// Add refer script
					// CurrencyCode

					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";

					// MCBuyRate
					MCBuyRate.LinkCustomAttributes = MCBuyRate.FldTagACustomAttributes; // DN
					MCBuyRate.HrefValue = "";

					// MCBuyRateType
					MCBuyRateType.LinkCustomAttributes = MCBuyRateType.FldTagACustomAttributes; // DN
					MCBuyRateType.HrefValue = "";

					// MCBuyMinBid
					MCBuyMinBid.LinkCustomAttributes = MCBuyMinBid.FldTagACustomAttributes; // DN
					MCBuyMinBid.HrefValue = "";

					// MCBuyMaxBid
					MCBuyMaxBid.LinkCustomAttributes = MCBuyMaxBid.FldTagACustomAttributes; // DN
					MCBuyMaxBid.HrefValue = "";

					// MCSellRate
					MCSellRate.LinkCustomAttributes = MCSellRate.FldTagACustomAttributes; // DN
					MCSellRate.HrefValue = "";

					// MCSellRateType
					MCSellRateType.LinkCustomAttributes = MCSellRateType.FldTagACustomAttributes; // DN
					MCSellRateType.HrefValue = "";

					// MCSellMinBid
					MCSellMinBid.LinkCustomAttributes = MCSellMinBid.FldTagACustomAttributes; // DN
					MCSellMinBid.HrefValue = "";

					// MCSellMaxBid
					MCSellMaxBid.LinkCustomAttributes = MCSellMaxBid.FldTagACustomAttributes; // DN
					MCSellMaxBid.HrefValue = "";
				} else if (RowType == EW_ROWTYPE_EDIT) { // Edit row

					// CurrencyCode
					CurrencyCode.EditAttrs["class"] = "form-control";
					CurrencyCode.EditValue = CurrencyCode.CurrentValue;

					// MCBuyRate
					MCBuyRate.EditAttrs["class"] = "form-control";
					MCBuyRate.EditValue = MCBuyRate.CurrentValue; // DN
					MCBuyRate.PlaceHolder = ew_RemoveHtml(MCBuyRate.FldCaption);
					if (ew_NotEmpty(MCBuyRate.EditValue) && ew_IsNumeric(Convert.ToString(MCBuyRate.EditValue))) {
					MCBuyRate.EditValue = ew_FormatNumber(MCBuyRate.EditValue, -2, -1, -2, 0);
					MCBuyRate.OldValue = MCBuyRate.EditValue;
					}

					// MCBuyRateType
					MCBuyRateType.EditValue = MCBuyRateType.Options(false);

					// MCBuyMinBid
					MCBuyMinBid.EditAttrs["class"] = "form-control";
					MCBuyMinBid.EditValue = MCBuyMinBid.CurrentValue; // DN
					MCBuyMinBid.PlaceHolder = ew_RemoveHtml(MCBuyMinBid.FldCaption);
					if (ew_NotEmpty(MCBuyMinBid.EditValue) && ew_IsNumeric(Convert.ToString(MCBuyMinBid.EditValue))) {
					MCBuyMinBid.EditValue = ew_FormatNumber(MCBuyMinBid.EditValue, -2, -1, -2, 0);
					MCBuyMinBid.OldValue = MCBuyMinBid.EditValue;
					}

					// MCBuyMaxBid
					MCBuyMaxBid.EditAttrs["class"] = "form-control";
					MCBuyMaxBid.EditValue = MCBuyMaxBid.CurrentValue; // DN
					MCBuyMaxBid.PlaceHolder = ew_RemoveHtml(MCBuyMaxBid.FldCaption);
					if (ew_NotEmpty(MCBuyMaxBid.EditValue) && ew_IsNumeric(Convert.ToString(MCBuyMaxBid.EditValue))) {
					MCBuyMaxBid.EditValue = ew_FormatNumber(MCBuyMaxBid.EditValue, -2, -1, -2, 0);
					MCBuyMaxBid.OldValue = MCBuyMaxBid.EditValue;
					}

					// MCSellRate
					MCSellRate.EditAttrs["class"] = "form-control";
					MCSellRate.EditValue = MCSellRate.CurrentValue; // DN
					MCSellRate.PlaceHolder = ew_RemoveHtml(MCSellRate.FldCaption);
					if (ew_NotEmpty(MCSellRate.EditValue) && ew_IsNumeric(Convert.ToString(MCSellRate.EditValue))) {
					MCSellRate.EditValue = ew_FormatNumber(MCSellRate.EditValue, -2, -1, -2, 0);
					MCSellRate.OldValue = MCSellRate.EditValue;
					}

					// MCSellRateType
					MCSellRateType.EditValue = MCSellRateType.Options(false);

					// MCSellMinBid
					MCSellMinBid.EditAttrs["class"] = "form-control";
					MCSellMinBid.EditValue = MCSellMinBid.CurrentValue; // DN
					MCSellMinBid.PlaceHolder = ew_RemoveHtml(MCSellMinBid.FldCaption);
					if (ew_NotEmpty(MCSellMinBid.EditValue) && ew_IsNumeric(Convert.ToString(MCSellMinBid.EditValue))) {
					MCSellMinBid.EditValue = ew_FormatNumber(MCSellMinBid.EditValue, -2, -1, -2, 0);
					MCSellMinBid.OldValue = MCSellMinBid.EditValue;
					}

					// MCSellMaxBid
					MCSellMaxBid.EditAttrs["class"] = "form-control";
					MCSellMaxBid.EditValue = MCSellMaxBid.CurrentValue; // DN
					MCSellMaxBid.PlaceHolder = ew_RemoveHtml(MCSellMaxBid.FldCaption);
					if (ew_NotEmpty(MCSellMaxBid.EditValue) && ew_IsNumeric(Convert.ToString(MCSellMaxBid.EditValue))) {
					MCSellMaxBid.EditValue = ew_FormatNumber(MCSellMaxBid.EditValue, -2, -1, -2, 0);
					MCSellMaxBid.OldValue = MCSellMaxBid.EditValue;
					}

					// Edit refer script
					// CurrencyCode

					CurrencyCode.LinkCustomAttributes = CurrencyCode.FldTagACustomAttributes; // DN
					CurrencyCode.HrefValue = "";

					// MCBuyRate
					MCBuyRate.LinkCustomAttributes = MCBuyRate.FldTagACustomAttributes; // DN
					MCBuyRate.HrefValue = "";

					// MCBuyRateType
					MCBuyRateType.LinkCustomAttributes = MCBuyRateType.FldTagACustomAttributes; // DN
					MCBuyRateType.HrefValue = "";

					// MCBuyMinBid
					MCBuyMinBid.LinkCustomAttributes = MCBuyMinBid.FldTagACustomAttributes; // DN
					MCBuyMinBid.HrefValue = "";

					// MCBuyMaxBid
					MCBuyMaxBid.LinkCustomAttributes = MCBuyMaxBid.FldTagACustomAttributes; // DN
					MCBuyMaxBid.HrefValue = "";

					// MCSellRate
					MCSellRate.LinkCustomAttributes = MCSellRate.FldTagACustomAttributes; // DN
					MCSellRate.HrefValue = "";

					// MCSellRateType
					MCSellRateType.LinkCustomAttributes = MCSellRateType.FldTagACustomAttributes; // DN
					MCSellRateType.HrefValue = "";

					// MCSellMinBid
					MCSellMinBid.LinkCustomAttributes = MCSellMinBid.FldTagACustomAttributes; // DN
					MCSellMinBid.HrefValue = "";

					// MCSellMaxBid
					MCSellMaxBid.LinkCustomAttributes = MCSellMaxBid.FldTagACustomAttributes; // DN
					MCSellMaxBid.HrefValue = "";
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
				if (!CurrencyCode.FldIsDetailKey && ew_Empty(CurrencyCode.FormValue))
					gsFormError = ew_AddMessage(gsFormError, CurrencyCode.ReqErrMsg.Replace("%s", CurrencyCode.FldCaption));
				if (!ew_CheckNumber(MCBuyRate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCBuyRate.FldErrMsg);
				if (!ew_CheckNumber(MCBuyMinBid.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCBuyMinBid.FldErrMsg);
				if (!ew_CheckNumber(MCBuyMaxBid.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCBuyMaxBid.FldErrMsg);
				if (!ew_CheckNumber(MCSellRate.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCSellRate.FldErrMsg);
				if (!ew_CheckNumber(MCSellMinBid.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCSellMinBid.FldErrMsg);
				if (!ew_CheckNumber(MCSellMaxBid.FormValue))
					gsFormError = ew_AddMessage(gsFormError, MCSellMaxBid.FldErrMsg);

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
				vw_CurrencyMC = vw_CurrencyMC ?? new cvw_CurrencyMC();
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

				// CurrencyCode
				// MCBuyRate

				MCBuyRate.SetDbValue(ref rsnew, MCBuyRate.CurrentValue, System.DBNull.Value, MCBuyRate.ReadOnly);

				// MCBuyRateType
				MCBuyRateType.SetDbValue(ref rsnew, MCBuyRateType.CurrentValue, System.DBNull.Value, MCBuyRateType.ReadOnly);

				// MCBuyMinBid
				MCBuyMinBid.SetDbValue(ref rsnew, MCBuyMinBid.CurrentValue, System.DBNull.Value, MCBuyMinBid.ReadOnly);

				// MCBuyMaxBid
				MCBuyMaxBid.SetDbValue(ref rsnew, MCBuyMaxBid.CurrentValue, System.DBNull.Value, MCBuyMaxBid.ReadOnly);

				// MCSellRate
				MCSellRate.SetDbValue(ref rsnew, MCSellRate.CurrentValue, System.DBNull.Value, MCSellRate.ReadOnly);

				// MCSellRateType
				MCSellRateType.SetDbValue(ref rsnew, MCSellRateType.CurrentValue, System.DBNull.Value, MCSellRateType.ReadOnly);

				// MCSellMinBid
				MCSellMinBid.SetDbValue(ref rsnew, MCSellMinBid.CurrentValue, System.DBNull.Value, MCSellMinBid.ReadOnly);

				// MCSellMaxBid
				MCSellMaxBid.SetDbValue(ref rsnew, MCSellMaxBid.CurrentValue, System.DBNull.Value, MCSellMaxBid.ReadOnly);

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

				// Load db values from rsold
				if (rsold != null) {
					LoadDbValues(rsold);
				}
				try {

					// CurrencyCode
					CurrencyCode.SetDbValue(ref rsnew, CurrencyCode.CurrentValue, "", false);

					// MCBuyRate
					MCBuyRate.SetDbValue(ref rsnew, MCBuyRate.CurrentValue, System.DBNull.Value, false);

					// MCBuyRateType
					MCBuyRateType.SetDbValue(ref rsnew, MCBuyRateType.CurrentValue, System.DBNull.Value, false);

					// MCBuyMinBid
					MCBuyMinBid.SetDbValue(ref rsnew, MCBuyMinBid.CurrentValue, System.DBNull.Value, false);

					// MCBuyMaxBid
					MCBuyMaxBid.SetDbValue(ref rsnew, MCBuyMaxBid.CurrentValue, System.DBNull.Value, false);

					// MCSellRate
					MCSellRate.SetDbValue(ref rsnew, MCSellRate.CurrentValue, System.DBNull.Value, false);

					// MCSellRateType
					MCSellRateType.SetDbValue(ref rsnew, MCSellRateType.CurrentValue, System.DBNull.Value, false);

					// MCSellMinBid
					MCSellMinBid.SetDbValue(ref rsnew, MCSellMinBid.CurrentValue, System.DBNull.Value, false);

					// MCSellMaxBid
					MCSellMaxBid.SetDbValue(ref rsnew, MCSellMaxBid.CurrentValue, System.DBNull.Value, false);
				} catch (Exception e) {
					if (EW_DEBUG_ENABLED) throw;
					FailureMessage = e.Message;
					return false;
				}

				// Call Row Inserting event
				bool bInsertRow = Row_Inserting(rsold, ref rsnew);

				// Check if key value entered
				if (bInsertRow && ValidateKey && ew_Empty(rsnew["CurrencyCode"])) {
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
				item.Body = "<button id=\"emf_vw_CurrencyMC\" class=\"ewExportLink ewEmail\" title=\"" + Language.Phrase("ExportToEmailText") + "\" data-caption=\"" + Language.Phrase("ExportToEmailText") + "\" onclick=\"ew_EmailDialogShow({lnk:'emf_vw_CurrencyMC',hdr:ewLanguage.Phrase('ExportToEmailText'),f:document.fvw_CurrencyMClist,sel:false" + url + "});\">" + Language.Phrase("ExportToEmail") + "</button>";
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
