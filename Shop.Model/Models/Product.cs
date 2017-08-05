﻿using Shop.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Model.Models
{
	[Table("Products")]
	public class Product : Audittable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Tự động tăng dần
		public int ID { set; get; }
		[Required]
		public string Name { set; get; }

		[Required]
		public string Alias { set; get; }
		
		public int ProductCategoryID { set; get; }
		//Khoá ngoại
		[ForeignKey("ProductCategoryID")]
		public virtual IEnumerable<Product> ProductCategory { set; get; }

		public decimal Price { set; get; }

		public decimal? PromotionPrice { set; get; }

		public int? Warranty { set; get; }

		public string Description { set; get; }

		public string Content { set; get; }

		public XElement MoreImages { set; get; }

		public string Image { set; get; }

		public bool? HomeFlag { set; get; }

		public bool? HotFlag { set; get; }

		public int? ViewCount { set; get; }



	}
}
