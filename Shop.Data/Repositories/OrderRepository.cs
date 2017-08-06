using Shop.Data.Infrastructure;
using Shop.Model.Models;

namespace Shop.Data.Repositories
{
	public interface IOrderRepository : IRepository<Order> { }

	public class OrderRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
	{
		public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}