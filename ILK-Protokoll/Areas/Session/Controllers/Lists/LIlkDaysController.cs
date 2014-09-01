﻿using System;
using System.Diagnostics;
using System.Text;
using System.Web.Mvc;
using ILK_Protokoll.Areas.Session.Models.Lists;
using ILK_Protokoll.util;

namespace ILK_Protokoll.Areas.Session.Controllers.Lists
{
	public class LIlkDaysController : ParentController<IlkDay>
	{
		public LIlkDaysController()
		{
			_dbSet = db.LIlkDays;
		}

		public override PartialViewResult _CreateForm()
		{
			ViewBag.UserList = new SelectList(db.GetUserOrdered(GetCurrentUser()), "ID", "ShortName");
			ViewBag.SessionTypeList = new SelectList(db.GetActiveSessionTypes(), "ID", "Name");

			return base._CreateForm();
		}

		public override ActionResult _BeginEdit(int id)
		{
			ViewBag.UserList = new SelectList(db.GetUserOrdered(GetCurrentUser()), "ID", "ShortName");
			ViewBag.SessionTypeList = new SelectList(db.GetActiveSessionTypes(), "ID", "Name");
			return base._BeginEdit(id);
		}
		
		public override ActionResult Download(int id)
		{
			Debug.Assert(Request.Url != null, "Request.Url != null");

			var ilkDay = _dbSet.Find(id);
			if (ilkDay.GUID == null)
			{
				ilkDay.GUID = Guid.NewGuid();
				db.SaveChanges();
			}

			var ical = CreateCalendarEvent("ILK-Tag: " + ilkDay.Topics.Shorten(50),
				ilkDay.Topics + "\r\n\r\nTeilnehmer: " + ilkDay.Participants + "\r\n\r\nhttp://" + Request.Url.Authority + Url.Content("~/ViewLists#ilkDay_table"),
				ilkDay.Start, ilkDay.End,
				ilkDay.Place, ilkDay.GUID.ToString(), false);

			return Content(ical, "text/calendar", Encoding.UTF8);
		}
	}
}