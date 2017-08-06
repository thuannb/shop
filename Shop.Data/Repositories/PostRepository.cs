﻿using Shop.Data.Infrastructure;
using Shop.Model.Models;

namespace Shop.Data.Repositories
{
	public interface IPostRepository : IRepository<Post> { }

	public class PostRepository : RepositoryBase<Post>, IPostRepository
	{
		public PostRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}