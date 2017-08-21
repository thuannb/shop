﻿using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
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
	}
}