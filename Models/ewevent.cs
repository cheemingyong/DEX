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

		// Static file options
		public static StaticFileOptions ew_StaticFileOptions;

		//
		// Global user code
		//
		// Static constructor

		static DEX()
		{
			var provider = new FileExtensionContentTypeProvider();
			ew_StaticFileOptions = new StaticFileOptions()
			{
				ContentTypeProvider = provider
			};

			// ContentType Mapping event
			ContentType_Mapping(provider.Mappings);

			// Class Init event
			Class_Init();
		}

		//
		// Global events
		//
		// ContentType Mapping event

		public static void ContentType_Mapping(IDictionary<string, string> Mappings) {

			// Example:
			//Mappings[".image"] = "image/png"; // Add new mappings
			//Mappings[".rtf"] = "application/x-msdownload"; // Replace an existing mapping
			//Mappings.Remove(".mp4"); // Remove MP4 videos

		}

		// Class Init event
		public static void Class_Init() {

			// Enter your code here
		}

		// Page Loading event
		public static void Page_Loading() {

			// Enter your code here
		}

		// Page Rendering event
		public static void Page_Rendering() {

			//ew_Write("Page Rendering");
		}

		// Page Unloaded event
		public static void Page_Unloaded() {

			// Enter your code here
		}

		// AuditTrail Inserting event
		public static bool AuditTrail_Inserting(OrderedDictionary rsnew) {
			return true;
		}

		//
		// Connection
		//

		public class cConnection<N, M, R, T> : cConnectionBase<N, M, R, T>
			where N : DbConnection
			where M : DbCommand
			where R : DbDataReader
		{

			// Constructor
			public cConnection(string dbid) : base(dbid)
			{
			}

			// Constructor
			public cConnection() : base()
			{
			}
		}

		// Execute SQL
		public static int ew_Execute(string Sql, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				return c.ExecuteNonQuery(Sql);
			}
		}

		// Execute SQL and return first value of first row
		public static object ew_ExecuteScalar(string Sql, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				return c.ExecuteScalar(Sql);
			}
		}

		// Execute SQL and return first value of first row as string
		// for use with As<TValue>, As<TValue>(String, TValue) and Is<TValue>

		public static string ew_ExecuteValue(string Sql, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				return Convert.ToString(c.ExecuteScalar(Sql));
			}
		}

		// Execute SQL and return first row as OrderedDictionary
		public static OrderedDictionary ew_ExecuteRow(string Sql, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				return c.GetRow(Sql);
			}
		}

		// Execute SQL and return List<OrderedDictionary>
		public static List<OrderedDictionary> ew_ExecuteRows(string Sql, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				return c.GetRows(Sql);
			}
		}

		// Executes the query, and returns the row(s) as JSON
		public static string ew_ExecuteJson(string Sql, bool FirstOnly = true, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				if (FirstOnly) {
					var list = new List<OrderedDictionary>();
					list.Add(c.GetRow(Sql));
					return JsonConvert.SerializeObject(list);
				} else {
					return JsonConvert.SerializeObject(c.GetRows(Sql));
				}
			}
		}

		// Execute SQL and return first row
		public static DbDataRecord ew_ExecuteRecord(string Sql, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				return c.GetRecord(Sql);
			}
		}

		// Execute SQL and return List<DbDataRecord>
		public static List<DbDataRecord> ew_ExecuteRecords(string Sql, string dbid = "DB")
		{
			using (var c = ew_CreateConn(dbid)) {
				return c.GetRecords(Sql);
			}
		}

		//
		// Advanced Security
		//

		public class cAdvancedSecurity : cAdvancedSecurityBase {

			// Constructor
			public cAdvancedSecurity() : base() {
			}
		}

		//
		// Menu
		//

		public class cMenu : cMenuBase {

			// Constructor
			public cMenu(object MenuId, bool Mobile = false) : base(MenuId, Mobile) {
			}

			// Render
			public override string Render(bool ret = false) {
				if (IsRoot)
					Menu_Rendering(this);
				return base.Render(ret);
			}
		}
	}
}
