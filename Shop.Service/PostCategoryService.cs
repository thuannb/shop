using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System.Collections.Generic;
using System;

namespace Shop.Service
{
	public interface IPostCategoryService
	{
		PostCategory Add(PostCategory postCategory);

		void Update(PostCategory postCategory);

		PostCategory Delete(int id);

		IEnumerable<PostCategory> GetAll();

		IEnumerable<PostCategory> GetAllByParentId(int id);

		PostCategory GetById(int id);

		void Save();
	}

	public class PostCategoryService : IPostCategoryService
	{
		private IPostCategoryRepository _postCategoryRepository;
		private IUnitOfWork _unitOfWork;

		public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
		{
			this._postCategoryRepository = postCategoryRepository;
			this._unitOfWork = unitOfWork;
		}

		public PostCategory Add(PostCategory postCategory)
		{
			return this._postCategoryRepository.Add(postCategory);
		}

		public PostCategory Delete(int id)
		{
			return this._postCategoryRepository.Delete(id);
		}

		public IEnumerable<PostCategory> GetAll()
		{
			return _postCategoryRepository.GetAll();
		}

		public IEnumerable<PostCategory> GetAllByParentId(int parentId)
		{
			return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
		}

		public PostCategory GetById(int id)
		{
			return _postCategoryRepository.GetSingleById(id);
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void Update(PostCategory postCategory)
		{
			_postCategoryRepository.Update(postCategory);
		}
	}
}