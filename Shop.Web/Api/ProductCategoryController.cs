using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
	[RoutePrefix("api/productcategory")]
	public class ProductCategoryController : ApiControllerBase
	{
		private IProductCategoryService _productCategoryService;

		public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
			: base(errorService)
		{
			this._productCategoryService = productCategoryService;
		}

		[Route("getall")]
		//Giống như PostMain: POST là Get là lấy ra
		[HttpGet]
		public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
		{
			return CreateHttpResponse(request, () =>
			{
				var totalRow = 0;
				var model = _productCategoryService.GetAll(keyword);

				totalRow = model.Count();
				var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

				var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

				var pageinationSet = new PaginationSet<ProductCategoryViewModel>()
				{
					Items = responseData,
					Page = page,
					TotalCount = totalRow,
					PagesCount = (int)Math.Ceiling((decimal)totalRow / pageSize)
				};

				var response = request.CreateResponse(HttpStatusCode.OK, pageinationSet);
				return response;
			});
		}

		[Route("getallparent")]
		//Giống như PostMain: POST là Get là lấy ra
		[HttpGet]
		public HttpResponseMessage GetAllParent(HttpRequestMessage request)
		{
			return CreateHttpResponse(request, () =>
			{
				var model = _productCategoryService.GetAll();				
				var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
				var response = request.CreateResponse(HttpStatusCode.OK, responseData);
				return response;
			});
		}

		[Route("getByID/{id:int}")]
		//Giống như PostMain: POST là Get là lấy ra
		[HttpGet]
		public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
		{
			return CreateHttpResponse(request, () =>
			{
				var model = _productCategoryService.GetById(id);
				var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
				var response = request.CreateResponse(HttpStatusCode.OK, responseData);
				return response;
			});
		}

		[Route("create")]
		//Giống như PostMain: POST là Create
		[HttpPost]
		//Khong can xac thuc
		[AllowAnonymous]
		public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
		{

			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage respone = null;
				if (ModelState.IsValid)
				{
					var newProductCategory = new ProductCategory();
					newProductCategory.UpdateProductCategory(productCategoryVm);

					newProductCategory.CreatedDate = DateTime.Now;

					_productCategoryService.Add(newProductCategory);
					_productCategoryService.Save();

					//Mapp lai view cho nguoi dung
					var responeData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
					respone = request.CreateResponse(HttpStatusCode.Created, responeData);
				}
				else
				{
					request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
				}
				return respone;
			});
		}

		[Route("update")]
		//Giống như PostMain: POST là Create
		[HttpPut]
		//Khong can xac thuc
		[AllowAnonymous]
		public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage respone = null;
				if (ModelState.IsValid)
				{
					var updateProductCategory = _productCategoryService.GetById(productCategoryVm.ID);
					updateProductCategory.UpdateProductCategory(productCategoryVm);
					updateProductCategory.UpdatedDate = DateTime.Now;

					_productCategoryService.Update(updateProductCategory);
					_productCategoryService.Save();

					//Mapp lai view cho nguoi dung
					var responeData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(updateProductCategory);
					respone = request.CreateResponse(HttpStatusCode.Created, responeData);
				}
				else
				{
					request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
				}
				return respone;
			});
		}
	}
}