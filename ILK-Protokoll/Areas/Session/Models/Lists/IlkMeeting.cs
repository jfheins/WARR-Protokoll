﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ILK_Protokoll.Areas.Administration.Models;
using ILK_Protokoll.Models;
using ILK_Protokoll.util;

namespace ILK_Protokoll.Areas.Session.Models.Lists
{
	/// <summary>
	///    ILK-Regeltermine
	/// </summary>
	[Table("L_IlkMeeting")]
	public class IlkMeeting : BaseItem
	{
		public IlkMeeting()
		{
			Start = DateTime.Today.AddHours(9);
		}

		[DisplayName("Beginn")]
		[DataType(DataType.DateTime)]
		[FutureDate]
		public DateTime Start { get; set; }


		[DisplayName("Ort")]
		public string Place { get; set; }


		[DisplayName("Sitzungstyp")]
		public virtual SessionType SessionType { get; set; }

		[ForeignKey("SessionType")]
		public int SessionTypeID { get; set; }

		[DisplayName("Verant.")]
		public virtual User Organizer { get; set; }

		[ForeignKey("Organizer")]
		public int OrganizerID { get; set; }

		[DisplayName("Anmerkung")]
		public string Comments { get; set; }
	}
}