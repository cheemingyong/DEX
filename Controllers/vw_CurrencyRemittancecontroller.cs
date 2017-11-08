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
		[Route("vw_CurrencyRemittancelist/{CurrencyCode?}")]
		[Route("Home/vw_CurrencyRemittancelist/{CurrencyCode?}")]
		public IActionResult vw_CurrencyRemittancelist()
		{

			// Create page object
			vw_CurrencyRemittance_list = new cvw_CurrencyRemittance_list(this);

			// Execute page
			return vw_CurrencyRemittance_list.Page_Init() ?? vw_CurrencyRemittance_list.Page_Main();
		}

		// delete
		[Route("vw_CurrencyRemittancedelete/{CurrencyCode?}")]
		[Route("Home/vw_CurrencyRemittancedelete/{CurrencyCode?}")]
		public IActionResult vw_CurrencyRemittancedelete()
		{

			// Create page object
			vw_CurrencyRemittance_delete = new cvw_CurrencyRemittance_delete(this);

			// Execute page
			return vw_CurrencyRemittance_delete.Page_Init() ?? vw_CurrencyRemittance_delete.Page_Main();
		}
	}
}
