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

		// _lookup
		public static c_lookup _lookup {
			get { return (c_lookup)ew_ViewData["_lookup"]; }
			set { ew_ViewData["_lookup"] = value; }
		}

		//
		// Page class
		//

		public class c_lookup : IAspNetMakerPage
		{

			// Private properties
			private string sql;
			private string dbid;

			// Page terminated // DN
			private bool _terminated = false;

			// Page class constructor
			public c_lookup(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;

				// Language object
				Language = Language ?? new cLanguage();
			}

			//  Page init
			public IActionResult Page_Init() {
				if (!IsPost)
					return ew_Controller.Content("Missing post data."); // No post data

				// Header
				ew_Header(false); // DN

				// Get post data
				sql = ew_Post("s");
				sql = ew_Decrypt(sql);
				if (ew_Empty(sql))
					return ew_Controller.Content("Missing SQL.");
				dbid = ew_Post("d");

				// Connection
				Conn = ew_GetConn(dbid);

				// Global Page Loading event
				Page_Loading();
				ew_Response.Clear(); // Clear output
				return null;
			}

			//
			// Page main
			//

			public IActionResult Page_Main() {
				var value = "";
				var fn = "";
				if (sql.Contains("{filter}")) {
					var filters = "";
					string[] keyarr = ew_Form.Keys.ToArray();
					Dictionary<string, string> ar = new Dictionary<string, string>();

					// Get the filter values (for "IN")
					foreach (var key in keyarr) {
						if (Regex.IsMatch(key, @"^f\d+$")) {
							var filter = ew_Decrypt(ew_Post(key));
							if (filter != "") {
								int i = ew_ConvertToInt(Regex.Replace(key, @"^f", ""));
								value = ew_Post("v" + Convert.ToString(i));
								if (value == "") {
									if (i > 0) { // Empty parent field

										//continue; // Allow
										ew_AddFilter(ref filters, "1=0"); // Disallow
									}
									continue;
								}
								var arValue = value.Split(EW_LOOKUP_FILTER_VALUE_SEPARATOR);
								var fldtype = ew_ConvertToInt(ew_Post("t" + Convert.ToString(i)));
								var flddatatype = ew_FieldDataType(fldtype);
								bool bValidData = true;
								for (var j = 0; j < arValue.Length; j++) {
									if (flddatatype == EW_DATATYPE_NUMBER && !ew_IsNumeric(arValue[j])) {
										bValidData = false;
										break;
									} else {
										arValue[j] = ew_QuotedValue(arValue[j], ew_FieldDataType(fldtype), dbid);
									}
								}
								if (bValidData)
									filter = filter.Replace("{filter_value}", String.Join(",", arValue));
								else
									filter = "1=0";
								fn = ew_Post("fn" + Convert.ToString(i));
								if (ew_Empty(fn))
									ew_AddFilter(ref filters, filter);
								else // DN
									ew_Invoke(fn, new object[] { filters, filter });
							}
						}
					}
					sql = sql.Replace("{filter}", (filters != "") ? filters : "1=1");
				}

				// Get the query value (for "LIKE" or "=")
				value = ew_AdjustSql(ew_Get("q"), dbid); // Get the query value from querystring
				if (value == "")
					value = ew_AdjustSql(ew_Post("q"), dbid); // Get the value from post
				if (value != "") {
					sql = Regex.Replace(sql, @"LIKE '(%)?\{query_value\}%'", ew_Like("'$1{query_value}%'", dbid));
					sql = sql.Replace("{query_value}", value);
				}

				// Replace {query_value_n}
				var mall = Regex.Matches(sql, @"\{query_value_(\d+)\}");
				for (var i = 0; i < mall.Count; i++) {
					var j = mall[i].Groups[1].Value;
					var v = ew_AdjustSql(ew_Post("q" + j), dbid);
					sql = sql.Replace("{query_value_" + j + "}", v);
				}

				// Get data
				try {
					var rsarr = Conn.GetRows(sql);
					if (rsarr == null)
						return ew_Controller.Content("Failed to execute " + sql);

					// Format date
					var ardt = new List<string>();

					// Output
					foreach (OrderedDictionary row in rsarr) {
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
					return ew_Controller.Content(ew_ArrayToJson(rsarr), "text/plain", Encoding.UTF8); // Returns utf-8 data
				} finally {
					Page_Terminate();
				}
			}

			// Page_Terminate
			public IActionResult Page_Terminate(string url = "") {  // DN
				if (_terminated)
					return new EmptyResult();
				ew_CloseConn();
				_terminated = true;
				return new EmptyResult();
			}
		}
	}
}
