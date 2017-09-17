using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
	public class HomeController : Controller
	{
		private IProductCategoryService _productCategoryService;
		private IFooterService _footerService;
		private ISlideService _slideService;
		private IProductService _productService;

		public HomeController(IProductCategoryService productCategoryService,
			IFooterService footerService,
			ISlideService slideService,
			IProductService productService)
		{
			_productCategoryService = productCategoryService;
			_footerService = footerService;
			_slideService = slideService;
			_productService = productService;
		}

		public ActionResult Index()
		{
			var model = _slideService.GetSlides();
			var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(model);
			
			var modelLastestProduct = _productService.GetLastestProduct(3);
			var lastestProductViewModel=Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>> (modelLastestProduct);

			var modelHotProduct = _productService.GetHotProduct(3);
			var hotProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(modelHotProduct);

			var homeViewModel = new HomeViewModel();
			homeViewModel.Slides = slideViewModel;
			homeViewModel.LastestProducts = lastestProductViewModel;
			homeViewModel.HotProducts = hotProductViewModel;

			return View(homeViewModel);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		//Tên phương thức trùng với tên View
		[ChildActionOnly]//Thuộc tính này chỉ định không được gọi trang footer.
		public ActionResult Footer()
		{
			var model = _footerService.GetByID();
			var listFooterViewModel = Mapper.Map<Footer, FooterViewModel>(model);
			return PartialView(listFooterViewModel);
		}

		//Tên phương thức trùng với tên View
		[ChildActionOnly]//Thuộc tính này chỉ định không được gọi trang footer.
		public ActionResult Header()
		{
			return PartialView();
		}

		//Tên phương thức trùng với tên View
		[ChildActionOnly]//Thuộc tính này chỉ định không được gọi trang footer.
		public ActionResult Category()
		{
			var model = _productCategoryService.GetAll();
			var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
			return PartialView(listProductCategoryViewModel);
		}
	}
}