﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using Shop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.UnitTest.ServiceTest
{
	[TestClass]
	public class PostCategoryServiceTest
	{
		//Mock: Giả lập đối tượng
		private Mock<IPostCategoryRepository> _mockRepository;
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private IPostCategoryService _categoryService;
		private List<PostCategory> _listCategory;

		[TestInitialize]
		public void Initialize()
		{
			_mockRepository = new Mock<IPostCategoryRepository>();
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_categoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
			_listCategory = new List<PostCategory>()
			{
				new PostCategory() {ID =1 ,Name="DM1",Status=true },
				new PostCategory() {ID =2 ,Name="DM2",Status=true },
				new PostCategory() {ID =3 ,Name="DM3",Status=true },
			};
		}

		[TestMethod]
		public void PostCategory_Service_GetAll()
		{
			//setup method
			_mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);

			//call action
			var result = _categoryService.GetAll() as List<PostCategory>;

			//compare
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count);
		}

		[TestMethod]
		public void PostCategory_Service_Create()
		{
			PostCategory category = new PostCategory();
			int id = 1;
			category.Name = "Test";
			category.Alias = "test";
			category.Status = true;
			//setup
			_mockRepository.Setup(m => m.Add(category)).Returns((PostCategory p) =>
			{
				p.ID = 1;
				return p;
			});
			//call
			var result = _categoryService.Add(category);
			//compare
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.ID);


		}
	}
}
