﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Web.Models
{
	public class SlideViewModel
	{
		public string Name { set; get; }
		
		public string Description { set; get; }
		
		public string Image { set; get; }
		
		public string Url { set; get; }

		public int? DisplayOrder { set; get; }
		
		public bool Status { set; get; }

		public string Content { set; get; }
	}
}