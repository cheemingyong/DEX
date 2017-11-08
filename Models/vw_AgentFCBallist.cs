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

		// vw_AgentFCBal_list
		public static cvw_AgentFCBal_list vw_AgentFCBal_list {
			get { return (cvw_AgentFCBal_list)ew_ViewData["vw_AgentFCBal_list"]; }
			set { ew_ViewData["vw_AgentFCBal_list"] = value; }
		}

		//
		// Page class for vw_AgentFCBal
		//

		public class cvw_AgentFCBal_list : cvw_AgentFCBal_list_base
		{

			// Construtor
			public cvw_AgentFCBal_list(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cvw_AgentFCBal_list_base : cvw_AgentFCBal, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "vw_AgentFCBal";

			// Page object name
			public string PageObjName = "vw_AgentFCBal_list";

			// Page terminated // DN
			private bool _terminated = false;

			// Grid form hidden field names
			public string FormName = "fvw_AgentFCBallist";
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

			public cvw_AgentFCBal_list_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (vw_AgentFCBal)
				if (vw_AgentFCBal == null || vw_AgentFCBal is cvw_AgentFCBal)
					vw_AgentFCBal = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				ExportPdfUrl = PageUrl + "export=pdf";
				AddUrl = "vw_AgentFCBaladd";
				InlineAddUrl = PageUrl + "a=add";
				GridAddUrl = PageUrl + "a=gridadd";
				GridEditUrl = PageUrl + "a=gridedit";
				MultiDeleteUrl = "vw_AgentFCBaldelete";
				MultiUpdateUrl = "vw_AgentFCBalupdate";

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
				FilterOptions.TagClassName = "ewFilterOption fvw_AgentFCBallistsrch";

				// List actions
				ListActions = new cListActions();
			}

			//
			// Page_Init
			//

			public IActionResult Page_Init() {

				// Header
				ew_Header(EW_CACHE);

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
				Balance.SetVisibility();
				CurrCode.SetVisibility();

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
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { vw_AgentFCBal, "" }); // DN
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
					vw_AgentFCBal.AgentId.FormValue = arKeyFlds[0];
				}
				return true;
			}

			// Check if empty row
			public bool EmptyRow() {
				return false;
			}
			#pragma warning disable 162

			// Get list of filters
			public string GetFilterList() {

				// Initialize
				string sFilterList = "";
				sFilterList = ew_Concat(sFilterList, AgentId.AdvancedSearch.ToJSON(), ","); // Field AgentId
				sFilterList = ew_Concat(sFilterList, AgentName.AdvancedSearch.ToJSON(), ","); // Field AgentName
				sFilterList = ew_Concat(sFilterList, Balance.AdvancedSearch.ToJSON(), ","); // Field Balance
				sFilterList = ew_Concat(sFilterList, CurrCode.AdvancedSearch.ToJSON(), ","); // Field CurrCode
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

				// Field Balance
				if (filter.ContainsKey("x_Balance")) {
					Balance.AdvancedSearch.SearchValue = filter["x_Balance"];
					Balance.AdvancedSearch.SearchOperator = filter["z_Balance"];
					Balance.AdvancedSearch.SearchCondition = filter["v_Balance"];
					Balance.AdvancedSearch.SearchValue2 = filter["y_Balance"];
					Balance.AdvancedSearch.SearchOperator2 = filter["w_Balance"];
					Balance.AdvancedSearch.Save();
				}

				// Field CurrCode
				if (filter.ContainsKey("x_CurrCode")) {
					CurrCode.AdvancedSearch.SearchValue = filter["x_CurrCode"];
					CurrCode.AdvancedSearch.SearchOperator = filter["z_CurrCode"];
					CurrCode.AdvancedSearch.SearchCondition = filter["v_CurrCode"];
					CurrCode.AdvancedSearch.SearchValue2 = filter["y_CurrCode"];
					CurrCode.AdvancedSearch.SearchOperator2 = filter["w_CurrCode"];
					CurrCode.AdvancedSearch.Save();
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
				BuildBasicSearchSQL(ref sWhere, AgentId, arKeywords, type);
				BuildBasicSearchSQL(ref sWhere, AgentName, arKeywords, type);
				BuildBasicSearchSQL(ref sWhere, CurrCode, arKeywords, type);
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
					UpdateSort(AgentId); // AgentId
					UpdateSort(AgentName); // AgentName
					UpdateSort(Balance); // Balance
					UpdateSort(CurrCode); // CurrCode
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
						Balance.Sort = "";
						CurrCode.Sort = "";
					}

					// Reset start position
					StartRec = 1;
					StartRecordNumber = StartRec;
				}
			}

			// Set up list options
			public void SetupListOptions() {
				cListOption item;

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
				oListOpt.Body = "<input type=\"checkbox\" name=\"key_m\" value=\"" + ew_HtmlEncode(AgentId.CurrentValue) + "\" onclick='ew_ClickMultiCheckbox(event);'>";
				RenderListOptionsExt();

				// Call ListOptions_Rendered event
				ListOptions_Rendered();
			}

			// Set up other options
			public void SetupOtherOptions() {
				cListOptions option;
				cListOption item;
				var options = OtherOptions;
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
				item.Body = "<a class=\"ewSaveFilter\" data-form=\"fvw_AgentFCBallistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ewDeleteFilter\" data-form=\"fvw_AgentFCBallistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
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
					option = options["action"];

					// Set up list action buttons
					foreach (KeyValuePair<string, cListAction> kvp in ListActions.Items) {
						if (kvp.Value.Select == EW_ACTION_MULTIPLE) {
							item = option.Add("custom_" + kvp.Value.Action);
							string caption = kvp.Value.Caption;
							var icon = (kvp.Value.Icon != "") ? "<span class=\"" + ew_HtmlEncode(kvp.Value.Icon) + "\" data-caption=\"" + ew_HtmlEncode(caption) + "\"></span> " : caption;
							item.Body = "<a class=\"ewAction ewListAction\" title=\"" + ew_HtmlEncode(caption) + "\" data-caption=\"" + ew_HtmlEncode(caption) + "\" href=\"\" onclick=\"ew_SubmitAction(event,jQuery.extend({f:document.fvw_AgentFCBallist}," + kvp.Value.ToJson(true) + ")); return false;\">" + icon + "</a>";
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
				item.Body = "<button type=\"button\" class=\"btn btn-default ewSearchToggle" + SearchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fvw_AgentFCBallistsrch\">" + Language.Phrase("SearchBtn") + "</button>";
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

			// Load basic search values // DN
			public void LoadBasicSearchValues() {
				if (ew_QueryString.ContainsKey(EW_TABLE_BASIC_SEARCH)) {
	                BasicSearch.Keyword = ew_Get(EW_TABLE_BASIC_SEARCH);
	                Command = "search";
				}
	            if (ew_QueryString.ContainsKey(EW_TABLE_BASIC_SEARCH_TYPE))
				    BasicSearch.Type = ew_Get(EW_TABLE_BASIC_SEARCH_TYPE);
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
				Balance.DbValue = row["Balance"];
				CurrCode.DbValue = row["CurrCode"];
			}
			#pragma warning restore 162, 168

			// Load DbValue from recordset
			public void LoadDbValues(OrderedDictionary row) {
				if (row == null)
					return;
				AgentId.SetDbValue(row["AgentId"]);
				AgentName.SetDbValue(row["AgentName"]);
				Balance.SetDbValue(row["Balance"]);
				CurrCode.SetDbValue(row["CurrCode"]);
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
				if (ew_SameStr(Balance.FormValue, Balance.CurrentValue) && ew_IsNumeric(ew_StrToFloat(Balance.CurrentValue)))
					Balance.CurrentValue = ew_StrToFloat(Balance.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// AgentId
				// AgentName
				// Balance
				// CurrCode

				if (RowType == EW_ROWTYPE_VIEW) { // View row

					// AgentId
					AgentId.ViewValue = AgentId.CurrentValue;

					// AgentName
					AgentName.ViewValue = AgentName.CurrentValue;

					// Balance
					Balance.ViewValue = Balance.CurrentValue;

					// CurrCode
					CurrCode.ViewValue = CurrCode.CurrentValue;

					// AgentId
					AgentId.LinkCustomAttributes = AgentId.FldTagACustomAttributes; // DN
					AgentId.HrefValue = "";
					AgentId.TooltipValue = "";

					// AgentName
					AgentName.LinkCustomAttributes = AgentName.FldTagACustomAttributes; // DN
					AgentName.HrefValue = "";
					AgentName.TooltipValue = "";

					// Balance
					Balance.LinkCustomAttributes = Balance.FldTagACustomAttributes; // DN
					Balance.HrefValue = "";
					Balance.TooltipValue = "";

					// CurrCode
					CurrCode.LinkCustomAttributes = CurrCode.FldTagACustomAttributes; // DN
					CurrCode.HrefValue = "";
					CurrCode.TooltipValue = "";
				}

				// Call Row Rendered event
				if (RowType != EW_ROWTYPE_AGGREGATEINIT)
					Row_Rendered();
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
				item.Body = "<button id=\"emf_vw_AgentFCBal\" class=\"ewExportLink ewEmail\" title=\"" + Language.Phrase("ExportToEmailText") + "\" data-caption=\"" + Language.Phrase("ExportToEmailText") + "\" onclick=\"ew_EmailDialogShow({lnk:'emf_vw_AgentFCBal',hdr:ewLanguage.Phrase('ExportToEmailText'),f:document.fvw_AgentFCBallist,sel:false" + url + "});\">" + Language.Phrase("ExportToEmail") + "</button>";
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
