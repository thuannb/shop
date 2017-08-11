using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
	[RoutePrefix("api/postCategoryController")]
	public class PostCategoryController : ApiControllerBase
	{
		private IPostCategoryService _postCategoryService;

		//Khởi tạo constructor kế thừa ApiControllerBase
		public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) :
			base(errorService)
		{
			this._postCategoryService = postCategoryService;
		}

		[Route("getAll")]
		public HttpResponseMessage Get(HttpRequestMessage request)
		{
			return CreateHttpResponse(request, () =>
			{
				var listCategory = _postCategoryService.GetAll();
				//Đối tượng trả về là List<PostCategoryViewModel>
				var listCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(listCategory);
				HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategoryVM);
				return response;
			});
		}

		[Route("add")]
		public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;

				if (ModelState.IsValid)
				{
					PostCategory newPostCategory = new PostCategory();
					//Phương thức UpdatePostCategory là 1 phương thức Extenssion : tham khảo có truyển từ this
					newPostCategory.UpdatePostCategory(postCategoryVm);
					var postCategory = _postCategoryService.Add(newPostCategory);

					_postCategoryService.Save();
					response = request.CreateResponse(HttpStatusCode.Created, postCategory);
				}
				else
				{
					request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
				}

				return response;
			});
		}

		[Route("update")]
		public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;

				if (ModelState.IsValid)
				{
					var postCategoryDB = _postCategoryService.GetById(postCategoryVM.ID);
					postCategoryDB.UpdatePostCategory(postCategoryVM);

					_postCategoryService.Update(postCategoryDB);
					_postCategoryService.Save();

					response = request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
				}

				return response;
			});
		}

		public HttpResponseMessage Delete(HttpRequestMessage request, int id)
		{
			return CreateHttpResponse(request, () =>
			{
				HttpResponseMessage response = null;

				if (ModelState.IsValid)
				{
					_postCategoryService.Delete(id);
					_postCategoryService.Save();
					response = request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
				}

				return response;
			});
		}
	}
}