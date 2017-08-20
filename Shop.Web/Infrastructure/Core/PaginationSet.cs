﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Web.Infrastructure.Core
{
	public class PaginationSet<T>
	{
		public int Page { get; set; }
		public int PagesCount { get; set; }
		public int TotalCount { get; set; }
		public IEnumerable<T> Items { get; set; }

		public int Count {
			get {
				return Items != null ? Items.Count() : 0;
			}
		}
	}
}