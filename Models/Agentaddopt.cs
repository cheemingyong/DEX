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

		// Agent_addopt
		public static cAgent_addopt Agent_addopt {
			get { return (cAgent_addopt)ew_ViewData["Agent_addopt"]; }
			set { ew_ViewData["Agent_addopt"] = value; }
		}

		//
		// Page class for Agent
		//

		public class cAgent_addopt : cAgent_addopt_base
		{

			// Construtor
			public cAgent_addopt(Controller controller = null) : base(controller) {
			}
		}

		//
		// Page base class
		//

		public class cAgent_addopt_base : cAgent, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "addopt";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Table name
			public new string TableName = "Agent";

			// Page object name
			public string PageObjName = "Agent_addopt";

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

			public cAgent_addopt_base(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Table object (Agent)
				if (Agent == null || Agent is cAgent)
					Agent = this;

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

			//
			// Page main
			//

			public IActionResult Page_Main() {

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Process form if post back
				if (IsPost) {
					CurrentAction = ObjForm.GetValue("a_addopt"); // Get form action
					LoadFormValues(); // Load form values

					// Validate form
					if (!ValidateForm()) {
						CurrentAction = "I"; // Form error, reset action
						FailureMessage = gsFormError;
					}
				} else { // Not post back
					CurrentAction = "I"; // Display blank record
					LoadDefaultValues(); // Load default values
				}

				// Perform action based on action code
				switch (CurrentAction) {
					case "I": // Blank record, no action required
						break;
					case "A": // Add new record
						SendEmail = true; // Send email on add success
						if (AddRow()) { // Add successful
							dynamic row = new ExpandoObject();
							row.x_AgentId = AgentId.DbValue;
							row.x_AgentName = AgentName.DbValue;
							row.x_AgentRiskRating = AgentRiskRating.DbValue;
							row.x_AgentRiskCredit = AgentRiskCredit.DbValue;
							row.x_Address1 = Address1.DbValue;
							row.x_Address2 = Address2.DbValue;
							row.x_Address3 = Address3.DbValue;
							row.x_Country = Country.DbValue;
							row.x_ZipCode = ZipCode.DbValue;
							row.x_Fax = Fax.DbValue;
							row.x_Phone = Phone.DbValue;
							row.x_Mobile = Mobile.DbValue;
							row.x_BuzType = BuzType.DbValue;
							row.x_ClassType = ClassType.DbValue;
							row.x_DefContactPName = DefContactPName.DbValue;
							row.x_DefContactPNric = DefContactPNric.DbValue;
							row.x_DefContactPNation = DefContactPNation.DbValue;
							row.x_DefContactPOccupation = DefContactPOccupation.DbValue;
							row.x_TermsId = TermsId.DbValue;
							row.x_LedgerBal = LedgerBal.DbValue;
							row.x_AvailableBal = AvailableBal.DbValue;
							row.x__Email = _Email.DbValue;
							row.x_URL = URL.DbValue;
							row.x_CustType = CustType.DbValue;
							row.x_RemittanceLicNO = RemittanceLicNO.DbValue;
							row.x_MCLicNo = MCLicNo.DbValue;
							row.x_BankYesNo = BankYesNo.DbValue;
							row.x_BankODLimit = BankODLimit.DbValue;
							row.x_BankAcctNO = BankAcctNO.DbValue;
							row.x_CreditLimit = CreditLimit.DbValue;
							row.x_ReferBy = ReferBy.DbValue;
							row.x_AgentImageName = AgentImageName.DbValue;
							row.x_status = status.DbValue;
							row.x_CreatedBy = CreatedBy.DbValue;
							row.x_CreatedDate = CreatedDate.DbValue;
							row.x_ModifiedUser = ModifiedUser.DbValue;
							row.x_ModifiedDate = ModifiedDate.DbValue;
							row.x_PPExpiryDate = PPExpiryDate.DbValue;
							row.x_TTExpiryDate = TTExpiryDate.DbValue;
							row.x_MCExpiryDate = MCExpiryDate.DbValue;
							row.x_Action = Action.DbValue;
							row.x_Remark = Remark.DbValue;
							row.x_MCType = MCType.DbValue;
							row.x_CustDOB = CustDOB.DbValue;
							row.x_DefContactDOB = DefContactDOB.DbValue;
							row.x_ScanImage = ScanImage.DbValue;
							row.x_BizNature = BizNature.DbValue;
							row.x_DefContactPOB = DefContactPOB.DbValue;
							row.x_NewTran = NewTran.DbValue;
							row.x_BizRegNo = BizRegNo.DbValue;
							row.x_BizRegDate = BizRegDate.DbValue;
							row.x_BizRegPlace = BizRegPlace.DbValue;
							row.x_BizRegExpDate = BizRegExpDate.DbValue;
							row.x_UnIncorpExec = UnIncorpExec.DbValue;
							row.x_DefContactAuthorzLetter = DefContactAuthorzLetter.DbValue;
							row.x_Politician = Politician.DbValue;
							row.x_BizPartnerNo = BizPartnerNo.DbValue;
							row.x_Remark2 = Remark2.DbValue;
							row.x_BannedListRemark = BannedListRemark.DbValue;
							if (!EW_DEBUG_ENABLED)
								ew_Response.Clear();
							 ew_ResponseWrite(ew_ArrayToJson(new List<dynamic>() { row }), "utf-8"); // Returns utf-8 data
						} else {
							ew_ResponseWrite(ShowMessage(false), "utf-8"); // DN
						}
						return Page_Terminate();
				}

				// Render row
				RowType = EW_ROWTYPE_ADD; // Render add type
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
				AgentId.CurrentValue = System.DBNull.Value;
				AgentId.OldValue = AgentId.CurrentValue;
				AgentName.CurrentValue = System.DBNull.Value;
				AgentName.OldValue = AgentName.CurrentValue;
				AgentRiskRating.CurrentValue = AgentRiskRating.FldDefault;
				AgentRiskCredit.CurrentValue = AgentRiskCredit.FldDefault;
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
				ReferBy.CurrentValue = System.DBNull.Value;
				ReferBy.OldValue = ReferBy.CurrentValue;
				AgentImageName.CurrentValue = System.DBNull.Value;
				AgentImageName.OldValue = AgentImageName.CurrentValue;
				status.CurrentValue = status.FldDefault;
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
				BizRegNo.CurrentValue = System.DBNull.Value;
				BizRegNo.OldValue = BizRegNo.CurrentValue;
				BizRegDate.CurrentValue = System.DBNull.Value;
				BizRegDate.OldValue = BizRegDate.CurrentValue;
				BizRegPlace.CurrentValue = System.DBNull.Value;
				BizRegPlace.OldValue = BizRegPlace.CurrentValue;
				BizRegExpDate.CurrentValue = System.DBNull.Value;
				BizRegExpDate.OldValue = BizRegExpDate.CurrentValue;
				UnIncorpExec.CurrentValue = UnIncorpExec.FldDefault;
				DefContactAuthorzLetter.CurrentValue = DefContactAuthorzLetter.FldDefault;
				Politician.CurrentValue = Politician.FldDefault;
				BizPartnerNo.CurrentValue = BizPartnerNo.FldDefault;
				Remark2.CurrentValue = System.DBNull.Value;
				Remark2.OldValue = Remark2.CurrentValue;
				BannedListRemark.CurrentValue = System.DBNull.Value;
				BannedListRemark.OldValue = BannedListRemark.CurrentValue;
			}

			// Load form values
			public void LoadFormValues() {
				if (!AgentId.FldIsDetailKey) {
					AgentId.FormValue = ObjForm.GetValue("x_AgentId");
				}
				if (!AgentName.FldIsDetailKey) {
					AgentName.FormValue = ObjForm.GetValue("x_AgentName");
				}
				if (!AgentRiskRating.FldIsDetailKey) {
					AgentRiskRating.FormValue = ObjForm.GetValue("x_AgentRiskRating");
				}
				if (!AgentRiskCredit.FldIsDetailKey) {
					AgentRiskCredit.FormValue = ObjForm.GetValue("x_AgentRiskCredit");
				}
				if (!Address1.FldIsDetailKey) {
					Address1.FormValue = ObjForm.GetValue("x_Address1");
				}
				if (!Address2.FldIsDetailKey) {
					Address2.FormValue = ObjForm.GetValue("x_Address2");
				}
				if (!Address3.FldIsDetailKey) {
					Address3.FormValue = ObjForm.GetValue("x_Address3");
				}
				if (!Country.FldIsDetailKey) {
					Country.FormValue = ObjForm.GetValue("x_Country");
				}
				if (!ZipCode.FldIsDetailKey) {
					ZipCode.FormValue = ObjForm.GetValue("x_ZipCode");
				}
				if (!Fax.FldIsDetailKey) {
					Fax.FormValue = ObjForm.GetValue("x_Fax");
				}
				if (!Phone.FldIsDetailKey) {
					Phone.FormValue = ObjForm.GetValue("x_Phone");
				}
				if (!Mobile.FldIsDetailKey) {
					Mobile.FormValue = ObjForm.GetValue("x_Mobile");
				}
				if (!BuzType.FldIsDetailKey) {
					BuzType.FormValue = ObjForm.GetValue("x_BuzType");
				}
				if (!ClassType.FldIsDetailKey) {
					ClassType.FormValue = ObjForm.GetValue("x_ClassType");
				}
				if (!DefContactPName.FldIsDetailKey) {
					DefContactPName.FormValue = ObjForm.GetValue("x_DefContactPName");
				}
				if (!DefContactPNric.FldIsDetailKey) {
					DefContactPNric.FormValue = ObjForm.GetValue("x_DefContactPNric");
				}
				if (!DefContactPNation.FldIsDetailKey) {
					DefContactPNation.FormValue = ObjForm.GetValue("x_DefContactPNation");
				}
				if (!DefContactPOccupation.FldIsDetailKey) {
					DefContactPOccupation.FormValue = ObjForm.GetValue("x_DefContactPOccupation");
				}
				if (!TermsId.FldIsDetailKey) {
					TermsId.FormValue = ObjForm.GetValue("x_TermsId");
				}
				if (!LedgerBal.FldIsDetailKey) {
					LedgerBal.FormValue = ObjForm.GetValue("x_LedgerBal");
				}
				if (!AvailableBal.FldIsDetailKey) {
					AvailableBal.FormValue = ObjForm.GetValue("x_AvailableBal");
				}
				if (!_Email.FldIsDetailKey) {
					_Email.FormValue = ObjForm.GetValue("x__Email");
				}
				if (!URL.FldIsDetailKey) {
					URL.FormValue = ObjForm.GetValue("x_URL");
				}
				if (!CustType.FldIsDetailKey) {
					CustType.FormValue = ObjForm.GetValue("x_CustType");
				}
				if (!RemittanceLicNO.FldIsDetailKey) {
					RemittanceLicNO.FormValue = ObjForm.GetValue("x_RemittanceLicNO");
				}
				if (!MCLicNo.FldIsDetailKey) {
					MCLicNo.FormValue = ObjForm.GetValue("x_MCLicNo");
				}
				if (!BankYesNo.FldIsDetailKey) {
					BankYesNo.FormValue = ObjForm.GetValue("x_BankYesNo");
				}
				if (!BankODLimit.FldIsDetailKey) {
					BankODLimit.FormValue = ObjForm.GetValue("x_BankODLimit");
				}
				if (!BankAcctNO.FldIsDetailKey) {
					BankAcctNO.FormValue = ObjForm.GetValue("x_BankAcctNO");
				}
				if (!CreditLimit.FldIsDetailKey) {
					CreditLimit.FormValue = ObjForm.GetValue("x_CreditLimit");
				}
				if (!ReferBy.FldIsDetailKey) {
					ReferBy.FormValue = ObjForm.GetValue("x_ReferBy");
				}
				if (!AgentImageName.FldIsDetailKey) {
					AgentImageName.FormValue = ObjForm.GetValue("x_AgentImageName");
				}
				if (!status.FldIsDetailKey) {
					status.FormValue = ObjForm.GetValue("x_status");
				}
				if (!CreatedBy.FldIsDetailKey) {
					CreatedBy.FormValue = ObjForm.GetValue("x_CreatedBy");
				}
				if (!CreatedDate.FldIsDetailKey) {
					CreatedDate.FormValue = ObjForm.GetValue("x_CreatedDate");
					CreatedDate.CurrentValue = ew_UnformatDateTime(CreatedDate.CurrentValue, 0);
				}
				if (!ModifiedUser.FldIsDetailKey) {
					ModifiedUser.FormValue = ObjForm.GetValue("x_ModifiedUser");
				}
				if (!ModifiedDate.FldIsDetailKey) {
					ModifiedDate.FormValue = ObjForm.GetValue("x_ModifiedDate");
					ModifiedDate.CurrentValue = ew_UnformatDateTime(ModifiedDate.CurrentValue, 0);
				}
				if (!PPExpiryDate.FldIsDetailKey) {
					PPExpiryDate.FormValue = ObjForm.GetValue("x_PPExpiryDate");
					PPExpiryDate.CurrentValue = ew_UnformatDateTime(PPExpiryDate.CurrentValue, 0);
				}
				if (!TTExpiryDate.FldIsDetailKey) {
					TTExpiryDate.FormValue = ObjForm.GetValue("x_TTExpiryDate");
					TTExpiryDate.CurrentValue = ew_UnformatDateTime(TTExpiryDate.CurrentValue, 0);
				}
				if (!MCExpiryDate.FldIsDetailKey) {
					MCExpiryDate.FormValue = ObjForm.GetValue("x_MCExpiryDate");
					MCExpiryDate.CurrentValue = ew_UnformatDateTime(MCExpiryDate.CurrentValue, 0);
				}
				if (!Action.FldIsDetailKey) {
					Action.FormValue = ObjForm.GetValue("x_Action");
				}
				if (!Remark.FldIsDetailKey) {
					Remark.FormValue = ObjForm.GetValue("x_Remark");
				}
				if (!MCType.FldIsDetailKey) {
					MCType.FormValue = ObjForm.GetValue("x_MCType");
				}
				if (!CustDOB.FldIsDetailKey) {
					CustDOB.FormValue = ObjForm.GetValue("x_CustDOB");
					CustDOB.CurrentValue = ew_UnformatDateTime(CustDOB.CurrentValue, 0);
				}
				if (!DefContactDOB.FldIsDetailKey) {
					DefContactDOB.FormValue = ObjForm.GetValue("x_DefContactDOB");
					DefContactDOB.CurrentValue = ew_UnformatDateTime(DefContactDOB.CurrentValue, 0);
				}
				if (!ScanImage.FldIsDetailKey) {
					ScanImage.FormValue = ObjForm.GetValue("x_ScanImage");
				}
				if (!BizNature.FldIsDetailKey) {
					BizNature.FormValue = ObjForm.GetValue("x_BizNature");
				}
				if (!DefContactPOB.FldIsDetailKey) {
					DefContactPOB.FormValue = ObjForm.GetValue("x_DefContactPOB");
				}
				if (!NewTran.FldIsDetailKey) {
					NewTran.FormValue = ObjForm.GetValue("x_NewTran");
				}
				if (!BizRegNo.FldIsDetailKey) {
					BizRegNo.FormValue = ObjForm.GetValue("x_BizRegNo");
				}
				if (!BizRegDate.FldIsDetailKey) {
					BizRegDate.FormValue = ObjForm.GetValue("x_BizRegDate");
					BizRegDate.CurrentValue = ew_UnformatDateTime(BizRegDate.CurrentValue, 0);
				}
				if (!BizRegPlace.FldIsDetailKey) {
					BizRegPlace.FormValue = ObjForm.GetValue("x_BizRegPlace");
				}
				if (!BizRegExpDate.FldIsDetailKey) {
					BizRegExpDate.FormValue = ObjForm.GetValue("x_BizRegExpDate");
					BizRegExpDate.CurrentValue = ew_UnformatDateTime(BizRegExpDate.CurrentValue, 0);
				}
				if (!UnIncorpExec.FldIsDetailKey) {
					UnIncorpExec.FormValue = ObjForm.GetValue("x_UnIncorpExec");
				}
				if (!DefContactAuthorzLetter.FldIsDetailKey) {
					DefContactAuthorzLetter.FormValue = ObjForm.GetValue("x_DefContactAuthorzLetter");
				}
				if (!Politician.FldIsDetailKey) {
					Politician.FormValue = ObjForm.GetValue("x_Politician");
				}
				if (!BizPartnerNo.FldIsDetailKey) {
					BizPartnerNo.FormValue = ObjForm.GetValue("x_BizPartnerNo");
				}
				if (!Remark2.FldIsDetailKey) {
					Remark2.FormValue = ObjForm.GetValue("x_Remark2");
				}
				if (!BannedListRemark.FldIsDetailKey) {
					BannedListRemark.FormValue = ObjForm.GetValue("x_BannedListRemark");
				}
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
					if (ew_NotEmpty(AgentRiskCredit.EditValue) && ew_IsNumeric(Convert.ToString(AgentRiskCredit.EditValue))) AgentRiskCredit.EditValue = ew_FormatNumber(AgentRiskCredit.EditValue, -2, -1, -2, 0);

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
					if (ew_NotEmpty(LedgerBal.EditValue) && ew_IsNumeric(Convert.ToString(LedgerBal.EditValue))) LedgerBal.EditValue = ew_FormatNumber(LedgerBal.EditValue, -2, -1, -2, 0);

					// AvailableBal
					AvailableBal.EditAttrs["class"] = "form-control";
					AvailableBal.EditValue = AvailableBal.CurrentValue; // DN
					AvailableBal.PlaceHolder = ew_RemoveHtml(AvailableBal.FldCaption);
					if (ew_NotEmpty(AvailableBal.EditValue) && ew_IsNumeric(Convert.ToString(AvailableBal.EditValue))) AvailableBal.EditValue = ew_FormatNumber(AvailableBal.EditValue, -2, -1, -2, 0);

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
					if (ew_NotEmpty(BankODLimit.EditValue) && ew_IsNumeric(Convert.ToString(BankODLimit.EditValue))) BankODLimit.EditValue = ew_FormatNumber(BankODLimit.EditValue, -2, -1, -2, 0);

					// BankAcctNO
					BankAcctNO.EditAttrs["class"] = "form-control";
					BankAcctNO.EditValue = BankAcctNO.CurrentValue; // DN
					BankAcctNO.PlaceHolder = ew_RemoveHtml(BankAcctNO.FldCaption);

					// CreditLimit
					CreditLimit.EditAttrs["class"] = "form-control";
					CreditLimit.EditValue = CreditLimit.CurrentValue; // DN
					CreditLimit.PlaceHolder = ew_RemoveHtml(CreditLimit.FldCaption);
					if (ew_NotEmpty(CreditLimit.EditValue) && ew_IsNumeric(Convert.ToString(CreditLimit.EditValue))) CreditLimit.EditValue = ew_FormatNumber(CreditLimit.EditValue, -2, -1, -2, 0);

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

			// Set up Breadcrumb
			public void SetupBreadcrumb() {
				Breadcrumb = new cBreadcrumb();
				var url = ew_CurrentUrl();
				Breadcrumb.Add("list", TableVar, ew_AppPath(AddMasterUrl("Agentlist")), "", TableVar, true);
				var PageId = "addopt";
				Breadcrumb.Add("addopt", PageId, url);
			}
			#pragma warning disable 168, 1522

			// Setup lookup filters of a field
			public override void SetupLookupFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
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

			// Setup AutoSuggest filters of a field
			public override void SetupAutoSuggestFilters(cField fld, string pageId = null) {
				string sSqlWrk, sWhereWrk;
				pageId = pageId ?? PageID;
				switch (fld.FldVar) {
				}
			}
			#pragma warning restore 168, 1522

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
