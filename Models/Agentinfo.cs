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

// Models (Table)
namespace AspNetMaker2017.Models {
	public partial class DEX {

		// Agent
		public static cAgent Agent {
			get { return (cAgent)ew_ViewData["Agent"]; }
			set { ew_ViewData["Agent"] = value; }
		}

		//
		// Table class for Agent
		//

		public class cAgent: cTable {
			public cField AgentId;
			public cField AgentName;
			public cField AgentRiskRating;
			public cField AgentRiskCredit;
			public cField Address1;
			public cField Address2;
			public cField Address3;
			public cField Country;
			public cField ZipCode;
			public cField Fax;
			public cField Phone;
			public cField Mobile;
			public cField BuzType;
			public cField ClassType;
			public cField DefContactPName;
			public cField DefContactPNric;
			public cField DefContactPNation;
			public cField DefContactPOccupation;
			public cField TermsId;
			public cField LedgerBal;
			public cField AvailableBal;
			public cField _Email;
			public cField URL;
			public cField CustType;
			public cField RemittanceLicNO;
			public cField MCLicNo;
			public cField BankYesNo;
			public cField BankODLimit;
			public cField BankAcctNO;
			public cField CreditLimit;
			public cField ReferBy;
			public cField AgentImageName;
			public cField status;
			public cField CreatedBy;
			public cField CreatedDate;
			public cField ModifiedUser;
			public cField ModifiedDate;
			public cField PPExpiryDate;
			public cField TTExpiryDate;
			public cField MCExpiryDate;
			public cField Action;
			public cField Remark;
			public cField MCType;
			public cField CustDOB;
			public cField DefContactDOB;
			public cField ScanImage;
			public cField BizNature;
			public cField DefContactPOB;
			public cField NewTran;
			public cField BizRegNo;
			public cField BizRegDate;
			public cField BizRegPlace;
			public cField BizRegExpDate;
			public cField UnIncorpExec;
			public cField DefContactAuthorzLetter;
			public cField Politician;
			public cField BizPartnerNo;
			public cField Remark2;
			public cField BannedListRemark;
			public int RowCnt = 0; // DN

			//
			// Table class constructor
			//

			public cAgent() {

				// Language object // DN
				Language = Language ?? new cLanguage();
				TableVar = "Agent";
				TableName = "Agent";
				TableType = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[Agent]";
				DBID = "DB"; // DN
				ExportAll = true;
				ExportPageBreakCount = 0; // Page break per every n record (PDF only)
				ExportPageOrientation = "portrait"; // Page orientation (PDF only)
				ExportPageSize = "a4"; // Page size (PDF only)
				ExportColumnWidths = new float[] {  }; // Column widths (PDF only) // DN
				DetailAdd = false; // Allow detail add
				DetailEdit = false; // Allow detail edit
				DetailView = false; // Allow detail view
				ShowMultipleDetails = false; // Show multiple details
				GridAddRowCount = 5;
				AllowAddDeleteRow = ew_AllowAddDeleteRow(); // Allow add/delete row
				UserIDAllowSecurity = 0; // User ID Allow
				BasicSearch = new cBasicSearch(TableVar);

				// AgentId
				AgentId = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_AgentId",
					FldName = "AgentId",
					FldExpression = "[AgentId]",
					FldBasicSearchExpression = "[AgentId]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[AgentId]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				AgentId.Init();
				AgentId.SetupLookupFilters = SetupLookupFilters;
				AgentId.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("AgentId", AgentId);

				// AgentName
				AgentName = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_AgentName",
					FldName = "AgentName",
					FldExpression = "[AgentName]",
					FldBasicSearchExpression = "[AgentName]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[AgentName]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				AgentName.Init();
				AgentName.SetupLookupFilters = SetupLookupFilters;
				AgentName.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("AgentName", AgentName);

				// AgentRiskRating
				AgentRiskRating = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_AgentRiskRating",
					FldName = "AgentRiskRating",
					FldExpression = "[AgentRiskRating]",
					FldBasicSearchExpression = "CAST([AgentRiskRating] AS NVARCHAR)",
					FldType = 3,
					FldDbType = SqlDbType.Int,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[AgentRiskRating]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "SELECT",
					Sortable = true, // Allow sort
					UsePleaseSelect = true, // Use PleaseSelect by default
					PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
					OptionCount = 10,
					FldDefaultErrMsg = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				AgentRiskRating.Init();
				AgentRiskRating.FldDefault = 5;
				AgentRiskRating.SetupLookupFilters = SetupLookupFilters;
				AgentRiskRating.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("AgentRiskRating", AgentRiskRating);

				// AgentRiskCredit
				AgentRiskCredit = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_AgentRiskCredit",
					FldName = "AgentRiskCredit",
					FldExpression = "[AgentRiskCredit]",
					FldBasicSearchExpression = "CAST([AgentRiskCredit] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[AgentRiskCredit]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				AgentRiskCredit.Init();
				AgentRiskCredit.FldDefault = 0;
				AgentRiskCredit.SetupLookupFilters = SetupLookupFilters;
				AgentRiskCredit.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("AgentRiskCredit", AgentRiskCredit);

				// Address1
				Address1 = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Address1",
					FldName = "Address1",
					FldExpression = "[Address1]",
					FldBasicSearchExpression = "[Address1]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Address1]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Address1.Init();
				Address1.SetupLookupFilters = SetupLookupFilters;
				Address1.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Address1", Address1);

				// Address2
				Address2 = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Address2",
					FldName = "Address2",
					FldExpression = "[Address2]",
					FldBasicSearchExpression = "[Address2]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Address2]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Address2.Init();
				Address2.SetupLookupFilters = SetupLookupFilters;
				Address2.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Address2", Address2);

				// Address3
				Address3 = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Address3",
					FldName = "Address3",
					FldExpression = "[Address3]",
					FldBasicSearchExpression = "[Address3]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Address3]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Address3.Init();
				Address3.SetupLookupFilters = SetupLookupFilters;
				Address3.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Address3", Address3);

				// Country
				Country = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Country",
					FldName = "Country",
					FldExpression = "[Country]",
					FldBasicSearchExpression = "[Country]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Country]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "SELECT",
					Sortable = true, // Allow sort
					UsePleaseSelect = true, // Use PleaseSelect by default
					PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
					IsUpload = false
				};
				Country.Init();
				Country.SetupLookupFilters = SetupLookupFilters;
				Country.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Country", Country);

				// ZipCode
				ZipCode = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_ZipCode",
					FldName = "ZipCode",
					FldExpression = "[ZipCode]",
					FldBasicSearchExpression = "[ZipCode]",
					FldType = 129,
					FldDbType = SqlDbType.Char,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[ZipCode]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ZipCode.Init();
				ZipCode.SetupLookupFilters = SetupLookupFilters;
				ZipCode.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("ZipCode", ZipCode);

				// Fax
				Fax = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Fax",
					FldName = "Fax",
					FldExpression = "[Fax]",
					FldBasicSearchExpression = "[Fax]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Fax]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Fax.Init();
				Fax.SetupLookupFilters = SetupLookupFilters;
				Fax.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Fax", Fax);

				// Phone
				Phone = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Phone",
					FldName = "Phone",
					FldExpression = "[Phone]",
					FldBasicSearchExpression = "[Phone]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Phone]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Phone.Init();
				Phone.SetupLookupFilters = SetupLookupFilters;
				Phone.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Phone", Phone);

				// Mobile
				Mobile = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Mobile",
					FldName = "Mobile",
					FldExpression = "[Mobile]",
					FldBasicSearchExpression = "[Mobile]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Mobile]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Mobile.Init();
				Mobile.SetupLookupFilters = SetupLookupFilters;
				Mobile.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Mobile", Mobile);

				// BuzType
				BuzType = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BuzType",
					FldName = "BuzType",
					FldExpression = "[BuzType]",
					FldBasicSearchExpression = "[BuzType]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BuzType]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				BuzType.Init();
				BuzType.SetupLookupFilters = SetupLookupFilters;
				BuzType.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BuzType", BuzType);

				// ClassType
				ClassType = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_ClassType",
					FldName = "ClassType",
					FldExpression = "[ClassType]",
					FldBasicSearchExpression = "[ClassType]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[ClassType]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ClassType.Init();
				ClassType.SetupLookupFilters = SetupLookupFilters;
				ClassType.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("ClassType", ClassType);

				// DefContactPName
				DefContactPName = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_DefContactPName",
					FldName = "DefContactPName",
					FldExpression = "[DefContactPName]",
					FldBasicSearchExpression = "[DefContactPName]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[DefContactPName]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				DefContactPName.Init();
				DefContactPName.SetupLookupFilters = SetupLookupFilters;
				DefContactPName.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("DefContactPName", DefContactPName);

				// DefContactPNric
				DefContactPNric = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_DefContactPNric",
					FldName = "DefContactPNric",
					FldExpression = "[DefContactPNric]",
					FldBasicSearchExpression = "[DefContactPNric]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[DefContactPNric]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				DefContactPNric.Init();
				DefContactPNric.SetupLookupFilters = SetupLookupFilters;
				DefContactPNric.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("DefContactPNric", DefContactPNric);

				// DefContactPNation
				DefContactPNation = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_DefContactPNation",
					FldName = "DefContactPNation",
					FldExpression = "[DefContactPNation]",
					FldBasicSearchExpression = "[DefContactPNation]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[DefContactPNation]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				DefContactPNation.Init();
				DefContactPNation.SetupLookupFilters = SetupLookupFilters;
				DefContactPNation.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("DefContactPNation", DefContactPNation);

				// DefContactPOccupation
				DefContactPOccupation = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_DefContactPOccupation",
					FldName = "DefContactPOccupation",
					FldExpression = "[DefContactPOccupation]",
					FldBasicSearchExpression = "[DefContactPOccupation]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[DefContactPOccupation]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				DefContactPOccupation.Init();
				DefContactPOccupation.SetupLookupFilters = SetupLookupFilters;
				DefContactPOccupation.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("DefContactPOccupation", DefContactPOccupation);

				// TermsId
				TermsId = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_TermsId",
					FldName = "TermsId",
					FldExpression = "[TermsId]",
					FldBasicSearchExpression = "[TermsId]",
					FldType = 129,
					FldDbType = SqlDbType.Char,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[TermsId]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				TermsId.Init();
				TermsId.SetupLookupFilters = SetupLookupFilters;
				TermsId.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("TermsId", TermsId);

				// LedgerBal
				LedgerBal = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_LedgerBal",
					FldName = "LedgerBal",
					FldExpression = "[LedgerBal]",
					FldBasicSearchExpression = "CAST([LedgerBal] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[LedgerBal]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				LedgerBal.Init();
				LedgerBal.SetupLookupFilters = SetupLookupFilters;
				LedgerBal.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("LedgerBal", LedgerBal);

				// AvailableBal
				AvailableBal = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_AvailableBal",
					FldName = "AvailableBal",
					FldExpression = "[AvailableBal]",
					FldBasicSearchExpression = "CAST([AvailableBal] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[AvailableBal]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				AvailableBal.Init();
				AvailableBal.SetupLookupFilters = SetupLookupFilters;
				AvailableBal.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("AvailableBal", AvailableBal);

				// _Email
				_Email = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x__Email",
					FldName = "Email",
					FldExpression = "[Email]",
					FldBasicSearchExpression = "[Email]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Email]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				_Email.Init();
				_Email.SetupLookupFilters = SetupLookupFilters;
				_Email.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Email", _Email);

				// URL
				URL = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_URL",
					FldName = "URL",
					FldExpression = "[URL]",
					FldBasicSearchExpression = "[URL]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[URL]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				URL.Init();
				URL.SetupLookupFilters = SetupLookupFilters;
				URL.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("URL", URL);

				// CustType
				CustType = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_CustType",
					FldName = "CustType",
					FldExpression = "[CustType]",
					FldBasicSearchExpression = "[CustType]",
					FldType = 129,
					FldDbType = SqlDbType.Char,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[CustType]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				CustType.Init();
				CustType.SetupLookupFilters = SetupLookupFilters;
				CustType.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("CustType", CustType);

				// RemittanceLicNO
				RemittanceLicNO = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_RemittanceLicNO",
					FldName = "RemittanceLicNO",
					FldExpression = "[RemittanceLicNO]",
					FldBasicSearchExpression = "[RemittanceLicNO]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[RemittanceLicNO]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				RemittanceLicNO.Init();
				RemittanceLicNO.SetupLookupFilters = SetupLookupFilters;
				RemittanceLicNO.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("RemittanceLicNO", RemittanceLicNO);

				// MCLicNo
				MCLicNo = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_MCLicNo",
					FldName = "MCLicNo",
					FldExpression = "[MCLicNo]",
					FldBasicSearchExpression = "[MCLicNo]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[MCLicNo]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				MCLicNo.Init();
				MCLicNo.SetupLookupFilters = SetupLookupFilters;
				MCLicNo.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("MCLicNo", MCLicNo);

				// BankYesNo
				BankYesNo = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BankYesNo",
					FldName = "BankYesNo",
					FldExpression = "[BankYesNo]",
					FldBasicSearchExpression = "[BankYesNo]",
					FldType = 11,
					FldDbType = SqlDbType.Bit,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BankYesNo]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					FldDataType = EW_DATATYPE_BOOLEAN,
					OptionCount = 2,
					IsUpload = false
				};
				BankYesNo.Init();
				BankYesNo.SetupLookupFilters = SetupLookupFilters;
				BankYesNo.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BankYesNo", BankYesNo);

				// BankODLimit
				BankODLimit = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BankODLimit",
					FldName = "BankODLimit",
					FldExpression = "[BankODLimit]",
					FldBasicSearchExpression = "CAST([BankODLimit] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BankODLimit]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				BankODLimit.Init();
				BankODLimit.SetupLookupFilters = SetupLookupFilters;
				BankODLimit.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BankODLimit", BankODLimit);

				// BankAcctNO
				BankAcctNO = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BankAcctNO",
					FldName = "BankAcctNO",
					FldExpression = "[BankAcctNO]",
					FldBasicSearchExpression = "[BankAcctNO]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BankAcctNO]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				BankAcctNO.Init();
				BankAcctNO.SetupLookupFilters = SetupLookupFilters;
				BankAcctNO.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BankAcctNO", BankAcctNO);

				// CreditLimit
				CreditLimit = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_CreditLimit",
					FldName = "CreditLimit",
					FldExpression = "[CreditLimit]",
					FldBasicSearchExpression = "CAST([CreditLimit] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[CreditLimit]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				CreditLimit.Init();
				CreditLimit.FldDefault = 0;
				CreditLimit.SetupLookupFilters = SetupLookupFilters;
				CreditLimit.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("CreditLimit", CreditLimit);

				// ReferBy
				ReferBy = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_ReferBy",
					FldName = "ReferBy",
					FldExpression = "[ReferBy]",
					FldBasicSearchExpression = "[ReferBy]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[ReferBy]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ReferBy.Init();
				ReferBy.SetupLookupFilters = SetupLookupFilters;
				ReferBy.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("ReferBy", ReferBy);

				// AgentImageName
				AgentImageName = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_AgentImageName",
					FldName = "AgentImageName",
					FldExpression = "[AgentImageName]",
					FldBasicSearchExpression = "[AgentImageName]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[AgentImageName]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				AgentImageName.Init();
				AgentImageName.SetupLookupFilters = SetupLookupFilters;
				AgentImageName.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("AgentImageName", AgentImageName);

				// status
				status = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_status",
					FldName = "status",
					FldExpression = "[status]",
					FldBasicSearchExpression = "[status]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[status]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				status.Init();
				status.FldDefault = "Active";
				status.SetupLookupFilters = SetupLookupFilters;
				status.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("status", status);

				// CreatedBy
				CreatedBy = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_CreatedBy",
					FldName = "CreatedBy",
					FldExpression = "[CreatedBy]",
					FldBasicSearchExpression = "[CreatedBy]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[CreatedBy]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				CreatedBy.Init();
				CreatedBy.SetupLookupFilters = SetupLookupFilters;
				CreatedBy.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("CreatedBy", CreatedBy);

				// CreatedDate
				CreatedDate = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_CreatedDate",
					FldName = "CreatedDate",
					FldExpression = "[CreatedDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[CreatedDate]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[CreatedDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				CreatedDate.Init();
				CreatedDate.SetupLookupFilters = SetupLookupFilters;
				CreatedDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("CreatedDate", CreatedDate);

				// ModifiedUser
				ModifiedUser = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_ModifiedUser",
					FldName = "ModifiedUser",
					FldExpression = "[ModifiedUser]",
					FldBasicSearchExpression = "[ModifiedUser]",
					FldType = 129,
					FldDbType = SqlDbType.Char,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[ModifiedUser]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ModifiedUser.Init();
				ModifiedUser.SetupLookupFilters = SetupLookupFilters;
				ModifiedUser.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("ModifiedUser", ModifiedUser);

				// ModifiedDate
				ModifiedDate = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_ModifiedDate",
					FldName = "ModifiedDate",
					FldExpression = "[ModifiedDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[ModifiedDate]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[ModifiedDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				ModifiedDate.Init();
				ModifiedDate.SetupLookupFilters = SetupLookupFilters;
				ModifiedDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("ModifiedDate", ModifiedDate);

				// PPExpiryDate
				PPExpiryDate = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_PPExpiryDate",
					FldName = "PPExpiryDate",
					FldExpression = "[PPExpiryDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[PPExpiryDate]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[PPExpiryDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				PPExpiryDate.Init();
				PPExpiryDate.SetupLookupFilters = SetupLookupFilters;
				PPExpiryDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("PPExpiryDate", PPExpiryDate);

				// TTExpiryDate
				TTExpiryDate = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_TTExpiryDate",
					FldName = "TTExpiryDate",
					FldExpression = "[TTExpiryDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[TTExpiryDate]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[TTExpiryDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				TTExpiryDate.Init();
				TTExpiryDate.SetupLookupFilters = SetupLookupFilters;
				TTExpiryDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("TTExpiryDate", TTExpiryDate);

				// MCExpiryDate
				MCExpiryDate = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_MCExpiryDate",
					FldName = "MCExpiryDate",
					FldExpression = "[MCExpiryDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[MCExpiryDate]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[MCExpiryDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				MCExpiryDate.Init();
				MCExpiryDate.SetupLookupFilters = SetupLookupFilters;
				MCExpiryDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("MCExpiryDate", MCExpiryDate);

				// Action
				Action = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Action",
					FldName = "Action",
					FldExpression = "[Action]",
					FldBasicSearchExpression = "[Action]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Action]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Action.Init();
				Action.SetupLookupFilters = SetupLookupFilters;
				Action.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Action", Action);

				// Remark
				Remark = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Remark",
					FldName = "Remark",
					FldExpression = "[Remark]",
					FldBasicSearchExpression = "[Remark]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Remark]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Remark.Init();
				Remark.SetupLookupFilters = SetupLookupFilters;
				Remark.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Remark", Remark);

				// MCType
				MCType = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_MCType",
					FldName = "MCType",
					FldExpression = "[MCType]",
					FldBasicSearchExpression = "[MCType]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[MCType]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				MCType.Init();
				MCType.FldDefault = "OTHER";
				MCType.SetupLookupFilters = SetupLookupFilters;
				MCType.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("MCType", MCType);

				// CustDOB
				CustDOB = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_CustDOB",
					FldName = "CustDOB",
					FldExpression = "[CustDOB]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[CustDOB]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[CustDOB]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				CustDOB.Init();
				CustDOB.SetupLookupFilters = SetupLookupFilters;
				CustDOB.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("CustDOB", CustDOB);

				// DefContactDOB
				DefContactDOB = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_DefContactDOB",
					FldName = "DefContactDOB",
					FldExpression = "[DefContactDOB]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[DefContactDOB]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[DefContactDOB]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				DefContactDOB.Init();
				DefContactDOB.SetupLookupFilters = SetupLookupFilters;
				DefContactDOB.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("DefContactDOB", DefContactDOB);

				// ScanImage
				ScanImage = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_ScanImage",
					FldName = "ScanImage",
					FldExpression = "[ScanImage]",
					FldBasicSearchExpression = "[ScanImage]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[ScanImage]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ScanImage.Init();
				ScanImage.SetupLookupFilters = SetupLookupFilters;
				ScanImage.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("ScanImage", ScanImage);

				// BizNature
				BizNature = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BizNature",
					FldName = "BizNature",
					FldExpression = "[BizNature]",
					FldBasicSearchExpression = "[BizNature]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BizNature]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				BizNature.Init();
				BizNature.SetupLookupFilters = SetupLookupFilters;
				BizNature.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BizNature", BizNature);

				// DefContactPOB
				DefContactPOB = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_DefContactPOB",
					FldName = "DefContactPOB",
					FldExpression = "[DefContactPOB]",
					FldBasicSearchExpression = "[DefContactPOB]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[DefContactPOB]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				DefContactPOB.Init();
				DefContactPOB.SetupLookupFilters = SetupLookupFilters;
				DefContactPOB.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("DefContactPOB", DefContactPOB);

				// NewTran
				NewTran = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_NewTran",
					FldName = "NewTran",
					FldExpression = "[NewTran]",
					FldBasicSearchExpression = "[NewTran]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[NewTran]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				NewTran.Init();
				NewTran.FldDefault = "Y";
				NewTran.SetupLookupFilters = SetupLookupFilters;
				NewTran.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("NewTran", NewTran);

				// BizRegNo
				BizRegNo = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BizRegNo",
					FldName = "BizRegNo",
					FldExpression = "[BizRegNo]",
					FldBasicSearchExpression = "[BizRegNo]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BizRegNo]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				BizRegNo.Init();
				BizRegNo.SetupLookupFilters = SetupLookupFilters;
				BizRegNo.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BizRegNo", BizRegNo);

				// BizRegDate
				BizRegDate = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BizRegDate",
					FldName = "BizRegDate",
					FldExpression = "[BizRegDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[BizRegDate]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[BizRegDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				BizRegDate.Init();
				BizRegDate.SetupLookupFilters = SetupLookupFilters;
				BizRegDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BizRegDate", BizRegDate);

				// BizRegPlace
				BizRegPlace = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BizRegPlace",
					FldName = "BizRegPlace",
					FldExpression = "[BizRegPlace]",
					FldBasicSearchExpression = "[BizRegPlace]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BizRegPlace]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				BizRegPlace.Init();
				BizRegPlace.SetupLookupFilters = SetupLookupFilters;
				BizRegPlace.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BizRegPlace", BizRegPlace);

				// BizRegExpDate
				BizRegExpDate = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BizRegExpDate",
					FldName = "BizRegExpDate",
					FldExpression = "[BizRegExpDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[BizRegExpDate]", 0, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 0,
					FldVirtualExpression = "[BizRegExpDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDate").Replace("%s", EW_DATE_FORMAT),
					IsUpload = false
				};
				BizRegExpDate.Init();
				BizRegExpDate.SetupLookupFilters = SetupLookupFilters;
				BizRegExpDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BizRegExpDate", BizRegExpDate);

				// UnIncorpExec
				UnIncorpExec = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_UnIncorpExec",
					FldName = "UnIncorpExec",
					FldExpression = "[UnIncorpExec]",
					FldBasicSearchExpression = "CAST([UnIncorpExec] AS NVARCHAR)",
					FldType = 2,
					FldDbType = SqlDbType.SmallInt,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[UnIncorpExec]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				UnIncorpExec.Init();
				UnIncorpExec.FldDefault = 0;
				UnIncorpExec.SetupLookupFilters = SetupLookupFilters;
				UnIncorpExec.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("UnIncorpExec", UnIncorpExec);

				// DefContactAuthorzLetter
				DefContactAuthorzLetter = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_DefContactAuthorzLetter",
					FldName = "DefContactAuthorzLetter",
					FldExpression = "[DefContactAuthorzLetter]",
					FldBasicSearchExpression = "CAST([DefContactAuthorzLetter] AS NVARCHAR)",
					FldType = 2,
					FldDbType = SqlDbType.SmallInt,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[DefContactAuthorzLetter]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				DefContactAuthorzLetter.Init();
				DefContactAuthorzLetter.FldDefault = 0;
				DefContactAuthorzLetter.SetupLookupFilters = SetupLookupFilters;
				DefContactAuthorzLetter.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("DefContactAuthorzLetter", DefContactAuthorzLetter);

				// Politician
				Politician = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Politician",
					FldName = "Politician",
					FldExpression = "[Politician]",
					FldBasicSearchExpression = "CAST([Politician] AS NVARCHAR)",
					FldType = 2,
					FldDbType = SqlDbType.SmallInt,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Politician]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				Politician.Init();
				Politician.FldDefault = 0;
				Politician.SetupLookupFilters = SetupLookupFilters;
				Politician.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Politician", Politician);

				// BizPartnerNo
				BizPartnerNo = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BizPartnerNo",
					FldName = "BizPartnerNo",
					FldExpression = "[BizPartnerNo]",
					FldBasicSearchExpression = "CAST([BizPartnerNo] AS NVARCHAR)",
					FldType = 2,
					FldDbType = SqlDbType.SmallInt,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BizPartnerNo]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				BizPartnerNo.Init();
				BizPartnerNo.FldDefault = 1;
				BizPartnerNo.SetupLookupFilters = SetupLookupFilters;
				BizPartnerNo.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BizPartnerNo", BizPartnerNo);

				// Remark2
				Remark2 = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_Remark2",
					FldName = "Remark2",
					FldExpression = "[Remark2]",
					FldBasicSearchExpression = "[Remark2]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[Remark2]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Remark2.Init();
				Remark2.SetupLookupFilters = SetupLookupFilters;
				Remark2.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("Remark2", Remark2);

				// BannedListRemark
				BannedListRemark = new cField<SqlDbType> {
					TblVar = "Agent",
					TblName = "Agent",
					FldVar = "x_BannedListRemark",
					FldName = "BannedListRemark",
					FldExpression = "[BannedListRemark]",
					FldBasicSearchExpression = "[BannedListRemark]",
					FldType = 202,
					FldDbType = SqlDbType.NVarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[BannedListRemark]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				BannedListRemark.Init();
				BannedListRemark.SetupLookupFilters = SetupLookupFilters;
				BannedListRemark.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("BannedListRemark", BannedListRemark);
			}

			// Set Field Visibility
			public override bool SetFieldVisibility(string fldname) {
				var fld = FieldByName(fldname);
				return fld.Visible; // Returns original value
			}

			// Setup lookup filters of a field // DN
			public virtual void SetupLookupFilters(cField fld, string pageId = null) {

				// To be overridden by page class
			}

			// Setup AutoSuggest filters of a field // DN
			public virtual void SetupAutoSuggestFilters(cField fld, string pageId = null) {

				// To be overridden by page class
			}

			// Invoke method of table class and subclasses // DN
			public object Invoke(string name, object[] parameters = null) {
				MethodInfo mi = this.GetType().GetMethod(name);
				return mi?.Invoke(this, parameters);
			}
			#pragma warning disable 618

			// Connection
			public cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> Connection {
				get {
					return ew_GetConn(DBID);
				}
			}
			#pragma warning restore 618

			// Single column sort
			public void UpdateSort(cField ofld) {
				string sLastSort, sSortField, sThisSort;
				if (CurrentOrder == ofld.FldName) {
					sSortField = ofld.FldExpression;
					sLastSort = ofld.Sort;
					if (CurrentOrderType == "ASC" || CurrentOrderType == "DESC") {
						sThisSort = CurrentOrderType;
					} else {
						sThisSort = (sLastSort == "ASC") ? "DESC" : "ASC";
					}
					ofld.Sort = sThisSort;
					SessionOrderBy = sSortField + " " + sThisSort;	// Save to Session
				} else {
					ofld.Sort = "";
				}
			}

			// Current detail table name
			public string CurrentDetailTable {
				get { return Convert.ToString(ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_DETAIL_TABLE]); }
				set { ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_DETAIL_TABLE] = value; }
			}

			// List of current detail table names
			public List<string> CurrentDetailTables {
				get { return CurrentDetailTable.Split(',').ToList(); }
			}

			// Get detail URL
			public string DetailUrl {
				get {
					string sDetailUrl = "";
					if (CurrentDetailTable == "AgentBank") {
						sDetailUrl = AgentBank.ListUrl + "?" + EW_TABLE_SHOW_MASTER + "=" + TableVar;
						sDetailUrl += "&fk_AgentId=" + ew_UrlEncode(AgentId.CurrentValue);
					}
					if (ew_Empty(sDetailUrl))
						sDetailUrl = "Agentlist";
					return sDetailUrl;
				}
			}

			// Table level SQL
			// FROM

			private string _SqlFrom = "";
			public string SqlFrom {
				get { return ew_NotEmpty(_SqlFrom) ? _SqlFrom : "[dbo].[Agent]"; }
				set { _SqlFrom = value; }
			}

			// SELECT
			private string _SqlSelect = "";
			public string SqlSelect { // Select
				get { return ew_NotEmpty(_SqlSelect) ? _SqlSelect : "SELECT * FROM " + SqlFrom; }
				set { _SqlSelect = value; }
			}

			// WHERE // DN
			private string _SqlWhere = "";
			public string SqlWhere {
				get {
					string sWhere = "";
					return ew_NotEmpty(_SqlWhere) ? _SqlWhere : sWhere;
				}
				set {
					_SqlWhere = value;
				}
			}

			// Group By
			private string _SqlGroupBy = "";
			public string SqlGroupBy {
				get { return ew_NotEmpty(_SqlGroupBy) ? _SqlGroupBy : ""; }
				set { _SqlGroupBy = value; }
			}

			// Having
			private string _SqlHaving = "";
			public string SqlHaving {
				get { return ew_NotEmpty(_SqlHaving) ? _SqlHaving : ""; }
				set { _SqlHaving = value; }
			}

			// Order By
			private string _SqlOrderBy = "";
			public string SqlOrderBy {
				get { return ew_NotEmpty(_SqlOrderBy) ? _SqlOrderBy : ""; }
				set { _SqlOrderBy = value; }
			}

			// Apply User ID filters
			public string ApplyUserIDFilters(string Filter) {
				string sFilter = Filter;
				return sFilter;
			}

			// Check if User ID security allows view all
			public bool UserIDAllow(string id = "") {
				int allow = EW_USER_ID_ALLOW;
				switch (id) {
					case "add":
					case "copy":
					case "gridadd":
					case "register":
					case "addopt":
						return ((allow & 1) == 1);
					case "edit":
					case "gridedit":
					case "update":
					case "changepwd":
					case "forgotpwd":
						return ((allow & 4) == 4);
					case "delete":
						return ((allow & 2) == 2);
					case "view":
						return ((allow & 32) == 32);
					case "search":
						return ((allow & 64) == 64);
					default:
						return ((allow & 8) == 8);
				}
			}

			// Get SQL
			public string GetSQL(string where, string orderby = "") {
				return ew_BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, where, orderby);
			}

			// Table SQL
			public string SQL {
				get {
					string sFilter = CurrentFilter;
					string sSort = SessionOrderBy;
					return ew_BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, sFilter, sSort);
				}
			}

			// Table SQL with List page filter
			public string SelectSQL {
				get {
					string sSort = "";
					string sFilter = SessionWhere;
					ew_AddFilter(ref sFilter, CurrentFilter);
					Recordset_Selecting(ref sFilter);
					sSort = SessionOrderBy;
					return ew_BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, sFilter, sSort);
				}
			}

			// Get ORDER BY clause
			public string OrderBy {
				get {
					string sSort = SessionOrderBy;
					return ew_BuildSelectSql("", "", "", "", SqlOrderBy, "", sSort);
				}
			}

			// Get record count by reading data reader
			public int GetRecordCount(string sSql) {
				try {
					var cnt = 0;
					using (var dr = Connection.OpenDataReader(sSql)) {
						while (dr.Read())
							cnt++;
					}
					return cnt;
				} catch {
					if (EW_DEBUG_ENABLED)
						throw;
					return -1;
				}
			}

			// Try to get record count by SELECT COUNT(*)
			public int TryGetRecordCount(string sSql) {
				try {
					var sOrderBy = OrderBy;
					if (sSql.EndsWith(sOrderBy))
						sSql = sSql.Substring(0, sSql.Length - sOrderBy.Length); // Remove ORDER BY clause
					if ((new List<string>() { "TABLE", "VIEW", "LINKTABLE" }).Contains(TableType) && sSql.StartsWith(SqlSelect)) { // handle Custom Field
						sSql = "SELECT COUNT(*) FROM " + SqlFrom + sSql.Substring(SqlSelect.Length);
					} else {
						sSql = "SELECT COUNT(*) FROM (" + sSql + ") EW_COUNT_TABLE";
					}
					return Convert.ToInt32(Connection.ExecuteScalar(sSql));
				} catch {
					return GetRecordCount(sSql);
				}
			}

			// Get record count based on filter (for detail record count in master table pages)
			public int LoadRecordCount(string sFilter) {
				var sql = GetSQL(sFilter);
				return TryGetRecordCount(sql);
			}

			// Get record count (for current List page)
			public int SelectRecordCount() {
				var sql = SelectSQL;
				return TryGetRecordCount(sql);
			}

			// Insert
			public int Insert(OrderedDictionary rs) {
				string names = "";
				string values = "";
				foreach (DictionaryEntry f in rs) {
					var fld = FieldByName((string)f.Key);
					if (fld != null) {
						names += fld.FldExpression + ",";
						values += SqlParameter(fld) + ",";
					}
				}
				if (names.EndsWith(",")) names = names.Remove(names.Length - 1);
				if (values.EndsWith(",")) values = values.Remove(values.Length - 1);
				if (ew_Empty(names)) return -1;
				string Sql = "INSERT INTO " + UpdateTable + " (" + names + ") VALUES (" + values + ")";
				var command = Connection.GetCommand(Sql);
				foreach (DictionaryEntry f in rs) {
					var fld = (cField<SqlDbType>)FieldByName((string)f.Key); // DN
					if (fld?.FldIsCustom ?? true)
						continue;
					try {
						command.Parameters.Add(fld.FldVar, fld.Type).Value = ParameterValue(fld, f.Value);
					} catch {
						if (EW_DEBUG_ENABLED) throw;
					}
				}
				int result = command.ExecuteNonQuery();
				if (result > 0) {
				}
				return result;
			}

			// Update
			#pragma warning disable 168, 219
			public int Update(OrderedDictionary rs, object where = null, OrderedDictionary rsold = null, bool curfilter = true) {
				var bCascadeUpdate = false;
				DbDataReader drWrk;
				OrderedDictionary rskey = new OrderedDictionary();
				int iUpdate;
				var rscascade = new OrderedDictionary();
				string swhere = "";
				var values = "";
				foreach (DictionaryEntry f in rs) {
					var fld = FieldByName((string)f.Key);
					if (fld != null)
						values += fld.FldExpression + "=" + SqlParameter(fld) + ",";
				}
				if (values.EndsWith(","))
					values = values.Remove(values.Length - 1);
				if (ew_Empty(values))
					return -1;
				string Sql = "UPDATE " + UpdateTable + " SET " + values;
				string filter = curfilter ? CurrentFilter : "";
				if (ew_IsDictionary(where))
					swhere = ArrayToFilter((OrderedDictionary)where);
				else
					swhere = (string)where;
				ew_AddFilter(ref filter, swhere);
				if (ew_NotEmpty(filter))
					Sql += " WHERE " + filter;
				var command = Connection.GetCommand(Sql);
				foreach (DictionaryEntry f in rs) {
					var fld = (cField<SqlDbType>)FieldByName((string)f.Key); // DN
					if (fld?.FldIsCustom ?? true)
						continue;
					try {
						command.Parameters.Add(fld.FldVar, fld.Type).Value = ParameterValue(fld, f.Value);
					} catch {
						if (EW_DEBUG_ENABLED) throw;
					}
				}
				int result = command.ExecuteNonQuery();
				return result;
			}
			#pragma warning restore 168, 219

			// Convert to parameter name for use in SQL
			public string SqlParameter(cField fld) {
				string sSymbol = ew_GetSqlParamSymbol(DBID);
				string sValue = sSymbol;
				if (sSymbol != "?")
					sValue += fld.FldVar;
				return sValue;
			}

			// Convert value to object for parameter
			public object ParameterValue(cField fld, object value) {
				return value;
			}
			#pragma warning disable 168

			// Delete
			public int Delete(OrderedDictionary rs, object where = null, bool curfilter = true) {
				string swhere = "";
				DbDataReader drWrk;
				string Sql = "DELETE FROM " + UpdateTable + " WHERE ";
				string filter = curfilter ? CurrentFilter : "";
				if (ew_IsDictionary(where))
					swhere = ArrayToFilter((OrderedDictionary)where);
				else
					swhere = (string)where;
				ew_AddFilter(ref filter, swhere);
				if (rs != null) {
					cField fld;
					fld = FieldByName("AgentId");
					ew_AddFilter(ref filter, fld.FldExpression + "=" + ew_QuotedValue(rs["AgentId"], fld.FldDataType, DBID));
				}
				if (ew_NotEmpty(filter))
					Sql += filter;
				else
					Sql += "0=1"; // Avoid delete
				int result = Connection.ExecuteNonQuery(Sql);
				return result;
			}
			#pragma warning restore 168

			// Key filter WHERE clause
			private string SqlKeyFilter {
				get {
					return "[AgentId] = '@AgentId@'";
				}
			}

			// Key filter
			public string KeyFilter {
				get {
					string sKeyFilter = SqlKeyFilter;
					sKeyFilter = sKeyFilter.Replace("@AgentId@", ew_AdjustSql(AgentId.CurrentValue, DBID)); // Replace key value
					return sKeyFilter;
				}
			}

			// Return URL
			public string ReturnUrl {
				get {
					string name = EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_RETURN_URL;

					// Get referer URL automatically
					if (ew_NotEmpty(ew_ReferUrl()) && ew_ReferPage() != ew_CurrentPage() &&
						ew_ReferPage() != "login") // Referer not same page or login page
							ew_Session[name] = ew_ReferUrl(); // Save to Session
					if (ew_NotEmpty(ew_Session[name])) {
						return Convert.ToString(ew_Session[name]);
					} else {
						return "Agentlist";
					}
				}
				set {
					ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_RETURN_URL] = value;
				}
			}

			// List URL
			public string ListUrl {
				get {
					return "Agentlist";
				}
			}

			// View URL
			public string ViewUrl {
				get {
					return GetViewUrl();
				}
			}

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (ew_NotEmpty(parm))
					url = KeyUrl("Agentview", UrlParm(parm));
				else
					url = KeyUrl("Agentview", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "Agentadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (ew_NotEmpty(parm))
					url = "Agentadd?" + UrlParm(parm);
				else
					url = "Agentadd";
				return ew_AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl {
				get {
					return GetEditUrl();
				}
			}

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				if (ew_NotEmpty(parm))
					url = KeyUrl("Agentedit", UrlParm(parm));
				else
					url = KeyUrl("Agentedit", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
				return ew_AppPath(AddMasterUrl(url)); // DN
			}

			// Inline edit URL
			public string InlineEditUrl	{
				get {
					var url = KeyUrl(ew_CurrentPage(), UrlParm("a=edit"));
					return ew_AppPath(AddMasterUrl(url)); // DN
				}
			}

			// Copy URL
			public string CopyUrl {
				get {
					return GetCopyUrl();
				}
			}

			// Copy URL
			public string GetCopyUrl(string parm = "") {
				string url = "";
				if (ew_NotEmpty(parm))
					url = KeyUrl("Agentadd", UrlParm(parm));
				else
					url = KeyUrl("Agentadd", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
				return ew_AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl	{
				get {
					var url  = KeyUrl(ew_CurrentPage(), UrlParm("a=copy"));
					return ew_AppPath(AddMasterUrl(url)); // DN
				}
			}

			// Delete URL
			public string DeleteUrl	{
				get {
					var url = KeyUrl("Agentdelete", UrlParm());
					return ew_AppPath(url); // DN
				}
			}

			// Add master URL
			public string AddMasterUrl(string url) {
				return url;
			}
			public string KeyToJson() {
				string json = "";
						json += "AgentId:" + ew_VarToJson(AgentId.CurrentValue, "string", "'");
				return "{" + json + "}";
			}

			// Add key value to URL
			public string KeyUrl(string url, string parm = "") { // DN
				string sUrl = url;
				if (!Convert.IsDBNull(AgentId.CurrentValue)) {
					sUrl += "/" + ew_UrlEncode(Convert.ToString(AgentId.CurrentValue));
				} else {
					return "javascript:ew_Alert(ewLanguage.Phrase('InvalidRecord'));";
				}
				if (ew_Empty(parm))
					return sUrl;
				else
					return sUrl + "?" + parm;
			}

			// Sort URL (already URL-encoded)
			public string SortUrl(cField fld) {
				if (ew_NotEmpty(CurrentAction) || ew_NotEmpty(Export) ||
					(new List<int>() {141, 201, 203, 128, 204, 205}).Contains(fld.FldType)) { // Unsortable data type
					return "";
				} else if (fld.Sortable) {
					string sUrlParm = UrlParm("order=" + ew_UrlEncode(fld.FldName) + "&amp;ordertype=" + fld.ReverseSort());
					return AddMasterUrl(ew_CurrentPage() + "?" + sUrlParm);
				}
				return "";
			}

			// Get record keys
			public List<string> GetRecordKeys() {
			    var arKeys = new List<string>();
			    var ar = new List<string>();
			    if (ew_Form.ContainsKey("key_m") || ew_QueryString.ContainsKey("key_m")) {
			        var key_m = IsPost ? ew_Form["key_m"].ToArray() : ew_QueryString["key_m"].ToArray();
			        arKeys.AddRange((IEnumerable<string>)key_m);
			    } else if (RouteValues.Count > 0 || ew_QueryString.Count > 0 || ew_Form.Count > 0) { // DN
					var key = "";
					if (RouteValues.ContainsKey("AgentId")) { // AgentId
						key = Convert.ToString(RouteValues["AgentId"]);
					} else if (IsPost) {
						key = ew_Post("AgentId");
					} else {
						key = ew_Get("AgentId");
					}
			        arKeys.Add(key);
			    }

			    // Check keys
			    foreach (var keys in arKeys) {
			        ar.Add(keys);
			    }
			    return ar;
			}

			// Get key filter
			public string GetKeyFilter() {
				List<string> arKeys = GetRecordKeys();
				string sKeyFilter = "";
				foreach (var keys in arKeys) {
					if (ew_NotEmpty(sKeyFilter))
						sKeyFilter += " OR ";
					AgentId.CurrentValue = keys;
					sKeyFilter += "(" + KeyFilter + ")";
				}
				return sKeyFilter;
			}
			#pragma warning disable 618

			// Load rows based on filter
			public DbDataReader LoadRs(string sFilter, cConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {

				// Set up filter (SQL WHERE clause) and get return SQL
				string sSql = GetSQL(sFilter);
				try {
					var rs = cnn?.OpenDataReader(sSql) ?? Connection.OpenDataReader(sSql); // DN
					if (rs != null && rs.HasRows)
						return rs;
					rs?.Close();
					rs?.Dispose();
				} catch {}
				return null;
			}
			#pragma warning restore 618

			// Load row values from recordset
			public void LoadListRowValues(DbDataReader rs) {
				AgentId.DbValue = rs["AgentId"];
				AgentName.DbValue = rs["AgentName"];
				AgentRiskRating.DbValue = rs["AgentRiskRating"];
				AgentRiskCredit.DbValue = rs["AgentRiskCredit"];
				Address1.DbValue = rs["Address1"];
				Address2.DbValue = rs["Address2"];
				Address3.DbValue = rs["Address3"];
				Country.DbValue = rs["Country"];
				ZipCode.DbValue = rs["ZipCode"];
				Fax.DbValue = rs["Fax"];
				Phone.DbValue = rs["Phone"];
				Mobile.DbValue = rs["Mobile"];
				BuzType.DbValue = rs["BuzType"];
				ClassType.DbValue = rs["ClassType"];
				DefContactPName.DbValue = rs["DefContactPName"];
				DefContactPNric.DbValue = rs["DefContactPNric"];
				DefContactPNation.DbValue = rs["DefContactPNation"];
				DefContactPOccupation.DbValue = rs["DefContactPOccupation"];
				TermsId.DbValue = rs["TermsId"];
				LedgerBal.DbValue = rs["LedgerBal"];
				AvailableBal.DbValue = rs["AvailableBal"];
				_Email.DbValue = rs["Email"];
				URL.DbValue = rs["URL"];
				CustType.DbValue = rs["CustType"];
				RemittanceLicNO.DbValue = rs["RemittanceLicNO"];
				MCLicNo.DbValue = rs["MCLicNo"];
				BankYesNo.DbValue = ew_ConvertToBool(rs["BankYesNo"]) ? "1" : "0";
				BankODLimit.DbValue = rs["BankODLimit"];
				BankAcctNO.DbValue = rs["BankAcctNO"];
				CreditLimit.DbValue = rs["CreditLimit"];
				ReferBy.DbValue = rs["ReferBy"];
				AgentImageName.DbValue = rs["AgentImageName"];
				status.DbValue = rs["status"];
				CreatedBy.DbValue = rs["CreatedBy"];
				CreatedDate.DbValue = rs["CreatedDate"];
				ModifiedUser.DbValue = rs["ModifiedUser"];
				ModifiedDate.DbValue = rs["ModifiedDate"];
				PPExpiryDate.DbValue = rs["PPExpiryDate"];
				TTExpiryDate.DbValue = rs["TTExpiryDate"];
				MCExpiryDate.DbValue = rs["MCExpiryDate"];
				Action.DbValue = rs["Action"];
				Remark.DbValue = rs["Remark"];
				MCType.DbValue = rs["MCType"];
				CustDOB.DbValue = rs["CustDOB"];
				DefContactDOB.DbValue = rs["DefContactDOB"];
				ScanImage.DbValue = rs["ScanImage"];
				BizNature.DbValue = rs["BizNature"];
				DefContactPOB.DbValue = rs["DefContactPOB"];
				NewTran.DbValue = rs["NewTran"];
				BizRegNo.DbValue = rs["BizRegNo"];
				BizRegDate.DbValue = rs["BizRegDate"];
				BizRegPlace.DbValue = rs["BizRegPlace"];
				BizRegExpDate.DbValue = rs["BizRegExpDate"];
				UnIncorpExec.DbValue = rs["UnIncorpExec"];
				DefContactAuthorzLetter.DbValue = rs["DefContactAuthorzLetter"];
				Politician.DbValue = rs["Politician"];
				BizPartnerNo.DbValue = rs["BizPartnerNo"];
				Remark2.DbValue = rs["Remark2"];
				BannedListRemark.DbValue = rs["BannedListRemark"];
			}

			// Render list row values
			public void RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

		   // Common render codes
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

				// Call Row Rendered event
				Row_Rendered();
			}

			// Render edit row values
			public void RenderEditRow() {

				// Call Row Rendering event
					Row_Rendering();

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

			// Email
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

				// Call Row Rendered event
					Row_Rendered();
			}

			// Aggregate list row values
			public void AggregateListRowValues() {
			}

			// Aggregate list row (for rendering)
			public void AggregateListRow() {

				// Call Row Rendered event
				Row_Rendered();
			}
			public dynamic ExportDoc;

			// Export data in HTML/CSV/Word/Excel/Email/PDF format
			public void ExportDocument(dynamic Doc, DbDataReader Recordset, int StartRec, int StopRec, string ExportPageType = "") {
				if (Recordset == null || Doc == null)
					return;
				if (!Doc.ExportCustom) {

					// Write header
					Doc.ExportTableHeader();
					if (Doc.Horizontal) { // Horizontal format, write header
						Doc.BeginExportRow();
						if (ExportPageType == "view") {
							if (AgentId.Exportable) Doc.ExportCaption(AgentId);
							if (AgentName.Exportable) Doc.ExportCaption(AgentName);
							if (AgentRiskRating.Exportable) Doc.ExportCaption(AgentRiskRating);
							if (AgentRiskCredit.Exportable) Doc.ExportCaption(AgentRiskCredit);
						} else {
							if (AgentId.Exportable) Doc.ExportCaption(AgentId);
							if (AgentName.Exportable) Doc.ExportCaption(AgentName);
							if (AgentRiskRating.Exportable) Doc.ExportCaption(AgentRiskRating);
							if (AgentRiskCredit.Exportable) Doc.ExportCaption(AgentRiskCredit);
							if (Address1.Exportable) Doc.ExportCaption(Address1);
							if (Address2.Exportable) Doc.ExportCaption(Address2);
							if (Address3.Exportable) Doc.ExportCaption(Address3);
							if (Country.Exportable) Doc.ExportCaption(Country);
							if (ZipCode.Exportable) Doc.ExportCaption(ZipCode);
							if (Fax.Exportable) Doc.ExportCaption(Fax);
							if (Phone.Exportable) Doc.ExportCaption(Phone);
							if (Mobile.Exportable) Doc.ExportCaption(Mobile);
							if (BuzType.Exportable) Doc.ExportCaption(BuzType);
							if (ClassType.Exportable) Doc.ExportCaption(ClassType);
							if (DefContactPName.Exportable) Doc.ExportCaption(DefContactPName);
							if (DefContactPNric.Exportable) Doc.ExportCaption(DefContactPNric);
							if (DefContactPNation.Exportable) Doc.ExportCaption(DefContactPNation);
							if (DefContactPOccupation.Exportable) Doc.ExportCaption(DefContactPOccupation);
							if (TermsId.Exportable) Doc.ExportCaption(TermsId);
							if (LedgerBal.Exportable) Doc.ExportCaption(LedgerBal);
							if (AvailableBal.Exportable) Doc.ExportCaption(AvailableBal);
							if (_Email.Exportable) Doc.ExportCaption(_Email);
							if (URL.Exportable) Doc.ExportCaption(URL);
							if (CustType.Exportable) Doc.ExportCaption(CustType);
							if (RemittanceLicNO.Exportable) Doc.ExportCaption(RemittanceLicNO);
							if (MCLicNo.Exportable) Doc.ExportCaption(MCLicNo);
							if (BankYesNo.Exportable) Doc.ExportCaption(BankYesNo);
							if (BankODLimit.Exportable) Doc.ExportCaption(BankODLimit);
							if (BankAcctNO.Exportable) Doc.ExportCaption(BankAcctNO);
							if (CreditLimit.Exportable) Doc.ExportCaption(CreditLimit);
							if (ReferBy.Exportable) Doc.ExportCaption(ReferBy);
							if (AgentImageName.Exportable) Doc.ExportCaption(AgentImageName);
							if (status.Exportable) Doc.ExportCaption(status);
							if (CreatedBy.Exportable) Doc.ExportCaption(CreatedBy);
							if (CreatedDate.Exportable) Doc.ExportCaption(CreatedDate);
							if (ModifiedUser.Exportable) Doc.ExportCaption(ModifiedUser);
							if (ModifiedDate.Exportable) Doc.ExportCaption(ModifiedDate);
							if (PPExpiryDate.Exportable) Doc.ExportCaption(PPExpiryDate);
							if (TTExpiryDate.Exportable) Doc.ExportCaption(TTExpiryDate);
							if (MCExpiryDate.Exportable) Doc.ExportCaption(MCExpiryDate);
							if (Action.Exportable) Doc.ExportCaption(Action);
							if (Remark.Exportable) Doc.ExportCaption(Remark);
							if (MCType.Exportable) Doc.ExportCaption(MCType);
							if (CustDOB.Exportable) Doc.ExportCaption(CustDOB);
							if (DefContactDOB.Exportable) Doc.ExportCaption(DefContactDOB);
							if (ScanImage.Exportable) Doc.ExportCaption(ScanImage);
							if (BizNature.Exportable) Doc.ExportCaption(BizNature);
							if (DefContactPOB.Exportable) Doc.ExportCaption(DefContactPOB);
							if (NewTran.Exportable) Doc.ExportCaption(NewTran);
							if (BizRegNo.Exportable) Doc.ExportCaption(BizRegNo);
							if (BizRegDate.Exportable) Doc.ExportCaption(BizRegDate);
							if (BizRegPlace.Exportable) Doc.ExportCaption(BizRegPlace);
							if (BizRegExpDate.Exportable) Doc.ExportCaption(BizRegExpDate);
							if (UnIncorpExec.Exportable) Doc.ExportCaption(UnIncorpExec);
							if (DefContactAuthorzLetter.Exportable) Doc.ExportCaption(DefContactAuthorzLetter);
							if (Politician.Exportable) Doc.ExportCaption(Politician);
							if (BizPartnerNo.Exportable) Doc.ExportCaption(BizPartnerNo);
							if (Remark2.Exportable) Doc.ExportCaption(Remark2);
							if (BannedListRemark.Exportable) Doc.ExportCaption(BannedListRemark);
						}
						Doc.EndExportRow();
					}
				}

				// Move to first record
				// For List page only. For View page, the recordset is alreay at the start record. // DN

				int RecCnt = StartRec - 1;
				if (ExportPageType != "view") {
					if (Connection.SelectOffset) {
						Recordset.Read();
					} else {
						for (int i = 0; i < StartRec; i++) // Move to the start record and use do-while loop
							Recordset.Read();
					}
				}
				do { // DN
					RecCnt++;
					if (RecCnt >= StartRec) {
						int RowCnt = RecCnt - StartRec + 1;

						// Page break
						if (ExportPageBreakCount > 0) {
							if (RowCnt > 1 && (RowCnt - 1) % ExportPageBreakCount == 0)
								Doc.ExportPageBreak();
						}
						LoadListRowValues(Recordset);

						// Render row
						RowType = EW_ROWTYPE_VIEW; // Render view
						ResetAttrs();
						RenderListRow();
						if (!Doc.ExportCustom) {
							Doc.BeginExportRow(RowCnt); // Allow CSS styles if enabled
							if (ExportPageType == "view") {
								if (AgentId.Exportable) Doc.ExportField(AgentId);
								if (AgentName.Exportable) Doc.ExportField(AgentName);
								if (AgentRiskRating.Exportable) Doc.ExportField(AgentRiskRating);
								if (AgentRiskCredit.Exportable) Doc.ExportField(AgentRiskCredit);
							} else {
								if (AgentId.Exportable) Doc.ExportField(AgentId);
								if (AgentName.Exportable) Doc.ExportField(AgentName);
								if (AgentRiskRating.Exportable) Doc.ExportField(AgentRiskRating);
								if (AgentRiskCredit.Exportable) Doc.ExportField(AgentRiskCredit);
								if (Address1.Exportable) Doc.ExportField(Address1);
								if (Address2.Exportable) Doc.ExportField(Address2);
								if (Address3.Exportable) Doc.ExportField(Address3);
								if (Country.Exportable) Doc.ExportField(Country);
								if (ZipCode.Exportable) Doc.ExportField(ZipCode);
								if (Fax.Exportable) Doc.ExportField(Fax);
								if (Phone.Exportable) Doc.ExportField(Phone);
								if (Mobile.Exportable) Doc.ExportField(Mobile);
								if (BuzType.Exportable) Doc.ExportField(BuzType);
								if (ClassType.Exportable) Doc.ExportField(ClassType);
								if (DefContactPName.Exportable) Doc.ExportField(DefContactPName);
								if (DefContactPNric.Exportable) Doc.ExportField(DefContactPNric);
								if (DefContactPNation.Exportable) Doc.ExportField(DefContactPNation);
								if (DefContactPOccupation.Exportable) Doc.ExportField(DefContactPOccupation);
								if (TermsId.Exportable) Doc.ExportField(TermsId);
								if (LedgerBal.Exportable) Doc.ExportField(LedgerBal);
								if (AvailableBal.Exportable) Doc.ExportField(AvailableBal);
								if (_Email.Exportable) Doc.ExportField(_Email);
								if (URL.Exportable) Doc.ExportField(URL);
								if (CustType.Exportable) Doc.ExportField(CustType);
								if (RemittanceLicNO.Exportable) Doc.ExportField(RemittanceLicNO);
								if (MCLicNo.Exportable) Doc.ExportField(MCLicNo);
								if (BankYesNo.Exportable) Doc.ExportField(BankYesNo);
								if (BankODLimit.Exportable) Doc.ExportField(BankODLimit);
								if (BankAcctNO.Exportable) Doc.ExportField(BankAcctNO);
								if (CreditLimit.Exportable) Doc.ExportField(CreditLimit);
								if (ReferBy.Exportable) Doc.ExportField(ReferBy);
								if (AgentImageName.Exportable) Doc.ExportField(AgentImageName);
								if (status.Exportable) Doc.ExportField(status);
								if (CreatedBy.Exportable) Doc.ExportField(CreatedBy);
								if (CreatedDate.Exportable) Doc.ExportField(CreatedDate);
								if (ModifiedUser.Exportable) Doc.ExportField(ModifiedUser);
								if (ModifiedDate.Exportable) Doc.ExportField(ModifiedDate);
								if (PPExpiryDate.Exportable) Doc.ExportField(PPExpiryDate);
								if (TTExpiryDate.Exportable) Doc.ExportField(TTExpiryDate);
								if (MCExpiryDate.Exportable) Doc.ExportField(MCExpiryDate);
								if (Action.Exportable) Doc.ExportField(Action);
								if (Remark.Exportable) Doc.ExportField(Remark);
								if (MCType.Exportable) Doc.ExportField(MCType);
								if (CustDOB.Exportable) Doc.ExportField(CustDOB);
								if (DefContactDOB.Exportable) Doc.ExportField(DefContactDOB);
								if (ScanImage.Exportable) Doc.ExportField(ScanImage);
								if (BizNature.Exportable) Doc.ExportField(BizNature);
								if (DefContactPOB.Exportable) Doc.ExportField(DefContactPOB);
								if (NewTran.Exportable) Doc.ExportField(NewTran);
								if (BizRegNo.Exportable) Doc.ExportField(BizRegNo);
								if (BizRegDate.Exportable) Doc.ExportField(BizRegDate);
								if (BizRegPlace.Exportable) Doc.ExportField(BizRegPlace);
								if (BizRegExpDate.Exportable) Doc.ExportField(BizRegExpDate);
								if (UnIncorpExec.Exportable) Doc.ExportField(UnIncorpExec);
								if (DefContactAuthorzLetter.Exportable) Doc.ExportField(DefContactAuthorzLetter);
								if (Politician.Exportable) Doc.ExportField(Politician);
								if (BizPartnerNo.Exportable) Doc.ExportField(BizPartnerNo);
								if (Remark2.Exportable) Doc.ExportField(Remark2);
								if (BannedListRemark.Exportable) Doc.ExportField(BannedListRemark);
							}
							Doc.EndExportRow();
						}
					}

					// Call Row Export server event
					if (Doc.ExportCustom)
						Row_Export(Recordset);
				} while (RecCnt < StopRec && Recordset.Read()); // DN
				if (!Doc.ExportCustom) {
					Doc.ExportTableFooter();
				}
			}

			// Get auto fill value
			public string GetAutoFill(string id, string val) {
				var rsarr = new List<OrderedDictionary>();
				int rowcnt = 0;

				// Output (using ew_Response) // DN
				if (ew_IsList(rsarr) && rowcnt > 0) {
					foreach (OrderedDictionary row in rsarr) {
						for (var i = 0; i < row.Count; i++) {
							var str = Convert.ToString(row[i]);
							if (ew_Empty(ew_Post("keepHTML")))
								str = ew_RemoveHtml(str);
							if (str.Contains("\r") || str.Contains("\n")) {
								if (ew_NotEmpty(ew_Post("keepCRLF"))) {
									str = str.Replace("\r", "\\r").Replace("\n", "\\n");
								} else {
									str = str.Replace("\r", " ").Replace("\n", " ");
								}
							}
							row[i] = str;
						}
					}
					return ew_ArrayToJson(rsarr);
				} else {
					return "false";
				}
			}

			// TblFilter
			private string _TblFilter;
			public string TblFilter {
				get {
					return ew_NotEmpty(_TblFilter) ? _TblFilter : "";
				}
				set {
					_TblFilter = value;
				}
			}

			// TblBasicSearchDefault
			private string _TblBasicSearchDefault;
			public string TblBasicSearchDefault {
				get {
					return ew_NotEmpty(_TblBasicSearchDefault) ? _TblBasicSearchDefault : "";
				}
				set {
					_TblBasicSearchDefault = value;
				}
			}

			// Table level events
			// Recordset Selecting event

			public void Recordset_Selecting(ref string filter) {

				// Enter your code here
			}

			// Recordset Search Validated event
			public void Recordset_SearchValidated() {

				// Enter your code here
			}

			// Recordset Searching event
			public void Recordset_Searching(ref string filter) {

				// Enter your code here
			}

			// Row_Selecting event
			public void Row_Selecting(ref string filter) {

				// Enter your code here
			}

			// Row Selected event
			public void Row_Selected(ref OrderedDictionary od) {

				//ew_Write("Row Selected");
			}

			// Row Inserting event
			public bool Row_Inserting(OrderedDictionary rsold, ref OrderedDictionary rsnew) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Inserted event
			public void Row_Inserted(OrderedDictionary rsold, OrderedDictionary rsnew) {

				//ew_Write("Row Inserted");
			}

			// Row Updating event
			public bool Row_Updating(OrderedDictionary rsold, ref OrderedDictionary rsnew) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Updated event
			public void Row_Updated(OrderedDictionary rsold, OrderedDictionary rsnew) {

				// ew_Write("Row Updated");
			}

			// Row Update Conflict event
			public bool Row_UpdateConflict(OrderedDictionary rsold, ref OrderedDictionary rsnew) {

				// Enter your code here
				// To ignore conflict, set return value to false

				return true;
			}

			// Row Export event
			// ExportDoc = export document object

			public void Row_Export(DbDataReader rs) {

					// ExportDoc.Text += "my content"; // Build HTML with field value: $rs["MyField"] or $this->MyField->ViewValue
			}

			// Page Exporting event
			// ExportDoc = export document object

			public bool Page_Exporting() {

				//ExportDoc.Text = "my header"; // Export header
				//return false; // Return FALSE to skip default export and use Row_Export event

				return true; // Return TRUE to use default export and skip Row_Export event
			}

			// Page Exported event
			// ExportDoc = export document object

			public void Page_Exported() {

				//ExportDoc.Text += "my footer"; ' Export footer
				//ew_Write(ExportDoc.Text);

			}

			// Recordset Deleting event
			public bool Row_Deleting(OrderedDictionary rs) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Recordset Deleted event
			public void Row_Deleted(OrderedDictionary rs) {

				//ew_Write("Row Deleted");
			}

			// Email Sending event
			public bool Email_Sending(ref cEmail Email, dynamic Args) {

				//ew_End(Email);
				return true;
			}

			// Lookup Selecting event
			public void Lookup_Selecting(cField fld, ref string filter) {

				// Enter your code here
			}

			// Row Rendering event
			public void Row_Rendering() {

				// Enter your code here
			}

			// Row Rendered event
			public void Row_Rendered() {

				//ew_VarPrint(<FieldName>); // View field properties
			}

			// User ID Filtering event
			public void UserID_Filtering(ref string filter) {

				// Enter your code here
			}
		}
	}
}
