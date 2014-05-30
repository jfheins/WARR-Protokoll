﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ILK_Protokoll.DataLayer;
using ILK_Protokoll.Models;
using ILK_Protokoll.ViewModels;

namespace ILK_Protokoll.Controllers
{
	public class TopicsController : BaseController
	{

		// GET: Topics
		public ActionResult Index()
		{
			var topics = db.Topics.Include(t => t.SessionType).Include(t => t.TargetSessionType);
			return View(topics.ToList());
		}

		// GET: Topics/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Topic topic = db.Topics.Find(id);
			if (topic == null)
			{
				return HttpNotFound();
			}
			return View(topic);
		}

		// GET: Topics/Create
		public ActionResult Create()
		{
			var viewmodel = new TopicEdit
			{
				SessionTypeList = new SelectList(db.SessionTypes, "ID", "Name"),
				TargetSessionTypeList = new SelectList(db.SessionTypes, "ID", "Name"),
				UserList = new SelectList(db.GetUserOrdered(GetCurrentUser()), "ID", "Name")
			};
			return View(viewmodel);
		}

		// POST: Topics/Create
		// Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
		// finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(
			[Bind(Include = "ID,Attachments,Description,Duties,OwnerID,Owner,Priority,Proposal,SessionTypeID,Title,ToDo")]
			TopicEdit input)
		{
			if (ModelState.IsValid)
			{
				var t = new Topic();
				t.IncorporateUpdates(input);
				t.SessionTypeID = input.SessionTypeID;
				foreach (var user in db.SessionTypes.Include(st => st.Attendees).First(st => st.ID == input.SessionTypeID).Attendees)
					t.Votes.Add(new Vote(user, VoteKind.None));

				db.Topics.Add(t);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			input.SessionTypeList = new SelectList(db.SessionTypes, "ID", "Name", input.SessionTypeID);
			input.TargetSessionTypeList = new SelectList(db.SessionTypes, "ID", "Name", input.TargetSessionTypeID);
			input.UserList = new SelectList(db.GetUserOrdered(GetCurrentUser()), "ID", "Name");
			return View(input);
		}

		// GET: Topics/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Topic topic = db.Topics.Find(id);
			if (topic == null)
			{
				return HttpNotFound();
			}

			var viewmodel = TopicEdit.FromTopic(topic);
			viewmodel.SessionTypeList = new SelectList(db.SessionTypes, "ID", "Name");
			viewmodel.TargetSessionTypeList = new SelectList(db.SessionTypes, "ID", "Name");
			viewmodel.UserList = new SelectList(db.GetUserOrdered(GetCurrentUser()), "ID", "Name", viewmodel.OwnerID);

			return View(viewmodel);
		}

		// POST: Topics/Edit/5
		// Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
		// finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(
			[Bind(Include = "ID,Attachments,Description,Duties,OwnerID,Priority,Proposal,TargetSessionTypeID,Title,ToDo")] TopicEdit input)
		{
			if (ModelState.IsValid)
			{
				var topic = db.Topics.Find(input.ID);
				db.TopicHistory.Add(TopicHistory.FromTopic(topic));

				topic.IncorporateUpdates(input);
				topic.TargetSessionTypeID = input.TargetSessionTypeID;
				db.Entry(topic).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			var t = db.Topics.Find(input.ID);
			input.SessionType = t.SessionType;
			input.SessionTypeList = new SelectList(db.SessionTypes, "ID", "Name", input.SessionTypeID);
			input.TargetSessionTypeList = new SelectList(db.SessionTypes, "ID", "Name", input.TargetSessionTypeID);
			input.UserList = new SelectList(db.GetUserOrdered(GetCurrentUser()), "ID", "Name");
			return View(input);
		}

		// GET: Topics/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Topic topic = db.Topics.Find(id);
			if (topic == null)
			{
				return HttpNotFound();
			}
			return View(topic);
		}

		// POST: Topics/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Topic topic = db.Topics.Find(id);
			db.Topics.Remove(topic);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}