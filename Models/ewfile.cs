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

		// _file
		public static c_file _file {
			get { return (c_file)ew_ViewData["_file"]; }
			set { ew_ViewData["_file"] = value; }
		}

		//
		// Page class
		//
		// - Uncomment ** for database connectivity / Page_Loading / Page_Unloaded server event

		public class c_file : IAspNetMakerPage
		{

			// Page ID
			public string PageID = "file";

			// Project ID
			public string ProjectID = "{81F4D3E8-F4E3-43BC-AFB1-939A7CBF1C5F}";

			// Page object name
			public string PageObjName = "_file";

			// Page terminated // DN
			private bool _terminated = false;

			//
			// Page class constructor
			//

			public c_file(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
				CurrentPage = this;
				TokenTimeout = ew_SessionTimeoutTime();

				// Language object
				Language = Language ?? new cLanguage();

				// Start time
				StartTime = Environment.TickCount;

				// Open connection
				//**Conn = ew_GetConn();

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

			//  Page init
			public IActionResult Page_Init() {

				// Check token
				if (!ValidPost())
					ew_End(Language.Phrase("InvalidPostRequest")); // DN

				// Header
				ew_Header(false);

				// Create Token
				CreateToken();

				// Global Page Loading event
				//**Page_Loading();

				ew_Response.Clear(); // Clear output
				return null;
			}

			// Main
			public IActionResult Page_Main() {

				// Get fn / table name parameters
				string key = EW_RANDOM_KEY + ew_Session.SessionID;
				string fn = (ew_NotEmpty(ew_Get("fn"))) ? ew_Get("fn") : "";
				if (fn != "" && EW_ENCRYPT_FILE_PATH)
					fn = ew_Decrypt(fn, key);
				string table = (ew_NotEmpty(ew_Get("t"))) ? ew_Get("t") : "";
				if (ew_NotEmpty(table) && EW_ENCRYPT_FILE_PATH)
					table = ew_Decrypt(table, key);

				// Get resize parameters
				var resize = ew_NotEmpty(ew_Get("resize"));
				int width = ew_NotEmpty(ew_Get("width")) ? ew_ConvertToInt(ew_Get("width")) : 0;
				int height = ew_NotEmpty(ew_Get("height")) ? ew_ConvertToInt(ew_Get("height")) : 0;
				if (ew_Get("width") == "" && ew_Get("height") == "") {
					width = ew_ConvertToInt(EW_THUMBNAIL_DEFAULT_WIDTH);
					height = ew_ConvertToInt(EW_THUMBNAIL_DEFAULT_HEIGHT);
				}

				// Resize image from physical file
				if (ew_NotEmpty(fn)) {
					fn = ew_IncludeTrailingDelimiter(ew_PathCombine(ew_AppRoot(), Path.GetDirectoryName(fn), true), true) + Path.GetFileName(fn);
					if (File.Exists(fn)) { // Does not support remote path
						ew_Response.Clear();
						string ext = Path.GetExtension(fn).Replace(".", "").ToLower();
						string ct = ew_ContentType(null, fn);
						if (EW_IMAGE_ALLOWED_FILE_EXT.Contains(ext)) {
							if (width > 0 || height > 0)
								return ew_Controller.File(ew_ResizeFileToBinary(fn, ref width, ref height), ct, Path.GetFileName(fn));
							else
								return ew_Controller.PhysicalFile(fn, ct, Path.GetFileName(fn));
						} else if (EW_DOWNLOAD_ALLOWED_FILE_EXT.Contains(ext)) {
							return ew_Controller.PhysicalFile(fn, ct, Path.GetFileName(fn));
						}
					}
				}
				return new EmptyResult();
			}

			// Page_Terminate
			public IActionResult Page_Terminate(string url = "") {  // DN
				if (_terminated)
					return new EmptyResult();
				_terminated = true;
				return new EmptyResult();
			}
		}
	}
}
