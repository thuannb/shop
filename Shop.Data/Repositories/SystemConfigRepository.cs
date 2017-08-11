﻿using Shop.Data.Infrastructure;
using Shop.Model.Models;

namespace Shop.Data.Repositories
{
	public interface ISystemConfigRepository : IRepository<SystemConfig> { }

	public class SystemConfigRepository : RepositoryBase<SystemConfig>, ISystemConfigRepository
	{
		public SystemConfigRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}