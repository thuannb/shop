using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }

		[Required]
		[MaxLength(255)]
		public string CustomerName { set; get; }

		[Required]
		[MaxLength(255)]
		public string CustomerAddress { set; get; }

		[Required]
		[MaxLength(255)]
		public string CustomerEmail { set; get; }

		[Required]
		[MaxLength(50)]
		public string CustomerMobile { set; get; }

		[Required]
		[MaxLength(255)]
		public string CustomerMessage { set; get; }

		[Required]
		[MaxLength(255)]
		public string PaymentMethod { set; get; }

		[Required]
		[MaxLength(255)]
		public string PaymentSatus { set; get; }

		public DateTime? CreatedDate { set; get; }

		[MaxLength(50)]
		public string CreatedBy { set; get; }

		public DateTime? UpdateDate { set; get; }

		[MaxLength(50)]
		public string UpdatedBy { set; get; }

		[Required]
		public bool Status { set; get; }

		//Khoa ngoai
		public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }
	}
}