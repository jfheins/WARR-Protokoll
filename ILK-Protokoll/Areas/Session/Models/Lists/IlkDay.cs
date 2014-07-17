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
	///    Klausur-Tage
	/// </summary>
	[Table("L_IlkDay")]
	public class IlkDay : BaseItem
	{
		public IlkDay()
		{
			Start = DateTime.Today.AddHours(9);
			End = DateTime.Today.AddHours(17);
		}

		[DisplayName("Beginn")]
		[DataType(DataType.DateTime)]
		[FutureDate(ErrorMessage = "Das Startdatum muss in der Zukunft liegen. ")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime Start { get; set; }

		[DisplayName("Ende")]
		[DataType(DataType.DateTime)]
		[FutureDate(ErrorMessage = "Das Enddatum muss in der Zukunft liegen. ")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime End { get; set; }

		[DisplayName("Ort")]
		public string Place { get; set; }

		[DisplayName("Sitzung")]
		public virtual SessionType SessionType { get; set; }

		[ForeignKey("SessionType")]
		public int SessionTypeID { get; set; }

		[DisplayName("Organisator")]
		public virtual User Organizer { get; set; }

		[ForeignKey("Organizer")]
		public int OrganizerID { get; set; }

		[DisplayName("Themen")]
		public string Topics { get; set; }

		[DisplayName("Teilnehmer")]
		public string Participants { get; set; }
	}
}