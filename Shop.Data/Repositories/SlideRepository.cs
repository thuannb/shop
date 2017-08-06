using Shop.Data.Infrastructure;
using Shop.Model.Models;

namespace Shop.Data.Repositories
{
	public interface ISlideRepository : IRepository<SupportOnline> { }

	public class SlideRepository : RepositoryBase<SupportOnline>, ISlideRepository
	{
		public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}