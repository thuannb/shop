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
using Shop.Model.Models;
using System.Web;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Shop.Web.App_Start.Startup))]

namespace Shop.Web.App_Start
{
	//Trong 1 project có 2 class trùng tên nhau: Startup. thì để partial
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			//Mục đích: Thay thế mặc định Start của .NET thành cái của mình Register
			ConfigAutofact(app);
			//Call đến Class Startup kia(Startup.Auth). Vì vậy 1 tên class trùng nhau thì mình đặt là partial.
			ConfigureAuth(app);
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

			//Asp.net Identity
			builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
			builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
			builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
			builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
			builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

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
