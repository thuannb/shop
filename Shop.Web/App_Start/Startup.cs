using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using Shop.Data.Infrastructure;
using Shop.Data;
using Shop.Web.Api;
using Shop.Data.Repositories;
using Shop.Service;
using System.Web.Mvc;
using System.Web.Http;
using Autofac.Integration.WebApi;

[assembly: OwinStartup(typeof(Shop.Web.App_Start.Startup))]

namespace Shop.Web.App_Start
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			//Mục đích: Thay thế mặc định Start của .NET thành cái của mình Register
			ConfigAutofact(app);
		}
		
		private void ConfigAutofact(IAppBuilder app)
		{
			var builder = new ContainerBuilder();
			//Đăng ký khởi tạo các controller
			//Mỗi lần khởi tạo thì nó tự động khởi tạo.
			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			
			//Register cho WEB API Controller
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
			builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
			builder.RegisterType<ShopDbContext>().AsSelf().InstancePerRequest();

			//Repository. Những cái gì là của Repository
			builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();

			//Service
			builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();

			//Gán vào API
			//Hiểu nó là 1 cái thùng chứa
			IContainer container = builder.Build();
			//Dùng DependencyResolver của MVC để set cho Autofac.
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
		}
	}
}
