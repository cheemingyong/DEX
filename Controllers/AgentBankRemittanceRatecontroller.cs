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
		[Route("AgentBankRemittanceRatelist/{id?}")]
		[Route("Home/AgentBankRemittanceRatelist/{id?}")]
		public IActionResult AgentBankRemittanceRatelist()
		{

			// Create page object
			AgentBankRemittanceRate_list = new cAgentBankRemittanceRate_list(this);

			// Execute page
			return AgentBankRemittanceRate_list.Page_Init() ?? AgentBankRemittanceRate_list.Page_Main();
		}

		// add
		[Route("AgentBankRemittanceRateadd/{id?}")]
		[Route("Home/AgentBankRemittanceRateadd/{id?}")]
		public IActionResult AgentBankRemittanceRateadd()
		{

			// Create page object
			AgentBankRemittanceRate_add = new cAgentBankRemittanceRate_add(this);

			// Execute page
			return AgentBankRemittanceRate_add.Page_Init() ?? AgentBankRemittanceRate_add.Page_Main();
		}

		// edit
		[Route("AgentBankRemittanceRateedit/{id?}")]
		[Route("Home/AgentBankRemittanceRateedit/{id?}")]
		public IActionResult AgentBankRemittanceRateedit()
		{

			// Create page object
			AgentBankRemittanceRate_edit = new cAgentBankRemittanceRate_edit(this);

			// Execute page
			return AgentBankRemittanceRate_edit.Page_Init() ?? AgentBankRemittanceRate_edit.Page_Main();
		}

		// delete
		[Route("AgentBankRemittanceRatedelete/{id?}")]
		[Route("Home/AgentBankRemittanceRatedelete/{id?}")]
		public IActionResult AgentBankRemittanceRatedelete()
		{

			// Create page object
			AgentBankRemittanceRate_delete = new cAgentBankRemittanceRate_delete(this);

			// Execute page
			return AgentBankRemittanceRate_delete.Page_Init() ?? AgentBankRemittanceRate_delete.Page_Main();
		}
	}
}
