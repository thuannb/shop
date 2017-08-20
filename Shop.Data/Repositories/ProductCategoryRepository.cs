using Shop.Data.Infrastructure;
using Shop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data.Repositories
{
	//Thêm mới 1 phương thức chưa tồn tại
	public interface IProductCategoryRepository : IRepository<ProductCategory>
	{
		IEnumerable<ProductCategory> GetByAlias(string alias);
	}

	//Kế thừa tất cả các phương thức: Thêm, xóa, sửa,... từ bên Repository
	public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
	{
		public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public IEnumerable<ProductCategory> GetByAlias(string alias)
		{
			return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
		}
	}
}