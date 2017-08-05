using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Model.Abstract
{
	//Mục đích của Abstrac này là dùng để cho các lớp khác kế thừa các thuộc tính trùng lắp ở các bảng.
	//Ví dụ bảng Products có 4 cột này, bảng Orders cũng có 4 cột này.
	//abstract là 1 lớp ảo
	public abstract class Audittable : IAudittable
	{
		public DateTime? CreatedDate { set; get; }

		[MaxLength(256)]
		public string CreatedBy { set; get; }

		public DateTime? UpdatedDate { set; get; }

		[MaxLength(256)]
		public string UpdatedBy { set; get; }

		public bool Status { set; get; }

		[MaxLength(256)]
		public string MetaDescription { set; get; }

		[MaxLength(256)]
		public string MetaKeyword { set; get; }
	}
}