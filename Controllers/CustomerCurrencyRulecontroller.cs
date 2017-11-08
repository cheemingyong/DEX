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
		[Route("CustomerCurrencyRulelist/{id?}")]
		[Route("Home/CustomerCurrencyRulelist/{id?}")]
		public IActionResult CustomerCurrencyRulelist()
		{

			// Create page object
			CustomerCurrencyRule_list = new cCustomerCurrencyRule_list(this);

			// Execute page
			return CustomerCurrencyRule_list.Page_Init() ?? CustomerCurrencyRule_list.Page_Main();
		}

		// add
		[Route("CustomerCurrencyRuleadd/{id?}")]
		[Route("Home/CustomerCurrencyRuleadd/{id?}")]
		public IActionResult CustomerCurrencyRuleadd()
		{

			// Create page object
			CustomerCurrencyRule_add = new cCustomerCurrencyRule_add(this);

			// Execute page
			return CustomerCurrencyRule_add.Page_Init() ?? CustomerCurrencyRule_add.Page_Main();
		}

		// edit
		[Route("CustomerCurrencyRuleedit/{id?}")]
		[Route("Home/CustomerCurrencyRuleedit/{id?}")]
		public IActionResult CustomerCurrencyRuleedit()
		{

			// Create page object
			CustomerCurrencyRule_edit = new cCustomerCurrencyRule_edit(this);

			// Execute page
			return CustomerCurrencyRule_edit.Page_Init() ?? CustomerCurrencyRule_edit.Page_Main();
		}

		// delete
		[Route("CustomerCurrencyRuledelete/{id?}")]
		[Route("Home/CustomerCurrencyRuledelete/{id?}")]
		public IActionResult CustomerCurrencyRuledelete()
		{

			// Create page object
			CustomerCurrencyRule_delete = new cCustomerCurrencyRule_delete(this);

			// Execute page
			return CustomerCurrencyRule_delete.Page_Init() ?? CustomerCurrencyRule_delete.Page_Main();
		}
	}
}
