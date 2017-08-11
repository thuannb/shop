using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.UnitTest.RepositoryTest
{
	[TestClass]
	public class PostCategoryRepositoryTest
	{
		DbFactory dbFactory;
		IPostCategoryRepository postCategoryRepostory;
		IUnitOfWork unitOfWork;

		[TestInitialize]
		public void Initialize()
		{
			dbFactory = new DbFactory();
			postCategoryRepostory = new PostCategoryRepository(dbFactory);
			unitOfWork = new UnitOfWork(dbFactory);
		}

		//Test phuong thuc
		[TestMethod]
		public void PostCategory_Repository_Create()
		{
			PostCategory postCategory = new PostCategory();
			postCategory.Name = "TEST 001 - Name";
			postCategory.Alias = "TEST 001 - Alias";
			postCategory.Status = true;

			var result = postCategoryRepostory.Add(postCategory);
			//Thuc hien vao Database
			unitOfWork.Commit();

			//Khac null la ok.
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void PostCategory_Repository_GetAll()
		{
			var result = postCategoryRepostory.GetAll().ToList();
			//Khac null la ok.
			Assert.AreEqual(3, result.Count);
		}
	}
}
