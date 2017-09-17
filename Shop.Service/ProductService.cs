using Shop.Common;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
	public interface IProductService
	{
		Product Add(Product product);
		Product Delete(int id);
		void Update(Product product);
		IEnumerable<Product> GetAll();
		IEnumerable<Product> GetAll(string keyword);
		IEnumerable<Product> GetLastestProduct(int top);
		IEnumerable<Product> GetHotProduct(int top);
		Product GetByID(int id);
		void Save();
	}
	public class ProductService : IProductService
	{
		IProductRepository _productRepository;
		ITagRepository _tagRepository;
		IProductTagRepository _productTagRepository;

		IUnitOfWork _unitOfWork;

		public ProductService(IProductRepository productRepostory, IProductTagRepository productTagRepository,
			ITagRepository tagRepository, IUnitOfWork unitOfWork)
		{
			this._productRepository = productRepostory;
			this._tagRepository = tagRepository;
			this._productTagRepository = productTagRepository;
			this._unitOfWork = unitOfWork;
		}

		public Product Add(Product product)
		{
			var productRes = this._productRepository.Add(product);
			_unitOfWork.Commit();

			if (!string.IsNullOrEmpty(product.Tags))
			{
				string[] tags = product.Tags.Split(',');
				for (int i = 0; i < tags.Length; i++)
				{
					string tagId = StringHelper.ToUnsignString(tags[i]);
					if (this._productTagRepository.Count(x => x.TagID == tagId) == 0)
					{
						Tag tag = new Tag();
						tag.ID = tagId;
						tag.Name = tags[i].Trim();
						tag.Type = CommonConstants.ProductTag;

						this._tagRepository.Add(tag);
					}

					ProductTag productTag = new ProductTag();
					productTag.ProductID = product.ID;
					productTag.TagID = tagId;

					this._productTagRepository.Add(productTag);
				}
			}

			return productRes;
		}

		public Product Delete(int id)
		{
			return this._productRepository.Delete(id);
		}

		public IEnumerable<Product> GetAll()
		{
			return this._productRepository.GetAll();
		}

		public IEnumerable<Product> GetAll(string keyword)
		{
			if (!string.IsNullOrEmpty(keyword))
				return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
			else
				return _productRepository.GetAll();
		}

		public Product GetByID(int id)
		{
			return this._productRepository.GetSingleById(id);
		}

		public void Save()
		{
			this._unitOfWork.Commit();
		}

		public void Update(Product product)
		{
			this._productRepository.Update(product);

			if (!string.IsNullOrEmpty(product.Tags))
			{
				string[] tags = product.Tags.Split(',');
				for (int i = 0; i < tags.Length; i++)
				{
					string tagId = StringHelper.ToUnsignString(tags[i]);
					if (this._productTagRepository.Count(x => x.TagID == tagId) == 0)
					{
						Tag tag = new Tag();
						tag.ID = tagId;
						tag.Name = tags[i].Trim();
						tag.Type = CommonConstants.ProductTag;

						this._tagRepository.Add(tag);
					}

					//Xoa truoc khi update
					this._productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
					ProductTag productTag = new ProductTag();
					productTag.ProductID = product.ID;
					productTag.TagID = tagId;

					this._productTagRepository.Add(productTag);
				}
			}
		}

		public IEnumerable<Product> GetLastestProduct(int top)
		{
			return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedBy).Take(top);
		}

		public IEnumerable<Product> GetHotProduct(int top)
		{
			return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedBy).Take(top);
		}
	}
}
