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

		// AgentBank_grid
		public static cAgentBank_grid AgentBank_grid {
			get { return (cAgentBank_grid)ew_ViewData["AgentBank_grid"]; }
			set { ew_ViewData["AgentBank_grid"] = value; }
		}

		//
		// Page class for AgentBank
		//

		public class cAgentBank_grid : cAgentBank_grid_base
		{

			// Construtor
			public cAgentBank_grid(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cAgentBank_grid_base : cAgentBank, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "grid";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "AgentBank";

			// Page object name
			public string PageObjName = "AgentBank_grid";

			// Page terminated // DN
			private bool _terminated = false;

			// Grid form hidden field names
			public string FormName = "fAgentBankgrid";
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

			public cAgentBank_grid_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				FormActionName += "_" + FormName;
				FormKeyName += "_" + FormName;
				FormOldKeyName += "_" + FormName;
				FormBlankRowName += "_" + FormName;
				FormKeyCountName += "_" + FormName;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (AgentBank)
				if (AgentBank == null || AgentBank is cAgentBank)
					AgentBank = this;
				AddUrl = "AgentBankadd";

				// Start time
				StartTime = Environment.TickCount;

				// Open connection
				Conn = Connection; // DN

				// List options
				ListOptions = new cListOptions();
				ListOptions.TableVar = TableVar;

				// Other options
				OtherOptions["addedit"] = new cListOptions();
				OtherOptions["addedit"].Tag = "div";
				OtherOptions["addedit"].TagClassName = "ewAddEditOption";
			}

			//
			// Page_Init
			//

			public IActionResult Page_Init() {

				// Header
				ew_Header(EW_CACHE);

				// Create form object
				ObjForm = new cFormObj();

				// Get grid add count
				int gridaddcnt = ew_ConvertToInt(ew_Get(EW_TABLE_GRID_ADD_ROW_COUNT));
				if (gridaddcnt > 0)
					GridAddRowCount = gridaddcnt;

				// Set up list options
				SetupListOptions();
				id.SetVisibility();
				id.Visible = !IsAdd && !IsCopy && !IsGridAdd;
				currencycode.SetVisibility();
				agentid.SetVisibility();
				BankId.SetVisibility();
				lastavgcost.SetVisibility();

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

				// Set up master detail parameters
				SetUpMasterParms();

				// Setup other options
				SetupOtherOptions();
				return null;
			}

			//
			// Page_Terminate
			//

			public IActionResult Page_Terminate(string url = "") { // DN
				if (_terminated) // DN
					return new EmptyResult();

				// Export
				if (ew_NotEmpty(CustomExport) && CustomExport == Export && EW_EXPORT.ContainsKey(CustomExport)) {
					var sContent = "";
					sContent = Regex.Match(ew_View.Output.ToString(), @"<html>[\s\S]+</html>", RegexOptions.IgnoreCase).Value;
					if (ew_Empty(gsExportFile))
						gsExportFile = TableVar;
					dynamic doc = ew_CreateInstance(EW_EXPORT[CustomExport], new object[] { AgentBank, "" }); // DN
					doc.Text.Append(sContent);
					if (Export == "email") {
					} else {
						doc.Export();
					}
					ew_DeleteTmpImages(); // Delete temp images
				}
				if (ew_Empty(url))
					return null;
				Page_Redirecting(ref url);

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
			public bool ShowOtherOptions = false;
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
				string sFilter = "";

				// Get command
				Command = ew_Get("cmd").ToLower();
				if (IsPageRequest) { // Validate request

					// Set up records per page
					SetUpDisplayRecs();

					// Handle reset command
					ResetCmd();

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

					// Show grid delete link for grid add / grid edit
					if (AllowAddDeleteRow) {
						if (CurrentAction == "gridadd" || CurrentAction == "gridedit") {
							var item = ListOptions["griddelete"];
							if (item != null) item.Visible = true;
						}
					}

					// Set up sorting order
					SetUpSortOrder();
				}

				// Restore display records
				if (RecordsPerPage == -1 || RecordsPerPage > 0) {
					DisplayRecs = RecordsPerPage; // Restore from Session
				} else {
					DisplayRecs = 20; // Load default
				}

				// Load Sorting Order
				LoadSortOrder();

				// Build filter
				sFilter = "";

				// Restore master/detail filter
				DbMasterFilter = MasterFilter; // Restore master filter
				DbDetailFilter = DetailFilter; // Restore detail filter
				ew_AddFilter(ref sFilter, DbDetailFilter);
				ew_AddFilter(ref sFilter, SearchWhere);

				// Load master record
				if (CurrentMode != "add" && ew_NotEmpty(MasterFilter) && CurrentMasterTable == "Agent") {
					using (var rsmaster = Agent.LoadRs(DbMasterFilter)) {
						MasterRecordExists = (rsmaster != null && rsmaster.Read());
						if (!MasterRecordExists) {
							FailureMessage = Language.Phrase("NoRecord"); // Set no record found
							return Page_Terminate("Agentlist"); // Return to master page
						} else {
							Agent.LoadListRowValues(rsmaster);
						}
					}
					Agent.RowType = EW_ROWTYPE_MASTER; // Master row
					Agent.RenderListRow(); // Note: Do it outside "using" // DN
				}

				// Set up filter in session
				SessionWhere = sFilter;
				CurrentFilter = "";

				// Load record count first
				if (!IsAddOrEdit) // DN
					TotalRecs = SelectRecordCount();
				return null;
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
				lastavgcost.SetFormValue("", false); // Clear form value
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

					// Get new recordset
					List<OrderedDictionary> rsnew = Connection.GetRows(sSql, true); // Use main connection (faster) // DN

				// Call Grid_Updated event
				Grid_Updated(rsold, rsnew);
					ClearInlineMode(); // Clear inline edit mode
				} else {
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
					AgentBank.id.FormValue = arKeyFlds[0];
					if (!ew_IsNumeric(AgentBank.id.FormValue))
						return false;
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
						if (rowaction == "insert") {
							RowOldKey = ObjForm.GetValue(FormOldKeyName);
							LoadOldRecord(); // Load old recordset
						}
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
								sKey += id.CurrentValue;

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
						ClearInlineMode(); // Clear grid add mode and return
						return true;
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					bGridInsert = false;
				}
				if (bGridInsert) {

					// Get new recordset
					CurrentFilter = sWrkFilter;
					string sSql = SQL;
					List<OrderedDictionary> rsnew = Connection.GetRows(sSql, true); // Use main connection (faster) // DN

					// Call Grid_Inserted event
					Grid_Inserted(rsnew);
					ClearInlineMode(); // Clear grid add mode
				} else {
					if (ew_Empty(FailureMessage))
						FailureMessage = Language.Phrase("InsertFailed"); // Set insert failed message
				}
				return bGridInsert;
			}
			#pragma warning restore 168, 219

			// Check if empty row
			public bool EmptyRow() {
				if (ObjForm.HasValue("x_currencycode") && ObjForm.HasValue("o_currencycode") && !ew_SameStr(currencycode.CurrentValue, currencycode.OldValue))
					return false;
				if (ObjForm.HasValue("x_agentid") && ObjForm.HasValue("o_agentid") && !ew_SameStr(agentid.CurrentValue, agentid.OldValue))
					return false;
				if (ObjForm.HasValue("x_BankId") && ObjForm.HasValue("o_BankId") && !ew_SameStr(BankId.CurrentValue, BankId.OldValue))
					return false;
				if (ObjForm.HasValue("x_lastavgcost") && ObjForm.HasValue("o_lastavgcost") && !ew_SameStr(lastavgcost.CurrentValue, lastavgcost.OldValue))
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

			// Set up sort parameters
			public void SetUpSortOrder() {

				// Check for "order" parameter
				if (ew_NotEmpty(ew_Get("order"))) {
					CurrentOrder = ew_Get("order");
					CurrentOrderType = ew_Get("ordertype");
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

					// Reset master/detail keys
					if (ew_SameText(Command, "resetall")) {
						CurrentMasterTable = ""; // Clear master table
						DbMasterFilter = "";
						DbDetailFilter = "";
						agentid.SessionValue = "";
					}

					// Reset sorting order
					if (ew_SameText(Command, "resetsort")) {
						string sOrderBy = "";
						SessionOrderBy = sOrderBy;
						SessionOrderByList = sOrderBy;
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

				// Drop down button for ListOptions
				ListOptions.UseImageAndText = true;
				ListOptions.UseDropDownButton = false;
				ListOptions.DropDownButtonPhrase = Language.Phrase("ButtonListOptions");
				ListOptions.UseButtonGroup = false;
				if (ListOptions.UseButtonGroup && ew_IsMobile())
					ListOptions.UseDropDownButton = true;
				ListOptions.ButtonClass = "btn-sm"; // Class for button group
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
					if (ObjForm.HasValue(FormOldKeyName))
						RowOldKey = ObjForm.GetValue(FormOldKeyName);
					if (ew_NotEmpty(RowOldKey))
						MultiSelectKey += "<input type=\"hidden\" name=\"" + OldKeyName + "\" id=\"" + OldKeyName + "\" value=\"" + ew_HtmlEncode(RowOldKey) + "\">";
					if (RowAction == "delete") {
						string rowkey = ObjForm.GetValue(FormKeyName);
						SetupKeyValues(rowkey);
					}
					if (RowAction == "insert" && CurrentAction == "F" && EmptyRow())
						MultiSelectKey += "<input type=\"hidden\" name=\"" + BlankRowName + "\" id=\"" + BlankRowName + "\" value=\"1\">";
				}

				// "delete"
				if (AllowAddDeleteRow) {
					if (CurrentMode == "add" || CurrentMode == "copy" || CurrentMode == "edit") {
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
				if (CurrentMode == "view") { // View mode
				} // End View mode
				if (CurrentMode == "edit" && ew_IsNumeric(RowIndex)) {
					MultiSelectKey += "<input type=\"hidden\" name=\"" + KeyName + "\" id=\"" + KeyName + "\" value=\"" + id.CurrentValue + "\">";
				}
				RenderListOptionsExt();
			}

			// Set record key
			public void SetRecordKey(ref string key, DbDataReader dr) {
				if (dr == null)
					return;
				var row = Connection.GetRow(dr); // DN
				key = "";
				if (ew_NotEmpty(key))
					key += EW_COMPOSITE_KEY_SEPARATOR;
				key += row["id"];
			}
			#pragma warning restore 168, 219

			// Set up other options
			public void SetupOtherOptions() {
				cListOptions option;
				cListOption item;
				option = OtherOptions["addedit"];
				option.UseDropDownButton = false;
				option.DropDownButtonPhrase = Language.Phrase("ButtonAddEdit");
				option.UseButtonGroup = true;
				option.ButtonClass = "btn-sm"; // Class for button group
				item = option.Add(option.GroupOptionName);
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
				if ((CurrentMode == "add" || CurrentMode == "copy" || CurrentMode == "edit") && CurrentAction != "F") { // Check add/copy/edit mode
					if (AllowAddDeleteRow) {
						option = options["addedit"];
						option.UseDropDownButton = false;
						option.UseImageAndText = true;
						item = option.Add("addblankrow");
						item.Body = "<a class=\"ewAddEdit ewAddBlankRow\" title=\"" + ew_HtmlTitle(Language.Phrase("AddBlankRow")) + "\" data-caption=\"" + ew_HtmlTitle(Language.Phrase("AddBlankRow")) + "\" href=\"javascript:void(0);\" onclick=\"ew_AddGridRow(this);\">" + Language.Phrase("AddBlankRow") + "</a>";
						item.Visible = true;
						ShowOtherOptions = item.Visible;
					}
				}
				if (CurrentMode == "view") { // Check view mode
					option = options["addedit"];
					item = option.GetItem("add");
					if (ew_NotEmpty(item) && item.Visible)
						ShowOtherOptions = true;
					else
						ShowOtherOptions = false;
				}
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

			// Confirm page
			public bool ConfirmPage = false; // DN

			// Get upload files
			public void GetUploadFiles() {

				// Get upload data
			}

			// Load default values
			public void LoadDefaultValues() {
				id.CurrentValue = System.DBNull.Value;
				id.OldValue = id.CurrentValue;
				currencycode.CurrentValue = System.DBNull.Value;
				currencycode.OldValue = currencycode.CurrentValue;
				agentid.CurrentValue = System.DBNull.Value;
				agentid.OldValue = agentid.CurrentValue;
				BankId.CurrentValue = System.DBNull.Value;
				BankId.OldValue = BankId.CurrentValue;
				lastavgcost.CurrentValue = lastavgcost.FldDefault;
				lastavgcost.OldValue = lastavgcost.CurrentValue;
			}

			// Load form values
			public void LoadFormValues() {
				ObjForm.FormName = FormName;
				if (!id.FldIsDetailKey && CurrentAction != "gridadd" && CurrentAction != "add")
					id.FormValue = ObjForm.GetValue("x_id");
				if (!currencycode.FldIsDetailKey) {
					currencycode.FormValue = ObjForm.GetValue("x_currencycode");
				}
				if (ObjForm.HasValue("o_currencycode"))
					currencycode.OldValue = ObjForm.GetValue("o_currencycode");
				if (!agentid.FldIsDetailKey) {
					agentid.FormValue = ObjForm.GetValue("x_agentid");
				}
				if (ObjForm.HasValue("o_agentid"))
					agentid.OldValue = ObjForm.GetValue("o_agentid");
				if (!BankId.FldIsDetailKey) {
					BankId.FormValue = ObjForm.GetValue("x_BankId");
				}
				if (ObjForm.HasValue("o_BankId"))
					BankId.OldValue = ObjForm.GetValue("o_BankId");
				if (!lastavgcost.FldIsDetailKey) {
					lastavgcost.FormValue = ObjForm.GetValue("x_lastavgcost");
				}
				if (ObjForm.HasValue("o_lastavgcost"))
					lastavgcost.OldValue = ObjForm.GetValue("o_lastavgcost");
			}

			// Restore form values
			public void RestoreFormValues() {
				if (CurrentAction != "gridadd" && CurrentAction != "add")
					id.CurrentValue = id.FormValue;
				currencycode.CurrentValue = currencycode.FormValue;
				agentid.CurrentValue = agentid.FormValue;
				BankId.CurrentValue = BankId.FormValue;
				lastavgcost.CurrentValue = lastavgcost.FormValue;
			}

			// Load recordset // DN
			public DbDataReader LoadRecordset(int offset = -1, int rowcnt = -1) {

				// Load list page SQL
				string sSql = SelectSQL;

				// Load recordset (Recordset_Selected event not supported) // DN
				return Connection.SelectLimit(sSql, rowcnt, offset, ew_NotEmpty(OrderBy) || ew_NotEmpty(SessionOrderByList));
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
				currencycode.DbValue = row["currencycode"];
				agentid.DbValue = row["agentid"];
				if (row.Contains("EV__agentid")) {
					agentid.VirtualValue = row["EV__agentid"]; // Set up virtual field value
				} else {
					agentid.VirtualValue = ""; // Clear value
				}
				BankId.DbValue = row["BankId"];
				lastavgcost.DbValue = row["lastavgcost"];
			}
			#pragma warning restore 162, 168

			// Load DbValue from recordset
			public void LoadDbValues(OrderedDictionary row) {
				if (row == null)
					return;
				id.SetDbValue(row["id"]);
				currencycode.SetDbValue(row["currencycode"]);
				agentid.SetDbValue(row["agentid"]);
				BankId.SetDbValue(row["BankId"]);
				lastavgcost.SetDbValue(row["lastavgcost"]);
			}
			#pragma warning disable 618

			// Load old record
			public bool LoadOldRecord(cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {

				// Load key values from Session
				bool bValidKey = true;
				string[] arKeys = { RowOldKey };
				int cnt = arKeys.Length;
				if (cnt >= 1) {
					if (ew_NotEmpty(arKeys[0]))
						id.CurrentValue = arKeys[0]; //  id;
					else
						bValidKey = false;
				} else {
					bValidKey = false;
				}

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
				if (ew_SameStr(lastavgcost.FormValue, lastavgcost.CurrentValue) && ew_IsNumeric(ew_StrToFloat(lastavgcost.CurrentValue)))
					lastavgcost.CurrentValue = ew_StrToFloat(lastavgcost.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// id
				// currencycode
				// agentid
				// BankId
				// lastavgcost

				if (RowType == EW_ROWTYPE_VIEW) { // View row

					// id
					id.ViewValue = id.CurrentValue;

					// currencycode
					if (ew_NotEmpty(currencycode.CurrentValue)) {
						sFilterWrk = "[CurrencyCode]" + ew_SearchString("=", Convert.ToString(currencycode.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT [CurrencyCode], [CurrencyCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Currency]";
						sWhereWrk = "";
						currencycode.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(currencycode, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrencyCode] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							currencycode.ViewValue = currencycode.DisplayValue(odwrk);
						} else {
							currencycode.ViewValue = currencycode.CurrentValue;
						}
					} else {
						currencycode.ViewValue = System.DBNull.Value;
					}

					// agentid
					if (ew_NotEmpty(agentid.VirtualValue)) {
						agentid.ViewValue = agentid.VirtualValue;
					} else {
						agentid.ViewValue = agentid.CurrentValue;
					if (ew_NotEmpty(agentid.CurrentValue)) {
						sFilterWrk = "[AgentId]" + ew_SearchString("=", Convert.ToString(agentid.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT DISTINCT [AgentId], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Agent]";
						sWhereWrk = "";
						agentid.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentId]"}, {"dx2", "[AgentName]"}};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(agentid, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							odwrk[2] = Convert.ToString(odwrk[2]);
							agentid.ViewValue = agentid.DisplayValue(odwrk);
						} else {
							agentid.ViewValue = agentid.CurrentValue;
						}
					} else {
						agentid.ViewValue = System.DBNull.Value;
					}
					}

					// BankId
					BankId.ViewValue = BankId.CurrentValue;

					// lastavgcost
					lastavgcost.ViewValue = lastavgcost.CurrentValue;

					// id
					id.LinkCustomAttributes = id.FldTagACustomAttributes; // DN
					id.HrefValue = "";
					id.TooltipValue = "";

					// currencycode
					currencycode.LinkCustomAttributes = currencycode.FldTagACustomAttributes; // DN
					currencycode.HrefValue = "";
					currencycode.TooltipValue = "";

					// agentid
					agentid.LinkCustomAttributes = agentid.FldTagACustomAttributes; // DN
					agentid.HrefValue = "";
					agentid.TooltipValue = "";

					// BankId
					BankId.LinkCustomAttributes = BankId.FldTagACustomAttributes; // DN
					BankId.HrefValue = "";
					BankId.TooltipValue = "";

					// lastavgcost
					lastavgcost.LinkCustomAttributes = lastavgcost.FldTagACustomAttributes; // DN
					lastavgcost.HrefValue = "";
					lastavgcost.TooltipValue = "";
				} else if (RowType == EW_ROWTYPE_ADD) { // Add row

					// id
					// currencycode

					currencycode.EditAttrs["class"] = "form-control";
						if (ew_Empty(currencycode.CurrentValue)) {
							sFilterWrk = "0=1";
						} else {
							sFilterWrk = "[CurrencyCode]" + ew_SearchString("=", Convert.ToString(currencycode.CurrentValue), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT [CurrencyCode], [CurrencyCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[Currency]";
						sWhereWrk = "";
						currencycode.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(currencycode, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrencyCode] ASC/*EndOrderBy*/";
					rswrk = ew_GetConn("").GetRows(sSqlWrk);
					currencycode.EditValue = rswrk;

					// agentid
					agentid.EditAttrs["class"] = "form-control";
					if (ew_NotEmpty(agentid.SessionValue)) {
						agentid.CurrentValue = agentid.SessionValue;
						agentid.OldValue = agentid.CurrentValue;
					if (ew_NotEmpty(agentid.VirtualValue)) {
						agentid.ViewValue = agentid.VirtualValue;
					} else {
						agentid.ViewValue = agentid.CurrentValue;
					if (ew_NotEmpty(agentid.CurrentValue)) {
						sFilterWrk = "[AgentId]" + ew_SearchString("=", Convert.ToString(agentid.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT DISTINCT [AgentId], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Agent]";
						sWhereWrk = "";
						agentid.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentId]"}, {"dx2", "[AgentName]"}};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(agentid, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							odwrk[2] = Convert.ToString(odwrk[2]);
							agentid.ViewValue = agentid.DisplayValue(odwrk);
						} else {
							agentid.ViewValue = agentid.CurrentValue;
						}
					} else {
						agentid.ViewValue = System.DBNull.Value;
					}
					}
					} else {
					agentid.EditValue = agentid.CurrentValue; // DN
					agentid.PlaceHolder = ew_RemoveHtml(agentid.FldCaption);
					}

					// BankId
					BankId.EditAttrs["class"] = "form-control";
					BankId.EditValue = BankId.CurrentValue; // DN
					BankId.PlaceHolder = ew_RemoveHtml(BankId.FldCaption);

					// lastavgcost
					lastavgcost.EditAttrs["class"] = "form-control";
					lastavgcost.EditValue = lastavgcost.CurrentValue; // DN
					lastavgcost.PlaceHolder = ew_RemoveHtml(lastavgcost.FldCaption);
					if (ew_NotEmpty(lastavgcost.EditValue) && ew_IsNumeric(Convert.ToString(lastavgcost.EditValue))) {
					lastavgcost.EditValue = ew_FormatNumber(lastavgcost.EditValue, -2, -1, -2, 0);
					lastavgcost.OldValue = lastavgcost.EditValue;
					}

					// Add refer script
					// id

					id.LinkCustomAttributes = id.FldTagACustomAttributes; // DN
					id.HrefValue = "";

					// currencycode
					currencycode.LinkCustomAttributes = currencycode.FldTagACustomAttributes; // DN
					currencycode.HrefValue = "";

					// agentid
					agentid.LinkCustomAttributes = agentid.FldTagACustomAttributes; // DN
					agentid.HrefValue = "";

					// BankId
					BankId.LinkCustomAttributes = BankId.FldTagACustomAttributes; // DN
					BankId.HrefValue = "";

					// lastavgcost
					lastavgcost.LinkCustomAttributes = lastavgcost.FldTagACustomAttributes; // DN
					lastavgcost.HrefValue = "";
				} else if (RowType == EW_ROWTYPE_EDIT) { // Edit row

					// id
					id.EditAttrs["class"] = "form-control";
					id.EditValue = id.CurrentValue;

					// currencycode
					currencycode.EditAttrs["class"] = "form-control";
						if (ew_Empty(currencycode.CurrentValue)) {
							sFilterWrk = "0=1";
						} else {
							sFilterWrk = "[CurrencyCode]" + ew_SearchString("=", Convert.ToString(currencycode.CurrentValue), EW_DATATYPE_STRING, "");
						}
						sSqlWrk = "SELECT [CurrencyCode], [CurrencyCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld], '' AS [SelectFilterFld], '' AS [SelectFilterFld2], '' AS [SelectFilterFld3], '' AS [SelectFilterFld4] FROM [dbo].[Currency]";
						sWhereWrk = "";
						currencycode.LookupFilters = new Dictionary<string, string>() {};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(currencycode, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrencyCode] ASC/*EndOrderBy*/";
					rswrk = ew_GetConn("").GetRows(sSqlWrk);
					currencycode.EditValue = rswrk;

					// agentid
					agentid.EditAttrs["class"] = "form-control";
					if (ew_NotEmpty(agentid.SessionValue)) {
						agentid.CurrentValue = agentid.SessionValue;
						agentid.OldValue = agentid.CurrentValue;
					if (ew_NotEmpty(agentid.VirtualValue)) {
						agentid.ViewValue = agentid.VirtualValue;
					} else {
						agentid.ViewValue = agentid.CurrentValue;
					if (ew_NotEmpty(agentid.CurrentValue)) {
						sFilterWrk = "[AgentId]" + ew_SearchString("=", Convert.ToString(agentid.CurrentValue).Trim(), EW_DATATYPE_STRING, "");
						sSqlWrk = "SELECT DISTINCT [AgentId], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Agent]";
						sWhereWrk = "";
						agentid.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentId]"}, {"dx2", "[AgentName]"}};
						if (ew_NotEmpty(sFilterWrk)) {
							sWhereWrk = ew_AddFilter(sWhereWrk, sFilterWrk);
						}
					Lookup_Selecting(agentid, ref sWhereWrk);
						if (sWhereWrk != "") {
							sSqlWrk += " WHERE " + sWhereWrk;
						}
					sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						rswrk = ew_GetConn("").GetRows(sSqlWrk);
						if (rswrk != null && rswrk.Count > 0) { // Lookup values found
							odwrk = rswrk[0];
							odwrk[1] = Convert.ToString(odwrk[1]);
							odwrk[2] = Convert.ToString(odwrk[2]);
							agentid.ViewValue = agentid.DisplayValue(odwrk);
						} else {
							agentid.ViewValue = agentid.CurrentValue;
						}
					} else {
						agentid.ViewValue = System.DBNull.Value;
					}
					}
					} else {
					agentid.EditValue = agentid.CurrentValue; // DN
					agentid.PlaceHolder = ew_RemoveHtml(agentid.FldCaption);
					}

					// BankId
					BankId.EditAttrs["class"] = "form-control";
					BankId.EditValue = BankId.CurrentValue; // DN
					BankId.PlaceHolder = ew_RemoveHtml(BankId.FldCaption);

					// lastavgcost
					lastavgcost.EditAttrs["class"] = "form-control";
					lastavgcost.EditValue = lastavgcost.CurrentValue; // DN
					lastavgcost.PlaceHolder = ew_RemoveHtml(lastavgcost.FldCaption);
					if (ew_NotEmpty(lastavgcost.EditValue) && ew_IsNumeric(Convert.ToString(lastavgcost.EditValue))) {
					lastavgcost.EditValue = ew_FormatNumber(lastavgcost.EditValue, -2, -1, -2, 0);
					lastavgcost.OldValue = lastavgcost.EditValue;
					}

					// Edit refer script
					// id

					id.LinkCustomAttributes = id.FldTagACustomAttributes; // DN
					id.HrefValue = "";

					// currencycode
					currencycode.LinkCustomAttributes = currencycode.FldTagACustomAttributes; // DN
					currencycode.HrefValue = "";

					// agentid
					agentid.LinkCustomAttributes = agentid.FldTagACustomAttributes; // DN
					agentid.HrefValue = "";

					// BankId
					BankId.LinkCustomAttributes = BankId.FldTagACustomAttributes; // DN
					BankId.HrefValue = "";

					// lastavgcost
					lastavgcost.LinkCustomAttributes = lastavgcost.FldTagACustomAttributes; // DN
					lastavgcost.HrefValue = "";
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

				// Check if validation required
				if (!EW_SERVER_VALIDATE)
					return (gsFormError == "");
				if (!currencycode.FldIsDetailKey && ew_Empty(currencycode.FormValue))
					gsFormError = ew_AddMessage(gsFormError, currencycode.ReqErrMsg.Replace("%s", currencycode.FldCaption));
				if (!agentid.FldIsDetailKey && ew_Empty(agentid.FormValue))
					gsFormError = ew_AddMessage(gsFormError, agentid.ReqErrMsg.Replace("%s", agentid.FldCaption));
				if (!ew_CheckNumber(lastavgcost.FormValue))
					gsFormError = ew_AddMessage(gsFormError, lastavgcost.FldErrMsg);

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
				AgentBank = AgentBank ?? new cAgentBank();
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

				// id
				// currencycode

				currencycode.SetDbValue(ref rsnew, currencycode.CurrentValue, "", currencycode.ReadOnly);

				// agentid
				agentid.SetDbValue(ref rsnew, agentid.CurrentValue, System.DBNull.Value, agentid.ReadOnly);

				// BankId
				BankId.SetDbValue(ref rsnew, BankId.CurrentValue, System.DBNull.Value, BankId.ReadOnly);

				// lastavgcost
				lastavgcost.SetDbValue(ref rsnew, lastavgcost.CurrentValue, 0, lastavgcost.ReadOnly);
				bool bValidMasterRecord;
				object KeyValue;
				string sMasterFilter;

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
				string sMasterFilter = "";

				// Set up foreign key field value from Session
				if (CurrentMasterTable == "Agent") {
					agentid.CurrentValue = agentid.SessionValue;
				}
				bool bValidMasterRecord;

				// Load db values from rsold
				if (rsold != null) {
					LoadDbValues(rsold);
				}
				try {

					// id
					// currencycode

					currencycode.SetDbValue(ref rsnew, currencycode.CurrentValue, "", false);

					// agentid
					agentid.SetDbValue(ref rsnew, agentid.CurrentValue, System.DBNull.Value, false);

					// BankId
					BankId.SetDbValue(ref rsnew, BankId.CurrentValue, System.DBNull.Value, false);

					// lastavgcost
					lastavgcost.SetDbValue(ref rsnew, lastavgcost.CurrentValue, 0, ew_Empty(lastavgcost.CurrentValue));
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

			// Set up master/detail based on QueryString
			public void SetUpMasterParms() {

				// Hide foreign keys
				string sMasterTblVar = CurrentMasterTable;
				if (sMasterTblVar == "Agent") {
					agentid.Visible = false;

					//if (Agent.EventCancelled) EventCancelled = true;
					if (ew_ConvertToBool(ew_Get("mastereventcancelled")))
						EventCancelled = true;
				}
				DbMasterFilter = MasterFilter; //  Get master filter
				DbDetailFilter = DetailFilter; // Get detail filter
			}
			#pragma warning disable 168, 1522

			// Setup lookup filters of a field
			public override void SetupLookupFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
					case "x_currencycode":
						sSqlWrk = "";
							sSqlWrk = "SELECT [CurrencyCode] AS [LinkFld], [CurrencyCode] AS [DispFld], '' AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Currency]";
							sWhereWrk = "";
							currencycode.LookupFilters = new Dictionary<string, string>() {};
						currencycode.LookupFilters.Add("s", sSqlWrk);
						currencycode.LookupFilters.Add("d", "");
						currencycode.LookupFilters.Add("f0", "[CurrencyCode] = {filter_value}");
						currencycode.LookupFilters.Add("t0", "200");
						currencycode.LookupFilters.Add("fn0", "");
						sSqlWrk = "";
						Lookup_Selecting(currencycode, ref sWhereWrk);
							if (sWhereWrk != "") {
								sSqlWrk += " WHERE " + sWhereWrk;
							}
						sSqlWrk += " /*BeginOrderBy*/ORDER BY [CurrencyCode] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							currencycode.LookupFilters["s"] += sSqlWrk;
						break;
					case "x_agentid":
						sSqlWrk = "";
							sSqlWrk = "SELECT DISTINCT [AgentId] AS [LinkFld], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld], '' AS [Disp3Fld], '' AS [Disp4Fld] FROM [dbo].[Agent]";
							sWhereWrk = "{filter}";
							agentid.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentId]"}, {"dx2", "[AgentName]"}};
						agentid.LookupFilters.Add("s", sSqlWrk);
						agentid.LookupFilters.Add("d", "");
						agentid.LookupFilters.Add("f0", "[AgentId] = {filter_value}");
						agentid.LookupFilters.Add("t0", "200");
						agentid.LookupFilters.Add("fn0", "");
						sSqlWrk = "";
						Lookup_Selecting(agentid, ref sWhereWrk);
							if (sWhereWrk != "") {
								sSqlWrk += " WHERE " + sWhereWrk;
							}
						sSqlWrk += " /*BeginOrderBy*/ORDER BY [AgentName] ASC/*EndOrderBy*/";
						if (sSqlWrk != "")
							agentid.LookupFilters["s"] += sSqlWrk;
						break;
				}
			}

			// Setup AutoSuggest filters of a field
			public override void SetupAutoSuggestFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
					case "x_agentid":
						sSqlWrk = "";
						sSqlWrk = "SELECT DISTINCT  TOP " + EW_AUTO_SUGGEST_MAX_ENTRIES + " [AgentId], [AgentId] AS [DispFld], [AgentName] AS [Disp2Fld] FROM [dbo].[Agent]";
						sWhereWrk = "" + "[AgentId]" + " LIKE '{query_value}%' OR " + "[AgentId]" + " + '" + ew_ValueSeparator(1, AgentBank.agentid) + "' + " + "[AgentName]" + " LIKE '{query_value}%'";
						agentid.LookupFilters = new Dictionary<string, string>() {{"dx1", "[AgentId]"}, {"dx2", "[AgentName]"}};
						fld.LookupFilters = new Dictionary<string, string>() {{"s", sSqlWrk}, {"d", ""}}; // DN
						sSqlWrk = "";
					Lookup_Selecting(agentid, ref sWhereWrk);
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
