using Shop.Model.Abstract;
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
		public string Name { set; get; }
		[Required]
		public string Alias { set; get; }

		public string ParentID { set; get; }

		public string Description { set; get; }

		public int? DisplayOrder { set; get; }

		public string Image { set; get; }

		public bool? HomeFlag { set; get; }

		//Có khóa là khóa ngoại với bảng Products.
		public virtual IEnumerable<Product> Products { set; get; }
	}
}
