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
		[Route("CurrencyRulelist/{id?}")]
		[Route("Home/CurrencyRulelist/{id?}")]
		public IActionResult CurrencyRulelist()
		{

			// Create page object
			CurrencyRule_list = new cCurrencyRule_list(this);

			// Execute page
			return CurrencyRule_list.Page_Init() ?? CurrencyRule_list.Page_Main();
		}

		// add
		[Route("CurrencyRuleadd/{id?}")]
		[Route("Home/CurrencyRuleadd/{id?}")]
		public IActionResult CurrencyRuleadd()
		{

			// Create page object
			CurrencyRule_add = new cCurrencyRule_add(this);

			// Execute page
			return CurrencyRule_add.Page_Init() ?? CurrencyRule_add.Page_Main();
		}

		// edit
		[Route("CurrencyRuleedit/{id?}")]
		[Route("Home/CurrencyRuleedit/{id?}")]
		public IActionResult CurrencyRuleedit()
		{

			// Create page object
			CurrencyRule_edit = new cCurrencyRule_edit(this);

			// Execute page
			return CurrencyRule_edit.Page_Init() ?? CurrencyRule_edit.Page_Main();
		}

		// delete
		[Route("CurrencyRuledelete/{id?}")]
		[Route("Home/CurrencyRuledelete/{id?}")]
		public IActionResult CurrencyRuledelete()
		{

			// Create page object
			CurrencyRule_delete = new cCurrencyRule_delete(this);

			// Execute page
			return CurrencyRule_delete.Page_Init() ?? CurrencyRule_delete.Page_Main();
		}
	}
}
