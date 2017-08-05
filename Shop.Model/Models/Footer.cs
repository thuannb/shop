using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Models
{
	[Table("Footers")]//Tên bảng tương ứng với CSDL
	public class Footer//Tên Model không cần số nhiều
	{
		[Key]//Khóa chính
		public string ID { set; get; }

		[Required]//Bắt buộc nhập
		public string Content { set; get; }
	}
}