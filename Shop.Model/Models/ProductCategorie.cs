﻿using Shop.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Models
{
	[Table("ProductCategories")]
	public class ProductCategorie : Audittable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Tự động tăng dần
		public int ID { set; get; }

		[Required]
		[MaxLength(255)]
		public string Name { set; get; }

		[Required]
		[Column(TypeName ="varchar")]
		[MaxLength(255)]
		public string Alias { set; get; }

		public int? ParentID { set; get; }

		[MaxLength(500)]
		public string Description { set; get; }

		public int? DisplayOrder { set; get; }

		[MaxLength(500)]
		public string Image { set; get; }

		public bool? HomeFlag { set; get; }

		//Có khóa là khóa ngoại với bảng Products.
		public virtual IEnumerable<Product> Products { set; get; }
	}
}
