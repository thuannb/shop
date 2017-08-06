using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
	[Table("MenuGroups")]
	public class MenuGroup
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Tự động tăng dần
		public int ID { set; get; }

		[Required]
		[MaxLength(255)]
		public string Name { set; get; }

		//MenuGroups có khóa chính là khóa ngoại của bảng Menus
		//Như vậy khi cần lấy dữ liệu: Tất cả các menu có MenuGroupID.
		public virtual IEnumerable<Menu> Menus { set; get; }
	}
}