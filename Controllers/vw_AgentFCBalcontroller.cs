// ASP.NET Maker 2017
// Copyright (c) e.World Technology Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AspNetMaker2017.Models;
using static AspNetMaker2017.Models.DEX;

//
// Controllers
//

namespace AspNetMaker2017.Controllers
{
	public partial class HomeController : Controller
	{

		// list
		[Route("vw_AgentFCBallist/{AgentId?}")]
		[Route("Home/vw_AgentFCBallist/{AgentId?}")]
		public IActionResult vw_AgentFCBallist()
		{

			// Create page object
			vw_AgentFCBal_list = new cvw_AgentFCBal_list(this);

			// Execute page
			return vw_AgentFCBal_list.Page_Init() ?? vw_AgentFCBal_list.Page_Main();
		}
	}
}
