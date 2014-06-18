﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILK_Protokoll.Controllers;

namespace ILK_Protokoll.Areas.Session.Controllers
{
	public class MasterController : BaseController
	{
		// GET: Session/Master
		public ActionResult Index()
		{
			return View(new Models.ActiveSession(db.SessionTypes.First()));
		}
	}
}