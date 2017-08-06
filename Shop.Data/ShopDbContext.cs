using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
	public class ShopDbContext : DbContext
	{
		//Chuoi ket noi
		public ShopDbContext() : base("ShopConnection")
		{
			//Khi Load dữ liệu bảng cha thì không Load dữ liệu bảng con.
			this.Configuration.LazyLoadingEnabled = false;
		}
		//Có bấy nhiêu bảng dữ liệu từ bên Model thì khai báo ra
		public DbSet<Footer> Footers { set; get; }
		public DbSet<Menu> Menus { set; get; }
		public DbSet<MenuGroup> MenuGroups { set; get; }
		public DbSet<Order> Order { set; get; }
		public DbSet<OrderDetail> OrderDetails { set; get; }
		public DbSet<Page> Pages { set; get; }
		public DbSet<Post> Posts { set; get; }
		public DbSet<PostCategory> PostCategories { set; get; }
		public DbSet<PostTag> PostTag { set; get; }
		public DbSet<Product> Products { set; get; }
		public DbSet<ProductCategory> ProductCategories { set; get; }
		public DbSet<ProductTag> ProductTags { set; get; }
		public DbSet<Slide> Slides { set; get; }
		public DbSet<SupportOnline> SupportOnlines { set; get; }
		public DbSet<SystemConfig> SystemConfig { set; get; }
		public DbSet<Tag> Tags { set; get; }
		public DbSet<VisitorStatistic> VisitorStatistic { set; get; }

		//Ghi đè: Khi chạy chương trình sẽ tạo ra Entities Framework
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
