using Shop.Model.Models;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Web.Infrastructure.Extensions
{
	public static class EntityExtensions
	{
		//Những thằng Virtual không gán vào.
		//Từ khóa this có ý nghĩa: Phương thức này có 2 tham số, tham số có chữ this là tham số sẽ nhận giá trị từ tham số kia.
		//Class Extension này dùng để viết thêm các thuộc tính set hoặc get?...
		public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
		{
			postCategory.ID = postCategoryVm.ID;
			postCategory.Name = postCategoryVm.Name;
			postCategory.Description = postCategoryVm.Description;
			postCategory.Alias = postCategoryVm.Alias;
			postCategory.ParentID = postCategoryVm.ParentID;
			postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
			postCategory.Image = postCategoryVm.Image;
			postCategory.HomeFlag = postCategoryVm.HomeFlag;

			postCategory.CreatedDate = postCategoryVm.CreatedDate;
			postCategory.CreatedBy = postCategoryVm.CreatedBy;
			postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
			postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
			postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
			postCategory.MetaDescription = postCategoryVm.MetaDescription;
			postCategory.Status = postCategoryVm.Status;

		}

		public static void UpdatePost(this Post post, PostViewModel postVm)
		{
			post.ID = postVm.ID;
			post.Name = postVm.Name;
			post.Description = postVm.Description;
			post.Alias = postVm.Alias;
			post.PostCategoryID = postVm.CategoryID;
			post.Content = postVm.Content;
			post.Image = postVm.Image;
			post.HomeFlag = postVm.HomeFlag;
			post.ViewCount = postVm.ViewCount;

			post.CreatedDate = postVm.CreatedDate;
			post.CreatedBy = postVm.CreatedBy;
			post.UpdatedDate = postVm.UpdatedDate;
			post.UpdatedBy = postVm.UpdatedBy;
			post.MetaKeyword = postVm.MetaKeyword;
			post.MetaDescription = postVm.MetaDescription;
			post.Status = postVm.Status;
		}
	}
}