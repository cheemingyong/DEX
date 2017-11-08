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
		[Route("Agentlist/{AgentId?}")]
		[Route("Home/Agentlist/{AgentId?}")]
		public IActionResult Agentlist()
		{

			// Create page object
			Agent_list = new cAgent_list(this);

			// Execute page
			return Agent_list.Page_Init() ?? Agent_list.Page_Main();
		}

		// addopt
		[Route("Agentaddopt")]
		[Route("Home/Agentaddopt")]
		public IActionResult Agentaddopt()
		{

			// Create page object
			Agent_addopt = new cAgent_addopt(this);

			// Execute page
			return Agent_addopt.Page_Init() ?? Agent_addopt.Page_Main();
		}
	}
}
