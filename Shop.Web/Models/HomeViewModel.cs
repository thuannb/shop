using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Web.Models
{
	//Trong 1 view sử dụng nhiều cái Model
	//Tạo ra 1 viewmodel chung như thế này để gọi
	public class HomeViewModel
	{
		public IEnumerable<SlideViewModel> Slides { set; get; }
		public IEnumerable<ProductViewModel> LastestProducts { set; get; }
		public IEnumerable<ProductViewModel> HotProducts { set; get; }
	}
}