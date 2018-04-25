using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.data.Interfaces;

namespace refactor_me.data
{
    public class ProductDataService : IProductDataService
    {
        private readonly RefactorMeEntities _entities;

        public ProductDataService(RefactorMeEntities entities)
        {
            _entities = entities;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _entities.Products.ToList();
        }
        

        public IEnumerable<Product> SearchByName(string name)
        {
            return _entities.Products.Where(x => x.Name.Contains(name));
        }

        public Product GetProduct(Guid id)
        {
            return _entities.Products.SingleOrDefault(x => x.Id == id);
        }

        public void Create(Product product)
        {
            _entities.Products.Add(product);
            _entities.SaveChanges();
        }

        public void Update(Product product)
        {
            var entity = _entities.Products.Single(x => x.Id == product.Id);
            entity.DeliveryPrice = product.DeliveryPrice;
            entity.Description = product.Description;
            entity.Name = product.Name;
            entity.Price = product.Price;
            _entities.SaveChanges();

        }

        public void Delete(Guid id)
        {
            var entity = _entities.Products.Single(x => x.Id == id);
            foreach (var option in entity.ProductOptions.ToList())
            {
                _entities.ProductOptions.Remove(option);
            }
               
            _entities.Products.Remove(entity);
            _entities.SaveChanges();
        }

        public IEnumerable<ProductOption> GetOptions(Guid productId)
        {
            return _entities.ProductOptions.Where(x => x.ProductId == productId);
        }

        public ProductOption GetOption(Guid productId, Guid id)
        {
            return _entities.ProductOptions
                .SingleOrDefault(x => x.ProductId == productId && x.Id == id);
        }

        public void CreateOption(Guid productId, ProductOption option)
        {
            _entities.ProductOptions.Add(option);
            _entities.SaveChanges();
        }

        public void UpdateOption(ProductOption option)
        {
            var entity = _entities.ProductOptions.Single(x => x.Id == option.Id);
           
            entity.Description = option.Description;
            entity.Name = option.Name;
            entity.ProductId = option.ProductId;
         
            _entities.SaveChanges();
        }

        public void DeleteOption(Guid productId, Guid id)
        {
            var entity = _entities.ProductOptions.Single(x =>x.ProductId == productId && x.Id == id);
            _entities.ProductOptions.Remove(entity);
            _entities.SaveChanges();
        }
    }
}
