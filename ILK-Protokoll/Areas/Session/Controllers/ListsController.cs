﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILK_Protokoll.Controllers;

namespace ILK_Protokoll.Areas.Session.Controllers
{
	public class ListsController : BaseController
	{
		// GET: Session/List
		public ActionResult Index()
		{
			return View();
		}
	}
}