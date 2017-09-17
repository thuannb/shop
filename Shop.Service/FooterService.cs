using Shop.Common;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
	public interface IFooterService
	{
		IEnumerable<Footer> GetAll();
		Footer GetByID();
	}
	public class FooterService : IFooterService
	{
		IFooterRepository _footerRepository;
		IUnitOfWork _unitOfWork;
		public FooterService(IFooterRepository footerRepository,IUnitOfWork unitOfWork)
		{
			_footerRepository = footerRepository;
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Footer> GetAll()
		{
			return this._footerRepository.GetAll();
		}

		public Footer GetByID()
		{
			return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
		}
	}
}
