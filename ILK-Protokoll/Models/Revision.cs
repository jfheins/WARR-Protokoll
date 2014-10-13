﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ILK_Protokoll.Models
{
	public class Revision
	{
		public int ID { get; set; }

		public Document ParentDocument { get; set; }
				
		[ForeignKey("ParentDocument")]
		public int ParentDocumentID { get; set; }

		[Required]
		[Display(Name = "Uploaddatum")]
		public DateTime Created { get; set; }

		[Display(Name = "Ersteller")]
		public virtual User Uploader { get; set; }

		[ForeignKey("Uploader")]
		public int UploaderID { get; set; }

		public Guid Guid { get; set; }

		/// <summary>
		///    Enthält den sicheren Namen der für die Speicherung auf dem Server verwendet wird. Alle unsicheren Zeichen wurden
		///    entfernt.
		/// </summary>
		[Required(AllowEmptyStrings = true)]
		[ScaffoldColumn(false)]
		public string SafeName { get; set; }

		/// <summary>
		///    Enthält die Dateiendung ohne führenden Punkt.
		/// </summary>
		[Required(AllowEmptyStrings = true)]
		[ScaffoldColumn(false)]
		public string Extension { get; set; }

		public string FileName
		{
			get
			{
				if (string.IsNullOrWhiteSpace(Extension))
					return ID + "_" + SafeName;
				else
					return ID + "_" + SafeName + '.' + Extension;
			}
		}
	}
}