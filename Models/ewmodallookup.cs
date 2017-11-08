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
namespace AspNetMaker2017.Models
{
	public partial class DEX
	{

		// _modallookup
		public static c_modallookup _modallookup
		{
			get { return (c_modallookup)ew_ViewData["_modallookup"]; }
			set { ew_ViewData["_modallookup"] = value; }
		}

		//
		// Page class
		//

		public class c_modallookup : IAspNetMakerPage
		{

			// Private properties
			private static string _sql;
			private static string _dbid;

			// Page terminated // DN
			private bool _terminated = false;

			// Page class constructor
			public c_modallookup(Controller controller = null)
			{ // DN
				if (controller != null)
					ew_Controller = controller;
			}

			//  Page init
			public IActionResult Page_Init()
			{
				if (!IsPost)
					return ew_Controller.Content("Missing post data."); // No post data

				// Language object
				Language = new cLanguage("", ew_Post("lang"));

				// Header
				ew_Header(false); // DN

				// Get post data
				_sql = ew_Post("s");
				_sql = ew_Decrypt(_sql);
				if (ew_Empty(_sql))
					return ew_Controller.Content("Missing SQL.");
				_dbid = ew_Empty(ew_Post("d")) ? "DB" : ew_Post("d");

				// Connection
				Conn = ew_GetConn(_dbid);
				return null;
			}
			public string SQL;
			public List<OrderedDictionary> Recordset;
			public int TotalRecs;
			public int RowCnt;
			public int ColSpan = 1;
			public int RecCount;
			public int StartOffset = 0; // 0-based, not StartRec which is 1-based
			public string LookupTable;
			public string LookupTableCaption;
			public string LinkField;
			public string LinkFieldCaption;
			public string[] DisplayFields = new string[4];
			public string[] DisplayFieldCaptions = new string[4];
			public static string[] DisplayFieldExpressions = new string[4];
			public List<string> ParentFields = new List<string>();
			public bool Multiple = false;
			public int PageSize = 10;
			public static string SearchValue = "";
			public string SearchFilter = "";
			public static string SearchType = ""; // Auto ("=" => Exact Match, "AND" => All Keywords, "OR" => Any Keywords)
			public string filter;

			//
			// Page main
			//

			public IActionResult Page_Main()
			{
				string value = "";
				string fn = "";
				string Action, filterwrk;
				int fldtype, flddatatype;
				filter = ew_Post("f0");
				filter = ew_Decrypt(filter);
				Multiple = ew_Post("m") == "1";
				if (ew_NotEmpty(ew_Post("n")))
					PageSize = ew_ConvertToInt(ew_Post("n"));
				Action = ew_Post("action");
				if (ew_NotEmpty(ew_Post("start")))
					StartOffset = ew_ConvertToInt(ew_Post("start"));

				// Load lookup table/field names
				LookupTable = ew_Post("lt");
				if (LookupTable == "")
					return ew_Controller.Content("Missing lookup table.");
				LookupTableCaption = Language.TablePhrase(LookupTable, "TblCaption");
				LinkField = ew_Post("lf");
				if (LinkField == "")
					return ew_Controller.Content("Missing link field.");
				LinkFieldCaption = Language.FieldPhrase(LookupTable, LinkField, "FldCaption");
				string[] keyarr = ew_Form.Keys.ToArray();
				Dictionary<string, string> ar = new Dictionary<string, string>();
				foreach (var key in keyarr) {
					if (Regex.IsMatch(key, @"^ldf\d+$")) {
						int i = ew_ConvertToInt(Regex.Replace(key, @"^ldf", ""));
						string fldvar = ew_Post(key);
						if (fldvar != "") {
							string fldcaption = Language.FieldPhrase(LookupTable, fldvar, "FldCaption");
							if (fldcaption == "")
								fldcaption = fldvar;
							DisplayFields[i-1] = fldvar;
							DisplayFieldCaptions[i-1] = fldcaption;
							DisplayFieldExpressions[i-1]= ew_Decrypt(ew_Post("dx" + i));
							ColSpan++;
						}
					}
				}

				// Load search filter / selected key values
				fldtype = ew_ConvertToInt(ew_Post("t0"));
				flddatatype = ew_FieldDataType(fldtype);
				if (ew_NotEmpty(ew_Post("sv"))) {
					SearchValue = ew_Post("sv");
					SearchFilter = GetSearchFilter();
					filter = "";
				} else if (ew_NotEmpty(ew_Post("keys"))) {
					string[] arKeys = ew_Post("keys").Split(EW_LOOKUP_FILTER_VALUE_SEPARATOR);
					if (ew_IsArray(arKeys) && arKeys.Length > 0) {
						filterwrk = "";
						int cnt = arKeys.Length;
						for (var i = 0; i < cnt; i++) {
							arKeys[i] = ew_QuotedValue(arKeys[i], flddatatype, _dbid);
							filterwrk += ((filterwrk != "") ? " OR " : "") + filter.Replace("{filter_value}", arKeys[i]);
						}
						filter = filterwrk;
						PageSize = -1;
					} else {
						filter = "1=0";
					}
				} else {
					filter = "";
				}
				string filters = "";
				keyarr = ew_Form.Keys.ToArray();

				//Dictionary<string, string> ar = new Dictionary<string, string>();
				// Get the filter values (for "IN")

				foreach (var key in keyarr) {
					if (Regex.IsMatch(key, @"^f\d+$")) {
						var filter2 = ew_Decrypt(ew_Post(key));
						if (filter2 != "") {
							int i = ew_ConvertToInt(Regex.Replace(key, @"^f", ""));
							value = ew_Post("v" + Convert.ToString(i));
							if (value == "") {
								if (i > 0) { // Empty parent field

									//continue; // Allow
									ew_AddFilter(ref filters, "1=0"); // Disallow
								}
								continue;
							}
							ParentFields.Add(Convert.ToString(i));
							var arValue = value.Split(EW_LOOKUP_FILTER_VALUE_SEPARATOR);
							fldtype = ew_ConvertToInt(ew_Post("t" + Convert.ToString(i)));
							flddatatype = ew_FieldDataType(fldtype);
							bool bValidData = true;
							for (var j = 0; j < arValue.Length; j++) {
								if (flddatatype == EW_DATATYPE_NUMBER && !ew_IsNumeric(arValue[j])) {
									bValidData = false;
									break;
								} else {
									arValue[j] = ew_QuotedValue(arValue[j], ew_FieldDataType(fldtype), _dbid);
								}
							}
							if (bValidData)
								filter2 = filter2.Replace("{filter_value}", String.Join(",", arValue));
							else
								filter2 = "1=0";
							fn = ew_Post("fn" + Convert.ToString(i));
							if (ew_Empty(fn))
								ew_AddFilter(ref filters, filter2);
							else // DN
								ew_Invoke(fn, new object[] { filters, filter2 });
						}
					}
				}
				string where = ""; // Initialize
				if (SearchFilter != "" && SearchValue != "")
					ew_AddFilter(ref where, SearchFilter);
				if (filter != "")
					ew_AddFilter(ref where, filter);
				if (filters != "")
					ew_AddFilter(ref where, filters);
				_sql = _sql.Replace("{filter}", (where != "") ? where : "1=1");
				SQL = _sql;

				//Page_Error(sql); // Show SQL for debugging
				// Get records

				TotalRecs = GetRecordCount(_sql);
				if (PageSize > 0)
					Recordset = Conn.GetRows(Conn.SelectLimit(SQL, PageSize, StartOffset, Regex.IsMatch(SQL, @"\/\*BeginOrderBy\*\/[\s\S]+\/\*EndOrderBy\*\/")));
				if (Recordset == null)
					Recordset = Conn.GetRows(SQL);
				if (!Conn.SelectOffset)
					Recordset.RemoveRange(0, StartOffset);

				// Return JSON
				return Page_Response();
			}

			// Get search filter
			public static string GetSearchFilter()
			{
				if (SearchValue.Trim() == "")
					return "";
				string searchStr = "";
				string search = SearchValue.Trim();
				string searchType = SearchType;
				if (searchType != "=") {
					List<string> ar = new List<string>();
					foreach (Match match in Regex.Matches(search, @"""([^""]*)""", RegexOptions.IgnoreCase)) {
						int p = search.IndexOf(Convert.ToString(match.Groups[0]));
						string str = search.Substring(0, p);
						search = search.Substring(p + match.Groups[0].Length);
						if (ew_NotEmpty(str))
							ar.AddRange(str.Trim().Split(' '));
						ar.Add(match.Groups[1].Value); // Save quoted keyword
					}

					// Match individual keywords
					if (ew_NotEmpty(search))
						ar.AddRange(search.Trim().Split(' '));

					// Search keyword in any fields
					if (searchType == "OR" || searchType == "AND") {
						string searchFilter = "";
						foreach (var sKeyword in ar) {
							if (sKeyword != "") {
								searchFilter = GetSearchSQL(new List<string>() { sKeyword });
								if (searchStr != "")
									searchStr += " " + searchType + " ";
								searchStr += "(" + searchFilter + ")";
							}
						}
					} else {
						searchStr = GetSearchSQL(ar);
					}
				} else {
					searchStr = GetSearchSQL(new List<string>() { search });
				}
				return searchStr;
			}

			// Get search SQL
			public static string GetSearchSQL(List<string> arKeywords)
			{
				string sWhere = "";
				foreach (string sql in DisplayFieldExpressions) {
					if (ew_NotEmpty(sql)) {
						BuildSearchSQL(ref sWhere, sql, arKeywords);
					}
				}
				return sWhere;
			}

			// Build search SQL
			public static void BuildSearchSQL(ref string Where, string FldExpr, List<string> arKeywords)
			{
				string sSearchType = SearchType;
				string sDefCond = (sSearchType == "OR") ? "OR" : "AND";
				var arSQL = new List<string>(); // Array for SQL parts
				var arCond = new List<string>(); // Array for search conditions
				int cnt = arKeywords.Count;
				int j = 0; // Number of SQL parts
				for (int i = 0; i < cnt; i++) {
					string Keyword = arKeywords[i];
					Keyword = Keyword.Trim();
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
							if (aKeyword == "OR" && sSearchType == "") {
								if (j > 0)
									arCond[j-1] = "OR";
							} else {
								sWrk = FldExpr + ew_Like(ew_QuotedValue("%" + aKeyword + "%", EW_DATATYPE_STRING, _dbid), _dbid);
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

			// Try to get record count
			public int GetRecordCount(string sql)
			{
				int cnt = 0;
				string sqlwrk;
				string pattern = @"^SELECT\s([\s\S]+)?\sFROM\s";
				sql = Regex.Replace(sql, @"\/\*BeginOrderBy\*\/[\s\S]+\/\*EndOrderBy\*\/", ""); // Remove ORDER BY clause (MSSQL)
				try {
					if (Regex.IsMatch(sql, pattern)) {
						sqlwrk = "SELECT COUNT(*) FROM " + Regex.Replace(sql, pattern, "");
					} else {
						sqlwrk = "SELECT COUNT(*) FROM (" + sql + ") EW_COUNT_TABLE";
					}
					cnt = ew_ConvertToInt(Conn.ExecuteScalar(sqlwrk));
				} catch { // Unable to get count, get record count directly
					try {
						using (var dr = Conn.OpenDataReader(sql)) {
							while (dr.Read())
								cnt++;
						}
					} catch {
						if (EW_DEBUG_ENABLED)
							throw;
						return -1;
					}
				}
				return cnt;
			}

			// Page_Terminate
			public IActionResult Page_Terminate(string url = "")
			{  // DN
				if (_terminated)
					return new EmptyResult();
				ew_CloseConn();
				_terminated = true;
				return new EmptyResult();
			}

			// Show page response
			public IActionResult Page_Response()
			{
				if (Recordset == null) {
					IDictionary<string, string> result = new Dictionary<string, string>();
					result.Add("Result", "ERROR");
					result.Add("Message", "Failed to execute SQL");
					if (EW_DEBUG_ENABLED)
						result["Message"] += ": " + SQL; // To be viewed in browser Network panel for debugging
					return ew_Controller.Content(ew_ArrayToJson(result), "text/plain", Encoding.UTF8); // Returns utf-8 data
				}

				// Format date
				var ardt = new List<string>();

				// Output
				foreach (OrderedDictionary row in Recordset) {
					for (var i = 0; i < row.Count; i++) {
						string str = Convert.ToString(row[i]);
						ardt.Add(ew_Post("df" + i));
						if (ew_NotEmpty(ardt[i]) && ew_ConvertToInt(ardt[i]) >= 0) // Format date
							str = ew_FormatDateTime(str, ew_ConvertToInt(ardt[i]));
						if (ew_Empty(ew_Post("keepHTML")))
							str = ew_RemoveHtml(str);
						if (str.Contains("\r") || str.Contains("\n") || str.Contains("\t")) {
							if (ew_NotEmpty(ew_Post("keepCRLF"))) {
								str = str.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t");
							} else {
								str = str.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
							}
						}
						row[i] = str;
					}
				}
				return ew_Controller.Content("{\"Result\": \"OK\", \"Records\": " + ew_ArrayToJson(Recordset) + ", \"TotalRecordCount\": " + TotalRecs + "}", "text/plain", Encoding.UTF8); // Returns utf-8 data
			}

			// Show page error
			public IActionResult Page_Error(string msg)
			{
				IDictionary<string, string> result = new Dictionary<string, string>()
				{
					{"Result", "ERROR"},
					{"Message", msg}
				};
				return ew_Controller.Content(ew_ArrayToJson(result), "text/plain", Encoding.UTF8); // Returns utf-8 data
			}
		}
	}
}
