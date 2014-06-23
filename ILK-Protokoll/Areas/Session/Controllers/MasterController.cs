﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILK_Protokoll.Areas.Session.Models;
using ILK_Protokoll.Controllers;

namespace ILK_Protokoll.Areas.Session.Controllers
{
	public class MasterController : BaseController
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			ViewBag.SMasterStyle = "active";
		}

		// GET: Session/Master
		public ActionResult Index()
		{
			return View(new ActiveSession(db.SessionTypes.First()));
		}
	}
}