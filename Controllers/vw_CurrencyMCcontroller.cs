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
		[Route("vw_CurrencyMClist/{CurrencyCode?}")]
		[Route("Home/vw_CurrencyMClist/{CurrencyCode?}")]
		public IActionResult vw_CurrencyMClist()
		{

			// Create page object
			vw_CurrencyMC_list = new cvw_CurrencyMC_list(this);

			// Execute page
			return vw_CurrencyMC_list.Page_Init() ?? vw_CurrencyMC_list.Page_Main();
		}
	}
}
