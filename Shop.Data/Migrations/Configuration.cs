namespace Shop.Data.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Model.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Shop.Data.ShopDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(Shop.Data.ShopDbContext context)
		{
			this.CreateUser(context);
			this.CreateProductCategory(context);

		}

		private void CreateProductCategory(ShopDbContext context)
		{
			if (context.ProductCategories.Count() == 0)
			{
				List<ProductCategory> productCategory = new List<ProductCategory>()
			{
				new ProductCategory { Name="Điện lạnh", Alias="dien-lanh", Status=true },
				new ProductCategory { Name="Viễn thông", Alias="vien-thong", Status=true },
				new ProductCategory { Name="Đồ gia dụng", Alias="do-gia-dung", Status=true },
				new ProductCategory { Name="Mỹ phẩm", Alias="my-pham", Status=true }
			};
				context.ProductCategories.AddRange(productCategory);
				context.SaveChanges();
			}
		}

		private void CreateUser(ShopDbContext context)
		{
			//  This method will be called after migrating to the latest version.
			var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShopDbContext()));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShopDbContext()));
			var user = new ApplicationUser()
			{
				UserName = "thuannb",
				Email = "thuannbsts@gmail.com",
				EmailConfirmed = true,
				BirthDay = DateTime.Now,
				FullName = "STSoft"

			};

			manager.Create(user, "123456$");

			if (!roleManager.Roles.Any())
			{
				roleManager.Create(new IdentityRole { Name = "Admin" });
				roleManager.Create(new IdentityRole { Name = "User" });
			}

			var adminUser = manager.FindByEmail("thuannbsts@gmail.com");

			manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
		}
	}
}