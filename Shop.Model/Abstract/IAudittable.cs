using System;

namespace Shop.Model.Abstract
{
	//Mục đích của Abstrac này là dùng để cho các lớp khác kế thừa các thuộc tính trùng lắp ở các bảng.
	//Ví dụ bảng Products có 4 cột này, bảng Orders cũng có 4 cột này.

	public interface IAudittable
	{
		//? cho phép Nullable
		DateTime? CreatedDate { set; get; }

		string CreatedBy { set; get; }

		DateTime? UpdatedDate { set; get; }
		string UpdatedBy { set; get; }

		string MetaKeyword { set; get; }
		string MetaDescription { set; get; }

		bool Status { set; get; }
	}
}