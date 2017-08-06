using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
	[Table("Menus")]
	public class Menu
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Tự động tăng dần
		public int ID { set; get; }

		[Required]
		[MaxLength(255)]
		public string Name { set; get; }

		[Required]
		[MaxLength(500)]
		public string URL { set; get; }

		[Required]
		public int MenuGroupID { set; get; }

		//Nếu là khóa ngoại của bảng khác. Chúng ta sử dụng virtual
		[ForeignKey("MenuGroupID")]
		public virtual IEnumerable<MenuGroup> MenuGroups { set; get; }

		[Required]
		public int DisplayOrder { set; get; }

		//Dấu chẩm hỏi là cho phép Nullable
		public int? Target { set; get; }

		[Required]
		public bool Status { set; get; }
	}
}