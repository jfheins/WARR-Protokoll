﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using ILK_Protokoll.Models;
using ILK_Protokoll.util;

namespace ILK_Protokoll.DataLayer
{
	public class DataContext : DbContext
	{
		public DataContext() : base("DataContext")
		{
		}

		public DbSet<SessionType> SessionTypes { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<Topic> Topics { get; set; }
		public DbSet<TopicHistory> TopicHistory { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Vote> Votes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		/// <summary>
		/// Gibt alle Benutzer zurück, mit dem aktuellen Benutzer als erstes Element und den restlichen benutzern in alphabetischer Reihenfolge.
		/// </summary>
		/// <param name="currentUser"></param>
		/// <returns></returns>
		public IEnumerable<User> GetUserOrdered(User currentUser)
		{
			return
				currentUser.ToEnumerable()
					.Concat(Users.Where(u => u.ID != currentUser.ID).ToList().OrderBy(u => u.Name, StringComparer.CurrentCultureIgnoreCase));
		}
		
		public Topic GetTopicAt(int id, DateTime time)
		{
			var t = Topics.Find(id);
			if (time > t.ValidFrom)
				return t;
			else if (time < t.Created)
				return null;
			else
			{
				var histdata = TopicHistory.Single(x => x.ValidFrom < time && x.ValidUntil > time);
				t.IncorporateHistory(histdata);
				return t;
			}
		}
	}
}