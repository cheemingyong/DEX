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

		// CurrencyRule
		public static cCurrencyRule CurrencyRule {
			get { return (cCurrencyRule)ew_ViewData["CurrencyRule"]; }
			set { ew_ViewData["CurrencyRule"] = value; }
		}

		//
		// Table class for CurrencyRule
		//

		public class cCurrencyRule: cTable {
			public cField id;
			public cField RuleType;
			public cField AgentId;
			public cField CurrencyCode;
			public cField TransactionType;
			public cField TransactionTypeCondition;
			public cField SingleTransactionBuyAmount;
			public cField TransactionPeriodType;
			public cField PeriodStart;
			public cField PeriodEnd;
			public cField TransactionPeriodCondition;
			public cField PeriodBuyAmount;
			public cField NoOfTransactions;
			public cField FeeCost;
			public cField CreatedDate;
			public cField CreatedBy;
			public int RowCnt = 0; // DN

			//
			// Table class constructor
			//

			public cCurrencyRule() {

				// Language object // DN
				Language = Language ?? new cLanguage();
				TableVar = "CurrencyRule";
				TableName = "CurrencyRule";
				TableType = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[CurrencyRule]";
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

				// id
				id = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_id",
					FldName = "id",
					FldExpression = "[id]",
					FldBasicSearchExpression = "CAST([id] AS NVARCHAR)",
					FldType = 3,
					FldDbType = SqlDbType.Int,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[id]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "NO",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				id.Init();
				id.SetupLookupFilters = SetupLookupFilters;
				id.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("id", id);

				// RuleType
				RuleType = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_RuleType",
					FldName = "RuleType",
					FldExpression = "[RuleType]",
					FldBasicSearchExpression = "[RuleType]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[RuleType]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "RADIO",
					Sortable = true, // Allow sort
					OptionCount = 2,
					IsUpload = false
				};
				RuleType.Init();
				RuleType.SetupLookupFilters = SetupLookupFilters;
				RuleType.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("RuleType", RuleType);

				// AgentId
				AgentId = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
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

				// CurrencyCode
				CurrencyCode = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_CurrencyCode",
					FldName = "CurrencyCode",
					FldExpression = "[CurrencyCode]",
					FldBasicSearchExpression = "[CurrencyCode]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[CurrencyCode]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				CurrencyCode.Init();
				CurrencyCode.SetupLookupFilters = SetupLookupFilters;
				CurrencyCode.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("CurrencyCode", CurrencyCode);

				// TransactionType
				TransactionType = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_TransactionType",
					FldName = "TransactionType",
					FldExpression = "[TransactionType]",
					FldBasicSearchExpression = "[TransactionType]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[TransactionType]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "RADIO",
					Sortable = true, // Allow sort
					OptionCount = 2,
					IsUpload = false
				};
				TransactionType.Init();
				TransactionType.SetupLookupFilters = SetupLookupFilters;
				TransactionType.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("TransactionType", TransactionType);

				// TransactionTypeCondition
				TransactionTypeCondition = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_TransactionTypeCondition",
					FldName = "TransactionTypeCondition",
					FldExpression = "[TransactionTypeCondition]",
					FldBasicSearchExpression = "[TransactionTypeCondition]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[TransactionTypeCondition]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "RADIO",
					Sortable = true, // Allow sort
					OptionCount = 5,
					IsUpload = false
				};
				TransactionTypeCondition.Init();
				TransactionTypeCondition.SetupLookupFilters = SetupLookupFilters;
				TransactionTypeCondition.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("TransactionTypeCondition", TransactionTypeCondition);

				// SingleTransactionBuyAmount
				SingleTransactionBuyAmount = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_SingleTransactionBuyAmount",
					FldName = "SingleTransactionBuyAmount",
					FldExpression = "[SingleTransactionBuyAmount]",
					FldBasicSearchExpression = "CAST([SingleTransactionBuyAmount] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[SingleTransactionBuyAmount]",
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
				SingleTransactionBuyAmount.Init();
				SingleTransactionBuyAmount.SetupLookupFilters = SetupLookupFilters;
				SingleTransactionBuyAmount.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("SingleTransactionBuyAmount", SingleTransactionBuyAmount);

				// TransactionPeriodType
				TransactionPeriodType = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_TransactionPeriodType",
					FldName = "TransactionPeriodType",
					FldExpression = "[TransactionPeriodType]",
					FldBasicSearchExpression = "[TransactionPeriodType]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[TransactionPeriodType]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "RADIO",
					Sortable = true, // Allow sort
					OptionCount = 6,
					IsUpload = false
				};
				TransactionPeriodType.Init();
				TransactionPeriodType.SetupLookupFilters = SetupLookupFilters;
				TransactionPeriodType.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("TransactionPeriodType", TransactionPeriodType);

				// PeriodStart
				PeriodStart = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_PeriodStart",
					FldName = "PeriodStart",
					FldExpression = "[PeriodStart]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[PeriodStart]", 7, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 7,
					FldVirtualExpression = "[PeriodStart]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDateDMY").Replace("%s", EW_DATE_SEPARATOR),
					IsUpload = false
				};
				PeriodStart.Init();
				PeriodStart.SetupLookupFilters = SetupLookupFilters;
				PeriodStart.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("PeriodStart", PeriodStart);

				// PeriodEnd
				PeriodEnd = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_PeriodEnd",
					FldName = "PeriodEnd",
					FldExpression = "[PeriodEnd]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[PeriodEnd]", 7, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 7,
					FldVirtualExpression = "[PeriodEnd]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDateDMY").Replace("%s", EW_DATE_SEPARATOR),
					IsUpload = false
				};
				PeriodEnd.Init();
				PeriodEnd.SetupLookupFilters = SetupLookupFilters;
				PeriodEnd.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("PeriodEnd", PeriodEnd);

				// TransactionPeriodCondition
				TransactionPeriodCondition = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_TransactionPeriodCondition",
					FldName = "TransactionPeriodCondition",
					FldExpression = "[TransactionPeriodCondition]",
					FldBasicSearchExpression = "[TransactionPeriodCondition]",
					FldType = 200,
					FldDbType = SqlDbType.VarChar,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[TransactionPeriodCondition]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "RADIO",
					Sortable = true, // Allow sort
					OptionCount = 5,
					IsUpload = false
				};
				TransactionPeriodCondition.Init();
				TransactionPeriodCondition.SetupLookupFilters = SetupLookupFilters;
				TransactionPeriodCondition.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("TransactionPeriodCondition", TransactionPeriodCondition);

				// PeriodBuyAmount
				PeriodBuyAmount = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_PeriodBuyAmount",
					FldName = "PeriodBuyAmount",
					FldExpression = "[PeriodBuyAmount]",
					FldBasicSearchExpression = "CAST([PeriodBuyAmount] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[PeriodBuyAmount]",
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
				PeriodBuyAmount.Init();
				PeriodBuyAmount.SetupLookupFilters = SetupLookupFilters;
				PeriodBuyAmount.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("PeriodBuyAmount", PeriodBuyAmount);

				// NoOfTransactions
				NoOfTransactions = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_NoOfTransactions",
					FldName = "NoOfTransactions",
					FldExpression = "[NoOfTransactions]",
					FldBasicSearchExpression = "CAST([NoOfTransactions] AS NVARCHAR)",
					FldType = 3,
					FldDbType = SqlDbType.Int,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[NoOfTransactions]",
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
				NoOfTransactions.Init();
				NoOfTransactions.SetupLookupFilters = SetupLookupFilters;
				NoOfTransactions.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("NoOfTransactions", NoOfTransactions);

				// FeeCost
				FeeCost = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_FeeCost",
					FldName = "FeeCost",
					FldExpression = "[FeeCost]",
					FldBasicSearchExpression = "CAST([FeeCost] AS NVARCHAR)",
					FldType = 131,
					FldDbType = SqlDbType.Decimal,
					FldDateTimeFormat = -1,
					FldVirtualExpression = "[FeeCost]",
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
				FeeCost.Init();
				FeeCost.SetupLookupFilters = SetupLookupFilters;
				FeeCost.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("FeeCost", FeeCost);

				// CreatedDate
				CreatedDate = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
					FldVar = "x_CreatedDate",
					FldName = "CreatedDate",
					FldExpression = "[CreatedDate]",
					FldBasicSearchExpression = ew_CastDateFieldForLike("[CreatedDate]", 7, "DB"),
					FldType = 135,
					FldDbType = SqlDbType.DateTime,
					FldDateTimeFormat = 7,
					FldVirtualExpression = "[CreatedDate]",
					FldIsVirtual = false,
					FldForceSelection = false,
					FldSelectMultiple = false,
					FldVirtualSearch = false,
					FldViewTag = "FORMATTED TEXT",
					FldHtmlTag = "TEXT",
					Sortable = true, // Allow sort
					FldDefaultErrMsg = Language.Phrase("IncorrectDateDMY").Replace("%s", EW_DATE_SEPARATOR),
					IsUpload = false
				};
				CreatedDate.Init();
				CreatedDate.SetupLookupFilters = SetupLookupFilters;
				CreatedDate.SetupAutoSuggestFilters = SetupAutoSuggestFilters;
				Fields.Add("CreatedDate", CreatedDate);

				// CreatedBy
				CreatedBy = new cField<SqlDbType> {
					TblVar = "CurrencyRule",
					TblName = "CurrencyRule",
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

			// Table level SQL
			// FROM

			private string _SqlFrom = "";
			public string SqlFrom {
				get { return ew_NotEmpty(_SqlFrom) ? _SqlFrom : "[dbo].[CurrencyRule]"; }
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

					// Get insert id if necessary
					id.DbValue = Connection.GetLastInsertId();
					rs["id"] = id.DbValue;
				}
				return result;
			}

			// Update
			#pragma warning disable 168, 219
			public int Update(OrderedDictionary rs, object where = null, OrderedDictionary rsold = null, bool curfilter = true) {
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
				string Sql = "DELETE FROM " + UpdateTable + " WHERE ";
				string filter = curfilter ? CurrentFilter : "";
				if (ew_IsDictionary(where))
					swhere = ArrayToFilter((OrderedDictionary)where);
				else
					swhere = (string)where;
				ew_AddFilter(ref filter, swhere);
				if (rs != null) {
					cField fld;
					fld = FieldByName("id");
					ew_AddFilter(ref filter, fld.FldExpression + "=" + ew_QuotedValue(rs["id"], fld.FldDataType, DBID));
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
					return "[id] = @id@";
				}
			}

			// Key filter
			public string KeyFilter {
				get {
					string sKeyFilter = SqlKeyFilter;
					if (!ew_IsNumeric(id.CurrentValue))
						sKeyFilter = "0=1";	// Invalid key
					sKeyFilter = sKeyFilter.Replace("@id@", ew_AdjustSql(id.CurrentValue, DBID)); // Replace key value
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
						return "CurrencyRulelist";
					}
				}
				set {
					ew_Session[EW_PROJECT_NAME + "_" + TableVar + "_" + EW_TABLE_RETURN_URL] = value;
				}
			}

			// List URL
			public string ListUrl {
				get {
					return "CurrencyRulelist";
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
					url = KeyUrl("CurrencyRuleview", UrlParm(parm));
				else
					url = KeyUrl("CurrencyRuleview", UrlParm(EW_TABLE_SHOW_DETAIL + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "CurrencyRuleadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (ew_NotEmpty(parm))
					url = "CurrencyRuleadd?" + UrlParm(parm);
				else
					url = "CurrencyRuleadd";
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
				url = KeyUrl("CurrencyRuleedit", UrlParm(parm));
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
				url = KeyUrl("CurrencyRuleadd", UrlParm(parm));
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
					var url = KeyUrl("CurrencyRuledelete", UrlParm());
					return ew_AppPath(url); // DN
				}
			}

			// Add master URL
			public string AddMasterUrl(string url) {
				return url;
			}
			public string KeyToJson() {
				string json = "";
						json += "id:" + ew_VarToJson(id.CurrentValue, "number", "'");
				return "{" + json + "}";
			}

			// Add key value to URL
			public string KeyUrl(string url, string parm = "") { // DN
				string sUrl = url;
				if (!Convert.IsDBNull(id.CurrentValue)) {
					sUrl += "/" + id.CurrentValue;
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
					if (RouteValues.ContainsKey("id")) { // id
						key = Convert.ToString(RouteValues["id"]);
					} else if (IsPost) {
						key = ew_Post("id");
					} else {
						key = ew_Get("id");
					}
			        arKeys.Add(key);
			    }

			    // Check keys
			    foreach (var keys in arKeys) {
			        if (!ew_IsNumeric(keys)) // id
			            continue;
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
					id.CurrentValue = keys;
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
				id.DbValue = rs["id"];
				RuleType.DbValue = rs["RuleType"];
				AgentId.DbValue = rs["AgentId"];
				CurrencyCode.DbValue = rs["CurrencyCode"];
				TransactionType.DbValue = rs["TransactionType"];
				TransactionTypeCondition.DbValue = rs["TransactionTypeCondition"];
				SingleTransactionBuyAmount.DbValue = rs["SingleTransactionBuyAmount"];
				TransactionPeriodType.DbValue = rs["TransactionPeriodType"];
				PeriodStart.DbValue = rs["PeriodStart"];
				PeriodEnd.DbValue = rs["PeriodEnd"];
				TransactionPeriodCondition.DbValue = rs["TransactionPeriodCondition"];
				PeriodBuyAmount.DbValue = rs["PeriodBuyAmount"];
				NoOfTransactions.DbValue = rs["NoOfTransactions"];
				FeeCost.DbValue = rs["FeeCost"];
				CreatedDate.DbValue = rs["CreatedDate"];
				CreatedBy.DbValue = rs["CreatedBy"];
			}

			// Render list row values
			public void RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

		   // Common render codes
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

				// Call Row Rendered event
				Row_Rendered();
			}

			// Render edit row values
			public void RenderEditRow() {

				// Call Row Rendering event
					Row_Rendering();

			// id
			id.EditAttrs["class"] = "form-control";
			id.EditValue = id.CurrentValue;

			// RuleType
			RuleType.EditValue = RuleType.Options(false);

			// AgentId
			AgentId.EditAttrs["class"] = "form-control";
			AgentId.EditValue = AgentId.CurrentValue; // DN
			AgentId.PlaceHolder = ew_RemoveHtml(AgentId.FldCaption);

			// CurrencyCode
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
							if (id.Exportable) Doc.ExportCaption(id);
							if (RuleType.Exportable) Doc.ExportCaption(RuleType);
							if (AgentId.Exportable) Doc.ExportCaption(AgentId);
							if (CurrencyCode.Exportable) Doc.ExportCaption(CurrencyCode);
							if (TransactionType.Exportable) Doc.ExportCaption(TransactionType);
							if (TransactionTypeCondition.Exportable) Doc.ExportCaption(TransactionTypeCondition);
							if (SingleTransactionBuyAmount.Exportable) Doc.ExportCaption(SingleTransactionBuyAmount);
							if (TransactionPeriodType.Exportable) Doc.ExportCaption(TransactionPeriodType);
							if (PeriodStart.Exportable) Doc.ExportCaption(PeriodStart);
							if (PeriodEnd.Exportable) Doc.ExportCaption(PeriodEnd);
							if (TransactionPeriodCondition.Exportable) Doc.ExportCaption(TransactionPeriodCondition);
							if (PeriodBuyAmount.Exportable) Doc.ExportCaption(PeriodBuyAmount);
							if (NoOfTransactions.Exportable) Doc.ExportCaption(NoOfTransactions);
							if (FeeCost.Exportable) Doc.ExportCaption(FeeCost);
							if (CreatedDate.Exportable) Doc.ExportCaption(CreatedDate);
							if (CreatedBy.Exportable) Doc.ExportCaption(CreatedBy);
						} else {
							if (id.Exportable) Doc.ExportCaption(id);
							if (RuleType.Exportable) Doc.ExportCaption(RuleType);
							if (AgentId.Exportable) Doc.ExportCaption(AgentId);
							if (CurrencyCode.Exportable) Doc.ExportCaption(CurrencyCode);
							if (TransactionType.Exportable) Doc.ExportCaption(TransactionType);
							if (TransactionTypeCondition.Exportable) Doc.ExportCaption(TransactionTypeCondition);
							if (SingleTransactionBuyAmount.Exportable) Doc.ExportCaption(SingleTransactionBuyAmount);
							if (TransactionPeriodType.Exportable) Doc.ExportCaption(TransactionPeriodType);
							if (PeriodStart.Exportable) Doc.ExportCaption(PeriodStart);
							if (PeriodEnd.Exportable) Doc.ExportCaption(PeriodEnd);
							if (TransactionPeriodCondition.Exportable) Doc.ExportCaption(TransactionPeriodCondition);
							if (PeriodBuyAmount.Exportable) Doc.ExportCaption(PeriodBuyAmount);
							if (NoOfTransactions.Exportable) Doc.ExportCaption(NoOfTransactions);
							if (FeeCost.Exportable) Doc.ExportCaption(FeeCost);
							if (CreatedDate.Exportable) Doc.ExportCaption(CreatedDate);
							if (CreatedBy.Exportable) Doc.ExportCaption(CreatedBy);
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
								if (id.Exportable) Doc.ExportField(id);
								if (RuleType.Exportable) Doc.ExportField(RuleType);
								if (AgentId.Exportable) Doc.ExportField(AgentId);
								if (CurrencyCode.Exportable) Doc.ExportField(CurrencyCode);
								if (TransactionType.Exportable) Doc.ExportField(TransactionType);
								if (TransactionTypeCondition.Exportable) Doc.ExportField(TransactionTypeCondition);
								if (SingleTransactionBuyAmount.Exportable) Doc.ExportField(SingleTransactionBuyAmount);
								if (TransactionPeriodType.Exportable) Doc.ExportField(TransactionPeriodType);
								if (PeriodStart.Exportable) Doc.ExportField(PeriodStart);
								if (PeriodEnd.Exportable) Doc.ExportField(PeriodEnd);
								if (TransactionPeriodCondition.Exportable) Doc.ExportField(TransactionPeriodCondition);
								if (PeriodBuyAmount.Exportable) Doc.ExportField(PeriodBuyAmount);
								if (NoOfTransactions.Exportable) Doc.ExportField(NoOfTransactions);
								if (FeeCost.Exportable) Doc.ExportField(FeeCost);
								if (CreatedDate.Exportable) Doc.ExportField(CreatedDate);
								if (CreatedBy.Exportable) Doc.ExportField(CreatedBy);
							} else {
								if (id.Exportable) Doc.ExportField(id);
								if (RuleType.Exportable) Doc.ExportField(RuleType);
								if (AgentId.Exportable) Doc.ExportField(AgentId);
								if (CurrencyCode.Exportable) Doc.ExportField(CurrencyCode);
								if (TransactionType.Exportable) Doc.ExportField(TransactionType);
								if (TransactionTypeCondition.Exportable) Doc.ExportField(TransactionTypeCondition);
								if (SingleTransactionBuyAmount.Exportable) Doc.ExportField(SingleTransactionBuyAmount);
								if (TransactionPeriodType.Exportable) Doc.ExportField(TransactionPeriodType);
								if (PeriodStart.Exportable) Doc.ExportField(PeriodStart);
								if (PeriodEnd.Exportable) Doc.ExportField(PeriodEnd);
								if (TransactionPeriodCondition.Exportable) Doc.ExportField(TransactionPeriodCondition);
								if (PeriodBuyAmount.Exportable) Doc.ExportField(PeriodBuyAmount);
								if (NoOfTransactions.Exportable) Doc.ExportField(NoOfTransactions);
								if (FeeCost.Exportable) Doc.ExportField(FeeCost);
								if (CreatedDate.Exportable) Doc.ExportField(CreatedDate);
								if (CreatedBy.Exportable) Doc.ExportField(CreatedBy);
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
