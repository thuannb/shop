using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
	[Table("OrderDetails")]
	public class OrderDetail
	{
		[Key]
		[Column(Order = 1)]//1 Table co 2 khoa chinh
		public int OrderID { set; get; }

		[ForeignKey("OrderID")]
		public virtual IEnumerable<Order> Orders { get; set; }

		[Key]
		[Column(Order = 2)]//1 Table co 2 khoa chinh
		public int ProductID { set; get; }

		[ForeignKey("ProductID")]
		public virtual IEnumerable<Product> Products { get; set; }

		public int? Quantity { set; get; }
	}
}