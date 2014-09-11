﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ILK_Protokoll.Areas.Administration.Models;
using ILK_Protokoll.Areas.Session.Models.Lists;
using ILK_Protokoll.Models;

namespace ILK_Protokoll.Areas.Session.Models
{
	public class ActiveSession
	{
		public ActiveSession()
		{
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			PresentUsers = new List<User>();
			LockedTopics = new List<TopicLock>();
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
			Start = DateTime.Now;
		}

		public ActiveSession(SessionType type)
			: this()
		{
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			SessionType = type;
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
		}

		public int ID { get; set; }

		[DisplayName("Sitzungsleiter")]
		[Required]
		public virtual User Manager { get; set; }

		[ForeignKey("Manager")]
		public int ManagerID { get; set; }

		[DisplayName("Sitzungstyp")]
		[Required]
		public virtual SessionType SessionType { get; set; }

		[DisplayName("Anwesenheit")]
		[Required]
		public virtual ICollection<User> PresentUsers { get; set; }

		[DisplayName("Weitere Personen")]
		public string AdditionalAttendees { get; set; }

		[DisplayName("Notizen")]
		[DataType(DataType.MultilineText)]
		public string Notes { get; set; }

		[DisplayName("Beginn")]
		[Required]
		public DateTime Start { get; set; }

		[DisplayName("Ende")]

		[NotMapped]
		public DateTime End { get; set; }

		public virtual ICollection<TopicLock> LockedTopics { get; set; }
	}
}