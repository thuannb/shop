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
using System.Web.Script.Serialization;

namespace Shop.Web.Api
{
	[RoutePrefix("api/product")]
	[Authorize]
	public class ProductController : ApiControllerBase
    {
		private IProductService _productService;

		public ProductController(IErrorService errorService, IProductService productService)
			: base(errorService)
		{
			this._productService = productService;
		}

		[Route("getall")]
		//Giống như PostMain: POST là Get là lấy ra
		[HttpGet]
		public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
		{
			return CreateHttpResponse(request, () =>
			{
				var totalRow = 0;
				var model = _productService.GetAll(keyword);

				totalRow = model.Count();
				var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

				var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

				var pageinationSet = new PaginationSet<ProductViewModel>()
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
				var model = _productService.GetAll();
				var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
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
				var model = _productService.GetByID(id);
				var responseData = Mapper.Map<Product, ProductViewModel>(model);
				var response = request.CreateResponse(HttpStatusCode.OK, responseData);
				return response;
			});
		}

		[Route("create")]
		//Giống như PostMain: POST là Create
		[HttpPost]
		//Khong can xac thuc
		[AllowAnonymous]
		public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage respone = null;
				if (ModelState.IsValid)
				{
					var newProduct = new Product();
					newProduct.UpdateProduct(productVm);

					newProduct.CreatedDate = DateTime.Now;
					newProduct.CreatedBy = User.Identity.Name;
					_productService.Add(newProduct);
					_productService.Save();

					//Mapp lai view cho nguoi dung
					var responeData = Mapper.Map<Product, ProductViewModel>(newProduct);
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
		public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage respone = null;
				if (ModelState.IsValid)
				{
					var updateProduct = _productService.GetByID(productVm.ID);
					updateProduct.UpdateProduct(productVm);
					updateProduct.UpdatedDate = DateTime.Now;
					updateProduct.UpdatedBy = User.Identity.Name;
					_productService.Update(updateProduct);
					_productService.Save();

					//Mapp lai view cho nguoi dung
					var responeData = Mapper.Map<Product, ProductViewModel>(updateProduct);
					respone = request.CreateResponse(HttpStatusCode.Created, responeData);
				}
				else
				{
					request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
				}
				return respone;
			});
		}

		[Route("delete")]
		[HttpDelete]
		[AllowAnonymous]
		public HttpResponseMessage Delete(HttpRequestMessage request, int id)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage respone = null;
				if (ModelState.IsValid)
				{
					var oldProduct = _productService.Delete(id);
					_productService.Save();

					//Mapp lai view cho nguoi dung
					var responeData = Mapper.Map<Product, ProductViewModel>(oldProduct);
					respone = request.CreateResponse(HttpStatusCode.Created, responeData);
				}
				else
				{
					request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
				}
				return respone;
			});
		}

		/// <summary>
		///  Xóa nhiều bản ghi
		/// </summary>
		/// <param name="request"></param>
		/// <param name="listID"></param>
		/// <returns></returns>
		[Route("deletemulti")]
		[HttpDelete]
		[AllowAnonymous]
		public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listDeleteProduct)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage respone = null;
				if (ModelState.IsValid)
				{
					//Convert string to JSON
					var listDelete = new JavaScriptSerializer().Deserialize<List<int>>(listDeleteProduct);
					foreach (var id in listDelete)
					{
						_productService.Delete(id);
					}
					_productService.Save();

					respone = request.CreateResponse(HttpStatusCode.OK, listDelete.Count());
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
