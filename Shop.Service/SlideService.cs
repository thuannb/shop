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
	public interface ISlideService
	{
		IEnumerable<Slide> GetSlides();
	}
	public class SlideService : ISlideService
	{
		ISlideRepository _slideRepository;
		IUnitOfWork _unitOfWork;
		public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
		{
			_slideRepository = slideRepository;
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Slide> GetSlides()
		{
			return _slideRepository.GetMulti(x => x.Status);
		}
	}
}
