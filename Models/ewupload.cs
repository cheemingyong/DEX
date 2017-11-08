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

		// _ewupload
		public static c_ewupload _ewupload {
			get { return (c_ewupload)ew_ViewData["_ewupload"]; }
			set { ew_ViewData["_ewupload"] = value; }
		}

		//
		// Page class
		//

		public class c_ewupload : IAspNetMakerPage
		{
			public string UploadTable;

			// Page terminated // DN
			private bool _terminated = false;

			// Download file content
			public IActionResult DownloadFileContent() {
				var name = ew_Get("id");
				UploadTable = ew_Get("table");
				var filename = ew_Get(name);
				var folder = ew_UploadTempPath(name, UploadTable);
				var version = ew_Get("version");
				if (ew_NotEmpty(version))
					folder = ew_PathCombine(folder, version, true);

				// Show file content
				var file = ew_IncludeTrailingDelimiter(folder, true) + filename;
				if (ew_FileExists(file)) {
					var value = File.ReadAllBytes(file);
					ew_AddHeader("X-Content-Type-Options", "nosniff");
					var contentType = ew_ContentType(value.Take(11), filename);
					return ew_Controller.File(value, contentType, filename);
				}
				return new EmptyResult();
			}

			// Delete file
			public IActionResult DeleteFile() {
				if (ew_Get("id") != "") {
					var name = ew_Get("id");
					UploadTable = ew_Get("table");
					var filename = ew_Get(name);
					var folder = ew_UploadTempPath(name, UploadTable);
					ew_DeleteFile(ew_IncludeTrailingDelimiter(folder, true) + filename);
					var version = EW_UPLOAD_THUMBNAIL_FOLDER;
					folder = ew_PathCombine(folder, version, true);
					ew_DeleteFile(ew_IncludeTrailingDelimiter(folder, true) + filename);
					return ew_Controller.Content("{\"success\": true}", "text/plain");
				}
				return new EmptyResult();
			}

			// Download file list
			public IActionResult DownloadFileList() {
				var name = ew_Get("id");
				UploadTable = ew_Get("table");
				var files = new List<object[]>();
				if (name != "") {
					var folder = ew_UploadTempPath(name, UploadTable);
					if (Directory.Exists(folder)) {
						var ar = Directory.GetFiles(folder);
						foreach (var file in ar) {
							var value = File.ReadAllBytes(file);
							var filesize = value.Length;
							var filetype = ew_ContentType(value.Take(11), file);
							files.Add(new object[] {name, Path.GetFileName(file), filetype, filesize});
						}
					}
					return OutputJSON(name, files);
				}
				return new EmptyResult();
			}

			// Upload file
			public IActionResult UploadFile() {
				if (ew_Request.Form.Files.Count > 0) { // DN
					var Language = new cLanguage();
					var ObjForm = new cFormObj();
					var name = ObjForm.GetValue("id");
					UploadTable = ObjForm.GetValue("table");
					var folder = ew_UploadTempPath(name, UploadTable);
					var exts = ObjForm.GetValue("exts");
					var extList = exts.Split(',');
					var allowedExtList = new List<string>(EW_UPLOAD_ALLOWED_FILE_EXT.Split(','));
					exts = String.Join(",", extList.Where(ext => allowedExtList.Contains(ext, StringComparer.OrdinalIgnoreCase))); // Make sure exts is a subset of EW_UPLOAD_ALLOWED_FILE_EXT
					if (ew_Empty(exts))
						exts = EW_UPLOAD_ALLOWED_FILE_EXT;
					var filetypes = ".(" + exts.Replace(",", "|") + ")$";
					var maxsize = ew_ConvertToInt(ObjForm.GetValue("maxsize"));
					var maxfilecount = ew_ConvertToInt(ObjForm.GetValue("maxfilecount"));
					var filename = ObjForm.GetUploadFileName(name);
					if (EW_UPLOAD_CONVERT_ACCENTED_CHARS) {
						filename = ew_HtmlEncode(filename);
						filename = Regex.Replace(filename, @"&([a-zA-Z])(uml|acute|grave|circ|tilde|cedil);", "$1");
						filename = ew_HtmlDecode(filename);
					}
					var filetype = ObjForm.GetUploadFileContentType(name);
					var filesize = ObjForm.GetUploadFileSize(name);
					var value = (byte[])ObjForm.GetUploadFileData(name);

					// Check file types
					if (!Regex.IsMatch(filename, filetypes, RegexOptions.IgnoreCase)) {
						var fileerror = Language.Phrase("UploadErrMsgAcceptFileTypes");
						return OutputJSON("files", new List<object[]>() { new object[] { name, filename, filetype, filesize, fileerror }});
					}

					// Check file size
					if (maxsize < filesize) {
						var  fileerror = Language.Phrase("UploadErrMsgMaxFileSize");
						return OutputJSON("files", new List<object[]>() { new object[] { name, filename, filetype, filesize, fileerror }});
					}

					// Check max file count
					var filecount = ew_FolderFileCount(folder);
					if (maxfilecount > 0 && maxfilecount <= filecount) {
						var fileerror = Language.Phrase("UploadErrMsgMaxNumberOfFiles");
						return OutputJSON("files", new List<object[]>() { new object[] { name, filename, filetype, filesize, fileerror }});
					}

					// Delete all files in directory if replace
					var version = EW_UPLOAD_THUMBNAIL_FOLDER;
					if (ObjForm.GetValue("replace") == "1")
	                    ew_CleanPath(folder, false);
					ew_SaveFile(folder, filename, ref value);
					folder = ew_PathCombine(folder, version, true);
					var w = EW_UPLOAD_THUMBNAIL_WIDTH;
					var h = EW_UPLOAD_THUMBNAIL_HEIGHT;
					ew_ResizeBinary(ref value, ref w, ref h);
					ew_SaveFile(folder, filename, ref value);
					return OutputJSON("files", new List<object[]> { new object[] { name, filename, filetype, filesize } });
				}
				return new EmptyResult();
			}

			// Output JSON
			public IActionResult OutputJSON(string id, List<object[]> files) {
				var ar = new List<Dictionary<string, object>>();
				var baseurl = ew_ConvertFullUrl(ew_CurrentPage());
				if (ew_IsList(files)) {
					foreach (var file in files) {
						if (file.Length >= 4) {
							var name = Convert.ToString(file[0]);
							var filename = Convert.ToString(file[1]);
							var fileerror = (file.Length > 4) ? Convert.ToString(file[4]) : "";
							var table = (UploadTable != "") ? Convert.ToString("&table=" + UploadTable) : "";
							var url = baseurl + "?id=" + name + table + "&" + name + "=" + ew_RawUrlEncode(filename) + "&download=1";
							var version = EW_UPLOAD_THUMBNAIL_FOLDER;
							var thumbnail_url = baseurl + "?id=" + name + table + "&" + name + "=" + ew_RawUrlEncode(filename) + "&version=" + version + "&download=1";
							var delete_url = baseurl + "?id=" + name + table + "&" + name + "=" + ew_RawUrlEncode(filename) + "&delete=1";
							var obj = new Dictionary<string, object>();
							obj.Add("name", filename);
							obj.Add("size", ew_ConvertToInt(file[3]));
							obj.Add("type", Convert.ToString(file[2]));
							obj.Add("url", url);
							if (ew_NotEmpty(fileerror)) {
								obj.Add("error", fileerror);
							} else {
								obj.Add(version + "Url", thumbnail_url);
							}
							obj.Add("deleteUrl", delete_url);
							obj.Add("deleteType", "GET"); // Use GET
							ar.Add(obj);
						}
					}
				}

				// Set file header / content type
				ew_AddHeader(HeaderNames.ContentDisposition, "inline; filename=files.json");

				// Output json
				return ew_Controller.Content("{\"" + id + "\":" + ew_ArrayToJson(ar) + "}", "text/plain", Encoding.UTF8); // Returns utf-8 data
			}

			// Page class constructor
			public c_ewupload(Controller controller = null) { // DN
				if (controller != null)
					ew_Controller = controller;
			}

			//  Page init
			public IActionResult Page_Init() {  // DN

				// Header
				ew_Header(false); // DN

				// Global Page Loading event
				//**Page_Loading();

				ew_Response.Clear(); // Clear output
				return null;
			}

			//
			// Page main
			//

			public IActionResult Page_Main() { // DN

				// Handle download file content
				if (ew_Get("download") != "") {
					return DownloadFileContent();
				} else if (ew_Get("delete") != "") { // Handle delete file
					return DeleteFile();
				} else if (ew_Get("id") != "") { // Handle download file list
					return DownloadFileList();
				} else if (ew_Request.Form.Files.Count > 0) { // Handle upload file (multi-part)
					return UploadFile();
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
