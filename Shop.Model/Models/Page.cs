﻿using Shop.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
	[Table("Pages")]
	public class Page : Audittable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }

		[Required]
		[MaxLength(256)]
		public string Name { set; get; }

		[Required]
		[Column(TypeName = "varchar")]
		[MaxLength(256)]
		public string Alias { set; get; }

		public string Content { set; get; }
	}
}