﻿using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ILK_Protokoll.Controllers;

namespace ILK_Protokoll.Areas.Administration.Controllers
{
	public class RecycleBinController : BaseController
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			ViewBag.AdminStyle = "active";
			ViewBag.ARBStyle = "active";
		}

		// GET: Administration/RecycleBin
		public ActionResult Index()
		{
			var items =
				db.Attachments.Where(a => a.Deleted != null).Include(a => a.Topic).OrderByDescending(a => a.Created).ToList();
			return View(items);
		}


		public ActionResult _Restore(int attachmentID)
		{
			var attachment = db.Attachments.Include(a => a.Uploader).Single(a => a.ID == attachmentID);

			if (attachment.TopicID == null && attachment.EmployeePresentationID == null) // Verwaist
				return HTTPStatus(422, "Wiederherstellungsziel ist nicht mehr vorhanden");

			attachment.Deleted = null;

			try
			{
				db.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				var message = ErrorMessageFromException(e);
				return HTTPStatus(500, message);
			}

			return new HttpStatusCodeResult(HttpStatusCode.NoContent);
		}

		[HttpGet]
		public ActionResult Purge()
		{
			var itemcount = db.Attachments.Count(a => a.Deleted != null);
			return View(itemcount);
		}

		[HttpPost, ActionName("Purge")]
		[ValidateAntiForgeryToken]
		public ActionResult PurgeConfirmed()
		{
			db.Attachments.RemoveRange(db.Attachments.Where(a => a.Deleted != null));
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}