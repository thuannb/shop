using Shop.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		[Column(TypeName = "varchar")]
		[MaxLength(255)]
		public string Alias { set; get; }

		public int ProductCategoryID { set; get; }

		//Khoá ngoại
		[ForeignKey("ProductCategoryID")]
		public virtual ProductCategory ProductCategory { set; get; }

		public decimal Price { set; get; }

		public decimal? PromotionPrice { set; get; }

		public int? Warranty { set; get; }

		[MaxLength(500)]
		public string Description { set; get; }

		public string Content { set; get; }

		[Column(TypeName = "xml")]
		public string MoreImages { set; get; }

		[MaxLength(500)]
		public string Image { set; get; }

		public bool? HomeFlag { set; get; }

		public bool? HotFlag { set; get; }

		public int? ViewCount { set; get; }

		public string Tags { set; get; }
	}
}