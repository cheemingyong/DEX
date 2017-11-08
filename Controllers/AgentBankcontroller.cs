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
		[Route("AgentBanklist/{id?}")]
		[Route("Home/AgentBanklist/{id?}")]
		public IActionResult AgentBanklist()
		{

			// Create page object
			AgentBank_list = new cAgentBank_list(this);

			// Execute page
			return AgentBank_list.Page_Init() ?? AgentBank_list.Page_Main();
		}
	}
}
