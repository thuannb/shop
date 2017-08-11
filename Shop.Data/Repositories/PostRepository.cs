using Shop.Data.Infrastructure;
using Shop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data.Repositories
{
	public interface IPostRepository : IRepository<Post>
	{
		//Viết thêm 1 phương thức lấy danh sách các bài viết thuộc Tag
		IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);
	}

	public class PostRepository : RepositoryBase<Post>, IPostRepository
	{
		public PostRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
		{
			var query = from p in DbContext.Posts
						join pt in DbContext.PostTag
						on p.ID equals pt.PostID
						where pt.TagID == tag && p.Status == true
						orderby p.CreatedDate ascending
						select p;

			totalRow = query.Count();
			query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
			return query;
		}
	}
}